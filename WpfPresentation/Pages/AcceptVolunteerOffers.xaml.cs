/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/14
/// Summary:  Interaction logic for AcceptVolunteerOffers.xaml
/// </summary>
/// <remarks>
/// Updated By: Stan Anderson
/// Updated: 2025/02/14
/// What was Changed: Added connections	
/// 
/// Updated By: Kate Rich
/// Updated: 2025-04-14
/// What Was Changed:
///     Added the btnBackgroundCheck_Click method to redirect users
///     to a page where they can submit a new Background Check record;
///     also, added in functionality for this method to work properly.
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed:
///     Added Automation Logic to the Accept & Reject button click event handlers:
///         On Accept, automatically grant the newly approved Volunteer WriteAccess to the project's Forum
///         & assign them the ProjectRole of 'Volunteer'.
///         On Reject, automatically revoke the Volunteer's WriteAccess to the project's Forum (if it exists)
///         & delete any existing ProjectRoles they may have had.
/// </remarks>

using DataDomain;
using LogicLayer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class AcceptVolunteerOffers : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        int _project;
        public ObservableCollection<VMVolunteerStatus> _volunteerStatuses { get; set; }


        IVolunteerStatusManager _volunteerStatusManager = new VolunteerStatusManager();
        IVolunteerStatusProjectRoleManager _volunteerStatusProjectRoleManager = new VolunteerStatusProjectRoleManager();
        IForumPermissionManager _forumPermissionManager = new ForumPermissionManager();
        IUserManager _userManager = new UserManager();
        IProjectManager _projectManager = new ProjectManager();

        public AcceptVolunteerOffers(int project)
        {

            InitializeComponent();
            this._project = project;
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnViewRejected.Visibility = Visibility.Collapsed;
            btnViewAll.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Project Starter", _project) || 
                main.HasProjectRole("Volunteer Directors", _project) ||
                main.HasProjectRole("Moderator", _project))
            {
                btnViewRejected.Visibility = Visibility.Visible;
            }
            if(main.HasProjectRole("Project Starter", _project) ||
               main.HasProjectRole("Volunteer Director", _project) ||
               main.HasProjectRole("Event Coordinator", _project) ||
               main.HasProjectRole("Moderator", _project) ||
               main.HasProjectRole("Background Checker", _project) ||
               main.HasSystemRole("Admin"))
            {
                btnViewAll.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/14
        /// Summary:  Method called to populate the lvVolunteerStatus list view
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/02/14
        /// What was Changed: Initial creation	
        /// </summary>
        private void LoadVolunteerStatusList()
        {
            try
            {
                _volunteerStatuses = new ObservableCollection<VMVolunteerStatus>(_volunteerStatusManager.GetPendingVolunteerStatusByProjectID(_project));
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;

                MessageBox.Show(message);
            }
            DataContext = this;
            if (_volunteerStatuses.IsNullOrEmpty())
            {
                txtNoRequests.Visibility = Visibility.Visible;
            }
            else
            {
                txtNoRequests.Visibility = Visibility.Hidden;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadVolunteerStatusList();
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/06
        /// Summary:  Method called when an accept button is clicked. 
        ///           Updates the relevent VolunteerStatus record for Approved value to true	
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            VMVolunteerStatus volunteerStatus = button.DataContext as VMVolunteerStatus;
            volunteerStatus.Approved = true;
            _volunteerStatusManager.UpdateVolunteerStatus(volunteerStatus);

            _volunteerStatusProjectRoleManager.InsertUserProjectRole(volunteerStatus.UserID, volunteerStatus.ProjectID, "Volunteer");
            ForumPermission newForumPermission = new ForumPermission()
            {
                UserID = volunteerStatus.UserID,
                ProjectID = volunteerStatus.ProjectID,
                WriteAccess = true
            };
            _forumPermissionManager.AddForumPermission(newForumPermission);


            _volunteerStatuses.Remove(volunteerStatus);
            if(_volunteerStatuses.IsNullOrEmpty())
            {
                txtNoRequests.Visibility = Visibility.Visible;
            }
            else
            {
                txtNoRequests.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/06
        /// Summary:  Method called when an reject button is clicked. 
        ///           Updates the relevent VolunteerStatus record for Approved value to false	
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            VMVolunteerStatus volunteerStatus = button.DataContext as VMVolunteerStatus;
            volunteerStatus.Approved = false;
            _volunteerStatusManager.UpdateVolunteerStatus(volunteerStatus);

            // Delete their exisitng project roles.
            _volunteerStatusProjectRoleManager.DeleteUserRoles(volunteerStatus.UserID, volunteerStatus.ProjectID);
            // Revoke ForumPermission, if it existed.
            ForumPermission newForumPermission = new ForumPermission()
            {
                UserID = volunteerStatus.UserID,
                ProjectID = volunteerStatus.ProjectID,
                WriteAccess = false
            };
            _forumPermissionManager.EditForumPermissionWriteAccessValue(newForumPermission);

            _volunteerStatuses.Remove(volunteerStatus);
            if (_volunteerStatuses.IsNullOrEmpty())
            {
                txtNoRequests.Visibility = Visibility.Visible;
            }
            else
            {
                txtNoRequests.Visibility = Visibility.Hidden;
            }
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            // go to the ViewVolunteerList based on projectID
            NavigationService.GetNavigationService(this)?.Navigate(new ViewVolunteerList(_project));
        }

        private void btnViewRejected_Click(object sender, RoutedEventArgs e)
        {
            // go to ViewRejectedVolunteer based on projectID
            NavigationService.GetNavigationService(this)?.Navigate(new ViewRejectedVolunteerOffers(_project));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button button = sender as Button;
            if (button == null) return;

            VMVolunteerStatus volunteerStatus = button.DataContext as VMVolunteerStatus;

            List<User> users = _userManager.GetAllUsers();
            User volunteer = null;

            foreach(User user in users)
            {
                if(user.UserID == volunteerStatus.UserID)
                {
                    volunteer = user;
                    break;
                }
            }

            NavigationService.Navigate(new ViewVolunteerProfile(volunteer));
        }

        // Author: Kate Rich
        private void btnBackgroundCheck_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if(button == null)
            {
                return;
            }

            List<User> users = new List<User>();
            User targetUser = null;
            Project project = null;

            VMVolunteerStatus volunteerStatus = button.DataContext as VMVolunteerStatus;
            try
            {
                project = _projectManager.GetProjectByID(_project);
                users = _userManager.GetAllUsers();

                foreach(User user in users)
                {
                    if(user.UserID == volunteerStatus.UserID)
                    {
                        targetUser = user;
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message);
            }

            NavigationService.Navigate(new createBackgroundCheckDesktop(targetUser, project));
        }
    }
}
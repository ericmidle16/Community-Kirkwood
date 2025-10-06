/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-02
/// Summary:
/// 	C# code file which contains the Presentation Layer code for the
/// 	View Project feature - displaying a single project's information
/// 	to a user.
/// 
/// Updated By: Stan Anderson
/// Updated: 2025-04-07
/// What Was Changed: Added connections and checked if project takes donations
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed: 
///     Added Automation Logic for when a Volunteer Leaves a Project on their own:
///         ProjectRoles are already being deleted with Ellie's implementation.
///         Added logic to revoke the Volunteer's existing WriteAccess for the Project's Forum.
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class viewProjectDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        ProjectVM _project = null;
        IProjectManager _projectManager = new ProjectManager();
        IVolunteerStatusProjectRoleManager _volunteerRoleManager = new VolunteerStatusProjectRoleManager();
        IForumPermissionManager _forumPermissionManager = new ForumPermissionManager();
        List<VolunteerStatusProjectRole> _volunteerRole = new List<VolunteerStatusProjectRole>();

        VolunteerStatus _volunteerStatus = new VolunteerStatus();
        IVolunteerStatusManager _volunteerStatusManager = new VolunteerStatusManager();

        public viewProjectDesktop(int projectID)
        {
            _project = _projectManager.GetProjectInformationByProjectID(projectID);
            
            InitializeComponent();

            // Check if the project accepts donations
            btnDonate.IsEnabled = true;
            if(!_project.AcceptsDonations)
            {
                btnDonate.IsEnabled = false;
            }
        }

        // author: Stan Anderson
        private void validatePermissions()
        {
            btnMembers.Visibility = Visibility.Hidden;
            btnBudget.Visibility = Visibility.Hidden;
            btnEditProject.Visibility = Visibility.Hidden;
            btnJoinLeaveProject.Visibility = Visibility.Visible;
            btnForum.Visibility = Visibility.Hidden;

            if(!main.isLoggedIn)
            {
                // the user is NOT logged in
                btnJoinLeaveProject.IsEnabled = false;
            }
            else
            {
                btnJoinLeaveProject.IsEnabled = true;

                List<VolunteerStatus> statuses = _volunteerStatusManager.SelectVolunteerStatusByProjectID(_project.ProjectID);
                bool applied = false;
                if(statuses != null && statuses.Count > 0)
                {
                    foreach(VolunteerStatus statt in statuses)
                    {
                        if(statt.UserID == main.UserID)
                        {
                            applied = true;
                            break;
                        }
                    }
                }

                // check for roles
                if(main.IsProjectStarter(_project.ProjectID))
                {
                    btnEditProject.Visibility = Visibility.Visible;
                    btnJoinLeaveProject.IsEnabled = false; // you cannot leave nor join your own project!
                    btnJoinLeaveProject.Content = "Leave Project";
                    btnJoinLeaveProject.IsEnabled = false;
                    btnJoinLeaveProject.Style = this.FindResource("DangerButton") as Style;
                }
                if(main.IsVolunteer(_project.ProjectID) ||
                    main.IsProjectStarter(_project.ProjectID))
                {
                    btnForum.Visibility = Visibility.Visible;
                    btnJoinLeaveProject.Content = "Leave Project";
                    btnJoinLeaveProject.IsEnabled = true;
                    btnJoinLeaveProject.Style = this.FindResource("DangerButton") as Style;
                }
                else if(applied) // requested but not yet accepted
                {
                    btnJoinLeaveProject.Content = "Join Project";
                    btnJoinLeaveProject.IsEnabled = false;
                }
                else
                {
                    btnJoinLeaveProject.Content = "Join Project";
                    btnJoinLeaveProject.IsEnabled = true;
                    btnJoinLeaveProject.Style = this.FindResource("CallToAction") as Style;
                }
                if(main.HasProjectRole("Project Starter", _project.ProjectID) || 
                    main.HasProjectRole("Purchaser", _project.ProjectID) ||
                    main.HasProjectRole("Accountant", _project.ProjectID))
                {
                    btnBudget.Visibility = Visibility.Visible;
                }
                if(main.HasProjectRole("Project Starter", _project.ProjectID) ||
                   main.HasProjectRole("Volunteer Director", _project.ProjectID) ||
                   main.HasProjectRole("Event Coordinator", _project.ProjectID) ||
                   main.HasProjectRole("Background Checker", _project.ProjectID) ||
                   main.HasProjectRole("Scheduler", _project.ProjectID) ||
                   main.HasSystemRole("Admin"))
                {
                    btnMembers.Visibility = Visibility.Visible;
                }
            }
        }

        // Author: Kate Rich
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblProjectName.Content = _project.Name;
            txtProjectStatus.Text = _project.Status;
            txtProjectStartDate.Text = _project.StartDate.ToString("MM/dd/yyyy");
            txtProjectType.Text = _project.ProjectTypeID;
            txtProjectLocationName.Text = _project.LocationName;
            txtProjectCityState.Text = _project.City + ", " + _project.State;
            txtProjectDescription.Text = _project.Description;

            validatePermissions();
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/12
        /// 
        /// This method contains the logic for leaving a project
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/07 
        /// example: Added join project
        /// </remarks> 
        private void leaveOrJoinProject()
        {
            try
            {
                _volunteerRole = _volunteerRoleManager.GetUserProjectRolesByUserIDProjectID(main.UserID, _project.ProjectID);
            }
            catch(Exception ex)
            {
                _volunteerRole = new List<VolunteerStatusProjectRole>(); // Avoid null reference
            }
            if(btnJoinLeaveProject.Content.Equals("Leave Project")) // Check that the user has joined a project
            {

                MessageBoxResult result = MessageBox.Show("Choosing this option will permanently remove you from being a volunteer on this project. Are you sure you want to leave?",
                                                      "Cancel Confirmation",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if(_volunteerRole != null && _volunteerRole.Any()) // if the user has a role
                        {
                            _volunteerRoleManager.DeleteUserRoles(main.UserID, _project.ProjectID);

                            ForumPermission newForumPermission = new ForumPermission()
                            {
                                UserID = main.UserID,
                                ProjectID = _project.ProjectID,
                                WriteAccess = false
                            };
                            _forumPermissionManager.EditForumPermissionWriteAccessValue(newForumPermission);
                        }

                        _volunteerStatusManager.DeactivateVolunteerByUserIDAndProjectID(main.UserID, _project.ProjectID);
                        if(!main.IsVolunteer(_project.ProjectID))
                        {
                            MessageBox.Show("You have successfully left the project.", "Left Project", MessageBoxButton.OK, MessageBoxImage.Warning);

                        }
                        else
                        {
                            MessageBox.Show("There was an issue leaving the project", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        validatePermissions();
                       
                    }
                    catch(Exception ex)
                    {
                        string message = ex.InnerException == null ? ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    return;  // Popup box disappears
                }
            } 
            else
            {
                // Akoi Kollie's Sign Up To Project
                try
                {
                    if(_volunteerStatusManager.AddVolunteerStatus(main.UserID, _project.ProjectID))
                    {
                        MessageBox.Show("We received your submission to join our project watch your notification for approval information from our volunteer director along with the next steps to get involved.", "Application Recieved", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(_project.ProjectID));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Request Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void btnJoinLeaveProject_Click(object sender, RoutedEventArgs e)
        {
            if(main.isLoggedIn)
            {
                leaveOrJoinProject();
            }
            
        }
        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new PgEventList(_project.ProjectID));
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ListOfAllProjects());
        }
        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewTasksByEventID(_project));
        }
        private void btnMembers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewVolunteerList(_project.ProjectID));
        }
        private void btnBudget_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewProjectFunds(_project));
        }
        private void btnForum_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewProjectForum(_project.ProjectID));
        }

        private void btnDonate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new DonateToProject(_project.ProjectID));
        }

        private void btnEditProject_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new EditProject(_project));
        }

        private void btnNeedList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewNeedList(_project.ProjectID));
        }
    }
}
/// <summary>
/// Creator:  Jennifer Nicewanner
/// Created:  2025/02/14
/// Summary:  Interaction logic for ViewVolunteerList.xaml
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// What was Changed: Updated UI and added permissions
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataDomain;
using LogicLayer;

namespace WpfPresentation.Pages
{
    public partial class ViewVolunteerList : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IUserManager _userManager = new UserManager();
        IVolunteerStatusProjectRoleManager _volunteerStatusProjectRoleManager = new VolunteerStatusProjectRoleManager();
        IForumPermissionManager _forumPermissionManager = new ForumPermissionManager();
        int ProjectID;

        public ViewVolunteerList()
        {
            InitializeComponent();
        }

        public ViewVolunteerList(int ProjectID)
        {
            this.ProjectID = ProjectID;
            InitializeComponent();
            GetPrivileges();
        }

        private void GetPrivileges()
        {
            btnViewRequests.Visibility = Visibility.Collapsed;
            btnRoles.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Project Starter", ProjectID) || 
                main.HasProjectRole("Volunteer Director", ProjectID) || 
                main.SystemRoles.Contains("Admin"))
            {
                btnViewRequests.Visibility = Visibility.Visible;
                btnRoles.Visibility = Visibility.Visible;
            }
        }

        private void PopulateDataGrid()
        {           
            List<User> ProjectVolunteers = new List<User>();
            try
            {
                ProjectVolunteers = _userManager.GetApprovedUserByProjectID(ProjectID);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error retrieving volunteers for project", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            lblVolunteers.Content = "Project Volunteers (" + ProjectVolunteers.Count + ")";
            
            VolunteersDataGrid.ItemsSource = ProjectVolunteers;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateDataGrid();
                validatePermissions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(ProjectID));
        }


        private void btnViewRequests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new AcceptVolunteerOffers(ProjectID));
        }

        private void btnViewUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = (User)VolunteersDataGrid.SelectedItem;
                if(user == null)
                {
                    MessageBox.Show("Please select a valid volunteer", "Invalid Volunteer", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    NavigationService.GetNavigationService(this)?.Navigate(new View_Profile(user.Email));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBackgroundChecks_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new BGC_008_viewListOfBackgroundChecks(ProjectID));
        }



        private void validatePermissions()
        {
            btnBackgroundChecks.Visibility = Visibility.Hidden;
            btnViewRequests.Visibility = Visibility.Hidden;
            btnRemoveVolunteer.Visibility = Visibility.Hidden;
            btnRoles.Visibility = Visibility.Hidden;
            btnSchedule.Visibility = Visibility.Hidden;
            if (main.isLoggedIn)
            {
                if (main.IsProjectStarter(ProjectID))
                {
                   
                    btnViewRequests.Visibility = Visibility.Visible;
                    btnRemoveVolunteer.Visibility= Visibility.Visible;
                    btnRoles.Visibility = Visibility.Visible;
                }
                if (main.HasProjectRole("Project Starter", ProjectID) || 
                    main.HasProjectRole("Background Checker", ProjectID) ||
                    main.HasProjectRole("Volunteer Coordinator", ProjectID) ||
                    main.HasSystemRole("Admin"))
                {
                    btnBackgroundChecks.Visibility = Visibility.Visible;
                }
                else
                {
                    btnBackgroundChecks.Visibility = Visibility.Collapsed;
                }
                if(main.HasProjectRole("Scheduler", ProjectID) ||
                   main.HasProjectRole("Project Starter", ProjectID) ||
                   main.HasProjectRole("Volunteer Director", ProjectID) ||
                   main.HasProjectRole("Event Coordinator", ProjectID) ||
                   main.HasSystemRole("Admin"))
                {
                    btnViewRequests.Visibility = Visibility.Visible;
                    btnSchedule.Visibility = Visibility.Visible;
                }

            }


        }


        /// <summary>
        /// Creator:    Chase Hannen
        /// Created:    2025/02/10
        /// Summary:    The C# code for the Unassign Volunteers page
        /// Last Updated By:
        ///             Stan Anderson
        /// Last Updated:
        ///             2025/04/09
        /// What was Changed:
        ///             Moved
        /// </summary>
        private void btnRemoveVolunteer_Click(object sender, RoutedEventArgs e)
        {

            // Changes 'Approved' from True to False
            var selectedUser = VolunteersDataGrid.SelectedItem as User;
            if (selectedUser != null)
            {
                // Ask if they are sure they want to unassign
                if (MessageBox.Show("Are you sure you want to unassign " + selectedUser.GivenName + " " + selectedUser.FamilyName + "?",
                                "Unassign " + selectedUser.GivenName + " " + selectedUser.FamilyName, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    // Need to get project ID from selected project
                    _userManager.UnassignVolunteerByProject(selectedUser.UserID, ProjectID);

                    _volunteerStatusProjectRoleManager.DeleteUserRoles(selectedUser.UserID, ProjectID);

                    ForumPermission newForumPermission = new ForumPermission()
                    {
                        UserID = main.UserID,
                        ProjectID = ProjectID,
                        WriteAccess = false
                    };
                    _forumPermissionManager.EditForumPermissionWriteAccessValue(newForumPermission);

                    MessageBox.Show("User was unassigned!", "User Unassigned", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Refresh the data grid after unassignment
                    NavigationService.Navigate(new ViewVolunteerList(ProjectID));
                }
            }
            else
            {
                MessageBox.Show("Please select a volunteer to unassign", "Invalid Volunteer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Creator:    Eric Idle
        /// Created:    2025/04/12
        /// Summary:    To navigate to assign project roles
        /// Last Updated By:
        ///             
        /// Last Updated:
        ///            
        /// What was Changed:
        ///             Moved
        /// </summary>
        private void btnRoles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = (User)VolunteersDataGrid.SelectedItem;
                if (user == null)
                {
                    MessageBox.Show("Please select a valid volunteer", "Invalid Volunteer", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    NavigationService.GetNavigationService(this)?.Navigate(new UpdateUserProjectRole(user.UserID, ProjectID));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Creator:    Chase Hannen
        /// Created:    2025/04/25
        /// Summary:    To navigate to schedule
        /// </summary>
        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GetNavigationService(this)?.Navigate(new viewScheduleDesktop(ProjectID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

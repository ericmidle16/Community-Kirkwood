/// <summary>
/// Creator: Eric Idle
/// Created: 2025/04/11
/// 
/// Page to view user project roles and edit
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for UpdateUserProjectRole.xaml
    /// </summary>
    public partial class UpdateUserProjectRole : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        VolunteerStatusProjectRoleManager _volunteerProjectRoleManager = new VolunteerStatusProjectRoleManager();
        ProjectRoleManager _projectRoleManager = new ProjectRoleManager();
        UserManager _userManager = new UserManager();
        ProjectManager _projectManager = new ProjectManager();
        int _UserID;
        int _ProjectID;

        public UpdateUserProjectRole(int UserID, int ProjectID)
        {
            InitializeComponent();
            _UserID = UserID;
            _ProjectID = ProjectID;
            RefreshRoleList();
        }
        


        private void RefreshRoleList()
        {
            try
            {
                List<ProjectRole> allProjectRoles = _projectRoleManager.GetAllProjectRoles();
                List<VolunteerStatusProjectRole> currentRoles = _volunteerProjectRoleManager.GetUserProjectRolesByUserIDProjectID(_UserID, _ProjectID);

                List<ProjectRole> assignedRoles = allProjectRoles.Where(projectRole => currentRoles.Any(userProjectRole => userProjectRole.ProjectRoleID == projectRole.ProjectRoleID)).ToList();
                List<ProjectRole> unassignedRoles = allProjectRoles.Except(assignedRoles).ToList();

                grdUnassignedRoleList.ItemsSource = unassignedRoles;
                grdAssignedRoleList.ItemsSource = assignedRoles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing roles: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            List<ProjectRole> unassignedRoles = grdUnassignedRoleList.ItemsSource as List<ProjectRole>;
            List<ProjectRole> assignedRoles = grdAssignedRoleList.ItemsSource as List<ProjectRole>;
            ProjectRole selectedRole = (ProjectRole)grdUnassignedRoleList.SelectedItem;

            if (selectedRole == null)
            {
                MessageBox.Show("No project role is selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                _volunteerProjectRoleManager.InsertUserProjectRole(_UserID, _ProjectID, selectedRole.ProjectRoleID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add project role!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            RefreshRoleList();
        }

        private void btnRemoveRole_Click(object sender, RoutedEventArgs e)
        {
            List<ProjectRole> unassignedRoles = grdUnassignedRoleList.ItemsSource as List<ProjectRole>;
            List<ProjectRole> assignedRoles = grdAssignedRoleList.ItemsSource as List<ProjectRole>;
            ProjectRole selectedRole = (ProjectRole)grdAssignedRoleList.SelectedItem;

            if (selectedRole == null)
            {
                MessageBox.Show("No project role is selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                _volunteerProjectRoleManager.RemoveProjectRoleByUserIDProjectID(_UserID, _ProjectID, selectedRole.ProjectRoleID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to remove system role!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            RefreshRoleList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewVolunteerList(_ProjectID));
        }
    }
}

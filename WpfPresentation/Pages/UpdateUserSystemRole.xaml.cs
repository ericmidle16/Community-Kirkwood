/// <summary>
/// Creator: Eric Idle
/// Created: 2025/04/11
/// 
/// Page to view user system roles and edit
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
    /// Interaction logic for UpdateUserSystemRole.xaml
    /// </summary>
    public partial class UpdateUserSystemRole : Page
    {
        UserSystemRoleManager _userSystemRoleManager = new UserSystemRoleManager();
        SystemRoleManager _systemRoleManager = new SystemRoleManager();
        UserManager _userManager = new UserManager();
        int _UserID;

        public UpdateUserSystemRole(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            RefreshRoleList();
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            List<SystemRole> unassignedRoles = grdUnassignedRoleList.ItemsSource as List<SystemRole>;
            List<SystemRole> assignedRoles = grdAssignedRoleList.ItemsSource as List<SystemRole>;
            SystemRole selectedRole = (SystemRole)grdUnassignedRoleList.SelectedItem;

            if (selectedRole == null)
            {
                MessageBox.Show("No system role is selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                _userSystemRoleManager.InsertUserSystemRole(_UserID, selectedRole.SystemRoleID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add system role!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
            RefreshRoleList();
        }

        private void RefreshRoleList()
        {
            try
            {
                List<SystemRole> allSystemRoles = _systemRoleManager.GetAllSystemRoles();
                List<UserSystemRole> currentRoles = _userSystemRoleManager.GetUserSystemRolesByUserID(_UserID);

                List<SystemRole> assignedRoles = allSystemRoles.Where(systemRole => currentRoles.Any(userSystemRole => userSystemRole.SystemRoleID == systemRole.SystemRoleID)).ToList();
                List<SystemRole> unassignedRoles = allSystemRoles.Except(assignedRoles).ToList();

                grdUnassignedRoleList.ItemsSource = unassignedRoles;
                grdAssignedRoleList.ItemsSource = assignedRoles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing roles: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRemoveRole_Click(object sender, RoutedEventArgs e)
        {
            List<SystemRole> unassignedRoles = grdUnassignedRoleList.ItemsSource as List<SystemRole>;
            List<SystemRole> assignedRoles = grdAssignedRoleList.ItemsSource as List<SystemRole>;
            SystemRole selectedRole = (SystemRole)grdAssignedRoleList.SelectedItem;

            if (selectedRole == null)
            {
                MessageBox.Show("No system role is selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                _userSystemRoleManager.RemoveUserSystemRole(_UserID, selectedRole.SystemRoleID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to remove system role!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            RefreshRoleList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new frmViewAllUsers());
        }
    }
}

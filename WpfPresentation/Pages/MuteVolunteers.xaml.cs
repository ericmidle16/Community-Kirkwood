/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/06
/// Summary:  Interaction logic for MuteVolunteers.xaml
/// </summary>
/// <remarks>
/// Last Updated By: 
/// Last Updated:
/// What was Changed: 
/// </remarks>
using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class MuteVolunteers : Page
    {
        VolunteerStatusManager _volunteerStatusManager = new VolunteerStatusManager();
        ForumPermissionManager _forumPermissionManager = new ForumPermissionManager();
        int _projectID;
        public MuteVolunteers(int projectID)
        {
            InitializeComponent();
            _projectID = projectID;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewProjectForum(_projectID));
        }

        private void grdMuteUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var forumPermission = grdMuteUsers.SelectedCells;
            var selectedItem = dataGrid?.SelectedItem as ForumPermission;

            if (selectedItem == null)
            {
                return;
            }

            selectedItem.WriteAccess = !selectedItem.WriteAccess;
            try
            {
                bool success = _forumPermissionManager.EditForumPermissionWriteAccessValue(selectedItem);
                if (success)
                {
                    loadDataGrid();
                }
                else
                {
                    MessageBox.Show("Error", "Error updating forum permissions", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {

                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;

                MessageBox.Show(message, "Error updating forum permissions", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void loadDataGrid()
        {
            List<ForumPermissionVM> forumPermissions = new List<ForumPermissionVM>();

            try
            {
                forumPermissions = _forumPermissionManager.GettForumPermissionsByProjectID(_projectID);

                grdMuteUsers.ItemsSource = forumPermissions;
                grdMuteUsers.Columns.RemoveAt(1);
                grdMuteUsers.Columns.RemoveAt(1);
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;

                MessageBox.Show(message, "Error loading forum permissions", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

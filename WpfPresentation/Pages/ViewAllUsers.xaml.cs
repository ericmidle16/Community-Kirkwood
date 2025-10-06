/// <summary>
/// Jacob McPherson
/// Created: 2025/02/03
/// 
/// Interaction logic for frmViewAllUsers.xaml
/// </summary>
///
/// <remarks>
/// Updater Name: Stan Anderson
/// Updated: 2025/04/06
/// </remarks>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataDomain;
using LogicLayer;
using WpfPresentation.Pages;

namespace WpfPresentation
{
    public partial class frmViewAllUsers : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        List<User> _users;
        UserManager _userManager;

        public frmViewAllUsers()
        {
            try
            {
                InitializeComponent();
                _userManager = new UserManager();
                _users = _userManager.GetAllUsers();

                grdUserList.ItemsSource = _users.Where(user => user.UserID != main.UserID);
                lblUsers.Content = "Users (" + _users.Count + ")";
                GetPrivileges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading User List", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GetPrivileges()
        {
            btnView.Visibility = Visibility.Collapsed;
            if (main.ProjectRoles.Any(upr => upr.ProjectRole == "Project Starter") ||
                main.ProjectRoles.Any(upr => upr.ProjectRole == "Volunteer Director") ||
                main.ProjectRoles.Any(upr => upr.ProjectRole == "Event Coordinator") ||
                main.ProjectRoles.Any(upr => upr.ProjectRole == "Moderator") ||
                main.ProjectRoles.Any(upr => upr.ProjectRole == "Scheduler") ||
                main.ProjectRoles.Any(upr => upr.ProjectRole == "Background Checker") ||
                main.SystemRoles.Contains("Admin"))
            {
                btnView.Visibility = Visibility.Visible;
            }
        }



        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = (User)grdUserList.SelectedItem;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user to view.", "Invalid User", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NavigationService.GetNavigationService(this)?.Navigate(new ViewSingleUser(selectedUser.Email));
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = (User)grdUserList.SelectedItem;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user to view.", "Invalid User", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NavigationService.GetNavigationService(this)?.Navigate(new View_Profile(selectedUser.Email));
            }

        }
    }
}

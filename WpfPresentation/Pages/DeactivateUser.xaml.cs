/// <summary>
/// Jacob McPherson
/// Created: 2025/02/11
/// 
/// Interaction logic for frmDeactivateUser.xaml
/// </summary>
///
/// <remarks>
/// Updater Name: Stan Anderson
/// Updated: 2025/04/09
/// </remarks>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using LogicLayer;

namespace WpfPresentation.Pages
{
    public partial class frmDeactivateUser : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private int _UserID;
        public frmDeactivateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        //Default Constructor for Testing
        public frmDeactivateUser()
        {
            InitializeComponent();
            _UserID = 100000;
        }

        private void btnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            UserManager _userManager = new UserManager();
            string password = pwdPassword.Password;
            bool success = false;

            try
            {
                if(main.isLoggedIn && main.UserID == _UserID)
                {
                    success = _userManager.DeactivateUser(_userManager.GetUserInformationByUserID(_UserID).Email, password);
                }
                else
                {
                    MessageBox.Show("You do not have permission to deactivate this account.", "Invalid Access", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            } 
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (success)
            {
                //MainWindow.UpdateUser();
                MessageBox.Show("Successfully Deactivated Profile", "Account Deactivated", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GetNavigationService(this)?.Navigate(new ViewHomepage());
            }
            else
            {
                MessageBox.Show("Incorrect Password", "Bad Password", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

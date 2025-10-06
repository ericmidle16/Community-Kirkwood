/// <summary>
/// Brodie Pasker
/// Created: 2025/03/07
/// 
/// Log In page for User
/// </summary>
///
/// <remarks>
/// Updater Name: Stan Anderson
/// Updated: 2025/04/07
/// 
/// /// Updater Name: Syler Bushlack
/// Updated: 2025/04/19
/// What was Changed: added main = MainWindow.UpdateMainWindow(accessToken); to the btnLogIn_Click method so that mian's accesstoken is updated
/// 
/// Updater Name: Ellie Wacker
/// Updated: 2025/04/23
/// What was Changed: added main.UpdateUser() for Stan
/// </remarks>
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using DataDomain;

namespace WpfPresentation.Pages
{
    public partial class LogInDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow();

        public bool loggedIn = false;
        private UserVM _accessToken;
        public LogInDesktop(UserVM accessToken)
        {
            InitializeComponent();
            _accessToken = accessToken;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbxUsername.Focus();
            btnLogIn.IsDefault = true;
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            var loginLogout = new UserManager();
            string email = tbxUsername.Text;
            string password = tbxPassword.Password;
            UserVM userVM = null;

            if (email.Length < 7 || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid Email");
                tbxUsername.Focus();
                tbxUsername.SelectAll();
                return;
            }
            if (password.Length < 7)
            {
                MessageBox.Show("Invalid Password");
                tbxPassword.Focus();
                tbxPassword.SelectAll();
                return;
            }
            // try to log in
            try
            {
                // reset the log in
                if (password == "newuser")
                {
                    // Change password UI changeover
                }

                UserVM user = loginLogout.LoginUser(email, password);
                if (user.Suspended == true)
                {
                    MessageBox.Show(
                          "You are suspended!" +
                        "\nReason: " + user.RestrictionDetails +
                        "\nYou will be unsuspended: " + user.ReactivationDate.ToString(),
                          "User Suspended", MessageBoxButton.OK);
                    return;
                }
                setAccessToken(user);
                main.UpdateUser();

                MessageBox.Show("You are now logged in!", "Logged In", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ViewHomepage());
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }
                MessageBox.Show(message, "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _accessToken = null;
            NavigationService.GoBack();
        }

        private void btnCreateAnAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateAccount());
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnForgotUsername_Click(object sender, RoutedEventArgs e)
        {

        }
        private void setAccessToken(UserVM user)
        {
            _accessToken.UserID = user.UserID;
            _accessToken.GivenName = user.GivenName;
            _accessToken.FamilyName = user.FamilyName;
            _accessToken.Biography = user.Biography;
            _accessToken.PhoneNumber = user.PhoneNumber;
            _accessToken.Email = user.Email;
            _accessToken.City = user.City;
            _accessToken.State = user.State;
            _accessToken.Image = user.Image;
            _accessToken.ImageMimeType = user.ImageMimeType;
            _accessToken.ReactivationDate = user.ReactivationDate;
            _accessToken.Suspended = user.Suspended;
            _accessToken.ReadOnly = user.ReadOnly;
            _accessToken.Active = user.Active;
            _accessToken.RestrictionDetails = user.RestrictionDetails;
            _accessToken.Roles = user.Roles;
            _accessToken.ProjectRoles = user.ProjectRoles;
        }
    }
}

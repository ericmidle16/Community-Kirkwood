/// <summary>
/// Ellie Wacker
/// Created: 2025/02/02
/// 
/// Reset Password Popup Window. I decided to make reset password a window since it is supposed to function as a popup
/// and windows are able to perform that function. In order to popup the window, ResetPasswordPopup requires the current 
/// User object and the current UserManager object as inputs
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

using System.Windows;
using LogicLayer;
using DataDomain;

namespace WpfPresentation.Windows
{
    public partial class ResetPasswordPopup : Window
    {
        private User _user;
        private IUserManager _userManager;

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/02
        /// 
        /// This method sets the private variables _user and _userManager to the passed in parameters user and userManager.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="user"></param>
        /// <param name="userManager"></param>
        public ResetPasswordPopup(User user)
        {
            _userManager = new UserManager();
            this._user = user;
            InitializeComponent();
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/02
        /// 
        /// This method occurs after the user presses submit. It checks the username and password boxes have the desired length and that both the newPassword 
        /// and retypePassword match. If these conditions are met it attempts to update the password. If the update password is sucessful a confirmation message 
        /// appears and the main page is brought back and if not an error message appears.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Validation for username
            if (txtUsername.Text.Length < 7 || txtUsername.Text.Length > 250 || txtUsername.Text != _user.Email)
            {
                MessageBox.Show("Invalid Username.");
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }

            // Validation for old password
            if (pwdOldPassword.Password.Length < 6 || pwdOldPassword.Password.Length > 100)
            {
                MessageBox.Show("Invalid Current Password");
                pwdOldPassword.Focus();
                pwdOldPassword.SelectAll();
                return;
            }

            // Validation for new password
            if (pwdNewPassword.Password.Length < 6 || pwdNewPassword.Password.Length > 100 || pwdNewPassword.Password == pwdOldPassword.Password)
            {
                MessageBox.Show("Invalid New Password");
                pwdNewPassword.Focus();
                pwdNewPassword.SelectAll();
                return;
            }

            // Validation for retyped password
            if (string.Compare(pwdNewPassword.Password, pwdRetypePassword.Password) != 0)
            {
                MessageBox.Show("New Password and Retyped Password must match.");
                pwdRetypePassword.Password = ""; 
                pwdNewPassword.Focus();
                pwdNewPassword.SelectAll();
                return;
            }

            // Proceed with password update
            string oldPassword = pwdOldPassword.Password;
            string newPassword = pwdNewPassword.Password;
            string username = txtUsername.Text;

            try
            {
                if (_userManager.UpdatePassword(username, oldPassword, newPassword))
                {
                    MessageBox.Show("Password Updated");
                    this.Close();  // Successful update
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/02
        /// 
        /// This method occurs after the user presses cancel. A popup box will appear asking the user if they are sure they want to cancel.
        /// If they press yes, they will be brought back to the main page. If they press no, the popup will disappear.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?",
                                                      "Cancel Confirmation",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();  // User is brought back to main page
            }
            else
            {
                return;  // Popup box disappears
            }
        }
    }
}

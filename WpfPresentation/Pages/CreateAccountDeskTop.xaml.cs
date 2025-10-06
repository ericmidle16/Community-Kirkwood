/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/02
/// Summary:This class handles the user interface for creating a new account.
/// It interacts with the UserManager to validate user input and insert
/// a new user into the system.
/// Last Upaded By: Stan Anderson
/// Last Updated: 2025/04/07
/// What Was Changed: UI
/// </summary>
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class CreateAccount : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private UserManager _userManager;
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Initializes the CreateAccount page and sets up the UserManager instance. 
        /// This constructor prepares the UI components and ensures user data management
        /// can be accessed during account creation.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public CreateAccount()
        {
            InitializeComponent();
            _userManager = new UserManager(); 
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary:Handles the Create Account button click event. Validates user input fields,
        /// checks for an existing email, and attempts to create a new user account.
        /// Displays appropriate success or error messages based on the outcome.
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/07
        /// What Was Changed: Changed the navigation
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/07
        /// What Was Changed: Added a default profile picture to be added when creating the account
        /// </summary>
        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEnterEmail.Text;
            string givenName = txtEnterGivenName.Text;
            string familyName = txtEnterFamilyName.Text;
            string phoneNumber = txtEnterPhoneNumber.Text;
            string password = pwdChoosePassword.Password; // PasswordBox stores password

            // Validate input
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(givenName) ||
                string.IsNullOrWhiteSpace(familyName) || string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("All fields must be filled out.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password.Length < 7 || password.Length > 100)
            {
                MessageBox.Show("Password must be at least 7 characters long and cannot exceed 100 characters.",
                                "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (email.Length > 250)
            {
                MessageBox.Show("Email cannot exceed 250 characters.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format. Email must contain '@yourgmailtype.com'.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (givenName.Length > 50 || familyName.Length > 50)
            {
                MessageBox.Show("Given name and family name cannot exceed 50 characters.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!phoneNumber.All(char.IsDigit) || phoneNumber.Length < 10 || phoneNumber.Length > 11)
            {
                MessageBox.Show("Phone number must be between 10 and 11 digits and only contain numbers.",
                                "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Checks if the email already exists
                if (_userManager.DoesEmailExist(email))
                {
                    MessageBox.Show("An account with this email already exists. Please use a different email.", "Duplicate Email", MessageBoxButton.OK
                        , MessageBoxImage.Warning );
                    return;
                }

                byte[] defaultProfilePicture = ImageUtils.LoadEmbeddedImageAsBytes("WpfPresentation.Images.defaultPFP.jpg");
                string defaultProfilePictureMimeType = "image/jpeg";

                // Call the UserManager to insert the new user
                bool isUserCreated = _userManager.InsertUser(givenName, familyName, phoneNumber, email, password, defaultProfilePicture, defaultProfilePictureMimeType);

                if (isUserCreated)
                {
                    MessageBox.Show("Account created successfully!\n\nPlease log in to your new account to continue.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // cannot cast User into UserVM. User will have to log in
                    main.sendToLogin();

                }
                else
                {
                    MessageBox.Show("Account creation failed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Handles the Cancel button click event. If user clicks cancel, message box
        /// will appear to ask them if they are sure they want to cancel. If user clicks
        /// yes on message box the Create Account page will close. If the user clicks 
        /// no on the message box the message box will close and the user will be returned
        /// to the Create Account page. 
        /// 
        /// Updated By: Stan Anderson
        /// Updated: 2025/04/07
        /// What Was Changed: Changed redirect
        /// 
        /// Last Updated By: Kate Rich
        /// Last Updated: 2025-04-25
        /// What Was Changed:
        ///     Updated the if-statement to just go back if the user chooses 'Yes', as they can only get to this
        ///     page through the Log In page.
        /// </summary>
        private void CancelCreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel? Your entered data will be lost.",
                                                      "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();

            }

        }
    }
}

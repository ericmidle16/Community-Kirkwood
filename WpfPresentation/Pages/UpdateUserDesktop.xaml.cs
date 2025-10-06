/// <summary>
/// Brodie Pasker
/// Created: 2025/03/07
/// 
/// Page for updating the user profile
/// </summary>
///
/// <remarks>
/// Updater Stan Anderson
/// Updated: 2025/04/08
/// </remarks>
using DataDomain;
using LogicLayer;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfPresentation.Pages
{
    ///<summary>
    /// Brodie Pasker
    /// Created: 2025/03/07
    /// 
    /// Creates all the required variables needed for the page
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name: Stan Anderson
    /// Updated: 2025/04/09
    /// example: Added additional "states"
    /// </remarks>
    public partial class UpdateUserDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference


        string[] states = new string[]
        {
            "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut",
            "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa",
            "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan",
            "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire",
            "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio",
            "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota",
            "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia",
            "Wisconsin", "Wyoming"
            // A few of the other parts of the USA:
            ,"District of Columbia", "Puerto Rico"
        };
        int maxCharsInRTB = 250;
        User _currentUserData = null;
        User _oldUserData = null;
        string imageMimeType = null;
        ///<summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Sets up the variables for the selected user
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="email">The user's email associated with the profile getting edited</param>
        public UpdateUserDesktop(string UserEmail)
        {
            InitializeComponent();
            UserManager userManager = new UserManager();
            User selectedUser = userManager.RetrieveUserDetailsByEmail(UserEmail);
            _currentUserData = selectedUser;
            _oldUserData = selectedUser;
            lblTextBoxCharDisplay.Content = $"0/{maxCharsInRTB}";
            PopulateStateField();
            PopulateUserField();


        }
        ///<summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Populates the combobox with all the state names in the states string array
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public void PopulateStateField()
        {
            foreach(string state in states)
            {
                cbxState.Items.Add(state);
            }
        }
        ///<summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Populates the all the fields with user data so that the user can edit it
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>

        public void PopulateUserField()
        {
            tbxFirstName.Text = _currentUserData.GivenName;
            tbxLastName.Text = _currentUserData.FamilyName;
            tbxEmail.Text = _currentUserData.Email;
            tbxCity.Text = _currentUserData.City;
            cbxState.Text = _currentUserData.State;
            tbxPhoneNumber.Text = _currentUserData.PhoneNumber;
            tbxAboutMe.Text = _currentUserData.Biography;
            if(_currentUserData.Image != null)
            {
                imgUserProfileImage.Source = ImageUtils.ConvertByteArrayToBitmapImage(_currentUserData.Image);
            } else
            {
                imgUserProfileImage.Source = null;
            }
            if (_currentUserData.ImageMimeType != null)
            {
                imageMimeType = _currentUserData.ImageMimeType;
            }
        }
        
         /// <summary>
         /// Brrodie Pasker
         /// Created: 2025/03/07
         /// 
         /// Updates the character count display for the about me text box
         /// </summary>
         ///
         /// <remarks>
         /// Updater Name
         /// Updated: yyyy/mm/dd 
         /// example: Fixed a problem when user inputs bad data
         /// </remarks>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void tbxAboutMe_TextChanged(object sender, TextChangedEventArgs e)
        {   
            TextBox rtb = (TextBox)sender; // Same as "sender as RichTextBox"
            lblTextBoxCharDisplay.Content = $"{rtb.Text.Length}/{maxCharsInRTB}";
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Opens up a file dialog when someone clicks select image and then loads it into the page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            // Create a file picker dialog for user to select a file
            var fileDialog = new OpenFileDialog();
            // Set the filter/file types that the user can select from the file dialog
            fileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            
            // Show dialog and store the result to dialogResult
            var dialogResult = fileDialog.ShowDialog();

            // Check if user selected a file
            if (dialogResult.HasValue && dialogResult.Value)
            {
                // Get selected file's path
                string filePath = fileDialog.FileName;

                BitmapImage image = new BitmapImage(new Uri(fileDialog.FileName));

                // Set the source of the image preview of the UI to the selected file
                imgUserProfileImage.Source = image;

                // Get the MIME type of the selected image
                string mimeType = ImageUtils.GetMimeTypeFromFilePath(filePath);

                // Convert the image preview to a byte array
                var byteArrayFromEncoder = ImageUtils.ConvertBitmapSourceToByteArray(image, mimeType);
                imageMimeType = mimeType;
            }
        }
        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Validates all the fields that need values for the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/09
        /// example: Moved the update into the try block
        /// </remarks>
        private bool ValidateAllFields()
        {
            bool validated = true;
            if (tbxFirstName.Text.Length <= 0)
            {
                MessageBox.Show("You must enter your first name");
                tbxFirstName.Focus();
                validated = false;
                return validated;
            }
            if (tbxLastName.Text.Length <= 0)
            {
                MessageBox.Show("You must enter your last name");
                tbxLastName.Focus();
                validated = false;
                return validated;
            }
            if (tbxEmail.Text.Length <= 0)
            {
                MessageBox.Show("You must enter your email");
                tbxEmail.Focus();
                validated = false;
                return validated;
            }
            string emailPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
            Regex regexEmail = new Regex(emailPattern);
            if (!regexEmail.IsMatch(tbxEmail.Text))
            {
                MessageBox.Show("You must enter a valid email");
                tbxPhoneNumber.Focus();
                validated = false;
                return validated;
            }
            if (tbxPhoneNumber.Text.Length <= 0)
            {
                MessageBox.Show("You must enter your phone number");
                tbxPhoneNumber.Focus();
                validated = false;
                return validated;
            }
            string phonePattern = "^\\(?\\d{3}\\)?[-\\s]?\\d{3}[-\\s]?\\d{4}$";
            Regex regexPhone = new Regex(phonePattern);
            if (!regexPhone.IsMatch(tbxPhoneNumber.Text))
            {
                MessageBox.Show("You must enter a valid phone number");
                tbxPhoneNumber.Focus();
                validated = false;
                return validated;
            }
            return validated;
        }
        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Saves the new, updated user data into the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/07 
        /// example: Updated message boxes
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveButton_Click(object sender, RoutedEventArgs e)
        {
            UserManager userManager = new UserManager();

            if (!ValidateAllFields())
            {
                return;
            }
            UserVM newDetails = new UserVM()
            {
                UserID = _currentUserData.UserID,
                GivenName = tbxFirstName.Text,
                FamilyName = tbxLastName.Text,
                Biography = tbxAboutMe.Text,
                Email = tbxEmail.Text,
                PhoneNumber = tbxPhoneNumber.Text,
                City = tbxCity.Text,
                State = cbxState.Text,
            };
            if(newDetails.City == "")
            {
                newDetails.City = null;
            }
            if(newDetails.State == "")
            {
                newDetails.State = null;
            }
            if(imgUserProfileImage.Source != null)
            {
                newDetails.Image = ImageUtils.ConvertImageSourceToByteArray(imgUserProfileImage.Source, imageMimeType);
                newDetails.ImageMimeType = imageMimeType;
            } else
            {
                newDetails.Image = null;
                newDetails.ImageMimeType = null;
            }
            try
            {
                userManager.UpdateUser(_oldUserData, newDetails);
                updateUserValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Profile was updated successfully","User Details Updated!", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show("Please log out and log back in to view your changes");
            NavigationService.Navigate(new View_Profile(newDetails.Email));
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Sets the new user values so that there are not concurrency issues
        /// </summary>
        private void updateUserValues()
        {
            _currentUserData.GivenName = tbxFirstName.Text;
            _currentUserData.FamilyName = tbxLastName.Text;
            _currentUserData.Biography = tbxAboutMe.Text;
            _currentUserData.Email = tbxEmail.Text;
            _currentUserData.PhoneNumber = tbxPhoneNumber.Text;
            _currentUserData.City = tbxCity.Text;
            _currentUserData.State = cbxState.Text;
            if (_currentUserData.City == "")
            {
                _currentUserData.City = null;
            }
            if (_currentUserData.State == "")
            {
                _currentUserData.State = null;
            }
            if (imgUserProfileImage.Source != null)
            {
                _currentUserData.Image = ImageUtils.ConvertImageSourceToByteArray(imgUserProfileImage.Source, imageMimeType);
                _currentUserData.ImageMimeType = imageMimeType;
            }
            else
            {
                _currentUserData.Image = null;
                _currentUserData.ImageMimeType = null;
            }
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Cancels any changes the user has made to their profile
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/07
        /// example: Changed redirect
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel? \nYou will lose any unsaved data!", "Unsaved Data", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void btnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new frmDeactivateUser(_currentUserData.UserID));
        }
    }
}

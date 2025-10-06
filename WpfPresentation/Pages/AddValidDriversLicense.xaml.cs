/// <summary>
/// Ellie Wacker
/// Created: 2025/03/04
/// 
/// C# code file which contains the Presentation Layer code for the
/// AddValidDriversLicense feature 
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// </summary>
using DataDomain;
using LogicLayer;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Tesseract;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for addInsurance.xaml
    /// </summary>
    public partial class AddValidDriversLicense : System.Windows.Controls.Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IVehicleManager _vehicleManager = new VehicleManager();
        Vehicle _vehicle = new Vehicle();
        IDocumentManager _documentManager = new DocumentManager();
        IUserSystemRoleManager _userSystemRoleManager = new UserSystemRoleManager();
        List<UserSystemRole> _userSystemRoles = new List<UserSystemRole>();
        string filename;
        string newFileName;
        bool isValid;


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/24
        /// 
        /// This method contains the default constructor for addValidDriversLicense() which is also paramatized
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public AddValidDriversLicense()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// This button click leads to an openFileDialog that allows users to browse through their photos and upload one of their in
        /// It also calls the IsValidDriversLicense method to validate the image
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void btnImagePick_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                filename = dlg.FileName;
                txtImagePath.Text = filename;

                BitmapImage bitmap = new BitmapImage(new Uri(filename));
                imgPreview.Source = bitmap;
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/03
        /// SaveImageFile saves a copy of the file to the programs images folder and deletes any images there with the same name
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void SaveImageFile(string originalPath, string destinationPath)
        {
            try
            {
                string newFileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(destinationPath);
                string imagesDirectory = System.IO.Path.GetDirectoryName(destinationPath);

                // Delete any existing files with the same name but different extensions
                string[] existingFiles = Directory.GetFiles(imagesDirectory, newFileNameWithoutExt + ".*");
                foreach (string existingFile in existingFiles)
                {
                    File.Delete(existingFile);
                }

                // Copy new file
                File.Copy(originalPath, destinationPath, true);
                Console.WriteLine($"Image saved to: {destinationPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving image: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/03
        /// IsValidDriversLicense checks if the extractedText from the uploaded image contains the information that a valid drivers license would
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public bool IsValidDriversLicense(string extractedText)
        {
            // Check if there is text in the image
            if (string.IsNullOrWhiteSpace(extractedText))
            {
                MessageBox.Show("No text detected in the image. Please upload a clear driver's license.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Convert text to lowercase for easier comparison
            string lowerText = extractedText.ToLower();

            // 1. Check for common driver's license-related keywords
            bool containsLicenseKeyword = lowerText.Contains("driver's license") ||
                                          lowerText.Contains("driver license") ||
                                          lowerText.Contains("dl number") ||
                                          lowerText.Contains("class") ||
                                          lowerText.Contains("restrictions") ||
                                          lowerText.Contains("endorsements") ||
                                          lowerText.Contains("date of birth") ||
                                          lowerText.Contains("expires");

            // Validate possible driver's license number pattern with a regex
            bool containsLicenseNumber = Regex.IsMatch(lowerText, @"\b\d{2,3}\s?\d{3}\s?\d{3}\b");

            // Final validation check: License keywords + License number 
            _userSystemRoles = _userSystemRoleManager.GetUserSystemRolesByUserID(main.UserID);
            if (containsLicenseKeyword && containsLicenseNumber)
            {
                try
                {
                    _documentManager.InsertDocument("Vehicle", main.UserID.ToString(), newFileName, "Driver's License", [], main.UserID, "Driver's License for Vehicle");
                    if (!_userSystemRoles.Any(role => role.SystemRoleID == "Driver")) // if any of the roleIDs match driver
                    {
                        _userSystemRoleManager.InsertUserSystemRole(main.UserID, "Driver");
                    }
                    MessageBox.Show("Success! Driver's license saved.", "License Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    isValid = true;
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving document: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            // Handle failure cases
            if (!containsLicenseKeyword)
            {
                MessageBox.Show("Driver's license-related keywords not found. Please upload a valid license.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!containsLicenseNumber)
            {
                MessageBox.Show("No driver's license number detected. Please ensure the document is clear and legible.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            else
            {
                MessageBox.Show("The document did not pass validation.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/04/10
        /// This is a helper method to get the project path 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private string GetProjectPath(string relativePath)
        {
            return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..", relativePath));
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/03
        /// ProcessImageWithOCR uses tessaract ocr to find and extract the text in the uploaded file
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 


        private void ProcessImageWithOCR(string imagePath)
        {
            try
            {
                // Ensure that tessdata is in the correct location
                string tessDataPath = GetProjectPath("tessdata"); ;
                Console.WriteLine($"Tesseract tessdata path: {tessDataPath}");

                // Initialize Tesseract Engine
                using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            string extractedText = page.GetText();
                            IsValidDriversLicense(extractedText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error running Tesseract: " + ex.ToString()); // Log the full exception
                MessageBox.Show("OCR Error: " + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/03
        /// btnSave_Click uses the previously declared methods to check the image and upload it if its valid
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Please select an image first.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string actualFileName = System.IO.Path.GetFileName(filename); // gets the actual fileName not the path
            if (actualFileName.Contains(' ')) // checks the actual file name for empty spaces
            {
                MessageBox.Show("The file name contains spaces. Please rename the file and try again.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                string imagesDirectory = GetProjectPath(@"Images\License"); // Where the images will be saved
                string fileExtension = System.IO.Path.GetExtension(filename); // gets the file extension
                newFileName = $"license_{main.UserID}{fileExtension}"; // creates new file name
                string savedFileName = System.IO.Path.Combine(imagesDirectory, newFileName); // full path

                ProcessImageWithOCR(filename); // checks the text in the image
                if (isValid)
                {
                    SaveImageFile(filename, savedFileName); //saves the image
                    NavigationService.Navigate(new ViewPersonalVehicles(main.UserID));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during save process: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/03
        /// btnCancel_Click asks the user to confirm if they want to cancel and brings them back to viewAllPersonalVehicles
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?",
                                                      "Cancel Confirmation",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new ViewPersonalVehicles(main.UserID));
            }
            else
            {
                return;  // Popup box disappears
            }
        }
    }

}

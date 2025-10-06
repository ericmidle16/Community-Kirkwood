/// <summary>
/// Ellie Wacker
/// Created: 2025/03/04
/// 
/// C# code file which contains the Presentation Layer code for the
/// Add Insurance feature 
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
    public partial class AddInsurance : System.Windows.Controls.Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IVehicleManager _vehicleManager = new VehicleManager();
        Vehicle _vehicle = new Vehicle();
        IDocumentManager _documentManager = new DocumentManager();
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
        public AddInsurance(Vehicle vehicle)
        {
            this._vehicle = vehicle;
            InitializeComponent();
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// This button click leads to an openFileDialog that allows users to browse through their photos and upload one(if it has the specified extension)
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
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png"; // Only allow files with these extension types

            bool? result = dlg.ShowDialog();
            if (result == true) // open the dialog
            {
                filename = dlg.FileName;
                txtImagePath.Text = filename;

                BitmapImage bitmap = new BitmapImage(new Uri(filename));
                imgPreview.Source = bitmap;
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// This method saves a copy of the image file to the desired location and overwrites any old files with the same name (ignoring extensions)
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
                string newFileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(destinationPath); // gets the file name without the extension
                string imagesDirectory = System.IO.Path.GetDirectoryName(destinationPath); // the directory where the image will be saved

                // Delete any existing files with the same name but different extensions
                string[] existingFiles = Directory.GetFiles(imagesDirectory, newFileNameWithoutExt + ".*");
                foreach (string existingFile in existingFiles)
                {
                    if (File.Exists(existingFile))
                    {
                        File.Delete(existingFile);
                    }
                }

                // save new copy of file
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
        /// Created: 2025/02/28
        /// This helper method uses OCR to extract text from the image to check if it is valid insurance
        /// (I used chat gpt to help learn how to do this)
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public bool IsValidInsurance(string extractedText)
        {
            // Checks if there is text in the image
            if (string.IsNullOrWhiteSpace(extractedText))
            {
                MessageBox.Show("No text detected in the image.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            string lowerText = extractedText.ToLower(); // makes lowercase

            // Sees if extracted text contains common insurance words
            bool containsInsuranceKeyword = lowerText.Contains("insurance") ||
                                            lowerText.Contains("policy") ||
                                            lowerText.Contains("coverage") ||
                                            lowerText.Contains("provider") ||
                                            lowerText.Contains("insured") ||
                                            lowerText.Contains("claim") ||
                                            lowerText.Contains("expiration") ||
                                            lowerText.Contains("valid until") ||
                                            lowerText.Contains("expires");

            // Uses a regex to check for a valid date
            Match dateMatch = Regex.Match(lowerText, @"(\b\d{1,2}[/\-]\d{1,2}[/\-]\d{2,4}\b)");

            // Makes sure passes validation
            if (containsInsuranceKeyword )
            {
                try
                {
                    _documentManager.InsertDocument("Vehicle", _vehicle.VehicleID, newFileName, "Car Insurance", [], main.UserID, "The Vehicle Insurance");
                    _vehicleManager.UpdateActiveByVehicleID(_vehicle.VehicleID, true);
                    MessageBox.Show("Valid insurance document detected", "Insurance Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    isValid = true;
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving document: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            // If it doesnt pass the file is deleted
            if (!containsInsuranceKeyword)
            {
                MessageBox.Show("Key word not found. Please upload a valid insurance document.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
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
                string tessDataPath = GetProjectPath("tessdata");
                Console.WriteLine($"Tesseract tessdata path: {tessDataPath}");

                // Initialize Tesseract Engine
                using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            string extractedText = page.GetText();
                            IsValidInsurance(extractedText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error running Tesseract: " + ex.ToString()); // Log the full exception
                MessageBox.Show("OCR Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                return;
            }

            try
            {
                string imagesDirectory = GetProjectPath(@"Images\Insurance");
                string fileExtension = System.IO.Path.GetExtension(filename);
                newFileName = $"insurance_{main.UserID}_{_vehicle.VehicleID}{fileExtension}";
                string savedFileName = System.IO.Path.Combine(imagesDirectory, newFileName);

                ProcessImageWithOCR(filename);
                if(isValid)
                {

                    SaveImageFile(filename, savedFileName);

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
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Cancel?",
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

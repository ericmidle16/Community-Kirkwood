/// <summary>
/// Creator:    Nikolas Bell
/// Created:    2025/02/28
/// Summary:    The C# code for the Edit Location page
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025/04/24
/// What was Changed: Reformatted page, deletion button, removed helpers reference
/// </summary>

using DataAccessLayer;
using DataDomain;
using LogicLayer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for EditLocation.xaml
    /// </summary>
    public partial class EditLocation : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        Location _location = null;
        ILocationManager _locationManager = new LocationManager(new LocationAccessor());
        string imageMimeType = null;

        /// <summary>
        /// Creator:  Nikolas Bell
        /// Created:  2025/02/28
        /// Summary:  Default constructor used exclusively for debugging purposes. 
        ///           Initializes the form and loads a test location.
        /// Last Updated By: Nikolas Bell
        /// Last Updated: 2025/02/28
        /// What was Changed: Added parameter descriptions and debug note.
        /// </summary>
        
        /// <summary>
        /// Creator:  Nikolas Bell
        /// Created:  2025/02/28
        /// Summary:  Initializes the form with a given Location object and loads its data.
        /// Last Updated By: Nikolas Bell
        /// Last Updated: 2025/02/28
        /// What was Changed: Added parameter descriptions.
        /// </summary>
        /// <param name="location">The Location object containing data to be edited.</param>
        public EditLocation(Location location)
        {
            _location = _locationManager.GetLocationByID(location.LocationID);
            InitializeComponent();
            LoadData();
        }

        /// <summary>
        /// Creator:  Nikolas Bell
        /// Created:  2025/02/28
        /// Summary:  Populates the combo boxes and loads the location data into the form fields.
        /// Last Updated By: Nikolas Bell
        /// Last Updated: 2025/02/28
        /// What was Changed: Initial documentation added.
        /// 
        /// Last Updated By: Chase Hannen
        /// Last Updated: 2025/04/24
        /// What was Changed: Removed helper method reference, made state a nullable text field,
        ///                     added deletion capability, implemented image
        private void LoadData()
        {
            try
            {
                txtLocationName.Text = _location.Name;
                txtAddress.Text = _location.Address;
                txtCity.Text = _location.City;
                txtState.Text = _location.State;
                txtCountry.Text = _location.Country;
                txtZip.Text = _location.Zip;
                if (_location.Image != null)
                {
                    imgLocation.Source = ImageUtils.ConvertByteArrayToBitmapImage(_location.Image);
                }
                txtDescription.Text = _location.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private bool ValidateData()
        {
            return (ValidateName() &&
                ValidateAddress() &&
                ValidateCity()
                );
        }

        private bool ValidateName()
        {
            if (txtLocationName.Text.IsNullOrEmpty())
            {
                MessageBox.Show("You must enter a name.");
                return false;
            }
            else { return true; }
        }
        private bool ValidateAddress()
        {
            if (txtAddress.Text.IsNullOrEmpty())
            {
                MessageBox.Show("You must enter an address.");
                return false;
            }
            else { return true; }
        }
        private bool ValidateCity()
        {
            if (txtCity.Text.IsNullOrEmpty())
            {
                MessageBox.Show("You must enter a city.");
                return false;
            }
            else { return true; }
        }
        

        /// <summary>
        /// Creator:  Chase Hannen
        /// Created:  2025/04/24
        /// Summary:  Deactivates the selected location (sets 'Active' to false)
        /// Last Updated By: Nikolas Bell
        /// Last Updated: 2025/02/28
        /// What was Changed: Added parameter descriptions.
        /// </summary>



        /// <summary>
        /// Creator:  Nikolas Bell
        /// Created:  2025/02/28
        /// Summary:  Validates input data, updates the location record, and shows a message indicating success or failure.
        /// Last Updated By: Nikolas Bell
        /// Last Updated: 2025/02/28
        /// What was Changed: Initial documentation added.
        /// 
        /// Last Updated By: Chase Hannen
        /// Last Updated: 2025/04/24
        /// What was Changed: Removed helpers method, implemented delete feature PRS-008
        /// </summary>

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this location?", "Delete " + _location.Name + "?",
                                                        MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _locationManager.DeactivateLocationByLocationID(_location.LocationID);
                MessageBox.Show("Location deleted");
                NavigationService.GetNavigationService(this)?.Navigate(new ViewLocationList());
            }
        }

        private void btnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            // Create file picker dialog
            var fileDialog = new OpenFileDialog();
            // Set filter and file types that the user can select
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
                imgLocation.Source = image;
                // Get MIME type of image
                string mimeType = ImageUtils.GetMimeTypeFromFilePath(filePath);
                // Convert to byte array
                var byteArrayFromEncoder = ImageUtils.ConvertBitmapSourceToByteArray(image, mimeType);
                imageMimeType = mimeType;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateData()) { return; }

            Location updatedLocation = new Location() {
                LocationID = _location.LocationID, 
                Name = txtLocationName.Text,
                Address = txtAddress.Text,
                City = txtCity.Text, 
                State = txtState.Text, 
                Zip = txtZip.Text, 
                Description = txtDescription.Text
            };

            try
            {
                if (imgLocation.Source != null)
                {
                    updatedLocation.Image = ImageUtils.ConvertImageSourceToByteArray(imgLocation.Source, imageMimeType);
                    updatedLocation.ImageMimeType = imageMimeType;
                    // Image MimeType Check
                    if (updatedLocation.ImageMimeType == "image/png" || updatedLocation.ImageMimeType == "image/jpeg")
                    {

                    }
                    else
                    {
                        MessageBox.Show("Image must be .png or .jpg", "Invalid Image Type", MessageBoxButton.OK, MessageBoxImage.Error);
                        updatedLocation.Image = null;
                        imgLocation.Source = null;
                        btnChooseImage.Focus();
                        return;
                    }
                }

                if (_locationManager.UpdateLocationByID
                    (updatedLocation.LocationID, updatedLocation))
                {
                    MessageBox.Show("Edit successfully saved.");
                    NavigationService.GetNavigationService(this)?.Navigate(new ViewLocationList());
                }
                else
                {
                    throw new Exception("Database error.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save unsuccessful.", ex.Message);
            }
        }
    }
}

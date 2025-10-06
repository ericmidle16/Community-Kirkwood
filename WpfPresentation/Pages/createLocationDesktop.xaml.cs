/// <summary>
/// Creator:    Chase Hannen
/// Created:    2025/02/02
/// Summary:    The C# code for the Create Location page
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// What was Changed: Updated message boxes; Fixed the issue where it doesn't submit without an image
/// </summary>

using LogicLayer;
using DataDomain;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class createLocationDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        LocationManager _locationManager = new LocationManager();
        string imageMimeType = null;

        public createLocationDesktop()
        {
            InitializeComponent(); 
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtLocationName.Focus();
            btnSubmit.IsDefault = true;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var locationManager = new LocationManager();
            Location newLocation = new Location()
            {
                Name = txtLocationName.Text,
                Address = txtAddress.Text,
                City = txtCity.Text,
                State = txtState.Text,
                Zip = txtZip.Text,
                Country = txtCountry.Text,
                Description = txtDescription.Text
            };            

            // NOT NULL checks
            if (newLocation.Name.Equals("") || newLocation.Name.Equals(null))
            {
                MessageBox.Show("Name cannot be blank", "Invalid Name", MessageBoxButton.OK, MessageBoxImage.Error);
                txtLocationName.Focus();
                txtLocationName.SelectAll();
                return;
            }
            if (newLocation.Address.Equals("") || newLocation.Address.Equals(null))
            {
                MessageBox.Show("Address cannot be blank", "Invalid Address", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAddress.Focus();
                txtAddress.SelectAll();
                return;
            }
            if (newLocation.City.Equals("") || newLocation.City.Equals(null))
            {
                MessageBox.Show("City cannot be blank", "Invalid City", MessageBoxButton.OK, MessageBoxImage.Error);
                txtCity.Focus();
                txtCity.SelectAll();
                return;
            }

            // Attempt to add location
            try
            {
                if (newLocation.Country.Equals(""))
                {
                    newLocation.Country = "USA";
                }

                newLocation.Image = null;
                if (imgLocation.Source != null)
                {
                    newLocation.Image = ImageUtils.ConvertImageSourceToByteArray(imgLocation.Source, imageMimeType);
                    newLocation.ImageMimeType = imageMimeType;
                    // Image MimeType Check
                    if (newLocation.ImageMimeType == "image/png" || newLocation.ImageMimeType == "image/jpeg")
                    {
                        
                    }
                    else
                    {
                        MessageBox.Show("Image must be .png or .jpg", "Invalid Image Type", MessageBoxButton.OK, MessageBoxImage.Error);
                        newLocation.Image = null;
                        imgLocation.Source = null;
                        btnChooseImage.Focus();
                        return;
                    }
                }

                bool locationAdded = _locationManager.AddNewLocation(newLocation);
                if (locationAdded)
                {
                    MessageBox.Show("Location added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GetNavigationService(this)?.Navigate(new ViewLocationList());
                }


            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Error Adding Item", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "Error Adding Item", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are You Sure You Want to Cancel?", "Cancel?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
    }
}

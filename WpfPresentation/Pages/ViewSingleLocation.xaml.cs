/// <summary>
/// Creator:  Jennifer Nicewanner
/// Created:  ?
/// Summary:  Interaction logic for ViewSingleLocation.xaml
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// What was Changed: Updated	
/// </summary>
/// 
using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class ViewSingleLocation : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private Location _location = null;
        private LocationManager _locationManager = new LocationManager();
        private int locationID;

        public ViewSingleLocation()
        {
            InitializeComponent();
        }

        public ViewSingleLocation(int locationID)
        {
            this.locationID = locationID;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
            try
            {
                _location = _locationManager.GetLocationByID(locationID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Location not found.", "Error", MessageBoxButton.OK);
            }
           

            txtName.Content = _location.Name.ToString();
            txtStreet.Text = _location.Address.ToString();
            txtCity.Text = _location.City.ToString();
            txtState.Text = _location.State.ToString();
            txtZip.Text = _location.Zip.ToString();
            txtCountry.Text = _location.Country.ToString();
            if (_location.Image != null)
            {
                imgImage.Source = ImageUtils.ConvertByteArrayToBitmapImage(_location.Image);
            }
            
            txtDetails.Text = _location.Description.ToString();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.GoBack();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

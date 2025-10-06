/// <summary>
/// Creator: Christivie Mauwa
/// Created: ?
/// Summary: C# for viewing a donation invoice
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025-04-07
/// What Was Changed: updated UI
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-30
/// What Was Changed: Implemented the link to the DownloadTaxForm page
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class DonationInvoice : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private IDonationManager _donationManager;
        private ILocationManager _locationManager;
        private IProjectManager _projectManager;
        private DonationVM _donation;
        private Location _location;

        public DonationInvoice(DonationVM donation)
        {
            _donationManager = new DonationManager();
            _locationManager = new LocationManager();
            _projectManager = new ProjectManager();

            _donation = donation;

            InitializeComponent();
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnDownLoad.Visibility = Visibility.Collapsed;
            if(main.HasProjectRole("Accountant", _donation.ProjectID) || 
                main.HasProjectRole("Purchaser", _donation.ProjectID) ||
                main.HasProjectRole("Project Starter", _donation.ProjectID) ||
                main.SystemRoles.Contains("Admin"))

            {
                btnDownLoad.Visibility = Visibility.Visible;
            }
        }

        

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Go back to the previous page
            NavigationService.GoBack();
        }

        private void btnDownLoad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new DownloadTaxForm());
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                _donation = _donationManager.RetrieveDonationByDonationID(_donation.DonationID);

                if(_donation != null)
                {
                    txtDonationID.Text = _donation.DonationID.ToString();
                    txtDonationTotal.Text = _donation.Amount.ToString();
                    txtDonationDate.Text = _donation.DonationDate.ToString("MM-dd- yyyy");

                    txtProjectID.Text = _donation.ProjectID.ToString();
                    txtProjectName.Text = _donation.Name.ToString();

                    var project = _projectManager.GetProjectByID(_donation.ProjectID);
                    if (project != null)
                    {
                        int locationID = project.LocationID;
                        _location = _locationManager.GetLocationByID(locationID);
                        if (_location != null)
                        {
                            txtLocation.Text = _location.Address + ", " + _location.City + ", " +_location.State ;

                        }
                        else
                        {
                            MessageBox.Show("Location not found.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Project not found.");
                    }
                }
                else
                {
                    MessageBox.Show("Donation data not found.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading donation data: {ex.Message}");
            }
        }
    }
}
/// <summary>
/// Creator: Christivie Mauwa
/// Created: ?
/// Summary: C# for viewing a single donation
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025-04-07
/// What Was Changed: updated UI
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-30
/// What Was Changed: Removed references to btnReceipt because it was removed do to the feature not being coded
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class ViewSingleDonationDetails : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private IDonationManager _donationManager;
        private ILocationManager _locationManager;
        private IProjectManager _projectManager;
        private DonationVM _donation;
        int donation;
        int projectID;
        private Location _location;

        public ViewSingleDonationDetails(int donation, int projectID)
        {
            InitializeComponent();
            this.donation = donation;
            this.projectID = projectID;
            _donationManager = new DonationManager();
            _locationManager = new LocationManager();
            _projectManager = new ProjectManager();
            var selectedDonation = ViewSingleDonationDetails.SelectedDonation;
            validatePermission();
        }

        public ViewSingleDonationDetails(int donation)
        {
            InitializeComponent();
            this.donation = donation;
            this.projectID = 0;
            _donationManager = new DonationManager();
            _locationManager = new LocationManager();
            _projectManager = new ProjectManager();
            var selectedDonation = ViewSingleDonationDetails.SelectedDonation;
            validatePermission();
        }

        private void validatePermission()
        {
            btnInvoice.Visibility = Visibility.Hidden;

            if(main.isLoggedIn && projectID != 0)
            {
                if(main.HasProjectRole("Accountant", projectID) ||
                    main.HasProjectRole("Purchaser", projectID) ||
                    main.HasProjectRole("Project Starter", projectID) ||
                    main.HasSystemRole("Admin"))
                {
                    btnInvoice.Visibility = Visibility.Visible;
                }
            }
        }
        public static DonationVM SelectedDonation { get; set; }

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if the donation object is loaded
                if(_donation != null)
                {
                    this.NavigationService.Navigate(new DonationInvoice(_donation));
                }
                else
                {
                    MessageBox.Show("Donation data not found. Cannot view the invoice.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error navigating to the invoice page: {ex.Message}");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void btnProjectPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(_donation.ProjectID));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _donation = _donationManager.RetrieveDonationByDonationID(donation);

                if(_donation != null)
                {
                    txtDonationDate.Text = _donation.DonationDate.ToString("MM-dd-yyyy");
                    txtDonationAmount.Text = _donation.Amount.ToString();
                    txtComment.Text = _donation.Description.ToString();

                    txtProjectID.Text = _donation.ProjectID.ToString();
                    txtProjectName.Text = _donation.Name.ToString();

                    var project = _projectManager.GetProjectByID(_donation.ProjectID);
                    if(project != null)
                    {
                        int locationID = project.LocationID;
                        _location = _locationManager.GetLocationByID(locationID);
                        if(_location != null)
                        {
                            txtLocation.Text = _location.Address + ", " + _location.City + ", " + _location.State;

                        }
                        else
                        {
                            MessageBox.Show("Location not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Project not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Donation data not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading donation data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

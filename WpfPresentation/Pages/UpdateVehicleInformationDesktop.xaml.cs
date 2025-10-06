using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Jackson Manternach
    /// Created: 2025/02/21
    /// 
    /// Code used for the UpdateVehicleInformationDesktop WPF page
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    public partial class UpdateVehicleInformationDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference
        private string _vehicleID;
        private bool _active;
        private string _licensePlate;
        private string _color;
        private int _year;
        private bool _insuranceStatus;
        private string _make;
        private string _model;
        private int _seats;
        private string _transportUtility;
        private VehicleManager _vehicleManager = new VehicleManager();
        public UpdateVehicleInformationDesktop(string vehicleID, string licensePlate, string color, int year,
            bool insuranceStatus, string make, string model, int seats, string transportUtility)
        {
            _vehicleID = vehicleID;
            _licensePlate = licensePlate;
            _color = color;
            _year = year;
            _insuranceStatus = insuranceStatus;
            _make = make;
            _model = model;
            _seats = seats;
            _transportUtility = transportUtility;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtLicensePlate.Text = _licensePlate;
            chkActive.IsChecked = _active;
            txtColor.Text = _color;
            txtYear.Text = _year.ToString();
            chkInsuranceStatus.IsChecked = _insuranceStatus;
            txtMake.Text = _make;
            txtModel.Text = _model;
            txtNumberOfSeats.Text = _seats.ToString();
            txtTransportUtility.Text = _transportUtility;

        }

        /// <summary>
        /// Your Name
        /// Created: 2025/02/20
        /// This is the code that runs when you click the "Update Vehicle" button in the UI, the button is supposed to update an
        /// existing row in the Vehicle table
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: 2025/02/20 
        /// </remarks>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtColor.Text))
                {
                    MessageBox.Show("Please enter a color for the vehicle.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtColor.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtYear.Text))
                {
                    MessageBox.Show("Please enter a year for the vehicle.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtYear.Focus();
                    return;
                }

                int year;
                if (!int.TryParse(txtYear.Text, out year))
                {
                    MessageBox.Show("Year must be a number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtYear.Focus();
                    return;
                }

                if (year < 1900 || year > DateTime.Now.Year + 1)
                {
                    MessageBox.Show($"Year must be between 1900 and {DateTime.Now.Year + 1}.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtYear.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLicensePlate.Text))
                {
                    MessageBox.Show("Please enter a license plate number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtLicensePlate.Focus();
                    return;
                }

                string licensePlatePattern = @"^[A-Z0-9]{1,7}$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtLicensePlate.Text.ToUpper(), licensePlatePattern))
                {
                    MessageBox.Show("License plate format is invalid. Please use only letters and numbers (maximum 7 characters).",
                        "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtLicensePlate.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMake.Text))
                {
                    MessageBox.Show("Please enter the make of the vehicle.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtMake.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtModel.Text))
                {
                    MessageBox.Show("Please enter the model of the vehicle.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtModel.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumberOfSeats.Text))
                {
                    MessageBox.Show("Please enter the number of seats.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumberOfSeats.Focus();
                    return;
                }

                int seats;
                if (!int.TryParse(txtNumberOfSeats.Text, out seats))
                {
                    MessageBox.Show("Number of seats must be a number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumberOfSeats.Focus();
                    return;
                }

                if (seats <= 0 || seats > 20)
                {
                    MessageBox.Show("Number of seats must be between 1 and 20.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtNumberOfSeats.Focus();
                    return;
                }

                Vehicle vehicle = new Vehicle
                {
                    VehicleID = _vehicleID,
                    UserID = main.UserID,
                    Active = chkActive.IsChecked ?? false,
                    Color = txtColor.Text.Trim(),
                    Year = year,
                    LicensePlateNumber = _licensePlate,
                    InsuranceStatus = chkInsuranceStatus.IsChecked ?? false,
                    Make = txtMake.Text.Trim(),
                    Model = txtModel.Text.Trim(),
                    NumberOfSeats = seats,
                    TransportUtility = txtTransportUtility.Text.Trim()
                };

                bool result = _vehicleManager.UpdateVehicleByID(vehicle);
                if (result)
                {
                    MessageBox.Show("Vehicle information updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update vehicle information", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                NavigationService.Navigate(new ViewPersonalVehicles(main.UserID));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update vehicle information: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel? Any unsaved data will be lost.",
                                                      "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService?.Navigate(new ViewPersonalVehicles(main.UserID));
            }
        }
    }
}

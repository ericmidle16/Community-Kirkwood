/// <summary>
/// Ellie Wacker
/// Created: 2025-02-21
/// 
/// C# code file which contains the Presentation Layer code for the
/// View Personal Vehicles feature 
/// 
/// /// Last Updated By: Ellie Wacker
/// Last Updated: 2025/04/21
/// What Was Changed:
///     I added my methods for deactivating a vehicle.
/// </summary>
/// 
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08

using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfPresentation.Pages
{
    public partial class ViewPersonalVehicles : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        List<Vehicle> _vehicles = new List<Vehicle>();
        List<UserSystemRole> _userSystemRoles = new List<UserSystemRole>();
        IVehicleManager _vehicleManager = new VehicleManager();
        IUserSystemRoleManager _userSystemRoleManager = new UserSystemRoleManager();
        int _UserID;

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/21
        /// 
        /// This method contains the default constructor for viewPersonalVehicles()
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/08
        /// Removed Hardcoding
        /// </remarks> 
        public ViewPersonalVehicles(int UserID)
        {
            _UserID = UserID;
            InitializeComponent();
            validatePermission();
            GetPrivileges();
        }

        private void GetPrivileges()
        {
            btnAdd.Visibility = Visibility.Collapsed;
            btnActivate.Visibility = Visibility.Collapsed;
            btnLicense.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Collapsed;
            if (main.ProjectRoleValues.Contains("Driver") ||
                main.SystemRoles.Contains("Admin"))
            {
                btnAdd.Visibility = Visibility.Visible;
                btnActivate.Visibility = Visibility.Visible;
                btnLicense.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Visible;
            }
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/21
        /// 
        /// The page loaded method for viewPersonalVehicles()
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            getVehicleList();

        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/21
        /// 
        /// This helper method fills the data grid with the current user's vehicles
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public void getVehicleList()
        {
            try
            {
                _vehicles = _vehicleManager.GetVehiclesByUserID(_UserID);

                if (_vehicles.Count > 0)
                {
                    grdPersonalVehicles.ItemsSource = _vehicles;
                }
                else
                {
                    // MessageBox.Show("You have not entered any vehicles into our system");
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, _UserID.ToString());
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/21
        /// 
        /// This button click leads to the add drivers license page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            if (grdPersonalVehicles.SelectedItem != null)
            {
                Vehicle vehicle = (Vehicle)grdPersonalVehicles.SelectedItem;
                if (vehicle.Active == false)
                {
                    Vehicle selectedVehicle = (Vehicle)grdPersonalVehicles.SelectedItem;
                    NavigationService.Navigate(new AddInsurance(selectedVehicle));
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to deactivate your vehicle? You will have to upload your insurance again to reactivate it",
                                                        "Cancel Confirmation",
                                                        MessageBoxButton.YesNo,
                                                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _vehicleManager.UpdateActiveByVehicleID(vehicle.VehicleID, false);
                        NavigationService.Navigate(new ViewPersonalVehicles(_UserID));
                    }
                    else
                    {
                        return;  // Popup box disappears
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a vehicle first.");
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/04/21
        /// 
        /// This method sets the btnActivate's content to either deactivate or activate depending on
        /// whether the vehicle is active or not
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void grdPersonalVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vehicle vehicle = (Vehicle)grdPersonalVehicles.SelectedItem;
            if (vehicle.Active == false)
            {
                btnActivate.Content = "Activate Vehicle";
                btnActivate.Style = this.FindResource("CallToAction") as Style;

            }
            else
            {
                btnActivate.Content = "Deactivate Vehicle";
                btnActivate.Style = this.FindResource("DangerButton") as Style;
            }
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/21
        /// 
        /// This button click returns the user to the home screen
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/08
        /// example: Updated navigation
        /// </remarks> 
        private void btnLeave_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View_Profile(_UserID));
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/21
        /// 
        /// This button click brings the user to the add drivers license page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void btnLicense_Click(object sender, RoutedEventArgs e)
        {
            _userSystemRoles = _userSystemRoleManager.GetUserSystemRolesByUserID(_UserID);
            if (main.ProjectRoles.Any(role => role.ProjectRole == "Driver"))
            {
                MessageBoxResult result = MessageBox.Show("You have already inserted a valid license. Do you want to update it?",
                                                     "Cancel Confirmation",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new AddValidDriversLicense());
                }
                else
                {
                    return;
                }
            }
            else
            {
                NavigationService.Navigate(new AddValidDriversLicense());
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/21
        /// 
        /// This button click brings the user to the add vehicle page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPersonalVehicle());
        }


        /// <summary>
        /// Stan Anderson
        /// Created: 2025/04/08
        /// 
        /// Validate permissions
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void validatePermission()
        {
            btnActivate.Visibility = Visibility.Hidden;
            btnAdd.Visibility = Visibility.Hidden;
            btnLicense.Visibility = Visibility.Hidden;
            btnUpdate.Visibility = Visibility.Hidden;
            if (main.isLoggedIn && main.UserID == _UserID)
            {
                btnActivate.Visibility = Visibility.Visible;
                btnAdd.Visibility = Visibility.Visible;
                btnLicense.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Visible;

            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (grdPersonalVehicles.SelectedItem == null)
            {
                MessageBox.Show("You must select a vehicle");
            }
            else
            {
                Vehicle vehicle = (Vehicle)grdPersonalVehicles.SelectedItem;
                NavigationService.Navigate(new UpdateVehicleInformationDesktop(vehicle.VehicleID, vehicle.LicensePlateNumber,
                    vehicle.Color, vehicle.Year, vehicle.InsuranceStatus, vehicle.Make, vehicle.Model, vehicle.NumberOfSeats, vehicle.TransportUtility));
            }
        }
    }

}

/// <summary>
/// Ellie Wacker
/// Created: 2025/02/17
/// 
/// C# code file which contains the Presentation Layer code for the
/// Add Personal Vehicle feature 
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// </summary>
/// 
using DataDomain;
using LogicLayer;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for addPersonalVehicle.xaml
    /// </summary>
    public partial class AddPersonalVehicle : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        Vehicle _vehicle = null;
        IVehicleManager _vehicleManager = new VehicleManager();

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/18
        /// 
        /// This method contains the default constructor for addPersonalVehicle()
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/09
        /// example: Removed Hardcoding
        /// </remarks> 
        public AddPersonalVehicle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/18
        /// 
        /// This method contains the code for saving the vehicle and validation
        /// 
        /// The seat size is set to 60 since that is the standard coach bus size
        /// which should be the largest anyone should own
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/09
        /// example: Updated message boxes
        /// </remarks> 
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isInsured = false;
            bool willingDrive = false;

            //regex source = https://stackoverflow.com/questions/19547061/regex-representation-for-licence-plate
            string licensePlateRegex = @"^[A-Za-z]{1,3}-[A-Za-z]{1,2}-[0-9]{1,4}$";
            Regex validLicensePlate = new Regex(licensePlateRegex);

            string VINRegex = @"^[A-HJ-NPR-Z0-9]{17}$";
            Regex validVIN = new Regex(VINRegex);

            if(txtMake.Text.Length < 3 || txtMake.Text.Length > 50)
            {
                MessageBox.Show("Invalid Car Make.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtModel.Text.Length < 2 || txtModel.Text.Length > 50)
            {
                MessageBox.Show("Invalid Car Model.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int currentYear = DateTime.Now.Year;
            if (Convert.ToInt32(txtYear.Text) < 1886 || Convert.ToInt32(txtYear.Text) > currentYear + 1)
            { 
                MessageBox.Show("Invalid Car Year.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(txtColor.Text.Length < 3 || txtColor.Text.Length > 20)
            {
                MessageBox.Show("Invalid Color.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtNumberSeats.Text == "" )
            {
                MessageBox.Show("Please enter the number of seats.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Convert.ToInt32(txtNumberSeats.Text) > 60) //Standard coach bus seat number
            {
                MessageBox.Show("Invalid number of seats.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string licensePlateNumber = txtLicensePlate.Text.Replace("-", ""); // removes dashes
            if (licensePlateNumber.Length > 7)
            {
                MessageBox.Show("License Plate Number must be 7 characters or less.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!validLicensePlate.IsMatch(txtLicensePlate.Text))
            {
                MessageBox.Show("Invalid License Plate Number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!validVIN.IsMatch(txtVIN.Text))
            {
                MessageBox.Show("Invalid VIN Number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_vehicleManager.GetAllVehicles().Any(vehicle => vehicle.VehicleID == txtVIN.Text.Trim())) // checks to see if any vehicle ids match the txtVIN text
            {
                MessageBox.Show("A vehicle with that VIN number already exists.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (chkInsuredYes.IsChecked != true && chkInsuredNo.IsChecked != true)
            {
                MessageBox.Show("Please say if your vehicle is insured.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (chkInsuredYes.IsChecked == true)
            {
                isInsured = true;
            }
            if (chkInsuredNo.IsChecked == true)
            {
                MessageBox.Show("Your vehicle must be insured.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Get the selected items from the ListBox
            var selectedItems = lstTransport.SelectedItems.Cast<ListBoxItem>() 
                                      .Select(item => item.Content.ToString())
                                      .ToList();

            // Convert the list to a comma-separated string 
            string transportItems = string.Join(",", selectedItems);

            if (transportItems.Length == 0)
            {
                MessageBox.Show("Please say what your vehicle can transport.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                int result = _vehicleManager.InsertVehicle(txtVIN.Text, main.UserID, false, txtColor.Text, Convert.ToInt32(txtYear.Text), licensePlateNumber, isInsured, txtMake.Text, txtModel.Text, Convert.ToInt32(txtNumberSeats.Text), transportItems);
                if (result > 0)
                {
                    MessageBox.Show("Vehicle Information Saved!", "Vehicle Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new ViewPersonalVehicles(main.UserID));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/18
        /// 
        /// This method contains the code for canceling adding the vehicle
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


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/18
        /// 
        /// A method I got from stackoverflow that only allows numbers in the textbox
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        //source: https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private void TextBox_OnlyAcceptNumbers(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            // Use SelectionStart property to find the caret position.
            // Insert the previewed text into the existing text in the textbox.
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            double val;
            // If parsing is successful, set Handled to false
            e.Handled = !double.TryParse(fullText, out val);
        }

    }
}

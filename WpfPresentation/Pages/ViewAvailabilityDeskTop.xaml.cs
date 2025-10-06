/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/24
/// Summary: This class handles the avaiability interface for viewing 
/// user availability.
/// It interacts with the AvailabilityManger to validate to pull
/// the user avaibililty and display it.
/// Last Upaded By: Stan Anderson
/// Last Updated: 2025/04/7
/// What Was Changed: Added connections
/// </summary>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using LogicLayer;
using DataDomain;

namespace WpfPresentation.Pages
{
    public partial class ViewAvailabilityDeskTop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private AvailabilityManager _availabilityManager;
        int _UserID;


        public ViewAvailabilityDeskTop()
        {
            InitializeComponent();
            _availabilityManager = new AvailabilityManager();
            _UserID = main.UserID;
            LoadAvailabilityData(_UserID);
            validatePremission();
        }

        private void validatePremission()
        {
            btnAdd.Visibility = Visibility.Hidden;
            BtnUpdate.Visibility = Visibility.Hidden;
            BtnDelete.Visibility = Visibility.Hidden;
            tbNoAvailability.Text = "This User Has No Availabilty Set.";
            if (main.isLoggedIn && main.UserID == _UserID)
            {
                btnAdd.Visibility = Visibility.Visible;
                BtnUpdate.Visibility = Visibility.Visible;
                BtnDelete.Visibility = Visibility.Visible;
                tbNoAvailability.Text = "No availability at this time. Please add availability.";
            }
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Initializes the ViewAvailability page and sets up the ViewAvailabilityManager instance. 
        /// This constructor prepares the UI components and ensures user data management
        /// can be accessed during account creation. It also sets the userID and calls the user id
        /// for the LoadAvaibililty.
        /// Last Upaded By: Stan Anderson
        /// Last Updated: 2025-04-08
        /// What Was Change: Removed hard coding
        /// </summary>
        public ViewAvailabilityDeskTop(int UserID)
        {
            InitializeComponent();
            _availabilityManager = new AvailabilityManager();
            _UserID = UserID;
            LoadAvailabilityData(_UserID);
            validatePremission();
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: This method loads the data from the database 
        /// of the avaibility based on the userID. It will only show 
        /// the avaiblilty of the user selected. If the user is new or
        /// has not added avaibility yet. It will hide the list and show
        /// a textbox message instead stating they do not have avaibility 
        /// yet and to please add some.
        /// Last Upaded By: Skyann Heintz
        /// Last Updated: 2025-03-12
        /// What Was Changed: Changed the ViewAvaibilityManager to AvaibilityManager:
        /// </summary>
        private void LoadAvailabilityData(int UserID)
        {
            try
            {
                List<Availability> availabilityList = _availabilityManager.SelectAvailabilityByUser(UserID);

                if (availabilityList == null || !availabilityList.Any())
                {
                    AvailabilityDataGrid.Visibility = Visibility.Collapsed;
                    tbNoAvailability.Visibility = Visibility.Visible;
                }
                else
                {
                    AvailabilityDataGrid.Visibility = Visibility.Visible;
                    tbNoAvailability.Visibility = Visibility.Collapsed;
                    AvailabilityDataGrid.ItemsSource = availabilityList;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading availability data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: This method loads the AddAvaibility feature.
        /// It connects the two features together. It pulls the 
        /// userID from the list and uses it in the AddAvaibility feature.
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void AddAvailability_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAvailabilityDeskTop(_UserID));
        }

        private void UpdateAvailability_Click(object sender, RoutedEventArgs e)
        {
            if (AvailabilityDataGrid.SelectedItem == null)
            {
                MessageBox.Show("You must select an availability entry");
            }
            else
            {
                Availability availability = (Availability)AvailabilityDataGrid.SelectedItem;
                NavigationService.Navigate(new UpdateAvailabilityDesktop(availability.AvailabilityID, availability.StartDate, availability.EndDate));
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (main.isLoggedIn && main.UserID == _UserID)
            {
                if (AvailabilityDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("You must select an availability entry to delete.");
                }
                else
                {
                    try
                    {
                        Availability selectedAvailability = (Availability)AvailabilityDataGrid.SelectedItem;
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this entry?", "Confirm Delete", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            bool deleted = _availabilityManager.DeleteAvailabilityByAvailabilityID(selectedAvailability.AvailabilityID);

                            if (deleted)
                            {
                                MessageBox.Show("Entry successfully deleted");
                                LoadAvailabilityData(_UserID);
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete entry");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("An error occurred while deleting availability", ex);
                    }
                }
            }
        }
    }
}

/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/04
/// Summary:  View the list of Locations in the database.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/06
/// What was Changed: Updated btnViewLocation_Click() error message.
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025/04/24
/// What was Changed: Changed list to only get active locations
/// </summary>	

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

namespace WpfPresentation.Pages
{
    public partial class ViewLocationList : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        LocationManager _locationManager = new LocationManager();
        List<Location> _locations = new List<Location>();
        public ViewLocationList()
        {
            InitializeComponent();
            GetPrivileges();
        }


        private void GetPrivileges()
        {
            btnCreateLocation.Visibility = Visibility.Collapsed;
            btnEditLocation.Visibility = Visibility.Collapsed;
            if (main.ProjectRoleValues.Contains("Project Starter") ||
                main.ProjectRoleValues.Contains("Event Coordinator") ||
                main.SystemRoles.Contains("Admin"))
            {
                btnCreateLocation.Visibility = Visibility.Visible;
                btnEditLocation.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/12
        /// Summary:  This method retrieves and populates the Location list table.
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/06
        /// What was Changed: Hid image columns.
        /// 
        /// Last Updated By: Chase Hannen
        /// Last Updated: 2025/04/24
        /// What was Changed: Changed list to only get active locations,
        ///                     Hid Active column
        /// </summary>
        private void gridLocations_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _locations = _locationManager.ViewAllActiveLocations();

                gridLocations.ItemsSource = _locations;
                gridLocations.Columns[0].Visibility = Visibility.Hidden;
                gridLocations.Columns[7].Visibility = Visibility.Hidden;
                gridLocations.Columns[8].Visibility = Visibility.Hidden;
                gridLocations.Columns[10].Visibility = Visibility.Hidden;

                lblLocations.Content = "Locations (" + _locations.Count + ")";
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading Location List", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnViewLocation_Click(object sender, RoutedEventArgs e)
        {
            Location location = (Location)gridLocations.SelectedItem;
            if (location == null)
            {
                MessageBox.Show("Please select a location to view.", "Invalid Location", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NavigationService.GetNavigationService(this)?.Navigate(new ViewSingleLocation(location.LocationID));
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Changed to navigate to home page
            NavigationService.GetNavigationService(this)?.Navigate(new ViewHomepage());
        }

        private void btnCreateLocation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new createLocationDesktop());
        }

        private void btnEditLocation_Click(object sender, RoutedEventArgs e)
        {
            if (gridLocations.SelectedItem == null)
            {
                MessageBox.Show("Please select a location to edit it.");
                return;
            }
            Location location = ((Location)gridLocations.SelectedItem);
            NavigationService.GetNavigationService(this)?.Navigate(new EditLocation(location));
        }
    }
}

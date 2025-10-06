/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-10
/// Summary:
///     C# code file which contains the Presentation Layer code for the
///     View Background Checks feature - displaying a list of  
///     background checks that have been done or are in progress for
///     a project.
/// 
/// Updated By: Stan Anderson
/// Updated: 2025/04/08
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-15
/// What Was Changed:
///     Update UI connections & added a button to go back to the page showing
///     a list of members associated with a project.
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
    public partial class BGC_008_viewListOfBackgroundChecks : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IBackgroundCheckManager _backgroundCheckManager = new BackgroundCheckManager();
        IProjectManager _projectManager = new ProjectManager();

        int _projectID;
        List<BackgroundCheckVM> _backgroundChecks = new List<BackgroundCheckVM>();

        public BGC_008_viewListOfBackgroundChecks(int projectID)
        {
            _projectID = projectID;
            InitializeComponent();
            lblProjectName.Content = _projectManager.GetProjectByID(_projectID).Name;
            GetPrivileges();
        }
        private void GetPrivileges()
        {
            btnApplicationList.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Project Starter", _projectID) || 
                main.HasProjectRole("Volunteer Director", _projectID) ||
                main.SystemRoles.Contains("Admin"))
            {
                btnApplicationList.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Visible;
            }
        }

        private void grdBackgroundChecks_Loaded(object sender, RoutedEventArgs e)
        {
            showProjectBackgroundChecks();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(grdBackgroundChecks.SelectedItem == null)
            {
                MessageBox.Show("You must select a record in order to view/edit one.");
                return;
            }
            // Pass the selected background check to the create/edit page.
            this.NavigationService.Navigate(new createBackgroundCheckDesktop((BackgroundCheckVM)grdBackgroundChecks.SelectedItem));
        }


        private void showProjectBackgroundChecks()
        {
            try
            {
                _backgroundChecks = _backgroundCheckManager.GetBackgroundChecksByProjectID(_projectID);
                lblProjectName.Content = _backgroundChecks.First().ProjectName;
                grdBackgroundChecks.ItemsSource = _backgroundChecks;
            }
            catch(Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message);
            }
        }

        private void btnApplicationList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new AcceptVolunteerOffers(_projectID));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(_projectID));
        }

        private void btnMembers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewVolunteerList(_projectID));
        }
    }
}

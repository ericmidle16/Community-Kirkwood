/// <summary>
/// Creator:  Kate Rich
/// Created:  2025-03-03
/// Summary:
/// C# code file which contains the Presentation Layer code for the
/// View Donated Projects feature - displaying a summary of the 
/// monetary donations a user has made to projects.
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class DON05_viewDonatedProjects : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IDonationManager _donationManager = new DonationManager();
        List<DonationSummaryVM> _donationSummaries = new List<DonationSummaryVM>();

        int _userID;

        public DON05_viewDonatedProjects(int userID)
        {
            _userID = userID;
            InitializeComponent();
        }
        private void grdDonationSummaries_Loaded(object sender, RoutedEventArgs e)
        {
            showDonationSummaries();
        }

        public void showDonationSummaries()
        {
            _donationSummaries.Clear();

            try
            {
                _donationSummaries = _donationManager.GetMonetaryProjectDonationSummariesByUserID(_userID);
                grdDonationSummaries.ItemsSource = _donationSummaries;
                
            }
            catch(Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
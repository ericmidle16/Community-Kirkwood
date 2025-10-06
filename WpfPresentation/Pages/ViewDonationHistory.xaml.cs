/// <summary>
/// Christivie Mauwa
/// Created: 2025/02/06
/// 
/// Page for viewing donation history with filtering options
/// </summary>

using DataDomain;
using LogicLayer;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewDonationHistory.xaml
    /// </summary>
    public partial class ViewDonationHistory : Page
    {
        DonationManager _donationManager = new DonationManager();
        List<DonationVM> _userDonations;
        private User _user;

        public ViewDonationHistory(User user)
        {
            InitializeComponent();
            _donationManager = new DonationManager();
            _user = user;
        
        }
        
        private void lstView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _userDonations = _donationManager.RetrieveDonationByUserId(_user.UserID);

                if(_userDonations != null )
                {
                    lstView.ItemsSource = _userDonations;
                    tbNoDonationHistory.Visibility = Visibility.Collapsed;
                }
                else
                {
                    tbNoDonationHistory.Visibility = Visibility.Visible;
                    lstView.ItemsSource = null;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error loading donation data: {ex.Message}");
            }
        }


        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the filter criteria from the UI
            DateTime? startDate = StartDate.SelectedDate;
            DateTime? endDate = EndDate.SelectedDate;
            decimal? minAmount = GetAmountFromTextBox(txtMinAmount);
            decimal? maxAmount = GetAmountFromTextBox(txtMaxAmount);

            // Apply the filters to the donations
            var filteredDonations = FilterDonations(startDate, endDate, minAmount, maxAmount);

            // Display the filtered donations
            if(filteredDonations.Any())
            {
                lstView.ItemsSource = filteredDonations;
                tbNoDonationHistory.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbNoDonationHistory.Visibility = Visibility.Visible;
            }
        }

        private decimal? GetAmountFromTextBox(TextBox textBox)
        {
            if(decimal.TryParse(textBox.Text, out decimal result))
            {
                return result;
            }
            return null;
        }
        private List<DonationVM> FilterDonations(DateTime? startDate, DateTime? endDate, decimal? minAmount, decimal? maxAmount)
        {
            if(_userDonations == null)
            {
                _userDonations = _donationManager.RetrieveDonationByUserId(_user.UserID);
            }

            // Apply filters
            return _userDonations
        .Where(d => (!startDate.HasValue || d.DonationDate >= startDate.Value) &&
                    (!endDate.HasValue || d.DonationDate <= endDate.Value) &&
                    (!minAmount.HasValue || d.Amount >= minAmount.Value) &&
                    (!maxAmount.HasValue || d.Amount <= maxAmount.Value))
        .ToList();
        }

        private void RestorePlaceholder(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if(tb.Name == "txtMinAmount")
                {
                    tb.Text = "Min Amount";
                }
                else if(tb.Name == "txtMaxAmount")
                {
                    tb.Text = "Max Amount";
                }
            }
        }

        private void ClearTextBox(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(tb != null && (tb.Text == "Min Amount" || tb.Text == "Max Amount"))
            {
                tb.Text = string.Empty;
            }
        }

        private void lstView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(lstView.SelectedItem is DonationVM selectedDonation)
            {
                ViewSingleDonationDetails.SelectedDonation = selectedDonation; 
                this.NavigationService.Navigate(new ViewSingleDonationDetails(selectedDonation.DonationID));
            }
            else
            {
                MessageBox.Show("Please select a donation to view its invoice.");
            }
        }

        private void btnSummary_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new DON05_viewDonatedProjects(_user.UserID));
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Donation donation = (Donation)lstView.SelectedItem;
                if(donation == null)
                {
                    MessageBox.Show("Please choose a donation to view.", "Invalid Donation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    NavigationService.GetNavigationService(this)?.Navigate(new ViewSingleDonationDetails(donation.DonationID, donation.ProjectID));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
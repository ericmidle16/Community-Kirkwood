/// <summary>
/// Akoi Kollie
/// Created: 2025/03/19
/// 
/// This a is the class that display for user input when the project has loaded
/// It make connection the user input to the the database.
/// </summary>
///
/// <remarks>
/// Updater  Stan Anderson
/// Updated: 2025/04/07
/// 
/// Updater:    Chase Hannen
/// Updated:    2025/05/02
/// </remarks>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class ViewAllDonations : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private DonationManager _donationManager = new DonationManager();
        List<Donation> _donations = new List<Donation>();
        Project project = null;

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: Initializes the ViewAllDonations page and sets up the DonationManager instance. 
        /// This constructor prepares the UI components and ensures user data management
        /// can be accessed and display all donation made to a project 
        /// 
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/07
        /// What Was Changed: Removed hardcoding
        /// </summary>
        public ViewAllDonations(Project project)
        {
            this.project = project;
            InitializeComponent();
            validatePermission();
        }

        private void validatePermission()
        {
            btnView.Visibility = Visibility.Hidden;

            if (main.isLoggedIn)
            {
                if (main.HasProjectRole("Accountant", project.ProjectID) ||
                    main.HasProjectRole("Purchaser", project.ProjectID) ||
                    main.HasProjectRole("Project Starter", project.ProjectID) ||
                     main.HasProjectRole("Donator", project.ProjectID) ||
                    main.HasSystemRole("Admin"))
                {
                    btnView.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/03
        /// Summary:
        ///     This is the code that returns all donation to the project
        /// </summary>
        public void viewDonation()
        {
            _donations.Clear();
        }

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/03
        /// Summary:
        ///     This is the page loaded event that display list of all donations made to a project
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/07 
        /// example: Added title content
        /// 
        /// Updater Name:   Chase Hannen
        /// Updated:        2025/05/02
        /// Summary:        Removed MessageBox in catch, omitted and renamed columns
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _donations = _donationManager.SelectToViewDonations(project.ProjectID);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception: " + ex);
                _donations = new List<Donation>();
            }

            grdViewDonations.ItemsSource = _donations;
            lblTitle.Content = "Donations for " + project.Name + " (" + _donations.Count() + ")";
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Donation donation = (Donation)grdViewDonations.SelectedItem;
                if(donation == null)
                {
                    MessageBox.Show("Please select a donation to view", "Invalid Donation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    NavigationService.GetNavigationService(this)?.Navigate(new ViewSingleDonationDetails(donation.DonationID, project.ProjectID));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnViewExpenses_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new SelectAllProjectExpenses(project));
        }
        private void grdViewDonations_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            // Cancel columns to omit
            if(headerName == "DonationID")
            {
                e.Cancel = true;
            }
            if(headerName == "UserID")
            {
                e.Cancel = true;
            }
            if(headerName == "ProjectID")
            {
                e.Cancel = true;
            }
            // Rename column headers
            if(headerName == "DonationType")
            {
                e.Column.Header = "Donation Type";
            }
            if(headerName == "DonationDate")
            {
                e.Column.Header = "Donation Date";
            }
        }
    }
}

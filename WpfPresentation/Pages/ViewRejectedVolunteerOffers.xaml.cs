/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/20
/// Summary:  Interaction logic for ViewRejectedVolunteerOffers.xaml
/// </summary>
/// <remarks>
/// Updated By: Stan Anderson
/// Updated: 2025/04/09
/// What was Changed: Added connections	, removed hardcoding
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-11
/// What Was Changed:
///     Added functionality for the btnViewAll_Click & btnViewRequests_Click methods.
/// </remarks>

using DataDomain;
using LogicLayer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class ViewRejectedVolunteerOffers : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        int _projectID;
        public ObservableCollection<VMVolunteerStatus> _volunteerStatuses { get; set; }

        VolunteerStatusManager _volunteerStatusManager = new VolunteerStatusManager();
        UserManager _userManager = new UserManager();
        public ViewRejectedVolunteerOffers(int project)
        {
            InitializeComponent();
            this._projectID = project;
            GetPrivileges();
        }

        private void GetPrivileges()
        {
            btnViewAll.Visibility = Visibility.Collapsed;
            btnViewRequests.Visibility = Visibility.Collapsed;
            if (main.HasProjectRole("Project Starter", _projectID) || 
                main.HasProjectRole("Volunteer Director", _projectID) || 
                main.HasSystemRole("Admin")){
                btnViewRequests.Visibility = Visibility.Visible;
            }
            if(main.HasProjectRole("Project Starter", _projectID) ||
                main.HasProjectRole("Volunteer Director", _projectID) ||
                main.HasProjectRole("Background Checker", _projectID) ||
                main.HasProjectRole("Scheduler", _projectID) ||
                main.HasSystemRole("Admin"))
            {
                btnViewAll.Visibility = Visibility.Visible;
            }   
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/20
        /// Summary:  Method called to populate the lvVolunteerStatus list view
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/02/20
        /// What was Changed: Initial creation	
        /// </summary>
        private void LoadVolunteerStatusList()
        {
            try
            {
                _volunteerStatuses = new ObservableCollection<VMVolunteerStatus>(_volunteerStatusManager.GetRejectedVolunteerStatusByProjectID(_projectID));
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;

                MessageBox.Show(message);
            }
            DataContext = this;
            if (_volunteerStatuses.IsNullOrEmpty())
            {
                txtNoRequests.Visibility = Visibility.Visible;
            }
            else
            {
                txtNoRequests.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/02/20
        /// Summary:  Method called when an accept button is clicked. Promotes the user for 
        ///           confirmation of accepting the volunteer request
        ///           Updates the relevent VolunteerStatus record for Approved value to true
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/02/20
        /// What was Changed: Initial creation	
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to accept this volunteer request that was already rejected?", "Are you sure?", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                if (button == null) return;

                VMVolunteerStatus volunteerStatus = button.DataContext as VMVolunteerStatus;
                volunteerStatus.Approved = true;
                _volunteerStatusManager.UpdateVolunteerStatus(volunteerStatus);
                _volunteerStatuses.Remove(volunteerStatus);
                if (_volunteerStatuses.IsNullOrEmpty())
                {
                    txtNoRequests.Visibility = Visibility.Visible;
                }
                else
                {
                    txtNoRequests.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadVolunteerStatusList();
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            // view all volunteers by projectID
            NavigationService.GetNavigationService(this)?.Navigate(new ViewVolunteerList(_projectID));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnViewRequests_Click(object sender, RoutedEventArgs e)
        {
            // view all requests by projectID
            NavigationService.GetNavigationService(this)?.Navigate(new AcceptVolunteerOffers(_projectID));
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            VMVolunteerStatus volunteerStatus = button.DataContext as VMVolunteerStatus;

            List<User> users = _userManager.GetAllUsers();
            User volunteer = null;

            foreach (User user in users)
            {
                if(user.UserID == volunteerStatus.UserID)
                {
                    volunteer = user;
                    break;
                }
            }

            NavigationService.Navigate(new ViewVolunteerProfile(volunteer));
        }
    }
}

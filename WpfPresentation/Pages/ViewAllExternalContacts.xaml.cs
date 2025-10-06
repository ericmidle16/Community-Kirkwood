/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/24
/// Summary:  Page that shows all of the external contacts
/// Last Updated By: Jacob McPherson
/// Last Updated: 2025/04/15
/// What was Changed: Fixed Add External Contact Connection
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/05/01
/// What was Changed: Fixed pge crashing if user is not logged in by adding "main.isLoggedIn &&" to the if statment in Page_Loaded
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages { 

    public partial class ViewAllExternalContacts : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        ExternalContactManager viewExternalContactsManager;
        List<ExternalContact> externalContacts = new List<ExternalContact>();

        public ViewAllExternalContacts()
        {
            InitializeComponent();
            GetPrivileges();
        }

        private void GetPrivileges()
        {
            btnViewDetailsExternalContact.Visibility = Visibility.Visible;
            if (main.HasSystemRole("Guest"))
            {
                btnViewDetailsExternalContact.Visibility = Visibility.Hidden;
            }
        }




        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/24
        /// Summary:  Go back to the previous page
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/06
        /// What was Changed: Added content
        /// </summary>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/24
        /// Summary:  Go to the AddExternalContact page
        /// Last Updated By: Jacob McPherson
        /// Last Updated: 2025/04/15
        /// What was Changed: Fixed Constructor Parameter
        /// </summary>
        private void btnAddExternalContact_Click(object sender, RoutedEventArgs e)
        {
            if (main.isLoggedIn)
            {
                NavigationService.GetNavigationService(this)?.Navigate(new frmAddUpdateExternalContact());
            }
            else
            {
                MessageBox.Show("Please Log In", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/24
        /// Summary:  Go to the ViewExternalContactDetails page
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        private void btnViewDetailsExternalContact_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExternalContact x = (ExternalContact)gridExternalContacts.SelectedItem;
                if(x == null)
                {
                    MessageBox.Show("An invalid external contact was selected.", "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    int externalContactID = x.ExternalContactID;
                    this.NavigationService.Navigate(new ViewSingleExternalContact(externalContactID));
                }

            }
            catch (Exception)
            {
                MessageBox.Show("An invalid external contact was selected.", "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }


        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/24
        /// Summary:  Generate the external contact grid
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        private void gridExternalContacts_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                viewExternalContactsManager = new ExternalContactManager();
                externalContacts = viewExternalContactsManager.ViewAllExternalContacts();
                lblExternalContacts.Content = "External Contacts (" + externalContacts.Count + ")";

                gridExternalContacts.ItemsSource = externalContacts;
                gridExternalContacts.Columns[0].Visibility = Visibility.Hidden;
                gridExternalContacts.Columns[1].Header = "Contact Type";
                gridExternalContacts.Columns[2].Header = "Contact Name";
                gridExternalContacts.Columns[3].Header = "First Name";
                gridExternalContacts.Columns[4].Header = "Last Name";
                gridExternalContacts.Columns[5].Header = "Email";
                gridExternalContacts.Columns[6].Header = "Phone Number";
                gridExternalContacts.Columns[7].Header = "Job Title";
                gridExternalContacts.Columns[8].Visibility = Visibility.Hidden;
                gridExternalContacts.Columns[9].Visibility = Visibility.Hidden;
                gridExternalContacts.Columns[10].Visibility = Visibility.Hidden;
                gridExternalContacts.Columns[11].Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (main.isLoggedIn && main.ProjectRoleValues.Contains("Volunteers"))
            {
                btnAddExternalContact.Visibility = Visibility.Visible;
            }
        }
    }
}

/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/28
/// Summary:  This class is for viewing a single external contact
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/06
/// What was Changed: Added connection
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class ViewSingleExternalContact : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        ExternalContactManager viewAllExternalContactsManager = new ExternalContactManager();
        int externalContactID;
        ExternalContactVM externalContact;
        public ViewSingleExternalContact(int externalContactID)
        {
            this.externalContactID = externalContactID;
            InitializeComponent();
            DisplayInformation();
            validatePermission();
            GetPrivileges();
        }
        private void validatePermission()
        {
            btnEdit.Visibility = Visibility.Hidden;
            Deactivate.Visibility = Visibility.Hidden;
            if (main.isLoggedIn && main.ProjectRoleValues.Contains("Volunteer"))
            {
                btnEdit.Visibility = Visibility.Visible;
                Deactivate.Visibility = Visibility.Visible;
            }
        }

        private void GetPrivileges()
        {
            btnEdit.Visibility = Visibility.Collapsed;
            if (main.isLoggedIn && main.UserID == externalContact.AddedBy)
            {
                btnEdit.Visibility = Visibility.Visible;
            }
        }

        

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/28
        /// Summary:  This method retrieves and displays the information about the contact.
        /// Last Updated By: 
        /// Last Updated: 
        /// What was Changed: 
        /// </summary>
        public void DisplayInformation()
        {
            try
            {
                externalContact = viewAllExternalContactsManager.ViewSingleExternalContact(externalContactID);
                lblContactName.Text = externalContact.ContactName;
                lblFullName.Text = externalContact.GivenName + " " + externalContact.FamilyName;
                lblContactTypeID.Text = externalContact.ExternalContactTypeID;
                lblContactTypeDescription.Text = externalContact.TypeDescription;
                lblEmail.Text = externalContact.Email;
                lblPhone.Text = externalContact.PhoneNumber;
                lblJobTitle.Text = externalContact.JobTitle;
                lblAddress.Text = externalContact.Address;
                lblDescription.Text = externalContact.Description;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/28
        /// Summary:  This method will go back to the previous page
        /// Last Updated By: 
        /// Last Updated: 
        /// What was Changed: 
        /// </summary>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/28
        /// Summary:  This method will go to edit
        /// Last Updated By: Jacob McPherson
        /// Last Updated: 2025/04/15
        /// What was Changed: Required Login
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (main.isLoggedIn)
            {
                NavigationService.GetNavigationService(this)?.Navigate(new frmAddUpdateExternalContact(externalContact));
            }
            else
            {
                MessageBox.Show("Please Log In", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/28
        /// Summary:  This method will deactivate
        /// Last Updated By: Jacob McPherson
        /// Last Updated: 2025/03/11
        /// What was Changed: Added Functionality
        /// </summary>
        private void Deactivate_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;

            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to deactivate this external contact?", "Deactivate External Contact", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    success = viewAllExternalContactsManager.DeactivateExternalContact(externalContactID);

                    if (success)
                    {
                        MessageBox.Show("Contact Deactivated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GetNavigationService(this)?.Navigate(new ViewAllExternalContacts());
                    }
                    else
                    {
                        MessageBox.Show("Failed to Deactivate", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Deactivate", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }





        
    }
}

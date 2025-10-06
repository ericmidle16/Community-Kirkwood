/// <summary>
/// Jacob McPherson
/// Created: 2025/02/18
/// 
/// Interaction logic for frmAddUpdateExternalContact.xaml
/// </summary>
///
/// <remarks>
/// Updater Name: Stan Anderson
/// Updated: 2025/04/06
/// </remarks>
using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class frmAddUpdateExternalContact : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private ExternalContactManager _contactManager;
        private List<string> _types;
        private ExternalContact? _contact = null;

        //Constructor for Adding
        public frmAddUpdateExternalContact()
        {
            InitializeComponent();
            _contactManager = new ExternalContactManager();
            _types = _contactManager.GetAllExternalContactTypes();

            cmbType.ItemsSource = _types;
        }

        //Constructor for Updating
        public frmAddUpdateExternalContact(ExternalContact externalContact)
        {
            InitializeComponent();
            _contactManager = new ExternalContactManager();
            _types = _contactManager.GetAllExternalContactTypes();
            _contact = externalContact;

            cmbType.ItemsSource = _types;

            lblTitle.Content = "Update External Contact";

            for (int i = 0; i < cmbType.Items.Count; i++)
            {
                if (cmbType.Items[i].ToString() == _contact.ExternalContactTypeID)
                {
                    cmbType.SelectedIndex = i;
                }
            }
			PopulateTextBoxes();
            GetPrivileges();
        }


        private void GetPrivileges()
        {
            btnManageTypes.Visibility = Visibility.Collapsed;
            if (main.isLoggedIn)
            {
                btnManageTypes.Visibility = Visibility.Visible;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are You Sure You Want to Cancel?", "Cancel?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtCompanyName.Text == null || txtCompanyName.Text == "")
            {
                MessageBox.Show("Add a Company Name", "Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cmbType.Text == null || cmbType.Text == "")
            {
                MessageBox.Show("Select a Contact Type", "Required Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ExternalContact contact = new ExternalContact()
            {
                GivenName = txtGivenName.Text,
                FamilyName = txtFamilyName.Text,
                ContactName = txtCompanyName.Text,
                Email = txtEmail.Text,
                JobTitle = txtTitle.Text,
                PhoneNumber = txtPhone.Text,
                Address = txtAddress.Text,
                Description = txtDescription.Text,
                ExternalContactTypeID = cmbType.Text,
                AddedBy = main.UserID
            };

            bool success = false;
            
            if(_contact != null)
            {
                try
                {
                    contact.ExternalContactID = _contact.ExternalContactID;
                    success = _contactManager.EditExternalContact(main.UserID, contact, _contact);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Updating External Contact", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (success)
                {
                    MessageBox.Show("Contact succesfully updated!", "External Contact Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GetNavigationService(this)?.Navigate(new ViewAllExternalContacts());
                }
                else
                {
                    MessageBox.Show("There was an unexpected error updating the external contact.", "Error Updating External Contact", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    success = _contactManager.AddExternalContact(contact);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Adding External Contact", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (success)
                {
                    MessageBox.Show("Contact succesfully added!", "External Contact Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GetNavigationService(this)?.Navigate(new ViewAllExternalContacts());
                }
                else
                {
                    MessageBox.Show("There was an unexpected error adding the external contact.", "Error Adding External Contact", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnManageTypes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new frmAddExternalContactType());

        }

        public void PopulateTextBoxes()
        {
            if (_contact != null)
            {
                for (int i = 0; i < cmbType.Items.Count; i++)
                {
                    if (cmbType.Items[i].ToString() == _contact.ExternalContactTypeID)
                    {
                        cmbType.SelectedIndex = i;
                    }
                }

                txtGivenName.Text = _contact.GivenName;
                txtFamilyName.Text = _contact.FamilyName;
                txtEmail.Text = _contact.Email;
                txtPhone.Text = _contact.PhoneNumber;
                txtCompanyName.Text = _contact.ContactName;
                txtTitle.Text = _contact.JobTitle;
                txtAddress.Text = _contact.Address;
                txtDescription.Text = _contact.Description;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateTextBoxes();
        }
    }
}

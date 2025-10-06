/// <summary>
/// Creator: Christivie Mauwa
/// Created: 2025/02/06
/// Summary:
///     Class for the creation of User Objects with set data fields
/// </summary>
///
/// <remarks>
/// Updater Name: Stan Anderson
/// Updated: 2025/04/08
/// 
/// Updater Name: Syler Bushlack
/// Updated: 2025/04/08
/// What was changed: Fixed btnPay_Click validation and the Page_Loaded method so that txtUserName is auto populated
/// 
/// Updater Name: Chase Hannen
/// Updated: 2025/05/01
/// What was changed: Added UpdateAvailableFunds
/// </remarks>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class DonateToProject : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IDonationManager _donationManager = null;
        IProjectManager _projectManager = null;
        IUserManager _userManager = null;
        IDonationTypeManager _donationTypeManager = null;
        private DonationVM _donationVM;
        private ProjectVM _projectVM;
        private List<DonationType> _donationType;
        int projectID;

        public DonateToProject()
        {
            InitializeComponent();
            _donationManager = new DonationManager();
            _projectManager = new ProjectManager();
            _userManager = new UserManager();
            _donationTypeManager = new DonationTypeManager();
        }

        public DonateToProject(int projectID)
        {
            this.projectID = projectID;
            InitializeComponent();
            _donationManager = new DonationManager();
            _projectManager = new ProjectManager();
            _userManager = new UserManager();
            _donationTypeManager = new DonationTypeManager();
            GetProjectData(projectID);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Donation canceled.", "Cancel", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void GetProjectData(int projectID)
        {
            try
            {
                _projectVM = _projectManager.GetProjectVMByID(projectID);
                if(_projectVM == null)
                {
                    MessageBox.Show("No project found with the provided ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    txtProjectName.Text = _projectVM.Name;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
       
        private void GetDonationTypeData()
        {
            try
            {
                _donationType = _donationTypeManager.GetAllDonationType();
                if(_donationType == null || _donationType.Count == 0)
                {
                    MessageBox.Show("No donation types found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    cbDonationType.ItemsSource = _donationType;
                    cbDonationType.DisplayMemberPath = "DonationTypeID";
                    cbDonationType.SelectedValuePath = "DonationTypeID";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if(_projectVM == null)
            {
                MessageBox.Show("Project information is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(txtUserName.Text.Equals("") || txtUserName.Text == null)
            {
                MessageBox.Show("User information is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(cbDonationType.SelectedValue == null)
            {
                MessageBox.Show("Please select a donation type.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var selectedDonationTypeID = cbDonationType.SelectedValue.ToString();

            if(!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid donation amount.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                string userName;
                if(chkAnonymous.IsChecked == true)
                {
                    txtUserName.Text = "Anonymous";
                    txtUserName.IsEnabled = false;
                }
                else
                {
                    txtUserName.Text = $"{main.GivenName} {main.FamilyName}";
                    txtUserName.IsEnabled = true;
                }

                DonationVM newDonation = new DonationVM
                {
                    ProjectID = _projectVM.ProjectID,
                    UserID = chkAnonymous.IsChecked == true ? 0 : main.UserID,
                    DonationType = selectedDonationTypeID,
                    Amount = amount,
                    DonationDate = DateTime.Now,
                    Description = txtDescription.Text
                };
                bool success = _donationManager.AddDonation(newDonation);

                if(success)
                {
                    _projectManager.UpdateAvailableFunds(projectID, amount, true);

                    MessageBox.Show("Thank you for your donation!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Failed to process donation. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            _donationVM = new DonationVM();
            GetProjectData(projectID);

            if(main.isLoggedIn)
            {
                chkAnonymous.IsEnabled = true;
                txtUserName.Text = $"{main.GivenName} {main.FamilyName}";
            }
            else
            {
                chkAnonymous.IsChecked = true;
                chkAnonymous.IsEnabled = false;
                txtUserName.Text = "Anonymous";
            }

            GetDonationTypeData();
            txtDate.Text = DateTime.Now.ToString("MM-dd-yyyy");

        }

        private void chkAnonymous_Checked(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "Anonymous";
            txtUserName.IsEnabled = false;
        }

        private void chkAnonymous_Unchecked(object sender, RoutedEventArgs e)
        {
            txtUserName.IsEnabled = true;

            if(main.isLoggedIn)
            {
                txtUserName.Text = main.GivenName + " " + main.FamilyName;
            }
            else
            {
                txtUserName.Text = "";
            }
        }
    }
}
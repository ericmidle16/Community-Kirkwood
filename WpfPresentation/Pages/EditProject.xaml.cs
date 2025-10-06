/// <summary>
/// Christivie Mauwa
/// Created: 2025/02/06
/// 
/// Actual summary of the class if needed, example is for DTO
/// Class for the creation of User Objects with set data fields
/// </summary>
///
/// <remarks>
/// Updater Name: Stan Anderson
/// Updated: 2025/04/10
/// </remarks>
using DataDomain;
using LogicLayer;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class EditProject : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private ProjectVM _project;
        private IProjectManager _projectManager = null;
        LocationManager _locationManager = new LocationManager();
        List<Location> _locations = new List<Location>();
        public EditProject(Project project)
        {
            InitializeComponent();
            _projectManager = new ProjectManager();
            try
            {
                _project = _projectManager.GetProjectVMByID(project.ProjectID);
            }
            catch (Exception)
            {
                _project = (ProjectVM)project;
            }
            GetLocations();
            PopulateProjectDetails();
        }

       


        private void PopulateProjectDetails()
        {
            if (_project != null)
            {
                txtProjectName.Text = _project.Name;
                cbxLocationName.SelectedValue = _project.LocationID;
                dtpStartDate.Text = _project.StartDate.ToString("MM-dd-yyyy");
                txtDescription.Text = _project.Description;
                txtProjectType.Text = _project.ProjectTypeID;
                ckbAcceptsDonation.IsChecked = _project.AcceptsDonations;
                tbxPayPalAccount.Text = _project.PayPalAccount;
                cbStatus.SelectedItem = GetStatusComboBoxItem(_project.Status);
                txtUser.Text = $"{_project.GivenName} {_project.FamilyName}";
            }
        }
        private ComboBoxItem GetStatusComboBoxItem(string status)
        {
            // Find the ComboBoxItem matching the project's status
            foreach (ComboBoxItem item in cbStatus.Items)
            {
                if (item.Content.ToString() == status)
                {
                    return item;
                }
            }
            return null;
        }

        public bool emailValidation(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void GetLocations()
        {
            _locations = _locationManager.ViewLocationList();
            cbxLocationName.ItemsSource = _locations;
            cbxLocationName.DisplayMemberPath = "Name";
            cbxLocationName.SelectedValuePath = "LocationID";
        }
        // Helper Methods
        private void GetProjectData(int projectID)
        {
            try
            {
                // For testing the DB.
                _project = _projectManager.GetProjectVMByID(projectID);
                if (_project == null)
                {
                    MessageBox.Show("No project found with the provided ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(_project.ProjectID));
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            SaveChange();
            NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(_project.ProjectID));
        }
        private void SaveChange()
        {
            // Validate if the field has input
            if (string.IsNullOrEmpty(txtProjectName.Text) || string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Please fill in all the required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedLocation = cbxLocationName.SelectedItem as Location;

            if (selectedLocation == null)
            {
                MessageBox.Show("Please select a valid location.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ckbAcceptsDonation.IsChecked == true && string.IsNullOrWhiteSpace(tbxPayPalAccount.Text) || ckbAcceptsDonation.IsChecked == true && emailValidation(tbxPayPalAccount.Text) == false)
            {
                MessageBox.Show("Please enter a valid Paypal Email.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbxPayPalAccount.Focus();
                tbxPayPalAccount.SelectAll();
                return;
            }

            ProjectVM newProject = new ProjectVM()
            {
                ProjectID = _project.ProjectID,
                Name = txtProjectName.Text,
                LocationID = (int)cbxLocationName.SelectedValue,
                StartDate = _project.StartDate,
                Description = txtDescription.Text,
                ProjectTypeID = txtProjectType.Text,
                AcceptsDonations = (bool)ckbAcceptsDonation.IsChecked,
                PayPalAccount = null,
                Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString(),
                GivenName = txtUser.Text
            };
            if (ckbAcceptsDonation.IsChecked == true && emailValidation(tbxPayPalAccount.Text) == true)
            {
                newProject = new ProjectVM()
                {
                    ProjectID = _project.ProjectID,
                    Name = txtProjectName.Text,
                    LocationID = (int)cbxLocationName.SelectedValue,
                    StartDate = _project.StartDate,
                    Description = txtDescription.Text,
                    ProjectTypeID = txtProjectType.Text,
                    AcceptsDonations = (bool)ckbAcceptsDonation.IsChecked,
                    PayPalAccount = tbxPayPalAccount.Text,
                    Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    GivenName = txtUser.Text
                };
            }


            try
            {
                // Call EditProject with the old and new project
                bool success = _projectManager.EditProject(_project, newProject);
                if (success)
                {
                    MessageBox.Show("Project updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    _project = newProject;
                }
                else
                {
                    MessageBox.Show("Project updated?", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Updates the character count display for the about me text box
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/10
        /// Copied this from UpdateUserDesktop
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox rtb = (TextBox)sender; // Same as "sender as RichTextBox"
            lblTextBoxCharDisplay.Content = $"{rtb.Text.Length}/250";
        }

        private void ckbAcceptsDonation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

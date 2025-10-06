/// <summary>
/// Josh Nicholson
/// Created: 2025/02/07
/// Summary: Class for the createProject xaml 
/// error checking and input to the database
/// 
/// Updated By: Stan Anderson
/// Updated: 2025/04/08
/// What Was Changed: Added _accessToken stuff
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed:
///     Added automation logic for when a new Project is created:
///         Pull the newly inserted ProjectID from the AddProject() method.
///         Use that newProjectID to create ForumPermission record with WriteAccess set to true for the User who is logged in & created the new Project.
///         Create a Welcome Thread & Post in the Project Forum.
///         Create an Approved VolunteerStatus record for the User who is logged in & created the new Project.
///         Assign all existing ProjectRoles to the User who is logged in & created the new Project.
/// </summary>

using System.Windows;
using System.Windows.Controls;
using LogicLayer;
using DataDomain;
using System.Net.Mail;

namespace WpfPresentation.Pages
{
    public partial class createProject : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IProjectManager _projectManager = new ProjectManager();
        ILocationManager _locationManager = new LocationManager();
        List<Location> _locations = new List<Location>();
        List<ProjectVM> _projects = new List<ProjectVM>();

        private IThreadManager _threadManager = new ThreadManager();
        private IForumPermissionManager _forumPermissionManager = new ForumPermissionManager();
        private IVolunteerStatusManager _volunteerStatusManager = new VolunteerStatusManager();
        private IVolunteerStatusProjectRoleManager _volunteerStatusProjectRoleManager = new VolunteerStatusProjectRoleManager();
        private IProjectRoleManager _projectRoleManager = new ProjectRoleManager();

        public createProject()
        {
            InitializeComponent();
            populateComboBoxes();
            _projects = _projectManager.GetAllProjects();

            lblTextBoxCharDisplay.Content = "0/250";

        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/07
        /// Summary: Saves a project to the database if the inputs are valid,
        /// and one with the entered project name doesn't already exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProjectSave_Click(object sender, RoutedEventArgs e)
        {

            if(string.IsNullOrWhiteSpace(txtProjectName.Text) || txtProjectName.Text.Length > 100)
            {
                MessageBox.Show("Please enter a valid name.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtProjectName.Focus();
                txtProjectName.SelectAll();
                return;
            }

            if(cmbLocationName.SelectedItem == null)
            {
                MessageBox.Show("Please select a Location", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtProjectDesc.Focus();
                txtProjectDesc.SelectAll();
                return;
            }

            if(cmbProjectType.SelectedItem == null)
            {
                MessageBox.Show("Please select a Project Type", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtProjectDesc.Focus();
                txtProjectDesc.SelectAll();
                return;
            }

            if(cmbProjectStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a Status", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtProjectDesc.Focus();
                txtProjectDesc.SelectAll();
                return;
            }

            if(!dtpkProjectStart.SelectedDate.HasValue)
            {
                MessageBox.Show("Please enter a valid Start Date", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                dtpkProjectStart.Focus();
                return;
            }

            if(dtpkProjectStart.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Please enter a valid Start Date", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                dtpkProjectStart.Focus();
                return;
            }

            if(txtProjectDesc.Text.Length > 250)
            {
                MessageBox.Show("Please enter a valid Description", "Too Many Characters", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtProjectDesc.Focus();
                txtProjectDesc.SelectAll();
                return;
            }

            if(txtPaypalAccount.Text.Length > 50)
            {
                MessageBox.Show("Please enter a valid Paypal Email.", "Too Many Characters", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtProjectDesc.Focus();
                txtProjectDesc.SelectAll();
                return;
            }

            if(chbProjectDonation.IsChecked == true && string.IsNullOrWhiteSpace(txtPaypalAccount.Text) || chbProjectDonation.IsChecked == true && emailValidation(txtPaypalAccount.Text) == false)
            {
                MessageBox.Show("Please enter a valid Paypal Email.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtProjectDesc.Focus();
                txtProjectDesc.SelectAll();
                return;
            }

            if(_projects.Any(project => project.Name == txtProjectName.Text))
            {
                var result = MessageBox.Show("A project with this name exists. \nAre you sure you would like to add this project?",
                    "Confirm Project Addition",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    return; // Stop execution
                }
            }

            Project newProject = new Project()
            {
                Name = txtProjectName.Text,
                ProjectTypeID = (string)cmbProjectType.SelectedValue,
                LocationID = _locations.First(location => location.Name == cmbLocationName.SelectedItem).LocationID,
                UserID = main.UserID,
                StartDate = (DateTime)dtpkProjectStart.SelectedDate,
                Status = (string)cmbProjectStatus.SelectedValue,
                Description = txtProjectDesc.Text,
                AcceptsDonations = (bool)chbProjectDonation.IsChecked,
                PayPalAccount = txtPaypalAccount.Text
            };

            try
            {
                // Get the new Project ID.
                int newProjectID = _projectManager.AddProject(newProject);

                // Create ForumPermission record for the Project Starter
                ForumPermission newForumPermission = new ForumPermission()
                {
                    UserID = main.UserID,
                    ProjectID = newProjectID,
                    WriteAccess = true
                };
                _forumPermissionManager.AddForumPermission(newForumPermission);

                // Create Project Forum - First Thread & Post
                _threadManager.InsertForumPost(main.UserID, "Welcome to the Project!", newProjectID, "Welcome!!", DateTime.Now);

                // Create VolunteerStatus record for Project Starter & set Approved to true.
                _volunteerStatusManager.AddVolunteerStatus(main.UserID, newProjectID);
                VMVolunteerStatus newVolunteerStatus = new VMVolunteerStatus()
                {
                    UserID = main.UserID,
                    ProjectID = newProjectID,
                    Approved = true
                };
                _volunteerStatusManager.UpdateVolunteerStatus(newVolunteerStatus);

                // Get all exisitng ProjectRoles.
                List<ProjectRole> projectRoles = _projectRoleManager.GetAllProjectRoles();
                // Create VolunteerStatusProjectRole records (assign the Project Starter ALL OF THEM)
                foreach(ProjectRole projectRole in projectRoles)
                {
                    _volunteerStatusProjectRoleManager.InsertUserProjectRole(main.UserID, newProjectID, projectRole.ProjectRoleID);
                }

                MessageBox.Show("Project was successfully created.", "Project Made", MessageBoxButton.OK, MessageBoxImage.Information);
                _projects = _projectManager.GetAllProjects();

                // right now it doesn't consider this new project to be one of the user's projects. this needs to be fixed
                string name = main.GivenName + " " + main.FamilyName;
                NavigationService.Navigate(new viewAssignedProjects(main.UserID, name, main.Email));

            }
            catch(Exception ex)
            {
                MessageBox.Show("This project already exists.", "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/03/28
        /// Summary: Allow the PayPal Account to be entered if the checkbox is flipped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbProjectDonation_Click(object sender, RoutedEventArgs e)
        {
            txtPaypalAccount.IsEnabled = !txtPaypalAccount.IsEnabled;
            txtPaypalAccount.Text = "";
        }

        public void populateComboBoxes()
        {
            cmbProjectStatus.Items.Add("Not Started");
            cmbProjectStatus.Items.Add("In Progress");

            try
            {
                _locations = _locationManager.ViewLocationList();
                foreach (Location location in _locations)
                {
                    cmbLocationName.Items.Add(location.Name);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to obtain Locations.", "Procedure Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            try
            {
                List<ProjectTypeObject> projectType = _projectManager.GetAllProjectTypes();
                foreach (ProjectTypeObject projecttype in projectType)
                {
                    cmbProjectType.Items.Add(projecttype.ProjectType);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to obtain Project types.", "Procedure Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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

        private void btnProjectCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void getUser()
        {
            if(main.isLoggedIn == false) // the user is NOT logged in
            {
                MessageBoxResult result = MessageBox.Show("You must be logged in to create a new project.\n\nWould you like to be redirected to the login page?", "Invalid User", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    main.sendToLogin();
                } 
                else
                {
                    NavigationService.Navigate(new ListOfAllProjects());
                    btnProjectSave.IsEnabled = false;
                }
            }
            else // the user IS logged in
            {
                btnProjectSave.IsEnabled = true;
            }

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            getUser();
        }

        /// <summary>
        /// Brrodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Updates the character count display for the about me text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProjectDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox rtb = (TextBox)sender; // Same as "sender as RichTextBox"
            lblTextBoxCharDisplay.Content = $"{rtb.Text.Length}/{250}";
        }
    }
}
/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-10
/// Summary:
///     C# code file which contains the Presentation Layer code for the
///     Create Background Check feature - adding a background check record
///     to the DB.
/// 
/// Updated By: Kate Rich
/// Updated: 2025-02-26
/// What Was Changed:
///     Added the logic for viewing an existing BackgroundCheck record.
///     Linked this page to the View List of Background Checks page - no new stored procedure
///     needed, as I'm just passing an existing background check to this page from there.
///     Added the logic for updating an existing BackgroundCheck record.
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-15
/// What Was Changed:
///     Updated the btn_Back_Click() to go back to the correct page (UI connection).
///     Updated the Page_Loaded() method to have a dynamic title, depending on if it's a
///     new vs. existing background check record.
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for createBackgroundCheckDesktop.xaml
    /// </summary>
    public partial class createBackgroundCheckDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        IBackgroundCheckManager _backgroundCheckManager = new BackgroundCheckManager();

        User? _targetUser;
        Project? _project;

        BackgroundCheckVM? _backgroundCheck;

        public createBackgroundCheckDesktop(User targetUser, Project project)
        {
            _targetUser = targetUser;
            _project = project;

            InitializeComponent();
        }

        public createBackgroundCheckDesktop(BackgroundCheckVM backgroundCheck)
        {
            _backgroundCheck = backgroundCheck;

            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(_backgroundCheck == null)
            {
                // NEW BACKGROUND CHECK.

                // Validate Description field.
                if(txtDescription.Text.Length < 5 || txtDescription.Text.Length > 250)
                {
                    MessageBox.Show("Invalid Background Check Description...");
                    txtDescription.Focus();
                    txtDescription.SelectAll();
                    return;
                }

                // Set values.
                BackgroundCheck newBackgroundCheck = new BackgroundCheck()
                {
                    Investigator = main.UserID,
                    UserID = _targetUser.UserID,
                    ProjectID = _project.ProjectID,
                    Status = cboBackgroundStatus.SelectedItem.ToString(),
                    Description = txtDescription.Text
                };

                // Call manager in try-catch to submit to DB.
                bool result;
                try
                {
                    result = _backgroundCheckManager.AddBackgroundCheck(newBackgroundCheck);
                    if(result == false)
                    {
                        throw new Exception("Background Check Creation Failed...");
                    }
                    else
                    {
                        MessageBox.Show("Background Check Added Successfully!");

                        // Navigate to list of background checks for the project - with new background check record added.
                        this.NavigationService.Navigate(new BGC_008_viewListOfBackgroundChecks(_project.ProjectID));
                    }
                }
                catch(Exception ex)
                {
                    string message = ex.InnerException == null ? ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                    MessageBox.Show(message);
                }
            }
            if(_backgroundCheck != null)
            {
                // EXISTING BACKGROUND CHECK

                // Validate the Description Field.
                if(txtDescription.Text.Length < 5 || txtDescription.Text.Length > 250)
                {
                    MessageBox.Show("Invalid Background Check Description...");
                    txtDescription.Focus();
                    txtDescription.SelectAll();
                    return;
                }

                // Passed Validation - Create a new object to pass.
                BackgroundCheck newBackgroundCheck = new BackgroundCheck()
                {
                    BackgroundCheckID = _backgroundCheck.BackgroundCheckID,
                    Investigator = _backgroundCheck.Investigator,
                    UserID = _backgroundCheck.UserID,
                    ProjectID = _backgroundCheck.ProjectID,
                    Status = cboBackgroundStatus.Text,
                    Description = txtDescription.Text
                };

                // Call manager in try-catch to submit to DB.
                bool result;
                try
                {
                    result = _backgroundCheckManager.EditBackgroundCheck(_backgroundCheck, newBackgroundCheck);
                    if(result == false)
                    {
                        throw new Exception("Background Check Update Failed...");
                    }
                    else
                    {
                        MessageBox.Show("Background Check Updated Successfully!");

                        // Navigate to list of background checks for the project - showing the updated background check in the list.
                        this.NavigationService.Navigate(new BGC_008_viewListOfBackgroundChecks(_backgroundCheck.ProjectID));
                    }
                }
                catch(Exception ex)
                {
                    string message = ex.InnerException == null ? ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                    MessageBox.Show(message);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Ask if user is sure they want to abandon the form.
            var result = MessageBox.Show("Are you sure you want to abandon this form?\nYour changes will not be saved.", "Abandon Form?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                // Whether it's a NEW Background Check or an EXISTING one, just go back.
                NavigationService.GoBack();
            }
            // If no, then stay on form.
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cboBackgroundStatus.ItemsSource = new List<string>() { "In Progress", "Failed", "Passed" };

            txtInvestigatorFirstName.IsEnabled = false;
            txtInvestigatorLastName.IsEnabled = false;
            txtTargetFirstName.IsEnabled = false;
            txtTargetLastName.IsEnabled = false;
            txtProjectName.IsEnabled = false;

            if(_backgroundCheck == null)
            {
                // NEW BACKGROUND CHECK

                lblTitle.Content = "Create Background Check";

                // Prepopulate fields
                // Investigator's name - user who is logged in.
                txtInvestigatorFirstName.Text = main.GivenName;
                txtInvestigatorLastName.Text = main.FamilyName;

                // User's name - user who is target of background check - pulled from their volunteer application for the project.
                txtTargetFirstName.Text = _targetUser.GivenName;
                txtTargetLastName.Text = _targetUser.FamilyName;

                // Project name - project user is volunteering for - pulled from their volunteer application for the project.
                txtProjectName.Text = _project.Name;

                // Status - this is already prepopulated to the default value.
                cboBackgroundStatus.SelectedItem = "In Progress";
                cboBackgroundStatus.IsEnabled = false;

                txtDescription.Focus();
            }
            if(_backgroundCheck != null)
            {
                // EXISTING BACKGROUND CHECK

                lblTitle.Content = "Edit Background Check";
                btnSubmit.Content = "Save";

                txtInvestigatorFirstName.Text = _backgroundCheck.InvestigatorGivenName;
                txtInvestigatorLastName.Text = _backgroundCheck.InvestigatorFamilyName;

                txtTargetFirstName.Text = _backgroundCheck.VolunteerGivenName;
                txtTargetLastName.Text = _backgroundCheck?.VolunteerFamilyName;

                txtProjectName.Text = _backgroundCheck.ProjectName;

                cboBackgroundStatus.SelectedItem = _backgroundCheck.Status;
                cboBackgroundStatus.IsEnabled = true;

                lblDescription.Content = "Updated Findings/Reason for Background Check:";
                txtDescription.Text = _backgroundCheck.Description;

                cboBackgroundStatus.Focus();
            }
        }
    }
}
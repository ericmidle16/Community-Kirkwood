/// <summary>
/// Creator:    Chase Hannen
/// Created:    2025/02/10
/// Summary:    The C# code for the Unassign Volunteers page
/// Last Updated By:
///             Chase Hannen
/// Last Updated:
///             2025/02/10
/// What was Changed:
///             Initial Creation
/// </summary>

using System.Windows;
using System.Windows.Controls;
using LogicLayer;
using DataDomain;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for unassignVolunteersDesktop.xaml
    /// </summary>
    public partial class unassignVolunteersDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        UserManager _userManager = new UserManager();
        int _projectID;

        public unassignVolunteersDesktop(int projectID)
        {
            InitializeComponent();
            _projectID = projectID;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> users;
            try
            {
                users = _userManager.GetUsersByProjectID(_projectID);
                grdVolunteers.ItemsSource = users;
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message);
            }
        }

        private void grdVolunteers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            // Cancel columns to omit
            if (headerName == "UserID")
            {
                e.Cancel = true;
            }
            if (headerName == "Image")
            {
                e.Cancel = true;
            }
            if (headerName == "ImageMimeType")
            {
                e.Cancel = true;
            }
            if (headerName == "PasswordHash")
            {
                e.Cancel = true;
            }
            if (headerName == "ReactivationDate")
            {
                e.Cancel = true;
            }
            if (headerName == "Suspended")
            {
                e.Cancel = true;
            }
            if (headerName == "ReadOnly")
            {
                e.Cancel = true;
            }
            if (headerName == "Active")
            {
                e.Cancel = true;
            }
            if (headerName == "RestrictionDetails")
            {
                e.Cancel = true;
            }
            if (headerName == "Biography")
            { 
                e.Cancel = true; 
            }

            // Rename column headers
            if (headerName == "GivenName")
            {
                e.Column.Header = "First Name";
            }
            if (headerName == "FamilyName")
            {
                e.Column.Header = "Last Name";
            }
            if (headerName == "PhoneNumber")
            {
                e.Column.Header = "Phone Number";
            }
        }

        private void btnUnassign_Click(object sender, RoutedEventArgs e)
        {
            // Changes 'Approved' from True to False
            var selectedUser = grdVolunteers.SelectedItem as User;
            if (selectedUser != null)
            {
                // Ask if they are sure they want to unassign
                if (MessageBox.Show("Are you sure you want to unassign " + selectedUser.GivenName + " " + selectedUser.FamilyName + "?", 
                                "Unassign " + selectedUser.GivenName + " " + selectedUser.FamilyName, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Need to get project ID from selected project
                    // Hard coding 100002 for now

                    // Changed from hardcoded
                    _userManager.UnassignVolunteerByProject(selectedUser.UserID, _projectID);
                    // Refresh the data grid after unassignment
                    RefreshVolunteerList();
                }
            } else
            {
                MessageBox.Show("Please select a volunteer to unassign", "Invalid Volunteer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshVolunteerList()
        {
            try
            {
                List<User> volunteerList = _userManager.GetUsersByProjectID(100002);
                grdVolunteers.ItemsSource = volunteerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing volunteers: " + ex.Message);
            }
        }
    }
}

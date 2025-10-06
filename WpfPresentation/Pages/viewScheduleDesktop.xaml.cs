/// <summary>
/// Creator:    Chase Hannen
/// Created:    2025/03/30
/// Summary:    The C# code for the View Schedule page
/// Last Updated By: Chase Hannen
/// Last Updated: 2025/03/30
/// What was Changed: Initial Creation
/// </summary>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for viewScheduleDesktop.xaml
    /// </summary>
    public partial class viewScheduleDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        AvailabilityManager _availabilityManager = new AvailabilityManager();
        ProjectManager _projectManager = new ProjectManager();
        UserManager _userManager = new UserManager();
        Project _project = null;

        public viewScheduleDesktop(int projectID)
        {
            _project = _projectManager.GetProjectByID(projectID);
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Gets approved volunteers on project
                List<AvailabilityVM> availabilities = _availabilityManager.SelectAvailabilityVMByProjectID(_project.ProjectID);
                grdViewSchedule.ItemsSource = availabilities;
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message);
            }
        }

        private void grdViewSchedule_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            // Cancel columns to omit
            if (headerName == "UserID")
            {
                e.Cancel = true;
            }
            if (headerName == "AvailabilityID")
            {
                e.Cancel = true;
            }
            if (headerName == "IsAvailable")
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
            if (headerName == "StartDate")
            {
                e.Column.Header = "Start";
            }
            if (headerName == "EndDate")
            {
                e.Column.Header = "End";
            }
            if (headerName == "RepeatWeekly")
            {
                e.Column.Header = "Repeat Weekly?";
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

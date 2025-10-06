/// <summary>
/// Creator:    Chase Hannen
/// Created:    2025/03/18
/// Summary:    The C# code for the View Assigned Projects page
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/07
/// What was Changed: Connections and UI
/// </summary>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using LogicLayer;
using DataDomain;

namespace WpfPresentation.Pages
{
    public partial class viewAssignedProjects : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        ProjectManager _projectManager = new ProjectManager();
        int userID;
        string name;
        string email;

        public viewAssignedProjects(int userID, string name, string email)
        {
            this.userID = userID;
            this.name = name;
            this.email = email;
            InitializeComponent();
        }

        /// <summary>
        /// Creator: Chase Hannen
        /// Created: 2025/04/04
        /// Summary: Initializes the ViewAssignedProjects page and sets up the ProjectManager instance. 
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/07
        /// What Was Changed: Removed hardcoding and updated message box
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbkViewAssignedProjects.Content = name + "'s Projects"; // doesn't get projects where the user is project starter

            List<Project> projects;
            try
            {
                projects = _projectManager.GetAllProjectsByUserID(userID);
                grdProjects.ItemsSource = projects;
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ?
                    ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Creator: Chase Hannen
        /// Created: 2025/04/04
        /// Summary: Column headers that need not be viewed by the user are cancelled from view
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void grdProjects_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            // Cancel columns to omit
            if (headerName == "ProjectID")
            {
                e.Cancel = true;
            }
            if (headerName == "ProjectTypeID")
            {
                e.Cancel = true;
            }
            if (headerName == "LocationID")
            {
                e.Cancel = true;
            }
            if (headerName == "UserID")
            {
                e.Cancel = true;
            }
            if (headerName == "StartDate")
            {
                e.Cancel = true;
            }
            if (headerName == "AcceptsDonations")
            {
                e.Cancel = true;
            }
            if (headerName == "PayPalAccount")
            {
                e.Cancel = true;
            }
            if (headerName == "AvailableFunds")
            {
                e.Cancel = true;
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnViewProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View_Profile(email));
        }

        private void btnViewProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Project project = (Project)grdProjects.SelectedItem;
                if(project != null)
                {
                    NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(project.ProjectID));
                }
                else
                {
                    MessageBox.Show("Please select a project.", "Invalid Project", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}

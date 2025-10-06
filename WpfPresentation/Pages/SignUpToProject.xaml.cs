/// <summary>
/// Akoi Kollie
/// Created: 2025/03/19
/// 
/// This a is the class that display for user input when the project has loaded
/// It make connection the user input to the the database.
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// </remarks>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for SignUpToProject.xaml
    /// </summary>
    public partial class SignUpToProject : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private VolunteerStatus volunteerStatus;
        private VolunteerStatusManager _volunteerStatusManager = new VolunteerStatusManager();
        Project _project = new Project();

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: Initializes the SignUpToProject page and sets up the NotificationManager instance. 
        /// This constructor prepares the UI components and ensures user data management
        /// can be accessed during the SignUpToProject request. 
        /// Last Upaded By: Stan Anderson
        /// Last Updated: 2025/04/07
        /// What Was Changed: Added parameter
        /// </summary>
        public SignUpToProject(Project project)
        {
            InitializeComponent();
            _project = project;
        }


        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: This is the button shutdown every activities that the user is doing when on the 
        /// UI page when the page loaded and take the user where they were before the page loaded
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        
        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: This is the button the user click to sign up to project and wait for a notification of 
        /// approved from the project starter and also throw an error message if request was not submit
        /// Last Upaded By: Stan Anderson
        /// Last Updated: 2025/04/07
        /// What Was Changed: Updated message boxes
        /// </summary>

        // code to request use to joined the project by clicking joined project.
        private void btnJoinedProject_Click(object sender, RoutedEventArgs e)
        {
            // this needs to be replaced
            
            try
            {
                if (_volunteerStatusManager.AddVolunteerStatus(main.UserID, _project.ProjectID))
                {
                    MessageBox.Show("We received your submission to join our project watch your notification for approval information from our volunteer director alone with your next step to get involved", "Application Recieved", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(_project.ProjectID));

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Request Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }  
    }
}

/// <summary>
/// Christivie Mauwa
/// Created: 2025/02/28
/// 
/// Interaction logic for ViewHomepage.xaml
/// </summary>
///
/// <remarks>
/// Updater: Stan Anderson
/// Updated: 2025/04/07
/// </remarks>

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    public partial class ViewHomepage : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        public ViewHomepage()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            // check if user is logged in
            // if yes, go to create project page
            // if no, go to the login page

            NavigationService.GetNavigationService(this)?.Navigate(new createProject());

        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.GetNavigationService(this)?.Navigate(new ListOfAllProjects());

        }

        private void btnDonate_Click(object sender, RoutedEventArgs e)
        {

            // go to a donation page

            NavigationService.GetNavigationService(this)?.Navigate(new ListOfAllProjects());

        }

        private void btnViewEvent_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.GetNavigationService(this)?.Navigate(new PgEventList());

        }

        private void ViewHomepage_Loaded(object sender, RoutedEventArgs e)
        {
            txtMission.Text = "At CommUnity, our mission is to ignite the spirit " +
                "of community collaboration through an open-source " +
                "platform that empowers individuals to initiate, " +
                "volunteer for, and complete impactful projects " +
                "within their local neighborhoods. We believe that meaningful " +
                "change starts at the grassroots level, and by connecting " +
                "passionate community members with opportunities to contribute, " +
                "we can collectively foster a stronger, more vibrant society. " +
                "Whether it's organizing neighborhood cleanups, supporting " +
                "local charities, or building shared community spaces, " +
                "CommUnity is dedicated to providing the tools and connections " +
                "needed to turn ideas into action. Join us in making a difference, " +
                "one project at a time.";


            // make sure the image displays, because right now it does not display

        }
    }
}

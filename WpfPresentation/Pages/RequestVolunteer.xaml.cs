/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This a is the class that display for user input when the project has loaded
/// It make connection the user input to the the database.
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// 
/// Updater  Syler Bushlack
/// Updated: 2025/05/1
/// What was changed: updated the RequestVolunteer constructor to have an event object parameter that is set to _event
///     and removed the hardcoded valuse originally stored in _event
/// </remarks>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataDomain;
using LogicLayer;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for RequestVolunteer.xaml
    /// </summary>
    public partial class RequestVolunteer : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private Notification Notification;
        private NotificationManager _notificationManager = new NotificationManager();
        Notification _notification = new Notification();
        Event _event = null;
        VolunteerStatusManager _volunteerStatusManager = new VolunteerStatusManager();

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: Initializes the RequestVolunteer page and sets up the VolunteerStatusManager instance. 
        /// This constructor prepares the UI components and ensures user data management
        /// can be accessed during the requestvolunteer. 
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>

        public RequestVolunteer(Event @event)
        {
            InitializeComponent();
            _event = @event;
        }

      


        // the sumit button code start here
        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: This is the button that save the user input of volunteerstatus
        /// This UI will check if the user input of volunteerstatus and
        /// validate if user input is null or white space.
        //It throw an error message if all fields are not fill up before user submit and 
        /// looping through each user and send a volunteer request to each user 
        /// Last Upaded By: Syler Bushlack
        /// Last Updated: 2025/05/01
        /// What Was Changed: remived the hardcoded values for event
        /// </summary>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            DateTime timeframe = txtTimeFrame.DisplayDate;
            string name = txtName.Text;
            string content = txtContent.Text;
            txtTimeFrame.DisplayDateStart = DateTime.Today;
            if (timeframe.Equals(null) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("All fields are required");
                return;
            }
            if (timeframe == DateTime.MinValue)
            {
                MessageBox.Show("select a time");
                return;
            }
            if (content.Length < 10 || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Give good description content, at least 10 character or more.");
                return;
            }

               _notification.Name = name;
                List<VolunteerStatus> volunteers = _volunteerStatusManager.SelectVolunteerStatusByProjectID(_event.ProjectID);

            try
            {  
                foreach (VolunteerStatus volunteerStatus in volunteers)
                {
                    Notification notification = new Notification();
                    notification.Name = name;
                    notification.Content = content;
                    notification.Date = timeframe;
                    notification.Sender = main.UserID;
                    notification.Receiver = volunteerStatus.UserID;
                    notification.Important = (bool)IsChecked.IsChecked;
                    notification.IsViewed = false;
                    _notificationManager.InsertNotification(notification);
                }
                MessageBox.Show("Request was sent");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Request was not send");
            }

            NavigationService.GoBack();
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
    }
}




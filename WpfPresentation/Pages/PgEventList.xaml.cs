/// <summary>
/// Yousif Omer
/// Created: 2025/02/01
/// Summary: This is an Interaction logic for PgEventList.xaml
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/08
/// Changes: Updated UI and MessageBoxes; permissions
/// </summary>

using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using EventManager = LogicLayer.EventManager;
using DataDomain;
using System.Linq;

namespace WpfPresentation.Pages
{
    
    public partial class PgEventList : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private IEventManager _eventManager;
        private int _projectID = 0;
        List<Event> events;

        public PgEventList()
        {
            _projectID = 0;
            InitializeComponent();

            btnEditEvent.Visibility = Visibility.Hidden;
            btnDeleteEvent.Visibility = Visibility.Hidden;
            btnCreateEvent.Visibility = Visibility.Hidden;

            _eventManager = new EventManager();
            GetPrivileges();
        }

        public PgEventList(int projectID)
        {
            _projectID = projectID;
            InitializeComponent();

            validatePermission();

            _eventManager = new EventManager();
            validatePermission();
            GetPrivileges();
        }

        private void GetPrivileges()
        {
            btnViewEventsTasks.Visibility = Visibility.Collapsed;
            btnCreateEvent.Visibility = Visibility.Collapsed;
            btnEditEvent.Visibility = Visibility.Collapsed;
            if (main.ProjectRoleValues.Contains("Project Starter") ||
                main.ProjectRoleValues.Contains("Event Coordinator") ||
                main.SystemRoles.Contains("Admin"))
            {
                btnCreateEvent.Visibility = Visibility.Visible;
                btnEditEvent.Visibility = Visibility.Visible;
            }
            if (main.IsVolunteer(_projectID)){
                btnViewEventsTasks.Visibility = Visibility.Visible;
            }
        }

        private void validatePermission()
        {

            btnEditEvent.Visibility = Visibility.Hidden;
            btnDeleteEvent.Visibility = Visibility.Hidden;
            btnCreateEvent.Visibility = Visibility.Hidden;

            if (main.isLoggedIn)
            {
                if (main.IsProjectStarter(_projectID))
                {

                    btnEditEvent.Visibility = Visibility.Visible;
                    btnDeleteEvent.Visibility = Visibility.Visible;
                    btnCreateEvent.Visibility = Visibility.Visible;
                }
                else if (main.IsVolunteer(_projectID))
                {

                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            refreshEventsList();
        }

        private void refreshEventsList()
        {
            
            if (_projectID != 0)
            {
                events = _eventManager.ViewEventListByProjectID(_projectID);
            }
            else
            { 
                events = _eventManager.ViewEventList(true);
            }

            lblEvents.Content = "Events (" + events.Count + ")";

            dgEventList.ItemsSource = events;

            dgEventList.Columns[3].Header = "Date Created";
            dgEventList.Columns[4].Header = "Start Date";
            dgEventList.Columns[5].Header = "End Date";
            dgEventList.Columns[6].Header = "Name";
            dgEventList.Columns[8].Header = "Volunteers Needed";
            dgEventList.Columns[10].Header = "Notes";
            dgEventList.Columns[11].Header = "Description";
            dgEventList.Columns[12].Header = "Active";

            dgEventList.Columns[0].Visibility = Visibility.Hidden;
            dgEventList.Columns[1].Visibility = Visibility.Hidden;
            dgEventList.Columns[2].Visibility = Visibility.Hidden;
            dgEventList.Columns[7].Visibility = Visibility.Hidden;
            dgEventList.Columns[9].Visibility = Visibility.Hidden;
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            Event _event = new Event();
            _event = (Event)dgEventList.SelectedItem;

            var selectedItem = (Event)dgEventList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Please select an event to edit!", "Invalid Event", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                NavigationService.GetNavigationService(this)?.Navigate(new UpdateEvent(selectedItem,_eventManager));
            }
        }

        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            Event _event = new Event();
            _event = (Event)dgEventList.SelectedItem;

            var selectedItem = (Event)dgEventList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Please select an event to deactivate!", "Invalid Event", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    // check for authority

                    MessageBoxResult result = MessageBox.Show("Are you sure you want to deactivate this event?", "Deactivate Event", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        _eventManager.DeactivateEventById(_event.EventID);
                        refreshEventsList();
                        MessageBox.Show("Event deactivated successfully!", "Deactivation Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deactivating event!", "Deactivation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnViewAnEvent_Click(object sender, RoutedEventArgs e)
        {
            Event _event = new Event();
            _event = (Event)dgEventList.SelectedItem;
            try
            {
              
                var selectedItem = (Event)dgEventList.SelectedItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Please select an event to view its details!", "Invalid Event", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else 
                {
               
                    Event result = _eventManager.SelectEventByID(selectedItem.EventID);

                    NavigationService.GetNavigationService(this)?.Navigate(new PgViewAnEvent(result, _eventManager));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading event.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnViewEventsTasks_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Event)dgEventList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Please select an event to view its details!", "Invalid Event", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            { 
                NavigationService.GetNavigationService(this)?.Navigate(new ViewTasksByEventID(selectedItem.EventID, selectedItem.Name));
            }
        }

        private void btnCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new CreateEventDesktop(_projectID));

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

/// <summary>
/// Creator: Yousif Omer
/// Created: 2025/02/24
/// Summary:
///     This class is created to greate pg for view un event
/// </summary>
///
/// <remarks>
/// Updated By: Stan Anderson
/// Updated: 2025/04/06
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-25
/// What Was Changed:
///     Removed unnecessary PgViewAnEvent constructors; only one is needed & it's okay that it
///     takes in an IEventManager as an argument/parameter.
/// </remarks>

using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    
    public partial class PgViewAnEvent : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private Event _event = null;
        private IEventManager _eventManger = null;

        public PgViewAnEvent(Event @event, IEventManager @eventManager)
        {
            InitializeComponent();

            // markReadOnly();

            _event = @event;
            _eventManger = @eventManager;

            lblEvent.Content = _event.Name;

            txtEventTypeID.Text = _event.EventTypeID.ToString();
            txtDescription.Text = _event.Description.ToString();
            txtNotes.Text = _event.Notes.ToString();
            txtVolunteersNeeded.Text = _event.VolunteersNeeded.ToString();
            dpDateCreated.Text = _event.DateCreated.ToString();
            dpStartDate.Text = _event.StartDate.ToString();
            dpEndDate.Text = _event.EndDate.ToString();

            validatePermission();
        }

        private void validatePermission()
        {
            int projectID = _event.ProjectID;
            btnViewTasks.Visibility = Visibility.Hidden;
            btnDeleteEvent.Visibility = Visibility.Hidden;
            btnEditEvent.Visibility = Visibility.Hidden;
            btnViewVolunteers.Visibility = Visibility.Hidden;
            if (main.isLoggedIn)
            {
                if (main.IsVolunteer(projectID)){
                    btnViewTasks.Visibility = Visibility.Visible;
                    btnViewVolunteers.Visibility = Visibility.Visible;
                }
                if (main.HasProjectRole("Project Starter", _event.ProjectID) ||
                   main.HasProjectRole("Event Coordinator", _event.ProjectID) ||
                   main.HasProjectRole("Volunteer Coordinator", _event.ProjectID) ||
                   main.HasSystemRole("Admin"))
                {
                    btnDeleteEvent.Visibility = Visibility.Visible;
                    btnEditEvent.Visibility = Visibility.Visible;
                }
            }
        }



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void markReadOnly()
        {
            txtEventTypeID.IsReadOnly = true;
            txtDescription.IsReadOnly = true;
            txtNotes.IsReadOnly = true;
            txtVolunteersNeeded.IsReadOnly = true;
            dpDateCreated.IsEnabled = false;
            dpStartDate.IsEnabled = false;
            dpEndDate.IsEnabled = false;
        }

        private void btnViewProject_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(_event.ProjectID));
        }

        private void btnViewVolunteers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new ViewEventVolunteers(_event));

            //NavigationService.GetNavigationService(this)?.Navigate(new ViewEventVolunteers(_event.EventID, _event.Name));
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this)?.Navigate(new UpdateEvent(_event, _eventManger));
        }

        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MessageBoxResult result = MessageBox.Show("Are you sure you want to deactivate this event?", "Deactivate Event", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _eventManger.DeactivateEventById(_event.EventID);
                    MessageBox.Show("Event deactivated successfully!", "Deactivation Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    NavigationService.GetNavigationService(this)?.Navigate(new PgEventList());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error deactivating event!", "Deactivation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        

        private void btnViewTasks_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewTasksByEventID(_event.EventID, _event.Name));
        }
    }
}

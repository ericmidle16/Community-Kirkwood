/// <summary>
/// Yousif Omer
/// Created: 2025/02/01
/// 
/// Actual summary for update event example page
/// </summary>
///
/// <remarks>
/// Updater Name: Stan Anderson
/// Updated: 2025/04/07
/// Changes: Updated UI
/// 
/// Updater Name: Syler Bushlack
/// Updated: 2025/05/01
/// Changes: Fixed btnUpdateEvent_Click method so the try catch dosn't throw and exception to the user crashing the program and instead
///     flashes a message box. Also added "newEvent.EventTypeID = _event.EventTypeID;" to fix updates not working. Changed PgEventList
///     to include _event.ProjectID as a peramiter
/// </remarks>
/// <summary>
/// Interaction logic for EditEvent.xaml
/// </summary>

using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataDomain;
namespace WpfPresentation.Pages
{
    
    public partial class UpdateEvent : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private Event _event = null;
        private IEventManager _eventManger = null;


        public UpdateEvent(Event @event, IEventManager @eventManager)
        {
            InitializeComponent();
            _event = @event;
            _eventManger = @eventManager;
           
        }


        private void btnUpdateEvent_Click(object sender, RoutedEventArgs e)
        {

            Event newEvent = new Event();
            newEvent.EventID = _event.EventID;
            newEvent.DateCreated = DateTime.Parse(dpDateCreated.Text);
            newEvent.StartDate = DateTime.Parse(dpStartDate.Text);
            newEvent.EndDate = DateTime.Parse(dpEndDate.Text);
            newEvent.Name = txtName.Text;
            newEvent.VolunteersNeeded = int.Parse(txtVolunteersNeeded.Text.ToString());
            newEvent.Notes = txtNotes.Text;
            newEvent.Description = txtDescription.Text;
            newEvent.Active = true;
            newEvent.EventTypeID = _event.EventTypeID;
  


            newEvent.ProjectID = _event.ProjectID;
            try
            {
                _eventManger.EditEvent(_event,newEvent);
                NavigationService.GetNavigationService(this)?.Navigate(new PgEventList(_event.ProjectID));
            }
            catch (Exception)
            {

                MessageBox.Show("Failed to update event", "Failed Update", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            txtEventID.Text = _event.EventID.ToString();
            txtEventTypeID.Text = _event.EventTypeID;
            txtProjectID.Text = _event.ProjectID.ToString();
            dpDateCreated.Text = _event.DateCreated.ToString();
            dpStartDate.Text = _event.StartDate.ToString();
            dpEndDate.Text = _event.EndDate.ToString();
            txtName.Text = _event.Name.ToString();
            txtLocationID.Text = _event.LocationID.ToString();
            txtVolunteersNeeded.Text = _event.VolunteersNeeded.ToString();
            txtUserID.Text = _event.UserID.ToString();
            txtNotes.Text = _event.Notes.ToString();
            txtDescription.Text = _event.Description.ToString();
            txtActive.Text = _event.Active.ToString();


            txtEventID.IsReadOnly = true;
            txtEventTypeID.IsReadOnly=true;
            txtProjectID.IsReadOnly=true;
            txtLocationID.IsReadOnly=true;
            txtUserID.IsReadOnly=true;
            txtActive.IsReadOnly=true;

            dpDateCreated.IsEnabled = false;
        }

        private void Cams_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.GetNavigationService(this)?.Navigate(new PgEventList());
        }
    }
}

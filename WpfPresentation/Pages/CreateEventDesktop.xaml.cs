using DataDomain;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using LogicLayer;

namespace WpfPresentation.Pages
{
    public partial class CreateEventDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow();

        public byte[] imageData = null;
        LogicLayer.EventManager _eventManager = new LogicLayer.EventManager();
        LocationManager _locationManager = new LocationManager();
        ProjectManager _projectManager = new ProjectManager();
        int _projectID;

        public CreateEventDesktop(int projectID)
        {
            _projectID = projectID;
            InitializeComponent();
            InitializeTimeSelectors();
        }

       


        private void InitializeTimeSelectors()
        {
            // Populate hours (1-12)
            for (int i = 1; i <= 12; i++)
            {
                string hourText = i.ToString();
                cboStartHour.Items.Add(hourText);
                cboEndHour.Items.Add(hourText);
            }

            // Add AM/PM
            cboStartAMPM.Items.Add("AM");
            cboStartAMPM.Items.Add("PM");
            cboEndAMPM.Items.Add("AM");
            cboEndAMPM.Items.Add("PM");

            // Set default values
            cboStartHour.SelectedIndex = 8; // 9 AM
            cboStartAMPM.SelectedIndex = 0; // AM

            cboEndHour.SelectedIndex = 4; // 5 PM
            cboEndAMPM.SelectedIndex = 1; // PM
        }

        private DateTime CombineDateAndTime(DatePicker datePicker, ComboBox hourBox, ComboBox ampmBox)
        {
            DateTime date;
            if (!DateTime.TryParse(datePicker.Text, out date))
            {
                throw new FormatException("Invalid date format");
            }

            // Default to midnight if any time component is not selected
            if (hourBox.SelectedItem == null || ampmBox.SelectedItem == null)
            {
                return date;
            }

            int hour = int.Parse(hourBox.SelectedItem.ToString());
            int minute = 0; // Minutes always set to 0
            bool isPM = ampmBox.SelectedItem.ToString() == "PM";

            // Convert to 24-hour format
            if (isPM && hour < 12)
                hour += 12;
            else if (!isPM && hour == 12)
                hour = 0;

            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var locations = _locationManager.ViewLocationList();
                locations = locations.OrderBy(loc => loc.Name).ToList();
                foreach (var location in locations)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = location.Name;
                    item.Tag = location.LocationID;
                    cboBoxLocation.Items.Add(item);
                }

                var eventTypes = _eventManager.SelectEventTypes();
                eventTypes = eventTypes.OrderBy(et => et.EventTypeID).ToList();
                foreach (var eventType in eventTypes)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = eventType.EventTypeID;
                    item.Tag = eventType.EventTypeID;
                    cboBoxEventType.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load combo box data: " + ex.Message);
            }
        }

        //private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openDialog = new OpenFileDialog();
        //    openDialog.Filter = "ImageFiles|*.jpg;*.png;*.bmp;*.tiff;";

        //    if (openDialog.ShowDialog().Equals(true))
        //    {
        //        string filePath = openDialog.FileName;
        //        imgPreview.Source = new BitmapImage(new Uri(filePath));

        //        imageData = File.ReadAllBytes(filePath);
        //    }
        //}

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtEventName.Text.Length <= 0 || txtEventName.Text.Length > 50)
            {
                MessageBox.Show("Invalid Event Name (Empty or over 50 characters)");
                return;
            }
            if (cboBoxEventType.SelectedItem == null)
            {
                MessageBox.Show("You must select an Event Type");
                return;
            }
            if (cboBoxLocation.SelectedItem == null)
            {
                MessageBox.Show("You must select a Location");
                return;
            }

            int volunteersNeeded;

            if (!int.TryParse(txtVolunteersNeeded.Text, out volunteersNeeded))
            {
                MessageBox.Show("Volunteers Needed must be a valid number");
                return;
            }
            if (volunteersNeeded < 0)
            {
                MessageBox.Show("Volunteers Needed cannot be negative");
                return;
            }

            if (cboStartHour.SelectedItem == null || cboStartAMPM.SelectedItem == null)
            {
                MessageBox.Show("Please select a start time");
                return;
            }
            if (cboEndHour.SelectedItem == null || cboEndAMPM.SelectedItem == null)
            {
                MessageBox.Show("Please select an end time");
                return;
            }

            DateTime startDateTime;
            DateTime endDateTime;

            try
            {
                startDateTime = CombineDateAndTime(dpEventStart, cboStartHour, cboStartAMPM);
                endDateTime = CombineDateAndTime(dpEventEnd, cboEndHour, cboEndAMPM);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (endDateTime < startDateTime)
            {
                MessageBox.Show("Event End date/time cannot be before Start date/time");
                return;
            }

            ComboBoxItem selectedLocation = (ComboBoxItem)cboBoxLocation.SelectedItem;
            int locationId = (int)selectedLocation.Tag;

            ComboBoxItem selectedEventType = (ComboBoxItem)cboBoxEventType.SelectedItem;
            string eventType = (string)selectedEventType.Tag;

            Event newEvent = new Event
            {
                EventTypeID = eventType,
                ProjectID = _projectID,
                StartDate = startDateTime,
                EndDate = endDateTime,
                Name = txtEventName.Text,
                LocationID = locationId,
                VolunteersNeeded = Int32.Parse(txtVolunteersNeeded.Text),
                UserID = main.UserID,
                Notes = txtNotes.Text,
                Description = txtDescription.Text
            };

            try
            {
                bool result = _eventManager.InsertEvent(newEvent);
                if (result)
                {
                    MessageBox.Show("Event created successfully!");
                    NavigationService.GetNavigationService(this)?.Navigate(new PgEventList());
                }
                else
                {
                    MessageBox.Show("Event failed to create");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Event failed to create." + ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel? Any unsaved data will be lost.",
                                                      "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService?.Navigate(new PgEventList());
            }
        }
    }
}
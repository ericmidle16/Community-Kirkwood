using LogicLayer;
using System.Windows;
using System.Windows.Controls;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for UpdateAvailabilityDesktop.xaml
    /// </summary>
    public partial class UpdateAvailabilityDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference
        private AvailabilityManager _availabilityManager;
        private int _availabilityID;
        private DateTime _startDate;
        private DateTime _endDate;
        private bool _isAllDay;
        private bool _repeatWeekly;

        public UpdateAvailabilityDesktop(int availabilityID, DateTime startDate, DateTime endDate)
        {
            _availabilityID = availabilityID;
            _startDate = startDate;
            _endDate = endDate;
            InitializeComponent();
            _availabilityManager = new AvailabilityManager();

            LoadTimeOptions();
            LoadExistingAvailabilityData();
        }


        private void LoadTimeOptions()
        {
            // Populate time dropdown options
            for (int hour = 1; hour <= 12; hour++)
            {
                for (int minute = 0; minute < 60; minute += 30)
                {
                    cbStartTime.Items.Add($"{hour}:{minute:D2} AM");
                    cbEndTime.Items.Add($"{hour}:{minute:D2} AM");
                }
            }

            for (int hour = 1; hour <= 12; hour++)
            {
                for (int minute = 0; minute < 60; minute += 30)
                {
                    cbStartTime.Items.Add($"{hour}:{minute:D2} PM");
                    cbEndTime.Items.Add($"{hour}:{minute:D2} PM");
                }
            }
        }

        private void LoadExistingAvailabilityData()
        {
            try
            {
                dpStartDate.SelectedDate = _startDate;
                dpEndDate.SelectedDate = _endDate;
                _isAllDay = _startDate.TimeOfDay.TotalMinutes == 0 && _endDate.TimeOfDay.TotalMinutes == 0;
                rbAllDayYes.IsChecked = _isAllDay;
                rbAllDayNo.IsChecked = !_isAllDay;
                _repeatWeekly = false;
                rbRepeatWeeklyYes.IsChecked = _repeatWeekly;
                rbRepeatWeeklyNo.IsChecked = !_repeatWeekly;

                // Set time values if not all day
                if (!_isAllDay)
                {
                    string startTimeStr = _startDate.ToString("h:mm tt");
                    string endTimeStr = _endDate.ToString("h:mm tt");

                    cbStartTime.Text = startTimeStr;
                    cbEndTime.Text = endTimeStr;

                    cbStartTime.IsEnabled = true;
                    cbEndTime.IsEnabled = true;
                }
                else
                {
                    cbStartTime.IsEnabled = false;
                    cbEndTime.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading availability data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: This checks if the radio button for are "Do you have availability all day
        /// is checked yes or no. In this case if it is checked no it will enable the combo boxes
        /// for the start and end times for the user. Otherwise if the radio button yes is selected
        /// the start and end time combo boxes are not enabled. 
        /// </summary>
        private void AllDay_Checked(object sender, RoutedEventArgs e)
        {
            bool isAllDay = rbAllDayYes.IsChecked == true;
            cbStartTime.IsEnabled = !isAllDay;
            cbEndTime.IsEnabled = !isAllDay;
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: This method converts a 12-hour time string (e.g., "1:30 PM") to a 24-hour format (`TimeSpan`). 
        /// </summary>
        private TimeSpan ConvertTo24HourFormat(string time12h)
        {
            DateTime parsedTime;
            if (!DateTime.TryParseExact(time12h, new[] { "h:mm tt", "hh:mm tt" },
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out parsedTime))
            {
                MessageBox.Show($"Invalid time format: {time12h}", "Time Format Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return TimeSpan.Zero;
            }
            return parsedTime.TimeOfDay;
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: This method validates that both start and end times are selected, and ensures
        /// that the start time is earlier than the end time.
        /// </summary>
        private bool ValidateTimeSelection(DateTime startDate, DateTime endDate, string startTimeStr, string endTimeStr)
        {
            if (string.IsNullOrWhiteSpace(startTimeStr) || string.IsNullOrWhiteSpace(endTimeStr))
            {
                MessageBox.Show("Please select both start and end times.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            TimeSpan selectedStartTime = ConvertTo24HourFormat(startTimeStr);
            TimeSpan selectedEndTime = ConvertTo24HourFormat(endTimeStr);

            if (selectedStartTime == TimeSpan.Zero || selectedEndTime == TimeSpan.Zero)
            {
                return false;
            }

            if (selectedStartTime >= selectedEndTime)
            {
                MessageBox.Show("Start time must be before end time.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


        private void SaveAvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(rbAllDayYes.IsChecked == true || rbAllDayNo.IsChecked == true))
                {
                    MessageBox.Show("Please select whether you are available all day.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!(rbRepeatWeeklyYes.IsChecked == true || rbRepeatWeeklyNo.IsChecked == true))
                {
                    MessageBox.Show("Please select whether you want to repeat availability weekly.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool isAllDay = rbAllDayYes.IsChecked == true;

                if (!dpStartDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a start date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (dpStartDate.SelectedDate.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Start date cannot be in the past.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!dpEndDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select an end date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (dpEndDate.SelectedDate.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("End date cannot be in the past.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (dpEndDate.SelectedDate.Value.Date < dpStartDate.SelectedDate.Value.Date)
                {
                    MessageBox.Show("End date cannot be before start date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DateTime startDateTime = dpStartDate.SelectedDate.Value.Date;
                DateTime endDateTime = dpEndDate.SelectedDate.Value.Date;
                bool repeatWeekly = rbRepeatWeeklyYes.IsChecked == true;

                // Handle time if not all day
                if (!isAllDay)
                {
                    if (string.IsNullOrWhiteSpace(cbStartTime.Text) || string.IsNullOrWhiteSpace(cbEndTime.Text))
                    {
                        MessageBox.Show("Please select both start and end times.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    bool isValidTime = ValidateTimeSelection(startDateTime, endDateTime, cbStartTime.Text, cbEndTime.Text);
                    if (!isValidTime)
                    {
                        return;
                    }

                    TimeSpan startTime = ConvertTo24HourFormat(cbStartTime.Text);
                    TimeSpan endTime = ConvertTo24HourFormat(cbEndTime.Text);

                    startDateTime = startDateTime.Add(startTime);
                    endDateTime = endDateTime.Add(endTime);
                }

                // Update the specific availability entry using the method from your manager
                bool updateSuccess = _availabilityManager.UpdateAvailabilityByID(
                    _availabilityID,
                    main.UserID,
                    isAllDay,  // Using isAllDay for the isAvailable parameter
                    repeatWeekly,
                    startDateTime,
                    endDateTime);

                if (updateSuccess)
                {
                    MessageBox.Show("Availability updated successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.Navigate(new ViewAvailabilityDeskTop());
                }
                else
                {
                    MessageBox.Show("Failed to update availability.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Handles the Cancel button click event.
        /// </summary>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel? Any unsaved changes will be lost.",
                                                      "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService?.Navigate(new ViewAvailabilityDeskTop());
            }
        }
    }
}
/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/10
/// Summary: This class handles the avaiability interface for adding a new availability.
/// It interacts with the AvaiabilityManger to validate user input and insert
/// a new availability.
/// Last Upaded By: Stan Anderson
/// Last Updated: 2025/04/08
/// What Was Changed: Added connections
/// </summary>
using LogicLayer;
using System.Windows;
using System.Windows.Controls;

namespace WpfPresentation.Pages
{
    public partial class AddAvailabilityDeskTop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private AvailabilityManager _availabilityManager;

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Initializes the AddAvailability page and sets up the AvailabilityManager instance. 
        /// This constructor prepares the UI components and ensures user data management
        /// can be accessed during account creation. 
        /// 
        /// It also contains code which populates the start and end time combo boxes with AM times 
        /// which appear first and then PM times which appear after all the AMs. The time starts at
        /// 1 am and continues until 12pm in 30 minute opitions. So, the user can chose to have a start 
        /// time of 7 am and end time of 7:30 am if they wish.
        /// 
        /// 
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public AddAvailabilityDeskTop(int userID) //int userID
        {
            InitializeComponent();
            _availabilityManager = new AvailabilityManager();


            // Auto-fill User ID field
            txtUserID.Text = main.UserID.ToString();
            txtUserID.IsEnabled = false; // Make it read-only to prevent editing
           


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

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: This checks if the radio button for are "Do you have availability all day
        /// is checked yes or no. In this case if it is checked no it will enable the combo boxes
        /// for the start and end times for the user. Otherwise if the radio button yes is selected
        /// the start and end time combo boxes are not enabled. 
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
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
        /// If the input is invalid, a warning is displayed and `TimeSpan.Zero` is returned. 
        /// Uses `DateTime.TryParseExact` to handle both single- and double-digit hours, 
        /// supporting AM/PM time format. This method ensures the time
        /// is correctly converted.
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
        /// If any time field is empty or invalid, or if the start time is not before the end time, a warning message is shown,
        /// and the method returns false. It uses the ConvertTo24HourFormat method to convert the string inputs into TimeSpan
        /// for comparison in 24-hour format. Returns true if the times are valid and in the correct order. This is needed
        /// to ensure that users provide valid time inputs, preventing invalid time ranges.
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
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


        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: This method handles the click event for saving availability. It validates that all necessary fields are filled out, including
        /// user ID, availability dates, and time selections. It ensures that the start date is not in the past and checks for duplicate 
        /// availability entries. If all validations pass, it inserts the availability into the system. If the user opts to repeat weekly,
        /// the availability is inserted for the next three weeks equaling a month of availability data. Success and error messages are displayed based on the result of the 
        /// insertion attempt. If an exception occurs during the process, an error message is shown.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void SaveAvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUserID.Text))
                {
                    MessageBox.Show("Please enter a user ID.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

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

                if (!isAllDay)
                {
                    if (string.IsNullOrWhiteSpace(cbStartTime.Text) || string.IsNullOrWhiteSpace(cbEndTime.Text))
                    {
                        MessageBox.Show("Please select both start and end times.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    bool isValidTime = ValidateTimeSelection(dpStartDate.SelectedDate.Value, dpEndDate.SelectedDate.Value, cbStartTime.Text, cbEndTime.Text);
                    if (!isValidTime)
                    {
                        return;
                    }
                }

                int userID = main.UserID;//Convert.ToInt32(txtUserID.Text);
                bool repeatWeekly = rbRepeatWeeklyYes.IsChecked == true;
                DateTime startDate = dpStartDate.SelectedDate.Value;
                DateTime endDate = dpEndDate.SelectedDate.Value;
                TimeSpan selectedStartTime = new TimeSpan(7, 0, 0);
                TimeSpan selectedEndTime = new TimeSpan(15, 0, 0);

                if (!isAllDay)
                {
                    selectedStartTime = ConvertTo24HourFormat(cbStartTime.Text);
                    selectedEndTime = ConvertTo24HourFormat(cbEndTime.Text);
                }

                bool exists = _availabilityManager.SelectExistingAvailability(userID, startDate, endDate);
                if (exists)
                {
                    MessageBox.Show("You have already inserted your availability for this date. Please choose different date.",
                                    "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                DateTime currentInsertDate = startDate;
                while (currentInsertDate <= endDate)
                {
                    DateTime availabilityStartDate = currentInsertDate.Date.Add(selectedStartTime); //added .add(selectedStartTime and end
                    DateTime availabilityEndDate = currentInsertDate.Date.Add(selectedEndTime);

                    if (_availabilityManager.SelectExistingAvailability(userID, availabilityStartDate, availabilityEndDate))
                    {
                        MessageBox.Show($"You have already inserted your availability for {currentInsertDate.ToShortDateString()}. Please choose different date(s).",
                                        "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    bool isInsertedSuccessfully = _availabilityManager.InsertAvailability(userID, isAllDay, repeatWeekly, availabilityStartDate, availabilityEndDate);

                    if (!isInsertedSuccessfully)
                    {
                        MessageBox.Show("Failed to insert availability on " + currentInsertDate.ToShortDateString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                        
                    }

                    // If repeatWeekly is selected, insert for the next 3 weeks
                    if (repeatWeekly)
                    {
                        for (int i = 1; i <= 3; i++)
                        {
                            DateTime newStartDate = availabilityStartDate.AddDays(7 * i);
                            DateTime newEndDate = availabilityEndDate.AddDays(7 * i);

                            if (_availabilityManager.SelectExistingAvailability(userID, newStartDate, newEndDate))
                            {
                                MessageBox.Show($"You have already inserted your availability for {newStartDate.ToShortDateString()}. Please choose different date(s).",
                                                "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                            _availabilityManager.InsertAvailability(userID, isAllDay, repeatWeekly, newStartDate, newEndDate);
                        }
                    }

                    currentInsertDate = currentInsertDate.AddDays(1); // Move to the next day
                }

                MessageBox.Show("Availability inserted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //var parentWindow = Window.GetWindow(this);
                //parentWindow.Close();
                NavigationService?.Navigate(new ViewAvailabilityDeskTop(userID));
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Handles the Cancel button click event. If the user clicks cancel, a confirmation message
        /// will appear to ask them if they are sure they want to cancel.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel? Any unsaved data will be lost.",
                                                      "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Close the current page
                //var parentWindow = Window.GetWindow(this);
                // parentWindow.Close();

                // return to viewing 
                NavigationService?.Navigate(new ViewAvailabilityDeskTop(main.UserID));
            }
        }
    }
}
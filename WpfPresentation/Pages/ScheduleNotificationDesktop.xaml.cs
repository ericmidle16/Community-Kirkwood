using DataDomain;
using LogicLayer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfPresentation.Pages
{
    
    public partial class ScheduleNotificationCalendarDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        //   Managers to fetch data
        private readonly NotificationManager _notificationManager = new();
        private readonly UserManager _userManager = new();

        //   Backing collections 
        private List<NotificationVM> _allNotifications;   // raw list from DB
        public ObservableCollection<User> Users { get; }   // for recipients list
        public ObservableCollection<CalendarDayViewModel> Calendar { get; }
            = new ObservableCollection<CalendarDayViewModel>();

        //   Current calendar view state 
        private int _year;
        private int _month;

        public ScheduleNotificationCalendarDesktop()
        {
            InitializeComponent();

            // assign DataContext so XAML bindings work
            DataContext = this;

            Users = new ObservableCollection<User>(_userManager.GetAllUsers());

            // start on current month
            var today = DateTime.Today;
            _year = today.Year;
            _month = today.Month;

            // load & draw
            LoadAllNotifications();
            BuildCalendar();
        }

        /// <summary>
        /// Fetch all notifications for this user once,
        /// so we can filter by date during calendar build.
        /// </summary>
        private void LoadAllNotifications()
        {
            _allNotifications = _notificationManager
                .GetAllSenderNotificationsByUserID(main.UserID)
                .ToList();
        }

        /// <summary>
        /// Constructs the 6×7 calendar grid for the current _year/_month,
        /// populates Calendar collection (which the ItemsControl is bound to).
        /// </summary>
        private void BuildCalendar()
        {
            Calendar.Clear();

            // first of month and how many blanks before day 1
            var firstOfMonth = new DateTime(_year, _month, 1);
            int lead = (int)firstOfMonth.DayOfWeek;

            // we always show 6 weeks (42 days)
            for (int slot = 0; slot < 42; slot++)
            {
                var date = firstOfMonth.AddDays(slot - lead);
                bool inMonth = date.Month == _month;

                // find notifications covering this date
                var notes = _allNotifications
                    .Where(n => n.StartDate.Date <= date && date <= n.EndDate.Date)
                    .ToList();

                Calendar.Add(new CalendarDayViewModel
                {
                    Date = date,
                    IsCurrentMonth = inMonth,
                    IsToday = date == DateTime.Today,
                    Notifications = notes
                });
            }

            // update the header
            lblMonthYear.Content = firstOfMonth.ToString("MMMM yyyy");

            // refresh the ItemsControl
            CalendarDays.Items.Refresh();
        }

        // <- Move back one month
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            var dt = new DateTime(_year, _month, 1).AddMonths(-1);
            _year = dt.Year;
            _month = dt.Month;
            BuildCalendar();
        }

        // -> Move forward one month
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            var dt = new DateTime(_year, _month, 1).AddMonths(1);
            _year = dt.Year;
            _month = dt.Month;
            BuildCalendar();
        }

        // Clicking a day shows its notifications in the right-hand list
        private void Day_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement fe &&
                fe.DataContext is CalendarDayViewModel vm)
            {
                tbkDetailTitle.Text = vm.Date.ToString("MMMM d, yyyy");
                lstNotifications.ItemsSource = vm.Notifications;
            }
        }

        // Show the full-screen "New" form
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            CalendarView.Visibility = Visibility.Collapsed;
            NewNotificationPanel.Visibility = Visibility.Visible;
        }

        // Cancel out of "New" form back to calendar
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NewNotificationPanel.Visibility = Visibility.Collapsed;
            CalendarView.Visibility = Visibility.Visible;

            // reset the calendar title & view
            tbkDetailTitle.Text = $"Notifications for {DateTime.Today:MMMM d, yyyy}";
            BuildCalendar();
        }

        // Gather new-notification data and save
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string title = tbxNewName.Text.Trim();
            string content = tbxNewContent.Text.Trim();

            // collect user IDs
            List<int> recips = lstRecipients.SelectedItems
                             .Cast<User>()
                             .Select(u => u.UserID)
                             .ToList();

            // basic validation
            if (string.IsNullOrEmpty(title) || !recips.Any())
            {
                MessageBox.Show("Please enter a title and select at least one recipient.");
                return;
            }



            // schedule it
            _notificationManager.ScheduleNotification(
                new NotificationVM()
                {
                    Name = title,
                    Content = content,
                    StartDate = dpStartDate.SelectedDate ?? DateTime.Today,
                    EndDate = dpEndDate.SelectedDate ?? DateTime.Today,
                    Important = cbxImportant.IsChecked == true,
                    Sender = main.UserID
                },
                recips
            );

            // refresh and return
            LoadAllNotifications();
            BuildCalendar();
            BtnCancel_Click(null, null);
        }

        /// <summary>
        /// Lightweight VM for each calendar cell.
        /// </summary>
        public class CalendarDayViewModel
        {
            public DateTime Date { get; set; }
            public bool IsCurrentMonth { get; set; }
            public bool IsToday { get; set; }
            public List<NotificationVM> Notifications { get; set; } = new();
        }
    }
}

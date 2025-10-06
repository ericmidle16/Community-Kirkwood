using DataDomain;

namespace WebPresentation.Models
{
    // page VM
    public class ScheduleNotificationsViewModel
    {
        // calendar state
        public int Year { get; set; }
        public int Month { get; set; }
        public string DisplayMonthYear { get; set; }
        public List<CalendarDayViewModel> Calendar { get; set; }

        // for “New” form inputs
        public string NewTitle { get; set; }
        public string NewContent { get; set; }
        public DateTime? NewStartDate { get; set; }
        public DateTime? NewEndDate { get; set; }
        public bool NewImportant { get; set; }
        public List<int> NewRecipients { get; set; }

        /// <summary>
        /// Which date is currently “selected” in the calendar
        /// </summary>
        public DateTime SelectedDate { get; set; }

        /// <summary>
        /// All of the notifications covering that selected date
        /// </summary>
        public List<NotificationVM> SelectedNotifications { get; set; }
            = new List<NotificationVM>();


        // pick‐list of users
        public List<User> Users { get; set; }
    }
}

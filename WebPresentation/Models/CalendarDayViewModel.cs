using DataDomain;

namespace WebPresentation.Models
{
    public class CalendarDayViewModel
    {
        public DateTime Date { get; set; }
        public bool IsCurrentMonth { get; set; }
        public bool IsToday { get; set; }
        public List<NotificationVM> Notifications { get; set; } = new();
    }
}

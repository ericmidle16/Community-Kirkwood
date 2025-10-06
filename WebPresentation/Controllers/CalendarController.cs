using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebPresentation.Models;
using static WebPresentation.Controllers.CalendarItemVM;
using Task = DataDomain.Task;
using Microsoft.AspNetCore.Authorization;

namespace WebPresentation.Controllers
{
    public class CalendarController : Controller
    {
        private TaskManager _taskManager = new TaskManager();
        private EventManager _eventManager = new EventManager();
        private ProjectManager _projectManager = new ProjectManager();

        private List<Task> _tasks = new List<Task>();
        private List<Event> _events = new List<Event>();
        private List<ProjectVM> _projects = new List<ProjectVM>();

        private SignInManager<IdentityUser> _signInManager;

        public CalendarController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        private LogicLayer.IUserManager _userManagerLogic = new UserManager();
        private Models.AccessToken _accessToken = new Models.AccessToken("");

        private void getAccessToken()
        {

            if (_signInManager.IsSignedIn(User))
            {
                string email = User.Identity.Name;
                try
                {
                    _accessToken = new Models.AccessToken(email);
                }
                catch { }
            }
            else
            {
                return;
            }
        }

        // GET: CalendarController
        [Authorize]
        public ActionResult Index(string year, string month)
        {
            getAccessToken();
            int userId = _accessToken.UserID;

            // Step 1 Resolve the month being shown
            var today = DateTime.Today;
            int y;
            if(year == "" || year == null)
            {
                y = today.Year;
            } else
            {
                y = int.Parse(year); 
            }
            int m;
            if(month == "" || month == null)
            {
                m = today.Month;
            } else
            {
                m = int.Parse(month);
            }

            DateTime firstOfMonth = new DateTime(y, m, 1);
            int daysInMonth = DateTime.DaysInMonth(y, m);
            int firstDow = (int)firstOfMonth.DayOfWeek;   // 0‑6  Sun=0
            

            // Step 2 Pull raw data and convert everything to CalendarItemVM
            IEnumerable<CalendarItemVM> items = Enumerable.Empty<CalendarItemVM>()
                // Tasks
                .Concat(_taskManager.GetTasksByUserID(userId).Select(t => new CalendarItemVM
                {
                    Type = "Task",
                    ID = t.TaskID,
                    Name = t.Name,
                    StartDate = t.TaskDate.Date,
                    EndDate = t.TaskDate.Date
                }))
                // Events
                .Concat(_eventManager.ViewEventListByApprovedUserID(userId).Select(e => new CalendarItemVM
                {
                    Type = "Event",
                    ID = e.EventID,
                    Name = e.Name,
                    StartDate = e.StartDate.Date,
                    EndDate = e.EndDate.Date
                }))
                // Projects
                .Concat(_projectManager.GetAllApprovedProjectsByUserID(userId).Select(p => new CalendarItemVM
                {
                    Type = "Project",
                    ID = p.ProjectID,
                    Name = p.Name,
                    StartDate = p.StartDate.Date,
                    EndDate = p.StartDate.Date
                }));

            // Step 3 Prepare a 6 x 7 grid:  weeks -> days -> list of CalendarFragment
            var grid = Enumerable.Range(0, 6)
                                 .Select(_ => Enumerable.Range(0, 7)
                                                        .Select(__ => new List<CalendarFragment>())
                                                        .ToList())
                                 .ToList();

            // Step 4 Expand each span onto every day it covers within this month
            foreach (var itm in items)
            {
                DateTime spanStart = itm.StartDate.Date;
                DateTime spanEnd = itm.EndDate.Date;

                for (DateTime d = spanStart; d <= spanEnd; d = d.AddDays(1))
                {
                    if (d.Year == y && d.Month == m)
                    {
                        int offset = (d.Day - 1) + firstDow;
                        int row = offset / 7;
                        int col = offset % 7;

                        grid[row][col].Add(new CalendarFragment(
                            itm,
                            isStart: d == spanStart,
                            isEnd: d == spanEnd));
                    }
                }
            }

            // Step 5 Send everything to the Razor page
            var vm = new CalendarMonthVM
            {
                Year = y,
                Month = m,
                Grid = grid
            };

            return View(vm);     // expects Views/Calendar/Calendar.cshtml
        }
    }
    public class CalendarFragment
    {
        public CalendarItemVM Item { get; }
        public bool IsStart { get; }
        public bool IsEnd { get; }
        public CalendarFragment(CalendarItemVM item, bool isStart, bool isEnd)
        {
            Item = item;
            IsStart = isStart;
            IsEnd = isEnd;
        }
    }
    /// <summary>
    /// A logical item that will appear on the calendar.
    /// If it lasts more than one day set <see cref="StartDate"/> /= <see cref="EndDate"/>.
    /// </summary>
    public class CalendarItemVM
    {
        public string Type { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Holds a six‑week (6 × 7) grid for one month.
        /// Each cell lists the fragments that fall on that day.
        /// </summary>
        public class CalendarMonthVM
        {
            public int Year { get; set; }
            public int Month { get; set; }

            /// <remarks>
            /// Grid[week][dayOfWeek] -> List&lt;CalendarFragment&gt;  
            /// week -> 0‑5, dayOfWeek -> 0‑6  (Sun‑Sat)
            /// </remarks>
            public List<List<List<CalendarFragment>>> Grid { get; set; }
                = new();   // initialise to avoid null checks

            /// <summary>Convenience label like “April 2025”.</summary>
            public string MonthName => new DateTime(Year, Month, 1)
                                       .ToString("MMMM yyyy");
        }
    }
}

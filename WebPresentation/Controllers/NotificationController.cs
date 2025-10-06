using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using System.Security.Claims;
using WebPresentation.Models;

namespace WebPresentation.Controllers
{
    public class NotificationController : Controller
    {
        private NotificationManager _notificationManager;
        private UserManager _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public NotificationController(SignInManager<IdentityUser> signInManager)
        {
            _notificationManager = new NotificationManager();
            _userManager = new UserManager();
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

        // GET: NotificationController
        [Authorize]
        public ActionResult Index()
        {
            getAccessToken();
            int userID = _accessToken.UserID;
            var vms = _notificationManager.GetAllNotificationsByUserID(userID);

            return View(vms);
        }

        // POST: NotificationController/Create
        [HttpPost]
        public IActionResult MarkAsViewed([FromForm] int id)
        {
            try
            {
                _notificationManager.MarkNotificationAsViewed(id);
                return Json(new { success = true });
            }
            catch
            {
                Response.StatusCode = 500;
                return Json(new { success = false, error = "Could not mark viewed." });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Schedule(int? year, int? month, DateTime? selectedDate)
        {
            getAccessToken();
            int userId = _accessToken.UserID;
            var today = DateTime.Today;
            int y = year ?? today.Year;
            int m = month ?? today.Month;

            // fetch all notifications once
            var all = _notificationManager
                .GetAllSenderNotificationsByUserID(userId)
                .ToList();

            // build calendar cells...
            var firstOfMonth = new DateTime(y, m, 1);
            int lead = (int)firstOfMonth.DayOfWeek;
            var cells = Enumerable.Range(0, 35)
                .Select(slot => {
                    var date = firstOfMonth.AddDays(slot - lead);
                    return new CalendarDayViewModel
                    {
                        Date = date,
                        IsCurrentMonth = date.Month == m,
                        IsToday = date == today,
                        Notifications = all
                            .Where(n => n.StartDate.Date <= date && date <= n.EndDate.Date)
                            .ToList()
                    };
                })
                .ToList();

            // figure out which date we’re showing details for
            var selDate = selectedDate?.Date ?? today;
            var selNotes = all
                .Where(n => n.StartDate.Date <= selDate && selDate <= n.EndDate.Date)
                .ToList();

            var vm = new ScheduleNotificationsViewModel
            {
                Year = y,
                Month = m,
                DisplayMonthYear = firstOfMonth.ToString("MMMM yyyy"),
                Calendar = cells,
                Users = _userManager.GetAllUsers().ToList(),
                SelectedDate = selDate,
                SelectedNotifications = selNotes
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ScheduleNotificationsViewModel form)
        {
            getAccessToken();
            int userId = _accessToken.UserID;
            if (string.IsNullOrWhiteSpace(form.NewTitle)
             || form.NewRecipients == null
             || !form.NewRecipients.Any())
            {
                ModelState.AddModelError("", "Please enter a title and select at least one recipient.");
            }

            if (!ModelState.IsValid)
            {
                // -- Rebuild the calendar grid --
                var firstOfMonth = new DateTime(form.Year, form.Month, 1);
                int lead = (int)firstOfMonth.DayOfWeek;
                var all = _notificationManager
                    .GetAllSenderNotificationsByUserID(userId)
                    .ToList();

                form.DisplayMonthYear = firstOfMonth.ToString("MMMM yyyy");
                form.Calendar = Enumerable
                    .Range(0, 42)
                    .Select(slot =>
                    {
                        var date = firstOfMonth.AddDays(slot - lead);
                        return new CalendarDayViewModel
                        {
                            Date = date,
                            IsCurrentMonth = date.Month == form.Month,
                            IsToday = date == DateTime.Today,
                            Notifications = all
                                .Where(n => n.StartDate.Date <= date && date <= n.EndDate.Date)
                                .ToList()
                        };
                    })
                    .ToList();

                // Re-populate the user list
                form.Users = _userManager.GetAllUsers().ToList();

                // Return the Schedule view, passing back the same VM
                return View("Schedule", form);
            }

            // … otherwise process and redirect …
            var note = new NotificationVM
            {
                Name = form.NewTitle.Trim(),
                Content = form.NewContent?.Trim(),
                StartDate = form.NewStartDate ?? DateTime.Today,
                EndDate = form.NewEndDate ?? form.NewStartDate ?? DateTime.Today,
                Important = form.NewImportant,
                Sender = userId
            };
            _notificationManager.ScheduleNotification(note, form.NewRecipients);

            return RedirectToAction(nameof(Schedule), new { year = form.Year, month = form.Month });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int? year, int? month)
        {
            // pull in your user ID & lists just like in Schedule(...)
            getAccessToken();
            int userId = _accessToken.UserID;

            // default back to today's month/year if none passed
            var today = DateTime.Today;
            int y = year ?? today.Year;
            int m = month ?? today.Month;

            // rebuild calendar cells & user list
            var firstOfMonth = new DateTime(y, m, 1);
            int lead = (int)firstOfMonth.DayOfWeek;
            var all = _notificationManager.GetAllSenderNotificationsByUserID(_accessToken.UserID).ToList();
            var cells = Enumerable.Range(0, 42).Select(slot => {
                var date = firstOfMonth.AddDays(slot - lead);
                return new CalendarDayViewModel
                {
                    Date = date,
                    IsCurrentMonth = date.Month == m,
                    IsToday = date == DateTime.Today,
                    Notifications = all.Where(n => n.StartDate.Date <= date && date <= n.EndDate.Date).ToList()
                };
            }).ToList();

            var vm = new ScheduleNotificationsViewModel
            {
                Year = y,
                Month = m,
                DisplayMonthYear = firstOfMonth.ToString("MMMM yyyy"),
                Calendar = cells,
                Users = _userManager.GetAllUsers().ToList(),
                SelectedDate = DateTime.Today
            };

            return View(vm);  // this will serve Views/Notification/Create.cshtml
        }

    }
}

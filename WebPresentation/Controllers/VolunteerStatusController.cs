/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/04/05
/// Summary:  The controller for volunteerStatuses
/// 
/// Last Updated By: Dat Tran
/// Last Updated: 2025-04-27
/// What was changed: Added TempData to use with Sweet Alert.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed:
///     Added Automation Logic to the Accept & Reject button click event handlers:
///         On Accept, automatically grant the newly approved Volunteer WriteAccess to the project's Forum
///         & assign them the ProjectRole of 'Volunteer'.
///         On Reject, automatically revoke the Volunteer's WriteAccess to the project's Forum (if it exists)
///         & delete any existing ProjectRoles they may have had.
/// </summary>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace WebPresentation.Controllers
{
    public class VolunteerStatusController : Controller
    {
        private IVolunteerStatusManager _volunteerStatusManager = new VolunteerStatusManager();
        private IVolunteerStatusProjectRoleManager _volunteerStatusProjectRoleManager = new VolunteerStatusProjectRoleManager();
        private IForumPermissionManager _forumPermissionManager = new ForumPermissionManager();

        private INotificationManager _notificationManager = new NotificationManager();
        Event _event = null;
        UserVM _accessTon = null;
        int _projectID;


        private SignInManager<IdentityUser> _signInManager;

        public VolunteerStatusController(SignInManager<IdentityUser> signInManager)
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

        // GET: VolunteerStatusController
        public ActionResult Index(int id)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", id) &&
                !_accessToken.HasProjectRole("Volunteer Director", id))
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.projectid = id;
            var pendingVolunteerStatuses = _volunteerStatusManager.GetPendingVolunteerStatusByProjectID(id);
            return View(pendingVolunteerStatuses);
        }

        public ActionResult UpdateVolunteerStatus(bool approved, int userID, int projectID)
        {
            getAccessToken();
            if(!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", projectID) ||
                !_accessToken.HasProjectRole("Volunteer Director", projectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                var oldvolunteerStatus = _volunteerStatusManager.GetVolunteerStatusByUserIDAndProjectID(userID, projectID);
                VMVolunteerStatus volunteerStatus = new VMVolunteerStatus()
                {
                    UserID = userID,
                    ProjectID = projectID,
                    Approved = approved,
                };
                _volunteerStatusManager.UpdateVolunteerStatus(volunteerStatus);

                if(volunteerStatus.Approved == true)
                {
                    ForumPermission newForumPermission = new ForumPermission()
                    {
                        UserID = volunteerStatus.UserID,
                        ProjectID = volunteerStatus.ProjectID,
                        WriteAccess = true
                    };
                    _forumPermissionManager.AddForumPermission(newForumPermission);
                    _volunteerStatusProjectRoleManager.InsertUserProjectRole(volunteerStatus.UserID, volunteerStatus.ProjectID, "Volunteer");
                }
                else
                {
                    ForumPermission newForumPermission = new ForumPermission()
                    {
                        UserID = volunteerStatus.UserID,
                        ProjectID = volunteerStatus.ProjectID,
                        WriteAccess = false
                    };
                    _forumPermissionManager.EditForumPermissionWriteAccessValue(newForumPermission);
                    _volunteerStatusProjectRoleManager.DeleteUserRoles(volunteerStatus.UserID, volunteerStatus.ProjectID);
                }

                if(oldvolunteerStatus.Approved == false)
                {
                    TempData["StatusUpdate"] = "Volunteer accepted into project.";
                    return RedirectToAction("ViewAllRejectedVolunteers", new { id = projectID});
                }
                else {
                    TempData["StatusUpdate"] = "Volunteer status updated.";

                    return RedirectToAction("Index", new { id = projectID });
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Index", new { id = projectID });
            }
        }

        // GET: VolunteerStatusController
        public ActionResult ViewAllRejectedVolunteers(int id)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", id) &&
                !_accessToken.HasProjectRole("Volunteer Director", id))
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.projectid = id;
            var rejectedVolunteerStatuses = _volunteerStatusManager.GetRejectedVolunteerStatusByProjectID(id);
            return View(rejectedVolunteerStatuses);
        }

        // GET: VolunteerStatusController/Create
        public ActionResult Create(int projectId)
        {
            getAccessToken();
            if(!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", projectId) &&
                !_accessToken.HasProjectRole("Volunteer Director", projectId) &&
                !_accessToken.HasProjectRole("Event Coordinator", projectId))
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            _projectID = projectId;
            Notification notification = new Notification();
            notification.Date = DateTime.Now;
            ViewBag.projectId = projectId;
            ViewBag.userID = _accessToken.UserID;
            return View(notification);
        }

        // POST: VolunteerStatusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Notification notification, int passedProjectID)
        {
            getAccessToken();

            int projectID = passedProjectID;
            string name = notification.Name;
            string content = notification.Content;
            DateTime date = (DateTime)notification.Date;

            bool important = notification.Important;

            List<VolunteerStatus> volunteers = _volunteerStatusManager.SelectVolunteerStatusByProjectID(projectID);

            try
            {
                foreach(VolunteerStatus volunteerStatus in volunteers)
                {
                    notification.Name = name;
                    notification.Content = content;
                    notification.Date = date;
                    notification.Sender = _accessToken.UserID;
                    notification.Receiver = volunteerStatus.UserID;
                    notification.Important = important;
                    notification.IsViewed = false;
                    _notificationManager.InsertNotification(notification);
                }

                return RedirectToAction("Index", "Event", new { id = projectID });
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error", ex);
                return View();
            }
        }
    }
}
/// <summary>
/// Creator: Kate Rich
/// Created: 2025-03-27
/// Summary:
///     A controller that handles navigation to the proper views
///     related to background checks for a project.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-08
/// What Was Changed:
///     Added in the Create & Edit functionalities for background checks.
/// Last Updated By: Dat Tran
/// Last Updated: 2025-04-27
/// What was changed: Added TempData for Create/Update Sweet Alerts. 
/// Last Updated By: Dat Tran
/// Last Updated: 2025-05-01
/// What was changed: Added a ViewBag for ProjectID to use with the Index.  
/// </summary>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebPresentation.Controllers
{
    //[Authorize(ProjectRoles = "Background Checker")]
    public class BackgroundCheckController : Controller
    {
        private IBackgroundCheckManager _backgroundCheckManager = new BackgroundCheckManager();
        private IUserManager _userManager = new UserManager();
        private IProjectManager _projectManager = new ProjectManager();
        private SignInManager<IdentityUser> _signInManager;

        public BackgroundCheckController(SignInManager<IdentityUser> signInManager)
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

        // GET: BackgroundCheckController
        public ActionResult Index(int id)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", id) &&
                !_accessToken.HasProjectRole("Background Checker", id) &&
                !_accessToken.HasProjectRole("Volunteer Coordinator", id)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            var backgroundChecks = _backgroundCheckManager.GetBackgroundChecksByProjectID(id);
            ViewBag.ProjectID = id; // So you can safely link back to the project
            return View(backgroundChecks);
        }


        // GET: BackgroundCheckController/Details/5
        public ActionResult Details(int id, int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", id) &&
                !_accessToken.HasProjectRole("Background Checker", id) &&
                !_accessToken.HasProjectRole("Volunteer Coordinator", id)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var backgroundCheck = _backgroundCheckManager.GetBackgroundCheckByID(id);
            return View(backgroundCheck);
        }


        //GET: BackgroundCheckController/Create
        public ActionResult Create(int userID, int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                !_accessToken.HasProjectRole("Background Checker", projectID)
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                User target = _userManager.GetUserById(userID);
                Project project = _projectManager.GetProjectByID(projectID);

                BackgroundCheckVM backgroundCheck = new BackgroundCheckVM();

                //backgroundCheck.Investigator = _accessToken.UserID;
                //backgroundCheck.InvestigatorGivenName = _accessToken.GivenName;
                //backgroundCheck.InvestigatorFamilyName = _accessToken.FamilyName;

                backgroundCheck.Investigator = 100000;
                backgroundCheck.InvestigatorGivenName = "Hank";
                backgroundCheck.InvestigatorFamilyName = "Hill";
                backgroundCheck.UserID = userID;
                backgroundCheck.ProjectID = projectID;
                backgroundCheck.VolunteerGivenName = target.GivenName;
                backgroundCheck.VolunteerFamilyName = target.FamilyName;
                backgroundCheck.ProjectName = project.Name;

                return View(backgroundCheck);
            }
            catch
            {
                return View();
            }
        }

        // POST: BackgroundCheckController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BackgroundCheckVM backgroundCheck)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _backgroundCheckManager.AddBackgroundCheck(backgroundCheck);
                    TempData["AddSuccess"] = "Background check submitted.";
                    return RedirectToAction(nameof(Index), new { id = backgroundCheck.ProjectID });
                }
                else
                {
                    // Server-side validation
                    return View(backgroundCheck);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: BackgroundCheckController/Edit/5
        public ActionResult Edit(int id)
        {

            populateDropdown();

            var backgroundCheck = _backgroundCheckManager.GetBackgroundCheckByID(id);

            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                !_accessToken.HasProjectRole("Background Checker", backgroundCheck.ProjectID)
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View(backgroundCheck);
        }

        // POST: BackgroundCheckController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BackgroundCheckVM newBackgroundCheck)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var backgroundCheck = _backgroundCheckManager.GetBackgroundCheckByID(id);
                    TempData["EditSuccess"] = "Background check updated successfully!";
                    _backgroundCheckManager.EditBackgroundCheck(backgroundCheck, newBackgroundCheck);

                    return RedirectToAction(nameof(Index), new { id = backgroundCheck.ProjectID });
                }
                else
                {
                    // Server-side validation
                    return View(newBackgroundCheck);
                }
            }
            catch
            {
                return View();
            }
        }

        // Helper Methods
        public void populateDropdown()
        {
            List<string> statuses = new List<string>()
            {
                "In Progress",
                "Failed",
                "Passed"
            };
            ViewBag.Statuses = statuses;
        }
    }
}
/// <summary>
/// Jennifer Nicewanner
/// Created: 2025-04-13
/// 
/// The controller for a volunteer director to view and select project applicants
/// Last Updated By: Jennifer Nicewanner
/// Last Updated:    2025-04-13
/// What was Changed:Initial Creation
/// </summary>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace WebPresentation.Controllers
{
    //Author: Jennifer Nicewanner
    public class VolunteerController : Controller
    {
        UserManager _userManager = new UserManager();
        ProjectRoleManager _projectRoleManager = new ProjectRoleManager();
        ProjectManager _projectManager = new ProjectManager();
        VehicleManager _vehicleManager = new VehicleManager();
        SkillManager _skillManager = new SkillManager();
        AvailabilityManager _availabilityManager = new AvailabilityManager();
        VolunteerVM _user = new VolunteerVM();
        private SignInManager<IdentityUser> _signInManager;

        public VolunteerController(SignInManager<IdentityUser> signInManager)
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

        // GET: ViewProjectApplicants
        //Author: Jennifer Nicewanner
        [Authorize]
        public ActionResult VolunteerDetails(int id)
        {
            //_user = (VolunteerVM)_userManager.GetUserById(id);
            _user.Projects = _projectManager.GetAllProjectsByUserID(id);
            _user.Skills = _skillManager.GetUserSkillsByUserID(id);
            _user.Availability = _availabilityManager.SelectAvailabilityByUser(id);
            _user.Vehicles = _vehicleManager.GetVehiclesByUserID(id);
            return View(_user);
        }

        //Author: Jennifer Nicewanner
        public ActionResult Index(int id)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", id) &&
                !_accessToken.HasProjectRole("Volunteer Director", id) &&
                !_accessToken.HasProjectRole("Event Coordinator", id) &&
                !_accessToken.HasProjectRole("Background Checker", id) &&
                !_accessToken.HasProjectRole("Scheduler", id)
                )
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.ProjectID = id;
            var volunteers = _userManager.GetApprovedUserByProjectID(id);
            return View(volunteers);
        }
    }
}


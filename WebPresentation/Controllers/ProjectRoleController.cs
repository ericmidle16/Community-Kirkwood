/// <summary>
/// Eric Idle
/// Created: 2025-04-16
/// What was done:  Initial creation
/// 
/// Last Updated By: Eric Idle
/// Last Updated: 2025/04/25
/// What was Changed: Fixing adding and removing project roles
/// 
/// Last Updated By: Dat Tran
/// Last Updated: 2025/04/27
/// What was Changed: Added TempData for Sweet Alert usage.
/// </summary>
using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebPresentation.Controllers
{
    public class ProjectRoleController : Controller
    {
        ProjectRoleManager _projectRoleManager = new ProjectRoleManager();
        VolunteerStatusProjectRoleManager _volunteerRoleManager = new VolunteerStatusProjectRoleManager();
        UserManager _userManager = new UserManager();
        private SignInManager<IdentityUser> _signInManager;

        public ProjectRoleController(SignInManager<IdentityUser> signInManager)
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

        // GET: ProjectRoleController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProjectRoleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjectRoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectRoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectRoleController/UpdateProjectRole/5
        // Author: Eric Idle
        public ActionResult UpdateProjectRole(int userId, int projectId)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                !_accessToken.HasProjectRole("Project Starter", projectId)
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.UserID = userId;
            ViewBag.ProjectId = projectId;
            return View(createVMList(userId, projectId));
        }

        // POST: ProjectRoleController/UpdateProjectRole/5
        // Author: Eric Idle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProjectRole(int userId, int projectId, List<ProjectRoleVM> roles)
        {
            ViewBag.UserID = userId;
            ViewBag.ProjectID = projectId;
            List<User> volunteers = _userManager.GetApprovedUserByProjectID(projectId);
            

            try
            {
                foreach (ProjectRoleVM role in roles)
                {
                    if (role.Active)
                    {
                        _volunteerRoleManager.InsertUserProjectRole(userId, projectId, role.ProjectRoleID);
                    }
                    else
                    {
                        _volunteerRoleManager.RemoveProjectRoleByUserIDProjectID(userId, projectId, role.ProjectRoleID);
                    }
                }
                TempData["UpdateRole"] = "Project roles updated.";
                return RedirectToAction("Index", "Volunteer", new { id = projectId });
            }
            catch
            {
                return View(createVMList(userId, projectId));
            }
        }

        // GET: ProjectRoleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectRoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Creator:  Eric Idle
        /// Created:  2025/04/25
        /// Summary:  A helper method to feed lists into the Edit dropdowns
        /// </summary>
        private List<ProjectRoleVM> createVMList(int userId, int projectId)
        {
            List<ProjectRole> allRoles = _projectRoleManager.GetAllProjectRoles();
            List<VolunteerStatusProjectRole> currentRoles = _volunteerRoleManager.GetUserProjectRolesByUserIDProjectID(userId, projectId);
            List<ProjectRoleVM> allProjectRolesVM = new List<ProjectRoleVM>();

            List<ProjectRole> activeRoles = allRoles.Where(role => currentRoles.Any(userRole => userRole.ProjectRoleID == role.ProjectRoleID)).ToList();
            List<ProjectRole> inactiveRoles = allRoles.Except(activeRoles).ToList();

            foreach (ProjectRole active in activeRoles)
            {
                if(!active.ProjectRoleID.Equals("Project Starter"))
                {
                    allProjectRolesVM.Add(new ProjectRoleVM
                    {
                        ProjectRoleID = active.ProjectRoleID,
                        Description = active.Description,
                        Active = true
                    });
                }
            }

            foreach (ProjectRole inactive in inactiveRoles)
            {
                if(!inactive.ProjectRoleID.Equals("Project Starter"))
                {
                    allProjectRolesVM.Add(new ProjectRoleVM
                    {
                        ProjectRoleID = inactive.ProjectRoleID,
                        Description = inactive.Description,
                        Active = false
                    });
                }
            }

            return allProjectRolesVM;
        }
    }
}

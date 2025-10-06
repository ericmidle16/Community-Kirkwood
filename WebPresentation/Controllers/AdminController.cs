/// <summary>
/// Jennifer Nicewanner
/// Created: 2025-04-06
/// Summary:
///     The controller for User objects
/// 
/// Updated By: Jennifer Nicewanner
/// Updated:    2025-04-06
/// What was Changed:Initial Creation including a temporary Index plus Details Action Method
/// 
/// Updated By: Jennifer Nicewanner
/// Updated:    2025-04-22
/// What was Changed: Added Edit and Deactivate Action Methods
/// 
/// Last Updated By: Kate Rich
/// Last Updated:    2025-04-26
/// What was Changed: Moved the Index() method back to this controller from the UserController.
/// 
/// Last Updated By: Dat Tran
/// Last Updated: 2025-04-29
/// What was changed: Added TempData for Sweet Alert
/// </summary>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebPresentation.Controllers
{
    public class AdminController : Controller
    {

        private IUserManager _userManager = new UserManager();
        private SignInManager<IdentityUser> _signInManager;

        public AdminController(SignInManager<IdentityUser> signInManager)
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

        /// <summary>
        /// Creator:  Jacob McPherson
        /// Created:  2025/03/29
        /// Summary:  View All Users
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<User> users = _userManager.GetAllUsers();
            return View(users);
        }

        //Author: Jennifer Nicewanner
        // GET: AdminController/Details/5
        public ActionResult Details(string email)
        {
            var user = _userManager.GetUserByEmail(email);
            return View(user);
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(string email)
        {
            var user = _userManager.GetUserByEmail(email);
            return View(user);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string email, User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userManager.UpdateUserStatusByID(_userManager.GetUserByEmail(email), user);
                    TempData["EditUser"] = "User updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(user);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Deactivate/5
        public ActionResult Deactivate(string email)
        {
            var user = _userManager.GetUserByEmail(email);
            return View(user);
        }

        // POST: AdminController/Deactivate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(string email, User user)
        {
            try
            {
                user = _userManager.GetUserByEmail(email);

                if (ModelState.IsValid)
                {
                    _userManager.DeactivateUserByUserID(user.UserID);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(user);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
/// <summary>
/// Creator:  Jacob McPherson
/// Created:  2025/03/29
/// Summary:  The controller for users
/// 
/// Updated By: Jacob McPherson
/// Updated: 2025/04/24
/// What was Changed: Added Deactivate
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-26
/// What Was Changed:
///     Removed the Index() method from this controller & placed it back in the AdminController.
/// 
/// </summary>
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebPresentation.Controllers
{
    public class UserController : Controller
    {
        UserManager _userManager = new UserManager();

        private readonly UserManager<IdentityUser> _identityManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> identityManager, SignInManager<IdentityUser> signInManager)
        {
            _identityManager = identityManager;
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

        // GET: UserController/Details/5
        [Authorize]
        public ActionResult UserProfile(string email)
        {
            User user = new User();

            user = _userManager.RetrieveUserDetailsByEmail(email);
            user.PasswordHash = "";
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: UserController/Delete/5
        [Authorize]
        public ActionResult Delete()
        {
            ViewBag.Error = "";

            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(IFormCollection collection)
        {
            try
            {
                bool success = _userManager.DeactivateUser(GetUserEmail(), collection["password"]);

                if (!success)
                {
                    throw new ArgumentException("Wrong Password Or Email");
                }

                _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ViewBag.Error = "Wrong Password";

                return View();
            }
        }

        /// <summary>
        /// Creator:  Jacob McPherson
        /// Created:  2025/04/24
        /// Summary:  Gets Email of Current User
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        private string GetUserEmail()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return null;
            }

            var user = _identityManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                return null;
            }

            return user.Email;
        }
    }
}

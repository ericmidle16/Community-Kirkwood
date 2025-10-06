/// <summary>
/// Creator:  Jacob McPherson
/// Created:  2025/03/25
/// Summary:  The controller for external contact types
/// Last Updated By: Jacob McPherson
/// Last Updated: 2025/04/18
/// What was Changed: Fixed Create Method Redirect
/// Last Updated By: Dat Tran
/// Last Updated: 2025/04/27
/// What was Changed: Added TempData for Sweet Alert. 
/// </summary>
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebPresentation.Controllers
{
    public class ExternalContactTypeController : Controller
    {
        private ExternalContactManager _externalContactManager = new ExternalContactManager();
        private SignInManager<IdentityUser> _signInManager;

        public ExternalContactTypeController(SignInManager<IdentityUser> signInManager)
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

        // GET: ExternalContactTypeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ExternalContactTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExternalContactTypeController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExternalContactTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExternalContactType type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _externalContactManager.AddExternalContactType(type.ExternalContactTypeID, type.Description);

                    if (!result)
                    {
                        throw new ArgumentException("Insert Failed");
                    }
                    TempData["AddTypeSuccess"] = "External Contact Type added.";
                    return RedirectToAction("Index", "ExternalContact");
                }
                else
                {
                    return View(type);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ExternalContactTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExternalContactTypeController/Edit/5
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

        // GET: ExternalContactTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExternalContactTypeController/Delete/5
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
    }
}

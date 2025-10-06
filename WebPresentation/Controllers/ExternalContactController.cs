/// <summary>
/// Stan Anderson
/// Created: 2025-03-28
/// 
/// The controller for External Contact objects
/// Last Updated By: Jacob McPherson
/// Last Updated:  2025-04-24
/// What was Changed:  Removed Magic Numbers
/// Last Updated By: Dat Tran
/// Last Updated:  2025-04-27
/// What was Changed:  Added TempData to use with Sweet Alerts for Create, Update and Delete controllers.  
/// </summary>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebPresentation.Controllers
{
    public class ExternalContactController : Controller
    {
        ExternalContactManager _externalContactManager = new ExternalContactManager();
        UserManager _userManager = new UserManager();
        private readonly UserManager<IdentityUser> _identityManager;
        private SignInManager<IdentityUser> _signInManager;

        public ExternalContactController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> identityManager)
        {
            _signInManager = signInManager;
            _identityManager = identityManager;
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

        // GET: ExternalContactController
        public ActionResult Index()
        {
            List<ExternalContact> contacts = _externalContactManager.ViewAllExternalContacts();
            return View(contacts);
        }

        // GET: ExternalContactController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            ExternalContact externalContact = _externalContactManager.ViewSingleExternalContact(id);
            return View(externalContact);
        }

        // GET: ExternalContactController/Create
        [Authorize]
        public ActionResult Create()
        {
			ViewBag.Types = _externalContactManager.GetAllExternalContactTypes();
			
            return View();
        }

        // POST: ExternalContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(ExternalContact contact)
        {
            getAccessToken();
            try
            {
				if(ModelState.IsValid)
				{
                    User user = _userManager.GetUserByEmail(GetUserEmail());

                    if (user is null)
                    {
                        //Error if user doesnt relate to a legacy user
                        throw new ArgumentException("User Does Not Exist");
                    }

                    contact.AddedBy = _accessToken.UserID;

                    bool success = _externalContactManager.AddExternalContact(contact);

					if (!success)
					{
						throw new ArgumentException("Insert Failed");
					}
                    TempData["AddSuccess"] = "External contact added!";
                    return RedirectToAction(nameof(Index));
				}
				else
				{
					throw new ArgumentException("Invalid Data");
				}
            }
            catch (Exception ex)
            {
				ViewBag.Types = _externalContactManager.GetAllExternalContactTypes();
                ViewBag.Error = ex.Message;

                return View(contact);
            }
        }

        // GET: ExternalContactController/Edit/5
		[Authorize]
        public ActionResult Edit(int id)
        {
			ViewBag.Types = _externalContactManager.GetAllExternalContactTypes();

			ExternalContact contact = _externalContactManager.ViewSingleExternalContact(id);

            return View(contact);
        }

        // POST: ExternalContactController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
        public ActionResult Edit(int id, ExternalContact contact)
        {
            getAccessToken();
            try
            {
				if (ModelState.IsValid)
				{
					contact.ExternalContactID = id;
					ExternalContact oldContact = _externalContactManager.ViewSingleExternalContact(id);

                    User user = _userManager.GetUserByEmail(GetUserEmail());

                    if(user is null)
                    {
                        //Error if user doesnt relate to a legacy user
                        throw new ArgumentException("User Does Not Exist");
                    }

					bool success = _externalContactManager.EditExternalContact(_accessToken.UserID, contact, oldContact);

					if (!success)
					{
						throw new ArgumentException("Update Failed");
					}
                    TempData["UpdateSuccess"] = "External contact updated!";
                    return RedirectToAction(nameof(Index));
				}
				else
				{
					throw new ArgumentException("Invalid Data");
				}
            }
            catch (Exception ex)
            {
				ViewBag.Types = _externalContactManager.GetAllExternalContactTypes();
                ViewBag.Error = ex.Message;
				
                return View(contact);
            }
        }

        // GET: ExternalContactController/Delete/5
		[Authorize]
        public ActionResult Delete(int id)
        {
			ExternalContactVM contact = _externalContactManager.ViewSingleExternalContact(id);
			
            return View(contact);
        }

        // POST: ExternalContactController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
        public ActionResult Delete(int id, ExternalContactVM contact)
        {
            try
            {
				bool success = _externalContactManager.DeactivateExternalContact(id);

				if (!success)
				{
					throw new ArgumentException("Deactivate Failed");
				}
                TempData["DeleteSuccess"] = "External contact removed.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(contact);
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

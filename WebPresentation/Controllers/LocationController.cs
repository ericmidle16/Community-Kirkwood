/// <summary>
/// Stan Anderson
/// Created: 2025-03-28
/// 
/// The controller for Location objects
/// Last Updated By:  Jennifer Nicewanner
/// Last Updated:     2025-04-13
/// What was Changed: Added methods for the Details Controller
/// 
/// Last Updated By:  Chase Hannen
/// Last Updated:     2025-04-24
/// What was Changed: Added methods for Create and Delete
/// Last Updated By:  Dat Tran
/// Last Updated:     2025-04-25
/// What was Changed: Added TempData for Sweet Alerts
/// </summary>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Mono.TextTemplating;
using Microsoft.AspNetCore.Authorization;

namespace WebPresentation.Controllers
{
    public class LocationController : Controller
    {
        LocationManager _locationManager = new LocationManager();
        private SignInManager<IdentityUser> _signInManager;

        public LocationController(SignInManager<IdentityUser> signInManager)
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
        /// Stan Anderson
        /// Created: 2025-03-28
        /// GET: LocationController
        /// View all of the locations
        /// Last Updated By:Chase Hannen
        /// Last Updated:    2025-04-24
        /// What was Changed: Changed to viewing only active locations so delete works
        /// </summary>
        [Authorize]
        public ActionResult Index()
        {
            List<DataDomain.Location> locations = _locationManager.ViewAllActiveLocations();
            return View(locations);
        }

        // GET: LocationController/Details/5
        //Author: Jennifer Nicewanner
        [Authorize]
        public ActionResult Details(int id)
        {
            DataDomain.Location location = _locationManager.GetLocationByID(id);
            return View(location);
        }

        // GET: LocationController/Create
        // Author: Chase Hannen
        public ActionResult Create()
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                !_accessToken.ProjectRoles.Any(pr => pr.ProjectRole == "Project Starter")
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View();
        }

        // POST: LocationController/Create
        // Author: Chase Hannen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DataDomain.Location location)
        {
            // NOT NULL and max length checks
            if (location.Name.Equals("") || location.Name.Equals(null) || location.Name.Length > 50)
            {
                ModelState.AddModelError("Name", "Invalid Location Name.");
            }
            if (location.Address.Equals("") || location.Address.Equals(null) || location.Address.Length > 100)
            {
                ModelState.AddModelError("Address", "Invalid Location Address.");
            }
            if (location.City.Equals("") || location.City.Equals(null) || location.City.Length > 100)
            {
                ModelState.AddModelError("City", "Invalid Location City.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _locationManager.AddNewLocation(location);
                    TempData["SuccessMessage"] = "Location added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(location);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var location = _locationManager.GetLocationByID(id);
                getAccessToken();
                if (!_accessToken.IsLoggedIn ||
                    !_accessToken.ProjectRoles.Any(pr => pr.ProjectRole == "Project Starter")
                    )
                {
                    return RedirectToAction("AccessDenied", "Home");
                }

                if (location != null)
                {
                    return View(location);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error loading location: " + ex.Message);
                return View();
            }
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DataDomain.Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location);
            }

            try
            {
                _locationManager.UpdateLocationByID(id, location);
                TempData["UpdateSuccess"] = "Location updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating location: " + ex.Message);
                return View(location);
            }
        }

        // GET: LocationController/Delete/5
        // Author: Chase Hannen
        public ActionResult Delete(int id)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                !_accessToken.ProjectRoles.Any(pr => pr.ProjectRole == "Project Starter")
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            DataDomain.Location location = _locationManager.GetLocationByID(id);
            return View(location);
        }

        // POST: LocationController/Delete/5
        // Author: Chase Hannen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _locationManager.DeactivateLocationByLocationID(id);
                TempData["DeleteSuccess"] = "Location deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

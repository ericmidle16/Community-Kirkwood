/// <summary>
/// Creator: Christivie Mauwa
/// Created: 2025/02/06
/// Summary:
///     Class for the creation of User Objects with set data fields
/// </summary>
///
/// <remarks>
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-25
/// What Was Changed:
///     Added the Summary() action method, so users can view a summary of their monetary project donations.
/// </remarks>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Mono.TextTemplating;
using Microsoft.AspNetCore.Authorization;

namespace WebPresentation.Controllers
{
    public class DonationsController :  Controller
    {
        private IDonationManager _donationManager = new DonationManager();
        private IUserManager _userManager = new UserManager();
        private IProjectManager _projectManager = new ProjectManager();
        private ILocationManager _locationManager = new LocationManager();
        private IDonationTypeManager _donationTypeManager = new DonationTypeManager();
        private SignInManager<IdentityUser> _signInManager;

        public DonationsController(SignInManager<IdentityUser> signInManager)
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

        // Author: Christivie Mauwa
        // GET: DonationsController1
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var mgr = new DonationManager();
            var donations = mgr.GetAllDonation()
                               .GroupBy(d => new { d.UserID, d.ProjectID, d.Amount, d.DonationDate })
                               .Select(g => g.First())
                               .ToList();
            var donationVMs = new List<DonationVM>();

            foreach (var donation in donations)
            {
                var user = _userManager.GetUserById(donation.UserID);
                var project = _projectManager.GetProjectVMByID(donation.ProjectID);
                var location = project?.LocationID!= null? _locationManager.GetLocationByID(project.LocationID): null;

                var donationVM = new DonationVM
                {
                    DonationID = donation.DonationID,
                    DonationType = donation.DonationType,
                    UserID = donation.UserID,
                    ProjectID = donation.ProjectID,
                    Amount = donation.Amount,
                    DonationDate = donation.DonationDate,
                    Description = donation.Description,
                    UserName = $"{user?.GivenName} {user?.FamilyName}",
                    Name = project?.Name,
                    //LocationName = location != null ? $" {location.Address}, {location.City}, {location.State}" : ""
                };

                donationVMs.Add(donationVM);
            }

            return View(donationVMs);
        }

        // Author: Christivie Mauwa
        // GET: DonationsController1/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "View Single Donation Details";

            var donation = _donationManager.RetrieveDonationByDonationID(id);
            var user = _userManager.GetUserById(donation.UserID);
            var project = _projectManager.GetProjectVMByID(donation.ProjectID);
            var location = project?.LocationID != null ? _locationManager.GetLocationByID(project.LocationID) : null;

            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Donator", donation.ProjectID) &&
                !_accessToken.HasProjectRole("Accountant", donation.ProjectID) &&
                !_accessToken.HasProjectRole("Purchaser", donation.ProjectID) &&
                !_accessToken.HasProjectRole("Project Starter", donation.ProjectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var donationVM = new DonationVM
            {
                DonationID = donation.DonationID,
                DonationType = donation.DonationType,
                UserID = donation.UserID,
                ProjectID = donation.ProjectID,
                Amount = donation.Amount,
                DonationDate = donation.DonationDate,
                Description = donation.Description,
                UserName = $"{user?.GivenName}, {user?.FamilyName}",
                Name = project?.Name,
                LocationName = location != null ? $"{location.Address}, {location.City}, {location.State}" : ""
            };
            return View(donationVM);
        }

        // Author: Christivie Mauwa
        // GET: DonationController/History
        [Authorize]
        public ActionResult ViewDonationHistory(int id)
        {
            ViewBag.Title = "View Donation History";
            var donations = _donationManager.RetrieveDonationByUserId(id);
            return View(donations);
        }

        // Author: Christivie Mauwa
        //GET: DonationController/Invoice
        public ActionResult ViewDonationInvoice(int id)
        {
            ViewBag.Title = "View Donation Invoice";
            var donation = _donationManager.RetrieveDonationByDonationID(id);
            var project = _projectManager.GetProjectVMByID(donation.ProjectID);
            var location = project?.LocationID != null ? _locationManager.GetLocationByID(project.LocationID) : null;

            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Accountant", donation.ProjectID) &&
                !_accessToken.HasProjectRole("Purchaser", donation.ProjectID) &&
                !_accessToken.HasProjectRole("Project Starter", donation.ProjectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var donationVM = new DonationVM
            {
                UserID = donation.UserID,
                DonationID = donation.DonationID,
                ProjectID = donation.ProjectID,
                Amount = donation.Amount,
                DonationDate = donation.DonationDate,
                Name = project?.Name,
                LocationName = location != null ? $"{location.Address}, {location.City}, {location.State}" : ""
            };
            return View(donationVM);
        }

        // Author: Christivie Mauwa
        // GET: DonationsController1/Create
        [Authorize]
        public ActionResult Create(int userID, int projectID)
        {
            var typeMgr = new DonationTypeManager();
            var donationType= typeMgr.GetAllDonationType() ?? new List<DonationType>();
            var project = _projectManager.GetProjectVMByID(projectID);
            var user = _userManager.GetUserById(userID);

            ViewBag.DonationType = new SelectList(donationType, "DonationTypeID", "DonationTypeID");
            ViewBag.Title = "Donate to Project";

            var donationVM = new DonationVM
            {
                UserID = user.UserID  ,
                UserName = $"{user?.GivenName}, {user?.FamilyName}",
                ProjectID = project.ProjectID,
                Name = project.Name,
                DonationDate = DateTime.Now
            };

            return View(donationVM);

        }

        // Author: Christivie Mauwa
        // POST: DonationsController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DonationVM donationVM)
        {
            bool isAnonymous = Request.Form["Anonymous"] == "true";

            if (isAnonymous && donationVM.UserID > 0)
            {
                ModelState.AddModelError("UserID", "User ID should be 0 for anonymous donations.");
            }

            if (ModelState.IsValid)
            {

                try
                {
                    _donationManager.AddDonation(donationVM);
                    TempData["SuccessMessage"] = "Thank you for your donation!";
                    return RedirectToAction("Index","Project");
                }
                catch
                {
                    return View();
                }
            }

            ViewBag.DonationType = new SelectList(_donationTypeManager.GetAllDonationType(), "DonationTypeID", "DonationTypeID");

            return View(donationVM);
        }

        // GET: DonationController
        [Authorize]
        public ActionResult Summary(int userID)
        {
            ViewBag.ActiveUserID = userID;
            var donationSummaries = _donationManager.GetMonetaryProjectDonationSummariesByUserID(userID);
            return View(donationSummaries);
        }
    }
}

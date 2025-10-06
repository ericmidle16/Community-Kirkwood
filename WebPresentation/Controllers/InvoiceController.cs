/// <summary>
/// Creator:  Eric Idle
/// Created:  2025/04/10
/// Summary:  The controller for invoices
/// 
/// Last Updated By:Akoi Kollie
/// Last Updated: 2025/04/25
/// What Was Changed:
/// 
///Last Updated By: Dat Tran
/// Last Updated: 2025/04/28
/// What Was Changed: Added TempData to be used with Sweet Alert. 
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/05/01
/// What Was Changed: Changed the index view to only show invoices from the relevent project and changed the edit and details views to
///     include the projectID so they can redirect back to the index page
/// </summary>

using LogicLayer;
using DataDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace WebPresentation.Controllers
{
    public class InvoiceController : Controller
    {
        InvoiceManager _invoiceManager = new InvoiceManager();
        private SignInManager<IdentityUser> _signInManager;

        public InvoiceController(SignInManager<IdentityUser> signInManager)
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

        // GET: InvoiceController
        public ActionResult Index(int projectId)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Accountant", projectId) &&
                !_accessToken.HasProjectRole("Purchaser", projectId) &&
                !_accessToken.HasProjectRole("Project Starter", projectId)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            List<Invoice> invoices = _invoiceManager.GetAllInvoicesByProjectID(projectId);
            return View(invoices);
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(int id)
        {
            var invoice = _invoiceManager.GetInvoiceByInvoiceID(id);
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Accountant", invoice.ProjectID) &&
                !_accessToken.HasProjectRole("Purchaser", invoice.ProjectID) &&
                !_accessToken.HasProjectRole("Project Starter", invoice.ProjectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            return View(invoice);
        }

        // GET: InvoiceController/Create
        public ActionResult Create(int projectID, int expenseID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Accountant", projectID) &&
                !_accessToken.HasProjectRole("Purchaser", projectID) &&
                !_accessToken.HasProjectRole("Project Starter", projectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            Invoice invoice = new Invoice();
            invoice.InvoiceDate = DateTime.Now;
            invoice.ProjectID = projectID;
            invoice.ExpenseID = expenseID;
            populateDropDowns();
            ViewBag.ProjectID = projectID;
            return View(invoice);
        }
        private void populateDropDowns()
        {
            List<string> Status = new List<string>()
            {
                "Pending",
                "Paid",
                "Processed"
            };
            ViewBag.Status = Status;
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _invoiceManager.InsertInvoice(invoice);
                    TempData["CreateInvoice"] = "Invoice created successfully.";
                    return RedirectToAction("Index", new { projectId = invoice.ProjectID });
                }
                catch
                {
                    populateDropDowns();
                    return View(invoice);
                }
            }
            populateDropDowns();
            return View(invoice);
        }

        // GET: InvoiceController/Edit/5
        public ActionResult Edit(int id)
        {
            populateDropDowns();
            
            var invoice = _invoiceManager.GetInvoiceByInvoiceID(id);

            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Accountant", invoice.ProjectID) &&
                !_accessToken.HasProjectRole("Purchaser", invoice.ProjectID) &&
                !_accessToken.HasProjectRole("Project Starter", invoice.ProjectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.ProjectID = invoice.ProjectID;
            return View(invoice);
        }

        // POST: InvoiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Invoice invoice)
        {
            try
            {
                var rowsaffected = _invoiceManager.EditChangeInvoiceStatusByInvoiceID(invoice.InvoiceID, invoice.Status);
                if (rowsaffected == false)
                {
                    throw new Exception("Update failed");
                }
                TempData["EditInvoice"] = "Invoice editted.";
                return RedirectToAction("Index", new { projectId = invoice.ProjectID });
            }
            catch
            {
                return View();
            }
        }
    }
}
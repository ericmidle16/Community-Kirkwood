/// <summary>
/// Creator:  Eric Idle
/// Created:  2025/04/10
/// Summary:  The controller for expenses
/// 
/// Last Updated By:
/// Last Updated:
/// What Was Changed:
/// </summary>

using LogicLayer;
using DataDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Build.Evaluation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace WebPresentation.Controllers
{
    public class ExpenseController : Controller
    {
        ExpenseManager _expenseManager = new ExpenseManager();
        ExpenseTypeManager _expenseTypeManager = new ExpenseTypeManager();
        private SignInManager<IdentityUser> _signInManager;

        public ExpenseController(SignInManager<IdentityUser> signInManager)
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

        // GET: ExpenseController
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

            List<Expense> expenses = _expenseManager.GetAllExpensesByProjectID(projectId);
            ViewBag.ProjectID = projectId;
            return View(expenses);
        }

        // GET: ExpenseController/Details/5
        public ActionResult Details(int expenseId, int projectId)
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

            var expense = _expenseManager.GetExpenseByExpenseIDProjectID(expenseId, projectId);
            ViewBag.ProjectID = projectId;
            return View(expense);
        }

        private void populateDropDowns()
        {
            List<string> ExpenseTypeIDs = _expenseTypeManager
                                        .GetAllExpenseTypes()
                                        .Select(et => et.ExpenseTypeID.ToString())
                                        .ToList();

            ViewBag.ExpenseTypeIDs = ExpenseTypeIDs;
        }

        // GET: ExpenseController/Create
        public ActionResult Create(int projectId)
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

            var expense = new Expense();
            populateDropDowns();
            expense.ProjectID = projectId;
            ViewBag.ProjectID = projectId;
            return View(expense);
        }

        // POST: ExpenseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expense expense)
        {
            ViewBag.ProjectID = expense.ProjectID;
            try
            {
                if (expense.Date == null)
                {
                    ModelState.AddModelError("Date", "Pick a date.");
                }

                if (expense.Amount <= 0)
                {
                    ModelState.AddModelError("Amount", "Amount must be greater than zero.");
                }

                if (!ModelState.IsValid)
                {
                    populateDropDowns();
                    return View(expense);
                }

                _expenseManager.AddExpenseByProjectID(expense);
                return RedirectToAction("Index", new { projectID = expense.ProjectID });
            }
            catch
            {
                return View();
            }
        }

        // GET: ExpenseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExpenseController/Edit/5
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

        // GET: ExpenseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExpenseController/Delete/5
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

///<summary>
/// Creator: Dat Tran
/// Created: 2025-03-27
/// Summary: This class is the controller for the Need List web form. 
/// Last updated by:
/// Last updated: 
/// Changes:
/// </summary>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicLayer;
using DataDomain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Build.Evaluation;
using Microsoft.AspNetCore.Identity;

namespace WebPresentation.Controllers
{
    public class NeedListController : Controller
    {
        private NeedListManager _needListManager = new NeedListManager();
        private SignInManager<IdentityUser> _signInManager;

        public NeedListController(SignInManager<IdentityUser> signInManager)
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

        // GET: NeedListController
        public ActionResult NeedList(int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Volunteer", projectID) &&
                !_accessToken.HasProjectRole("Project Starter", projectID))
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.ProjectID = projectID;
            var needList = _needListManager.GetNeedList(projectID);

            if(needList == null || !needList.Any())
            {
                ViewBag.Message = "No items in the list. Please add items.";
            }
            return View(needList);
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-04-01
        /// Summary: This method is the ActionResult that handles the Update/Edit. 
        /// Entering sample data. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public ActionResult Update(int id)
        {
            try
            {
                var item = _needListManager.SelectSingleItem(id);
                if (item == null)
                {
                    return Ok("null value");

                }
                getAccessToken();
                if (!_accessToken.IsLoggedIn ||
                    (!_accessToken.HasProjectRole("Event Coordinator", item.ProjectID) &&
                    !_accessToken.HasProjectRole("Project Starter", item.ProjectID)
                    ))
                {
                    return RedirectToAction("AccessDenied", "Home");
                }

                ViewBag.ProjectID = item.ProjectID;
                return View(item);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        //GET: NeedListController/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int itemID, NeedList needList)
        {
            var updateList = new NeedList();
            
            updateList.ItemID = itemID;
            NeedList oldList = _needListManager.SelectSingleItem(itemID);
           
            try
            {
                
                if (ModelState.IsValid)
                {
                    bool updatedList = _needListManager.UpdateNeedList(needList.ProjectID, needList.Name, needList.Quantity, needList.Price, needList.Description, oldList.Name, oldList.Quantity, oldList.Price, oldList.Description, itemID );
                    TempData["SuccessMessage"] = "Data saved successfully!";
                    return RedirectToAction("NeedList", new { projectID = needList.ProjectID });
                }
                else
                {
                    return View(needList);
                }

            }
            catch
            {

                return View();
            }
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/03/30
        /// Summary: ActionResult that handles the Add Item to the need list function
        /// currently the projectID is being set
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public ActionResult AddItem(int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Event Coordinator", projectID) &&
                !_accessToken.HasProjectRole("Project Starter", projectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var needlist = new NeedList();
            needlist.ProjectID = projectID;
            ViewBag.ProjectID = projectID;
            return View(needlist);
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/03/30
        /// Summary: ActionResult that handles the AddItem. 
        /// It checks if the item name is already in the project, if it is it will display an model state error 
        /// when the user clicks Add Item on the view. Which will tell the user they have already entered that item 
        /// and that they need to either chose a new item name or update the item. It handles if the user puts in a 
        /// quantity or price that is less than 0 and displays a message for the user. It also handles if the user puts in 
        /// a too long item name or description.
        /// Last Updated By: Dat Tran
        /// Last Updated: 2025-04-18
        /// What Was Changed: Fixed issues with controller to view. 
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(int projectID, string name, int quantity, decimal price, string description)
        {
            ViewBag.ProjectID = projectID;
            try
            {
                //return Ok(projectID + name + quantity + price + description);
                if (quantity <= 0)
                {
                    ModelState.AddModelError("Quantity", "Quantity must be greater than zero.");
                    return View(new NeedList { ProjectID = projectID, Name = name, Quantity = quantity, Price = price, Description = description });
                }

                if (price <= 0)
                {
                    ModelState.AddModelError("Price", "Price must be greater than zero.");
                    return View(new NeedList { ProjectID = projectID, Name = name, Quantity = quantity, Price = price, Description = description });
                }

                if (name.Length > 50)
                {
                    ModelState.AddModelError("Name", "The length of your item's name cannot exceed 50 characters.");
                    return View(new NeedList { ProjectID = projectID, Name = name, Quantity = quantity, Price = price, Description = description });
                }

                if (description.Length > 250)
                {
                    ModelState.AddModelError("Description", "The length of your item's description cannot exceed 250 characters.");
                    return View(new NeedList { ProjectID = projectID, Name = name, Quantity = quantity, Price = price, Description = description });
                }

                bool exists = _needListManager.SelectExistingItemName(projectID, name);

                if (exists)
                {
                    ModelState.AddModelError("", "You have already inserted this item into your list. Please update the item or chose a new item to add.");
                    return View(new NeedList { ProjectID = projectID, Name = name, Quantity = quantity, Price = price, Description = description });
                }
                _needListManager.InsertItemToNeedList(projectID, name, quantity, price, description);
                TempData["AddItemSuccess"] = "Item added successfully!";
                return RedirectToAction("NeedList", new {projectID = projectID});


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request: " + ex.Message);
                return View(new NeedList { ProjectID = projectID, Name = name, Quantity = quantity, Price = price, Description = description });
            }
        }

        // GET: NeedListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NeedListController/Create
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

        // GET: NeedListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NeedListController/Edit/5
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

        // GET: NeedListController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: NeedListController/Delete/5

        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-04-02
        /// Summary: This method is the ActionResult that handles Deleting an item from a need list. 
        /// This one does not require a GET method because no view is necessary to see the item being deleted. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int itemID, int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Event Coordinator", projectID) &&
                !_accessToken.HasProjectRole("Project Starter", projectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                _needListManager.DeleteFromNeedList(itemID);
                TempData["DeleteSuccess"] = "Item deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["DeleteSuccess"] = "Error deleting item: " + ex.Message;
            }

            return RedirectToAction("NeedList", new {projectID = projectID});
        }

    }
}

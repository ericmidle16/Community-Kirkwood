/// <summary>
/// Josh Nicholson
/// Created: 2025-04-04
/// What was done:  Initial creation of update skills
/// 
/// Last Updated By:  Jennifer Nicewanner
/// Last Updated: 2025-04-13
/// What was Changed: Removed hardcoded user ID, initial creation of Index() and View
/// 
/// Last Updated By:  Dat Tran
/// Last Updated: 2025-04-27
/// What was Changed: Added TempData for create/update user skills to work with Sweet Alert. 
/// </summary>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebPresentation.Controllers
{
    public class SkillController : Controller
    {
        SkillManager _skillManager = new SkillManager();
        UserManager _userManager = new UserManager();
        private Models.AccessToken _accessToken = new Models.AccessToken("");
        private SignInManager<IdentityUser> _signInManager;

        public SkillController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

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

        [Authorize]
        public ActionResult Index(int id)
        {
            List<Skill> allSkills = _skillManager.GetAllSkills();
            List<UserSkill> currentSkills = _skillManager.GetUserSkillsByUserID(id);

            List<Skill> userSkills = allSkills.Where(skill => currentSkills.Any(userSkill => userSkill.SkillID == skill.SkillID)).ToList();

            return View(userSkills);
        }

        // GET: SkillController/Create
        // Author: Josh Nicholson
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillController/Create
        // Author: Josh Nicholson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Skill skill)
        {
            try
            {
                if (!string.IsNullOrEmpty(skill.SkillID) && skill.SkillID.Length > 50)
                {
                    ModelState.AddModelError("SkillID", "The SkillName field is too long.");
                }

                if (!string.IsNullOrEmpty(skill.Description) && skill.Description.Length > 250)
                {
                    ModelState.AddModelError("Description", "The Description field is too long.");
                }

                if (ModelState.IsValid)
                {
                    _skillManager.AddSkill(skill);
                    TempData["AddSkill"] = "Skill added successfully.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(skill);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: SkillController/UpdateSkills/5
        // Author: Josh Nicholson
        [Authorize]
        public ActionResult UpdateSkills(int userID)
        {
            getAccessToken();
            userID = _accessToken.UserID;

            ViewBag.UserID = userID;
            return View(createVMList(userID));
        }

        // POST: SkillController/UpdateSkills/5
        // Author: Josh Nicholson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSkills(int userID, List<SkillVM> skills)
        {
            getAccessToken();
            userID = _accessToken.UserID;
            ViewBag.UserID = userID;
            User user = _userManager.GetUserInformationByUserID(userID);

            try
            {
                foreach (SkillVM skill in skills)
                {
                    if (skill.Active)
                    {
                        _skillManager.AddUserSkill(userID, skill.SkillID);
                    }
                    else
                    {
                        _skillManager.RemoveUserSkill(userID, skill.SkillID);
                    }
                }
                TempData["SkillUpdate"] = "Your list of skills was updated.";
                return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
            }
            catch
            {
                return View(createVMList(userID));
            }
        }

        /// <summary>
        /// Creator:  Josh Nicholson
        /// Created:  2025/04/24
        /// Summary:  A helper method to feed lists into the UpdateSkills dropdowns
        /// </summary>
        private List<SkillVM> createVMList(int userID)
        {
            List<Skill> allSkills = _skillManager.GetAllSkills();
            List<UserSkill> currentSkills = _skillManager.GetUserSkillsByUserID(userID);
            List<SkillVM> allSkillsVM = new List<SkillVM>();

            List<Skill> activeSkills = allSkills.Where(skill => currentSkills.Any(userSkill => userSkill.SkillID == skill.SkillID)).ToList();
            List<Skill> inactiveSkills = allSkills.Except(activeSkills).ToList();

            foreach (Skill active in activeSkills)
            {
                allSkillsVM.Add(new SkillVM
                {
                    SkillID = active.SkillID,
                    Description = active.Description,
                    Active = true
                });
            }

            foreach (Skill inactive in inactiveSkills)
            {
                allSkillsVM.Add(new SkillVM
                {
                    SkillID = inactive.SkillID,
                    Description = inactive.Description,
                    Active = false
                });
            }

            return allSkillsVM;
        }
    }
}

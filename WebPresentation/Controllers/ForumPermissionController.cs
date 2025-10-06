///<summary>
/// Creator: Syler Bushlack
/// Created: 2025-04-24
/// Summary:
///     A controller that handles forum permissions
/// Updated By: Dat Tran
/// Updated: 2025-04-27
/// What was updated: Added bool checks for mute status to trigger the correct TempData for Sweet Alert.      
///</summary>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebPresentation.Controllers
{
    public class ForumPermissionController : Controller
    {
        IForumPermissionManager _forumPermissionManager = new ForumPermissionManager();
        private SignInManager<IdentityUser> _signInManager;

        public ForumPermissionController(SignInManager<IdentityUser> signInManager)
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

        // GET: ForumPermissionController
        public ActionResult Index(int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                !_accessToken.HasProjectRole("Moderator", projectID)
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var permissions = _forumPermissionManager.GettForumPermissionsByProjectID(projectID);
            ViewBag.ProjectID = projectID;
            return View(permissions);
        }

        public ActionResult Mute(int projectID, int userID, bool writeAccess)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                !_accessToken.HasProjectRole("Moderator", projectID)
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                var permission = new ForumPermission();
                permission.UserID = userID;
                permission.ProjectID = projectID;
                permission.WriteAccess = writeAccess;
                _forumPermissionManager.EditForumPermissionWriteAccessValue(permission);
                if (writeAccess == false)
                {
                    TempData["MuteUser"] = "User was muted.";
                }
                else
                {
                    TempData["UnmuteUser"] = "User was unmuted.";
                }
            }
            catch (Exception)
            {
                RedirectToAction(nameof(Index), new { projectID = projectID });
            }
            return RedirectToAction(nameof(Index), new { projectID = projectID });
        }
    }
}

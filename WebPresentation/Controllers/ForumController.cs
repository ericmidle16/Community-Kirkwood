/// <summary>
/// Creator: Jackson Manternach
/// Created: 2025/04/07
/// Summary: Controller class to handle forum code
///
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025/04/18
/// What Was Changed: Added ForumPermissionManager related items. Added CreateThread code.
/// Last Updated By: Dat Tran
/// Last Updated: 2025/04/27
/// What Was Changed: Added TempData for Sweet Alert.
/// </summary>


using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using LogicLayer;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace WebPresentation.Controllers
{
    public class ForumController : Controller
    {
        private readonly IThreadManager _threadManager = new ThreadManager();
        private readonly IProjectManager _projectManager = new ProjectManager();
        private readonly IForumPermissionManager _forumPermissionManager = new ForumPermissionManager();
        private SignInManager<IdentityUser> _signInManager;

        public ForumController(SignInManager<IdentityUser> signInManager)
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
        /// Creator: Jackson Manternach
        /// Created: 2025/04/07
        /// Summary: Method to get all threads and project information, includes default information in case a project
        /// is not found
        ///
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public IActionResult ViewProjectForum(int id)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Volunteer", id) &&
                !_accessToken.HasProjectRole("Project Starter", id)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                var threads = _threadManager.GetThreadsByProjectID(id);
                var project = _projectManager.GetProjectByID(id);

                ViewBag.ProjectName = project?.Name ?? "Project";
                ViewBag.ProjectID = id;

                return View(threads);
            }
            catch (Exception)
            {
                ViewBag.ProjectName = "Project";
                ViewBag.ProjectID = id;
                return View(new List<ThreadVM>());
            }
        }
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/18
        /// Summary: Method to create a new thread
        ///
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public ActionResult CreateThread(int userID, int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Volunteer", projectID) &&
                !_accessToken.HasProjectRole("Project Starter", projectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            userID = _accessToken.UserID;
            var project = _projectManager.GetProjectByID(projectID);
            var datePosted = DateTime.Now;

            var thread = new ThreadVM
            {
                UserID = userID,
                ProjectID = projectID,
                DatePosted = datePosted
            };

            ViewBag.ProjectID = projectID;
            return View(thread);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/18
        /// Summary: Method to create a new thread
        ///
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateThread(int userID, string content, int projectID, string threadName, DateTime datePosted)
        {
            ViewBag.ProjectID = projectID;
            try
            {
                if (!_forumPermissionManager.SelectUserWriteAccess(userID, projectID))
                {
                    ModelState.AddModelError("", "You do not have permission to post in this project forum.");
                    return View(new ThreadVM
                    {
                        UserID = userID,
                        Content = content,
                        ProjectID = projectID,
                        ThreadName = threadName,
                        DatePosted = datePosted
                    });
                }

                if (ModelState.IsValid)
                {
                    bool success = _threadManager.InsertForumPost(userID, content, projectID, threadName, datePosted);
                    TempData["SuccessMessage"] = "Project Thread created.";
                    return RedirectToAction("ViewProjectForum", new { id = projectID });

                }

                return View(new ThreadVM
                {
                    UserID = userID,
                    Content = content,
                    ProjectID = projectID,
                    ThreadName = threadName,
                    DatePosted = datePosted
                });

            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while trying to create the thread. Please try again later.");

                return View(new ThreadVM
                {
                    UserID = userID,
                    Content = content,
                    ProjectID = projectID,
                    ThreadName = threadName,
                    DatePosted = datePosted
                });

            }
        }
    }
}
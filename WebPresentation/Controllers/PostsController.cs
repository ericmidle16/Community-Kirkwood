/// <summary>
/// Creator: Nik Bell
/// Created: 2025-04-18
/// Summary:
///     A controller that handles navigation to the proper views
///     related to forum posts for a project.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-18
/// What Was Changed:
///     Updated the Index() method parameter to just be 'id', so that it can
///     properly handle values being passed to it from this & other Controller
///     classes.
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-18
/// What Was Changed:
///     Added method to edit the user's own posts. I also updated the create to reload the thread properly.
///     As it was directing back to a blank view.
///     
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-24
/// What Was Changed:
///     Added pin method to pin posts and added validation to check if user has permision to create a post
///     
/// Last Updated By: Dat Tran
/// Last Updated: 2024-04-26
/// What was changed: Added TempData for Sweet Alert CRUD functions.   
/// </summary>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicLayer;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using DataDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;

namespace WebPresentation.Controllers
{
    public class PostsController : Controller
    {
        IPostManager _postManager = new PostManager();
        IForumPermissionManager _forumPermissionManager = new ForumPermissionManager();
        private SignInManager<IdentityUser> _signInManager;
        IThreadManager _threadManager = new ThreadManager();

        public PostsController(SignInManager<IdentityUser> signInManager)
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

        // GET: PostsController
        public ActionResult Index(int id, int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Volunteer", projectID) &&
                !_accessToken.HasProjectRole("Project Starter", projectID) &&
                !_accessToken.HasProjectRole("Volunteer Director", projectID) &&
                !_accessToken.HasProjectRole("Event Coordinator", projectID) &&
                !_accessToken.HasProjectRole("Moderator", projectID) &&
                !_accessToken.HasProjectRole("Scheduler", projectID) &&
                !_accessToken.HasProjectRole("Driver", projectID) &&
                !_accessToken.HasProjectRole("Accountant", projectID) &&
                !_accessToken.HasProjectRole("Purchaser", projectID) &&
                !_accessToken.HasProjectRole("Background Checker", projectID))
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.threadId = id;
            ViewBag.projectID = projectID;
            var posts = _postManager.GetAllThreadPosts(id);
            return View(posts);
        }

        // GET: PostsController/Create
        public ActionResult Create(int id, int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Volunteer", projectID) &&
                !_accessToken.HasProjectRole("Project Starter", projectID) &&
                !_accessToken.HasProjectRole("Volunteer Director", projectID) &&
                !_accessToken.HasProjectRole("Event Coordinator", projectID) &&
                !_accessToken.HasProjectRole("Moderator", projectID) &&
                !_accessToken.HasProjectRole("Scheduler", projectID) &&
                !_accessToken.HasProjectRole("Driver", projectID) &&
                !_accessToken.HasProjectRole("Accountant", projectID) &&
                !_accessToken.HasProjectRole("Purchaser", projectID) &&
                !_accessToken.HasProjectRole("Background Checker", projectID))
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (id > 0)
            {
                ViewBag.threadId = id;
            }
            ViewBag.projectID = projectID;

            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            getAccessToken();
            // TODO: Replace with login system
            post.UserID = _accessToken.UserID;

            if (ModelState.IsValid)
            {
                try
                {
                    if (!_forumPermissionManager.SelectUserWriteAccess(post.UserID, post.ProjectID))
                    {
                        ModelState.AddModelError("", "You do not have permission to post in this project forum.");
                        ViewBag.threadId = post.ThreadID;
                        return View(post);
                    }
                    IPostManager postManager = new PostManager();

                    _postManager.CreateForumPost(post);
                    TempData["AddSuccess"] = "Thread post created successfully!";
                    return RedirectToAction(nameof(Index), new { id = post.ThreadID, projectID = post.ProjectID });
                }
                catch (Exception ex)
                {
                    // Optionally log or show error
                    ModelState.AddModelError("", "Error creating post: " + ex.Message);
                    return View(post);
                }
            }
            ViewBag.ProjectID = post.ProjectID;
            return View(post);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/18
        /// Summary: Method to edit a post. 
        ///
        /// Last Updated By: Skyann Heintz
        /// Last Updated: 2025-04-28
        /// What Was Changed: Added  ViewBag.ThreadID = post.ThreadID; so that
        ///     the user to go back to the same list when hitting the back button on the view.
        /// </summary>
        public ActionResult Edit(int id, int projectID)
        {
            try
            {
                var post = _postManager.SelectPostByID(id);

                getAccessToken();
                if (!_accessToken.IsLoggedIn ||
                    _accessToken.UserID != post.UserID
                    )
                {
                    return RedirectToAction("AccessDenied", "Home");
                }

                if (post == null)
                {
                    return NotFound();
                }
                ViewBag.ThreadID = post.ThreadID;
                ViewBag.ProjectID = projectID;
                return View(post);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error loading post: " + ex.Message);
                return View();
            }

        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/18
        /// Summary: Method to edit a post. 
        ///
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (int postID, Post post, int projectID)
        {
            try
            {
                var updatePosts = new Post();

                updatePosts.PostID = postID;
                Post oldPost = _postManager.SelectPostByID(postID);

                if (ModelState.IsValid)
                {
                    bool updatedPosts = _postManager.EditForumPost(postID, oldPost.UserID, post.Content);
                    TempData["SuccessMessage"] = "Post updated successfully.";
                    return RedirectToAction(nameof(Index), new { id = oldPost.ThreadID, projectID = post.ProjectID });

                }
                else
                {
                    return View(post);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
                return View(post);
            }
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id, int projectID)
        {
            var post = _postManager.SelectPostByID(id); // You must retrieve the post to get threadID

            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Moderator", projectID) &&
                _accessToken.UserID != post.UserID)
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (_postManager.DeleteForumPost(id))
            {
                TempData["DeleteSuccess"] = "Post was deleted.";
                return RedirectToAction("Index", new { id = post.ThreadID, projectID = projectID });
            }
            else
            {
                return View("Error");
            }
        }

        // POST: PostsController/Delete/5
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

        // Author: Syler Bushlack
        // Last updated by: Dat Tran
        // Last updated: 2025-04-30
        // What was changed: Modified the method to work the same as the old code but to also work with the Sweet Alert pinning/unpinning. 
        public ActionResult Pin(int postID, int threadID, int projectID)
        {
            try
            {
                Post post = _postManager.SelectPostByID(postID);
                bool wasPinned = post.Pinned;

                getAccessToken();
                if (!_accessToken.IsLoggedIn ||
                    !_accessToken.HasProjectRole("Moderator", projectID)
                    )
                {
                    return RedirectToAction("AccessDenied", "Home");
                }

                // Toggle pinned status
                post.Pinned = !post.Pinned;

                bool success = _postManager.EditPostPinnedValue(post);
                if (success)
                {
                    if (post.Pinned)
                    {
                        TempData["Pinned"] = "Post was pinned.";
                    }
                    else
                    {
                        TempData["Unpinned"] = "Post was unpinned.";
                    }
                }

                return RedirectToAction("Index", new { id = threadID, projectID = projectID });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { id = threadID, projectID = projectID });
            }
        }
    }
}

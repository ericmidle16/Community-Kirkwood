/// <summary>
/// Stan Anderson
/// Created: 2025-04-16
/// 
/// The controller for Tasks
/// Last Updated By: Dat Tran
/// Last Updated: 2025-04-27
/// What was Changed: Added TempData for Sweet Alert usage.
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-05-01
/// What was Changed: Fixed edit task so that an error message shows when trying to name a task the same as an already existing task
/// </summary>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebPresentation.Controllers
{
    public class TaskController : Controller
    {
        private TaskManager _taskManager = new TaskManager();
        private UserManager _userManager = new UserManager();
        private EventManager _eventManager = new EventManager();
        private SignInManager<IdentityUser> _signInManager;

        public TaskController(SignInManager<IdentityUser> signInManager)
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

        // GET: TaskController/ViewEventTasks
        // Author: Stan Anderson
        public ActionResult ViewEventTasks(int eventID, string eventName, int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", projectID) &&
                !_accessToken.HasProjectRole("Volunteer", projectID)
                )
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.EventName = eventName;
            ViewBag.EventID = eventID;
            ViewBag.ProjectID = projectID;
            List<DataDomain.Task> tasks = _taskManager.GetTasksByEventID(eventID);
            return View(tasks);
        }

        // GET: TaskController/ViewEventVolunteers
        // Author: Stan Anderson
        public ActionResult ViewEventVolunteers(int eventID, string eventName, int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", projectID) &&
                !_accessToken.HasProjectRole("Volunteer", projectID)
                )
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.EventName = eventName;
            ViewBag.EventID = eventID;
            ViewBag.ProjectID = projectID;
            List<TaskAssignedViewModel> assignedVolunteers = _taskManager.GetVolunteersAndTasksByEventID(eventID);
            return View(assignedVolunteers);
        }

        // GET: TaskController/ViewEventVolunteers
        // Author: Stan Anderson
        public ActionResult ViewUnassignedVolunteers(int eventID, string eventName, int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", projectID) &&
                !_accessToken.HasProjectRole("Volunteer", projectID)
                )
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            ViewBag.EventName = eventName;
            ViewBag.EventID = eventID;
            ViewBag.ProjectID = projectID;

            List<User> users = _userManager.GetApprovedUserByProjectID(projectID);
            List<TaskAssignedViewModel> tasks = _taskManager.GetVolunteersAndTasksByEventID(eventID);
            List<User> availables = new List<User>();
            foreach (User user in users)
            {
                bool free = true;
                foreach (TaskAssignedViewModel task in tasks)
                {
                    if (task.UserID == user.UserID)
                    {
                        free = false;
                        break;
                    }
                }
                if (free)
                {
                    availables.Add(user);
                }
            }

            return View(availables);
        }

        // GET: TaskController/AssignTask
        // Author: Stan Anderson
        public ActionResult AssignTask(int eventID, string eventName, int projectID, int userID, string userName)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", projectID) &&
                !_accessToken.HasProjectRole("Volunteer Director", projectID)
                )
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.EventName = eventName;
            ViewBag.EventID = eventID;
            ViewBag.ProjectID = projectID;
            ViewBag.UserID = userID;
            ViewBag.UserName = userName;

            List<DataDomain.Task> tasks = _taskManager.GetTasksByEventID(eventID);
            ViewBag.Tasks = tasks;
            return View();
        }

        // POST: TaskController/AssignTask
        // Author: Stan Anderson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignTask(TaskAssigned t, int eventID, string eventName, int projectID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _taskManager.TaskAssignment(t.TaskID, t.UserID);
                    TempData["AssignSuccess"] = "Task assigned successfully!";
                    return RedirectToAction("ViewEventVolunteers", new { eventID = eventID, eventName = eventName, projectID = projectID });

                }
            }
            catch (Exception)
            {

            }

            return View(t);
        }

        // GET: TaskController/Details/5
        // Author: Josh Nicholson
        public ActionResult Details(int id)
        {

            Event @event = _eventManager.SelectEventByID(_taskManager.GetTaskByTaskID(id).EventID);
            ViewBag.Event = @event;
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", @event.ProjectID) &&
                !_accessToken.HasProjectRole("Volunteer", @event.ProjectID)
                )
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            return View(_taskManager.GetTaskByTaskID(id));
        }

        // GET: TaskController/Create
        // Author: Josh Nicholson
        public ActionResult Create(int eventID)
        {

            Event @event = _eventManager.SelectEventByID(eventID);
            ViewBag.Event = @event;
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", @event.ProjectID) &&
                !_accessToken.HasProjectRole("Event Coordinator", @event.ProjectID) &&
                !_accessToken.HasProjectRole("Volunteer Director", @event.ProjectID)
                )
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            popTaskTypes();
            return View();
        }

        // POST: TaskController/Create
        // Author: Josh Nicholson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DataDomain.Task task, int eventID)
        {
            Event currentEvent = _eventManager.SelectEventByID(eventID);
            ViewBag.Event = currentEvent;

            task.ProjectID = currentEvent.ProjectID;
            task.EventID = eventID;

            try
            {
                if (!string.IsNullOrEmpty(task.Name) && task.Name.Length > 100)
                {
                    ModelState.AddModelError("Name", "The Name field is too long.");
                }

                if (!string.IsNullOrEmpty(task.Description) && task.Description.Length > 250)
                {
                    ModelState.AddModelError("Description", "The Description field is too long.");
                }

                if (ModelState.IsValid)
                {
                    _taskManager.AddTask(task);
                    TempData["TaskAdded"] = "Task successfully added to event.";
                    return RedirectToAction("ViewEventTasks", "Task", new { eventID = eventID, eventName = currentEvent.Name, projectID = currentEvent.ProjectID });
                }
                else
                {
                    popTaskTypes();
                    return View(task);
                }
            }
            catch
            {
                popTaskTypes();
                return View();
            }
        }

        // GET: TaskController/Edit/5
        // Author: Josh Nicholson
        public ActionResult Edit(int id)
        {
            Event @event = _eventManager.SelectEventByID(_taskManager.GetTaskByTaskID(id).EventID);
            ViewBag.Event = @event;
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Project Starter", @event.ProjectID) &&
                !_accessToken.HasProjectRole("Event Coordinator", @event.ProjectID) &&
                !_accessToken.HasProjectRole("Volunteer Director", @event.ProjectID)
                )
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            popTaskTypes();
            return View(_taskManager.GetTaskByTaskID(id));
        }

        // POST: TaskController/Edit/5
        // Author: Josh Nicholson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DataDomain.Task task)
        {
            List<DataDomain.Task> tasks =  _taskManager.GetTasksByProjectID(task.ProjectID);
            Event currentEvent = _eventManager.SelectEventByID(_taskManager.GetTaskByTaskID(id).EventID);
            ViewBag.Event = currentEvent;

            try
            {
                task.TaskID = id;

                if (!string.IsNullOrEmpty(task.Name) && task.Name.Length > 100)
                {
                    ModelState.AddModelError("Name", "The Name field is too long.");
                }

                foreach (DataDomain.Task t in tasks)
                {
                    if (t.TaskID != task.TaskID && t.Name.Equals(task.Name))
                    {
                        ModelState.AddModelError("Name", "A task with that name already exists.");
                    }
                }

                if (!string.IsNullOrEmpty(task.Description) && task.Description.Length > 250)
                {
                    ModelState.AddModelError("Description", "The Description field is too long.");
                }

                if (ModelState.IsValid)
                {
                    _taskManager.UpdateTaskByTaskID(task);
                    TempData["TaskUpdated"] = "Task successfully updated.";
                    return RedirectToAction("ViewEventTasks", "Task", new { eventID = currentEvent.EventID, eventName = currentEvent.Name, projectID = currentEvent.ProjectID });
                }
                else
                {
                    popTaskTypes();
                    return View(task);
                }
            }
            catch
            {
                popTaskTypes();
                return View();
            }
        }

        // GET: TaskController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: TaskController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        /// <summary>
        /// Creator:  Josh Nicholson
        /// Created:  2025/04/24
        /// Summary:  A helper method to feed lists into the create Task dropdowns
        /// </summary>
        private void popTaskTypes()
        {
            List<TaskTypeObject> taskType = _taskManager.GetAllTaskTypes();
            List<string> taskNames = new List<string>();
            foreach (TaskTypeObject tasktype in taskType)
            {
                taskNames.Add(tasktype.TaskType);
            }
            ViewBag.TaskTypes = taskNames;
        }
    }
}

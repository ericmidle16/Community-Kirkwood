/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/04/05
/// Summary:  The controller for projects
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-11
/// What Was Changed:
///     I changed our _projectManager instance variable to IProjectManager, so
///     that we are coding to the interface.
///     I added implementation for the viewing details of a single project.
/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/04/05
/// Summary:  The controller for projects
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-11
/// What Was Changed:
///     I changed our _projectManager instance variable to IProjectManager, so
///     that we are coding to the interface.
///     I added implementation for the viewing details of a single project.
///     
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025-04-21
/// What Was Changed:
///     I added my methods for leaving a project and the variables I needed
///     
/// Last Updated By: Christivie Mauwa
/// Last Updated: 2025-04-24
/// What Was Changed:
///     I added my methods for edit a project 
/// 
/// Last Updated By: Dat Tran
/// Last Updated: 2025-04-24
/// What was changed: Added TempData to use with Sweet Alerts.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed:
///     Added automation logic for when a new Project is created:
///         Pull the newly inserted ProjectID from the AddProject() method.
///         Use that newProjectID to create ForumPermission record with WriteAccess set to true for the User who is logged in & created the new Project.
///         Create a Welcome Thread & Post in the Project Forum.
///         Create an Approved VolunteerStatus record for the User who is logged in & created the new Project.
///         Assign all existing ProjectRoles to the User who is logged in & created the new Project.
/// </summary>

using DataDomain;
using LogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace WebPresentation.Controllers
{
    public class ProjectController : Controller
    {
        IProjectManager _projectManager = new ProjectManager();
        ILocationManager _locationManager = new LocationManager();
        IVolunteerStatusProjectRoleManager _volunteerStatusProjectRoleManager = new VolunteerStatusProjectRoleManager();
        IProjectRoleManager _projectRoleManager = new ProjectRoleManager();
        IThreadManager _threadManager = new ThreadManager();
        IForumPermissionManager _forumPermissionManager = new ForumPermissionManager();


        List<VolunteerStatusProjectRole> _volunteerRoles = new List<VolunteerStatusProjectRole>();
        IUserManager _userManager = new UserManager();
        VolunteerStatus _volunteerStatus = new VolunteerStatus();
        IVolunteerStatusManager _volunteerStatusManager = new VolunteerStatusManager();
        DataDomain.Project _project = new DataDomain.Project();
        Event _event = null;

        IDonationManager _donationManager = new DonationManager();
        INotificationManager _notificationManager = new NotificationManager();
        IEventManager _eventManager = new EventManager();

        private Models.AccessToken _accessToken = new Models.AccessToken("");
        private SignInManager<IdentityUser> _signInManager;

        public ProjectController(SignInManager<IdentityUser> signInManager)
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

        // GET: ProjectController
        public ActionResult Index(string sortOrder, string searchString)
        {
            getAccessToken();
            ViewBag.ActiveUserID = _accessToken.UserID;

            var projects = _projectManager.GetAllProjects();
            projects = ProjectListFilters(projects, sortOrder, searchString);

            return View(projects);
        }

        // GET: ProjectController
        [Authorize]
        public ActionResult IndexStartedProjects(string sortOrder, string searchString, int id)
        {
            getAccessToken();
            ViewBag.ActiveUserID = _accessToken.UserID;

            var projects = _projectManager.GetAllProjectVMsByUserID(id);
            projects = ProjectListFilters(projects, sortOrder, searchString);

            return View(projects);
        }

        /// <summary>
        /// Creator:  Syler Bushlack
        /// Created:  2025/04/11
        /// Summary:  Filter logic for project lists.
        ///             Accepts a List<ProjectVM>, string sortOrder, and string searchString
        ///             Sorts the project list in asc or desc order based on the sortOrder value
        ///             returns the sorted list of ProjectVM objects
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        public List<ProjectVM> ProjectListFilters(List<ProjectVM> projects, string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.ProjectNameParm = sortOrder == "ProjectName" ? "ProjectNameDesc" : "ProjectName";
            ViewBag.ProjectType = sortOrder == "ProjectType" ? "ProjectTypeDesc" : "ProjectType";
            ViewBag.Location = sortOrder == "Location" ? "LocationDesc" : "Location";
            ViewBag.Address = sortOrder == "Address" ? "AddressDesc" : "Address";
            ViewBag.Zip = sortOrder == "Zip" ? "ZipDesc" : "Zip";
            ViewBag.City = sortOrder == "City" ? "CityDesc" : "City";
            ViewBag.City = sortOrder == "State" ? "StateDesc" : "State";
            ViewBag.Status = sortOrder == "Status" ? "StatusDesc" : "Status";

            if(!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(s => s.Name.Contains(searchString)
                                       || s.Name.Contains(searchString)).Cast<ProjectVM>().ToList();
            }

            switch(sortOrder)
            {
                case "name_desc":
                    projects = projects.OrderByDescending(s => s.GivenName).Cast<ProjectVM>().ToList();
                    break;
                case "Date":
                    projects = projects.OrderBy(s => s.StartDate).Cast<ProjectVM>().ToList();
                    break;
                case "date_desc":
                    projects = projects.OrderByDescending(s => s.StartDate).Cast<ProjectVM>().ToList();
                    break;
                case "ProjectName":
                    projects = projects.OrderBy(s => s.Name).Cast<ProjectVM>().ToList();
                    break;
                case "ProjectNameDesc":
                    projects = projects.OrderByDescending(s => s.Name).Cast<ProjectVM>().ToList();
                    break;
                case "ProjectType":
                    projects = projects.OrderBy(s => s.ProjectTypeID).Cast<ProjectVM>().ToList();
                    break;
                case "ProjectTypeDesc":
                    projects = projects.OrderByDescending(s => s.ProjectTypeID).Cast<ProjectVM>().ToList();
                    break;
                case "Location":
                    projects = projects.OrderBy(s => s.LocationName).Cast<ProjectVM>().ToList();
                    break;
                case "LocationDesc":
                    projects = projects.OrderByDescending(s => s.LocationName).Cast<ProjectVM>().ToList();
                    break;
                case "Address":
                    projects = projects.OrderBy(s => s.Address).Cast<ProjectVM>().ToList();
                    break;
                case "AddressDesc":
                    projects = projects.OrderByDescending(s => s.Address).Cast<ProjectVM>().ToList();
                    break;
                case "Zip":
                    projects = projects.OrderBy(s => s.Zip).Cast<ProjectVM>().ToList();
                    break;
                case "ZipDesc":
                    projects = projects.OrderByDescending(s => s.Zip).Cast<ProjectVM>().ToList();
                    break;
                case "City":
                    projects = projects.OrderBy(s => s.City).Cast<ProjectVM>().ToList();
                    break;
                case "CityDesc":
                    projects = projects.OrderByDescending(s => s.City).Cast<ProjectVM>().ToList();
                    break;
                case "State":
                    projects = projects.OrderBy(s => s.State).Cast<ProjectVM>().ToList();
                    break;
                case "StateDesc":
                    projects = projects.OrderByDescending(s => s.State).Cast<ProjectVM>().ToList();
                    break;
                case "Status":
                    projects = projects.OrderBy(s => s.Status).Cast<ProjectVM>().ToList();
                    break;
                case "StatusDesc":
                    projects = projects.OrderByDescending(s => s.Status).Cast<ProjectVM>().ToList();
                    break;
                default:
                    projects = projects.OrderBy(s => s.GivenName).Cast<ProjectVM>().ToList();
                    break;
            }
            return projects;
        }

        // GET: ProjectController/Details/5
        // Author: Kate Rich
        public ActionResult Details(int id)
        {
            getAccessToken();
            ViewBag.ActiveUserID = _accessToken.UserID;

            var project = _projectManager.GetProjectInformationByProjectID(id);
            VolunteerStatus status = _volunteerStatusManager.GetVolunteerStatusByUserIDAndProjectID(_accessToken.UserID, id);

            if(status == null)
            {
                ViewBag.IsVolunteer = false;
                ViewBag.HasOffer = false;
            }
            else
            {
                ViewBag.HasOffer = true;
                // View bag for if the user sees the leave project button or join project button
                if (status.Approved == true)
                {
                    ViewBag.IsVolunteer = true;
                }
                else
                {
                    ViewBag.IsVolunteer = false;
                }
            }

            return View(project);
        }

        // GET: ProjectController/Create
        // Author: Josh Nicholson
        // Updated by: Dat Tran
        // Updated: 2025-04-26
        // What was changed: Added TempData for Sweet Alert message when project is created. 
        [Authorize]
        public ActionResult Create()
        {
            popDropdowns();
            return View();
        }

        // POST: ProjectController/Create
        // Author: Josh Nicholson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DataDomain.Project project, IFormCollection form)
        {
            try
            {

                getAccessToken();
                string selectedLocationName = form["SelectedLocationName"];

                project.UserID = _accessToken.UserID;

                List<DataDomain.Location> locations = _locationManager.ViewLocationList();
                foreach(DataDomain.Location location in locations)
                {
                    if(selectedLocationName == location.Name)
                    {
                        project.LocationID = location.LocationID;
                        break;
                    }
                }

                if(!string.IsNullOrEmpty(project.Name) && project.Name.Length > 50)
                {
                    ModelState.AddModelError("Name", "The Name field is too long.");
                }

                if(!string.IsNullOrEmpty(project.Description) && project.Description.Length > 250)
                {
                    ModelState.AddModelError("Description", "The Description field is too long.");
                }

                if(project.AcceptsDonations == true)
                {
                    if(string.IsNullOrEmpty(project.PayPalAccount))
                    {
                        ModelState.AddModelError("PayPalAccount", "The PayPalAccount field needs a valid email address.");
                    }
                    else if(project.PayPalAccount.Length > 250)
                    {
                        ModelState.AddModelError("PayPalAccount", "The PayPalAccount field email is too long.");
                    }
                }

                if(project.AcceptsDonations == false && !string.IsNullOrEmpty(project.PayPalAccount))
                {
                    ModelState.AddModelError("PayPalAccount", "The AcceptsDonations field must be true to use an email.");
                }

                if(ModelState.IsValid)
                {
                    // Get the new Project ID.
                    int newProjectID = _projectManager.AddProject(project);

                    // Create ForumPermission record for the Project Starter
                    ForumPermission newForumPermission = new ForumPermission()
                    {
                        UserID = _accessToken.UserID,
                        ProjectID = newProjectID,
                        WriteAccess = true
                    };
                    _forumPermissionManager.AddForumPermission(newForumPermission);

                    // Create Project Forum - First Thread & Post
                    _threadManager.InsertForumPost(_accessToken.UserID, "Welcome to the Project!", newProjectID, "Welcome!!", DateTime.Now);

                    // Create VolunteerStatus record for Project Starter & set Approved to true.
                    _volunteerStatusManager.AddVolunteerStatus(_accessToken.UserID, newProjectID);
                    VMVolunteerStatus newVolunteerStatus = new VMVolunteerStatus()
                    {
                        UserID = _accessToken.UserID,
                        ProjectID = newProjectID,
                        Approved = true
                    };
                    _volunteerStatusManager.UpdateVolunteerStatus(newVolunteerStatus);

                    // Get all exisitng ProjectRoles.
                    List<ProjectRole> projectRoles = _projectRoleManager.GetAllProjectRoles();
                    // Create VolunteerStatusProjectRole records (assign the Project Starter ALL OF THEM)
                    foreach(ProjectRole projectRole in projectRoles)
                    {
                        _volunteerStatusProjectRoleManager.InsertUserProjectRole(_accessToken.UserID, newProjectID, projectRole.ProjectRoleID);
                    }

                    TempData["SuccessMessage"] = "Project successfully created!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    popDropdowns();
                    return View(project);
                }
            }
            catch
            {
                popDropdowns();
                return View();
            }
        }

        // Author: christivie Mauwa
        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            var projectID = _projectManager.GetProjectVMByID(id);
            getAccessToken();
            if(!_accessToken.IsLoggedIn ||
                _accessToken.UserID != projectID.UserID)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            ViewBag.Locations = new SelectList(_locationManager.ViewLocationList(), "LocationID", "Name", projectID.LocationID);

            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Not Started", Text = "Not Started" },
                new SelectListItem { Value = "In Progress", Text = "In Progress" },
                new SelectListItem { Value = "Completed", Text = "Completed" }
            };
            return View( projectID);
        }

        // Author: Christivie
        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectVM project)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var actulaProject = _projectManager.GetProjectVMByID(project.ProjectID);
                    bool newProject = _projectManager.EditProject(actulaProject, project);

                    if(newProject)
                    {
                        TempData["UpdateSuccess"] = "Project updated successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update failed.");
                        return View();
                    }
                }
                catch
                {
                    return View();
                }
            }
            return View(project);
        }

        /// <summary>
        /// Creator:  Ellie Wacker
        /// Created:  2025/04/17
        /// Summary:  The get method for leave project. I styled it similarly to a delete page.
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        // GET: ProjectController/Leave/5
        [Authorize]
        public ActionResult Leave(int id, int userID)
        {
            ViewBag.UserID = userID;
            var project = _projectManager.GetProjectInformationByProjectID(id);
            return View(project);
        }

        /// <summary>
        /// Creator:  Ellie Wacker
        /// Created:  2025/04/17
        /// Summary:  The post method for leave project. The user's project roles are deleted if they have any and
        ///     they are taken off the project
        /// Last Updated By:
        /// Last Updated: 
        /// What was Changed: 	
        /// </summary>
        // POST: ProjectController/Leave/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Leave(int id, ProjectVM projectvm, int userID)
        {
            var project = _projectManager.GetProjectInformationByProjectID(id);

            try
            {
                try
                {
                    // If the user has any roles
                    _volunteerRoles = _volunteerStatusProjectRoleManager.GetUserProjectRolesByUserIDProjectID(userID, id);
                }
                catch (Exception ex)
                {
                    _volunteerRoles = new List<VolunteerStatusProjectRole>(); // Avoid null reference
                }
                if(_volunteerRoles.Any())
                {
                    // User has roles so they can leave
                    _volunteerStatusProjectRoleManager.DeleteUserRoles(userID, id);
                    ForumPermission newForumPermission = new ForumPermission()
                    {
                        UserID = userID,
                        ProjectID = id,
                        WriteAccess = false
                    };
                    _forumPermissionManager.EditForumPermissionWriteAccessValue(newForumPermission);
                }
                int result = _volunteerStatusManager.DeactivateVolunteerByUserIDAndProjectID(userID, id);
                
                Console.WriteLine(result);
                if(result > 0)
                {
                    TempData["SuccessMessage"] = "You have successfully left the project!";
                    return RedirectToAction(nameof(Index)); // go back to the projects page
                }
                else
                {
                    // If result is 0 stay on same page
                    TempData["ErrorMessage"] = "Leave project failed.";
                    return RedirectToAction("Leave", new { id = id, userID = userID });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["ErrorMessage"] = "Leave project failed.";
                return RedirectToAction("Leave", new { id = id, userID = userID });
            }
        }

        /// <summary>
        /// Creator:  Josh Nicholson
        /// Created:  2025/04/24
        /// Summary:  A helper method to feed lists into the create project dropdowns
        /// </summary>
        private void popDropdowns()
        {
            List<ProjectTypeObject> projectType = _projectManager.GetAllProjectTypes();
            List<string> projectTypes = new List<string>();
            foreach(ProjectTypeObject projecttype in projectType)
            {
                projectTypes.Add(projecttype.ProjectType);
            }
            ViewBag.ProjectTypes = projectTypes;

            List<string> projectStatus = new List<string>();
            projectStatus.Add("Not Started");
            projectStatus.Add("In Progress");
            ViewBag.ProjectStatus = projectStatus;

            List<DataDomain.Location> locations = _locationManager.ViewLocationList();
            List<string> locationNames = new List<string>();
            foreach(DataDomain.Location location in locations)
            {
                locationNames.Add(location.Name);
            }
            ViewBag.Locations = locationNames;
        }

        //Author: Akoi Kollie
        // GET: ProjectController/Edit/5
        public ActionResult RequestVolunteer(int eventID)
        {
            ViewBag.EventID = eventID;
            Notification notification = new Notification();
            notification.Date = DateTime.Now;
            return View(notification);
        }

        //Author: Akoi Kollie
        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestVolunteer(Notification notification, int eventID)
        {
            getAccessToken();

            string name = notification.Name;
            string content = notification.Content;
            DateTime date = (DateTime)notification.Date;

            bool important = notification.Important;

            try
            {
                _event = _eventManager.SelectEventByID(eventID);
                List<VolunteerStatus> volunteers = _volunteerStatusManager.SelectVolunteerStatusByProjectID(_event.ProjectID);

                foreach(VolunteerStatus volunteerStatus in volunteers)
                {
                    notification.Name = name;
                    notification.Content = content;
                    notification.Date = date;
                    notification.Sender = _accessToken.UserID;
                    notification.Receiver = volunteerStatus.UserID;
                    notification.Important = important;
                    notification.IsViewed = false;
                    _notificationManager.InsertNotification(notification);
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error", ex);
                return View();
            }
        }

        //Author: Akoi Kollie
        public ActionResult DonationIndex(int projectid)
        {
            getAccessToken();
            if(!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Accountant", projectid) &&
                !_accessToken.HasProjectRole("Project Starter", projectid))
                )
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            var donations = _donationManager.SelectToViewDonations(projectid);
            return View(donations);
        }

        //Author: Akoi Kollie
        // GET: ProjectController/Create
        [Authorize]
        public ActionResult JoinProject(int userID,int projectID)
        {
            List<VMVolunteerStatus> volunteerStatus = null;
            bool exist = (_volunteerStatusManager.GetVolunteerStatusByUserIDAndProjectID(userID, projectID) != null);
            if (exist)
            {
                string message = "You have already applied to join this project!";
                ViewBag.ErrorMessage = message;
                return RedirectToAction("Error", "Home");
            }

            var row = _volunteerStatusManager.AddVolunteerStatus(userID, projectID);
            return RedirectToAction(nameof(Index));
        }
    }
}
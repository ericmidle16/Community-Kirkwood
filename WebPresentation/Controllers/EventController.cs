/// <summary>
/// Creator: Yousif Omer
/// Created: 2025-04-04
/// Summary:
///     The controller for Events
/// 
/// Updated By: Stan Anderson
/// Updated: 2025/04/17
/// What was Changed: Updated index to allow project specific
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-04-25
/// What Was Changed:
///     Added logic to the GET version of the Create() method, so that a user can only
///     create an event if they go to create one from a specific project's event list.
/// </summary>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicLayer;
using DataDomain;
using Microsoft.AspNetCore.Identity;

namespace WebPresentation.Controllers
{
    public class EventController : Controller
    {
        private IEventManager _eventManager = new EventManager();
        private SignInManager<IdentityUser> _signInManager;

        public EventController(SignInManager<IdentityUser> signInManager)
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

        // GET: EventController
        public ActionResult Index(int id)
        {
            ViewBag.Title = "Events";
            ViewBag.ProjectID = id;
            var events = new List<Event>();
            if(id != 0)
            {
                events = _eventManager.ViewEventListByProjectID(id);
            }
            else
            {
                events = _eventManager.ViewEventList();
            }
            return View(events);
        }


        // GET: EventController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Event Details";
            var anevent = _eventManager.SelectEventByID(id);
            return View(anevent);
           
        }

        private LocationManager _locationManager = new LocationManager();
        private ProjectManager _projectManager = new ProjectManager();

        // GET: EventController/Create
        public ActionResult Create(int projectID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Event Coordinator", projectID) &&
                !_accessToken.HasProjectRole("Project Starter", projectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (projectID == 0)
            {
                // Must be viewing a specific project's events in order to create a new one!

                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = projectID;

            try
            {
                // Load locations for dropdown
                var locations = _locationManager.ViewLocationList()
                    .OrderBy(loc => loc.Name)
                    .ToList();
                ViewBag.Locations = locations;

                // Load event types for dropdown
                var eventTypes = _eventManager.SelectEventTypes()
                    .OrderBy(et => et.EventTypeID)
                    .ToList();
                ViewBag.EventTypes = eventTypes;

                // Create a new Event with current date for start and end dates
                var newEvent = new Event
                {
                    ProjectID = projectID,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today
                };

                return View(newEvent);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Failed to load data: " + ex.Message;
                return RedirectToAction("Index", new { id = projectID });
            }
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event newEvent, IFormFile imageFile, string startTime, string endTime, string startAMPM, string endAMPM)
        {
            try
            {
                // Always load dropdown data first to ensure it's available
                var locations = _locationManager.ViewLocationList()
                    .OrderBy(loc => loc.Name)
                    .ToList();
                ViewBag.Locations = locations;

                var eventTypes = _eventManager.SelectEventTypes()
                    .OrderBy(et => et.EventTypeID)
                    .ToList();
                ViewBag.EventTypes = eventTypes;

                ViewBag.ProjectID = newEvent.ProjectID;

                newEvent.UserID = 100000; // Temporary solution

                // Validate input - only validate required fields
                if (string.IsNullOrEmpty(newEvent.Name) || newEvent.Name.Length > 50)
                {
                    ModelState.AddModelError("Name", "Invalid Event Name (Empty or over 50 characters)");
                    return View(newEvent);
                }

                if (string.IsNullOrEmpty(newEvent.EventTypeID))
                {
                    ModelState.AddModelError("EventTypeID", "You must select an Event Type");
                    return View(newEvent);
                }

                if (newEvent.LocationID <= 0)
                {
                    ModelState.AddModelError("LocationID", "You must select a Location");
                    return View(newEvent);
                }

                if (newEvent.VolunteersNeeded < 0)
                {
                    ModelState.AddModelError("VolunteersNeeded", "Volunteers Needed cannot be negative");
                    return View(newEvent);
                }

                // Process start time
                if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(startAMPM))
                {
                    if (int.TryParse(startTime, out int startHour))
                    {
                        // Convert to 24-hour format
                        if (startAMPM == "PM" && startHour < 12)
                            startHour += 12;
                        else if (startAMPM == "AM" && startHour == 12)
                            startHour = 0;

                        newEvent.StartDate = new DateTime(
                            newEvent.StartDate.Year,
                            newEvent.StartDate.Month,
                            newEvent.StartDate.Day,
                            startHour,
                            0,
                            0
                        );
                    }
                    else
                    {
                        ModelState.AddModelError("StartDate", "Invalid start time format");
                        return View(newEvent);
                    }
                }

                // Process end time
                if (!string.IsNullOrEmpty(endTime) && !string.IsNullOrEmpty(endAMPM))
                {
                    if (int.TryParse(endTime, out int endHour))
                    {
                        // Convert to 24-hour format
                        if (endAMPM == "PM" && endHour < 12)
                            endHour += 12;
                        else if (endAMPM == "AM" && endHour == 12)
                            endHour = 0;

                        newEvent.EndDate = new DateTime(
                            newEvent.EndDate.Year,
                            newEvent.EndDate.Month,
                            newEvent.EndDate.Day,
                            endHour,
                            0,
                            0
                        );
                    }
                    else
                    {
                        ModelState.AddModelError("EndDate", "Invalid end time format");
                        return View(newEvent);
                    }
                }

                if (newEvent.EndDate < newEvent.StartDate)
                {
                    ModelState.AddModelError("EndDate", "Event End date/time cannot be before Start date/time");
                    return View(newEvent);
                }


                // Set empty description/notes to null or empty string to ensure they aren't required
                if (newEvent.Description == null)
                {
                    newEvent.Description = "";
                }

                if (newEvent.Notes == null)
                {
                    newEvent.Notes = "";
                }

                // Insert the event
                bool result = _eventManager.InsertEvent(newEvent);
                if (result)
                {
                    TempData["SuccessMessage"] = "Event created successfully!";
                    return RedirectToAction("Index", new { id = newEvent.ProjectID });
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create event. Please check all fields and try again.");
                    return View(newEvent);
                }
            }
            catch (Exception ex)
            {
                // Reload dropdown data on error
                var locations = _locationManager.ViewLocationList()
                    .OrderBy(loc => loc.Name)
                    .ToList();
                ViewBag.Locations = locations;

                var eventTypes = _eventManager.SelectEventTypes()
                    .OrderBy(et => et.EventTypeID)
                    .ToList();
                ViewBag.EventTypes = eventTypes;

                // Ensure ProjectID is in ViewBag
                ViewBag.ProjectID = newEvent.ProjectID;

                ModelState.AddModelError("", "Error creating event: " + ex.Message);
                if (ex.InnerException != null)
                {
                    ModelState.AddModelError("", "Inner exception: " + ex.InnerException.Message);
                }
                return View(newEvent);
            }
        }

        // GET: EventController/Edit/5
        public ActionResult Edit(int id)
        {
            Event @event = _eventManager.SelectEventByID(id);
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Event Coordinator", @event.ProjectID) &&
                !_accessToken.HasProjectRole("Project Starter", @event.ProjectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var eventTypes = _eventManager.SelectEventTypes()
                    .OrderBy(et => et.EventTypeID)
                    .ToList();

            List<string> types = new List<string>();
            foreach(var type in eventTypes)
            {
                types.Add(type.EventTypeID);
            }
            ViewBag.EventTypes = types;

            Event anevent = _eventManager.SelectEventByID(id);
            return View(anevent);
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Event @event)
        {
            var eventTypes = _eventManager.SelectEventTypes()
                    .OrderBy(et => et.EventTypeID)
                    .ToList();

            List<string> types = new List<string>();
            foreach(var type in eventTypes)
            {
                types.Add(type.EventTypeID);
            }
            ViewBag.EventTypes = types;

            try
            {
                Event originEvent = _eventManager.SelectEventByID(id);
                _eventManager.EditEvent(originEvent,@event);
                TempData["SuccessMessage"] = "Event updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            Event @event = _eventManager.SelectEventByID(id);
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Event Coordinator", @event.ProjectID) &&
                !_accessToken.HasProjectRole("Project Starter", @event.ProjectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            Event anEvent = _eventManager.SelectEventByID(id);
            return View(anEvent);
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Event @event = _eventManager.SelectEventByID(id);
            getAccessToken();
            if (!_accessToken.IsLoggedIn ||
                (!_accessToken.HasProjectRole("Event Coordinator", @event.ProjectID) &&
                !_accessToken.HasProjectRole("Project Starter", @event.ProjectID)
                ))
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                Event anEvent = _eventManager.SelectEventByID(id);
                var toDelete = _eventManager.DeactivateEventById(id);
                ViewBag.Title = "Delete Event";

                TempData["DeleteSuccess"] = "Event successfully deleted.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
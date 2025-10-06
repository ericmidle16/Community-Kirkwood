/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/23
/// Summary: Controller class that handles all code that deals with Availability
/// in the webform.
/// Last Upaded By:
/// Last Updated:
/// What Was Changed:
/// </summary>
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicLayer;
using DataDomain;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebPresentation.Controllers
{
    public class AvailabilityController : Controller
    {
        private AvailabilityManager _availabilityManager = new AvailabilityManager();
        private SignInManager<IdentityUser> _signInManager;

        public AvailabilityController(SignInManager<IdentityUser> signInManager)
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
        /// Creator:Skyann Heintz
        /// Created: 2025/03/23
        /// Summary: ActionResult that gets the signed in user's 
        /// Availability if they have any. It will also display 
        /// a ViewBag Message if there isn't any avaiblity to show 
        /// for the given user
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [Authorize]
        public ActionResult Availability(int userID)
        {
            // use 100000 to show the list works
            // use 1000001 to show the viewbag message
            getAccessToken();
            userID = _accessToken.UserID; // setting this for now until we get login system

            if (!_accessToken.IsLoggedIn)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            var availability = _availabilityManager.SelectAvailabilityByUser(userID);

            // ViewBag message will show if avaiblity is null or their is none
            if (availability == null || !availability.Any())
            {
                ViewBag.Message = "No availability at this time. Please add availability.";
            }

            return View(availability);
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/03/28
        /// Summary: ActionResult that handles the Add Avilability function 
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [Authorize]
        public ActionResult AddAvailability(int userID)
        {
            getAccessToken();
            userID = _accessToken.UserID; // Setting a temporary user ID 

            if (!_accessToken.IsLoggedIn)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var availability = new Availability();
            availability.UserID = userID;
            availability.StartDate = new DateTime(2025, 1, 1, 7, 0, 0);
            availability.EndDate = new DateTime(2025, 1, 1, 15, 0, 0); //3pm
            return View(availability);
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/03/28
        /// Summary: ActionResult that handles the AddAvaibility.
        /// It checks if the date already exists if it does when the user submits it will throw the 
        /// view back with an error message stating it exists. It checks that the end date is not before the start date.
        /// It checks the start time is not in the past and the end time is not in the past or before start time.
        /// If the user checks avaible all day it sets the time to 7 am and 3pm for them even if they select times.
        /// If the user checks repeats weekly the days and time will repeat for the next 4 weeks.
        /// Last Updated By: Dat Tran
        /// Last Updated: 2025-04-27
        /// What Was Changed: Fixed the if(!isAvailable) code block to make sure it checks both time AND date at the same time, not time and 
        /// date separately. 
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAvailability(int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate)
        {
            try
            {
                // Checks if the availability already exists
                bool exists = _availabilityManager.SelectExistingAvailability(userID, startDate, endDate);

                if (exists)
                {
                    ModelState.AddModelError("", "You have already inserted your availability for this date. Please choose a different date.");
                    return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                }

                if (isAvailable)
                {
                    // If the user is available all day, set start time to 7 AM and end time to 3 PM
                    startDate = startDate.Date.AddHours(7);  // Set start time to 7 AM
                    endDate = endDate.Date.AddHours(15);     // Set end time to 3 PM
                }

                if (!isAvailable)
                {
                    if (startDate < DateTime.Now)
                    {
                        ModelState.AddModelError("StartDate", "Start time cannot be in the past.");
                        return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                    }
                    if (endDate < DateTime.Now)
                    {
                        ModelState.AddModelError("EndDate", "End time cannot be in the past.");
                        return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                    }
                }

                // Date Time validations
                if (startDate.Date < DateTime.Now.Date)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be in the past.");
                    return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                }

                if (endDate < DateTime.Now.Date)
                {
                    ModelState.AddModelError("EndDate", "End date cannot be in the past.");
                    return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                }



                if (startDate.TimeOfDay > endDate.TimeOfDay)
                {
                    ModelState.AddModelError("EndDate", "End time cannot be before the start time.");
                    return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                }


                if (startDate > endDate)
                {
                    ModelState.AddModelError("EndDate", "End date cannot be before the start date.");
                    return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                }

                DateTime currentInsertDate = startDate;

                // Checks for availability on each day in the range and insert if it does not already exist
                while (currentInsertDate <= endDate)
                {
                    DateTime availabilityStartDate = currentInsertDate.Date.Add(startDate.TimeOfDay);
                    DateTime availabilityEndDate = currentInsertDate.Date.Add(endDate.TimeOfDay);

                    if (_availabilityManager.SelectExistingAvailability(userID, availabilityStartDate, availabilityEndDate))
                    {
                        ModelState.AddModelError("", $"You have already inserted your availability for {currentInsertDate.ToShortDateString()}. Please choose different date(s).");
                        return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                    }

                    bool isInsertedSuccessfully = _availabilityManager.InsertAvailability(userID, isAvailable, repeatWeekly, availabilityStartDate, availabilityEndDate);
                    TempData["AddSuccess"] = "Availability inserted successfully.";
                    if (!isInsertedSuccessfully)
                    {
                        ModelState.AddModelError("", $"Failed to insert availability on {currentInsertDate.ToShortDateString()}.");
                        return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                    }

                    // If repeatWeekly is selected, insert for the next 3 weeks
                    if (repeatWeekly)
                    {
                        for (int i = 1; i <= 3; i++)
                        {
                            DateTime newStartDate = availabilityStartDate.AddDays(7 * i);
                            DateTime newEndDate = availabilityEndDate.AddDays(7 * i);

                            if (_availabilityManager.SelectExistingAvailability(userID, newStartDate, newEndDate))
                            {
                                ModelState.AddModelError("", $"You have already inserted your availability for {newStartDate.ToShortDateString()}. Please choose different date(s).");
                                return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
                            }

                            _availabilityManager.InsertAvailability(userID, isAvailable, repeatWeekly, newStartDate, newEndDate);
                        }
                    }

                    currentInsertDate = currentInsertDate.AddDays(1); // Move to the next day
                }

                return RedirectToAction(nameof(Availability));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request: " + ex.Message);
                return View(new Availability { UserID = userID, IsAvailable = isAvailable, RepeatWeekly = repeatWeekly, StartDate = startDate, EndDate = endDate });
            }
        }
        // GET: Availability/Update
        [Authorize]
        public ActionResult Update(int availabilityID)
        {
            getAccessToken();
            if (!_accessToken.IsLoggedIn)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            try
            {
                // Get the availability entry by ID
                var availabilityList = _availabilityManager.SelectAvailabilityByUser(_accessToken.UserID); // Using hardcoded user ID as in your existing code
                var availability = availabilityList.FirstOrDefault(a => a.AvailabilityID == availabilityID);

                if (availability == null)
                {
                    ViewBag.Message = "Availability entry not found.";
                    return RedirectToAction("Availability");
                }

                // Pass the availability data to the view
                return View(availability);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error retrieving availability data: " + ex.Message;
                return RedirectToAction("Availability");
            }
        }

        // POST: Availability/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int availabilityID, int userID, bool isAvailable, bool repeatWeekly,
            DateTime startDate, string startTime, DateTime endDate, string endTime)
        {
            try
            {
                // Combine date and time
                DateTime fullStartDate = startDate;
                DateTime fullEndDate = endDate;

                if (!isAvailable && !string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                {
                    // Parse time strings (format: HH:mm)
                    if (!TimeSpan.TryParse(startTime, out TimeSpan startTimeSpan))
                    {
                        ModelState.AddModelError("startTime", "Invalid start time format.");
                    }
                    else
                    {
                        fullStartDate = startDate.Date.Add(startTimeSpan);
                    }

                    if (!TimeSpan.TryParse(endTime, out TimeSpan endTimeSpan))
                    {
                        ModelState.AddModelError("endTime", "Invalid end time format.");
                    }
                    else
                    {
                        fullEndDate = endDate.Date.Add(endTimeSpan);
                    }
                }
                else
                {
                    // If all day, set standard times
                    fullStartDate = startDate.Date.AddHours(7);  // 7 AM
                    fullEndDate = endDate.Date.AddHours(15);     // 3 PM
                }

                // Validate the input
                if (fullStartDate.Date < DateTime.Now.Date)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be in the past.");
                }

                if (fullEndDate.Date < DateTime.Now.Date)
                {
                    ModelState.AddModelError("EndDate", "End date cannot be in the past.");
                }

                if (fullEndDate.Date < fullStartDate.Date)
                {
                    ModelState.AddModelError("EndDate", "End date cannot be before start date.");
                }

                // For non-all-day availability, validate times
                if (!isAvailable && fullStartDate.Date == fullEndDate.Date)
                {
                    if (fullStartDate.TimeOfDay >= fullEndDate.TimeOfDay)
                    {
                        ModelState.AddModelError("endTime", "End time must be after start time.");
                    }
                }

                if (!ModelState.IsValid)
                {
                    return View(new Availability
                    {
                        AvailabilityID = availabilityID,
                        UserID = userID,
                        IsAvailable = isAvailable,
                        RepeatWeekly = repeatWeekly,
                        StartDate = fullStartDate,
                        EndDate = fullEndDate
                    });
                }

                // Update the availability
                bool updateSuccess = _availabilityManager.UpdateAvailabilityByID(
                    availabilityID,
                    userID,
                    isAvailable,
                    repeatWeekly,
                    fullStartDate,
                    fullEndDate);

                if (updateSuccess)
                {
                    TempData["UpdateSuccess"] = "Availability updated successfully!";
                    return RedirectToAction("Availability");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update availability. The system could not process your request.");
                    return View(new Availability
                    {
                        AvailabilityID = availabilityID,
                        UserID = userID,
                        IsAvailable = isAvailable,
                        RepeatWeekly = repeatWeekly,
                        StartDate = fullStartDate,
                        EndDate = fullEndDate
                    });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
                return View(new Availability
                {
                    AvailabilityID = availabilityID,
                    UserID = userID,
                    IsAvailable = isAvailable,
                    RepeatWeekly = repeatWeekly,
                    StartDate = startDate,
                    EndDate = endDate
                });
            }
        }

    }
}

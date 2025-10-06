/// <summary>
/// Ellie Wacker
/// Created: 2025-03-27
/// 
/// Controller for the Vehicle class.
/// </summary>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicLayer;
using DataDomain;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Tesseract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebPresentation.Controllers
{
    public class VehicleController : Controller
    {
        private IVehicleManager _manager = new VehicleManager();
        private IDocumentManager _docManager = new DocumentManager();
        IUserSystemRoleManager _userSystemRoleManager = new UserSystemRoleManager();
        List<UserSystemRole> _userSystemRoles = new List<UserSystemRole>();
        private IUserManager _userManager = new UserManager();
        private SignInManager<IdentityUser> _signInManager;

        public VehicleController(SignInManager<IdentityUser> signInManager)
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
        /// Ellie Wacker
        /// Created: 2025/03/27
        /// 
        /// GET: VehicleController
        /// The Controller method for getting all vehicles
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        [Authorize]
        public ActionResult Index(int id)
        {
            User user = _userManager.GetUserInformationByUserID(id);
            ViewBag.Email = user.Email;
            try
            {
                _userSystemRoles = _userSystemRoleManager.GetUserSystemRolesByUserID(id);
            }
            catch
            {

            }
            try
            {
                ViewBag.UserID = id;
                var vehicles = _manager.GetVehiclesByUserID(id);

                if (vehicles == null)
                {
                    vehicles = new List<Vehicle>();
                    ViewBag.Message = "You don't have any vehicles at this time.";
                }
                else if (!vehicles.Any())
                {
                    ViewBag.Message = "You don't have any vehicles at this time.";
                }

                return View(vehicles);
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.Message + "\n\n" + ex.InnerException.Message;
                ViewBag.Message = "You don't have any vehicles at this time.";
                return View();
            }

        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/27
        /// 
        /// GET: VehicleController/Details/5
        /// The Controller method for getting a detailed view of a vehicle
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/27
        /// 
        /// The Controller method for populating drop downs
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private void populateDropDowns()
        {
            List<string> TransportItems = new List<string>()
            {
               "Adults",
               "Children",
               "Animals",
               "Equipment",
               "Grocercies",
               "Plants",
               "Dirt/Sand/Salt",
               "Rocks",
               "Appliances",
               "Oil",
               "Paint/Sealers/Stain"
            };
            ViewBag.TransportItems = TransportItems;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/27
        /// 
        /// The Controller get method for creating a vehicle
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        // GET: VehicleController/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            ViewBag.UserID = id;
            populateDropDowns();
            return View();
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/27
        /// 
        /// The Controller post method for creating a vehicle
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        /// Updated By: Dat Tran
        /// Updated: 2025-04-27
        /// What was changed: Changed the REGEX of license plate numbers to work with optional spaces and dashes. Added
        /// TempData for adding a vehicle to use with Sweet Alert. 
        /// 
        /// Updated By: Dat Tran
        /// Updated: 2025-05-01
        /// What was changed: Changed the REGEX of license plate numbers to be less strict on the format. 
        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehicle vehicle, int id)
        {
            ViewBag.UserID = id;
            //regex source = https://stackoverflow.com/questions/19547061/regex-representation-for-licence-plate
            string licensePlateRegex = @"^[A-Z0-9]+([- ][A-Z0-9]+)*$";
            Regex validLicensePlate = new Regex(licensePlateRegex);

            string VINRegex = @"^[A-HJ-NPR-Z0-9]{17}$";
            Regex validVIN = new Regex(VINRegex);

            if (vehicle.Make.Length < 3 || vehicle.Make.Length > 50)
            {
                ModelState.AddModelError("Make", "Invalid Vehicle Make.");
            }

            if (vehicle.Model.Length < 3 || vehicle.Model.Length > 50)
            {
                ModelState.AddModelError("Model", "Invalid Vehicle Model.");
            }

            int currentYear = DateTime.Now.Year;
            if (vehicle.Year < 1886 || vehicle.Year > currentYear + 1)
            {
                ModelState.AddModelError("Year", "Invalid Vehicle Year.");
            }

            if (vehicle.Color.Length < 3 || vehicle.Color.Length > 20)
            {
                ModelState.AddModelError("Color", "Invalid Vehicle Color.");
            }

            if (vehicle.NumberOfSeats < 1 || vehicle.NumberOfSeats > 60) //Standard coach bus seat number
            {
                ModelState.AddModelError(nameof(vehicle.NumberOfSeats), "Invalid Number of Seats.");
            }

            if (!validLicensePlate.IsMatch(vehicle.LicensePlateNumber))
            {
                ModelState.AddModelError(nameof(vehicle.LicensePlateNumber), "Invalid License Plate Number.");
            }

            string licensePlateNumber = vehicle.LicensePlateNumber.Replace("-", "").Replace(" ", ""); // removes dashes/spaces
            if (licensePlateNumber.Length > 7)
            {
                ModelState.AddModelError(nameof(vehicle.LicensePlateNumber), "License Plate Number must be 7 characters or less.");
            }
            if (!validVIN.IsMatch(vehicle.VehicleID))
            {
                ModelState.AddModelError(nameof(vehicle.VehicleID), "Invalid VIN Number.");

            }
            if (_manager.GetAllVehicles().Any(existingVehicle => existingVehicle.VehicleID == vehicle.VehicleID.Trim()))
            {
                ModelState.AddModelError("VehicleID", "A vehicle with that VIN number already exists.");
            }


            if (!vehicle.InsuranceStatus)
            {
                ModelState.AddModelError("InsuranceStatus", "Your vehicle must be insured");

            }

            try
            {
                if (ModelState.IsValid)
                {
                    string[] transportOptions = Request.Form["TransportUtility"];
                    vehicle.TransportUtility = string.Join(",", transportOptions);
                    TempData["AddSuccess"] = "Vehicle successfully added.";
                    _manager.InsertVehicle(vehicle.VehicleID, id, false, vehicle.Color, vehicle.Year, licensePlateNumber, vehicle.InsuranceStatus, vehicle.Make, vehicle.Model, vehicle.NumberOfSeats, vehicle.TransportUtility);
                    return RedirectToAction(nameof(Index), new { id = id });
                }
                else
                {
                    Console.WriteLine("ModelState is invalid. Errors:");
                    foreach (var entry in ModelState)
                    {
                        foreach (var error in entry.Value.Errors)
                        {
                            Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
                        }
                    }

                    return View(vehicle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex);
                return View();
            }
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/27
        /// 
        /// The Controller get method for AddInsurance
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        // GET: VehicleController/AddInsurance
        [Authorize]
        public ActionResult AddInsurance(string vehicleID, int userID)
        {
            Vehicle vehicle = _manager.GetAllVehicles().FirstOrDefault(v => v.VehicleID == vehicleID); // gets the desired vehicle

            if (vehicle == null)
            {
                ViewBag.ErrorMessage = "Vehicle not found.";
                return View("Error");
            }
            ViewBag.UserID = userID;

            return View(vehicle);
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/27
        /// 
        /// The Controller post method for adding insurance
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        // POST: VehicleController/AddInsurance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInsurance(Vehicle vehicle, IFormFile imageFile, int userID)
        {
            ViewBag.UserID = userID;
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "An error ocurred please try again.";
                foreach (var entry in ModelState) // writes the errors to the console
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
                    }
                }
                return View(vehicle); // shows the view with the errors
            }
            if (vehicle == null)
            {
                ViewBag.ErrorMessage = "Vehicle not found.";
                return View(vehicle);
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                ViewBag.ErrorMessage = "No file selected.";
                return View(vehicle);
            }
            string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };
            string fileExtension = Path.GetExtension(imageFile.FileName).ToLower(); // gets the extension
            if (!allowedExtensions.Contains(fileExtension)) // sees if the extension is valid
            {
                ViewBag.ErrorMessage = "Invalid file type. Please upload a PNG, JPG, or JPEG.";
                return View(vehicle);
            }
            if (imageFile.FileName.Contains(' ')) // checks the actual file name for empty spaces
            {
                ViewBag.ErrorMessage = "The Image file name contains spaces. Please rename it and try again.";
                return View(vehicle);
            }
            // if all of that passes actually make the image file in the Insurance folder
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Insurance", imageFile.FileName);

                // creates the image in the desired location and overwrites it if it already exists
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                string extractedText = ExtractTextFromImage(filePath); // the image text
                bool isValidInsurance = IsValidInsurance(extractedText); // if its valid
                if (!isValidInsurance)
                {
                    System.IO.File.Delete(filePath); // Delete the invalid file
                    return View(vehicle);
                }
                // inserts the document
                _docManager.InsertDocument("Vehicle", vehicle.VehicleID, imageFile.FileName, "Car Insurance", [], userID, "The Vehicle Insurance");
                // updates the vehicles active field
                _manager.UpdateActiveByVehicleID(vehicle.VehicleID, true);

                //Use TempData instead of viewBag because TempData persists across redirects 
                TempData["AddInsuranceSuccess"] = "Insurance document added successfully!"; // shows the message in index
                Console.WriteLine($"File saved successfully: {System.IO.File.Exists(filePath)} - {filePath}");
                return RedirectToAction(nameof(Index), new { id = userID }); // go back to index
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error saving document: " + ex.Message;
                return View(vehicle);
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/27
        /// 
        /// The Controller get method for AddLicense
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        // GET: VehicleController/AddInsurance
        [Authorize]
        public ActionResult AddLicense(int userID)
        {
            ViewBag.UserID = userID;
            return View();
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/27
        /// 
        /// The Controller post method for adding a valid driver's license
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        // POST: VehicleController/AddLicense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLicense(IFormFile imageFile, int userID)
        {
            ViewBag.UserID = userID;
            try
            {
                _userSystemRoles = _userSystemRoleManager.GetUserSystemRolesByUserID(userID);
            }
            catch
            {

            }
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "An error ocurred please try again.";
                foreach (var entry in ModelState) // writes the errors to the console
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
                    }
                }
                return View(); // shows the view with the errors
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                ViewBag.ErrorMessage = "No file selected.";
                return View();
            }
            string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };
            string fileExtension = Path.GetExtension(imageFile.FileName).ToLower(); // gets the extension
            if (!allowedExtensions.Contains(fileExtension)) // sees if the extension is valid
            {
                ViewBag.ErrorMessage = "Invalid file type. Please upload a PNG, JPG, or JPEG.";
                return View();
            }
            if (imageFile.FileName.Contains(' ')) // checks the actual file name for empty spaces
            {
                ViewBag.ErrorMessage = "The Image file name contains spaces. Please rename it and try again.";
                return View();
            }
            // if all of that passes actually make the image file in the License folder
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "License", imageFile.FileName);

                // creates the image in the desired location and overwrites it if it already exists
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                string extractedText = ExtractTextFromImage(filePath); // the image text
                bool isValidLicense = IsValidLicense(extractedText); // if its valid
                if (!isValidLicense)
                {
                    System.IO.File.Delete(filePath); // Delete the invalid file
                    return View();
                }
                // inserts the document
                _docManager.InsertDocument("Vehicle", userID.ToString(), imageFile.FileName, "Driver's License", [], userID, "Driver's License for Vehicle");
                if (!_userSystemRoles.Any(role => role.SystemRoleID == "Driver")) // if any of the roleIDs match driver
                {
                    _userSystemRoleManager.InsertUserSystemRole(userID, "Driver");
                }

                //Use TempData instead of viewBag because TempData persists across redirects 
                TempData["AddLicenseSuccess"] = "Drivers license added successfully!"; // shows the message in index
                Console.WriteLine($"File saved successfully: {System.IO.File.Exists(filePath)} - {filePath}");
                return RedirectToAction(nameof(Index), new { id = userID }); // go back to index
            }
            catch (Exception ex)
            {

                Console.WriteLine(userID);
                ViewBag.ErrorMessage = "Error saving document: " + ex.Message;
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/04/01
        /// 
        /// The helper method for extracted the text from an image using tesseract
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        private string ExtractTextFromImage(string imagePath)
        {
            try
            {
                // Ensure that tessdata is in the correct location
                string tessDataPath = Path.Combine(Directory.GetCurrentDirectory(), "tessdata");
                Console.WriteLine($"Tesseract tessdata path: {tessDataPath}");

                // Initialize Tesseract Engine
                using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath)) // uses the Pix class to process the image
                    {
                        using (var page = engine.Process(img)) // runs OCR on the img
                        {
                            string extractedText = page.GetText();
                            return extractedText; // returns the text
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error running Tesseract: " + ex.ToString()); // logs the exception
                return "OCR error: " + ex.Message;
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/04/01
        /// 
        /// The helper method for seeing if the insurance is a valid insurance image
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public bool IsValidInsurance(string extractedText)
        {
            // Checks if there is text in the image
            if (string.IsNullOrWhiteSpace(extractedText.Trim()))
            {
                ViewBag.ErrorMessage = "No text detected in the image.";
                return false;
            }
            string lowerText = extractedText.ToLower(); // makes lowercase

            // Sees if extracted text contains common insurance words
            bool containsInsuranceKeyword = lowerText.Contains("insurance") ||
                                            lowerText.Contains("policy") ||
                                            lowerText.Contains("coverage") ||
                                            lowerText.Contains("provider") ||
                                            lowerText.Contains("insured") ||
                                            lowerText.Contains("claim") ||
                                            lowerText.Contains("expiration") ||
                                            lowerText.Contains("valid until") ||
                                            lowerText.Contains("expires");

            // Makes sure passes validation
            if (containsInsuranceKeyword)
            {
                return true;
            }

            ViewBag.ErrorMessage = "The document did not pass validation";
            return false;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/04/03
        /// 
        /// The helper method for seeing if the drivers license is a valid license image
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public bool IsValidLicense(string extractedText)
        {
            // Checks if there is text in the image
            if (string.IsNullOrWhiteSpace(extractedText.Trim()))
            {
                ViewBag.ErrorMessage = "No text detected in the image.";
                return false;
            }
            string lowerText = extractedText.ToLower(); // makes lowercase

            // Sees if extracted text contains common insurance words
            bool containsLicenseKeyword = lowerText.Contains("driver's license") ||
                                         lowerText.Contains("driver license") ||
                                         lowerText.Contains("dl number") ||
                                         lowerText.Contains("class") ||
                                         lowerText.Contains("restrictions") ||
                                         lowerText.Contains("endorsements") ||
                                         lowerText.Contains("date of birth") ||
                                         lowerText.Contains("expires");

            // Makes sure passes validation
            if (containsLicenseKeyword)
            {
                return true;
            }

            ViewBag.ErrorMessage = "The document did not pass validation";
            return false;
        }

        /// <summary>
        /// Creator:  Ellie Wacker
        /// Created:  2025/04/17
        /// Summary:  The method for deactivate vehicle. It is linked to an on click event
        ///
        /// Last Updated By: Dat Tran
        /// Last Updated: 2025-04-27
        /// What was Changed: 	Changed TempData to be more specific to work with Sweet Alert. 
        /// </summary>
        // GET: ProjectController/Deactivate/5
        [Authorize]
        public ActionResult Deactivate(string id, int userID)
        {
            var vehicle = _manager.GetAllVehicles().FirstOrDefault(v => v.VehicleID == id);
            try
            {
                _manager.UpdateActiveByVehicleID(id, false);
                TempData["DeleteSuccess"] = "You have successfully deactivated your vehicle!";
                return RedirectToAction(nameof(Index), new { id = userID }); // go back to index
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "An error occured. Please try again!";
                return View(vehicle);
            }
        }

        // GET: VehicleController/Edit/{vehicleID}
        [Authorize]
        public ActionResult Edit(string id, int userID)
        {
            ViewBag.UserID = userID;
            try
            {
                // populate the transport items dropdown
                populateDropDowns();

                var vehicle = _manager.GetAllVehicles().FirstOrDefault(v => v.VehicleID == id);

                if (vehicle == null)
                {
                    TempData["ErrorMessage"] = "Vehicle not found.";
                    return RedirectToAction(nameof(Index), new { id = userID });
                }

                // Check if the vehicle belongs to current user
                if (vehicle.UserID != userID)
                {
                    TempData["ErrorMessage"] = "You do not have permission to update this vehicle.";
                    return RedirectToAction(nameof(Index), new { id = userID });
                }

                return View(vehicle);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error retrieving vehicle information: " + ex.Message;
                return RedirectToAction(nameof(Index), new { id = userID });
            }
        }

        // POST: VehicleController/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vehicle vehicle, int userID)
        {
            ViewBag.UserID = userID;
            populateDropDowns(); // Make sure dropdowns are populated if validation fails

            var existingVehicle = _manager.GetAllVehicles().FirstOrDefault(v => v.VehicleID == vehicle.VehicleID);

            if (existingVehicle != null && existingVehicle.LicensePlateNumber == vehicle.LicensePlateNumber)
            {
                // Remove any validation errors for LicensePlateNumber if they exist
                if (ModelState["LicensePlateNumber"] != null && ModelState["LicensePlateNumber"].Errors.Count > 0)
                {
                    ModelState["LicensePlateNumber"].Errors.Clear();
                }
            }
            else
            {
                // Only validate if the license plate changed
                string licensePlateRegex = @"^[A-Za-z]{1,3}-[A-Za-z]{1,2}-[0-9]{1,4}$";
                Regex validLicensePlate = new Regex(licensePlateRegex);

                if (!validLicensePlate.IsMatch(vehicle.LicensePlateNumber))
                {
                    ModelState.AddModelError(nameof(vehicle.LicensePlateNumber), "Invalid License Plate Number.");
                }

                string licensePlateNumber = vehicle.LicensePlateNumber.Replace("-", ""); // removes dashes
                if (licensePlateNumber.Length > 7)
                {
                    ModelState.AddModelError("LicensePlateNumber", "License Plate Number must be 7 characters or less.");
                }
            }

            // Other validations
            if (vehicle.Make.Length < 3 || vehicle.Make.Length > 50)
            {
                ModelState.AddModelError("Make", "Invalid Vehicle Make.");
            }

            if (vehicle.Model.Length < 3 || vehicle.Model.Length > 50)
            {
                ModelState.AddModelError("Model", "Invalid Vehicle Model.");
            }

            int currentYear = DateTime.Now.Year;
            if (vehicle.Year < 1886 || vehicle.Year > currentYear + 1)
            {
                ModelState.AddModelError("Year", "Invalid Vehicle Year.");
            }

            if (vehicle.Color.Length < 3 || vehicle.Color.Length > 20)
            {
                ModelState.AddModelError("Color", "Invalid Vehicle Color.");
            }

            if (vehicle.NumberOfSeats < 1 || vehicle.NumberOfSeats > 60) //Standard coach bus seat number
            {
                ModelState.AddModelError(nameof(vehicle.NumberOfSeats), "Invalid Number of Seats.");
            }

            if (!vehicle.InsuranceStatus)
            {
                ModelState.AddModelError("InsuranceStatus", "Your vehicle must be insured");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    string[] transportOptions = Request.Form["TransportUtility"];
                    if (transportOptions != null && transportOptions.Length > 0)
                    {
                        vehicle.TransportUtility = string.Join(",", transportOptions);
                    }
                    else
                    {
                        vehicle.TransportUtility = "";
                    }

                    vehicle.UserID = userID;

                    if (existingVehicle != null && vehicle.InsuranceStatus != existingVehicle.InsuranceStatus)
                    {
                        if (!vehicle.InsuranceStatus)
                        {
                            ModelState.AddModelError("InsuranceStatus", "You cannot disable insurance for an active vehicle.");
                            return View(vehicle);
                        }
                    }

                    bool result = _manager.UpdateVehicleByID(vehicle);

                    if (result)
                    {
                        TempData["SuccessMessage"] = "Vehicle updated successfully!";
                        return RedirectToAction(nameof(Index), new { id = userID });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to update vehicle.";
                        return View(vehicle);
                    }
                }
                else
                {
                    Console.WriteLine("ModelState is invalid. Errors:");
                    foreach (var entry in ModelState)
                    {
                        foreach (var error in entry.Value.Errors)
                        {
                            Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
                        }
                    }

                    return View(vehicle);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error updating vehicle: " + ex.Message;
                Console.WriteLine("Exception in Edit POST: " + ex.ToString());
                return View(vehicle);
            }
        }
    }
}

/// <summary>
/// Ellie Wacker
/// Created: 2025-02-16
/// 
/// Class for fake Vehicle objects that are used in testing.
/// /// Interface that holds method declarations for managing Vehicle data.
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-25
/// What Was Changed:
///     Added SelectAllActiveVehiclesByUserID
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace DataAccessFakes
{
    public class VehicleAccessorFake : IVehicleAccessor
    {
        private List<Vehicle> _vehicles;

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/18
        /// 
        /// This method creates a fake vehicle
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public VehicleAccessorFake()
        {
            _vehicles = new List<Vehicle>();

            _vehicles.Add(new Vehicle()
            {
                VehicleID = "vehicle1",
                UserID = 101,
                Active = false,
                Color = "Red",
                Year = 2020,
                LicensePlateNumber = "ABC1234",
                InsuranceStatus = true,
                Make = "Toyota",
                Model = "Camry",
                NumberOfSeats = 5,
                TransportUtility = "Personal"
            });
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/18
        /// 
        /// This method shows validation for adding a vehicle that can be checked in the vehicleManagerTests
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int addVehicle(string vehicleID, int userID, bool active, string color, int year, string licensePlateNumber, bool insuranceStatus, string make, string model, int numberOfSeats, string transportUtility)
        {
            // Regex patterns
            string licensePlateRegex = @"^[A-Za-z]{1,3}-[A-Za-z]{1,2}-[0-9]{1,4}$";
            string vinRegex = @"^[A-HJ-NPR-Z0-9]{17}$";

            if (make.Length < 3 || make.Length > 50)
            {
                throw new ApplicationException("Invalid Car Make.");
            }
            if (model.Length < 2 || model.Length > 50)
            {
                throw new ApplicationException("Invalid Car Model.");
            }

            int currentYear = DateTime.Now.Year;
            if (year < 1886 || year > currentYear + 1)
            {
                throw new ApplicationException("Invalid Car Year.");
            }

            if (color.Length < 3 || color.Length > 20)
            {
                throw new ApplicationException("Invalid Color.");
            }

            if (numberOfSeats == 0)
            {
                throw new ApplicationException("Please enter the number of seats.");
            }

            if (numberOfSeats > 60)
            {
                throw new ApplicationException("Number of seats must be less than 60");
            }

            if (!Regex.IsMatch(licensePlateNumber, licensePlateRegex))
            {
                throw new ApplicationException("Invalid License Plate Number.");
            }

            if (!Regex.IsMatch(vehicleID, vinRegex))
            {
                throw new ApplicationException("Invalid VIN Number.");
            }

            if (insuranceStatus == false)
            {
                throw new ApplicationException("Vehicle must be insured.");
            }

            if (SelectAllVehicles().Any(vehicle => vehicle.VehicleID == vehicleID.Trim()))
            {
                throw new ApplicationException("A vehicle with that VIN number already exists.");
            }

            if(_vehicles.Count < 0)
            {
                throw new ApplicationException("There are no vehicles in this list");
            }


            int result = 0;

            var _vehicle = new Vehicle()
            {
                VehicleID = vehicleID,
                UserID = userID,
                Active = active,
                Color = color,
                Year = year,
                LicensePlateNumber = licensePlateNumber,
                InsuranceStatus = insuranceStatus,
                Make = make,
                Model = model,
                NumberOfSeats = numberOfSeats,
                TransportUtility = transportUtility
            };


            _vehicles.Add(_vehicle); 
            result = _vehicle.UserID;
  

            // Check if the result is valid, otherwise throw an exception
            if (result == 0)
            {
                throw new ArgumentException("Unable to insert vehicle");
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/18
        /// 
        /// This method gets a list of test vehicles
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public List<Vehicle> SelectAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            foreach (var vehicle in _vehicles)
            {
                vehicles.Add(vehicle);
            }
            return vehicles;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/21
        /// 
        /// This method gets a list of test vehicles with a certain user id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public List<Vehicle> SelectVehiclesByUserID(int userID)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            foreach (var vehicle in _vehicles)
            {
                if (vehicle.UserID == userID)
                {
                    vehicles.Add(vehicle);
                }
            }
            if (userID == 0)
            {
                throw new ArgumentException("Vehicle record not found");
            }
            return vehicles;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// 
        /// This method updates a vehicle's active field if the vehicle has a matching vehicleID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public int UpdateActiveByVehicleID(string vehicleID, bool active)
        {
            int count = 0;
            for (int i = 0; i < _vehicles.Count(); i++)
            {
                if (_vehicles[i].VehicleID == vehicleID)
                {
                    _vehicles[i].Active = active;
                    count++;
                }
            }
            if (count == 0)
            {
                throw new ArgumentException("Vehicle record not found");
            }
            return count;
        }

        public List<Vehicle> SelectAllActiveVehiclesByUserID(int userID)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            foreach (var vehicle in _vehicles)
            {
                if (vehicle.UserID == userID)
                {
                    vehicles.Add(vehicle);
                }
            }
            if (userID == 0)
            {
                throw new ArgumentException("Record of active vehicles not found for this user.");
            }
            return vehicles;
        }

        public int UpdateVehicleByID(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException("vehicle", "Vehicle cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(vehicle.VehicleID))
            {
                throw new ApplicationException("VehicleID cannot be empty.");
            }

            if (vehicle.UserID <= 0)
            {
                throw new ApplicationException("Invalid UserID.");
            }

            if (string.IsNullOrWhiteSpace(vehicle.Color) || vehicle.Color.Length < 3 || vehicle.Color.Length > 20)
            {
                throw new ApplicationException("Invalid Color.");
            }

            int currentYear = DateTime.Now.Year;
            if (vehicle.Year < 1886 || vehicle.Year > currentYear + 1)
            {
                throw new ApplicationException("Invalid Car Year.");
            }

            if (string.IsNullOrWhiteSpace(vehicle.LicensePlateNumber))
            {
                throw new ApplicationException("License Plate Number cannot be empty.");
            }

            string licensePlateRegex = @"^[A-Z0-9]{1,7}$";
            if (!Regex.IsMatch(vehicle.LicensePlateNumber.ToUpper(), licensePlateRegex))
            {
                throw new ApplicationException("Invalid License Plate Number.");
            }

            if (string.IsNullOrWhiteSpace(vehicle.Make) || vehicle.Make.Length < 3 || vehicle.Make.Length > 50)
            {
                throw new ApplicationException("Invalid Car Make.");
            }

            if (string.IsNullOrWhiteSpace(vehicle.Model) || vehicle.Model.Length < 2 || vehicle.Model.Length > 50)
            {
                throw new ApplicationException("Invalid Car Model.");
            }

            if (vehicle.NumberOfSeats <= 0 || vehicle.NumberOfSeats > 60)
            {
                throw new ApplicationException("Invalid Number of Seats.");
            }

            int rowsAffected = 0;

            for (int i = 0; i < _vehicles.Count; i++)
            {
                if (_vehicles[i].VehicleID == vehicle.VehicleID)
                {
                    _vehicles[i] = vehicle;
                    rowsAffected = 1;
                    break;
                }
            }

            if (rowsAffected == 0)
            {
                throw new ApplicationException("Vehicle not found.");
            }

            return rowsAffected;
        }
    }
}
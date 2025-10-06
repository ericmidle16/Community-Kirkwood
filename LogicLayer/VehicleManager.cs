/// <summary>
/// Ellie Wacker
/// Created: 2025-02-17
/// 
/// Class that implements the IVehicleManager Interface - used for
/// managing Vehicle data from Vehicle data fake objects &/or the DB.
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-25
/// What Was Changed:
///     Added GetAllActiveVehiclesByUserID
/// </summary>
/// 
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class VehicleManager : IVehicleManager
    {
        private IVehicleAccessor _vehicleAccessor;

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/17
        /// 
        /// Default constructor for VehicleManager
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public VehicleManager()
        {
            _vehicleAccessor = new VehicleAccessor();

        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/17
        /// 
        /// Parameterized constructor for VehicleManager
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public VehicleManager(IVehicleAccessor vehicleAccessor)
        {
            _vehicleAccessor = vehicleAccessor;

        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/17
        /// 
        /// Method for InsertVehicle
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int InsertVehicle(string vehicleID, int userID, bool active, string color, int year, string licensePlateNumber, bool insuranceStatus, string make, string model, int numberOfSeats, string transportUtility)
        {
            int result = 0;
            try
            {
                result = (_vehicleAccessor.addVehicle(vehicleID, userID, active, color, year, licensePlateNumber,  insuranceStatus,  make,  model,  numberOfSeats,  transportUtility));
                if (result == 0)
                {
                    throw new ApplicationException("Insert Vehicle Failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Insert Vehicle Failed", ex);
            }
            return result;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/17
        /// 
        /// Method for GetAllVehicles
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            try
            {
                vehicles = _vehicleAccessor.SelectAllVehicles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle list not found", ex);
            }
            return vehicles;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/20
        /// 
        /// Method for GetVehiclesByUserID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public List<Vehicle> GetVehiclesByUserID(int userID)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            try
            {
                vehicles = _vehicleAccessor.SelectVehiclesByUserID(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle list not found", ex);
            }
            return vehicles;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// 
        /// Method for UpdateActiveByVehicleID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public bool UpdateActiveByVehicleID(string vehicleID, bool active)
        {
            bool result = false;
            try
            {
                result = (1 == _vehicleAccessor.UpdateActiveByVehicleID(vehicleID, active));
                if (!result)
                {
                    throw new ApplicationException("No Vehicle Record Found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }

            return result;
        }

        public List<Vehicle> GetAllActiveVehiclesByUserID(int userID)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            try
            {
                vehicles = _vehicleAccessor.SelectAllActiveVehiclesByUserID(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle list not found", ex);
            }
            return vehicles;
        }

        public bool UpdateVehicleByID(Vehicle vehicle)
        {
            try
            {
                int rowsAffected = _vehicleAccessor.UpdateVehicleByID(vehicle);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating vehicle information.", ex);
            }
        }
    }
}

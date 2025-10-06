/// <summary>
/// Ellie Wacker
/// Created: 2025-02-17
/// 
/// Interface that holds method declarations for accessing Vehicle data.
/// 
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-25
/// What Was Changed:
///     Added SelectAllActiveVehiclesByUserID
/// </summary>
/// 
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IVehicleAccessor
    {
        public int addVehicle(string vehicleID, int userID, bool active, string color, int year, string licensePlateNumber, bool insuranceStatus, string make, string model, int numberOfSeats, string transportUtility);
        public List<Vehicle> SelectAllVehicles();
        public List<Vehicle> SelectVehiclesByUserID(int userID);
        public int UpdateActiveByVehicleID(string vehicleID,  bool active);
        public List<Vehicle> SelectAllActiveVehiclesByUserID(int userID);
        public int UpdateVehicleByID(Vehicle vehicle);
    }
}

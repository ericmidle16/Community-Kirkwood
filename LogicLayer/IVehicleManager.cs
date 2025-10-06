/// <summary>
/// Ellie Wacker
/// Created: 2025-02-17
/// 
/// Interface that holds method declarations for managing Vehicle data.
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-22
/// What Was Changed:
///     Added GetAllActiveVehiclesByUserID
/// 
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IVehicleManager
    {
        int InsertVehicle(string vehicleID, int userID, bool active, string color, int year, string licensePlateNumber, bool insuranceStatus, string make, string model, int numberOfSeats, string transportUtility);
        List<Vehicle> GetAllVehicles();
        List<Vehicle> GetVehiclesByUserID(int userID);
        bool UpdateActiveByVehicleID(string vehicleID, bool active);
        List<Vehicle> GetAllActiveVehiclesByUserID(int userID);
        public bool UpdateVehicleByID(Vehicle vehicle);
    }
}

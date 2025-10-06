/// <summary>
/// Ellie Wacker
/// Created: 2025-02-17
/// 
/// Class that implements the IVehicleAccessor Interface - used for
/// accessing Vehicle data from the DB.
/// 
/// Last Updated By: Jennifer Nicewanner
/// Last Updated: 2025-04-25
/// What Was Changed:
///     Added SelectAllActiveVehiclesByUserID
/// </summary>
 
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class VehicleAccessor : IVehicleAccessor
    {
        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/17
        /// 
        /// The Data Access Method for adding a vehicle into the system
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int addVehicle(string vehicleID, int userID, bool active, string color, int year, string licensePlateNumber, bool insuranceStatus, string make, string model, int numberOfSeats, string transportUtility)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_vehicle", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Adding all the parameters
            cmd.Parameters.Add("@VehicleID", SqlDbType.NVarChar, 17);
            cmd.Parameters["@VehicleID"].Value = vehicleID;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            cmd.Parameters.Add("@Color", SqlDbType.NVarChar, 50); // Adjust length as needed
            cmd.Parameters["@Color"].Value = color;

            cmd.Parameters.Add("@Year", SqlDbType.Int);
            cmd.Parameters["@Year"].Value = year;

            cmd.Parameters.Add("@LicensePlateNumber", SqlDbType.NVarChar, 50); // Adjust length as needed
            cmd.Parameters["@LicensePlateNumber"].Value = licensePlateNumber;

            cmd.Parameters.Add("@InsuranceStatus", SqlDbType.Bit);
            cmd.Parameters["@InsuranceStatus"].Value = insuranceStatus;

            cmd.Parameters.Add("@Make", SqlDbType.NVarChar, 50); // Adjust length as needed
            cmd.Parameters["@Make"].Value = make;

            cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 50); // Adjust length as needed
            cmd.Parameters["@Model"].Value = model;

            cmd.Parameters.Add("@NumberOfSeats", SqlDbType.Int);
            cmd.Parameters["@NumberOfSeats"].Value = numberOfSeats;

            cmd.Parameters.Add("@TransportUtility", SqlDbType.NVarChar, 100); // Adjust length as needed
            cmd.Parameters["@TransportUtility"].Value = transportUtility;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/17
        /// 
        /// The Data Access Method for selecting all vehicles in the system
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

            // connection
            var conn = DBConnection.GetConnection();

            //command
            var cmd = new SqlCommand("sp_select_all_vehicles", conn);

            //command type
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var vehicle = new Vehicle()
                        {
                            VehicleID = reader.GetString(0),
                            UserID = reader.GetInt32(1),
                            Active = reader.GetBoolean(2),
                            Color = reader.GetString(3),
                            Year = reader.GetInt32(4),
                            LicensePlateNumber = reader.GetString(5),
                            InsuranceStatus = reader.GetBoolean(6),
                            Make = reader.GetString(7),
                            Model = reader.GetString(8),
                            NumberOfSeats = reader.GetInt32(9),
                            TransportUtility = reader.GetString(10)
                        };
                        vehicles.Add(vehicle);
                    }
                }
                else
                {
                    throw new ArgumentException("Vehicle list not found");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return vehicles;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/21
        /// 
        /// The Data Access Method for selecting vehicles with a certain user id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Stan Anderson
        /// Updated: 2025/04/09
        /// example: Allowed it to show even if there are no vehicles
        /// </remarks> 
        public List<Vehicle> SelectVehiclesByUserID(int userID)
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            // connection
            var conn = DBConnection.GetConnection();

            //command
            var cmd = new SqlCommand("sp_select_vehicles_by_userID", conn);

            //command type
            cmd.CommandType = CommandType.StoredProcedure;

            //parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            //values
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var vehicle = new Vehicle()
                        {
                            VehicleID = reader.GetString(0),
                            UserID = reader.GetInt32(1),
                            Active = reader.GetBoolean(2),
                            Color = reader.GetString(3),
                            Year = reader.GetInt32(4),
                            LicensePlateNumber = reader.GetString(5),
                            InsuranceStatus = reader.GetBoolean(6),
                            Make = reader.GetString(7),
                            Model = reader.GetString(8),
                            NumberOfSeats = reader.GetInt32(9),
                            TransportUtility = reader.GetString(10)
                        };
                        vehicles.Add(vehicle);
                    }
                }
                else
                {
                    // throw new ArgumentException("Vehicle list not found");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return vehicles;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// 
        /// The Data Access Method for updating vehicles active field with a certain vehicle id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int UpdateActiveByVehicleID(string vehicleID, bool active)
        {
            int result = 0;

            //connection
            var conn = DBConnection.GetConnection();
            // command
            var cmd = new SqlCommand("sp_update_vehicle_active_by_vehicleID", conn);
            //type
            cmd.CommandType = CommandType.StoredProcedure;
            //parameters
            cmd.Parameters.Add("@VehicleID", SqlDbType.NVarChar, 17).Value = vehicleID;
            cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = active;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        /// <summary>
        /// Jennifer Nicewanner
        /// Created: 2025-04-24
        /// 
        /// This accessor method gets a list of vehicles with a certain user id.
        /// </summary>
        ///
        /// Last Updated By: Jennifer Nicewanner
        /// Last Updated: 2025-04-24
        /// What Was Changed:
        ///     Added sp_select_active_vehicles_by_userID
        /// </remarks> 
        public List<Vehicle> SelectAllActiveVehiclesByUserID(int userID)
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            // connection
            var conn = DBConnection.GetConnection();

            //command
            var cmd = new SqlCommand("sp_select_active_vehicles_by_userID", conn);

            //command type
            cmd.CommandType = CommandType.StoredProcedure;

            //parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            //values
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var vehicle = new Vehicle()
                        {
                            VehicleID = reader.GetString(0),
                            UserID = reader.GetInt32(1),
                            Active = reader.GetBoolean(2),
                            Color = reader.GetString(3),
                            Year = reader.GetInt32(4),
                            LicensePlateNumber = reader.GetString(5),
                            InsuranceStatus = reader.GetBoolean(6),
                            Make = reader.GetString(7),
                            Model = reader.GetString(8),
                            NumberOfSeats = reader.GetInt32(9),
                            TransportUtility = reader.GetString(10)
                        };
                        vehicles.Add(vehicle);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return vehicles;
        }

        /// <summary>
        /// Jackson Manternach
        /// Created: 2025/02/20
        /// Data access method to use the sp_update_vehicle_by_id stored procedure
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        /// <param name="vehicle">Reads in a Vehicle object</param>
        /// <exception cref="ApplicationException">Prompts the user that the update failed</exception>
        /// <returns>Rows edited</returns>   
        public int UpdateVehicleByID(Vehicle vehicle)
        {
            int rowsAffected = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_vehicle_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VehicleID", SqlDbType.NVarChar, 17).Value = vehicle.VehicleID;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = vehicle.UserID;
            cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = vehicle.Active;
            cmd.Parameters.Add("@Color", SqlDbType.NVarChar, 20).Value = vehicle.Color;
            cmd.Parameters.Add("@Year", SqlDbType.Int).Value = vehicle.Year;
            cmd.Parameters.Add("@LicensePlateNumber", SqlDbType.NVarChar, 7).Value = vehicle.LicensePlateNumber;
            cmd.Parameters.Add("@InsuranceStatus", SqlDbType.Bit).Value = vehicle.InsuranceStatus;
            cmd.Parameters.Add("@Make", SqlDbType.NVarChar, 50).Value = vehicle.Make;
            cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 50).Value = vehicle.Model;
            cmd.Parameters.Add("@NumberOfSeats", SqlDbType.Int).Value = vehicle.NumberOfSeats;
            cmd.Parameters.Add("@TransportUtility", SqlDbType.NVarChar, 500).Value = vehicle.TransportUtility;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update vehicle information.", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}


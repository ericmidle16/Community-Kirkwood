/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/10
/// Summary:Implements the IAvaliabilityAccessor 
/// interface to manage avaiability-related data operations,
/// such as inserting a new avaiability into the 
/// database, checking if that availability exists already
/// with that email and selecting availability.
/// Last Upaded By: Skyann Heintz
/// Last Updated: 2025-03-12
/// What Was Changed: Added SelectAvailabilityByUser
/// /// Last Updated By: Dat Tran
/// Last Updated: 2025-03-15
/// What was changed: Deleted UnassignedVolunteersAccessor and moved the method into this class.
/// 
/// Last Updated By: Chase Hannen
///	Last Updated Date:  2025-04-11
///	What Was Changed:
///	    Added SelectAvailabilityVMByProjectID
/// </summary>
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataDomain;
using DataAccessInterfaces;

namespace DataAccessLayer
{
    public class AvailabilityAccessor : IAvailabilityAccessor
    {
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Executes the stored procedure 
        /// sp_insert_availability to insert a new availability 
        /// record into the database for a user.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int InsertAvailability(int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate)
        {
            int rowsAffected = 0;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_insert_availability", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@IsAvailable", SqlDbType.Bit).Value = isAvailable;
                    cmd.Parameters.Add("@RepeatWeekly", SqlDbType.Bit).Value = repeatWeekly;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;

                    try
                    {
                        conn.Open();

                        rowsAffected = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Insert Availability failed", ex);
                    }
                }
            }
            return rowsAffected;
        }
       
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Executes the stored procedure sp_select_existing_availability to check if a user's availability 
        /// already exists for a given date range. It queries the database and returns a boolean indicating whether 
        /// availability exists for the specified user and dates. If an error occurs during the database access, 
        /// it throws an application exception with a relevant error message.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool SelectExistingAvailability(int userID, DateTime startDate, DateTime endDate)
        {
            bool exists = false;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_select_existing_availability", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;

                    try
                    {
                        conn.Open();
                        exists = (int)cmd.ExecuteScalar() > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Check Availability failed", ex);
                    }
                }
            }
            return exists;
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/23
        /// Summary: Executes the stored procedure sp_select_availability_by_user
        /// to retrieve availability details for a specific user based on the UserID.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public List<Availability> SelectAvailabilityByUser(int userID)
        {
            List<Availability> availabilityList = new List<Availability>();

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_select_availability_by_user", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Availability availability = new Availability
                                {
                                    AvailabilityID = reader.GetInt32(reader.GetOrdinal("AvailabilityID")),
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
                                    RepeatWeekly = reader.GetBoolean(reader.GetOrdinal("RepeatWeekly")),
                                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                    EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate"))
                                };
                                availabilityList.Add(availability);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Select Availability by User failed", ex);
                    }
                }
            }
            return availabilityList;
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-11
        /// Summary: This method connects to the database and uses the stored procedure to display the parameters.
        /// Though not sure if this is the correct way to implement a stored procedure that joins two tables. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public List<UserAvailability> SelectAvailableUsers(bool isAvailable, int eventID)
        {
            List<UserAvailability> available = new List<UserAvailability>();

            var connection = DBConnection.GetConnection();
            var command = new SqlCommand("sp_view_available_users", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IsAvailable", isAvailable);
            command.Parameters.AddWithValue("@EventID", eventID);

            try
            {
                connection.Open();
                var r = command.ExecuteReader();
                while (r.Read())
                    available.Add(new UserAvailability
                    {

                        UserID = r.GetInt32(0),
                        GivenName = r.GetString(1),
                        FamilyName = r.GetString(2),
                        City = r.GetString(3),
                        State = r.GetString(4),
                        IsAvailable = isAvailable
                    });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return available;
        }

        // Author: Chase Hannen
        public List<AvailabilityVM> SelectAvailabilityVMByProjectID(int projectID)
        {
            List<AvailabilityVM> availability = new List<AvailabilityVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_availabilityVM_by_projectID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    availability.Add(new AvailabilityVM
                    {
                        AvailabilityID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        IsAvailable = reader.GetBoolean(2),
                        RepeatWeekly = reader.GetBoolean(3),
                        StartDate = reader.GetDateTime(4),
                        EndDate = reader.GetDateTime(5),
                        GivenName = reader.GetString(6),
                        FamilyName = reader.GetString(7)
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return availability;
        } // END SelectAvailabilityVMByProjectID

        public int UpdateAvailabilityByID(int availabilityID, int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate)
        {
            int rowsAffected = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_availability_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AvailabilityID", SqlDbType.Int).Value = availabilityID;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            cmd.Parameters.Add("@IsAvailable", SqlDbType.Bit).Value = isAvailable;
            cmd.Parameters.Add("@RepeatWeekly", SqlDbType.Bit).Value = repeatWeekly;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update availability.", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public int DeleteAvailabilityByAvailabilityID(int availabilityID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_delete_availability_by_availability_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AvailabilityID", SqlDbType.Int);
            cmd.Parameters["@AvailabilityID"].Value = availabilityID;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database error deleting availability records", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}

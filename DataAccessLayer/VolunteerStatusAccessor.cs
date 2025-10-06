/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/13
/// Summary:  A Class containing all the methods that access 
///           the VolunteerStatus table in the database
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/13
/// What was Changed: Initial creation	
/// 
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025/03/31
/// What was Changed: I added my methods
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025/04/01
/// What was Changed: Added the AddVolunteerStatus & SelectVolunteerStatusByProjectID methods.
/// </remarks>

using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class VolunteerStatusAccessor : IVolunteerStatusAccessor
    {
        public List<VMVolunteerStatus> SelectRejectedVolunteerStatusByProjectID(int projectID)
        {
            List<VMVolunteerStatus> volunteerstatus = new List<VMVolunteerStatus>();

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_select_rejected_volunteerstatus_by_projectid", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);

            cmd.Parameters["@ProjectID"].Value = projectID;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    VMVolunteerStatus vs = new VMVolunteerStatus();
                    vs.UserID = r.GetInt32(0);
                    vs.ProjectID = r.GetInt32(1);
                    vs.Approved = r.IsDBNull(2) ? null : r.GetBoolean(2);
                    vs.Name = r.GetString(3);
                    volunteerstatus.Add(vs);
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
            return volunteerstatus;
        }

        public List<VMVolunteerStatus> SelectPendingVolunteerStatusByProjectID(int projectID)
        {
            List<VMVolunteerStatus> volunteerstatus = new List<VMVolunteerStatus>();

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_select_volunteerstatus_by_projectid", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);

            cmd.Parameters["@ProjectID"].Value = projectID;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    VMVolunteerStatus vs = new VMVolunteerStatus();
                    vs.UserID = r.GetInt32(0);
                    vs.ProjectID = r.GetInt32(1);
                    vs.Approved = r.IsDBNull(2) ? null : r.GetBoolean(2);
                    vs.Name = r.GetString(3);
                    volunteerstatus.Add(vs);
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
            return volunteerstatus;
        }

        public int UpdateVolunteerStatusByUserIDAndProjectID(VMVolunteerStatus volunteerStatus)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_volunteerstatus_by_userid_and_projectid", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters.Add("@Approved", SqlDbType.Bit);
            cmd.Parameters["@UserID"].Value = volunteerStatus.UserID;
            cmd.Parameters["@ProjectID"].Value = volunteerStatus.ProjectID;
            cmd.Parameters["@Approved"].Value = volunteerStatus.Approved;

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
        /// Created: 2025/02/08
        /// 
        /// The method for deactivating volunteers
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int DeactivateVolunteerByUserIDAndProjectID(int userID, int projectID)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetConnection();
            // command
            var cmd = new SqlCommand("sp_deactivate_volunteer_by_userid_and_projectid", conn);
            // type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            // values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ProjectID"].Value = projectID;
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
        /// Created: 2025/03/12
        /// 
        /// The method for selecting volunteer status by userID and projectID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Syler Bushlack
        /// Updated: 2025/04/17 
        /// What was Changed: Fixed approved value not accepting nulls from the database
        /// </remarks> 
        public VolunteerStatus SelectVolunteerStatusByUserIDAndProjectID(int userID, int projectID)
        {
            VolunteerStatus volunteerStatus = null;

            // connection
            var conn = DBConnection.GetConnection();
            // command
            var cmd = new SqlCommand("sp_select_volunteer_status_by_user_and_project_id", conn);
            // type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            // values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ProjectID"].Value = projectID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    volunteerStatus = new VolunteerStatus()
                    {
                        UserID = reader.GetInt32(0),
                        ProjectID = reader.GetInt32(1),
                        Approved = reader.IsDBNull(2) ? null : reader.GetBoolean(2)
                    };
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

            return volunteerStatus;
        }

        // Author: Akoi Kollie
        public int AddVolunteerStatus(int userid, int projectid)
        {
            int row = 0;
            //get the connection
            var conn = DBConnection.GetConnection();
            //a command to execute the store procedure
            var cmd = new SqlCommand("sp_insert_volunteer", conn);
            // tell the command type to use
            cmd.CommandType = CommandType.StoredProcedure;
            //add parameters to the command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            // set the values for the parameters
            cmd.Parameters["@UserID"].Value = userid;
            cmd.Parameters["@ProjectID"].Value = projectid;

            try
            {
                //Open connection
                conn.Open();
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return row;
        }

        // Author: Akoi Kollie
        public List<VolunteerStatus> SelectVolunteerStatusByProjectID(int projectid)
        {
            List<VolunteerStatus> volunteerStatuses = new List<VolunteerStatus>();
            var conn = DBConnection.GetConnection();
            //a command to execute the store procedure
            var cmd = new SqlCommand("sp_select_all_volunteers_by_projectid", conn);
            // tell the command type to use
            cmd.CommandType = CommandType.StoredProcedure;
            //add parameters to the command
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            // set the values for the parameter
            cmd.Parameters["@ProjectID"].Value = projectid;

            try
            {
                //Open connection
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    VolunteerStatus volunteerStatus = new VolunteerStatus();
                    volunteerStatus.ProjectID = r.GetInt32(0);
                    volunteerStatus.UserID = r.GetInt32(1);
                    volunteerStatus.Approved = r.IsDBNull(2) ? null : r.GetBoolean(2);
                    volunteerStatuses.Add(volunteerStatus);
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
            return volunteerStatuses;
        }
    }
}
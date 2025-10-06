/// <summary>
/// Ellie Wacker
/// Created: 2025-02-08
/// 
/// Class that implements the IVolunteerStatusProjectRoleAccessor Interface - used for
/// accessing VolunteerStatusProjectRole data from the DB.
/// </summary>

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
    public class VolunteerStatusProjectRoleAccessor : IVolunteerStatusProjectRoleAccessor
    {
        /// <summary>
        /// Eric Idle
        /// Created: 2025/04/01
        /// 
        /// The Data Access Method for adding a user's project role for a certain project
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int AddUserRoles(int userID, int projectID, string projectRoleID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_projectrole_by_userid_projectid_projectroleid", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Adding all the parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters["@ProjectID"].Value = projectID;

            cmd.Parameters.Add("@ProjectRoleID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@ProjectRoleID"].Value = projectRoleID;

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

        public int DeleteUserProjectRoleByUserIDProjectID(int userID, int projectID, string projectRoleID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_delete_projectrole_by_userid_projectid_projectroleid", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters["@ProjectID"].Value = projectID;

            cmd.Parameters.Add("@ProjectRoleID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@ProjectRoleID"].Value = projectRoleID;

            try
            {
                conn.Open();
                var r = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }



        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/08
        /// 
        /// The method for deleting user roles
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int DeleteUserRoles(int userID, int projectID)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetConnection();
            // command
            var cmd = new SqlCommand("sp_delete_user_roles", conn);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Eric Idle
        /// Created: 2025/04/01
        /// 
        /// The Data Access Method for finding all user's project roles for a certain project
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public List<VolunteerStatusProjectRole> SelectUserProjectRolesByUserIDProjectID(int userID, int projectID)
        {
            List<VolunteerStatusProjectRole> userProjectRoles = new List<VolunteerStatusProjectRole>();

            // connection
            var conn = DBConnection.GetConnection();

            //command
            var cmd = new SqlCommand("sp_select_volunteer_projectroles_by_userid_projectid", conn);

            //command type
            cmd.CommandType = CommandType.StoredProcedure;

            //parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);

            //values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ProjectID"].Value = projectID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var userProjectRole = new VolunteerStatusProjectRole()
                        {
                            UserID = reader.GetInt32(0),
                            ProjectID = reader.GetInt32(1),
                            ProjectRoleID = reader.GetString(2)
                        };
                        userProjectRoles.Add(userProjectRole);
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

            return userProjectRoles;
        }
    }
}
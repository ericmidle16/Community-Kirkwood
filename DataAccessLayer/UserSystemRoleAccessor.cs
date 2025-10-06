/// <summary>
/// Ellie Wacker
/// Created: 2025-03-05
/// 
/// Class that implements the IUserSystemRoleAccessor Interface - used for
/// accessing UserSystemRole data from the DB.
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
    public class UserSystemRoleAccessor : IUserSystemRoleAccessor
    {
        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/06
        /// 
        /// The Data Access Method for adding a userSystemRole 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int AddUserSystemRole(int userID, string systemRoleID)
        {

            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_user_system_role", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Adding all the parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            cmd.Parameters.Add("@SystemRoleID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@SystemRoleID"].Value = systemRoleID;

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
        /// Eric Idle
        /// Created: 2025/04/01
        /// 
        /// The Data Access Method for deleting a userSystemRole 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int DeleteUserSystemRole(int userID, string systemRoleID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_delete_user_systemrole_by_userid", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            cmd.Parameters.Add("@SystemRoleID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@SystemRoleID"].Value = systemRoleID;

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
        /// Created: 2025/03/06
        /// 
        /// The Data Access Method for selecting userSystemRoles with a certain user id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public List<UserSystemRole> SelectUserSystemRolesByUserID(int userID)
        {
            List<UserSystemRole> userSystemRoles = new List<UserSystemRole>();

            // connection
            var conn = DBConnection.GetConnection();

            //command
            var cmd = new SqlCommand("sp_select_user_system_roles_by_userID", conn);

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
                        var userSystemRole = new UserSystemRole()
                        {
                            UserID = reader.GetInt32(0),
                            SystemRoleID = reader.GetString(1)
                        };
                        userSystemRoles.Add(userSystemRole);
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

            return userSystemRoles;
        }
    }
}

/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/04/18
/// Summary: This code handles all the ForumPermissions stored procedures. 
/// 
/// Updated By: Syler Bushlack
/// Updated: 2025-04-24
/// What Was Changed: Added UpdateForumPermissionWriteAccessValue and SelectForumPermissionByProjectID method
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed: Added the InsertForumPermission method.
/// </summary>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class ForumPermissionAccessor : IForumPermissionAccessor
    {
        // Author: Kate Rich
        public int InsertForumPermission(ForumPermission forumPermission)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_forumPermission", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters.Add("@WriteAccess", SqlDbType.Bit);
            cmd.Parameters["@UserID"].Value = forumPermission.UserID;
            cmd.Parameters["@ProjectID"].Value = forumPermission.ProjectID;
            cmd.Parameters["@WriteAccess"].Value = forumPermission.WriteAccess;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        // Author: Syler Bushlack
        public List<ForumPermissionVM> SelectForumPermissionByProjectID(int projectID)
        {
            List<ForumPermissionVM> forumPermissions = new List<ForumPermissionVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_forumpermission_by_projectID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectID;
            cmd.Parameters["@ProjectID"].Value = projectID;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ForumPermissionVM f = new ForumPermissionVM();
                    f.UserID = r.GetInt32(0);
                    f.ProjectID = r.GetInt32(1);
                    f.WriteAccess = r.GetBoolean(2);
                    f.GivenName = r.GetString(3);
                    forumPermissions.Add(f);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error getting forum permissions.", ex);
            }
            return forumPermissions;
        }

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/04/18
        /// Summary: Executes the stored procedure 
        /// "sp_select_user_write_access to select the user's 
        /// write access for a project
        /// Last Upaded By:
        /// Last Updated: 
        /// What Was Changed: 
        /// </summary>
        public bool SelectUserWriteAccess(int userID, int projectID)
        {
            bool hasAccess = false;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_select_user_write_access", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectID;

                    try
                    {
                        conn.Open();
                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        hasAccess = result == 1;
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error checking write access", ex);
                    }
                }
            }

            return hasAccess;
        }

        // Author: Syler Bushlack
        public int UpdateForumPermissionWriteAccessValue(ForumPermission forumPermission)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_forumpermission_writeaccess_value", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters.Add("@WriteAccess", SqlDbType.Bit);
            cmd.Parameters["@UserID"].Value = forumPermission.UserID;
            cmd.Parameters["@ProjectID"].Value = forumPermission.ProjectID;
            cmd.Parameters["@WriteAccess"].Value = forumPermission.WriteAccess;

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
    }
}
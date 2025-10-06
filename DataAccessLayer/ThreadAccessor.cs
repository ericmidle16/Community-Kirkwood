/// <summary>
/// Created By: Jackson Manternach
/// Created: 2025/03/13
/// 
/// Data access methods for threads
/// </summary>
/// 
/// <remarks>
/// Updater Name: Skyann Heintz 
/// Updated: 2025-04-04
/// What Changed: Added InsertForumPost
/// </remarks>

using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ThreadAccessor : IThreadAccessor
    {

        /// <summary>
        /// Created By: Jackson Manternach
        /// Created: 2025/03/13
        /// 
        /// Method that retrieves all threads by a ProjectID
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Last Updated By: Kate Rich
        /// Last Updated: 2025-04-18
        /// What Was Changed:
        ///     Added code to retrieve the ThreadID of each thread pulled from the DB.
        /// </remarks>
        /// 
        /// <param name="projectID">Represents the ID of a specific project</param>
        public List<ThreadVM> SelectThreadByProjectID(int projectID)
        {
            List<ThreadVM> threads = new List<ThreadVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_threads_by_projectid", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters["@ProjectID"].Value = projectID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ThreadVM thread = new ThreadVM()
                        {
                            ThreadID = reader.GetInt32(0),
                            DatePosted = reader.GetDateTime(1),
                            ThreadName = reader.GetString(2),
                            UserID = reader.GetInt32(3),
                            GivenName = reader.GetString(4),
                            FamilyName = reader.GetString(5)
                        };
                        threads.Add(thread);
                    }
                }
                else
                {
                    throw new ArgumentException("No threads found in this project");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve data", ex);
            }
            finally
            {
                conn.Close();
            }

            return threads;
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/02
        /// Summary: Executes the stored procedure sp_insert_forum_post to insert a new thread post 
        /// into the database if the user has write access to the specified thread.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int InsertForumPost(int userID, string content, int projectID, string threadName, DateTime datePosted)
        {
            int rowsAffected = 0;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_insert_post", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@Content", SqlDbType.NVarChar, 500).Value = content;
                    cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectID;
                    cmd.Parameters.Add("@ThreadName", SqlDbType.NVarChar, 100).Value = threadName;
                    cmd.Parameters.Add("@DatePosted", SqlDbType.DateTime).Value = datePosted;

                    try
                    {
                        conn.Open();
                        // Executes the stored procedure and returns the number of rows affected
                        rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Insert Forum Post failed", ex);
                    }
                }
            }
            return rowsAffected;
        }
      
    }

}

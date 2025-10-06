/// <summary>
/// Created By:Nikolas Bell
/// Created: 2025-03-14
/// Summary:
///     Class that implements the ILocationAccessor Interface - used for
///     accessing Post data from the DB.
///     
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-03-31
/// What was Changed: Added InsertForumPost and Edit ForumPost code
/// 
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-04
/// What was Changed: Removed InsertForumPost
/// 
/// Last Updated By: Nik Bell
/// Last Updated: 2025-04-18
/// What was Changed: Added create and delete forum posts
/// 
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-18
/// What was Changed: Added select single post by post id
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-23
/// What was Changed: Added UpdatePostPinnedValue method
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-23
/// What was Changed: Fixed bug in the EditForumPost method
/// </summary>


using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PostAccessor : IPostAccessor
    {
        IEnumerable<PostVM> IPostAccessor.GetAllThreadPosts(int threadID)
        {
            List<PostVM> result = new List<PostVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_posts_by_threadID", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PostVM post = new PostVM(
                    reader.GetInt32(reader.GetOrdinal("PostID")),
                    reader.GetInt32(reader.GetOrdinal("UserID")),
                    reader.GetInt32(reader.GetOrdinal("ThreadID")),
                    reader.GetBoolean(reader.GetOrdinal("Reply")),
                    reader.GetBoolean(reader.GetOrdinal("Edited")),
                    reader.GetBoolean(reader.GetOrdinal("Pinned")),
                    reader.GetString(reader.GetOrdinal("Content")),
                    reader.GetDateTime(reader.GetOrdinal("DatePosted")),
                    reader.GetString(reader.GetOrdinal("GivenName")),
                    reader.GetString(reader.GetOrdinal("FamilyName"))
                    );
                    result.Add(post);
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not retrieve thread data.", ex);
            }
            return result;
        }



        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/02
        /// Summary: Executes the stored procedure sp_edit_forum_post to update a forum post's content and pinned status 
        /// if the user has the appropriate permissions.
        /// Last Updated By: Syler Bushlack
        /// Last Updated: 2025/04/30
        /// What Was Changed: Changed the rows affected code so that it returns one on a sucessful update
        /// </summary>
        public int EditForumPost(int postID, int userID, string newContent)
        {
            int rowsAffected = 0;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_edit_post", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@NewContent", SqlDbType.NVarChar, 500).Value = newContent;

                    try
                    {
                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                        //rowsAffected = Convert.ToInt32(cmd.ExecuteScalar()); // Gets the number of rows affected


                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Edit Forum Post failed", ex);
                    }
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Creator: Nik Bell
        /// Created: 2025/04/10
        /// Summary: Executes the stored procedure sp_delete_post_by_postID to deletes a post with the specified id
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int DeleteForumPost(int postID)
        {
            int rowsAffected = 0;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_delete_post_by_postID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;

                    try
                    {
                        conn.Open();
                        rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Delete Forum Post failed", ex);
                    }
                }
            }
            return rowsAffected;
        }

        /// <summary>
        /// Creator: Nik Bell
        /// Created: 2025/04/10
        /// Summary: Executes the stored procedure sp_reply_by_threadId to create a new forum post in a specified thread.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int CreateForumPost(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Content))
            {
                throw new ArgumentException("Post content cannot be empty.");
            }
            int rowsAffected = 0;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_reply_by_threadId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = post.UserID;
                    cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = post.ThreadID;
                    cmd.Parameters.Add("@Content", SqlDbType.NVarChar, 500).Value = post.Content;

                    try
                    {
                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery(); ;
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Create Forum Post failed", ex);
                    }
                }
            }
            return rowsAffected;
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/21
        /// Summary: Executes the stored procedure sp_select_post_by_id to retrieve a single post by its PostID.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public PostVM SelectPostByID(int postID)
        {
            PostVM post = null;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_select_post_by_id", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            post = new PostVM(
                                reader.GetInt32(reader.GetOrdinal("PostID")),
                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("ThreadID")),
                                reader.GetBoolean(reader.GetOrdinal("Reply")),
                                reader.GetBoolean(reader.GetOrdinal("Edited")),
                                reader.GetBoolean(reader.GetOrdinal("Pinned")),
                                reader.GetString(reader.GetOrdinal("Content")),
                                reader.GetDateTime(reader.GetOrdinal("DatePosted")),
                                reader.GetString(reader.GetOrdinal("GivenName")),
                                reader.GetString(reader.GetOrdinal("FamilyName"))
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Select Post By ID failed", ex);
                    }
                }
            }

            return post;
        }

        // Author: Syler Bushlack
        public int UpdatePostPinnedValue(Post post)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_post_pinned_value", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pinned", SqlDbType.Bit);
            cmd.Parameters.Add("@PostID", SqlDbType.Int);
            cmd.Parameters["@Pinned"].Value = post.Pinned;
            cmd.Parameters["@PostID"].Value = post.PostID;

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
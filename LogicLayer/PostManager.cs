/// <summary>
/// Nikolas Bell
/// Created: 2025-03-14
/// 
/// Summary: This PostManager class implements IPostManager
/// and handles operations related to forum posts.
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-03-31
/// What Was Changed: Added InsertForumPost, EditForumPost
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-04
/// What Was Changed: Removed InsertForumPost
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
/// What was Changed: Added EditPostPinnedValue method
/// </summary>
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class PostManager : IPostManager
    {
        private IPostAccessor _postAccessor;

        public PostManager()
        {
            _postAccessor = new PostAccessor();
        }
        public PostManager(IPostAccessor postAccessor)
        {
            _postAccessor = postAccessor;
        }
        public IEnumerable<PostVM> GetAllThreadPosts(int threadID)
        {
            return _postAccessor.GetAllThreadPosts(threadID);
        }


        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/03
        /// Summary: Updates an existing forum post if the user has permission.
        /// Returns true if the post was successfully edited, otherwise false.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool EditForumPost(int postID, int userID, string newContent)
        {
            try
            {
                int rowsAffected = _postAccessor.EditForumPost(postID, userID, newContent);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Post editing failed.", ex);
            }
        }

        ///<summary>
        /// Creator: Nik Bell
        /// Created: 2025/04/03
        /// Summary: Deletes an existing forum post.
        /// Returns true if the post was successfully deleted, otherwise false.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool DeleteForumPost(int postID)
        {
            return (1 == _postAccessor.DeleteForumPost(postID));
        }

        ///<summary>
        /// Creator: Nik Bell
        /// Created: 2025/04/03
        /// Summary: Adds a new forum post to the database
        /// Returns true if the post was successfully created, otherwise false.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool CreateForumPost(Post post)
        {
            return (1 == _postAccessor.CreateForumPost(post));
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/21
        /// Summary: Retrieves a specific forum post by its ID.
        /// Returns the PostVM object representing the forum post.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public PostVM SelectPostByID(int postID)
        {
            try
            {
                return _postAccessor.SelectPostByID(postID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve post by ID.", ex);
            }
        }

        // Author: Syler Bushlack
        public bool EditPostPinnedValue(Post post)
        {
            try
            {
                int rowsAffected = _postAccessor.UpdatePostPinnedValue(post);
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Pinning post failed.", ex);
            }
        }
    }
}

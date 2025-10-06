/// <summary>
/// Creator: Nikolas Bell
/// Created: 2025-03-14
/// Summary: Defines the IForumPostAccessor interface, 
/// which includes a method for inserting a new forum post 
/// into the database if the user has write access to the forum.
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-03-31
/// What Was Changed: Added InsertForumPost and EditForumPost
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-04
/// What Was Changed: Removed InsertFourmPost
/// 
/// Last Updated By: Nik Bell
/// Last Updated: 2025-04-18
/// What was Changed: Added create and delete forum posts
/// 
/// /// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-18
/// What was Changed: Added select single post by post id
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-23
/// What was Changed: Added UpdatePostPinnedValue method
/// </summary>
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IPostAccessor
    {
        public IEnumerable<PostVM> GetAllThreadPosts(int threadID);
        int EditForumPost(int postID, int userID, string newContent);
        int DeleteForumPost(int postID);
        int CreateForumPost(Post post);
        PostVM SelectPostByID(int postID);

        // Author: Syler Bushlack
        int UpdatePostPinnedValue(Post post);
    }
}

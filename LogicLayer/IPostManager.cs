/// <summary>
/// Creator: Nikolas Bell
/// Created: 2025-03-14
/// Summary: Defines the IPostManager interface
/// in the LogicLayer namespace, which includes 
/// methods for inserting a forum post, editing 
/// old post, and selecting the user's posts 
/// to test other features, and GetAllThreadPosts.
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-03-31
/// What Was Changed: Added comment, Added InsertForumPost, and EditForumPost
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-04
/// What Was Changed: Remvoed InsertForumPost
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
/// </summary>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IPostManager
    {
        public IEnumerable<PostVM> GetAllThreadPosts(int threadID);
        bool EditForumPost(int postID, int userID, string newContent);
        bool DeleteForumPost(int postID);
        bool CreateForumPost(Post post);
        PostVM SelectPostByID(int postID);

        // Author: Syler Bushlack
        bool EditPostPinnedValue(Post post);
    }
}

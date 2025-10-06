/// <summary>
/// Creator: Nikolas Bell
/// Created: 2025-03-14
/// Summary: Class for the PostAccessorFake class. 
/// Initializes a list of fake forum post records to simulate the behavior of the forum post data access layer.
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-01
/// What Was Changed: Added test fakes and Insert and Edit forum code.
/// 
/// Last Updated By: Nik Bell
/// Last Updated: 2025-04-18
/// What was Changed: Added create and delete forum posts
///
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-21
/// What was Changed: Added SelectPostById
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-23
/// What was Changed: Added UpdatePostPinnedValue method
/// </summary>
using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class PostAccessorFake : IPostAccessor
    {

        private List<PostVM> _refactoredPosts;
        private List<Post> _forumPosts;
        private Post _lastAddedPost;

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/02
        /// Summary: Constructor for the ForumPostAccessorFake class. 
        /// Initializes a list of fake forum post records to simulate the behavior of the forum post data access layer.
        /// Last Updated By: Nik Bell
        /// Last Updated: 2025-04-11
        /// What Was Changed: Added a refactored list so testing is less stupid.
        /// </summary>
        public PostAccessorFake()
        {
            _forumPosts = new List<Post>()
            {
                new Post()
                {
                    PostID = 1,
                    ThreadID = 100000,
                    UserID = 100002,
                    ProjectID = 100000,
                    Content = "This is a test post.",
                    DatePosted = DateTime.Now
                },
                new Post()
                {
                    PostID = 2,
                    ThreadID = 100000,
                    UserID = 100003,
                    ProjectID = 100000,
                    Content = "Another test post.",
                    DatePosted = DateTime.Now
                }
            };

            _refactoredPosts = new List<PostVM>
            {
            new PostVM(1, 1, 100000, false, false, false, "This is a fake post.", DateTime.Now, "John", "Doe"),
            new PostVM(2, 2, 100000, true, true, false, "This is a reply.", DateTime.Now.AddMinutes(30), "Jane", "Smith"),
            new PostVM(3, 3, 100000, false, false, true, "This post is pinned.", DateTime.Now.AddHours(1), "Alice", "Johnson")
        };
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/03/02
        /// Summary: Edits an existing forum post for a given forum and user. 
        /// Validates input values, ensuring the content is not empty and does not exceed character limits.
        /// Throws an exception if any validation
        /// fails or if a post with the same content exists for the user in the forum.
        /// </summary>
        public int EditForumPost(int postID, int userID, string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
            {
                throw new ApplicationException("Please enter content for your post.");
            }

            if (newContent.Length > 500)
            {
                throw new ApplicationException("Your post cannot exceed 500 characters.");
            }

            var postToEdit = _forumPosts.FirstOrDefault(p => p.PostID == postID && p.UserID == userID);

            if (postToEdit == null)
            {
                throw new ApplicationException("Forum post not found or user does not have permission to edit.");
            }

            postToEdit.Content = newContent;
            postToEdit.DatePosted = DateTime.Now; // Simulating an update timestamp

            return 1; // Simulate success by returning 1 to indicate one row affected

        }

        IEnumerable<PostVM> IPostAccessor.GetAllThreadPosts(int threadID)
        {
            return _refactoredPosts;
        }

        /// <summary>
        /// Creator: Nik Bell
        /// Created: 2025/03/02
        /// Summary: Deletes a specidfied post.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int DeleteForumPost(int postID)
        {
            int index = _refactoredPosts.FindIndex(p => p.PostID == postID);
            if (index >= 0)
            {
                _refactoredPosts.RemoveAt(index);
                return 1;
            }
            return 2;
        }

        /// <summary>
        /// Creator: Nik Bell
        /// Created: 2025/03/02
        /// Summary: Creates a post.
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

            PostVM postVM = new PostVM(
                _refactoredPosts.Count + 100000,
                post.UserID,
                post.ThreadID,
                false,
                false,
                false,
                post.Content,
                DateTime.Now,
                "John",
                "Doe");

            _refactoredPosts.Add(postVM);
            return 1;
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/21
        /// Summary: Selects a post by the id
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public PostVM SelectPostByID(int postID)
        {
            var post = _refactoredPosts.FirstOrDefault(p => p.PostID == postID);
            if (post == null)
            {
                throw new ApplicationException($"Post with ID {postID} not found.");
            }
            return post;
        }

        /// <summary>
        /// Creator: Syler Bushlack
        /// Created: 2025/04/23
        /// Summary: Updates the pinned value of a post
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int UpdatePostPinnedValue(Post newPost)
        {
            if (newPost == null)
            {
                throw new ApplicationException("Bad data");
            }

            int rowsAffected = 0;
            foreach (Post oldPost in _forumPosts)
            {
                if (oldPost.PostID == newPost.PostID)
                {
                    oldPost.Pinned = newPost.Pinned;
                    rowsAffected++;
                    break;
                }
            }
            return rowsAffected;
        }
    }
}

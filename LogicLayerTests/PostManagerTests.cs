/// <summary>
/// Creator: Nikolas Bell
/// Created: 2025-03-14
/// Summary: Unit tests for the ForumPostManager class.
/// Last Updated By:Skyann Heintz
/// Last Updated: 2025-03-31
/// What Was Changed: Added Insert and Edit unit tests.
/// 
/// Last Updated By:Skyann Heintz
/// Last Updated: 2025-04-21
/// What Was Changed: Added tests for SelectPostByID
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-23
/// What Was Changed: Added tests for EditPostPinnedValue
/// </summary>
using DataAccessFakes;
using DataAccessLayer;
using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class PostManagerTests
    {
        private IPostManager _postManager;

        [TestInitialize]
        public void Setup()
        {
            _postManager = new PostManager(new PostAccessorFake());
        }

        [TestMethod]
        public void TestSelectPostByThreadID()
        {
            // Act
            List<PostVM> members = (List<PostVM>)_postManager.GetAllThreadPosts(100000);

            // Assert
            Assert.IsNotNull(members, "Post list should not be null.");
        }


        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests updating a post should pass
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        public void TestEditForumPostReturnsSuccess()
        {
            // Arrange
            int postID = 1; // Existing post ID from ForumPostAccessorFake
            int userID = 100002; // Matching userID for the post
            string newContent = "This is the updated content.";

            // Act
            bool result = _postManager.EditForumPost(postID, userID, newContent);

            // Assert
            Assert.IsTrue(result, "Expected EditForumPost to return true for a successful edit.");
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests a user trying to update a none exisitng post
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditNonExistentPostThrowsException()
        {
            int userID = 9999;
            int postID = 100002;
            string content = "Updated content";
            // Attempting to edit a non-existent forum post
            _postManager.EditForumPost(userID, postID, content);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests a user trying to update someone else's post
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditPostWithInvalidUserThrowsException()
        {
            int userID = 1;
            int postID = 100006;
            string content = "Updated content";
            // Attempting to edit an existing post with a wrong user ID
            _postManager.EditForumPost(userID, postID, content);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests empty content throws an exception
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditEmptyContentThrowsException()
        {
            int userID = 100003;
            int postID = 100006;
            string content = "";
            _postManager.EditForumPost(userID, postID, content);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests too long conent should thorw exception
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditTooLongContentThrowsException()
        {
            int userID = 100003;
            int postID = 100006;

            string content = new string('A', 501);
            _postManager.EditForumPost(userID, postID, content);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/22
        /// Summary: Test for successfully selecting a post by its ID.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed: 
        /// </summary>
        [TestMethod]
        public void TestSelectPostByIDReturnsCorrectPost()
        {
            // Arrange
            int postID = 1; 

            // Act
            PostVM result = _postManager.SelectPostByID(postID);

            // Assert
            Assert.IsNotNull(result, "Expected post to be returned but got null.");
            Assert.AreEqual(postID, result.PostID, "Returned post does not match the requested ID.");
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/04/22
        /// Summary: Test selecting a post by an ID that doesn't exist should throw an exception.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed: 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectPostByInvalidIDThrowsException()
        {
            // Arrange
            int invalidPostID = 999999;

            // Act
            _postManager.SelectPostByID(invalidPostID);
        }

        /// <summary>
        /// Creator: Syler Bushlack
        /// Created: 2025/04/23
        /// Summary: Test updating a post's pinned value returns truw if succeeded
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed: 
        /// </summary>
        [TestMethod]
        public void TestEditPostPinnedValueUpdatesPinnedValue()
        {
            // Arrange
            Post post = new Post();
            post.PostID = 1;
            post.Pinned = true;

            // Act
            bool result = _postManager.EditPostPinnedValue(post);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Creator: Syler Bushlack
        /// Created: 2025/04/23
        /// Summary: Test updating a post's pinned value fails when given a invalid post object
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed: 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditPostPinnedValueFails()
        {
            // Arrange
            Post post = null;

            // Act
            _postManager.EditPostPinnedValue(post);
        }
    }
}

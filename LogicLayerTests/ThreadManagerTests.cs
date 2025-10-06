/// <summary>
/// Created By: Jackson Manternach
/// Created: 2025/03/14
/// 
/// Test methods for the ViewProjectForum feature
/// </summary>
/// 
/// <remarks>
/// Updater Name: Skyann Heintz
/// Updated: 2025-04-04
/// What Changed: Added InsertForumPost tests
/// Updater Name: Skyann Heintz
/// Updated: 2025-04-21
/// What Changed: Renamed to ThreadManagerTests
/// </remarks>

using DataAccessFakes;
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
    public class ThreadManagerTests
    {
        private IThreadManager? _threadManager;

        [TestInitialize]
        public void TestSetup()
        {
            _threadManager = new ThreadManager(new ThreadAccessorFake());
        }

        /// <summary>
        /// Created By: Jackson Manternach
        /// Created: 2025/03/14
        /// 
        /// Test method that tests if the GetThreadsByProjectID method returns valid data
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name:
        /// Updated:
        /// </remarks>
        [TestMethod]
        public void TestSelectThreadByProjectIDReturnsTrue()
        {
            // Arrange
            int projectID = 1;
            int expectedCount = 1;

            // Act
            List<ThreadVM> result = _threadManager.GetThreadsByProjectID(projectID);

            // Assert
            Assert.AreEqual(expectedCount, result.Count);
            Assert.IsTrue(result.TrueForAll(a => a.ProjectID == projectID));
        }

        /// <summary>
        /// Created By: Jackson Manternach
        /// Created: 2025/03/14
        /// 
        /// Test method that tests if that GetThreadsByProjectID method returns an exception if it cannot 
        /// find valid data
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name:
        /// Updated:
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectThreadByProjectIDReturnsException()
        {
            // Arrange
            int projectID = 4; // Non-existing project

            _threadManager.GetThreadsByProjectID(projectID);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests inserting a valid thread post 
        /// Last Updated By: 
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        public void TestInsertForumPostReturnsSuccess()
        {

            int userID = 100004;
            string content = "This is a new forum post for testing.";
            int projectID = 100001;
            string threadName = "Insert Thread";
            DateTime datePosted = DateTime.Now;

            bool result = _threadManager.InsertForumPost(userID, content, projectID, threadName, datePosted);

            Assert.IsTrue(result, "Expected InsertForumPost to return true for a successful insertion.");
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests empty content should throw exception
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEmptyContentThrowsException()
        {
            int userID = 100002;
            int projectID = 100005;
            string content = "";
            string threadName = "Empty Content Thread";
            DateTime datePosted = DateTime.Now;

            _threadManager.InsertForumPost(userID, content, projectID, threadName, datePosted);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests inserting too long of content should throw exception
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestTooLongContentThrowsException()
        {

            int userID = 100003;
            int projectID = 100006;
            string content = new string('A', 501);
            string threadName = "Long Content Thread";
            DateTime datePosted = DateTime.Now;


            _threadManager.InsertForumPost(userID, content, projectID, threadName, datePosted);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests inserting too long of thread name should throw exception 
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestTooLongThreadNameThrowsException()
        {

            int userID = 100003;
            int projectID = 100006;
            string content = "Too long thread name";
            string threadName = new string('A', 101);
            DateTime datePosted = DateTime.Now;


            _threadManager.InsertForumPost(userID, content, projectID, threadName, datePosted);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Tests empty thread name should return exception
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEmptyThreadNameThrowsException()
        {
            int userID = 100002;
            int projectID = 100005;
            string content = "Empty Thread Name";
            string threadName = "";
            DateTime datePosted = DateTime.Now;

            _threadManager.InsertForumPost(userID, content, projectID, threadName, datePosted);
        }
    }
}
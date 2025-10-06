/// <summary>
/// Creator: Skyann Heintz
/// Created: 2025/02/12
/// Summary: Unit tests for the AvailabilityManager class to test inserting and selecting availability.
/// Last Updated By:
/// Last Updated:
/// What Was Changed: Added tests from viewAvailabilityTests. Tests added were
/// TestSelectAvailabilityByUserValidUserIDReturnsTrue and TestSelectAvailabilityByUserInvalidUserIDThrowsException
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-11
/// What Was Changed: TestGetAvailabilityVMsByProjectID and TestGetAvailabilityVMsByUnknownProjectID
/// </summary>
using DataAccessFakes;
using DataDomain;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class AvailabilityTests
    {
        private IAvailabilityManager? _availabilityManager;

        [TestInitialize]
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/12
        /// Summary: This is a unit test setup method for 
        /// initializing _availabilityManager with a fake data accessor before running tests.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void Setup()
        {
            _availabilityManager = new AvailabilityManager(new AvailabilityAccessorFake());
        }

        [TestMethod]
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/12
        /// Summary: The unit test verifies that inserting availability returns true for valid data.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertAvailabilityReturnsTrue()
        {
            var availability = new Availability
            {
                UserID = 4,
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddDays(7)
            };

            // Provide the missing boolean arguments (assuming they are 'isAvailable' and another flag)
            bool isAvailable = true;  // or whatever value fits your logic
            bool someOtherFlag = false;  // or whatever value fits your logic

            bool actualResult = _availabilityManager.InsertAvailability(
                availability.UserID,
                isAvailable,
                someOtherFlag,
                availability.StartDate,
                availability.EndDate
            );

            Assert.IsTrue(actualResult, "Expected the availability insertion to succeed.");
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/12
        /// Summary: The unit test verifies that an ApplicationException is thrown 
        /// when attempting to insert availability with an invalid date range 
        /// (EndDate is earlier than StartDate).
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertAvailabilityInvalidDateRangeThrowsException()
        {
            var availability = new Availability
            {
                UserID = 4,
                StartDate = DateTime.Now.AddDays(7),  // StartDate is in the future
                EndDate = DateTime.Now  // EndDate is in the past, which is invalid
            };

            _availabilityManager.InsertAvailability(availability.UserID, true, false, availability.StartDate, availability.EndDate);
        }


        [TestMethod]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/12
        /// Summary: This test verifies that SelectExistingAvailability returns false when no availability exists for the given user and date range.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestSelectExistingAvailabilityReturnsFalseForNonExistingAvailability()
        {
            int userID = 2;
            DateTime startDate = new DateTime(2025, 2, 10, 18, 0, 0); 
            DateTime endDate = new DateTime(2025, 2, 10, 19, 0, 0); 

            bool isAvailabilityFound = _availabilityManager.SelectExistingAvailability(userID, startDate, endDate);

            Assert.IsFalse(isAvailabilityFound, "Expected SelectExistingAvailability to return false for a non-existing availability record.");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/12
        /// Summary: This test verifies that if the userID is invalid that an exception will be thorwn
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertAvailabilityInvalidUserIDThrowsException()
        {
            _availabilityManager.InsertAvailability(0, true, false, DateTime.Now, DateTime.Now.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/12
        /// Summary: This test verifies that InsertAvailability throws an exception when StartDate is equal to or after EndDate.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertAvailabilityStartDateAfterEndDateThrowsException()
        {
            _availabilityManager.InsertAvailability(1, true, false, DateTime.Now.AddDays(1), DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/12
        /// Summary: This test verifies that InsertAvailability throws an exception when StartDate is in the past.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertAvailabilityStartDateInPastThrowsException()
        {
            _availabilityManager.InsertAvailability(1, true, false, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/12
        /// Summary: This test verifies that InsertAvailability throws an exception when an overlapping availability exists for the user.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        public void TestInsertAvailabilityOverlappingAvailabilityThrowsException()
        {
            _availabilityManager.InsertAvailability(1, true, false, DateTime.Now.AddHours(1), DateTime.Now.AddHours(2));

            _availabilityManager.InsertAvailability(1, true, false, DateTime.Now.AddHours(1), DateTime.Now.AddHours(3));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/12
        /// Summary: This test verifies that InsertAvailability throws an exception when user id is empty.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        public void InsertAvailabilityShouldThrowExceptionWhenUserIDIsWhitespace()
        {
            int userID = 0; 
            bool isAvailable = true;
            bool repeatWeekly = false;
            DateTime startDate = DateTime.Now.AddDays(1);
            DateTime endDate = DateTime.Now.AddDays(2);

            _availabilityManager.InsertAvailability(userID, isAvailable, repeatWeekly, startDate, endDate);
        }


        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/24
        /// Summary: This test verifies viewing a user by their userID returns true
        /// when it occurs. So that they can view their avaliability
        /// Last Updated By: Skyann Heintz
        /// Last Updated: 2025-03-12
        /// What Was Changed: Upadated the test to have the right userID and the right
        /// expected count
        /// </summary>
        [TestMethod]
        public void TestSelectAvailabilityByUserValidUserIDReturnsTrue()
        {
            // Arrange
            int userID = 1;
            int expectedCount = 1;

            // Act
            List<Availability> result = _availabilityManager.SelectAvailabilityByUser(userID);

            // Assert
            Assert.AreEqual(expectedCount, result.Count);
            Assert.IsTrue(result.TrueForAll(a => a.UserID == userID));
        }
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: This test verifies that an exception is thrown when their
        /// is no user with the userID being called.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectAvailabilityByUserInvalidUserIDThrowsException()
        {
            // Arrange
            int userID = 999999; // Non-existing user

            _availabilityManager.SelectAvailabilityByUser(userID);
        }
        [TestMethod]
        //AUTHOR: Dat Tran
        public void TestGivenNameReturnsAvailableUsers()
        {
            const bool availability = true;
            List<UserAvailability> available = new List<UserAvailability>();
            const int expectedCount = 2;
            const int projectID = 1;
            int actualCount = 0;
            available = _availabilityManager.GetAvailableUsers(availability, projectID);
            actualCount = available.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        //AUTHOR: Dat Tran
        public void TestGivenNameReturnsUnavailableUsers()
        {
            const bool availability = false;
            List<UserAvailability> available = new List<UserAvailability>();
            const int expectedCount = 1;
            const int projectID = 1;
            int actualCount = 0;
            available = _availabilityManager.GetAvailableUsers(availability, projectID);
            actualCount = available.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        // Author: Chase Hannen
        public void TestGetAvailabilityVMsByProjectID()
        {
            // Arrange
            const int projectID = 1;
            const int expectedCount = 5;
            int actualCount = 0;
            List<AvailabilityVM> availabilityVMs = new List<AvailabilityVM>();
            // Act
            availabilityVMs = _availabilityManager.SelectAvailabilityVMByProjectID(projectID);
            actualCount = availabilityVMs.Count;
            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        // Author: Chase Hannen
        public void TestGetAvailabilityVMsByUnknownProjectID()
        {
            // Arrange
            const int projectID = 19; // Unused project ID
            const int expectedCount = 0;
            int actualCount = 1; // Setting to 1 so it has to change to 0
            List<AvailabilityVM> availabilityVMs = new List<AvailabilityVM>();
            // Act
            availabilityVMs = _availabilityManager.SelectAvailabilityVMByProjectID(projectID);
            actualCount = availabilityVMs.Count;
            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestUpdateAvailabilityByIDValidRecordReturnsTrue()
        {
            int availabilityID = 1;
            int userID = 1;
            bool isAvailable = false;
            bool repeatWeekly = true;
            DateTime startDate = DateTime.Now.AddDays(1);
            DateTime endDate = DateTime.Now.AddDays(2);

            bool result = _availabilityManager.UpdateAvailabilityByID(
                availabilityID,
                userID,
                isAvailable,
                repeatWeekly,
                startDate,
                endDate);

            Assert.IsTrue(result, "Expected UpdateAvailabilityByID to return true for a successful update");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateAvailabilityByIDInvalidIDThrowsException()
        {
            int invalidAvailabilityID = 0;
            int userID = 1;
            bool isAvailable = true;
            bool repeatWeekly = false;
            DateTime startDate = DateTime.Now.AddDays(1);
            DateTime endDate = DateTime.Now.AddDays(2);

            _availabilityManager.UpdateAvailabilityByID(
                invalidAvailabilityID,
                userID,
                isAvailable,
                repeatWeekly,
                startDate,
                endDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateAvailabilityByIDInvalidDateRangeThrowsException()
        {
            int availabilityID = 1;
            int userID = 1;
            bool isAvailable = true;
            bool repeatWeekly = false;
            DateTime startDate = DateTime.Now.AddDays(2);
            DateTime endDate = DateTime.Now.AddDays(1);

            _availabilityManager.UpdateAvailabilityByID(
                availabilityID,
                userID,
                isAvailable,
                repeatWeekly,
                startDate,
                endDate);
        }

        [TestMethod]
        public void TestDeleteAvailabilityByAvailabilityIDValidIDReturnsTrue()
        {
            int availabilityID = 2;
            bool result = _availabilityManager.DeleteAvailabilityByAvailabilityID(availabilityID);
            Assert.IsTrue(result, "Expected DeleteAvailabilityByAvailabilityID to return true for a successful deletion");

            //try
            //{
            //    var availabilities = _availabilityManager.SelectAvailabilityByUser(2);
            //    bool recordStillExists = availabilities.Any(a => a.AvailabilityID == availabilityID);
            //    Assert.IsFalse(recordStillExists, "The availability record should have been deleted");
            //}
            //catch (ApplicationException)
            //{
            //}
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeleteAvailabilityByAvailabilityIDInvalidIDThrowsException()
        {
            int invalidAvailabilityID = 0;
            _availabilityManager.DeleteAvailabilityByAvailabilityID(invalidAvailabilityID);
        }
    }
}

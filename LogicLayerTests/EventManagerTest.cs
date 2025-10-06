/// <summary>
/// Yousif Omer
/// Created: 2025/02/01
/// 
/// Actual summary of the event manager for logic test
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

using DataAccessFakes;
using DataAccessInterfaces;
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
    public class EventManagerTest
    {

        private IEventAccessor _fackEventAccessor;
        private EventManager _eventManager;

        [TestInitialize]
        public void TestSetUp()
        {
            _fackEventAccessor = new EventAccessorFakes();
            _eventManager = new EventManager(_fackEventAccessor);
        }

        [TestMethod]
        public void TestViewEventList()
        {
            // Arrange
            List<Event> ViewEventList;

            // Act
            ViewEventList = _eventManager.ViewEventList();

            // Assert
            Assert.AreEqual(2, ViewEventList.Count);

        }

        [TestMethod]
        public void TestEditEvent()
        {
            // Arrange
            bool expectedResults = false;
            Event oldEvent
             = new Event()
             {
                 EventID = 10,
                 EventTypeID = "AC01",
                 ProjectID = 1,
                 DateCreated = DateTime.Now,
                 StartDate = DateTime.Now,
                 EndDate = DateTime.Now,
                 Name = "New Name",
                 LocationID = 12,
                 VolunteersNeeded = 100,
                 UserID = 1000001,
                 Notes = "This is a note",
                 Description = "This is a Description"

             };
            Event newEvent
               = new Event()
               {
                   EventID = 10,
                   EventTypeID = "AC01",
                   ProjectID = 1,
                   DateCreated = DateTime.Now,
                   StartDate = DateTime.Now,
                   EndDate = DateTime.Now,
                   Name = "New Name",
                   LocationID = 12,
                   VolunteersNeeded = 100,
                   UserID = 1000001,
                   Notes = "This is a note",
                   Description = "This is a Description"

               };
            IEventManager eventManager = new EventManager(_fackEventAccessor);
            //Act
            bool actualResult = eventManager.EditEvent(oldEvent, newEvent);

            //Assert
            Assert.AreEqual(actualResult, expectedResults);
        }


        [TestMethod]
        public void TestDeactivateEventById()
        {
            // Arrange
            bool expectedResult = true;

            IEventManager eventrManager = new EventManager(_fackEventAccessor);
            // Act

            var actualResult = eventrManager.DeactivateEventById(10);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void TestActivateEventById()
        {
            // arrange
            Event selectedEvent = null;
            const int customerName = 10;
            int result = 0;

            // act
            selectedEvent = _eventManager.SelectEventByID(1);
            if (selectedEvent != null)
            {
                result = 1;
            }
            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestViewEventListByProjectID()
        {
            // Arrange
            List<Event> ViewEventList;

            // Act
            ViewEventList = _eventManager.ViewEventListByProjectID(1);

            // Assert
            Assert.AreEqual(1, ViewEventList.Count);

        }
        // Author: Brodie Pasker
        [TestMethod]
        public void ViewEventListByApprovedUserID()
        {
            int expectedResult = 2;
            int actualResult = 0;
            int UserID = 1;

            actualResult = _eventManager.ViewEventListByApprovedUserID(UserID).Count();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestInsertEventNullImageDescriptionNotes()
        {
            Event testEvent = new Event()
            {
                EventID = 10000000,
                EventTypeID = "AM3",
                ProjectID = 1,
                DateCreated = DateTime.Now,
                StartDate = DateTime.Parse("2025-05-01 12:34:56"),
                EndDate = DateTime.Parse("2025-05-04 12:34:56"),
                Name = "test",
                LocationID = 2,
                VolunteersNeeded = 0,
                UserID = 1,
                Active = true
            };

            bool actual = _eventManager.InsertEvent(testEvent);
            Assert.IsTrue(actual);

        }

        [TestMethod]
        public void TestInsertEventWithDescriptionNotes()
        {
            Event testEvent = new Event()
            {
                EventID = 10000000,
                EventTypeID = "AM3",
                ProjectID = 1,
                DateCreated = DateTime.Now,
                StartDate = DateTime.Parse("2025-05-01 12:34:56"),
                EndDate = DateTime.Parse("2025-05-04 12:34:56"),
                Name = "test",
                LocationID = 2,
                VolunteersNeeded = 0,
                UserID = 1,
                Notes = "aksdhalsgdasdg",
                Description = "lsakdfjdaksljf",
                Active = true
            };

            bool actual = _eventManager.InsertEvent(testEvent);
            Assert.IsTrue(actual);
        }
    }
}

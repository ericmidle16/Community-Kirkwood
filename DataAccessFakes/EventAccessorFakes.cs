/// <summary>
/// Yousif Omer
/// Created: 2025/02/14
/// 
/// This class is created to get un apdate Event List in the AccessorFakes
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessFakes
{
    public class EventAccessorFakes : IEventAccessor
    {
        private List<Event> events = null;
        private List<VolunteerStatus> volunteerStatuses = null;

        public EventAccessorFakes()
        {
            events = new List<Event>()
            {
                new Event()
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
                    Description = "This is a Description",
                    Active = true,

                },
                 new Event()
                 {
                    EventID = 12,
                    EventTypeID = "KI02",
                    ProjectID = 2,
                    DateCreated = DateTime.Now,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Name = "No Name",
                    LocationID = 10,
                    VolunteersNeeded = 24,
                    UserID = 1000004,
                    Notes = "This is a note",
                    Description = "This is a Description",
                    Active = true,

                 },

            };
            volunteerStatuses = new List<VolunteerStatus>()
            {
                new VolunteerStatus()
                {
                    UserID = 1,
                    ProjectID = 1,
                    Approved = true
                },
                new VolunteerStatus()
                {
                    UserID = 1,
                    ProjectID = 2,
                    Approved = true
                }
            };
        }

  

        public int DeactivateEventById(int eventId)
        {
            int rows = 0;
            foreach (var a in events)
            {
                if (a.EventID == Convert.ToInt32(eventId))
                {

                    rows = 1;
                }
            }
            return rows;
        }

        public List<Event> GetEvents(bool active = true)
        {
            List<Event> _events;
            _events = (from Event in events
                         
                          select Event).ToList();
            return _events;
        }

        public Event SelectEventByID(int eventId)
        {
              Event _event = new Event();
            foreach (var e in events)
            {
                if (e.EventID == 10)
                {
            _event = e;
                    break;
                }
            }
            return _event;
        }


        public int UpdateEvent(Event oldEvent, Event newEvent)
        {
            int result = 0;


            if (events.Contains(oldEvent))
            {
                events.Remove(oldEvent);
                events.Add(newEvent);

            }
            if (events.Contains(newEvent) && !events.Contains(oldEvent))
            {
                result = 1;
            }

            return result;
        }

        public List<Event> GetEventsByProjectID(int projectID)
        {
            List<Event> projectEvents = events.FindAll(p => p.ProjectID == projectID);
            if (projectEvents != null && projectEvents.Count > 0)
            {
                return projectEvents;
            }

            throw new Exception("Test Failed");
        }

        public List<Event> GetEventsByApprovedUserID(int UserID)
        {
            IEnumerable<Event> ev = from e in events
                                    join vs in volunteerStatuses on e.ProjectID equals vs.ProjectID
                                    where vs.UserID == UserID && vs.Approved == true
                                    select e;
            return ev.ToList();
        }

        public int InsertEvent(Event newEvent)
        {
            if (newEvent == null)
            {
                throw new ApplicationException("Event cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(newEvent.Name))
            {
                throw new ApplicationException("Event name cannot be empty or whitespace.");
            }
            if (newEvent.StartDate >= newEvent.EndDate)
            {
                throw new ApplicationException("Start date must be before end date.");
            }
            if (newEvent.ProjectID <= 0)
            {
                throw new ApplicationException("Invalid project ID.");
            }
            if (newEvent.UserID <= 0)
            {
                throw new ApplicationException("Invalid user ID.");
            }
            if (string.IsNullOrWhiteSpace(newEvent.EventTypeID))
            {
                throw new ApplicationException("Event type cannot be empty or whitespace.");
            }
            if (newEvent.VolunteersNeeded < 0)
            {
                throw new ApplicationException("Volunteers needed cannot be negative.");
            }

            events.Add(newEvent);
            return 1;
        }

        public List<EventType> GetEventTypes()
        {
            return new List<EventType>()
            {
                new EventType()
                {
                    EventTypeID = "AC01", Description = "Activity"
                },
                new EventType()
                {
                    EventTypeID = "KI02", Description = "Kickoff"
                }
            };
        }
    }
}

/// <summary>
/// Yousif Omer
/// Created: 2025/02/01
/// 
/// Actual summary of the creating event manager class for logic Manager layer
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

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
    public class EventManager : IEventManager
    {
        IEventAccessor _eventAccessor;

        public EventManager()
        {
            _eventAccessor = new EventAccessor();
        }
        public EventManager(IEventAccessor eventAccessor)
        {
            _eventAccessor = eventAccessor;

        }

        public bool DeactivateEventById(int eventId)
        {
            try
            {
                return (1 == _eventAccessor.DeactivateEventById(eventId));

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EditEvent(Event oldevent, Event newEvent)
        {
            bool result = false;
            try
            {
                result = (1 == _eventAccessor.UpdateEvent(oldevent, newEvent));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not update ");
            }
            return result;
        }

        public Event SelectEventByID(int eventId)
        {
            try
            {
                return _eventAccessor.SelectEventByID(eventId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Event not found!", ex);
            }
        }

        public List<Event> ViewEventList(bool active = true)
        {
            List<Event> result = new List<Event>();
            try
            {
                result =  _eventAccessor.GetEvents(active);
            }
            catch (Exception ex)
            {

                new ApplicationException("List is not found!");

            }
            return result;
        }

        public List<Event> ViewEventListByProjectID(int projectID)
        {
            List<Event> result = new List<Event>();
            try
            {
                result = _eventAccessor.GetEventsByProjectID(projectID);
            }
            catch (Exception ex)
            {

                new ApplicationException("List is not found!");

            }
            return result;
        }

        public List<Event> ViewEventListByApprovedUserID(int UserID)
        {
            List<Event> result = new List<Event>();
            try
            {
                result = _eventAccessor.GetEventsByApprovedUserID(UserID);
            }
            catch (Exception ex)
            {
                new ApplicationException("List is not found!");
            }
            return result;
        }

        public bool InsertEvent(Event newEvent)
        {
            bool result = false;
            try
            {
                int rowsAffected = _eventAccessor.InsertEvent(newEvent);
                if (rowsAffected.Equals(1))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data failed to insert", ex);
            }

            return result;
        }

        public List<EventType> SelectEventTypes()
        {
            List<EventType> eventTypes = null;

            try
            {
                eventTypes = _eventAccessor.GetEventTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Event types not found.", ex);
            }
            return eventTypes;

        }
    }
}
/// <summary>
/// Yousif Omer
/// Created: 2025/02/14
/// 
/// This class is created to make an Interface for the data access
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IEventAccessor
    {
     
        List<Event> GetEvents(bool active = true);
        int UpdateEvent(Event oldEvent, Event newEvent);
        int DeactivateEventById(int eventId);
        Event SelectEventByID(int eventId);
        List<Event> GetEventsByProjectID(int projectID);
        /// <summary>
        /// Creator:  Brodie Pasker
        /// Created:  2025/04/13
        /// Summary:  Interface method for getting a list of events for projects that the user has been approved to
        ///  Last Updated By:
        /// Last Updated:
        /// What was Changed:
        List<Event> GetEventsByApprovedUserID(int UserID);
        int InsertEvent(Event newEvent);
        List<EventType> GetEventTypes();
    }
}

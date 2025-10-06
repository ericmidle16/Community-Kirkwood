/// <summary>
/// Yousif Omer
/// Created: 2025/02/01
/// 
/// Actual summary of the Ieventmanager class for logic layer
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace LogicLayer
{
    public interface IEventManager
    {
     
        List<Event> ViewEventList(bool active = true);
        bool EditEvent(Event oldevent, Event newEvent);
        bool DeactivateEventById(int ID);
        Event SelectEventByID(int eventId);
        List<Event> ViewEventListByProjectID(int projectID);
        List<Event> ViewEventListByApprovedUserID(int UserID);
        public bool InsertEvent(Event newEvent);
        public List<EventType> SelectEventTypes();
    }
}
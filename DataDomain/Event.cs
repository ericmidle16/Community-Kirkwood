/// <summary>
/// Yousif Omer
/// Created: 2025/02/01
/// 
/// Actual summary of event class for VM in DataDomain
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DataDomain
{
    public class Event
    {

        public int EventID { get; set; }
        public string  EventTypeID { get; set; }
        public int ProjectID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public int LocationID { get; set; }
        public int VolunteersNeeded { get; set; }
        public int UserID { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
    public class EventVM : Event { }

}
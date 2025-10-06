/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/10
/// Summary: Represents a user's availability with start and end times,  
/// optional weekly repetition, and availability status.
/// 
/// Last Updated By: Chase Hannen
///	Last Updated Date:  2025-04-04
///	What Was Changed: Added Availability View Model
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Availability
    {
        public int AvailabilityID { get; set; }  
        public int UserID { get; set; } 
        public bool IsAvailable { get; set; }  
        public bool RepeatWeekly { get; set; } 
        public DateTime StartDate { get; set; }  
        public DateTime EndDate { get; set; }  
    }

    public class AvailabilityVM : Availability
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}

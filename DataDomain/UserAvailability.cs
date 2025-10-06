///<summary>
/// Creator: Dat Tran
/// Created: 02/11/2025
/// Summary: This class contains the attributes that are used in a large amount of project layers. 
/// Last updated by:
/// Last updated: 
/// Changes:
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
        
    public class UserAvailability
    {
        /*
         [AvailabilityID]	
         [UserID]			
         [IsAvailable]		
         [RepeatWeekly]		
         [StartDate]			
         [EndDate]			
         */

        public int AvailabilityID { get; set; }
        public int UserID { get; set; }
        public string? GivenName { get; set; }
        public string? FamilyName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public bool IsAvailable { get; set; }
        
    }
}

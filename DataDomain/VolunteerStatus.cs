/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/13
/// Summary:  Class for the creation of VolunteerStatus Objects
///             used to keep track of volunteer applications to projects
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/13
/// What was Changed: Initial creation	
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class VolunteerStatus
    {
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public bool? Approved { get; set; }
    }

    public class VMVolunteerStatus : VolunteerStatus
    {
        public string Name { get; set; }
    }
}

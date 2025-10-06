/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/17
/// Summary:  The class for tasks that are assigned.
///  Last Updated By: Stan Anderson
/// Last Updated: 2025/04/16
/// What was Changed: Added display name values
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class TaskAssigned
    {
        public int TaskID { get; set; }
        public int UserID { get; set; }
    }

    public class TaskAssignedViewModel : TaskAssigned
    {
        [DisplayName("Given Name")]
        public string GivenName { get; set; }

        [DisplayName("Family Name")]
        public string FamilyName { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        [DisplayName("Task")]
        public string TaskName { get; set; }

        [DisplayName("Task Description")]
        public string TaskDescription { get; set; }
        public int EventID { get; set; }
    }
}

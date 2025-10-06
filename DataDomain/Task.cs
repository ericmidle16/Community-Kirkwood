/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/10
/// Summary:  This is the class for tasks.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/16
/// What was Changed: Added Display Names
/// </summary>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataDomain
{
    public class Task
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Task Date")]
        public DateTime TaskDate { get; set; }

        public int ProjectID { get; set; }

        [DisplayName("Task Type")]
        public string TaskType { get; set; }
        public int EventID { get; set; }
        public bool Active { get; set; }
    }
}

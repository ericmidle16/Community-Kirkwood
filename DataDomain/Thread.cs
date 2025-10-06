/// <summary>
/// Creator: 
/// Created:
/// Summary:
/// 
/// Last Updated By: Nik Bell & Kate Rich
/// Last Updated: 2025-04-18
/// What Was Changed: Updated the ThreadVM class to include UserName & FormattedDatePosted.
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-04-18
/// What Was Changed: Added Content in order for web to work
/// </summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Thread
    {
        public int ThreadID { get; set; }
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public string ThreadName { get; set; }
        public DateTime DatePosted { get; set; }
        public string Content { get; set; } = string.Empty;
    }
    public class ThreadVM : Thread
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string UserName
        {
            get
            {
                return GivenName + " " + FamilyName;
            }
        }
        public string FormattedDatePosted
        {
            get
            {
                return DatePosted.ToShortDateString();
            }
        }
    }
}

/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/04/18
/// Summary: Represents the user's forum permissions
/// Last Updated By: Syler Bushlack
///	Last Updated Date:  2025-04-24
///	What Was Changed: added ForumPermissionVM
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class ForumPermission
    {
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public bool WriteAccess { get; set; }
    }

    public class ForumPermissionVM : ForumPermission
    {
        public string GivenName { get; set; }
    }
}

/// <summary>
/// Ellie Wacker
/// Created: 2025-02-09
/// 
/// ProjectRole class
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class ProjectRole
    {
        public string ProjectRoleID { get; set; }
        public string Description { get; set; }
    }

    public class ProjectRoleVM
    {
        public string ProjectRoleID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}

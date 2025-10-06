/// <summary>
/// Creator: Eric Idle
/// Created: 2025/04/11
/// 
/// Accessor for project roles
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class ProjectRoleAccessor : IProjectRoleAccessor
    {
        /// <summary>
        /// Eric Idle
        /// Created: 2025/02/06
        /// 
        /// This is to select all Project Roles from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: 
        /// </remarks>
        public List<ProjectRole> SelectAllProjectRoles()
        {
            List<ProjectRole> projectRoles = new List<ProjectRole>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_projectroles", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ProjectRole pr = new ProjectRole();
                    pr.ProjectRoleID = r.GetString(0);
                    pr.Description = r.GetString(1);

                    projectRoles.Add(pr);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return projectRoles;
        }
    }
}

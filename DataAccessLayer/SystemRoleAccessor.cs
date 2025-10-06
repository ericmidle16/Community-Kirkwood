/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/06
/// 
/// The accessor to System role that uses methods
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
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SystemRoleAccessor : ISystemRoleAccessor
    {
        /// <summary>
        /// Eric Idle
        /// Created: 2025/02/06
        /// 
        /// This is to select all System Roles from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: 
        /// </remarks>
        public List<SystemRole> SelectAllSystemRoles()
        {
            List<SystemRole> systemRoles = new List<SystemRole>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_systemroles", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    SystemRole sr = new SystemRole();
                    sr.SystemRoleID = r.GetString(0);
                    sr.Description = r.GetString(1);

                    systemRoles.Add(sr);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return systemRoles;
        }
    }
}

/// <summary>
/// Ellie Wacker
/// Created: 2025-02-08
/// 
/// Interface that holds method declarations for managing VolunteerStatusProjectRole data.
/// </summary>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IVolunteerStatusProjectRoleManager
    {
        int DeleteUserRoles(int userID, int projectID);
        List<VolunteerStatusProjectRole> GetUserProjectRolesByUserIDProjectID(int userID, int projectID);
        int InsertUserProjectRole(int userID, int projectID, string projectRoleID);
        int RemoveProjectRoleByUserIDProjectID(int userID, int projectID,  string projectRoleID);
    }
}

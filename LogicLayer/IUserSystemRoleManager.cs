/// <summary>
/// Ellie Wacker
/// Created: 2025-03-05
/// 
/// Interface that holds method declarations for managing UserSystemRole data.
/// </summary>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IUserSystemRoleManager
    {
        int InsertUserSystemRole(int userID, string systemRoleID);
        List<UserSystemRole> GetUserSystemRolesByUserID(int userID);
        bool RemoveUserSystemRole(int userID, string systemRoleID);

    }
}

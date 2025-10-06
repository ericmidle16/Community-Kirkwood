/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/04/18
/// Summary: Defines the IForumPermissionManager interface, 
/// which includes method for selecting a user's write access.
/// 
/// Updated By: Syler Bushlack
/// Updated: 2025-04-24
/// What Was Changed: Added EditForumPermissionWriteAccessValue and GettForumPermissionsByProjectID method
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed: Added the AddForumPermission method.
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IForumPermissionManager
    {
        bool SelectUserWriteAccess(int userID, int projectID);
        bool EditForumPermissionWriteAccessValue(ForumPermission forumPermission);
        List<ForumPermissionVM> GettForumPermissionsByProjectID(int projectID);
        bool AddForumPermission(ForumPermission forumPermission);
    }
}
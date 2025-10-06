/// <summary>
/// Creator: Skyann Heintz
/// Created: 2025/02/18
/// Summary: Implements the IForumPermissionAccessor interface with 
/// methods for selecting user write access based on userID and projectID
/// 
/// Updated By: Syler Bushlack
/// Updated: 2025-04-24
/// What Was Changed: added UpdateForumPermissionWriteAccessValue and SelectForumPermissionByProjectID methods
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed: Added implementation for the InsertForumPermission method.
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class ForumPermissionAccessorFake : IForumPermissionAccessor
    {
        private List<ForumPermission> _userWriteAccessRecords;

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/18
        /// Summary: Initializes a list of fake forum permission records to 
        /// simulate the behavior of the forum permission data access 
        /// layer.
        /// Last Updated By: 
        /// Last Updated: 
        /// What Was Changed: 
        /// </summary>
        public ForumPermissionAccessorFake()
        {
            _userWriteAccessRecords = new List<ForumPermission>()
            {
                new ForumPermission 
                { 
                    UserID = 1, 
                    ProjectID = 101, 
                    WriteAccess = true
                },
                new ForumPermission
                { 
                    UserID = 2, 
                    ProjectID = 101, 
                    WriteAccess = false
                },
                new ForumPermission 
                { 
                    UserID = 3, 
                    ProjectID = 102, 
                    WriteAccess = true 
                },
                new ForumPermission 
                { 
                    UserID = 4, 
                    ProjectID = 102,
                    WriteAccess = false 
                },
            };
        }

        // Author: Kate Rich
        public int InsertForumPermission(ForumPermission forumPermission)
        {
            int rowsAffected = 0;
            int count = _userWriteAccessRecords.Count;

            _userWriteAccessRecords.Add(new ForumPermission()
            {
                UserID = forumPermission.UserID,
                ProjectID = forumPermission.ProjectID,
                WriteAccess = forumPermission.WriteAccess
            });

            if(_userWriteAccessRecords.Count - count == 1)
            {
                rowsAffected++;
            }

            return rowsAffected;
        }

        // Author: Syler Bushlack
        public List<ForumPermissionVM> SelectForumPermissionByProjectID(int projectID)
        {
            List<ForumPermissionVM> forumPermissions = new List<ForumPermissionVM>();
            foreach (ForumPermission permission in _userWriteAccessRecords)
            {
                if (permission.ProjectID == projectID)
                {
                    ForumPermissionVM forumPermissionVM = new ForumPermissionVM();
                    forumPermissionVM.ProjectID = permission.ProjectID;
                    forumPermissionVM.UserID = permission.UserID;
                    forumPermissionVM.WriteAccess = permission.WriteAccess;
                    forumPermissions.Add(forumPermissionVM);
                }
            }
            return forumPermissions;
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/18
        /// Summary: Selects the user's write access by userID and projectID
        /// Last Updated By: 
        /// Last Updated: 
        /// What Was Changed: 
        /// </summary>
        public bool SelectUserWriteAccess(int userID, int projectID)
        {
            try
            {
                var userAccess = _userWriteAccessRecords
                    .FirstOrDefault(ua => ua.UserID == userID && ua.ProjectID == projectID);

                if (userAccess == null)
                {
                    throw new ApplicationException("User permission not found for this project.");
                }

                return userAccess.WriteAccess;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error checking user write access.", ex);
            }
        }

        // Author: Syler Bushlack
        public int UpdateForumPermissionWriteAccessValue(ForumPermission forumPermission)
        {
            int permissionsAffected = 0;
            for(int i = 0;  i < _userWriteAccessRecords.Count; i++)
            {
                if (_userWriteAccessRecords[i].ProjectID == forumPermission.ProjectID && _userWriteAccessRecords[i].UserID == forumPermission.UserID)
                {
                    _userWriteAccessRecords[i] = forumPermission;
                    permissionsAffected++;
                }
            }
            return permissionsAffected;
        }
    }
}
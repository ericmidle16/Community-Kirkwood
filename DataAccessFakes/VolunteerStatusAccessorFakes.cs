/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/14
/// Summary:  Class for the accessing fake data to test VolunteerStatusAccessor
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/20
/// What was Changed: Added SelectRejectedVolunteerStatusByUserID method
/// 
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025/03/31
/// What was Changed: Added all my methods
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025-04-01
/// What was Changed: Added the AddVolunteerStatus & SelectVolunteerStatusByProjectID methods.
/// </remarks>
using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class VolunteerStatusAccessorFakes : IVolunteerStatusAccessor
    {
        private List<User> _users;
        private List<VMVolunteerStatus> _volunteerStatus;
        private List<VolunteerStatus> _volunteerstatus;
        private List<ProjectVM> _projects;
        public VolunteerStatusAccessorFakes()
        {
            _projects = new List<ProjectVM>();
            _users = new List<User>();

            _projects.Add(new ProjectVM()
            {
                ProjectID = 1,
                Name = "School Donations",
                ProjectTypeID = "Donations"
            });
            _users.Add(new User()
            {
                UserID = 1,
                GivenName = "Test1",
                FamilyName = "Test1"
            });
            _volunteerStatus = new List<VMVolunteerStatus>();
            _volunteerStatus.Add(new VMVolunteerStatus()
            {
                UserID = 1,
                ProjectID = 1
            });
            _volunteerStatus.Add(new VMVolunteerStatus()
            {
                UserID = 2,
                ProjectID = 1,
                Approved = true
            });
            _volunteerStatus.Add(new VMVolunteerStatus()
            {
                UserID = 3,
                ProjectID = 1
            });
            _volunteerStatus.Add(new VMVolunteerStatus()
            {
                UserID = 4,
                ProjectID = 1,
                Approved = false
            });
            _volunteerStatus.Add(new VMVolunteerStatus()
            {
                UserID = 5,
                ProjectID = 1,
                Approved = false
            });

            _volunteerstatus = new List<VolunteerStatus>();
            _volunteerstatus.Add(new VolunteerStatus()
            {
                UserID = 10,
                ProjectID = 1,
                Approved = true,
            });
            _volunteerstatus.Add(new VolunteerStatus()
            {
                UserID = 100,
                ProjectID = 10,
                Approved = true,

            });

            _volunteerstatus.Add(new VolunteerStatus()
            {
                UserID = 100,
                ProjectID = 100,
                Approved = true,

            });
        }

        public List<VMVolunteerStatus> SelectPendingVolunteerStatusByProjectID(int projectID)
        {
            List<VMVolunteerStatus> volunteerStatuses = new List<VMVolunteerStatus>();
            foreach (VMVolunteerStatus volunteerStatus in _volunteerStatus)
            {
                if(volunteerStatus.Approved.HasValue)
                {
                    if (volunteerStatus.ProjectID == projectID && volunteerStatus.Approved.Value == false)
                    {
                        volunteerStatuses.Add(volunteerStatus);
                    }
                }
            }
            return volunteerStatuses;
        }

        public List<VMVolunteerStatus> SelectRejectedVolunteerStatusByProjectID(int projectID)
        {
            List<VMVolunteerStatus> volunteerStatuses = new List<VMVolunteerStatus>();
            foreach (VMVolunteerStatus volunteerStatus in _volunteerStatus) {
                if (volunteerStatus.ProjectID == projectID && !volunteerStatus.Approved.HasValue)
                {
                    volunteerStatuses.Add(volunteerStatus);
                }
            }
            return volunteerStatuses;
        }

        public int UpdateVolunteerStatusByUserIDAndProjectID(VMVolunteerStatus volunteerStatus)
        {
            foreach (VMVolunteerStatus volunteerStatuses in _volunteerStatus)
            {
                if (volunteerStatus.UserID == volunteerStatuses.UserID && volunteerStatus.ProjectID == volunteerStatuses.ProjectID)
                {
                    volunteerStatus.Approved = true;
                    return 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/08
        /// 
        /// The fake method for deactivating volunteers
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int DeactivateVolunteerByUserIDAndProjectID(int userID, int projectID)
        {
            int count = 0;
            foreach (var status in _volunteerStatus)
            {
                if (status.UserID == userID && status.ProjectID == projectID)
                {
                    status.Approved = false;
                    count++;
                }
            }
            if (count == 0)
            {
                throw new ArgumentException("VolunteerStatus record not found");
            }
            return count;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/12
        /// 
        /// The fake method for selecting a volunteer status
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public VolunteerStatus SelectVolunteerStatusByUserIDAndProjectID(int userID, int projectID)
        {
            foreach (VolunteerStatus volunteerStatus in _volunteerStatus)
            {
                if (volunteerStatus.UserID == userID && volunteerStatus.ProjectID == projectID)
                {
                    return volunteerStatus;
                }
            }
            throw new ArgumentException("Volunteer Status Record Not Found.");
        }

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: Check if user ID and project ID exist and add volunteerStatus
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int AddVolunteerStatus(int userid, int projectid)
        {
            VolunteerStatus volunter = new VolunteerStatus();
            volunter.UserID = userid;
            volunter.ProjectID = projectid;
            volunter.Approved = false;
            _volunteerstatus.Add(volunter);
            int count = 0;
            for (int i = 0; i < _volunteerstatus.Count; i++)
            {
                if (_volunteerstatus[i].UserID == userid && _volunteerstatus[i].ProjectID == projectid)
                {
                    count++;
                }
            }
            return count;
        }
        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: Check if project ID exist and add volunteerStatuses to the volunteerstatus
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public List<VolunteerStatus> SelectVolunteerStatusByProjectID(int projectid)
        {
            List<VolunteerStatus> volunteerStatuses = new List<VolunteerStatus>();
            foreach (VolunteerStatus volunteerStatus in _volunteerstatus)
            {
                if (volunteerStatus.ProjectID == projectid)
                {
                    volunteerStatuses.Add(volunteerStatus);
                }
            }
            return volunteerStatuses;
        }
    }
}
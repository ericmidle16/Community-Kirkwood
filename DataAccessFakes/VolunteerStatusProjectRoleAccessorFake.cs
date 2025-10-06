/// <summary>
/// Ellie Wacker
/// Created: 2025-02-08
/// 
/// Class for fake VolunteerStatusProjectRole objects that are used in testing.
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
    public class VolunteerStatusProjectRoleAccessorFake : IVolunteerStatusProjectRoleAccessor
    {
        private List<User> _users;
        private List<ProjectVM> _projects;
        private List<ProjectRole> _projectRoles;
        private List<VolunteerStatusProjectRole> _volunteerStatusProjectRoles;

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/08
        /// 
        /// The fake data for project, user, projectRole and volunteerStatusProjectRole
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public VolunteerStatusProjectRoleAccessorFake()
        {
            _projects = new List<ProjectVM>();
            _users = new List<User>();
            _projectRoles = new List<ProjectRole>();
            _volunteerStatusProjectRoles = new List<VolunteerStatusProjectRole>();

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
            _projectRoles.Add(new ProjectRole()
            {
                ProjectRoleID = "Manager",
                Description = "Helps set up"
            });
            _volunteerStatusProjectRoles.Add(new VolunteerStatusProjectRole()
            {
                UserID = 1,
                ProjectID = 1,
                ProjectRoleID = "Manager"
            });
        }

        public int AddUserRoles(int userID, int projectID, string projectRoleID)
        {
            throw new NotImplementedException();
        }

        public int DeleteUserProjectRoleByUserIDProjectID(int userID, int projectID, string projectRoleID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/08
        /// 
        /// The fake method for deleting user roles
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int DeleteUserRoles(int userID, int projectID)
        {
            int count = 0;
            foreach(var role in _volunteerStatusProjectRoles)
            {
                if (role.UserID == userID && role.ProjectID == projectID)
                {
                    role.UserID = 0;
                    count++;
                }
            }
            if(count == 0)
            {
                throw new ArgumentException("Delete Failed");
            }
            return count;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/04/22
        /// 
        /// The fake method for getting volunteer status project roles by user and project id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public List<VolunteerStatusProjectRole> SelectUserProjectRolesByUserIDProjectID(int userID, int projectID)
        {
            List<VolunteerStatusProjectRole> volunteerStatusProjectRoles = new List<VolunteerStatusProjectRole>();

            foreach (var role in _volunteerStatusProjectRoles)
            {
                if (role.UserID == userID && role.ProjectID == projectID)
                {
                    volunteerStatusProjectRoles.Add(role);
                }
            }

            if (volunteerStatusProjectRoles.Count == 0)
            {
                throw new ArgumentException("Volunteer status roles list not found");
            }

            return volunteerStatusProjectRoles;
        }
    }
}
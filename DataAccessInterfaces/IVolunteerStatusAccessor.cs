/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/13
/// Summary:  Interface for VolunteerStatusAccessor class
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/13
/// What was Changed: Initial creation	
/// 
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025/03/31
/// What was Changed: Added my methods
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025/04/01
/// What was Changed: Added the AddVolunteerStatus & SelectVolunteerStatusByProjectID methods.
/// 
/// </remarks>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IVolunteerStatusAccessor
    {
        List<VMVolunteerStatus> SelectPendingVolunteerStatusByProjectID(int projectID);

        List<VMVolunteerStatus> SelectRejectedVolunteerStatusByProjectID(int projectID);

        int UpdateVolunteerStatusByUserIDAndProjectID(VMVolunteerStatus volunteerStatus);

        int DeactivateVolunteerByUserIDAndProjectID(int userID, int projectID);
        VolunteerStatus SelectVolunteerStatusByUserIDAndProjectID(int userID, int projectID);
        public int AddVolunteerStatus(int userid, int projectid);
        public List<VolunteerStatus> SelectVolunteerStatusByProjectID(int projectid);
    }
}

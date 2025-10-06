/// <summary>
/// Creator:  Syler Bushlack
/// Created:  2025/02/13
/// Summary:  Interface for VolunteerStatusManager class	
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/13
/// What was Changed: Initial creation	
/// 
/// Last Updated By: Ellie Wacker
/// Last Updated: 2025/03/31
/// What was Changed: I added my methods
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025/04/01
/// What was changed: Added the AddVolunteerStatus & SelectVolunteerStatusByProjectID methods.
/// 
/// </remarks>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IVolunteerStatusManager
    {
        List<VMVolunteerStatus> GetRejectedVolunteerStatusByProjectID(int projectID);

        public List<VMVolunteerStatus> GetPendingVolunteerStatusByProjectID(int projectID);

        bool UpdateVolunteerStatus(VMVolunteerStatus volunteerStatus);
        int DeactivateVolunteerByUserIDAndProjectID(int userID, int projectID);
        VolunteerStatus GetVolunteerStatusByUserIDAndProjectID(int userID, int projectID);
        public bool AddVolunteerStatus(int userid, int projectid);
        List<VolunteerStatus> SelectVolunteerStatusByProjectID(int projectid);
    }
}

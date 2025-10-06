/// <summary>
/// Creator:  Kate Rich
/// Created:  2025/02/02
/// Summary:  Interface that holds method declarations for managing Project data.
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/06
/// What was Changed: Added GetAllProjects method
/// 
/// Last Updated By: Christivie
/// Last Updated: 2025/03/28
/// What was Changed: Added GetProjectVMByID, EditProject  method
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025/04/04
/// What was Changed: Added GetAllProjectsByUserID
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/04/17
/// What was Changed: Added GetAllProjectVMsByUserID
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed: Updated the AddProject() to return an int.
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025/05/02
/// What was Changed: Added UpdateAvailableFunds
/// </remarks>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IProjectManager
    {
        public List<ProjectVM> GetAllProjects();
        public List<ProjectVM> GetAllProjectVMsByUserID(int userID);
        Project GetProjectByID(int projectID);
        ProjectVM GetProjectVMByID(int projectID);
        ProjectVM GetProjectInformationByProjectID(int projectID);
        bool EditProject(ProjectVM oldProject, ProjectVM newProject);
        public int AddProject(Project project);
        public List<ProjectTypeObject> GetAllProjectTypes();
        public List<Project> GetAllProjectsByUserID(int userID);
        public List<ProjectVM> GetAllApprovedProjectsByUserID(int UserID);
        public int UpdateAvailableFunds(int projectID, decimal amount, bool isDonation);
    }
}
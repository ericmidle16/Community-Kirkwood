/// <summary>
/// Creator:  Kate Rich
/// Created:  2025/02/02
/// Summary:  Interface that holds method declarations for accessing Project data.
/// </summary>
/// <remarks>
/// Last Updated By: Christivie Mauwa
/// Last Updated: 2025/02/06
/// What was Changed: Added SelectAllProjectsForVM,SelectProjectVMByID, UpdateProject  methods.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IProjectAccessor
    {
        List<ProjectVM> SelectAllProjects();
        List<ProjectVM> SelectAllProjectVMsByUserID(int userID);
        Project SelectProjectByID(int projectID);
        ProjectVM SelectProjectInformationByProjectID(int projectID);
        List<ProjectVM> SelectAllProjectsForVM();
        ProjectVM SelectProjectVMByID(int projectID);
        int UpdateProject(ProjectVM oldProject, ProjectVM newProject);
        int AddProject(Project project);
        List<ProjectTypeObject> GetAllProjectTypes();
        List<Project> GetAllProjectsByUserID(int userID);
        List<ProjectVM> GetAllProjectsByApprovedUserID(int UserID);
        int UpdateAvailableFunds(int projectID, decimal amount, bool isDonation);
    }
}
/// <summary>
/// Creator:  Kate Rich
/// Created:  2025/02/02
/// Summary:  Class that implements the IProjectManager Interface - used for
/// 			managing Project data from Project Data Fake objects &/or the DB.
/// </summary>
/// <remarks>
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/02/06
/// What was Changed: Initial creation
/// 
/// Last Updated By: Christivie Mauwa
/// Last Updated: 2025/03/28
/// What was Changed: Initial creation
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
/// What was Changed: Updated the AddProject() method to return an int.
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025/05/02
/// What was Changed: Added UpdateAvailableFunds
/// </remarks>

using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ProjectManager : IProjectManager
    {
        private IProjectAccessor _projectAccessor;

		// Constructor for DB
        public ProjectManager()
        {
            _projectAccessor = new ProjectAccessor();
        }

        // Constructor for Tests
        public ProjectManager(IProjectAccessor projectAccessor)
        {
            _projectAccessor = projectAccessor;
        }

        // Author: Christivie
        // Last Updated by: Stan Anderson 2025/04/10
        public bool EditProject(ProjectVM oldProject, ProjectVM newProject)
        {
            int rowsAffected = 0;
            try
            {
                rowsAffected = _projectAccessor.UpdateProject(oldProject, newProject);
                if (rowsAffected != 1)
                {
                    throw new ApplicationException("No changes detected or invalid project ID.");
                }
                return rowsAffected == 1 || rowsAffected == 2;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed", ex);
            }
        }

        // Author: Syler Bushlack
        public List<ProjectVM> GetAllProjects()
        {
            List<ProjectVM> projects = null;

            try
            {
                projects = _projectAccessor.SelectAllProjects();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return projects;
        }

        // Author: Syler Bushlack
        public List<ProjectVM> GetAllProjectVMsByUserID(int userID)
        {
            List<ProjectVM> projects = null;

            try
            {
                projects = _projectAccessor.SelectAllProjectVMsByUserID(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }

            return projects;
        }

        // Author: Kate Rich
        public Project GetProjectByID(int projectID)
        {
            Project project = null;

            try
            {
                project = _projectAccessor.SelectProjectByID(projectID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("No Project Found", ex);
            }

            return project;
        }

        // Author: Kate Rich
        public ProjectVM GetProjectInformationByProjectID(int projectID)
        {
            ProjectVM project = null;

            try
            {
                project = _projectAccessor.SelectProjectInformationByProjectID(projectID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("No Project Found", ex);
            }

            return project;
        }


        public ProjectVM GetProjectVMByID(int projectID)
        {
            try
            {
                return _projectAccessor.SelectProjectVMByID(projectID);
            }
            catch (Exception ex)
            {
			throw new ApplicationException("Data not found.", ex);
			}
		}

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/07
        /// Summary: This method gets adds a project to the database.
        /// 
        /// Last Updated By: Kate Rich
        /// Last Updated: 2025-05-02
        /// What Was Changed: Updated to return the newly created ProjectID.
        /// </summary>
        public int AddProject(Project project)
        {
            int newProjectID;

            try
            {
                newProjectID = _projectAccessor.AddProject(project);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Project was not added", ex);
            }

            return newProjectID;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/03/27
        /// Summary: This method gets all the projectTypes from the database.
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<ProjectTypeObject> GetAllProjectTypes()
        {
            try
            {
                return _projectAccessor.GetAllProjectTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available", ex);
            }
        }

        /// <summary>
        /// Creator: Chase Hannen
        /// Created: 2025-04-04
        /// Summary: Gets all projects by UserID
        /// Last Updated By: 
        /// Last Updated: 
        /// What was Changed: 
        /// </summary>
        public List<Project> GetAllProjectsByUserID(int userID)
        {
            List<Project> projects = null;

            try
            {
                projects = _projectAccessor.GetAllProjectsByUserID(userID);
            }
            catch (Exception ex)
            {
                throw new Exception("Project access failed", ex);
            }

            return projects;
        }

        /// <summary>
        /// Creator: Brodie Pasker
        /// Created: 2025-04-20
        /// Summary: Gets all approved projects by UserID
        /// Last Updated By: 
        /// Last Updated: 
        /// What was Changed: 
        /// </summary>
        public List<ProjectVM> GetAllApprovedProjectsByUserID(int UserID)
        {
            List<ProjectVM> projects = null;

            try
            {
                projects = _projectAccessor.GetAllProjectsByApprovedUserID(UserID);
            }
            catch (Exception ex)
            {
                throw new Exception("Project access failed", ex);
            }

            return projects;
        }

        // Author: Chase Hannen
        public int UpdateAvailableFunds(int projectID, decimal amount, bool isDonation)
        {
            int rowsAffected = 0;

            try
            {
                rowsAffected = _projectAccessor.UpdateAvailableFunds(projectID, amount, isDonation);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Project access failed", ex); ;
            }

            return rowsAffected;
        }
    }
}
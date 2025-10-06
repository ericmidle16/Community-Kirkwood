/// <summary>
/// Kate Rich
/// Created: 2025-02-02
/// 
/// Class for fake Project objects that are used in testing.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-03-07
/// What Was Changed:
///     I removed the EndDate from my ProjectVM data fakes.
///     
/// Last Updated By: christivie Mauwa
/// Last Updated: 2025-03-28
/// What Was Changed:
///     Added SelectAllProjectsForVM, SelectProjectVMByID, UpdateProject
///     
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
///     Added GetAllProjectsByUserID and _applications fakes
///     
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-17
/// What Was Changed:
///     Added SelectAllProjectVMsByUserID
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed:
///     Updated the AddProject() method to return an int.
///     
/// Last Updated By: Chase Hannen
/// Last Updated: 2025/05/02
/// What was Changed: Added UpdateAvailableFunds
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessFakes
{
    public class ProjectAccessorFake : IProjectAccessor
    {
        private List<User> _users;
        private List<Project> _projects;
        private List<ProjectVM> _projectVMs;
        List<ProjectTypeObject> _projecttypes;
        private List<VolunteerStatus> _applications;

        public ProjectAccessorFake()
        {
            _projectVMs = new List<ProjectVM>();
            _projects = new List<Project>();
            _projecttypes = new List<ProjectTypeObject>();

            _projectVMs.Add(new ProjectVM()
            {
                ProjectID = 1,
                Name = "Propane Delivery to TLMS",
                ProjectTypeID = "Propane Emergency",
                LocationID = 2,
                LocationName = "Tom Landry Middle School",
                City = "Arlen",
                State = "Texas",
                UserID = 17,
                StartDate = new DateTime(2025, 2, 2),
                Status = "Active",
                Description = "Emergency delivery of propane to Tom Landry Middle School.",
                AvailableFunds = 17m
            });
            _projectVMs.Add(new ProjectVM()
            {
                ProjectID = 2,
                Name = "Make Strickland More Accessible",
                ProjectTypeID = "Accessibility",
                LocationID = 1,
                LocationName = "Strickland Propane",
                City = "Arlen",
                State = "Texas",
                UserID = 12,
                StartDate = new DateTime(2025, 3, 15),
                Status = "Not Started",
                Description = "A project intended to make Strickland Propane more accessible to the handicapped population of Arlen.",
                AvailableFunds = 0m
            });

            _projectVMs.Add(new ProjectVM()
			{
				ProjectID = 3,
				Name = "Solar Panel Installation at RHS",
				ProjectTypeID = "Renewable Energy",
				LocationID = 5,
				UserID = 23,
				StartDate = new DateTime(2025, 3, 15),
				Status = "In Progress",
				Description = "Installation of solar panels to promote renewable energy at Riverdale High School.",
				AcceptsDonations = false,
				PayPalAccount = "",
				AvailableFunds = 50m,
				LocationName = "Riverdale High School",
				Address = "123 Main St",
				City = "Riverdale",
				State = "California",
				Zip = "90210",
				GivenName = "John",
				FamilyName = "Doe"
            });

            _projectVMs.Add(new ProjectVM()
            {
                ProjectID = 99999,
                Name = "Syler's test project",
                ProjectTypeID = "test",
                LocationID = 19,
                UserID = 100005,
                StartDate = new DateTime(2025, 4, 11),
                Status = "In Progress",
                Description = "A test project for test methods",
                AcceptsDonations = false,
                PayPalAccount = "",
                AvailableFunds = 50m,
                LocationName = "Test Land",
                Address = "777 Test St",
                City = "Test Town",
                State = "Iowa",
                Zip = "77777",
                GivenName = "Syler",
                FamilyName = "Bushlack"
            });


            _projects.Add(new Project()
            {
                Name = "Project Name 1",
                LocationID = 100000,
                UserID = 100002,
                StartDate = new DateTime(2025, 1, 1),
                Description = "Project Description 1"
            });
            _projects.Add(new Project()
            {
                Name = "Project Name 2",
                LocationID = 100001,
                UserID = 100003,
                StartDate = new DateTime(2025, 1, 1),
                Description = "Project Description 2"
            });
            _projects.Add(new Project()
            {
                Name = "Project Name 3",
                LocationID = 100002,
                UserID = 100004,
                StartDate = new DateTime(2025, 1, 1),
                Description = "Project Description 3"
            });
            _projects.Add(new Project()
            {
                ProjectID = 1,
                Name = "Project Name 4",
                LocationID = 100003,
                UserID = 100004,
                StartDate = new DateTime(2025, 1, 1),
                Description = "Project Description 4"
            });

            _projecttypes.Add(new ProjectTypeObject()
            {
                ProjectType = "Project Type",
                Description = "This is a project type"
            });
            _projecttypes.Add(new ProjectTypeObject()
            {
                ProjectType = "Project Type 2",
                Description = "This is a project type"
            });

            _applications = new List<VolunteerStatus>();
            _applications.Add(new VolunteerStatus()
            {
                UserID = 6,
                ProjectID = 1,
                Approved = true
            });
            _applications.Add(new VolunteerStatus()
            {
                UserID = 7,
                ProjectID = 2,
                Approved = true
            });
            _applications.Add(new VolunteerStatus()
            {
                UserID = 8,
                ProjectID = 2,
                Approved = true
            });
        }

        // Author: Kate Rich
        public Project SelectProjectByID(int projectID)
        {
            foreach(ProjectVM p in _projectVMs)
            {
                if(p.ProjectID == projectID)
                {
                    return p;
                }
            }
            throw new ArgumentException("Project Record Not Found.");
        }

        // Author: Kate Rich
        public ProjectVM SelectProjectInformationByProjectID(int projectID)
        {
            foreach(ProjectVM p in _projectVMs)
            {
                if(p.ProjectID == projectID)
                {
                    return p;
                }
            }
            throw new ArgumentException("Project Record Not Found.");
        }

        public List<ProjectVM> SelectAllProjects()
        {
            return _projectVMs;
        }

        // Author: Josh Nicholson
        public int AddProject(Project newProject)
        {
            int oldCount = _projects.Count;

            // lambda expression to check if a project with Name already exists
            Project project = _projects.Find(p => p.Name == newProject.Name);

            if(project == null)
            {
                _projects.Add(newProject);

                if(_projects.Count != oldCount)
                {
                    return newProject.ProjectID;
                }
            }

            throw new Exception("Test Failed");
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/03/27
        /// Summary: The test for GetAllProjectTypes()
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<ProjectTypeObject> GetAllProjectTypes()
        {
            return _projecttypes;
        }

        public List<ProjectVM> SelectAllProjectsForVM()
        {
            return _projectVMs;
        }

        public ProjectVM SelectProjectVMByID(int projectID)
        {
            var project = _projectVMs.Find(s => s.ProjectID == projectID);
            return project;
        }

        public int UpdateProject(ProjectVM oldProject, ProjectVM newProject)
        {
            var project = _projectVMs.Where(p => p.LocationName == oldProject.LocationName &&
                                p.Address == oldProject.Address &&
                                p.City == oldProject.City &&
                                p.State == oldProject.State &&
                                p.Zip == oldProject.Zip &&
                                p.Status == oldProject.Status &&
                                p.Description == oldProject.Description).First();
            if (project == null)
            {
                throw new ApplicationException("Update Failed.");
            }
            int num = _projectVMs.FindIndex(p => p.ProjectID == oldProject.ProjectID);

            if (num == -1)
            {
                throw new ApplicationException("Update Failed: Project index not found.");
            }
            _projectVMs[num] = newProject;
            return 1;
        }

        // Author: Chase Hannen
        public List<Project> GetAllProjectsByUserID(int userID)
        {
            List<Project> projects = new List<Project>();

            foreach (VolunteerStatus assignedVolunteer in _applications)
            {
                if (assignedVolunteer.UserID == userID)
                {
                    foreach (Project project in _projects)
                    {
                        if (project.ProjectID == assignedVolunteer.ProjectID)
                        {
                            projects.Add(project);
                        }
                    }
                }
            }

            return projects;
        }

        // Syler Bushlack
        public List<ProjectVM> SelectAllProjectVMsByUserID(int userID)
        {
            List<ProjectVM> projects = new List<ProjectVM>();
            try
            {
                foreach (ProjectVM project in _projectVMs)
                {
                    if (project.UserID == userID)
                    {
                        projects.Add(project);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return projects;
        }

        public List<ProjectVM> GetAllProjectsByApprovedUserID(int UserID)
        {
            IEnumerable<ProjectVM> projects = from p in _projectVMs
                                              join a in _applications on p.ProjectID equals a.ProjectID
                                              where a.UserID == UserID
                                              select p;

            return projects.ToList();
        }

        // Author: Chase Hannen
        public int UpdateAvailableFunds(int projectID, decimal amount, bool isDonation)
        {
            int total = 0;
            foreach(ProjectVM projectVM in _projectVMs)
            {
                if(projectID == projectVM.ProjectID)
                {
                    if(isDonation)
                    {
                        projectVM.AvailableFunds += amount;
                        total++;
                    }
                }
            }
            return total;
        }
    }
}
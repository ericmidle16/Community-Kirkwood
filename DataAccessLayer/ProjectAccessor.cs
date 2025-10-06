/// <summary>
/// Creator:  Kate Rich
/// Created:  2025/02/02
/// Summary:  Class that implements the IProjectAccessor Interface - used for
/// accessing Project data from the DB.
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-03-07
/// What Was Changed:
///     I removed pulling the project EndDate in from the SelectProjectByID &
///     SelectProjectInformationByProjectID methods.
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
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/04/09
/// What was Changed: Made view project return AcceptsDonations
/// 
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025/04/17
/// What was Changed: Added GetAllProjectVMsByUserID
/// 
/// Last Updated By: Kate Rich
/// Last Updated: 2025-05-02
/// What Was Changed: Updated the AddProject() method to return an int.
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025/05/02
/// What was Changed: Added UpdateAvailableFunds and updated SelectProjectInformationByProjectID
/// </remarks>

using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProjectAccessor : IProjectAccessor
    {
        // Author: Syler Bushlack
        public List<ProjectVM> SelectAllProjects()
        {
            List<ProjectVM> projects = new List<ProjectVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_projects", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ProjectVM p = new ProjectVM();
                    p.ProjectID = r.GetInt32(0);
                    p.Name = r.GetString(1);
                    p.ProjectTypeID = r.GetString(2);
                    p.LocationID = r.GetInt32(3);
                    p.UserID = r.GetInt32(4);
                    p.StartDate = r.GetDateTime(5);
                    p.Status = r.GetString(6);
                    p.Description = r.GetString(7);
                    p.AcceptsDonations = r.GetBoolean(8);
                    p.PayPalAccount = r.IsDBNull(9) ? null : r.GetString(9);
                    p.AvailableFunds = r.GetDecimal(10);
                    p.GivenName = r.GetString(11);
                    p.LocationName = r.GetString(12);
                    projects.Add(p);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return projects;
        }

        // Author: Syler Bushlack
        public List<ProjectVM> SelectAllProjectVMsByUserID(int userID)
        {
            List<ProjectVM> projects = new List<ProjectVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_created_projects_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ProjectVM p = new ProjectVM();
                    p.ProjectID = r.GetInt32(0);
                    p.Name = r.GetString(1);
                    p.ProjectTypeID = r.GetString(2);
                    p.LocationID = r.GetInt32(3);
                    p.UserID = r.GetInt32(4);
                    p.StartDate = r.GetDateTime(5);
                    p.Status = r.GetString(6);
                    p.Description = r.GetString(7);
                    p.AvailableFunds = r.GetDecimal(8);
                    p.GivenName = r.GetString(9);
                    p.LocationName = r.GetString(10);
                    projects.Add(p);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return projects;
        }

        public List<ProjectVM> SelectAllProjectsForVM()
        {
            List<ProjectVM> projects = new List<ProjectVM>();
            var conn = DBConnection.GetConnection();
            var cmdText = "SP_Select_All_Projects_For_VM";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        projects.Add(new ProjectVM()
                        {
                            ProjectID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            ProjectTypeID = reader.GetString(2),
                            LocationID = reader.GetInt32(3),
                            UserID = reader.GetInt32(4),
                            StartDate = reader.GetDateTime(5),
                            Status = reader.GetString(6),
                            Description = reader.GetString(7),
                            AcceptsDonations = reader.GetBoolean(8),
                            PayPalAccount = reader.GetString(9),
                            AvailableFunds = reader.GetDecimal(10),
                            LocationName = reader.GetString(11),
                            Address = reader.GetString(12),
                            City = reader.GetString(13),
                            State = reader.GetString(14),
                            Zip = reader.GetString(15),
                            GivenName = reader.GetString(16),
                            FamilyName = reader.GetString(17)
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return projects;
        }

        // Author: Kate Rich
        public Project SelectProjectByID(int projectID)
        {
            Project project = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_project_by_projectID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters["@ProjectID"].Value = projectID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    project = new Project()
                    {
                        ProjectID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ProjectTypeID = reader.GetString(2),
                        LocationID = reader.GetInt32(3),
                        UserID = reader.GetInt32(4),
                        StartDate = reader.GetDateTime(5),
                        Status = reader.GetString(6),
                        Description = reader.GetString(7)
                    };
                }
                else
                {
                    throw new ArgumentException("Project Record Not Found...");
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("No Record Found...", ex);
            }
            return project;
        }

        // Author: Kate Rich
        public ProjectVM SelectProjectInformationByProjectID(int projectID)
        {
            ProjectVM project = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_projectInformation_by_projectID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters["@ProjectID"].Value = projectID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    project = new ProjectVM()
                    {
                        ProjectID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ProjectTypeID = reader.GetString(2),
                        LocationID = reader.GetInt32(3),
                        LocationName = reader.GetString(4),
                        City = reader.GetString(5),
                        State = reader.GetString(6),
                        UserID = reader.GetInt32(7),
                        StartDate = reader.GetDateTime(8),
                        Status = reader.GetString(9),
                        Description = reader.GetString(10),
                        AcceptsDonations = reader.GetBoolean(11),
                        PayPalAccount = reader.IsDBNull(12) ? null : reader.GetString(12),
                        AvailableFunds = reader.GetDecimal(13)
                    };
                }
                else
                {
                    throw new ArgumentException("Project Record Not Found...");
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("No Record Found...", ex);
            }
            return project;
        }

        public ProjectVM SelectProjectVMByID(int projectID)
        {
            ProjectVM project = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "SP_Select_Project_by_ID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectID", projectID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    project = new ProjectVM()
                    {
                        ProjectID = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                        ProjectTypeID = reader.IsDBNull(2) ? null : reader.GetString(2),
                        LocationID = reader.GetInt32(3),
                        UserID = reader.GetInt32(4),
                        StartDate = reader.GetDateTime(5),
                        Status = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Description = reader.IsDBNull(7) ? null : reader.GetString(7),
                        AcceptsDonations = !reader.IsDBNull(8) && reader.GetBoolean(8),
                        PayPalAccount = reader.IsDBNull(9) ? null : reader.GetString(9),
                        AvailableFunds = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10),
                        LocationName = reader.IsDBNull(11) ? null : reader.GetString(11),
                        Address = reader.IsDBNull(12) ? null : reader.GetString(12),
                        City = reader.IsDBNull(13) ? null : reader.GetString(13),
                        State = reader.IsDBNull(14) ? null : reader.GetString(14),
                        Zip = reader.IsDBNull(15) ? null : reader.GetString(15),
                        GivenName = reader.IsDBNull(16) ? null : reader.GetString(16),
                        FamilyName = reader.IsDBNull(17) ? null : reader.GetString(17)
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return project;
        }

        /// <summary>
        /// Josh Nicholson
        /// Created: 2025/02/07
        /// Summary: The accessor for the AddProject stored procedure
        /// 
        /// Last Updated By: Kate Rich
        /// Last Updated: 2025-05-02
        /// What Was Changed: Updated this method to return an int - newly inserted projectID.
        /// </summary>
        public int AddProject(Project project)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_add_project", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50)).Value = project.Name;
            cmd.Parameters.Add(new SqlParameter("@ProjectTypeID", SqlDbType.VarChar, 50)).Value = project.ProjectTypeID;
            cmd.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.Int)).Value = project.LocationID;
            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = project.UserID;
            cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date)).Value = project.StartDate;
            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 25)).Value = project.Status;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 250)).Value = project.Description;
            cmd.Parameters.Add(new SqlParameter("@AcceptsDonations", SqlDbType.Bit)).Value = project.AcceptsDonations;
            cmd.Parameters.Add(new SqlParameter("@PayPalAccount", SqlDbType.VarChar, 50)).Value = project.PayPalAccount != null ? (object)project.PayPalAccount : DBNull.Value;
            cmd.Parameters.Add(new SqlParameter("@AvailableFunds", SqlDbType.Decimal, 0) { Precision = 15, Scale = 2 }).Value = project.AvailableFunds;

            try
            {
                conn.Open();
                int r = Convert.ToInt32(cmd.ExecuteScalar());
                return r;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/03/27
        /// Summary: The accessor for the GetAllProjectTypes stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<ProjectTypeObject> GetAllProjectTypes()
        {
            List<ProjectTypeObject> projectTypes = new List<ProjectTypeObject>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_project_types", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ProjectTypeObject t = new ProjectTypeObject();
                    t.ProjectType = r.GetString(0);
                    t.Description = r.GetString(1);
                    projectTypes.Add(t);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return projectTypes;
        }

        public int UpdateProject(ProjectVM oldProject, ProjectVM newProject)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = "SP_Update_Project";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NewLocationID", newProject.LocationID);
            cmd.Parameters.AddWithValue("@NewStatus", newProject.Status == "" ? oldProject.Status : newProject.Status);
            cmd.Parameters.AddWithValue("@NewDescription", newProject.Description);
            cmd.Parameters.AddWithValue("@NewAcceptsDonations", newProject.AcceptsDonations);
            cmd.Parameters.AddWithValue("@NewPayPalAccount", newProject.PayPalAccount == null ? DBNull.Value : newProject.PayPalAccount);
            cmd.Parameters.AddWithValue("@OldProjectID", oldProject.ProjectID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        // Author: Chase Hannen
        public List<Project> GetAllProjectsByUserID(int userID)
        {
            List<Project> assignedProjects = new List<Project>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_projects_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Project project = new Project();
                    project.ProjectID = reader.GetInt32(0);
                    project.Name = reader.GetString(1);
                    project.ProjectTypeID = reader.GetString(2);
                    project.LocationID = reader.GetInt32(3);
                    project.UserID = reader.GetInt32(4);
                    project.StartDate = reader.GetDateTime(5);
                    project.Status = reader.GetString(6);
                    project.Description = reader.GetString(7);
                    project.AcceptsDonations = reader.GetBoolean(8);
                    project.PayPalAccount = reader.IsDBNull(9) ? null : reader.GetString(9);
                    project.AvailableFunds = reader.GetDecimal(10);
                    assignedProjects.Add(project);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return assignedProjects;
        } // END GetAllProjectsByUserID

        public List<ProjectVM> GetAllProjectsByApprovedUserID(int UserID)
        {
            List<ProjectVM> projects = new List<ProjectVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_projects_by_approved_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = UserID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    projects.Add(new ProjectVM()
                    {
                        ProjectID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        LocationName = reader.GetString(2),
                        StartDate = reader.GetDateTime(3),
                        Status = reader.GetString(4),
                        Description = reader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return projects;
        }

        // Author: Chase Hannen
        public int UpdateAvailableFunds(int projectID, decimal amount, bool isDonation)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_available_funds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal);
            cmd.Parameters.Add("@IsDonation", SqlDbType.Bit);
            cmd.Parameters["@ProjectID"].Value = projectID;
            cmd.Parameters["@Amount"].Value = amount;
            cmd.Parameters["@IsDonation"].Value = isDonation;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
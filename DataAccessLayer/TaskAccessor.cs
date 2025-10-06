/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/10
/// Summary:  Data Access for Task related stored procedures
/// Last Updated By: Josh Nicholson
/// Last Updated: 2025/03/27
/// What was Changed: Added more task/tasktype related stored procedures
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class TaskAccessor : ITaskAccessor
    {
        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/10
        /// Summary:  Inserts a new task assignment into the database using the provided taskID and userID
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public bool InsertTaskAssignment(int taskID, int userID)
        {
            bool result = false;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_task_assignment", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = taskID;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() == 1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Creator:  Stan Anderson
        /// Created:  2025/02/12
        /// Summary:  Selects a list of tasks by eventID from the database
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/13
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> SelectTasksByEventID(int eventID)
        {
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_tasks_by_eventid", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = eventID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new DataDomain.Task()
                    {
                        TaskID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        ProjectID = reader.GetInt32(3),
                        TaskType = reader.GetString(4),
                        EventID = eventID,
                        Active = true
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return tasks;
        }

        /// Creator:  Stan Anderson
        /// Created:  2025/02/18
        /// Summary:  The accessor for methods related to volunteers assigned to events.
        ///  Last Updated By: Stan Anderson
        /// Last Updated: 2025/02/18
        /// What was Changed: Moved to TaskAccessor
        /// </summary>
        public List<TaskAssignedViewModel> SelectVolunteersAndTasksByEventID(int eventID)
        {
            List<TaskAssignedViewModel> volunteers = new List<TaskAssignedViewModel>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_view_event_volunteers", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = eventID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    volunteers.Add(new TaskAssignedViewModel
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        City = reader.IsDBNull(3) ? null : reader.GetString(3),
                        State = reader.IsDBNull(4) ? null : reader.GetString(4),
                        TaskID = reader.GetInt32(5),
                        TaskName = reader.GetString(6),
                        TaskDescription = reader.GetString(7),
                        EventID = eventID
                    });
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return volunteers;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor for the GetAllTaskTypes stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public List<TaskTypeObject> GetAllTaskTypes()
        {
            List<TaskTypeObject> taskTypes = new List<TaskTypeObject>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_task_types", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    TaskTypeObject t = new TaskTypeObject();
                    t.TaskType = r.GetString(0);
                    t.Description = r.GetString(1);
                    taskTypes.Add(t);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskTypes;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor for the AddTask stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool AddTask(DataDomain.Task task)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_add_task", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100)).Value = task.Name;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 250)).Value = task.Description != null ? (object)task.Description : DBNull.Value;
            cmd.Parameters.Add(new SqlParameter("@TaskDate", SqlDbType.Date)).Value = task.TaskDate;
            cmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.Int)).Value = task.ProjectID;
            cmd.Parameters.Add(new SqlParameter("@TaskType", SqlDbType.VarChar, 50)).Value = task.TaskType;
            cmd.Parameters.Add(new SqlParameter("@EventID", SqlDbType.Int)).Value = task.EventID != 0 ? task.EventID : DBNull.Value;
            cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit)).Value = task.Active;

            try
            {
                conn.Open();
                var r = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor for the UpdateTaskByTaskID stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public bool UpdateTaskByTaskID(DataDomain.Task task)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_task_by_taskID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@TaskID", SqlDbType.Int)).Value = task.TaskID;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100)).Value = task.Name;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 250)).Value = task.Description != null ? (object)task.Description : DBNull.Value;
            cmd.Parameters.Add(new SqlParameter("@TaskDate", SqlDbType.Date)).Value = task.TaskDate;
            cmd.Parameters.Add(new SqlParameter("@TaskType", SqlDbType.VarChar, 50)).Value = task.TaskType;
            cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit)).Value = task.Active;

            try
            {
                conn.Open();
                var r = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// Creator: Josh Nicholson
        /// Created: 2025/02/20
        /// Summary: The accessor for the GetTaskByTaskID stored procedure
        /// Last Updated By: Josh Nicholson
        /// Last Updated: 2025/03/27
        /// What was Changed: Compiled to the main project clone
        /// </summary>
        public DataDomain.Task GetTaskByTaskID(int taskID)
        {
            DataDomain.Task t = new DataDomain.Task();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_task_by_taskID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@TaskID", SqlDbType.Int)).Value = taskID;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    t.TaskID = r.GetInt32(0);
                    t.Name = r.GetString(1);
                    t.Description = r.GetString(2);
                    t.TaskDate = r.GetDateTime(3);
                    t.ProjectID = r.GetInt32(4);
                    t.TaskType = r.GetString(5);
                    t.EventID = r.GetInt32(6);
                    t.Active = r.GetBoolean(7);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return t;
        }

        /// <summary>
        /// Creator:  Josh Nicholson
        /// Created:  2025/04/04
        /// Summary:  Selects a list of tasks by projectID from the database
        ///  Last Updated By: Josh Nicholson
        /// Last Updated: 2025/04/04
        /// What was Changed: Added comments
        /// </summary>
        public List<DataDomain.Task> SelectTasksByProjectID(int projectID)
        {
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_tasks_by_projectid", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters["@ProjectID"].Value = projectID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new DataDomain.Task()
                    {
                        TaskID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        ProjectID = projectID,
                        TaskType = reader.GetString(3),
                        EventID = reader.GetInt32(4),
                        Active = true
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return tasks;
        }

        /// <summary>
        /// Creator:  Brodie Pasker
        /// Created:  2025/04/13
        /// Summary:  Method for getting a list of tasks assined to a User from the database
        ///  Last Updated By: Brodie Pasker
        /// Last Updated: 2025/04/13
        /// What was Changed: creation
        /// </summary>
        public List<DataDomain.Task> SelectTasksByUserID(int UserID)
        {
            List<DataDomain.Task> tasks = new List<DataDomain.Task>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_tasks_assigned_by_user_id", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = UserID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new DataDomain.Task()
                    {
                        TaskID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        EventID = reader.GetInt32(2),
                        ProjectID = reader.GetInt32(3),
                        TaskDate = reader.GetDateTime(4),
                        Active = reader.GetBoolean(5)
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return tasks;
        }
    }
}

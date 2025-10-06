/// <summary>
/// Christivie Mauwa
/// Created: 2025/02/06
/// 
/// Actual summary of the class if needed, example is for DTO
/// Class for the creation of User Objects with set data fields
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Christivie Mauwa
    /// Created: 2025/02/21
    /// 
    /// Actual summary of the class if needed.
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    /// <param name="sender"></param>
    public class ProjectTypeAccessor : IProjectTypeAccessor
    {
        /// <summary>
        /// Creator:  Christivie Mauwa
        /// Created:  2025/03/07
        /// Summary:  This method returns the number input.
        /// Last Updated By: Christivie Mauwa
        /// Last Updated: 2025/03/07
        /// What was Changed: Initial Creation
        /// </summary>
        public List<ProjectType> SelectAllprojectTypes()
        {
            List<ProjectType> projectTypes = new List<ProjectType>();
            var conn = DBConnection.GetConnection();
            var cmdText = "SP_Select_All_ProjectTypes";
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
                        projectTypes.Add(new ProjectType()
                        {
                            ProjectTypeID = reader.GetString(0),
                            Description = reader.GetString(1)
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
            return projectTypes;
        }

        /// <summary>
        /// Creator:  Christivie Mauwa
        /// Created:  2025/03/07
        /// Summary:  This method returns the number input.
        /// Last Updated By: Christivie Mauwa
        /// Last Updated: 2025/03/07
        /// What was Changed: Initial Creation
        /// </summary>
        public ProjectType SelectProjectTypeID(string id)
        {
            ProjectType projectType = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "SP_Select_ProjectType_ID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectTypeID", id);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    projectType = new ProjectType()
                    {
                        ProjectTypeID = reader.GetString(0),
                        Description = reader.GetString(1)
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
            return projectType;
        }
    }
}

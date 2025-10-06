/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-10
/// Summary:
///     Class that implements the IBackgroundCheckAccessor Interface - used for
///     accessing BackgroundCheck data from the DB.
/// 
/// Updated By: Kate Rich
/// Updated: 2025-02-20
/// What Was Changed:
///     Added & implemented the SelectBackgroundChecksByProjectID method.
///     
/// Updated By: Kate Rich
/// Updated: 2025-02-26
/// What Was Changed:
///     Added & implemented the UpdateBackgroundCheck method.
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-03-27
/// What Was Changed:
///     Added & implemented the SelectBackgroundCheckByID method.
/// </summary>

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
    public class BackgroundCheckAccessor : IBackgroundCheckAccessor
    {
        // Author: Kate Rich
        public int InsertBackgroundCheck(BackgroundCheck backgroundCheck)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_backgroundCheck", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Investigator", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);
            cmd.Parameters["@Investigator"].Value = backgroundCheck.Investigator;
            cmd.Parameters["@UserID"].Value = backgroundCheck.UserID;
            cmd.Parameters["@ProjectID"].Value = backgroundCheck.ProjectID;
            cmd.Parameters["@Status"].Value = backgroundCheck.Status;
            cmd.Parameters["@Description"].Value = backgroundCheck.Description;
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

        public BackgroundCheckVM SelectBackgroundCheckByID(int backgroundCheckID)
        {
            Console.WriteLine(backgroundCheckID);

            BackgroundCheckVM backgroundCheck = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_backgroundCheck_by_ID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BackgroundCheckID", SqlDbType.Int);
            cmd.Parameters["@BackgroundCheckID"].Value = backgroundCheckID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Read();
                    backgroundCheck = new BackgroundCheckVM()
                    {
                        BackgroundCheckID = reader.GetInt32(0),
                        Investigator = reader.GetInt32(1),
                        InvestigatorGivenName = reader.GetString(2),
                        InvestigatorFamilyName = reader.GetString(3),
                        UserID = reader.GetInt32(4),
                        VolunteerGivenName = reader.GetString(5),
                        VolunteerFamilyName = reader.GetString(6),
                        ProjectID = reader.GetInt32(7),
                        ProjectName = reader.GetString(8),
                        Status = reader.GetString(9),
                        Description = reader.GetString(10)
                    };
                }
                else
                {
                    throw new ArgumentException("Background Check Record Not Found...");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return backgroundCheck;
        }

        // Author: Kate Rich
        public List<BackgroundCheckVM> SelectBackgroundChecksByProjectID(int projectID)
        {
            List<BackgroundCheckVM> backgroundChecks = new List<BackgroundCheckVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_backgroundChecks_by_projectID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters["@ProjectID"].Value = projectID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    backgroundChecks.Add(new BackgroundCheckVM()
                    {
                        BackgroundCheckID = reader.GetInt32(0),
                        Investigator = reader.GetInt32(1),
                        InvestigatorGivenName = reader.GetString(2),
                        InvestigatorFamilyName = reader.GetString(3),
                        UserID = reader.GetInt32(4),
                        VolunteerGivenName = reader.GetString(5),
                        VolunteerFamilyName = reader.GetString(6),
                        ProjectID = reader.GetInt32(7),
                        ProjectName = reader.GetString(8),
                        Status = reader.GetString(9),
                        Description = reader.GetString(10)
                    });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return backgroundChecks;
        }

        public int UpdateBackgroundCheck(BackgroundCheck oldBackgroundCheck, BackgroundCheck newBackgroundCheck)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_backgroundCheck", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BackgroundCheckID", SqlDbType.Int);
            cmd.Parameters.Add("@Investigator", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters.Add("@OldStatus", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@NewStatus", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 250);
            cmd.Parameters["@BackgroundCheckID"].Value = oldBackgroundCheck.BackgroundCheckID;
            cmd.Parameters["@Investigator"].Value = oldBackgroundCheck.Investigator;
            cmd.Parameters["@UserID"].Value = oldBackgroundCheck.UserID;
            cmd.Parameters["@ProjectID"].Value = oldBackgroundCheck.ProjectID;
            cmd.Parameters["@OldStatus"].Value = oldBackgroundCheck.Status;
            cmd.Parameters["@NewStatus"].Value = newBackgroundCheck.Status;
            cmd.Parameters["@OldDescription"].Value = oldBackgroundCheck.Description;
            cmd.Parameters["@NewDescription"].Value = newBackgroundCheck.Description;

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
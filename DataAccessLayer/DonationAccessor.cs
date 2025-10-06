/// <summary>
/// Creator: Kate Rich
/// Created: 2025-03-03
/// Summary:
///     Class that implements the IDonationAccessor Interface - used for
///     accessing Donation data from the DB.
/// 
/// Updated by : Christivie Mauwa
/// Updated : 2025-03-28
/// What was changed : Added SelectAllDonation, InsertDonation, SelectDonationByUserID,SelectDonationByDonationID
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025-04-01
/// What Was Changed:
///     Added the SelectToViewDonationsByProjectID method.
///     
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-05-02
/// What Was Changed: Made Amount nullable in SelectToViewDonationsByProjectID
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DonationAccessor : IDonationAccessor
    {
        // Author: Kate Rich
        public List<DonationSummaryVM> SelectMonetaryProjectDonationSummariesByUserID(int userID)
        {
            List<DonationSummaryVM> donationSummaries = new List<DonationSummaryVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_project_monetary_donation_summaries_by_userID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    donationSummaries.Add(new DonationSummaryVM()
                    {
                        ProjectID = reader.GetInt32(0),
                        ProjectName = reader.GetString(1),
                        MonetaryDonationTotal = reader.GetDecimal(2),
                        LastDonationDate = reader.GetDateTime(3),
                        ProjectLocation = reader.GetString(4)
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
            return donationSummaries;
        }

        // Author: Christivie Mauwa 
        public List<DonationVM> SelectAllDonation()
        {
            List<DonationVM> donationVMs = new List<DonationVM>();
            var conn = DBConnection.GetConnection();
            var cmdText = "SP_Select_All_Donation";
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
                        donationVMs.Add(new DonationVM()
                        {
                            DonationID = reader.GetInt32(0),
                            DonationType = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            UserID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                            ProjectID = reader.GetInt32(3),
                            Amount = reader.IsDBNull(4) ? 0.0m : reader.GetDecimal(4),
                            DonationDate = reader.GetDateTime(5),
                            Description = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                            UserName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                            Name = reader.IsDBNull(8) ? string.Empty : reader.GetString(8)

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
            return donationVMs;
        }

        // Author : Christivie
        public int InsertDonation(Donation donation)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_Insert_Donation";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DonationType", donation.DonationType);
            cmd.Parameters.AddWithValue("@UserID", donation.UserID != 0 ? donation.UserID : DBNull.Value);
            cmd.Parameters.AddWithValue("@ProjectID", donation.ProjectID);
            cmd.Parameters.AddWithValue("@Amount", donation.Amount);
            cmd.Parameters.AddWithValue("@DonationDate", donation.DonationDate);
            cmd.Parameters.AddWithValue("@Description", donation.Description);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        // Author: Christivie
        public List<DonationVM> SelectDonationByUserID(int userID)
        {
            List<DonationVM> donationVM = new List<DonationVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "SP_Select_Donation_by_User_ID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        donationVM.Add(new DonationVM()
                        {
                            DonationID = reader.GetInt32(0),
                            DonationType = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            UserID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                            ProjectID = reader.GetInt32(3),
                            Amount = reader.IsDBNull(4) ? 0.0m : reader.GetDecimal(4),
                            DonationDate = reader.GetDateTime(5),
                            Description = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                            UserName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                            Name = reader.IsDBNull(8) ? string.Empty : reader.GetString(8)
                        });
                    }
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
            return donationVM;
        }

        // Author : Chrisitive
        public DonationVM SelectDonationByDonationID(int donationID)
        {
            DonationVM donationVM = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "SP_Select_Donation_by_Donation_ID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DonationID", donationID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    donationVM = new DonationVM()
                    {
                        DonationID = reader.GetInt32(0),
                        UserID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        ProjectID = reader.GetInt32(2),
                        Name = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        Amount = reader.IsDBNull(4) ? 0.0m : reader.GetDecimal(4),
                        DonationDate = reader.GetDateTime(5),
                        Description = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
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
            return donationVM;
        }

        // Author: Akoi Kollie
        public List<Donation> SelectToViewDonationsByProjectID(int projectid)
        {
            List<Donation> donations = new List<Donation>();
            var conn = DBConnection.GetConnection();
            //a command to execute the store procedure
            var cmd = new SqlCommand("sp_select_to_view_donation_record_by_projectiID", conn);
            // tell the command type to use
            cmd.CommandType = CommandType.StoredProcedure;
            //add parameters to the command
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            // set the values for the parameter
            cmd.Parameters["@ProjectID"].Value = projectid;
            try
            {
                //Open connection
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Donation donation = new Donation();
                    donation.DonationID = r.GetInt32(0);
                    donation.DonationType = r.GetString(1);
                    donation.UserID = r.IsDBNull(2) ? 0 : r.GetInt32(2);
                    donation.ProjectID = r.GetInt32(3);
                    donation.Amount = r.IsDBNull(4) ? 0.0m : r.GetDecimal(4);
                    donation.DonationDate = r.GetDateTime(5);
                    donation.Description = r.GetString(6);
                    donations.Add(donation);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return donations;
        }
    }
}
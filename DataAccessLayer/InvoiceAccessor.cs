/// <summary>
/// Akoi Kollie
/// Created: 2025/02/12
/// 
/// This is making connection to the database
/// It using interface to connect to database and the invoice class.
/// </summary>
///
/// <remarks>
/// Updater  Akoi
/// Updated: 2025/02/28
/// 
/// Updater  Syler Bushlack
/// Updated: 2025/04/30
/// What was changed: Fixed bugs in SelectInvoiceByInvoiceID method
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
    public class InvoiceAccessor : IInvoiceAccessor
    {
        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/04/25
        /// Summary:
        ///     Calling "sp_select_all_invoices_by_projectid" stored procedure from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// /// <param name="projectId"></param>
        /// 
        public List<Invoice> GetInvoices()
        {
            List<Invoice> invoiceList = new List<Invoice>();
            //Make connection
            var ton = DBConnection.GetConnection();
            //The query to connection to the database
            var cmd = new SqlCommand("sp_view_all_invoices", ton);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                //open connection
                ton.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Invoice invoice = new Invoice();
                        invoice.InvoiceID = reader.GetInt32(0);
                        invoice.InvoiceNumber = reader.GetString(1);
                        invoice.ExpenseID = reader.IsDBNull(2) ? null : reader.GetInt32(2);
                        invoice.ProjectID = reader.GetInt32(3);
                        invoice.InvoiceDate = reader.GetDateTime(4);
                        invoice.Status = reader.GetString(5);
                        invoice.Description = reader.GetString(6);
                        invoiceList.Add(invoice);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ton.Close();
            }
            return invoiceList;
        }
        /// <summary>
        /// Eric Idle
        /// Created: 2025/02/19
        /// 
        /// Calling "sp_select_all_invoices_by_projectid" stored procedure from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// /// <param name="projectId"></param>
        public List<Invoice> SelectAllInvoicesByProjectID(int projectId)
        {
            List<Invoice> invoices = new List<Invoice>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_invoices_by_projectid", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters[0].Value = projectId;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Invoice i = new Invoice();
                    i.InvoiceID = r.GetInt32(0);
                    i.InvoiceNumber = r.GetString(1);
                    i.ExpenseID = null;
                    i.ProjectID = r.GetInt32(3);
                    i.InvoiceDate = r.GetDateTime(4);
                    i.Status = r.GetString(5);
                    i.Description = r.GetString(6);

                    invoices.Add(i);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return invoices;
        }

        public int InsertInvoice(Invoice invoice)
        {
            int results = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_invoice", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InvoiceNumber", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ExpenseID", SqlDbType.Int);
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters.Add("@InvoiceDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters["@InvoiceNumber"].Value = invoice.InvoiceNumber;
            cmd.Parameters["@ExpenseID"].Value = invoice.ExpenseID ?? (object)DBNull.Value;
            cmd.Parameters["@ProjectID"].Value = invoice.ProjectID;
            cmd.Parameters["@InvoiceDate"].Value = invoice.InvoiceDate;
            cmd.Parameters["@Status"].Value = invoice.Status;
            cmd.Parameters["@Description"].Value = invoice.Description;


            try
            {
                conn.Open();
                results = cmd.ExecuteNonQuery();

            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return results;
        }

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/04/01
        /// Summary:
        ///     Calling "sp_select_invoice_status_by_id" stored procedure from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Syler Bushlack
        /// Updated: 2025/04/20
        /// example: Fixed ExpenseID not accepting a null value when it should
        /// </remarks>
        /// <param name="invoiceid"></param>
        public Invoice SelectInvoiceByInvoiceID(int invoiceid)
        {
            Invoice invoice = null;
            var conn = DBConnection.GetConnection();
            //a command to execute the store procedure
            var cmd = new SqlCommand("sp_select_invoice_status_by_id", conn);
            // tell the command type to use
            cmd.CommandType = CommandType.StoredProcedure;
            //add parameters to the command
            cmd.Parameters.Add("@InvoiceID", SqlDbType.Int);
            // set the values for the parameter
            cmd.Parameters["@InvoiceID"].Value = invoiceid;

            try
            {
                //Open connection
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    invoice = new Invoice();
                    invoice.InvoiceID = r.GetInt32(0);
                    invoice.InvoiceNumber = r.GetString(1);
                    //invoice.ExpenseID = r.GetInt32(2);
                    invoice.ExpenseID = r.IsDBNull(2) ? null : r.GetInt32(2);
                    invoice.ProjectID = r.GetInt32(3);
                    invoice.InvoiceDate = r.GetDateTime(4);
                    invoice.Status = r.GetString(5);
                    invoice.Description = r.GetString(6);
                }
            }
            catch (Exception)
            {
                throw new Exception("Invoice not found");
            }
            return invoice;
        }

        public int UpdateChangeInvoiceStatusByInvoiceID(int invoiceid, string status)
        {
            int result = 0;
            var conn = DBConnection.GetConnection();
            //a command to execute the store procedure
            var cmd = new SqlCommand("sp_Update_invoice_status_by_id", conn);
            // tell the command type to use
            cmd.CommandType = CommandType.StoredProcedure;
            //add parameters to the command
            cmd.Parameters.Add("@InvoiceID", SqlDbType.Int);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            // set the values for the parameter
            cmd.Parameters["@InvoiceID"].Value = invoiceid;
            cmd.Parameters["@Status"].Value = status;
            try
            {
                //Open connection
                conn.Open();
                result = cmd.ExecuteNonQuery();

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
    }
}

/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/24
/// Summary:
///     The accessor to ExpenseAccessor that uses methods
/// </summary>
///
/// <remarks>
/// Updater Eric Idle
/// Updated: 2025/03/04
///     Added selecting a single expense based on expenseId, projectId
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
    public class ExpenseAccessor : IExpenseAccessor
    {
        /// <summary>
        /// Creator: Eric Idle
        /// Created: 2025/03/16
        /// Summary:
        ///     Calling "sp_insert_expense_by_projectid" stored procedure from the database
        /// </summary>
        /// <param name="projectId"></param>
        public int InsertExpenseByProjectID(Expense expense)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_expense_by_projectid", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters.Add("@ExpenseTypeID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Date", SqlDbType.Date);
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);
            cmd.Parameters["@ProjectID"].Value = expense.ProjectID;
            cmd.Parameters["@ExpenseTypeID"].Value = expense.ExpenseTypeID;
            cmd.Parameters["@Date"].Value = expense.Date;
            cmd.Parameters["@Amount"].Value = expense.Amount;
            cmd.Parameters["@Description"].Value = expense.Description;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Creator: Eric Idle
        /// Created: 2025/02/24
        /// Summary:
        ///     Calling "sp_select_all_expenses_by_projectid" stored procedure from the database
        /// </summary>
        /// <param name="projectId"></param>
        public List<Expense> SelectAllExpensesByProjectID(int projectId)
        {
            List<Expense> expenses = new List<Expense>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_expenses_by_projectid", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters[0].Value = projectId;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Expense e = new Expense();
                    e.ExpenseID = r.GetInt32(0);
                    e.ProjectID = r.GetInt32(1);
                    e.ExpenseTypeID = r.GetString(2);
                    e.Date = r.GetDateTime(3);
                    e.Amount = r.GetDecimal(4);
                    e.Description = r.GetString(5);

                    expenses.Add(e);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return expenses;
        }

        /// <summary>
        /// Creator: Eric Idle
        /// Created: 2025/03/04
        /// Summary:
        ///     Calling "sp_select_expenses_by_expenseid_projectid" stored procedure from the database
        /// </summary>
        /// <param name="expenseId"></param>
        /// <param name="projectId"></param>
        public Expense SelectExpenseByExpenseIDProjectID(int expenseId, int projectId)
        {
            // sp_select_expenses_by_expenseid_projectid
            Expense expense = new Expense();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_expenses_by_expenseid_projectid", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ExpenseID", SqlDbType.Int).Value = expenseId;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectId;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();

                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        expense.ExpenseID = r.GetInt32(0);
                        expense.ProjectID = r.GetInt32(1);
                        expense.ExpenseTypeID = r.GetString(2);
                        expense.Date = r.GetDateTime(3);
                        expense.Amount = r.GetDecimal(4);
                        expense.Description = r.GetString(5);
                    }
                }
                else
                {
                    // Handle the case where no data is returned
                    throw new Exception("No data found.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return expense;
        }
    }
}
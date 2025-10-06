/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/25
/// 
/// The ExpenseType Accessor class
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
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
    public class ExpenseTypeAccessor : IExpenseTypeAccessor
    {
        public List<ExpenseType> SelectAllExpenseTypes()
        {
            List<ExpenseType> expenseTypes = new List<ExpenseType>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_expensetypes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ExpenseType e = new ExpenseType();
                    e.ExpenseTypeID = r.GetString(0);
                    e.Description = r.GetString(1);

                    expenseTypes.Add(e);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return expenseTypes;
        }
    }
}

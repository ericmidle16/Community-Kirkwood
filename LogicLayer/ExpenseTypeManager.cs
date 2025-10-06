/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/25
/// 
/// The ExpenseType Manager class
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
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
    public class ExpenseTypeManager : IExpenseTypeManager
    {
        private IExpenseTypeAccessor _expenseTypeAccessor;

        public ExpenseTypeManager()
        {
            _expenseTypeAccessor = new ExpenseTypeAccessor();
        }

        public ExpenseTypeManager(IExpenseTypeAccessor expenseTypeAccessor)
        {
            _expenseTypeAccessor = expenseTypeAccessor;
        }

        public List<ExpenseType> GetAllExpenseTypes()
        {
            List<ExpenseType> expenseTypes = null;

            try
            {
                expenseTypes = _expenseTypeAccessor.SelectAllExpenseTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data no available", ex);
            }
            return expenseTypes;
        }
    }
}

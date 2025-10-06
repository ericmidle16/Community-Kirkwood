/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/24
/// 
/// Class accessor for fake ExpenseType data
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: 
/// </remarks>
using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class ExpenseTypeAccessorFakes : IExpenseTypeAccessor
    {
        private List<ExpenseType> _expenseTypes;

        public ExpenseTypeAccessorFakes()
        {
            _expenseTypes = new List<ExpenseType>();
            _expenseTypes.Add(new ExpenseType()
            {
                ExpenseTypeID = "ExpenseTypeOne",
                Description = "Expenses Desc of Type One"
            });
            _expenseTypes.Add(new ExpenseType()
            {
                ExpenseTypeID = "ExpenseTypeTwo",
                Description = "Expenses Desc of Type Two"
            });
            _expenseTypes.Add(new ExpenseType()
            {
                ExpenseTypeID = "ExpenseTypeThree",
                Description = "Expenses Desc of Type Three"
            });

        }

        public List<ExpenseType> SelectAllExpenseTypes()
        {
            return _expenseTypes;
        }
    }
}

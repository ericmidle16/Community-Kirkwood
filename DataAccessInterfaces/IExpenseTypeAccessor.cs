/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/25
/// 
/// The ExpenseType Accessor interface
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IExpenseTypeAccessor
    {
        List<ExpenseType> SelectAllExpenseTypes();
    }
}

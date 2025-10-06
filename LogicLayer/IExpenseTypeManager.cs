/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/25
/// 
/// The ExpenseType Manager interface
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

namespace LogicLayer
{
    public interface IExpenseTypeManager
    {
        List<ExpenseType> GetAllExpenseTypes();
    }
}

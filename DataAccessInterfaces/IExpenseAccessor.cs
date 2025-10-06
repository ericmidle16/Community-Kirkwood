/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/24
/// Summary:
///     The interface for Expense Accessors
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IExpenseAccessor
    {
        List<Expense> SelectAllExpensesByProjectID(int projectId);
        Expense SelectExpenseByExpenseIDProjectID(int expenseId, int projectId);
        int InsertExpenseByProjectID(Expense expense);
    }
}
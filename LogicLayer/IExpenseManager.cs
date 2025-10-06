/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/18
/// Summary:
///     The interface for manager to Expense
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IExpenseManager
    {
        List<Expense> GetAllExpensesByProjectID(int projectId);
        Expense GetExpenseByExpenseIDProjectID(int expenseId, int projectId);
        bool AddExpenseByProjectID(Expense expense);
    }
}
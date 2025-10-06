/// <summary>
/// Creator: Eric Idle
/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/24
/// Summary:
///     Class accessor for fake Expense data
/// </summary>
/// 
/// <remarks>
/// Updater Name Eric Idle
/// Updated: 2025/03/04
/// 
/// Updater:    Chase Hannen
/// Updated:    2025/05/02
///     Changed amount double to decimal
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
    public class ExpenseAccessorFakes : IExpenseAccessor
    {
        private List<Expense> _expenses;

        public ExpenseAccessorFakes()
        {
            _expenses = new List<Expense>();
            _expenses.Add(new Expense()
            {
                ExpenseID = 1000001,
                ProjectID = 1000001,
                ExpenseTypeID = "ExpenseTypeOne",
                Date = DateTime.Now,
                Amount = 100.59m,
                Description = "Expenses Desc of Type One"
            });
            _expenses.Add(new Expense()
            {
                ExpenseID = 1000002,
                ProjectID = 1000001,
                ExpenseTypeID = "ExpenseTypeTwo",
                Date = DateTime.Now,
                Amount = 10.47m,
                Description = "Expenses Desc of Type Two"
            });
            _expenses.Add(new Expense()
            {
                ExpenseID = 1000003,
                ProjectID = 1000002,
                ExpenseTypeID = "ExpenseTypeThree",
                Date = DateTime.Now,
                Amount = 500.13m,
                Description = "Expenses Desc of Type Three"
            });

        }

        /// <summary>
        /// Creator: Eric Idle
        /// Created: 2025/02/24
        /// Summary: 
        /// </summary>
        public int InsertExpenseByProjectID(Expense addExpense)
        {
            int initialCount = _expenses.Count;

            Expense expense = _expenses.Find(e => e.ExpenseID == addExpense.ExpenseID);
            if (expense == null)
            {
                _expenses.Add(addExpense);

                if(_expenses.Count != initialCount)
                {
                    return 1;
                }
            }
            throw new Exception("Test Failed.");
        }

        /// <summary>
        /// Creator: Eric Idle
        /// Created: 2025/02/24
        /// Summary: 
        /// </summary>
        public List<Expense> SelectAllExpensesByProjectID(int projectId)
        {
            List<Expense> filteredExpenses = _expenses.Where(p => p.ProjectID == projectId).ToList();
            return filteredExpenses;
        }

        /// <summary>
        /// Creator: Eric Idle
        /// Created: 2025/02/24
        /// Summary: 
        /// </summary>
        public Expense SelectExpenseByExpenseIDProjectID(int expenseId, int projectId)
        {
            List<Expense> filteredExpenses = _expenses
                .Where(e => e.ExpenseID == expenseId && e.ProjectID == projectId)
                .ToList();

            return filteredExpenses.FirstOrDefault();
        }
    }
}
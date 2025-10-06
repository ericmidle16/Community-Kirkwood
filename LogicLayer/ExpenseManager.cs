/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/18
/// Summary:
///     The manager to Expense that uses methods
/// </summary>

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
    public class ExpenseManager : IExpenseManager
    {
        private IExpenseAccessor _expenseAccessor;

        public ExpenseManager()
        {
            _expenseAccessor = new ExpenseAccessor();
        }

        public ExpenseManager(IExpenseAccessor expenseAccessor)
        {
            _expenseAccessor = expenseAccessor;
        }

        /// <summary>
        /// Creator: Eric Idle
        /// Created: 2025/03/04
        /// Summary:
        ///     This is to grab an specific expense based on expenseId and projectId
        /// </summary>
        public Expense GetExpenseByExpenseIDProjectID(int expenseId, int projectId)
        {
            Expense expense = null;

            try
            {
                expense = _expenseAccessor.SelectExpenseByExpenseIDProjectID(expenseId, projectId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data no available", ex);
            }
            return expense;
        }

        /// <summary>
        /// Creator: Eric Idle
        /// Created: 2025/02/24
        /// Summary:
        ///     This is to grab all expenses from the expense accessor
        /// </summary>
        public List<Expense> GetAllExpensesByProjectID(int projectId)
        {
            List<Expense> expenses = null;

            try
            {
                expenses = _expenseAccessor.SelectAllExpensesByProjectID(projectId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data no available", ex);
            }
            return expenses;
        }

        /// <summary>
        /// Creator: Eric Idle
        /// Created: 2025/03/16
        /// Summary:
        ///     This is to add a new expense to the project
        /// </summary>
        public bool AddExpenseByProjectID(Expense expense)
        {
            try
            {
                _expenseAccessor.InsertExpenseByProjectID(expense);
            }
            catch(Exception ex)
            {
                throw new Exception("Data not available", ex);
            }
            return true;
        }
    }
}
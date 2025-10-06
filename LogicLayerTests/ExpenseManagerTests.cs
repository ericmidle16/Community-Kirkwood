/// <summary>
/// Creator: Eric Idle
/// Created: 2025/02/28
/// 
/// Testing class for Expenses manager
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataAccessFakes;
using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class ExpenseManagerTests
    {
        private IExpenseManager _expenseManager;
        private Expense _expense = new Expense();

        [TestInitialize]
        public void TestSetup()
        {
            _expenseManager = new ExpenseManager(new ExpenseAccessorFakes());

        }

        /// <summary>
        /// Creator:  Eric Idle
        /// Created:  2025/02/28
        /// Summary:  
        /// Last Updated By:
        /// Last Updated:
        /// What was Changed:
        /// </summary>
        [TestMethod]
        public void TestGetAllExpensesByProjectID_CorrectInvoiceCount()
        {
            int projectId = 1000002;
            int numberOfExpenses = 1;
            List<Expense> expenses = _expenseManager.GetAllExpensesByProjectID(projectId);
            Assert.AreEqual(numberOfExpenses, expenses.Count());
        }

        /// <summary>
        /// Creator:  Eric Idle
        /// Created:  2025/02/28
        /// Summary:  
        /// Last Updated By:
        /// Last Updated:
        /// What was Changed:
        /// </summary>
        [TestMethod]
        public void TestGetAllExpensesByProjectID_WrongInvoiceCount()
        {
            int projectId = 1000002;
            int numberOfExpenses = 5;
            List<Expense> expenses = _expenseManager.GetAllExpensesByProjectID(projectId);
            Assert.AreNotEqual(numberOfExpenses, expenses.Count());
        }

        /// <summary>
        /// Creator:  Eric Idle
        /// Created:  2025/02/28
        /// Summary:  
        /// Last Updated By:
        /// Last Updated:
        /// What was Changed:
        /// </summary>
        [TestMethod]
        public void TestGetExpenseByExpenseIDProjectID_CorrectExpenseIDProjectID()
        {
            int expenseId = 1000003;
            int projectId = 1000002;
            int expectedExpenseId = 1000003;
            int expectedProjectId = 1000002;

            Expense expense = _expenseManager.GetExpenseByExpenseIDProjectID(expenseId, projectId);

            
            Assert.IsNotNull(expense, "Expense should not be null.");

            Assert.AreEqual(expectedExpenseId, expense.ExpenseID, "Expense ID does not match.");
            Assert.AreEqual(expectedProjectId, expense.ProjectID, "Project ID does not match.");
        }

        /// <summary>
        /// Creator:  Eric Idle
        /// Created:  2025/02/28
        /// Summary:  
        /// Last Updated By:
        /// Last Updated:
        /// What was Changed:
        /// </summary>
        [TestMethod]
        public void TestGetExpenseByExpenseIDProjectID_WrongExpenseIDProjectID()
        {
            int expenseId = 1000010;
            int projectId = 1000100;
            int expectedExpenseId = 1000003;
            int expectedProjectId = 1000002;

            Expense expense = _expenseManager.GetExpenseByExpenseIDProjectID(expenseId, projectId);


            Assert.IsNull(expense, "Expense should be null.");
        }

        /// <summary>
        /// Creator:  Eric Idle
        /// Created:  2025/02/28
        /// Summary:  
        /// Last Updated By:
        /// Last Updated:
        /// What was Changed:
        /// </summary>
        [TestMethod]
        public void TestExpenseAdd()
        {
            bool expectedResult = true;
            bool actualResult = false;

            var newExpense = new Expense()
            {
                ProjectID = 100001,
                ExpenseTypeID = "Expense Type 1",
                Date = new DateTime(2025, 1, 5),
                Amount = 123.67m,
                Description = "Big bills, big expenses"
            };

            actualResult = _expenseManager.AddExpenseByProjectID(newExpense);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

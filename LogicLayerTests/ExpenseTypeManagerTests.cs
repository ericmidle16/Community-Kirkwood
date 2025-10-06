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
using DataAccessInterfaces;
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
    public class ExpenseTypeManagerTests
    {
        private IExpenseTypeManager _expenseTypeManager;

        [TestInitialize]
        public void TestSetup()
        {
            _expenseTypeManager = new ExpenseTypeManager(new ExpenseTypeAccessorFakes());

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
        public void TestGetAllExpenseTypes_CorrectCount()
        {
            int numberOfExpenseTypes = 3;
            List<ExpenseType> expenseTypes = _expenseTypeManager.GetAllExpenseTypes();
            Assert.AreEqual(numberOfExpenseTypes, expenseTypes.Count());
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
        public void TestGetAllExpenseTypes_WrongCount()
        {
            int numberOfExpenseTypes = 5;
            List<ExpenseType> expenseTypes = _expenseTypeManager.GetAllExpenseTypes();
            Assert.AreNotEqual(numberOfExpenseTypes, expenseTypes.Count());
        }
    }
}

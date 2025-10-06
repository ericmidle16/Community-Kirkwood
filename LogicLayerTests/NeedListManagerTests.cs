using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using LogicLayer;
using DataDomain;

namespace LogicLayerTests
{
    [TestClass]
    public class NeedListManagerTests
    {
        private INeedListManager _needListManager;
        [TestInitialize]
        public void TestSetup()
        {
            _needListManager = new NeedListManager(new NeedListAccessorFakes());
        }
        [TestMethod]
        //CREATOR: Dat Tran
        public void TestProjectIDReturnsListOfOneItem()
        {
            const int projectID = 100005;
            const string expectedName = "Hatchet";
            string actualName = "";
            List<NeedList> needList = new List<NeedList>();
            needList = _needListManager.GetNeedList(projectID);
            actualName = needList[0].Name;
            Assert.AreEqual(expectedName, actualName);

        }
        [TestMethod]
        //CREATOR: Dat Tran
        public void TestProjectIDReturnsNoItems()
        {
            const int projectID = 100008;

            const int expectedLength = 0;
            int actualLength = 9;
            List<NeedList> needList = new List<NeedList>();
            needList = _needListManager.GetNeedList(projectID);
            actualLength = needList.Count;
            Assert.AreEqual(expectedLength, actualLength);

        }
        [TestMethod]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: This test verifies that inserting a valid item into the need list returns true.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertItemToNeedListReturnsTrue()
        {
            var item = new NeedList
            {
                Name = "Embroidery Hoop",
                Quantity = 2,
                Price = 5.99m,
                Description = "Wooden embroidery hoop, 6 inches."
            };

            bool result = _needListManager.InsertItemToNeedList(4,
                item.Name, item.Quantity, item.Price, item.Description);

            Assert.IsTrue(result, "Expected the item to be inserted successfully.");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: This test verifies that an exception is thrown when inserting an item with an empty name.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertItemToNeedListEmptyNameThrowsException()
        {
            _needListManager.InsertItemToNeedList(5, "", 1, 4.99m, "Invalid item with empty name.");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: This test verifies that an exception is thrown when inserting an item with a negative quantity.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertItemToNeedListNegativeQuantityThrowsException()
        {
            _needListManager.InsertItemToNeedList(6, "Thread Spool", -1, 2.99m, "Invalid item with negative quantity.");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: This test verifies that an exception is thrown when inserting an item with a negative price.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertItemToNeedListNegativePriceThrowsException()
        {
            _needListManager.InsertItemToNeedList(7, "Fabric Scissors", 1, -10.00m, "Invalid item with negative price.");
        }

        [TestMethod]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: This test verifies that inserting an item with a duplicate name throws an exception.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertItemToNeedListDuplicateNameThrowsException()
        {
            _needListManager.InsertItemToNeedList(1, "Test", 1, 5.99m, "Duplicate item should not be allowed.");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: This test verifies that an exception is thrown when
        /// inserting an item with a description exceeding the maximum length.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertItemToNeedListLongDescriptionThrowsException()
        {
            string longDescription = new string('A', 251); // Assuming max length is 500
            _needListManager.InsertItemToNeedList(6, "Fabric Marker", 2, 3.99m, longDescription);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: This test verifies that an exception is thrown when
        /// inserting an item with a name exceeding the maximum length.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public void TestInsertItemToNeedListLongNameThrowsException()
        {
            string longName = new string('A', 51); // Assuming max length is 500
            _needListManager.InsertItemToNeedList(6, longName, 2, 3.99m, "long name");
        }
        [TestMethod]
        //CREATOR: Dat Tran
        public void UpdateItemNameReturnsUpdatedName()
        {
            const int projectID = 100000;
            const string oldName = "Test1";
            const string newName = "Test2";
            const int oldQuantity = 1;
            const int newQuantity = 2;
            const decimal oldPrice = 9;
            const decimal newPrice = 10;
            const string oldDescription = "test";
            const string newDescription = "testing";
            const int itemID = 100001;
            const bool expectedResult = true;
            bool actualResult = false;
            actualResult = _needListManager.UpdateNeedList(projectID, newName, newQuantity, newPrice, newDescription, oldName, oldQuantity, oldPrice, oldDescription, itemID);
            Assert.AreEqual(expectedResult, actualResult);

        }
        [TestMethod]
        //CREATOR: Dat Tran
        public void UpdateItemNameReturnsFailedUpdate()
        {
            const int projectID = 100009;
            const string oldName = "Test1";
            const string newName = "Test2";
            const int oldQuantity = 1;
            const int newQuantity = 2;
            const decimal oldPrice = 9;
            const decimal newPrice = 10;
            const string oldDescription = "test";
            const string newDescription = "testing";
            const bool expectedResult = false;
            const int itemID = 1;
            bool actualResult = true;
            actualResult = _needListManager.UpdateNeedList(projectID, newName, newQuantity, newPrice, newDescription, oldName, oldQuantity, oldPrice, oldDescription, itemID);
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        //CREATOR: Dat Tran
        public void RemoveItemReturnsSuccess()
        {
            const int itemId = 100003;
            const int expectedResult = 1;

            int actualResult = 0;
            actualResult = _needListManager.DeleteFromNeedList(itemId);
            Assert.AreEqual(expectedResult, actualResult);



        }
        [TestMethod]
        //CREATOR: Dat Tran
        public void RemoveItemReturnsZero()
        {
            const int itemId = 12345;
            const int expectedResult = 0;

            int actualResult = 100;
            actualResult = _needListManager.DeleteFromNeedList(itemId);
            Assert.AreEqual(expectedResult, actualResult);



        }
    }
}

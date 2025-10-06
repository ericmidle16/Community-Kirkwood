using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;

namespace LogicLayer
{
    public class NeedListManager : INeedListManager
    {
        private INeedListAccessor _needListAccessor;
        public NeedListManager()
        {
            _needListAccessor = new NeedListAccessor();
        }
        public NeedListManager(INeedListAccessor needListAccessor)
        {
            _needListAccessor = needListAccessor;
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-19
        /// Summary: This method manages the method to view the Need list in NeedListAccessor.
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public List<NeedList> GetNeedList(int projectID)
        {
            List<NeedList> needList = new List<NeedList>();
            try
            {
                needList = _needListAccessor.ViewNeedList(projectID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not available", ex);
            }
            return needList;
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-19
        /// Summary: This class contains the method to edit the Need list in NeedlistAccessor. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public bool UpdateNeedList(int projectID, string newName, int newQuantity, decimal newPrice, string newDescription, string oldName, int oldQuantity, decimal oldPrice, string oldDescription, int itemID)
        {
            bool result = false;
            try
            {
                result = _needListAccessor.EditNeedList(projectID, newName, newQuantity, newPrice, newDescription, oldName, oldQuantity, oldPrice, oldDescription, itemID);

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed.", ex);
            }
            return result;
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-04-04
        /// Summary: This method manages the method to view a single item in the Need.
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public NeedList SelectSingleItem(int itemID)
        {
            NeedList needList = new NeedList();
            try
            {
                needList = _needListAccessor.ViewSingleItem(itemID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not available", ex);
            }
            return needList;
        }
        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/17
        /// Summary: Inserts a new item into the need list.
        /// Returns true if the item was successfully added, otherwise false.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool InsertItemToNeedList(int projectID, string name, int quantity, decimal price, string description)
        {
            try
            {
                int rowsAffected = _needListAccessor.InsertItemToNeedList(projectID, name, quantity, price, description);
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Item insertion to NeedList failed.", ex);
            }
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: This method checks if there is an existing item name for the specified projectID.
        /// If an error occurs, a new exception is thrown.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool SelectExistingItemName(int projectID, string name)
        {
            try
            {
                NeedListAccessor accessor = new NeedListAccessor();
                return accessor.SelectExistingItemName(projectID, name);
            }
            catch (Exception ex)
            {
                throw new Exception("Database error when checking existing item name.", ex);
            }
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-03-05
        /// Summary: This method manages the method in the NeedlistAccessor class to remove an item from the need list.
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public int DeleteFromNeedList(int itemID)
        {
            int list = 0;
            try
            {
                list = _needListAccessor.RemoveItemFromList(itemID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Delete failed.", ex);
            }
            return list;
        }
    }
}

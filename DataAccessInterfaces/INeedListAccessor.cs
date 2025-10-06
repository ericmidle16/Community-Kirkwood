///<summary>
/// Creator: Dat Tran
/// Created: 2025-03-14
/// Summary: This is the class that implements the interface for the NeedListAccessor class.  
/// Last updated by:
/// Last updated: 
/// Changes:
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface INeedListAccessor
    {
        public List<NeedList> ViewNeedList(int projectID);
        public int RemoveItemFromList(int itemID);
        public bool EditNeedList(int projectID, string newName, int newQuantity, decimal newPrice, string newDescription, string oldName, int oldQuantity, decimal oldPrice, string oldDescription, int itemID);

        public int InsertItemToNeedList(int projectID, string name, int quantity, decimal price, string description);
        public bool SelectExistingItemName(int projectID, string name);
        public NeedList ViewSingleItem(int itemID);

    }
}

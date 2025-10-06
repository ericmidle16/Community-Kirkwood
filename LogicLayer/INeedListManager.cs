
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayer
{
    public interface INeedListManager
    {
        public List<NeedList> GetNeedList(int projectID);
        public bool UpdateNeedList(int projectID, string newName, int newQuantity, decimal newPrice, string newDescription, string oldName, int oldQuantity, decimal oldPrice, string oldDescription, int itemID);
        public bool InsertItemToNeedList(int projectID, string name, int quantity, decimal price, string description);
        public bool SelectExistingItemName(int projectID, string name);
        public int DeleteFromNeedList(int itemID);
    }
}

///<summary>
/// Creator: Dat Tran
/// Created: 2025-03-14
/// Summary: This class creates some fake data to be used with the unit tests in the NeedListManagerTests class. 
/// Last updated by:
/// Last updated: 
/// Changes:
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class NeedListAccessorFakes : INeedListAccessor
    {
        private List<NeedList> _needList;
        private List<NeedList> needList;
        private NeedList _lastFoundItemName;
        public NeedListAccessorFakes()
        {
            _needList = new List<NeedList>();
            _needList.Add(new NeedList
            {

                ProjectID = 100005,
                Name = "Hatchet",
                Quantity = 2,
                Price = 10,
                Description = "Cuts Trees.",
                IsObtained = false
            });

            _needList.Add(new NeedList
            {

                ProjectID = 100006,
                Name = "Rope",
                Quantity = 1,
                Price = 5,
                Description = "Keep things in place.",
                IsObtained = false
            });

            _needList.Add(new NeedList
            {

                ProjectID = 100007,
                Name = "Hoe",
                Quantity = 1,
                Price = 8,
                Description = "Create farmland.",
                IsObtained = false
            });
            _needList.Add(new NeedList
            {
                ItemID = 100001,
                ProjectID = 100005,
                Name = "Hatchet",
                Quantity = 2,
                Price = 10,
                Description = "Cuts Trees.",
                IsObtained = false
            });

            _needList.Add(new NeedList
            {
                ItemID = 100002,
                ProjectID = 100006,
                Name = "Rope",
                Quantity = 1,
                Price = 5,
                Description = "Keep things in place.",
                IsObtained = false
            });

            _needList.Add(new NeedList
            {
                ItemID = 100003,
                ProjectID = 100007,
                Name = "Hoe",
                Quantity = 1,
                Price = 8,
                Description = "Create farmland.",
                IsObtained = false
            });
            _needList.Add(new NeedList()
            {
                ItemID = 1,
                ProjectID = 100000,
                Name = "Test1",
                Quantity = 1,
                Price = 9,
                Description = "test"

            });
            _needList.Add(new NeedList()
            {
                ItemID = 2,
                ProjectID = 100000,
                Name = "Test2",
                Quantity = 1,
                Price = 10,
                Description = "test2"

            });
            _needList.Add(new NeedList()
            {
                ProjectID = 100000,
                Name = "Test3",
                Quantity = 1,
                Price = 11,
                Description = "tes2"

            });
            needList = new List<NeedList>()
            {
                new NeedList()
                {
                    ItemID = 1,
                    ProjectID = 1,
                    Name = "Test",
                    Quantity = 1,
                    Price = 1,
                    Description = "Test"
                },
                new NeedList()
                {
                    ItemID = 2,
                    ProjectID = 1,
                    Name = "Test2",
                    Quantity = 4,
                    Price = 1.23M,
                    Description = "Test"
                },
                new NeedList()
                {
                    ItemID= 3,
                    ProjectID= 1,
                    Name= "Test3",
                    Quantity = 5,
                    Price= 5.00M,
                    Description = ""
                }
            };
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-19
        /// Summary: This method is the same method in the NeedlistAccessor, but checks to make sure each projectID is in the need list.
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public List<NeedList> ViewNeedList(int projectID)
        {
            List<NeedList> needList = new List<NeedList>();
            foreach (NeedList n in _needList)
            {
                if (n.ProjectID == projectID)
                {
                    needList.Add(n);
                }
            }
            return needList;
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-03-06
        /// Summary: This method is the same method in the Needlist Accessor but checks if itemID is the same as ItemID in Needlist,
        /// and if they are, list increments by one. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public int RemoveItemFromList(int itemID)
        {
            int list = 0;
            foreach (var item in _needList)
            {
                if (item.ItemID == itemID)
                {
                    list += 1;
                }
            }
            return list;
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-26
        /// Summary: This is the same method in the Needlist accessor. This will compare all of the old values to the new values.
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public bool EditNeedList(int projectID, string newName, int newQuantity, decimal newPrice, string newDescription, string oldName, int oldQuantity, decimal oldPrice, string oldDescription, int itemID)
        {
            bool result = false;
            foreach (var item in _needList)
            {
                if (item.ProjectID == projectID && item.Name == oldName && item.Quantity == oldQuantity && item.Price == oldPrice && item.Description == oldDescription)
                {

                    item.Name = newName;
                    item.Quantity = newQuantity;
                    item.Price = newPrice;
                    item.Description = newDescription;
                    result = true;
                }
            }


            return result;
        }
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Retrieves the last found item name record.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public NeedList GetLastFoundItemName()
        {
            return _lastFoundItemName;
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Inserts a new item into the need list for a given project. 
        ///          Validates input values, ensuring the name, quantity, price, and description meet requirements.
        ///          Throws an exception if any validation fails or if an item with the same name already exists.
        /// </summary>

        public int InsertItemToNeedList(int projectID, string name, int quantity, decimal price, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ApplicationException("Please enter a name for your item.");
            }

            if (name.Length > 50)
            {
                throw new ApplicationException("The name of your item cannot exceed 50 characters.");
            }

            if (quantity < 1)
            {
                throw new ApplicationException("Quantity must be at least 1.");
            }

            if (price <= 0)
            {
                throw new ApplicationException("Price must be greater than zero.");
            }

            // Check if price has more than 2 decimal places
            if (price % 0.01m != 0)
            {
                throw new ApplicationException("Price cannot have more than two decimal places.");
            }

            if (description.Length > 250)
            {
                throw new ApplicationException("Description of the item cannot exceed 250 characters.");
            }

            if (SelectExistingItemName(projectID, name))
            {
                throw new ApplicationException("An item with this name already exists in the project.");
            }

            var newItem = new NeedList
            {
                ItemID = needList.Count + 1, // Simulating auto-increment ID
                ProjectID = projectID,
                Name = name,
                Quantity = quantity,
                Price = price,
                Description = description
            };

            needList.Add(newItem);

            return 1;
        }


        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Checks if an item  record exists for a given project 
        /// within the specified date range.
        /// </summary>
        public bool SelectExistingItemName(int projectID, string name)
        {
            return needList.Any(a => a.ProjectID == projectID && a.Name == name);
        }

        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-04-04
        /// Summary: This method is the same method in the NeedlistAccessor, but checks to make sure the ItemID is in the need list.
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public NeedList ViewSingleItem(int itemID)
        {
            NeedList needList = new NeedList();

            return needList;
        }

    }
}

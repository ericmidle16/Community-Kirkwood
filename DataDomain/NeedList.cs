/// <summary>
/// Creator: Skyann Heintz
/// Created: 2025/02/17
/// Summary: Represents an item needed for a project, including name, 
/// quantity, price, description, and whether it has been obtained.
/// Last Updated By:
/// Last Updated:
/// What Was Changed:
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class NeedList
    {
        public int ItemID { get; set; }
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsObtained { get; set; }
    }
}

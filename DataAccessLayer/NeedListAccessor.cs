///<summary>
/// Creator: Dat Tran
/// Created: 2025-03-14
/// Summary: This class contains the methods to add, edit, view and remove items from a Need list.
/// Last updated by:
/// Last updated: 
/// Changes:
/// </summary>
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class NeedListAccessor: INeedListAccessor
    {
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 02/19/2025
        /// Summary: This method connects to the database and uses the stored procedure to display the parameters.
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public List<NeedList> ViewNeedList(int projectID)
        {
            List<NeedList> needList = new List<NeedList>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_view_needlist", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectID", projectID);
            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    needList.Add(new NeedList
                    {

                        ProjectID = r.GetInt32(0),
                        Name = r.GetString(1),
                        Quantity = r.GetInt32(2),
                        Price = r.GetDecimal(3),
                        Description = r.GetString(4),
                        IsObtained = r.GetBoolean(5),
                        ItemID = r.GetInt32(6)

                    });



                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return needList;
        }

        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-03-04
        /// Summary: This class contains the method to implement the stored procedure to remove an item from the list. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>

        public int RemoveItemFromList(int itemID)
        {
            int list = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_remove_item_from_needlist", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ItemID", SqlDbType.Int);

            cmd.Parameters["@ItemID"].Value = itemID;

            try
            {
                conn.Open();
                list = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //throw;
                throw new ApplicationException("Database error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return list;


        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-26
        /// Summary: This is the method to update the need list based from the old values to the new values. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public bool EditNeedList(int projectID, string newName, int newQuantity, decimal newPrice, string newDescription, string oldName, int oldQuantity, decimal oldPrice, string oldDescription, int itemID)
        {
            bool list = false;

            //List<NeedList> editNeedList = new List<NeedList>(); Do I need this?
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_edit_list_of_needed_items", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@NewPrice", SqlDbType.Decimal);
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 250);


            cmd.Parameters.Add("@OldName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@OldPrice", SqlDbType.Decimal);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 250);


            cmd.Parameters["@ProjectID"].Value = projectID;
            cmd.Parameters["@ItemID"].Value = itemID;
            cmd.Parameters["@NewName"].Value = newName;
            cmd.Parameters["@NewQuantity"].Value = newQuantity;
            cmd.Parameters["@NewPrice"].Value = newPrice;
            cmd.Parameters["@NewDescription"].Value = newDescription;

            cmd.Parameters["@OldName"].Value = oldName;
            cmd.Parameters["@OldQuantity"].Value = oldQuantity;
            cmd.Parameters["@OldPrice"].Value = oldPrice;
            cmd.Parameters["@OldDescription"].Value = oldDescription;



            try
            {
                conn.Open();
                list = cmd.ExecuteNonQuery() == 1;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/17
        /// Summary: Executes the stored procedure sp_insert_item_to_needlist 
        /// to insert a new item record into the NeedList for a specific project.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int InsertItemToNeedList(int projectID, string name, int quantity, decimal price, string description)
        {
            int rowsAffected = 0;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_insert_item_to_needlist", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectID;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value = description;

                    try
                    {
                        conn.Open();
                        rowsAffected = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Insert Item to Need List failed", ex);
                    }
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Executes the stored procedure sp_select_item_from_needlist to check if the NeedList item
        /// already has that item name for the projectID. 
        /// It queries the database and returns a boolean indicating whether 
        /// item name exists for the specified projecID. If an error occurs during the database access, 
        /// it throws an application exception with a relevant error message.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool SelectExistingItemName(int projectID, string name)
        {
            bool exists = false;

            using (var conn = DBConnection.GetConnection())
            {
                using (var cmd = new SqlCommand("sp_select_item_from_needlist", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectID;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;

                    try
                    {
                        conn.Open();
                        exists = (int)cmd.ExecuteScalar() > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Data access error: Check NeedList failed", ex);
                    }
                }
            }
            return exists;
        }

        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-04-04
        /// Summary: This method calls the procedure in the database to view a single item in the need list.
        /// Last updated by: 
        /// Last updated: 
        /// Changes: 
        /// </summary>
        public NeedList ViewSingleItem(int itemID)
        {
            NeedList needList = new NeedList();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_view_single_item", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemID", itemID);
            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    needList = new NeedList()
                    {
                        ProjectID = r.GetInt32(0),
                        ItemID = itemID,
                        Name = r.GetString(1),
                        Quantity = r.GetInt32(2),
                        Price = r.GetDecimal(3),
                        Description = r.GetString(4),
                        IsObtained = r.GetBoolean(5)


                    };
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return needList;
        }
    }
}

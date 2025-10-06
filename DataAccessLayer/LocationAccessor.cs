/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-02
/// Summary:
///     Class that implements the ILocationAccessor Interface - used for
///     accessing Location data from the DB.
/// 
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/13
/// What was Changed: Added GetLocationList()
/// 
/// Last Updated By:  Jennifer Nicewanner
/// Last Updated:  2025-03-28
/// What Was Changed:  Updated SelectLocationByID method to include image and image mime type
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-24
/// What Was Changed:
///     Added GetAllActiveLocations, DeactivateLocationByLocationID
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LocationAccessor : ILocationAccessor
    {
        // Author: Jennifer Nicewanner
        public Location SelectLocationByID(int locationID)
        {
            Location location = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_location_by_locationID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = locationID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    int fieldIndex = 6;
                    long fieldWidth;
                    byte[] image = null;
                    if (!reader.IsDBNull(fieldIndex))
                    {
                        fieldWidth = reader.GetBytes(fieldIndex, 0, null, 0, Int32.MaxValue);
                        image = new byte[fieldWidth];
                        reader.GetBytes(fieldIndex, 0, image, 0, image.Length);
                    }

                    location = new Location()
                    {
                        //[Name],
                        //[Address],
                        //[City],
                        //[State],
                        //[Zip],
                        //[Country],
                        //[Image],
                        //[ImageMimeType],
                        //[Description]

                        LocationID = locationID,
                        Name = reader.GetString(0),
                        Address = reader.GetString(1),
                        City = reader.GetString(2),
                        State = reader.GetString(3),
                        Zip = reader.GetString(4),
                        Country = reader.GetString(5),
                        Image = image,
                        ImageMimeType = reader.IsDBNull(7) ? null : reader.GetString(8),
                        Description = reader.GetString(8)
                    };
                }
                else
                {
                    throw new ArgumentException("Location Record Not Found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No Record Found", ex);
            }
            return location;
        }

        // Author: Stan Anderson
        public List<Location> GetLocationList()
        {
            List<Location> locations = new List<Location>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_view_location_list", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    locations.Add(new Location()
                    {
                        LocationID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        City = reader.GetString(3),
                        State = reader.GetString(4),
                        Zip = reader.GetString(5),
                        Country = reader.GetString(6),
                        Description = reader.GetString(7)
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return locations;
        }

        // Author: Chase Hannen
        public int InsertLocation(Location newLocation)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);

            cmd.Parameters["@Name"].Value = newLocation.Name;
            cmd.Parameters["@Address"].Value = newLocation.Address;
            cmd.Parameters["@City"].Value = newLocation.City;
            cmd.Parameters["@State"].Value = newLocation.State == null ? "" : newLocation.State;
            cmd.Parameters["@Zip"].Value = newLocation.Zip == null ? "" : newLocation.Zip;
            cmd.Parameters["@Country"].Value = newLocation.Country == null ? "" : newLocation.Country;
            cmd.Parameters.AddWithValue("@Image", ((object)newLocation.Image) ?? SqlBinary.Null);
            cmd.Parameters.AddWithValue("@ImageMimeType", ((object)newLocation.ImageMimeType) ?? DBNull.Value);
            cmd.Parameters["@Description"].Value = newLocation.Description == null ? "" : newLocation.Description;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Creator:  Nikolas Bell
        /// Created:  2025/02/28
        /// Summary:  Updates an existing location record in the database.
        /// Last Updated By: Nikolas Bell
        /// Last Updated: 2025/02/28
        /// What was Changed: Added parameter descriptions.
        /// 
        /// Last Updated By: Chase Hannen
        /// Last Updated: 2025/04/24
        /// What was Changed: Added Image and ImageMimeType
        /// </summary>
        /// <param name="id">The id of the location to update.</param>
        /// <param name="updatedLocation">The updated Location object containing new values.</param>
        /// <returns>The number of rows affected by the update.</returns>
        /// <exception cref="ApplicationException">Thrown when a database error occurs.</exception>
        public int UpdateLocationByID(int id, Location updatedLocation)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_location_by_locationID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);

            cmd.Parameters["@LocationID"].Value = id;
            cmd.Parameters["@Name"].Value = updatedLocation.Name;
            cmd.Parameters["@Address"].Value = updatedLocation.Address;
            cmd.Parameters["@City"].Value = updatedLocation.City;
            cmd.Parameters["@State"].Value = updatedLocation.State == null ? "" : updatedLocation.State;
            cmd.Parameters["@Zip"].Value = updatedLocation.Zip == null ? "" : updatedLocation.Zip;
            cmd.Parameters["@Country"].Value = updatedLocation.Country == null ? "" : updatedLocation.Country;
            cmd.Parameters.AddWithValue("@Image", ((object)updatedLocation.Image) ?? SqlBinary.Null);
            cmd.Parameters.AddWithValue("@ImageMimeType", ((object)updatedLocation.ImageMimeType) ?? DBNull.Value);
            cmd.Parameters["@Description"].Value = updatedLocation.Description == null ? "" : updatedLocation.Description;

            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("A database error has occured.");
            }
        }

        // Author: Chase Hannen

        public List<Location> GetAllActiveLocations()
        {
            List<Location> locations = new List<Location>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_active_projects", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    locations.Add(new Location()
                    {
                        LocationID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        City = reader.GetString(3),
                        State = reader.GetString(4),
                        Zip = reader.GetString(5),
                        Country = reader.GetString(6),
                        Description = reader.GetString(7),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return locations;
        }

        // Author: Chase Hannen
        public int DeactivateLocationByLocationID(int locationID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_location_by_locationID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = locationID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-02
/// Summary:
///     Class that implements the ILocationManager Interface - used for
/// managing Location data from Location data fake objects &/or the DB.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/13
/// What was Changed: Added ViewLocationList()
/// Last Updated By: Jennifer Nicewanner
/// Last Updated:  2025-03-28
/// What Was Changed: Updated the author for the GetLocationByID method
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
///     Added AddNewLocation, using Microsoft.Data.SqlClient for
///     targeted SQL exceptions
///     
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-24
/// What Was Changed:
///     Added ViewAllActiveLocations and DeactivateLocationByLocationID
/// </summary>

using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LogicLayer
{
    public class LocationManager : ILocationManager
    {
        private ILocationAccessor _locationAccessor;

        // Constructor for Tests
        public LocationManager(ILocationAccessor locationAccessor)
        {
            _locationAccessor = locationAccessor;
        }

        // Constructor for DB
        public LocationManager()
        {
            _locationAccessor = new LocationAccessor();
        }

        // Author: Jennifer Nicewanner
        public Location GetLocationByID(int locationID)
        {
            Location location = null;

            try
            {
                location = _locationAccessor.SelectLocationByID(locationID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No Location Found", ex);
            }
            return location;
        }

        // Author: Stan Anderson
        public List<Location> ViewLocationList()
        {
            List<Location> locations = new List<Location>();
            try
            {
                locations = _locationAccessor.GetLocationList();
            }
            catch (Exception)
            {
                throw;
            }
            return locations;
        }

        // Author: Chase Hannen
        public bool AddNewLocation(Location newLocation)
        {
            bool isAdded = false;
            int rowsAffected = 0;

            try
            {
                rowsAffected = _locationAccessor.InsertLocation(newLocation);
                if (rowsAffected == 1)
                {
                    isAdded = true;
                }
                else
                {
                    throw new Exception("Location not added");
                }
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ApplicationException("This location already exists!", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Location insertion failed", ex);
            }

            return isAdded;
        }

        /// <summary>
        /// Creator:  Nikolas Bell
        /// Created:  2025/02/28
        /// Summary:  Updates an existing location record and returns whether the update was successful.
        /// Last Updated By: Nikolas Bell
        /// Last Updated: 2025/02/28
        /// What was Changed: Initial documentation added.
        /// </summary>
        /// <param name="locationID">The unique identifier of the location to update.</param>
        /// <param name="updatedLocation">The updated Location object containing new values.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public bool UpdateLocationByID(int locationID, Location updatedLocation)
        {
            return (1 == _locationAccessor.UpdateLocationByID(locationID, updatedLocation));
        }

        // Author: Chase Hannen
        public List<Location> ViewAllActiveLocations()
        {
            List<Location> locations = new List<Location>();
            try
            {
                locations = _locationAccessor.GetAllActiveLocations();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return locations;
        }

        // Author: Chase Hannen
        public bool DeactivateLocationByLocationID(int locationID)
        {
            try
            {
                return (1 == _locationAccessor.DeactivateLocationByLocationID(locationID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-02
/// Summary:
///     Class for fake Location objects that are used in testing.
/// 
/// Last Updated: 2025/02/13
/// What was Changed: Added GetLocationList()
/// 
/// Last Updated By:  Jennifer Nicewanner
/// Last Updated:  2025-03-28
/// What Was Changed:  Updated the author for the SelectLocationByID testing
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
/// 	Added InsertLocation
/// 	
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-24
/// What Was Changed:
/// 	Added GetAllActiveLocations and DeactivateLocationByLocationID
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class LocationAccessorFake : ILocationAccessor
    {
        private List<Location> _locations;
        private List<Location> _locationsToDelete;

        public LocationAccessorFake()
        {
            _locations = new List<Location>();

            _locations.Add(new Location()
            {
                LocationID = 1,
                Name = "Strickland Propane",
                Address = "135 Los Gatos Road",
                City = "Arlen",
                State = "Texas",
                Zip = "76001",
                Country = "USA",
                Description = "The only place to get propane and propane accessories."
            });
            _locations.Add(new Location()
            {
                LocationID = 2,
                Name = "Tom Landry Middle School",
                Address = "123 Tom Landry Way",
                City = "Arlen",
                State = "Texas",
                Zip = "76001",
                Country = "USA",
                Description = "The best middle school to learn Espanol at."
            });
            _locations.Add(new Location()
            {
                LocationID = 3,
                Name = "Bad Location",
                Address = "123 Deactivated St",
                City = "Arlen",
                State = "Texas",
                Zip = "76001",
                Country = "USA",
                Description = "Do not return this location.",
                Active = false
            });
            _locations.Add(new Location()
            {
                LocationID = 4,
                Name = "Good Location",
                Address = "123 My Way",
                City = "Arlen",
                State = "Texas",
                Zip = "76001",
                Country = "USA",
                Description = "Return this location, please.",
                Active = true
            });

            _locationsToDelete = new List<Location>();

            _locationsToDelete.Add(new Location()
            {
                LocationID = 55,
                Name = "Goodbye",
                Address = "987 Wiley Blvd",
                City = "Arlen",
                State = "Texas",
                Zip = "76001",
                Country = "USA",
                Description = "Delete me",
                Active = true
            });
            _locationsToDelete.Add(new Location()
            {
                LocationID = 44,
                Name = "Location!",
                Address = "2468 Potato St",
                City = "Arlen",
                State = "Texas",
                Zip = "76001",
                Country = "USA",
                Description = "Yup, you're staying deactivated.",
                Active = false
            });
        }

        // Author: Jennifer Nicewanner
        public Location SelectLocationByID(int locationID)
        {
            foreach (Location l in _locations)
            {
                if (l.LocationID == locationID)
                {
                    return l;
                }
            }
            throw new ArgumentException("Location Record Not Found.");
        }

        // Author: Stan Anderson
        public List<Location> GetLocationList()
        {
            return _locations;
        }

        // Author: Chase Hannen
        public int InsertLocation(Location newLocation)
        {
            int isAdded = 0;
            int oldCount = _locations.Count;

            Location location = _locations.Find(l => l.Name == newLocation.Name);

            if (location == null)
            {
                _locations.Add(new Location()
                {
                    Name = newLocation.Name,
                    Address = newLocation.Address,
                    City = newLocation.City,
                    State = newLocation.State,
                    Zip = newLocation.Zip,
                    Country = newLocation.Country,
                    Description = newLocation.Description
                });
            }

            if (_locations.Count == oldCount + 1)
            {
                isAdded = 1;
            }
            else
            {
                throw new Exception("Location insertion failed.");
            }

            return isAdded;
        }

        //Author: Nik Bell
        public int UpdateLocationByID(int locationID, Location updatedLocation)
        {
            int index = _locations.FindIndex(x => x.LocationID == locationID);
            if (index != -1)
            {
                _locations[index] = updatedLocation;
                return 1;
            }
            else { return 0; }
        }

        // Author: Chase Hannen
        public List<Location> GetAllActiveLocations()
        {
            List<Location> locationsToReturn = new List<Location>();
            foreach (Location loc in _locations)
            {
                if (!loc.Active == false || !loc.Active == null)
                {
                    locationsToReturn.Add(loc);
                }
            }
            return locationsToReturn;
        }

        // Author: Chase Hannen
        public int DeactivateLocationByLocationID(int locationID)
        {
            int numDelete = 0;
            foreach (Location loc in _locationsToDelete)
            {
                if (loc.LocationID == locationID)
                {
                    loc.Active = false;
                    numDelete++;
                }
            }
            return numDelete;
        }
    }
}
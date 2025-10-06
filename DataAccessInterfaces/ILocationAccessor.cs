/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-02
/// Summary: 
///     Interface that holds method declarations for accessing Location data.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/13
/// What was Changed: Added GetLocationList()
/// Last Updated By:  Jennifer Nicewanner
/// Last Updated:  2025-03-28
/// What Was Changed:  Updated SelectLocationByID with image and image mime type fields
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
///     Added InsertLocation
///     
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-24
/// What Was Changed:
///     Added GetAllActiveLocations, DeactivateLocationByLocationID
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface ILocationAccessor
    {
        Location SelectLocationByID(int locationID);
        public List<Location> GetLocationList();
        int InsertLocation(Location newLocation);
        int UpdateLocationByID(int id, Location updatedLocation);
        List<Location> GetAllActiveLocations();
        int DeactivateLocationByLocationID(int locationID);
    }
}
/// <summary>
/// Kate Rich
/// Created: 2025-02-02
/// 
/// Interface that holds method declarations for managing Location data.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/13
/// What was Changed: Added ViewLocationList()
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-04
/// What Was Changed:
///     Added AddNewLocation
///     
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-04-24
/// What Was Changed:
///     Added ViewAllActiveLocations and DeactivateLocationByLocationID
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ILocationManager
    {
        Location GetLocationByID(int locationID);
        public List<Location> ViewLocationList();
        public bool AddNewLocation(Location newLocation);
        public bool UpdateLocationByID(int locationID, Location updatedLocation);
        List<Location> ViewAllActiveLocations();
        bool DeactivateLocationByLocationID(int locationID);
    }
}
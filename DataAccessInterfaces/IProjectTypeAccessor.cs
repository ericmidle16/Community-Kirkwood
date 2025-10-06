/// <summary>
/// Christivie Mauwa
/// Created: 2025/02/06
/// 
/// Actual summary of the class if needed, example is for DTO
/// Class for the creation of User Objects with set data fields
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Christivie Mauwa
    /// Created: 2025/02/21
    /// 
    /// Actual summary of the class if needed.
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    /// <param name="sender"></param>
    public interface IProjectTypeAccessor
    {
        List<ProjectType> SelectAllprojectTypes();
        ProjectType SelectProjectTypeID(string id);
    }
}

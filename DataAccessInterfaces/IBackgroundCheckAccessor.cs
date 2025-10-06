/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-10
/// Summary:
///     Interface that holds method declarations for accessing BackgroundCheck data.
/// 
/// Updated By: Kate Rich
/// Updated: 2025-02-20
/// What Was Changed:
///     Added the SelectBackgroundChecksByProjectID method.
///     
/// Updated By: Kate Rich
/// Updated: 2025-02-26
/// What Was Changed:
///     Added the UpdateBackgroundCheck method.
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-03-27
/// What Was Changed:
///     Added the SelectBackgroundCheckByID method.
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IBackgroundCheckAccessor
    {
        int InsertBackgroundCheck(BackgroundCheck backgroundCheck);
        List<BackgroundCheckVM> SelectBackgroundChecksByProjectID(int projectID);
        int UpdateBackgroundCheck(BackgroundCheck oldBackgroundCheck, BackgroundCheck newBackgroundCheck);
        BackgroundCheckVM SelectBackgroundCheckByID(int backgroundCheckID);
    }
}
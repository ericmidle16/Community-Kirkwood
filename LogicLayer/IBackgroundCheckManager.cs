/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-10
/// Summary:
///     Interface that holds method declarations for managing BackgroundCheck data.
/// 
/// Updated By: Kate Rich
/// Updated: 2025-02-20 
/// What Was Changed:
///     Added the GetBackgroundChecksByProjectID method.
///     
/// Updated By: Kate Rich
/// Updated: 2025-02-26
/// What Was Changed:
///     Added the EditBackgroundCheck method.
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-03-27
/// What Was Changed:
///     Added the GetBackgroundCheckByID method.
///     
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IBackgroundCheckManager
    {
        bool AddBackgroundCheck(BackgroundCheck backgroundCheck);
        List<BackgroundCheckVM> GetBackgroundChecksByProjectID(int projectID);
        bool EditBackgroundCheck(BackgroundCheck oldBackgroundCheck, BackgroundCheck newBackgroundCheck);
        BackgroundCheckVM GetBackgroundCheckByID(int backgroundCheckID);
    }
}
/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-02
/// Summary:
///     Class that implements the IBackgroundCheckManager Interface - used for
///     managing BackgroundCheck data from BackgroundCheck Data Fake Objects &/or the DB.
/// 
/// Updated By: Kate Rich
/// Updated: 2025-02-20 
/// What Was Changed:
///     Added & implemented the GetBackgroundChecksByProjectID method.
///     
/// Updated By: Kate Rich
/// Updated: 2025-02-26
/// What Was Changed:
///     Added & implemented the EditBackgroundCheck method.
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-03-27
/// What Was Changed:
///     Added & implemented the GetBackgroundCheckByID method.
/// </summary>

using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class BackgroundCheckManager : IBackgroundCheckManager
    {
        private IBackgroundCheckAccessor _backgroundCheckAccessor;

        // Constructor for Tests
        public BackgroundCheckManager(IBackgroundCheckAccessor backgroundCheckAccessor)
        {
            _backgroundCheckAccessor = backgroundCheckAccessor;
        }

        // Constructor for DB
        public BackgroundCheckManager()
        {
            _backgroundCheckAccessor = new BackgroundCheckAccessor();
        }

        // Author: Kate Rich
        public bool AddBackgroundCheck(BackgroundCheck backgroundCheck)
        {
            bool added = false;

            int rowsAffected = 0;

            try
            {
                rowsAffected = _backgroundCheckAccessor.InsertBackgroundCheck(backgroundCheck);
                if(rowsAffected == 1)
                {
                    added = true;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Background Check Creation Failed...", ex);
            }

            return added;
        }

        // Author: Kate Rich
        public List<BackgroundCheckVM> GetBackgroundChecksByProjectID(int projectID)
        {
            List<BackgroundCheckVM> backgroundChecks = new List<BackgroundCheckVM>();

            try
            {
                backgroundChecks = _backgroundCheckAccessor.SelectBackgroundChecksByProjectID(projectID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("No Background Checks Found", ex);
            }

            return backgroundChecks;
        }

        // Author: Kate Rich
        public bool EditBackgroundCheck(BackgroundCheck oldBackgroundCheck, BackgroundCheck newBackgroundCheck)
        {
            bool isUpdated = false;
            int rowsAffected = 0;

            try
            {
                rowsAffected = _backgroundCheckAccessor.UpdateBackgroundCheck(oldBackgroundCheck, newBackgroundCheck);
                if(rowsAffected == 1)
                {
                    isUpdated = true;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("No Background Checks Found", ex);
            }

            return isUpdated;
        }

        public BackgroundCheckVM GetBackgroundCheckByID(int backgroundCheckID)
        {
            BackgroundCheckVM backgroundCheck = null;

            try
            {
                backgroundCheck = _backgroundCheckAccessor.SelectBackgroundCheckByID(backgroundCheckID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Background Check Not Found", ex);
            }

            return backgroundCheck;
        }
    }
}
/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-10
/// Summary:
///     Class for fake BackgroundCheck objects that are used in testing.
/// 
/// Updated By: Kate Rich
/// Updated: 2025-02-20 
/// What Was Changed:
///     Created a new List<BackgroundCheckVM> to hold data for viewing background checks.
///     Added & implemented the SelectBackgroundChecksByProjectID method.
///     
/// Updated By: Kate Rich
/// Updated: 2025-02-26
/// What Was Changed:
///     Added & implemented the UpdateBackgroundCheck method.
///     
/// Last Updated By: Kate Rich
/// Last Updated: 2025-03-27
/// What Was Changed:
///     Added & implemented the SelectBackgroundCheckByID method.
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
    public class BackgroundCheckAccessorFake : IBackgroundCheckAccessor
    {
        private List<BackgroundCheck> _backgroundChecks;
        private List<BackgroundCheckVM> _backgroundCheckVMs;

        public BackgroundCheckAccessorFake()
        {
            _backgroundChecks = new List<BackgroundCheck>();

            _backgroundChecks.Add(new BackgroundCheck()
            {
                BackgroundCheckID = 1,
                Investigator = 10,
                UserID = 7,
                ProjectID = 1,
                Status = "In Progress",
                Description = ""
            });
            _backgroundChecks.Add(new BackgroundCheck()
            {
                BackgroundCheckID = 2,
                Investigator = 11,
                UserID = 6,
                ProjectID = 2,
                Status = "In Progress",
                Description = ""
            });

            _backgroundCheckVMs = new List<BackgroundCheckVM>();

            _backgroundCheckVMs.Add(new BackgroundCheckVM()
            {
                BackgroundCheckID = 3,
                Investigator = 1,
                InvestigatorGivenName = "Hank",
                InvestigatorFamilyName = "Hill",
                UserID = 2,
                VolunteerGivenName = "Peggy",
                VolunteerFamilyName = "Hill",
                ProjectID = 1,
                ProjectName = "Propane Delivery to TLMS",
                Status = "Passed",
                Description = "Peggy's background check showed that she is hazmat certified - therefore meaning she can help transport propane."
            });
            _backgroundCheckVMs.Add(new BackgroundCheckVM()
            {
                BackgroundCheckID = 4,
                Investigator = 2,
                InvestigatorGivenName = "Peggy",
                InvestigatorFamilyName = "Hill",
                UserID = 5,
                VolunteerGivenName = "Luanne",
                VolunteerFamilyName = "Platter",
                ProjectName = "Make Strickland More Accessible",
                Status = "In Progress",
                Description = "Even though Luanne is my niece, I need to make sure that she is legally allowed to help with this project. It's required for all volunteers, & she is no exception."
            });
            _backgroundCheckVMs.Add(new BackgroundCheckVM()
            {
                BackgroundCheckID = 5,
                Investigator = 1,
                InvestigatorGivenName = "Hank",
                InvestigatorFamilyName = "Hill",
                UserID = 3,
                VolunteerGivenName = "Joseph",
                VolunteerFamilyName = "Gribble",
                ProjectID = 1,
                ProjectName = "Propane Delivery to TLMS",
                Status = "Failed",
                Description = "Joseph is a child in middle school. He is not allowed to help transport propane. It's too dangerous."
            });
        }

        // Author: Kate Rich
        public int InsertBackgroundCheck(BackgroundCheck backgroundCheck)
        {
            int rowsAffected = 0;
            int count = _backgroundChecks.Count;

            _backgroundChecks.Add(new BackgroundCheck()
            {
                BackgroundCheckID = backgroundCheck.BackgroundCheckID,
                Investigator = backgroundCheck.Investigator,
                UserID = backgroundCheck.UserID,
                ProjectID = backgroundCheck.ProjectID,
                Status = backgroundCheck.Status,
                Description = backgroundCheck.Description
            });

            if(_backgroundChecks.Count - count == 1)
            {
                rowsAffected++;
            }

            return rowsAffected;
        }

        // Author: Kate Rich
        public BackgroundCheckVM SelectBackgroundCheckByID(int backgroundCheckID)
        {
            foreach(BackgroundCheckVM bgc in _backgroundCheckVMs)
            {
                if(bgc.BackgroundCheckID == backgroundCheckID)
                {
                    return bgc;
                }
            }
            throw new ArgumentException("Project Record Not Found.");
        }

        // Author: Kate Rich
        public List<BackgroundCheckVM> SelectBackgroundChecksByProjectID(int projectID)
        {
            List<BackgroundCheckVM> backgroundChecks = new List<BackgroundCheckVM>();

            foreach(BackgroundCheckVM bgc in _backgroundCheckVMs)
            {
                if(bgc.ProjectID == projectID)
                {
                    backgroundChecks.Add(bgc);
                }
            }

            return backgroundChecks;
        }

        // Author: Kate Rich
        public int UpdateBackgroundCheck(BackgroundCheck oldBackgroundCheck, BackgroundCheck newBackgroundCheck)
        {
            int rowsAffected = 0;

            foreach(BackgroundCheck bgc in _backgroundChecks)
            {
                if(bgc.BackgroundCheckID == oldBackgroundCheck.BackgroundCheckID
                    && bgc.Investigator == oldBackgroundCheck.Investigator
                    && bgc.UserID == oldBackgroundCheck.UserID
                    && bgc.ProjectID == oldBackgroundCheck.ProjectID)
                {
                    oldBackgroundCheck.Status = newBackgroundCheck.Status;
                    oldBackgroundCheck.Description = newBackgroundCheck.Description;
                    rowsAffected++;
                    break;
                }
            }
            return rowsAffected;
        }
    }
}
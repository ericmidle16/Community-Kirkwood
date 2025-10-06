/// <summary>
/// Creator: Skyann Heintz
/// Created: 2025/02/11
/// Summary: Implements the IAvailabilityAccessor interface with 
/// methods for inserting availability records, 
/// checking if availability exists for a user,
/// and storing a list of fake availability records for tests.
/// Last Updated By: Skyann Heintz
/// Last Updated: 2025-03-12
/// What Was Changed: Added in SelectAvailabilityByUser. 
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-03-30
/// What Was Changed: Added in _availabilityVMs and _volunteers for
///                     SelectAvailabilityVMByProjectID functionality
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
    public class AvailabilityAccessorFake : IAvailabilityAccessor
    {
        private List<Availability> _availabilityRecords;
        private Availability _lastFoundAvailability;
        private List<UserAvailability> _availability;
        private List<AvailabilityVM> _availabilityVMs;
        private List<VolunteerStatus> _volunteers;

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/11
        /// Summary: Constructor for the AvailabilityAccessorFake
        /// class. Initializes a list of fake availability records to 
        /// simulate the behavior of the availability data access 
        /// layer.
        /// Last Updated By: Skyann Heintz
        /// Last Updated: 2025-03-12
        /// What Was Changed: Added AvailabilityID into the _availabilityRecords
        /// to ensure that the SelectAvailabilityByUser could work.
        /// Last Updated By: Dat Tran
        /// Last Updated: 2025-03-14
        /// What was changed: Deleted UnassignedVolunteersFakes and move the fake data into Sky's constructor. 
        /// 
        /// Last Updated By: Chase Hannen
        /// Last Updated: 2025-04-11
        /// What Was Changed: Added _availabilityVM, implemented SelectAvailabilityVMByProjectID
        /// </summary>
        public AvailabilityAccessorFake()
        {
            _availabilityRecords = new List<Availability>()
            {
                new Availability()
                {
                    AvailabilityID = 1,
                    UserID = 1,
                    IsAvailable = true,
                    RepeatWeekly = false,
                    StartDate = new DateTime(2025, 2, 10, 9, 0, 0),
                    EndDate = new DateTime(2025, 2, 10, 17, 0, 0)
                },
                new Availability()
                {
                    AvailabilityID = 2,
                    UserID = 2,
                    IsAvailable = true,
                    RepeatWeekly = true,
                    StartDate = new DateTime(2025, 2, 11, 8, 0, 0),
                    EndDate = new DateTime(2025, 2, 11, 16, 0, 0)
                },
                new Availability()
                {
                    AvailabilityID = 3,
                    UserID = 2,
                    IsAvailable = false,
                    RepeatWeekly = false,
                    StartDate = new DateTime(2025, 2, 11, 7, 0, 0),
                    EndDate = new DateTime(2025, 2, 11, 5, 0, 0)
                }
            };
            _availability = new List<UserAvailability>();

            _availability.Add(new UserAvailability
            {
                UserID = 1000002,
                GivenName = "Donald",
                FamilyName = "Trump",
                City = "New York City",
                State = "New York",
                IsAvailable = false
            });
            _availability.Add(new UserAvailability
            {
                UserID = 1000003,
                GivenName = "Barack",
                FamilyName = "Obama",
                City = "Chicago",
                State = "Illinois",
                IsAvailable = true
            });
            _availability.Add(new UserAvailability
            {
                UserID = 1000004,
                GivenName = "Joe",
                FamilyName = "Biden",
                City = "Somewhere",
                State = "Pennsylvania",
                IsAvailable = true
            });

            // Author: Chase Hannen
            _availabilityVMs = new List<AvailabilityVM>()
            {
                new AvailabilityVM()
                {
                    AvailabilityID = 1,
                    UserID = 1,
                    IsAvailable = true,
                    RepeatWeekly = true,
                    StartDate = new DateTime(2025, 5, 1, 10, 0, 0),
                    EndDate = new DateTime(2025, 5, 1, 18, 0, 0)
                },
                new AvailabilityVM()
                {
                    AvailabilityID = 2,
                    UserID = 2,
                    IsAvailable = true,
                    RepeatWeekly = false,
                    StartDate = new DateTime(2025, 5, 1, 10, 0, 0),
                    EndDate = new DateTime(2025, 5, 1, 18, 0, 0)
                },
                new AvailabilityVM()
                {
                    AvailabilityID = 3,
                    UserID = 3,
                    IsAvailable = true,
                    RepeatWeekly = false,
                    StartDate = new DateTime(2025, 5, 1, 10, 0, 0),
                    EndDate = new DateTime(2025, 5, 1, 18, 0, 0)
                },
                new AvailabilityVM()
                {
                    AvailabilityID = 4,
                    UserID = 4,
                    IsAvailable = true,
                    RepeatWeekly = false,
                    StartDate = new DateTime(2024, 5, 1, 10, 0, 0),
                    EndDate = new DateTime(2024, 5, 1, 18, 0, 0)
                },
                new AvailabilityVM()
                {
                    AvailabilityID = 5,
                    UserID = 2,
                    IsAvailable = true,
                    RepeatWeekly = false,
                    StartDate = new DateTime(2025, 5, 2, 12, 0, 0),
                    EndDate = new DateTime(2025, 5, 2, 20, 0, 0)
                }
            };

            // Author: Chase Hannen
            _volunteers = new List<VolunteerStatus>()
            {
                new VolunteerStatus()
                {
                    UserID = 1,
                    ProjectID = 1,
                    Approved = true
                },
                new VolunteerStatus()
                {
                    UserID = 1,
                    ProjectID = 2,
                    Approved = true
                },
                new VolunteerStatus()
                {
                    UserID = 2,
                    ProjectID = 1,
                    Approved = true
                },
                new VolunteerStatus()
                {
                    UserID = 3,
                    ProjectID = 1,
                    Approved = true
                },
                new VolunteerStatus()
                {
                    UserID = 3,
                    ProjectID = 2,
                    Approved = true
                },
                new VolunteerStatus()
                {
                    UserID = 4,
                    ProjectID = 1,
                    Approved = true
                }
            };
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/11
        /// Summary: Inserts a new availability record into the fake 
        /// list after validating input parameters like userID and date range.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public int InsertAvailability(int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate)
        {
            if (userID <= 0)
            {
                throw new ApplicationException("Invalid user ID.");
            }

            if (startDate >= endDate)
            {
                throw new ApplicationException("Start date must be before end date.");
            }

            if (startDate < DateTime.Now)
            {
                throw new ApplicationException("Start date cannot be in the past.");
            }
            if (SelectExistingAvailability(userID, startDate, endDate))
            {
                throw new ApplicationException("An overlapping availability record already exists for the given user during this time period.");
            }
            if (string.IsNullOrWhiteSpace(userID.ToString()))
            {
                throw new ApplicationException("User ID cannot be empty or whitespace.");
            }
            var newAvailability = new Availability
            {
                UserID = userID,
                IsAvailable = isAvailable,
                RepeatWeekly = repeatWeekly,
                StartDate = startDate,
                EndDate = endDate
            };

            _availabilityRecords.Add(newAvailability);

            return 1;
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/11
        /// Summary: Retrieves the last found availability record.
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public Availability GetLastFoundAvailability()
        {
            return _lastFoundAvailability;
        }
        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/11
        /// Summary: Checks if an availability record exists for a given user 
        /// within the specified date range.
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public bool SelectExistingAvailability(int userID, DateTime startDate, DateTime endDate)
        {
            return _availabilityRecords.Any(a => a.UserID == userID &&
                                                a.StartDate < endDate &&
                                                a.EndDate > startDate);
        }

        /// <summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/24
        /// Summary: Retrieves a list of availability records for a specific user.
        /// /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public List<Availability> SelectAvailabilityByUser(int userID)
        {
            var availability = _availabilityRecords.Where(a => a.UserID == userID).ToList();

            if (availability.Count == 0)
            {
                throw new ApplicationException("User not found.");
            }

            return availability;
        }
        ///<summary>
        /// Creator: Dat Tran
        /// Created: 2025-02-11
        /// Summary: This method checks to see if an availability status in the method in the accessor matches the one below, 
        /// then add to the list. 
        /// Last updated by:
        /// Last updated: 
        /// Changes:
        /// </summary>
        public List<UserAvailability> SelectAvailableUsers(bool isAvailable, int eventID)
        {
            List<UserAvailability> availability = new List<UserAvailability>();
            foreach (UserAvailability a in _availability)
            {
                if (a.IsAvailable == isAvailable)
                {
                    availability.Add(a);
                }
            }
            return availability;
        }

        /// <summary>
        /// Creator: Chase Hannen
        /// Created: 2025/04/11
        /// Summary: Retrieves a view model of availability records for
        ///             all users on a project
        /// </summary>
        public List<AvailabilityVM> SelectAvailabilityVMByProjectID(int projectID)
        {
            List<AvailabilityVM> availabilities = new List<AvailabilityVM>();
            foreach (VolunteerStatus volunteerStatus in _volunteers)
            {
                if (volunteerStatus.ProjectID == projectID)
                {
                    foreach (AvailabilityVM availabilityVM in _availabilityVMs)
                    {
                        if (volunteerStatus.UserID == availabilityVM.UserID)
                        {
                            availabilities.Add(availabilityVM);
                        }
                    }
                }
            }
            return availabilities;
        }

        public int UpdateAvailabilityByID(int availabilityID, int userID, bool isAvailable, bool repeatWeekly, DateTime startDate, DateTime endDate)
        {
            if (availabilityID <= 0)
            {
                throw new ApplicationException("Invalid availability ID.");
            }

            if (userID <= 0)
            {
                throw new ApplicationException("Invalid user ID.");
            }

            if (startDate >= endDate)
            {
                throw new ApplicationException("Start date must be before end date.");
            }

            if (startDate < DateTime.Now)
            {
                throw new ApplicationException("Start date cannot be in the past.");
            }

            var availabilityToUpdate = _availabilityRecords.FirstOrDefault(a => a.AvailabilityID == availabilityID);

            if (availabilityToUpdate == null)
            {
                return 0;
            }

            if (availabilityToUpdate.UserID != userID)
            {
                throw new ApplicationException("Cannot update availability records belonging to another user.");
            }

            availabilityToUpdate.IsAvailable = isAvailable;
            availabilityToUpdate.RepeatWeekly = repeatWeekly;
            availabilityToUpdate.StartDate = startDate;
            availabilityToUpdate.EndDate = endDate;

            return 1;
        }

        public int DeleteAvailabilityByAvailabilityID(int availabilityID)
        {
            if (availabilityID <= 0)
            {
                throw new ApplicationException("Invalid availability ID.");
            }

            var availabilityToDelete = _availabilityRecords.FirstOrDefault(a => a.AvailabilityID == availabilityID);

            if (availabilityToDelete == null)
            {
                return 0;
            }

            _availabilityRecords.Remove(availabilityToDelete);
            return 1;
        }
    }
}

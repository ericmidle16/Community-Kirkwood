/// <summary>
/// Creator: Kate Rich
/// Created: 2025-03-03
/// Summary:
///     Class for fake Donation objects that are used in testing.
/// 
/// Updated By : Christivie Mauwa
/// Updated : 2025-03-28
/// What was changed : Added SelectAllDonation, InsertDonation, SelectDonationByUserID,SelectDonationByDonationID.
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025-04-01
/// What Was Changed:
///     Added more data fakes to the _donations list.
///     Added implementation for the SelectToViewDonationsByProjectID method. 
/// 
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-05-02
/// What Was Changed: Updated SelectMonetaryProjectDonationSummariesByUserID
///     to allow null amount value
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class DonationAccessorFake : IDonationAccessor
    {
        // Author : Christivie
        private List<Donation> donations = new List<Donation>();
        private List<DonationVM> donationVMs;
        List<Donation> _donations;

        public DonationAccessorFake()
        {
            _donations = new List<Donation>();

            _donations.Add(new Donation()
            {
                DonationID = 1,
                DonationType = "Monetary",
                UserID = 3,
                ProjectID = 5,
                Amount = 100.00m,
                DonationDate = new DateTime(2025, 3, 1),
                Description = ""
            });
            _donations.Add(new Donation()
            {
                DonationID = 2,
                DonationType = "Monetary",
                UserID = 3,
                ProjectID = 4,
                Amount = 10.00m,
                DonationDate = new DateTime(2025, 1, 31),
                Description = ""
            });
            _donations.Add(new Donation()
            {
                DonationID = 3,
                DonationType = "Material",
                UserID = 3,
                ProjectID = 5,
                Amount = 0m,
                DonationDate = new DateTime(2025, 2, 27),
                Description = ""
            });
            _donations.Add(new Donation()
            {
                DonationID = 4,
                DonationType = "Monetary",
                UserID = 2,
                ProjectID = 5,
                Amount = 50.00m,
                DonationDate = new DateTime(2025, 2, 12),
                Description = ""
            });
            _donations.Add(new Donation()
            {
                DonationID = 5,
                DonationType = "Monetary",
                UserID = 3,
                ProjectID = 5,
                Amount = 125.00m,
                DonationDate = new DateTime(2025, 2, 3),
                Description = ""
            });
            // Akoi Data Fakes
            _donations.Add(new Donation()
            {
                DonationID = 6,
                DonationType = "Construction",
                UserID = 1,
                ProjectID = 3,
                Amount = 1,
                DonationDate = new DateTime(2025, 5, 16),
                Description = "For construction materials"
            });
            _donations.Add(new Donation()
            {
                DonationID = 7,
                DonationType = "Construction",
                UserID = 2,
                ProjectID = 3,
                Amount = 10,
                DonationDate = new DateTime(2025, 8, 20),
                Description = "For construction materials to buy"
            });
            _donations.Add(new Donation()
            {
                DonationID = 8,
                DonationType = "Construction1",
                UserID = 4,
                ProjectID = 5,
                Amount = 20,
                DonationDate = new DateTime(2025, 10, 26),
                Description = "For construction materials to purchase"
            });

            donationVMs = new List<DonationVM>
            {
               new DonationVM
               {
                   DonationID = 100003,
                   DonationType ="cash",
                   UserID = 100002,
                   ProjectID = 100004,
                   Amount = 4m,
                   DonationDate = DateTime.Now,
                   Description = "Cash Donation",
                   UserName = "Nick",
                   Name = "Test",
                   LocationID = 100004

               },
               new DonationVM
               {
                   DonationID = 100005,
                   DonationType ="Credit",
                   UserID = 100007,
                   ProjectID = 100002,
                   Amount = 20m,
                   DonationDate = DateTime.Now,
                   Description = "Donation",
                   UserName = "Bob",
                   Name ="Test2",
                   LocationID = 100007

               }
            };
        }

        // Author: Kate Rich
        public List<DonationSummaryVM> SelectMonetaryProjectDonationSummariesByUserID(int userID)
        {
            // Create a new list - pulling the Project Name & Location, Sum of Donations, & Most Recent Donation Date from the original _donations list.
            List<DonationSummaryVM> _donationSummaries =
                 _donations.Where(d => d.UserID == userID && d.DonationType == "Monetary")
                    .GroupBy(d => d.ProjectID)
                    .Select(m => new DonationSummaryVM
                    {
                        ProjectID = m.Key, // Get the ProjectID for the current group - this allows sorting with OrderBy to work.
                        ProjectName = "Test Project", // Generic value - using Sum & Max date for testing.
                        MonetaryDonationTotal = m.Sum(d => d.Amount) ?? 0m,
                        LastDonationDate = m.Max(d => d.DonationDate),
                        ProjectLocation = "Test Location - Test City, Test State" // Generic value - using Sum & Max date for testing.
                    })
                    .OrderBy(v => v.ProjectID) // Lowest ProjectID will be first in the new list.
                    .ToList();

            return _donationSummaries;
        }

        //Author: Christivie
        public int InsertDonation(Donation donation)
        {
            int count = donations.Count;

            donations.Add(donation);

            return donations.Count - count;
        }

        //Auhtor: Christivie
        public List<DonationVM> SelectAllDonation()
        {
            return donationVMs;
        }

        //Author: Chrisitivie
        public DonationVM SelectDonationByDonationID(int donationID)
        {
            var donation = donationVMs.FirstOrDefault(d => d.DonationID == donationID);
            return donation;
        }

        // Author : Christivie
        public List<DonationVM> SelectDonationByUserID(int userID)
        {
            var donation = donationVMs.Where(s => s.UserID == userID).ToList();
            return donation;
        }

        /// <summary>
        /// Creator: Akoi Kollie
        /// Created: 2025/03/19
        /// Summary: Create list of donation by it project ID and returns donations lists. 
        /// 
        /// Last Updated By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public List<Donation> SelectToViewDonationsByProjectID(int projectid)
        {
            List<Donation> donate = new List<Donation>();
            foreach (Donation donation in _donations)
            {
                if (donation.ProjectID == projectid)
                {
                    donate.Add(donation);
                }
            }
            return donate;
        }
    }
}
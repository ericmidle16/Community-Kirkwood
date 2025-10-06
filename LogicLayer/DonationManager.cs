/// <summary>
/// Creator: Kate Rich
/// Created: 2025-02-02
/// Summary:
///     Class that implements the IDonationManager Interface - used for
///     managing BackgroundCheck data from Donation Data Fake Objects &/or the DB.
/// 
/// Updated by : Christivie Mauwa
/// Updated : 2025-03-28
/// What was changed : Added AddDonation, GetAllDonation, RetrieveDonationByDonationID,RetrieveDonationByUserId
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025-04-01
/// What Was Changed:
///     Added the SelectToViewDonations method.
///     
/// Last Updated By: Chase Hannen
/// Last Updated: 2025-05-02
/// What Was Changed: 
///     Changed 'ArgumentException' to 'ApplicationException' in SelectToViewDonations
/// </summary>

using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class DonationManager : IDonationManager
    {
        private IDonationAccessor _donationAccessor;

        // Constructor for Tests
        public DonationManager(IDonationAccessor donationAccessor)
        {
            _donationAccessor = donationAccessor;
        }

        // Constructor for DB
        public DonationManager()
        {
            _donationAccessor = new DonationAccessor();
        }

        // Author: Kate Rich
        public List<DonationSummaryVM> GetMonetaryProjectDonationSummariesByUserID(int userID)
        {
            List<DonationSummaryVM> _donationSummaries = new List<DonationSummaryVM>();

            try
            {
                _donationSummaries = _donationAccessor.SelectMonetaryProjectDonationSummariesByUserID(userID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("No Donations Found", ex);
            }

            return _donationSummaries;
        }

        // Author : Christivie
        public List<DonationVM> GetAllDonation()
        {
            try
            {
                return _donationAccessor.SelectAllDonation();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        // Author : Christivie
        public bool AddDonation(Donation donation)
        {
            try
            {
                int rowsAffected = _donationAccessor.InsertDonation(donation);
                return rowsAffected == 1;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Insertion failed", ex);
            }
        }

        // Author : Christivie
        public List<DonationVM> RetrieveDonationByUserId(int userID)
        {
            try
            {
                return _donationAccessor.SelectDonationByUserID(userID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Donation not found for this user.", ex);
            }
        }

        // Author : Christivie
        public DonationVM RetrieveDonationByDonationID(int donationID)
        {
            try
            {
                return _donationAccessor.SelectDonationByDonationID(donationID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Donation not found.", ex);
            }
        }

        //Author: Akoi Kollie
        public List<Donation> SelectToViewDonations(int projectid)
        {
            List<Donation> donations = null;
            try
            {
                donations = _donationAccessor.SelectToViewDonationsByProjectID(projectid);
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return donations;
        }
    }
}
/// <summary>
/// Creator: Kate Rich
/// Created: 2025-03-03
/// Summary:
///     Interface that holds method declarations for managing Donation data.
/// 
/// Updated By : Christivie Mauwa
/// Updated : 2025-03-28
/// What was changed : Added GetAllDonation, AddDonation, RetrieveDonationByUserId, RetrieveDonationByDonationID
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025-04-01
/// What Was Changed:
///     Added the SelectToViewDonations method.
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IDonationManager
    {
        List<DonationSummaryVM> GetMonetaryProjectDonationSummariesByUserID(int userID);
        List<DonationVM> GetAllDonation();
        bool AddDonation(Donation donation);
        List<DonationVM> RetrieveDonationByUserId(int userID);
        DonationVM RetrieveDonationByDonationID(int donationID);
        List<Donation> SelectToViewDonations(int projectid);
    }
}
/// <summary>
/// Creator: Kate Rich
/// Created: 2025-03-03
/// Summary:
///     Interface that holds method declarations for accessing Donation data.
/// 
/// Updated by : Christivie Mauwa
/// Updated : 2025-03-28
/// What was changed : Added SelectAllDonation, InsertDonation, SelectDonationByUserID,SelectDonationByDonationID
/// 
/// Last Updated By: Akoi Kollie
/// Last Updated: 2025-04-01
/// What Was Changed:
///     Added the SelectToViewDonationsByProjectID method.
/// </summary>

using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IDonationAccessor
    {
        List<DonationSummaryVM> SelectMonetaryProjectDonationSummariesByUserID(int userID);
        List<DonationVM> SelectAllDonation();
        List<DonationVM> SelectDonationByUserID(int userID);
        int InsertDonation(Donation donation);
        DonationVM SelectDonationByDonationID(int donationID);
        List<Donation> SelectToViewDonationsByProjectID(int projectid);
    }
}
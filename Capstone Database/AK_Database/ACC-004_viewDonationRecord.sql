print '' print '** Starting ACC-004_viewDonationRecord.sql'
GO

use [communityDb]
go

/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Stored Procedure for to view donation information by projectID
	</summary>
*/
print '' print '*** Creating procedure sp_select_to_view_donation_record_by_useriID'

Go
CREATE PROCEDURE [dbo].[sp_select_to_view_donation_record_by_projectiID]
(
    @ProjectID    [int]   
)
AS
    BEGIN
        SELECT [DonationID], [DonationType], [UserID], [ProjectID], [Amount], [DonationDate],[Description]
        FROM [Donation]
        WHERE [Donation].[ProjectID] = @ProjectID  
       
    END
GO
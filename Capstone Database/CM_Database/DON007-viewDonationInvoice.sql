/* 
	<summary>
	Creator:            Christivie Mauwa
	Created:            02/05/2025
	Summary:            Creation for Project table
	Last Updated By:    Christivie Mauwa
	Last Updated:       02/28/2025
	What Was Changed:   Initial Creation  
	</summary> 
*/
print '' print '*** using database communityDb'
GO
USE [communityDb]
GO


/*
    <summary>
    Creator: Creators Name
    Created: 03/11/2025
    Summary: This select data into the Donation table by ID
    Last Updated By: Christivie Mauwa
    Last Updated: 03/11/2025
    What Was Changed: Adding more rows to fill out datagrids
    </summary>
*/
/* Stored Procedures for DonationType */
print '' print '*** creating SP_Select_Donation_by_Donation_ID ***'
GO
CREATE PROCEDURE [dbo].[SP_Select_Donation_by_Donation_ID]
(
	@DonationID [int]
)
AS
	BEGIN
		SELECT 	[DonationID],[Donation].[UserID],[Donation].[ProjectID],[Project].[Name],[Amount], [DonationDate],[Donation].[Description]
		FROM 	[Donation] 
		JOIN [Project] on [Donation].[ProjectID]
			=[Project].[ProjectID]
		WHERE [Donation].[DonationID] = @DonationID
	END
GO
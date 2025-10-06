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
    Created: 1/30/2025
    Summary: This select data into the Donation table by ID
    Last Updated By: Christivie Mauwa
    Last Updated: 1/31/2025
    What Was Changed: Adding more rows to fill out datagrids
	
	Last Updated By: Chase Hannen
    Last Updated: 4/25/2025
    What Was Changed: Changed [User] to [Donation] ln 46
    </summary>
*/
/* Stored Procedures for DonationType */
print '' print '*** creating SP_Select_Donation_by_User_ID ***'
GO
CREATE PROCEDURE [dbo].[SP_Select_Donation_by_User_ID]
(
	@UserID [int]
)
AS
	BEGIN
		SELECT 	[DonationID],[Donation].[DonationType],[Donation].[UserID],[Donation].[ProjectID],[Amount],
		[DonationDate],[Donation].[Description],[User].[GivenName]+ ''+ [User].[FamilyName] As UserName,
		[Project].[Name]
		FROM 	[Donation] 
		JOIN [DonationType] on [Donation].[DonationType]
			=[DonationType].[DonationType]
		JOIN [User] ON [Donation].[UserID]
			= [User].[UserID]
		JOIN [Project] on [Donation].[ProjectID]
			= [Project].[ProjectID]
		WHERE [Donation].[UserID] = @UserID
	END
GO
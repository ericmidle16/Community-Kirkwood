/* 
	<summary>
	Creator:            Christivie Mauwa
	Created:            02/05/2025
	Summary:            Creation for Project table
	
	Last Updated By:    Chase Hannen
	Last Updated:       05/01/2025
	What Was Changed:   Created sp_update_available_funds
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
    Summary: This inserts data into the Donation table
    Last Updated By: Christivie Mauwa
    Last Updated: 1/31/2025
    What Was Changed: Adding more rows to fill out datagrids
    </summary>
*/
print'' print '*** CREATING sp_Insert_Donation '
GO
CREATE PROCEDURE sp_Insert_Donation
(	
	@DonationType [nvarchar] (50),
	@UserID 		[int],
	@ProjectID [int],
	@Amount [decimal](10,2),
	@DonationDate [date],
	@Description 	[nvarchar] 	(500)
)
AS
BEGIN
	INSERT INTO [dbo].[Donation]
		([DonationType],[UserID],[ProjectID],
		 [Amount],[DonationDate],[Description])
	VALUES
		(@DonationType,@UserID,@ProjectID,@Amount,
	@DonationDate ,@Description )
END
GO

/*
    <summary>
    Creator: Creators Name
    Created: 1/30/2025
    Summary: This select data into the Donation table
    Last Updated By: Christivie Mauwa
    Last Updated: 1/31/2025
    What Was Changed: Adding more rows to fill out datagrids
    </summary>
*/
print '' print '*** creating SP_Select_All_Donation ***'
GO
CREATE PROCEDURE [dbo].[SP_Select_All_Donation]
AS
	BEGIN
		SELECT [DonationID],[Donation].[DonationType],[Donation].[UserID],[Donation].[ProjectID],[Amount],
		[DonationDate],[Donation].[Description],[User].[GivenName]+ ''+ [User].[FamilyName] As UserName,
		[Project].[Name]
		FROM 	[Donation] 
		JOIN [DonationType] on [Donation].[DonationType]
			=[DonationType].[DonationType]
		JOIN [User] ON [User].[UserID]
			= [User].[UserID]
		JOIN [Project] on [Donation].[ProjectID]
			= [Project].[ProjectID]
	END
GO


/*
    <summary>
    Creator: Creators Name
    Created: 1/30/2025
    Summary: This select data into the DonationType table
    Last Updated By: Christivie Mauwa
    Last Updated: 1/31/2025
    What Was Changed: Adding more rows to fill out datagrids
    </summary>
*/
print '' print '*** creating SP_Select_All_DonationType ***'
GO
CREATE PROCEDURE [dbo].[SP_Select_All_DonationType]
AS
	BEGIN
		SELECT 	[DonationType],[Description]
		FROM 	[DonationType]
		ORDER BY [DonationType]
	END
GO

/*
    <summary>
    Creator: Chase Hannen
    Created: 05/01/2025
    Summary: Updates the AvailableFunds field of a Project
    </summary>
*/
print '' print '*** creating sp_update_available_funds ***'
GO
CREATE PROCEDURE [dbo].[sp_update_available_funds] (
	@ProjectID	[int],
	@Amount		[decimal](15,2),
	@IsDonation	[bit]
)
AS
	BEGIN
		IF @IsDonation = 1 
			UPDATE	[Project]
			SET 	[AvailableFunds] = [AvailableFunds] + @Amount
			WHERE	[ProjectID] = @ProjectID
			
			--RETURN @@ROWCOUNT;
		ELSE 
			UPDATE	[Project]
			SET		[AvailableFunds] = [AvailableFunds] - @Amount
			WHERE	[ProjectID] = @ProjectID
			
		RETURN @@ROWCOUNT
	END
GO
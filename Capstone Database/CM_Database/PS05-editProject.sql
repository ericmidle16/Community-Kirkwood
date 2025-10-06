
/* 
	<summary>
	Creator:            Christivie Mauwa
	Created:            02/05/2025
	Summary:            Creation for Project Type table
	Last Updated By:    Updaters Name
	Last Updated:       01/31/2025
	What Was Changed:   ???  
	</summary> 
*/
print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
    <summary>
    Creator: Christivie Mauwa
    Created: 2/21/2025
    Summary: This is the stored procedure for obtaining the 
						list of current project
    Last Updated By: Updaters Name
    Last Updated: 1/31/2025
    What Was Changed: Adding more rows to fill out datagrids
    </summary>
*/
/* Stored Procedures for Project */
print '' print '*** creating SP_Select_All_Projects_For_VM ***'
GO
CREATE PROCEDURE [dbo].[SP_Select_All_Projects_For_VM]
AS
	BEGIN
		SELECT 	[ProjectID],[Project].[Name],[Project].[ProjectTypeID], [Project].[LocationID], [Project].[UserID], [StartDate], 
		[Status], [Project].[Description],[AcceptsDonations],[PayPalAccount], [AvailableFunds],
		[Location].[Name],[Location].[Address],[Location].[City],[Location].[State],[Location].[Zip ],[User].[GivenName],[User].[FamilyName]
		FROM 	[Project] 
		JOIN [ProjectType] on [PROJECT].[ProjectTypeID]=[ProjectType].[ProjectTypeID]
		JOIN [Location] on [PROJECT].[LocationID] = [Location].[LocationID]
		JOIN [User] on [PROJECT].[UserID] = [User].[UserID]
		ORDER BY [LocationID]
	END
GO
/*
    <summary>
    Creator: Christivie Mauwa
    Created: 2/21/2025
    Summary: This is the stored procedure for obtaining the 
						list of current project IDs
    Last Updated By: Updaters Name
    Last Updated: 1/31/2025
    What Was Changed: Adding more rows to fill out datagrids
    </summary>
*/
print '' print '*** creating SP_Select_Project_by_ID ***'
GO
CREATE PROCEDURE [dbo].[SP_Select_Project_by_ID]
(
	@ProjectID [int]
)
AS
	BEGIN
		SELECT 	[ProjectID],[Project].[Name],[Project].[ProjectTypeID], [Project].[LocationID], [Project].[UserID], [StartDate], 
		[Status], [Project].[Description],[AcceptsDonations],[PayPalAccount], [AvailableFunds],
		[Location].[Name],[Location].[Address],[Location].[City],[Location].[State],[Location].[Zip ],[User].[GivenName],[User].[FamilyName]
		FROM 	[Project] 
		JOIN [ProjectType] on [PROJECT].[ProjectTypeID]=[ProjectType].[ProjectTypeID]
		JOIN [Location] on [PROJECT].[LocationID] = [Location].[LocationID]
		JOIN [User] on [PROJECT].[UserID] = [User].[UserID]
		WHERE[ProjectID] = @ProjectID;
	END
GO
/*
    <summary>
    Creator: Christivie Mauwa
    Created: 2/21/2025
    Summary: This is the stored procedure for updating each project
    Last Updated By: Updaters Name
    Last Updated: 1/31/2025
    What Was Changed: Adding more rows to fill out datagrids
    </summary>
*/
print '' print '*** creating SP_Update_Project ***'
GO
CREATE PROCEDURE [dbo].[SP_Update_Project]
(
	@NewLocationID [int] ,
	@NewStatus 		[nvarchar] (25),
	@NewDescription	[nvarchar] 	(250),
	@NewAcceptsDonations [bit],
	@NewPayPalAccount [nvarchar] (50),
	@OldProjectID	[int]
)
AS
	BEGIN
	/*  Update the project table*/
		UPDATE 	[Project]
		SET 
			[LocationID] = @NewLocationID,
			[Status] = @NewStatus, 
			[Description] = @NewDescription,
			[AcceptsDonations] = @NewAcceptsDonations,
			[PayPalAccount] = @NewPayPalAccount
		WHERE [ProjectID] = @OldProjectID
	END
GO

/*
    <summary>
    Creator: Christivie Mauwa
    Created: 2/21/2025
    Summary: This is the stored procedure for obtaining the 
						list of current project by status
    Last Updated By: Updaters Name
    Last Updated: 1/31/2025
    What Was Changed: Adding more rows to fill out datagrids
    </summary>
*/
print '' print '*** creating SP_Select_ProjectType_ID ***'
GO
CREATE PROCEDURE [dbo].[SP_Select_ProjectType_ID]
(
	@ProjectTypeID [nvarchar](50)
)
AS
	BEGIN
		SELECT 	[ProjectTypeID],[Description]
		FROM 	[ProjectType]
		WHERE ProjectTypeID = @ProjectTypeID
	END
GO


/*
    <summary>
    Creator: Christivie Mauwa
    Created: 2/21/2025
    Summary: This is the stored procedure for obtaining the 
						list of current project by status
    Last Updated By: Updaters Name
    Last Updated: 1/31/2025
    What Was Changed: Adding more rows to fill out datagrids
    </summary>
*/
print '' print '*** creating SP_Select_All_ProjectTypes ***'
GO
CREATE PROCEDURE [dbo].[SP_Select_All_ProjectTypes]
AS
	BEGIN
		SELECT 	[ProjectTypeID],[Description]
		FROM [projectType]
	END
GO
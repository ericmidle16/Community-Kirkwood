print '' print '*** Starting DON05_viewDonatedProjectsDesktop.sql ***'
GO


print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-27
		Summary:	Creation Script for the DonationType table.
	</summary>
*/
print '' print '*** Create DonationType Table ***'
GO
CREATE TABLE [dbo].[DonationType](
	[DonationType]			[nvarchar](50)		NOT NULL,
	[Description]			[nvarchar](500)		NULL		DEFAULT NULL,
	CONSTRAINT [pk_DonationType] PRIMARY KEY([DonationType] ASC)
)
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-27
		Summary:	Creation Script for the Donation table.
	</summary>
*/
print '' print '*** Create Donation Table ***'
GO
CREATE TABLE [dbo].[Donation](
	[DonationID]			[int]		IDENTITY(100000, 1)		NOT NULL,
	[DonationType]			[nvarchar](50)		NOT NULL,
	[UserID]				[int]				NULL,
	[ProjectID]				[int]				NOT NULL,
	[Amount]				[decimal](10, 2)	NULL,
	[DonationDate]			[datetime]			NOT NULL	DEFAULT GETDATE(),
	[Description]			[nvarchar](500)		NULL,
	CONSTRAINT [pk_DonationID] PRIMARY KEY([DonationID] ASC),
	CONSTRAINT [fk_Donation_DonationType_DonationType] FOREIGN KEY([DonationType])
		REFERENCES [DonationType]([DonationType])
			ON UPDATE CASCADE,
	CONSTRAINT [fk_Donation_User_UserID] FOREIGN KEY([UserID])
		REFERENCES [User]([UserID]),
	CONSTRAINT [fk_Donation_Project_ProjectID] FOREIGN KEY([ProjectID])
		REFERENCES [Project]([ProjectID])
)
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-03-02
		Summary:	Stored Procedure for viewing projects a User has donated to.
		
		Changed By:	Kate Rich
		Date Changed:	2025-03-03
		What Changed:
			I changed the stored procedure name from 'sp_select_sum_of_project_donations_by_userID'
			to 'sp_select_project_monetary_donation_summaries_by_userID'.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_project_monetary_donation_summaries_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_monetary_donation_summaries_by_userID]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT [Donation].[ProjectID], [Project].[Name], SUM([Amount]), MAX([DonationDate]), [Location].[Name] + ' - ' + [Location].[City] + ', ' + [Location].[State] AS 'Project Location'
		FROM [Donation]
			JOIN [Project] ON [Donation].[ProjectID] = [Project].[ProjectID]
				JOIN [Location] ON [Project].[LocationID] = [Location].[LocationID]
		WHERE [Donation].[UserID] = @UserID
			AND [Donation].[DonationType] = 'Monetary'
		GROUP BY [Donation].[ProjectID], [Project].[Name], [Location].[State], [Location].[City], [Location].[Name]
		ORDER BY [Donation].[ProjectID]
	END
GO
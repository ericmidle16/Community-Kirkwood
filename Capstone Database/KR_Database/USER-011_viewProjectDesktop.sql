print '' print '*** Starting USER-011_viewProjectDesktop.sql ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-03
		Summary:	Creation Script for the User table.
		
		Updated By: Brodie Pasker
		Last Updated: 2025-03-11
    	What Was Changed: Added ImageMimeType as a field, also added Biography
		
		Last Updated By: Jennifer Nicewanner
		Last Updated:   2025-3-13
		What was changed:  Added the Biography & ImageMimeType fields.
		
		Last Updated By: Chase Hannen
		Last Updated: 2025-04-24
		What Was Changed: Added the Active field to the Location table.
	</summary>
*/
print '' print '*** Create User Table ***'
GO
CREATE TABLE [dbo].[User](
	[UserID]				[int] 		IDENTITY(100000, 1)		NOT NULL,
	[GivenName]				[nvarchar](50)		NOT NULL,
	[FamilyName]			[nvarchar](50)		NOT NULL,
	[PhoneNumber]			[nvarchar](13)		NOT NULL,
	[Email]					[nvarchar](250)		NOT NULL,
	[City]					[nvarchar](50)		NULL		DEFAULT NULL,
	[State]					[nvarchar](50)		NULL		DEFAULT NULL,
	[Image]					[varbinary](MAX)	NULL		DEFAULT NULL,
	[ImageMimeType] 		[nvarchar](50) 		NULL, -- The file type to decode the image from	
	[PasswordHash]			[nvarchar](100)		NOT NULL	DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e', -- Default password = newuser
	[ReactivationDate]		[datetime]			NULL		DEFAULT NULL,
	[Suspended]				[bit]				NOT NULL	DEFAULT 0,
	[ReadOnly]				[bit]				NOT NULL	DEFAULT 0,
	[Active]				[bit]				NOT NULL	DEFAULT 1,
	[RestrictionDetails]	[nvarchar](250)		NOT NULL	DEFAULT '',
	[Biography]				[nvarchar](250)		NOT NULL	DEFAULT '',
	CONSTRAINT [pk_UserID] PRIMARY KEY([UserID] ASC),
	CONSTRAINT [ak_Email] UNIQUE([Email] ASC)
)
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-03
		Summary:	Creation Script for the ProjectType table.
	</summary>
*/
print '' print '*** Create ProjectType Table ***'
GO
CREATE TABLE [dbo].[ProjectType](
	[ProjectTypeID]			[nvarchar](50)		NOT NULL,
	[Description]			[nvarchar](250)		NOT NULL	DEFAULT '',
	CONSTRAINT [pk_ProjectTypeID] PRIMARY KEY([ProjectTypeID] ASC)
)
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-03
		Summary:	Creation Script for the Location table.
		
		Last Updated By: Jennifer Nicewanner
		Last Updated: 2025-03-28
		What Was Changed: Added the Image & ImageMimeType fields to the Location table.
		
		Last Updated By: Chase Hannen
		Last Updated: 2025-04-24
		What Was Changed: Added the Active field to the Location table.
	</summary>
*/
print '' print '*** Create Location Table ***'
GO
CREATE TABLE [dbo].[Location](
	[LocationID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[Name]				[nvarchar](50)		NOT NULL,
	[Address]			[nvarchar](100)		NOT NULL,
	[City]				[nvarchar](100)		NOT NULL,
	[State]				[nvarchar](20)		NOT NULL	DEFAULT '',
	[Zip]				[nvarchar](10)		NOT NULL	DEFAULT '',
	[Country]			[nvarchar](100)		NOT NULL	DEFAULT 'USA',
	[Image]				[varbinary] (MAX)				NULL,
	[ImageMimeType]		[nvarchar](50)					NULL,
	[Description]		[nvarchar](250)		NOT NULL	DEFAULT '',
	[Active]			[bit]				NULL		DEFAULT 1,
	CONSTRAINT [pk_LocationID] PRIMARY KEY([LocationID] ASC)
)
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-03
		Summary:	Creation Script for the Project table.
	</summary>
*/
print '' print '*** Create Project Table ***'
GO
CREATE TABLE [dbo].[Project](
	[ProjectID]			[int]		IDENTITY(100000, 1)		NOT NULL,
	[Name]				[nvarchar](50)		NOT NULL,
	[ProjectTypeID]		[nvarchar](50)		NOT NULL,
	[LocationID]		[int]				NOT NULL,
	[UserID]			[int]				NOT NULL,
	[StartDate]			[datetime]			NOT NULL	DEFAULT GETDATE(),
	[Status]			[nvarchar](25)		NOT NULL	DEFAULT 'In Progress',
	[Description]		[nvarchar](250)		NOT NULL	DEFAULT '',
	[AcceptsDonations]	[bit]				NOT NULL	DEFAULT 0,
	[PayPalAccount]		[nvarchar](50)		NULL,
	[AvailableFunds]	[decimal](15, 2)	NOT NULL	DEFAULT 0,
	CONSTRAINT [pk_ProjectID] PRIMARY KEY([ProjectID] ASC),
	CONSTRAINT [fk_Project_ProjectType_ProjectTypeID] FOREIGN KEY([ProjectTypeID])
		REFERENCES [ProjectType]([ProjectTypeID])
			ON UPDATE CASCADE,
	CONSTRAINT [fk_Project_Location_LocationID] FOREIGN KEY([LocationID])
		REFERENCES [Location]([LocationID]),
	CONSTRAINT [fk_Project_User_UserID] FOREIGN KEY([UserID])
		REFERENCES [User](UserID)
)
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-03
		Summary:	Stored Procedure for selecting a Project by its ProjectID.
		
		Last Updated By: Stan Anderson
		Last Updated: 2025/04/09
		Added AcceptsDonations
		
		Last Updated By: Chase Hannen
		Last Updated: 2025/05/01
		Added PayPalAccount and AvailableFunds
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_projectInformation_by_projectID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_projectInformation_by_projectID]
	(
		@ProjectID		[int]
	)
AS
	BEGIN
		SELECT [ProjectID], [Project].[Name], [ProjectTypeID], [Project].[LocationID], [Location].[Name], [Location].[City], [Location].[State], [UserID], [StartDate], [Status], [Project].[Description],[Project].[AcceptsDonations], [Project].[PayPalAccount], [Project].[AvailableFunds]
		FROM [Project]
			JOIN [Location] ON [Project].[LocationID] = [Location].[LocationID]
		WHERE [ProjectID] = @ProjectID
	END
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-03
		Summary:	Stored Procedure for selecting a Project by its ProjectID.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_project_by_projectID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_by_projectID]
	(
		@ProjectID		[int]
	)
AS
	BEGIN
		SELECT [ProjectID], [Name], [ProjectTypeID], [LocationID], [UserID], [StartDate], [Status], [Description]
		FROM [Project]
		WHERE [ProjectID] = @ProjectID
	END
GO
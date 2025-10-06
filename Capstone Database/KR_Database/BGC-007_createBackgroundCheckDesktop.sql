print '' print '*** Starting BGC-007_createBackgroundCheckDesktop.sql ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO


/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-10
		Summary:	Creation Script for the BackgroundCheck table.
	</summary>
*/
print '' print '*** Create BackgroundCheck Table ***'
GO
CREATE TABLE [dbo].[BackgroundCheck](
	[BackgroundCheckID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[Investigator]			[int]				NOT NULL,
	[UserID]				[int]				NOT NULL,
	[ProjectID]				[int]				NOT NULL,
	[Status]				[nvarchar](25)		NOT NULL		DEFAULT 'In Progress',
	[Description]			[nvarchar](250)		NOT NULL		DEFAULT '',
	CONSTRAINT [pk_BackgroundCheckID] PRIMARY KEY([BackgroundCheckID] ASC),
	CONSTRAINT [fk_BackgroundCheck_User_Investigator] FOREIGN KEY([Investigator])
		REFERENCES [User]([UserID]),
	CONSTRAINT [fk_BackgroundCheck_User_UserID] FOREIGN KEY([UserID])
		REFERENCES [User]([UserID]),
	CONSTRAINT [fk_BackgroundCheck_Project_ProjectID] FOREIGN KEY(ProjectID)
		REFERENCES [Project]([ProjectID])
)
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-11
		Summary:	Stored Procedure for selecting a User's information by their UserID.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_userInformation_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_userInformation_by_userID]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT [UserID], [GivenName], [FamilyName], [PhoneNumber], [Email], [City], [State]
		FROM [User]
		WHERE [UserID] = @UserID
	END
GO


/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-10
		Summary:	Stored Procedure for inserting a BackgroundCheck record into the DB.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_insert_backgroundCheck ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_backgroundCheck]
	(
		@Investigator	[int],
		@UserID			[int],
		@ProjectID		[int],
		@Status			[nvarchar](25),
		@Description	[nvarchar](250)
	)
AS
	BEGIN
		INSERT INTO [BackgroundCheck]
			([Investigator], [UserID], [ProjectID], [Status], [Description])
		VALUES
			(@Investigator, @UserID, @ProjectID, @Status, @Description)
		RETURN @@ROWCOUNT
	END
GO
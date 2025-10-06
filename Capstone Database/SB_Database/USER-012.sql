print '' print '*** starting USER-012.sql file'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO



/* 
	<summary>
	Creator:            Syler Bushlack
	Created:            2025/02/13
	Summary:            Creation for VolunteerStatus table
	Last Updated By: 	Syler Bushlack
	Last Updated: 		2025/02/13
	What was Changed: 	Initial creation	 
	</summary> 
*/
print "" print "*** creating VolunteerStatus table"
GO
CREATE TABLE [dbo].[VolunteerStatus] (
	[UserID]				[int]					              	NOT NULL,
	[ProjectID]	            [int]				                    NOT NULL,
	[Approved]				bit										NULL,
	
	CONSTRAINT [pk_userid_projectid] PRIMARY KEY([UserID], [ProjectID] ASC),
	CONSTRAINT [fk_volunteerstatus_userid] FOREIGN KEY([UserID])
		REFERENCES [User]([UserID]),
	CONSTRAINT [fk_volunteerstatus_projectd] FOREIGN KEY([ProjectID])
		REFERENCES [Project]([ProjectID])
)
GO


/* 
	<summary>
	Creator:            Syler Bushlack
	Created:            2025/02/04
	Summary:            Creation of astored procedure that returns all projects
	Last Updated By:    Syler Bushlack
	Last Updated:       2025/02/04
	What Was Changed:   Initial creation
	
	Last Updated By:    Kate Rich
	Last Updated:       2025-03-10
	What Was Changed:   Removed EndDate from sp_select_all_projects.
	</summary> 
*/
print "" print "*** creating procedure sp_select_all_projects"
GO
CREATE PROCEDURE [dbo].[sp_select_all_projects]
AS
	BEGIN
		SELECT 			[ProjectID], [Project].[Name], [ProjectTypeID], [Project].[LocationID],
						[Project].[UserID], [StartDate],
						[Status], [Project].[Description], [AcceptsDonations], [PayPalAccount], [AvailableFunds], [GivenName], [Location].[Name]
		FROM			[Project] join [User] on [Project].[UserID] = [User].[UserID] join [Location] on [Project].[LocationID] = [Location].[LocationID]
	END
GO

/* 
	<summary>
	Creator:            Syler Bushlack
	Created:            2025/02/13
	Summary:            Creation of a stored procedure that returns all not approved project join requests of a specific project
	Last Updated By:    Syler Bushlack
	Last Updated:       2025/02/13
	What Was Changed:   Initial creation
	</summary> 
*/
print "" print "*** creating procedure sp_select_volunteerstatus_by_projectid"
GO
CREATE PROCEDURE [dbo].[sp_select_volunteerstatus_by_projectid] 
(
	@ProjectID			[int]
)
AS
	BEGIN
		SELECT 			[VolunteerStatus].[UserID], [VolunteerStatus].[ProjectID], [Approved], [GivenName]
		FROM			[VolunteerStatus] join [Project] on [VolunteerStatus].[ProjectID] = [Project].[ProjectID] join [User] on [VolunteerStatus].[UserID] = [User].[UserID]
		WHERE			[Project].[ProjectID] = @ProjectID AND [Approved] IS NULL
	END
GO

/* 
	<summary>
	Creator:            Syler Bushlack
	Created:            2025/02/13
	Summary:            Creation of a stored procedure that returns all not approved project join requests of a specific project
	Last Updated By:    Syler Bushlack
	Last Updated:       2025/02/13
	What Was Changed:   Initial creation
	</summary> 
*/
print "" print "*** creating procedure sp_select_rejected_volunteerstatus_by_projectid"
GO
CREATE PROCEDURE [dbo].[sp_select_rejected_volunteerstatus_by_projectid] 
(
	@ProjectID			[int]
)
AS
	BEGIN
		SELECT 			[VolunteerStatus].[UserID], [VolunteerStatus].[ProjectID], [Approved], [GivenName]
		FROM			[VolunteerStatus] join [Project] on [VolunteerStatus].[ProjectID] = [Project].[ProjectID] join [User] on [VolunteerStatus].[UserID] = [User].[UserID]
		WHERE			[Project].[ProjectID] = @ProjectID AND [Approved] = 0
	END
GO

/* 
	<summary>
	Creator:            Syler Bushlack
	Created:            2025/02/13
	Summary:            Creation of a stored procedure that updates a specific project join requests
	Last Updated By:    Syler Bushlack
	Last Updated:       2025/02/13
	What Was Changed:   Initial creation
	</summary> 
*/
print '' print '*** creating procedure sp_update_volunteerstatus_by_userid_and_projectid'
GO
CREATE PROCEDURE [dbo].[sp_update_volunteerstatus_by_userid_and_projectid]
	(
		@UserID					[int],
		@ProjectID				[int],
		@Approved				[bit]
	)
AS
	BEGIN
		UPDATE 	[VolunteerStatus]
		SET		[Approved] = @Approved
		WHERE	[UserID] = @UserID
		AND		[ProjectID] = @ProjectID

		RETURN 	@@ROWCOUNT
	END
GO
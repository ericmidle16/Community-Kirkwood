print '' print '*** starting database USER_08'
/*
    <summary>
    Creator: Brodie Pasker
    Created: 2025/02/07
    Summary: Creation of the USER_01 database.sql.
    Last Updated By:
    Last Updated:
    What Was Changed:
    </summary>
*/

print '' print '***  using community database communityDb'
GO
USE [communityDb]
GO

print '' print '*** creating sp_select_tasks_assigned_by_user_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_tasks_assigned_by_user_id]
(
    @UserID     [int]
)
AS
    BEGIN
        SELECT [Task].[TaskID], [Task].[Name], [Task].[EventID], [Task].[ProjectID], [Task].[TaskDate], [Task].[Active  ]
        FROM [Task] JOIN [TaskAssigned] ON [Task].[TaskID] = [TaskAssigned].[TaskID]
        WHERE [TaskAssigned].[UserID] = @UserID
        AND [Task].[Active] = 1
    END
GO

print '' print '*** creating sp_select_projects_by_approved_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_projects_by_approved_userID]
(
    @UserID [int]
)
AS
    BEGIN
        SELECT [Project].[ProjectID], [Project].[Name], [Location].[Name], [StartDate], [Status], [Project].[Description]
		FROM [Project]
			JOIN [Location] ON [Project].[LocationID] = [Location].[LocationID]
            JOIN [VolunteerStatus] ON [Project].[ProjectID] = [VolunteerStatus].[ProjectID]
		WHERE [VolunteerStatus].[UserID] = @UserID
    END
GO

print '' print '*** creating sp_select_events_on_projects_user_approved ***'
GO
CREATE PROCEDURE [dbo].[sp_select_events_on_projects_user_approved]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT 	[Event].[EventID], [Event].[DateCreated], [Event].[StartDate], [Event].[EndDate], [Event].[Name]
		FROM	[Event] JOIN [VolunteerStatus] ON [Event].[ProjectID] = [VolunteerStatus].[ProjectID]
		WHERE	[VolunteerStatus].[UserID] = @UserID
		AND		[VolunteerStatus].[Approved] = 1
        AND     [Event].[Active] = 1
	END
GO
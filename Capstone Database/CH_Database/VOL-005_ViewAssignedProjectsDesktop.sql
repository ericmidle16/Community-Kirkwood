/*
Creator:	Chase Hannen
Created:	2025/03/18
Summary:	SQL script for View Assigned Projects
Last Updated By:
Last Updated:
*/
print '' print '*** starting VOL-005.sql file'

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	SP that selects all projects associated with one user
*/

print '' print '*** creating sp_select_all_projects_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_projects_by_userID]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT 	[Project].[ProjectID], [Project].[Name], [Project].[ProjectTypeID], [Project].[LocationID], 
				[Project].[UserID], [Project].[StartDate], [Project].[Status], [Project].[Description], 
				[Project].[AcceptsDonations], [Project].[PayPalAccount], [Project].[AvailableFunds]
		FROM	[Project] JOIN [VolunteerStatus] ON [Project].[ProjectID] = [VolunteerStatus].[ProjectID]
		WHERE	[VolunteerStatus].[UserID] = @UserID
		AND		[VolunteerStatus].[Approved] = 1
	END
GO	
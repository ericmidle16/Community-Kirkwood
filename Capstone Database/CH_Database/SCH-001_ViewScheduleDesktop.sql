/*
Creator:	Chase Hannen
Created:	2025/03/30
Summary:	SQL script for View Schedule
Last Updated By:
			Chase Hannen
Last Updated:
What was Changed: Updated SP
*/

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO


------------------ STORED PROCEDURES --------------------

print '' print '*** creating sp_select_availabilityVM_by_projectID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_availabilityVM_by_projectID]
	(
		@ProjectID	[int]
	)
AS
	BEGIN
		SELECT 	[Availability].[AvailabilityID], [Availability].[UserID], [Availability].[IsAvailable],
				[Availability].[RepeatWeekly], [Availability].[StartDate], [Availability].[EndDate],
				[User].[GivenName], [User].[FamilyName]
		FROM	[Availability] JOIN [User] ON [Availability].[UserID] = [User].[UserID]
				JOIN [VolunteerStatus] ON [User].[UserID] = [VolunteerStatus].[UserID]
				JOIN [Project] ON [VolunteerStatus].[ProjectID] = [Project].[ProjectID]
		WHERE	[Project].[ProjectID] = @ProjectID
		AND		[Availability].[IsAvailable] = 'True'
		AND		[Availability].EndDate > sysdatetime()
	END
GO
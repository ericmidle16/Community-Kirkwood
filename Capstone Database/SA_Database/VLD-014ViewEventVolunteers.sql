/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/14
/// Summary:  View Event Volunteers, views the volunteers assigned to an event
/// Last Updated By: 
/// Last Updated: 
/// What was Changed: 
/// </summary>
*/

print '' print '*** starting VLD-014ViewEventVolunteers.sql'
GO
USE [communityDb]
GO

/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/14
/// Summary:  The stored procedure that views all of the volunteers assigned to an event.
/// Last Updated By: 
/// Last Updated:
/// What was Changed:
/// </summary>
*/
print '' print '***creating sp_view_event_volunteers'
GO
CREATE PROCEDURE [dbo].[sp_view_event_volunteers]
(
	@EventID	int
)
AS
	BEGIN
		SELECT DISTINCT	[User].[UserID],[User].[GivenName],[User].[FamilyName],[User].[City],[User].[State],
				[Task].[TaskID],[Task].[Name],[Task].[Description]
		FROM	[Event]
		JOIN	[Project]
			on 	[Event].[ProjectID] = [Project].[ProjectID]
		JOIN	[VolunteerStatus]
			on	[Project].[ProjectID] = [VolunteerStatus].[ProjectID]
		JOIN	[TaskAssigned]
			on	[VolunteerStatus].[UserID] = [TaskAssigned].[UserID]
		JOIN	[User]
			on	[VolunteerStatus].[UserID] = [User].[UserID]
		JOIN	[Task]
			on	[TaskAssigned].[TaskID] = [Task].[TaskID]
		WHERE	[VolunteerStatus].[Approved] = 1
			AND	[Task].[EventID] = @EventID
	END
GO
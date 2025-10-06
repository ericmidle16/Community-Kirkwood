/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/11
/// Summary:  Creating tables needed for Task Assignment.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/13
/// What was Changed: Moved sp_get_all_tasks_by_eventid to its own file
/// </summary>
*/

print '' print '*** starting VLD-005TaskAssignment.sql'
GO
USE [communityDb]
GO


print '' print '*** creating Project Role Table'
GO
CREATE TABLE [ProjectRole] (
	[ProjectRoleID]		[nvarchar](50)		NOT NULL,
	[Description]		[nvarchar](250)		NOT NULL	DEFAULT '',
	
	CONSTRAINT [pk_projectroleid] PRIMARY KEY ([ProjectRoleID] ASC)
)
GO

print '' print '*** creating Volunteer Status Project Role Table'
GO
CREATE TABLE [VolunteerStatusProjectRole] (
	[UserID]		[int]			NOT NULL,
	[ProjectID]		[int]			NOT NULL,
	[ProjectRoleID]	[nvarchar](50)	NOT NULL,
	
	CONSTRAINT [fk_volunteerstatusprojectrole_userid] FOREIGN KEY ([UserID])
		REFERENCES [User]([UserID]),
	CONSTRAINT [fk_volunteerstatusprojectrole_projectid] FOREIGN KEY ([ProjectID])
		REFERENCES [Project]([ProjectID]),
	CONSTRAINT [fk_volunteerstatusprojectrole_projectroleid] FOREIGN KEY ([ProjectRoleID])
		REFERENCES [ProjectRole]([ProjectRoleID])
)
GO


/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/11
/// Summary:  The stored procedure for assigning a task to a volunteer.
/// Last Updated By: 
/// Last Updated:
/// What was Changed:
/// </summary>
*/
DROP PROCEDURE IF EXISTS [sp_task_assignment];
GO
print '' print '*** creating sp_task_assignment'
GO
CREATE PROCEDURE [sp_task_assignment]
	(
		@TaskID		[int],
		@UserID		[int]
	)
AS
	BEGIN
		INSERT INTO [TaskAssigned]
			([TaskID],[UserID])
		VALUES
			(@TaskID,@UserID)
	END
GO
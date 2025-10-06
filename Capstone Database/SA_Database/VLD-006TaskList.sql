/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/11
/// Summary:  Creating tables needed for Task Assignment.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/13
/// What was Changed: Moved sp_get_all_tasks_by_eventid into its own sql file
/// </summary>
*/

print '' print '*** starting VLD-006TaskList.sql'
GO
USE [communityDb]
GO

/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/11
/// Summary:  The stored procedure for viewing all the tasks associated with an event.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/12
/// What was Changed: Confirmed and fixed formatting
/// </summary>
*/
/*
DROP PROCEDURE IF EXISTS [sp_get_all_tasks_by_eventid];
GO
*/
print '' print '*** creating sp_get_all_tasks_by_eventid'
GO
CREATE PROCEDURE [sp_get_all_tasks_by_eventid]
(
	@EventID	[int]
)
AS
	BEGIN
		SELECT
				[TaskID],[Name],[Description],[ProjectID],[TaskType]
		FROM	[Task]
		WHERE	[EventID] = @EventID
			AND	[Active] = 1
	END
GO

/*
/// <summary>
/// Creator:  Josh Nicholson
/// Created:  2025/04/04
/// Summary:  The stored procedure for viewing all the tasks associated with an project.
/// Last Updated By: Josh Nicholson
/// Last Updated: 2025/04/04
/// What was Changed: creation
/// </summary>
*/
print '' print '*** creating sp_get_all_tasks_by_projectid'
GO
CREATE PROCEDURE [sp_get_all_tasks_by_projectid]
(
	@ProjectID	[int]
)
AS
	BEGIN
		SELECT
				[TaskID],[Name],[Description],[TaskType],[EventID]
		FROM	[Task]
		WHERE	[ProjectID] = @ProjectID
			AND	[Active] = 1
	END
GO
-- <summary>
-- Creator:  Josh Nicholson
-- Created:  2025/02/27
-- Summary:  SQL file for the updateTask feature's related tables and sample data
-- </summary>

print '' print '' print '*** creating the updateTask feature files'
GO
USE [communityDb]
GO

print '' print '*** creating procedure sp_update_task_by_taskID'
GO
CREATE PROCEDURE [dbo].[sp_update_task_by_taskID]
    (
        @TaskID			[int],
		@Name			[nvarchar](100),
		@Description	[nvarchar](250),
		@TaskDate		[date],
        @TaskType   	[nvarchar](50),
		@Active   		[bit]     		
    )
AS
    BEGIN
		UPDATE [dbo].[Task]
		SET [Name]        	= @Name,
			[Description] 	= @Description,
			[TaskDate]		= @TaskDate,
			[TaskType]    	= @TaskType,
			[Active]      	= @Active
		WHERE [TaskID] = @TaskID
    END 
GO

print '' print '*** creating procedure sp_get_task_by_taskID'
GO
CREATE PROCEDURE [dbo].[sp_get_task_by_taskID]
	(
		@TaskID			[int]
	)
AS
    BEGIN
        SELECT *		   
        FROM [dbo].[Task]
		WHERE [TaskID] = @TaskID
    END 
GO
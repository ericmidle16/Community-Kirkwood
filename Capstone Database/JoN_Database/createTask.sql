-- <summary>
-- Creator:  Josh Nicholson
-- Created:  2025/02/20
-- Summary:  SQL file for the createTask feature's related tables and sample data
-- </summary>

print '' print '' print '*** creating the createTask feature files'
GO
USE [communityDb]
GO

print '' print '*** creating procedure sp_add_task'
GO
CREATE PROCEDURE [dbo].[sp_add_task]
    (
        @Name   		[nvarchar](100),
		@Description	[nvarchar](250),
		@TaskDate		[date],
        @ProjectID  	[int],
        @TaskType   	[nvarchar](50),
        @EventID   		[int],
		@Active   		[bit]     		
    )
AS
    BEGIN
        INSERT INTO [dbo].[Task]
			([Name],[Description],[TaskDate],[ProjectID],[TaskType], [EventID], [Active])
		VALUES
			(@Name, @Description, @TaskDate, @ProjectID, @TaskType, @EventID, @Active)
	
		SELECT SCOPE_IDENTITY()
    END 
GO

print '' print '*** creating procedure sp_get_all_task_types'
GO
CREATE PROCEDURE [dbo].[sp_get_all_task_types]
AS
    BEGIN
        SELECT *		   
        FROM [dbo].[TaskType]
    END 
GO
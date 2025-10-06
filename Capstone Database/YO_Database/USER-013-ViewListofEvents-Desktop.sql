print '' print '*** Starting USER-013-ViewListofEvents-Desktop.sql ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO


print '' print '*** Creating sp_select_event_list'
GO
CREATE PROCEDURE [dbo].[sp_select_event_list]

(
	@Active			[bit]
)
AS
BEGIN
	SELECT 	[EventID],[EventTypeID],[ProjectID],[DateCreated],[StartDate],[EndDate],[Name],[LocationID],[VolunteersNeeded],[UserID],[Notes],[Description],[Active] 
	FROM 	[dbo].[Event]
	WHERE 	[Active] = @Active
	
END
GO

print '' print '*** Creating sp_select_event_list_by_project_id'
GO
CREATE PROCEDURE [dbo].[sp_select_event_list_by_project_id]

(
	@ProjectID			[int]
)
AS
BEGIN
	SELECT 	[EventID],[EventTypeID],[ProjectID],[DateCreated],[StartDate],[EndDate],[Name],[LocationID],[VolunteersNeeded],[UserID],[Notes],[Description],[Active] 
	FROM 	[dbo].[Event]
	WHERE 	[Active] = 1
	AND 	[ProjectID] = @ProjectID
END
GO

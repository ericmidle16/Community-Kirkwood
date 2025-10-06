print '' print '*** Starting SCH-02-DeleteEvent-Desktop.sql ***'
GO


print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
    <summary>
    Creator:            Yousif Omer
    Created:            2025/02/14
    Summary:            This is the stored procedure for the deactivate Event
    Last Updated By:    
    Last Updated:       
    What Was Changed:   
    </summary>
*/


print '' print '*** Creating sp_deactivate_event'
GO
CREATE PROCEDURE [sp_deactivate_event]
(
	@EventID		[int]
)
AS
BEGIN
	
	UPDATE  	[dbo].[Event]
	SET [Active] = 0
	WHERE 	[EventID] = @EventID
	RETURN @@ROWCOUNT
END







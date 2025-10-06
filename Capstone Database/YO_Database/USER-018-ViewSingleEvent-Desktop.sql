print '' print '*** Starting USER-018-ViewSingleEvent-Desktop.sql ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO



/*
    <summary>
    Creator:            Yousif Omer
    Created:            2025/02/02
    Summary:            This is the stored procedure for the View Event
    Last Updated By:    
    Last Updated:       
    What Was Changed:   
    </summary>
*/



print '' print '*** creating sp_select_single_event ***'
GO
	CREATE PROCEDURE [dbo].[sp_select_single_event]
(
    @EventID                [int]
)
AS
BEGIN
    SELECT 
	    E.[EventID], 
        E.[EventTypeID], 
        E.[ProjectID], 
        E.[DateCreated], 
        E.[StartDate], 
        E.[EndDate], 
        E.[Name], 
        E.[LocationID], 
        E.[VolunteersNeeded], 
        E.[UserID], 
        E.[Notes], 
        E.[Description], 
        E.[Active]
     
	
	FROM [dbo].[Event]E
	JOIN [dbo].[Project] P ON E.[ProjectID] = P.[ProjectID]
    JOIN [dbo].[Location] L ON E.[LocationID] = L.[LocationID]
    WHERE [EventID]=@EventID
        
END
GO


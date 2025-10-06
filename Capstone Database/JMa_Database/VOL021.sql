GO
USE [communityDb]
GO


/* 
    <summary>
    Creator:            Jackson Manternach
    Created:            03/13/2025
    Summary:            Stored procedure creation for selecting thread by a ProjectID
    
	Last Updated By:    Kate Rich
    Last Updated:       2025-04-18
    What Was Changed:   Updated the Select List to include the ThreadID.
    </summary> 
*/
print '' print '*** creating sp_select_threads_by_projectid'
GO
CREATE PROCEDURE [dbo].[sp_select_threads_by_projectid]
(
    @ProjectID [int]
)
AS
BEGIN
    SELECT t.[ThreadID], t.[DatePosted], t.[ThreadName], t.[UserID], u.[GivenName], u.[FamilyName]
    FROM [dbo].[Thread] t
	JOIN [dbo].[User] u ON t.UserID = u.UserID
    WHERE [ProjectID] = @ProjectID
    ORDER BY [DatePosted] DESC
END
GO
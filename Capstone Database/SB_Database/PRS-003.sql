print '' print '*** starting PRS-003.sql file'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/* 
	<summary>
	Creator:            Syler Bushlack
	Created:            2025/04/04
	Summary:            Creation of astored procedure that returns all of a user's projects
	Last Updated By:    Syler Bushlack
	Last Updated:       2025/04/04
	What Was Changed:   Initial creation
	
	Last Updated By:    
	Last Updated:       
	What Was Changed:   
	</summary> 
*/
print "" print "*** creating procedure sp_select_created_projects_by_userID"
GO
CREATE PROCEDURE [dbo].[sp_select_created_projects_by_userID](
	@UserID [int]
)
AS
	BEGIN
		SELECT 			[ProjectID], [Project].[Name], [ProjectTypeID], [Project].[LocationID],
						[Project].[UserID], [StartDate],
						[Status], [Project].[Description], [AvailableFunds], [GivenName], [Location].[Name]
		FROM			[Project] join [User] on [Project].[UserID] = [User].[UserID] join [Location] on [Project].[LocationID] = [Location].[LocationID]
		WHERE [Project].[UserID] = @UserID
	END
GO
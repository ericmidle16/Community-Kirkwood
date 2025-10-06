print '' print '*** starting MOD-002.sql file'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/* 
	<summary>
	Creator:            Syler Bushlack
	Created:            2025/04/23
	Summary:            Creation of a stored procedure that updates a user's ForumPermission WriteAccess
	Last Updated By:    Syler Bushlack
	Last Updated:       2025/04/23
	What Was Changed:   Initial creation
	
	Last Updated By:    
	Last Updated:       
	What Was Changed:   
	</summary> 
*/
print "" print "*** creating procedure sp_update_forumpermission_writeaccess_value"
GO
CREATE PROCEDURE [dbo].[sp_update_forumpermission_writeaccess_value](
    @UserID [INT],
    @ProjectID [INT],
    @WriteAccess [BIT]
)
AS
	BEGIN
		UPDATE [ForumPermission]
        SET [WriteAccess] = @WriteAccess
		WHERE [UserID] = @UserID
            AND [ProjectID] = @ProjectID
		
		RETURN 	@@ROWCOUNT
	END
GO

/* 
	<summary>
	Creator:            Syler Bushlack
	Created:            2025/04/24
	Summary:            Creation of a stored procedure that gets all user's ForumPermissions for a project
	Last Updated By:    Syler Bushlack
	Last Updated:       2025/04/23
	What Was Changed:   Initial creation
	
	Last Updated By:    
	Last Updated:       
	What Was Changed:   
	</summary> 
*/
print "" print "*** creating procedure sp_select_forumpermission_by_projectID"
GO
CREATE PROCEDURE [dbo].[sp_select_forumpermission_by_projectID](
    @ProjectID [INT]
)
AS
	BEGIN
		SELECT [ForumPermission].[UserID], [ProjectID], [WriteAccess], [GivenName] 
		FROM [ForumPermission] join [User] on [ForumPermission].[UserID] = [User].[UserID]
		WHERE [ProjectID] = @ProjectID
	END
GO
print '' print '*** starting MOD-001.sql file'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/* 
	<summary>
	Creator:            Syler Bushlack
	Created:            2025/04/23
	Summary:            Creation of a stored procedure that updates a post's pinned value
	Last Updated By:    Syler Bushlack
	Last Updated:       2025/04/23
	What Was Changed:   Initial creation
	
	Last Updated By:    
	Last Updated:       
	What Was Changed:   
	</summary> 
*/
print "" print "*** creating procedure sp_update_post_pinned_value"
GO
CREATE PROCEDURE [dbo].[sp_update_post_pinned_value](
	@Pinned [BIT],
    @PostID [INT]
)
AS
	BEGIN
		UPDATE [Post]
        SET [Pinned] = @Pinned
		WHERE [PostID] = @PostID
		
		RETURN 	@@ROWCOUNT
	END
GO
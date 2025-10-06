/*
/// <summary>
/// Creator:  Nikolas Bell
/// Created:  2025/04/02
/// Summary:  This script enables the deletion of forum posts
/// Last Updated By:
/// Last Updated:
/// What was Changed:
/// </summary>
*/
print '' print '*** Starting VOL-028DeleteThreadPosts.sql ***'
GO
USE [communityDb]
GO

print '' print '*** creating stored procedure sp_delete_post_by_postID ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_post_by_postID]
	(
		@PostID [int]
	)
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM [Post] WHERE [PostID] = @PostID;
    
    -- Return the number of affected rows
    SELECT @@ROWCOUNT;
END
GO
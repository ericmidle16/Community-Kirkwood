print '' print '*** starting database VOL_023'
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/03/02
	Summary:  Creation of the VOL_022 database sql
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/

print '' print '*** using database commmunityDb'
GO
USE [communityDb]
GO

/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/03/03
	Summary:  Creating stored procedure to 
			  edit a inserted post by user. The 
			  user can only edit their own post.
	Last Updated By: Skyann Heintz
	Last Updated: 2025-03-14
	What Was Changed: p.PostID -fpn.ProjectID changed to   p.ThreadID = fpn.ProjectIDs

    Last Updated By: Syler Bushlack
	Last Updated: 2025-04-30
	What Was Changed: SELECT @@ROWCOUNT AS RowsAffected; changed to RETURN @@ROWCOUNT 
        and commented out unneccessary sql validation
	</summary>
*/
print '' print '*** creating stored procedure sp_edit_post'
GO
CREATE PROCEDURE [dbo].[sp_edit_post]  
    (  
        @PostID 	    [int],  
        @UserID 		[int],  
        @NewContent 	[nvarchar](500)
    )  
AS  
BEGIN  
        -- Update the forum post  
        UPDATE [Post]  
        SET    [Content] = @NewContent,  
               [Edited] = 1  
            --    [DatePosted] = GETDATE()
        WHERE  [PostID] = @PostID;

        RETURN @@ROWCOUNT
END
GO

/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/03/03
	Summary:  Creating stored procedure to retrieve a user's inserted post. This is 
				just for testing purposes and isn't part of my features
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/
print '' print '*** creating stored procedure sp_select_user_post'
GO
CREATE PROCEDURE [dbo].[sp_select_user_post]  
    (  
        @ThreadID [int] = NULL,  
        @UserID  [int]
    )  
AS  
BEGIN  
    SET NOCOUNT ON;  
	SELECT
         [PostID], 
         [ThreadID], 
         [UserID], 
         [Edited], 
         [Content], 
         [DatePosted], 
         [Reply], 
         [Pinned]
    FROM [Post] p
    WHERE p.[UserID] = @UserID  
         AND (@ThreadID IS NULL OR p.[ThreadID] = @ThreadID)   
    ORDER BY [DatePosted] ASC;
END  
GO
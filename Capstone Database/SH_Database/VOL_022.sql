print '' print '*** starting database VOL_022'
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
    Created: 2025/04/21
    Summary: Creates a stored procedure to select a single post based on PostID.
    Last Updated By: 
    Last Updated: 
    What Was Changed: 
    </summary>
*/
PRINT '' 
PRINT '*** creating stored procedure sp_select_post_by_id'
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_select_post_by_id]
	(
		@PostID [int]
	)
AS
BEGIN
    SELECT 
        p.[PostID],
        p.[UserID],
        p.[ThreadID],
        p.[Reply],
        p.[Edited],
        p.[Pinned],
        p.[Content],
        p.[DatePosted],
        u.[GivenName],
        u.[FamilyName]
    FROM [Post] p
    JOIN [User] u ON p.[UserID] = u.[UserID]
    WHERE p.[PostID] = @PostID;
END
GO

/*
    <summary>
    Creator: Skyann Heintz
    Created: 2025/04/18
    Summary: Checks if a user has write access for a specific project.
    Last Updated By: 
    Last Updated: 
    What Was Changed: 
    </summary>
*/
print '' print '*** creating stored procedure sp_select_user_write_access'
GO
CREATE PROCEDURE [dbo].[sp_select_user_write_access]
	(
		@UserID [int],
		@ProjectID [int]
	)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM [ForumPermission]
        WHERE [UserID] = @UserID
          AND [ProjectID] = @ProjectID
          AND [WriteAccess] = 1
    )
    BEGIN
        SELECT 1 AS HasWriteAccess;
    END
    ELSE
    BEGIN
        SELECT 0 AS HasWriteAccess;
    END
END
GO



/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/03/02
	Summary:  Creating stored procedure to Insert
			a post by ThreadID, UserID and Content which is added
			by the user. Defaults pinned to 0 
			and edited to 0.
	Last Updated By: Skyann Heintz
	Last Updated: 2025-03-03
	What Was Changed: Added UserID to the first insert statement
	</summary>
*/


print '' print '*** creating stored procedure sp_insert_post'
GO
CREATE PROCEDURE [dbo].[sp_insert_post]  
    (  
        @UserID  [int],  
        @Content [nvarchar](500),
		@ProjectID [int],
		@ThreadName[nvarchar](100),
		@DatePosted[DateTime]
    )  
AS  
BEGIN  
    SET NOCOUNT ON;  


    -- Check if the user has write access to the forum  
    IF EXISTS (  
        SELECT 1 FROM [ForumPermission]  
        WHERE [UserID] = @UserID  
            AND [ProjectID] = @ProjectID 
            AND [WriteAccess] = 1  
    ) 
    BEGIN  
	
		DECLARE @ThreadID int;
		
		INSERT INTO [Thread] ([ProjectID],[UserID],[ThreadName],[DatePosted])
		VALUES (@ProjectID, @UserID, @ThreadName, @DatePosted);
		
		SET @ThreadID = SCOPE_IDENTITY();
		
        INSERT INTO [Post] ([ThreadID], [UserID], [Edited], [Pinned], [Content], [DatePosted])  
        VALUES (@ThreadID, @UserID, 0, 0, @Content, GETDATE());  

 	    SELECT @@ROWCOUNT AS RowsAffected; 
    END  
END 
GO
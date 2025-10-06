/*
/// <summary>
/// Creator:  Nikolas Bell
/// Created:  2025/03/14
/// Summary:  This script creates tables, stored procedures, and test data for forum posts in the community database.
/// Last Updated By: 
/// Last Updated: 
/// What was Changed: 
/// </summary>
*/

print '' print '*** Starting VOL-024VeiwForumPost.sql ***'
GO
USE [communityDb]
GO

/*
/// <summary>
/// Creates the Thread table to store forum discussion threads.
/// </summary>
*/
print '' print '*** Creating Thread TABLE ***'
GO
CREATE TABLE [dbo].[Thread] (
	[ThreadID]		INT			IDENTITY(100000,1) NOT NULL,
	[ProjectID]		INT			NOT NULL,
	[UserID]		INT			NOT NULL,
	[ThreadName]	NVARCHAR(100)	NOT NULL,
	DatePosted		DATETIME	NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_Thread PRIMARY KEY (ThreadID),
	CONSTRAINT FK_ForumPermission_Project FOREIGN KEY (ProjectID) REFERENCES Project(ProjectID),
	CONSTRAINT FK_Thread_User FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

/*
/// <summary>
/// Creates the ForumPermission table to manage user permissions for projects in the forum.
/// </summary>
*/
print '' print '*** Creating ForumPermission TABLE ***'
GO
CREATE TABLE [dbo].[ForumPermission] (
	[UserID]	[int]	NOT NULL,
	[ProjectID]	[int]	NOT NULL,
[WriteAccess]	[bit]	NOT NULL,
INDEX [ix_forumPermission_projectID] ([ProjectID]),
CONSTRAINT [pk_forumpermission] PRIMARY KEY ([UserID], [ProjectID]),
CONSTRAINT [fk_forumpermission_userid] FOREIGN KEY ([UserID])
		REFERENCES [User]([UserID]),
	CONSTRAINT [fk_forumpermission_projectid] FOREIGN KEY ([ProjectID])
		REFERENCES [Project]([ProjectID])
)
GO

/*
/// <summary>
/// Creates the Post table to store user posts within forum threads.
/// </summary>
*/
print '' print '*** Creating Post TABLE ***'
GO

CREATE TABLE [dbo].[Post] (
	[PostID] INT IDENTITY(100000,1) NOT NULL,
	[UserID] INT NOT NULL,
	[ThreadID] INT NOT NULL,
	[Reply] BIT NOT NULL DEFAULT 0,
	[Edited] BIT NOT NULL DEFAULT 0,
	[Pinned] BIT NOT NULL DEFAULT 0,
	[Content] NVARCHAR(500) NOT NULL,
	[DatePosted] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_Post PRIMARY KEY (PostID),
	CONSTRAINT FK_Post_Thread FOREIGN KEY (ThreadID) REFERENCES Thread(ThreadID),
	CONSTRAINT FK_Post_User FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

/*
/// <summary>
/// Creates a stored procedure to retrieve all posts within a given thread, ordered by date.
///
/// Last Updated By: Syler Bushlack
/// Last Updated: 2025-04-23
/// What was Changed: changed the orderby so it first orders by Pinned then by DatePosted
/// </summary>
*/
PRINT '*** Create Stored Procedure sp_select_all_posts_by_threadID ***'
GO
CREATE PROCEDURE sp_select_all_posts_by_threadID
    @ThreadID INT
AS
BEGIN
SET NOCOUNT ON;

    ;WITH OrderedPosts AS (
        SELECT 
            p.PostID,
            p.ThreadID,
            p.UserID,
            p.Reply,
            p.Edited,
            p.Pinned,
            p.Content,
            p.DatePosted,
            u.GivenName,
            u.FamilyName,
            -- Row number based on date to preserve natural order
            ROW_NUMBER() OVER (ORDER BY p.DatePosted ASC) AS RowNum
        FROM Post p
        JOIN [User] u ON p.UserID = u.UserID
        WHERE p.ThreadID = @ThreadID
    )
    SELECT 
        PostID,
        ThreadID,
        UserID,
        Reply,
        Edited,
        Pinned,
        Content,
        DatePosted,
        GivenName,
        FamilyName
    FROM OrderedPosts
    ORDER BY 
        Pinned DESC,    -- Show pinned first
        RowNum ASC;     -- Maintain natural order
END;
GO
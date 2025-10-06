/*
/// <summary>
/// Creator:  Nikolas Bell
/// Created:  2025/08/02
/// Summary:  This script enables the ability to create new posts
/// Last Updated By:
/// Last Updated:
/// What was Changed:
/// </summary>
*/
print '' print '*** Starting VOL-0XXReply.sql ***'
GO
USE [communityDb]
GO

PRINT '*** creating stored procedure sp_reply_by_threadId ***'
GO

CREATE PROCEDURE [dbo].[sp_reply_by_threadId]
	(
		@UserID INT,
		@ThreadID INT,
		@Content NVARCHAR(500)
	)
AS
BEGIN
	INSERT INTO [Post] ([UserID], [ThreadID], [Reply], [Edited], [Pinned], [Content], [DatePosted])
	VALUES (@UserID, @ThreadID, 1, 0, 0, @Content, GETDATE());
END
GO
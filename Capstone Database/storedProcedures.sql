print '' print '*** Starting StoredProcedures.sql ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-05-02
		Summary:	Stored Procedure for inserting a ForumPermission record into the DB.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_insert_forumPermission ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_forumPermission]
	(
		@UserID			[int],
		@ProjectID		[int],
		@WriteAccess	[bit]
	)
AS
	BEGIN
		INSERT INTO [ForumPermission]
			([UserID], [ProjectID], [WriteAccess])
		VALUES
			(@UserID, @ProjectID, @WriteAccess)
		RETURN @@ROWCOUNT
	END
GO
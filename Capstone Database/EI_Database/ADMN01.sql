print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*	=======================================================================================
		Beginning stored procedures
	======================================================================================= */

/*
    Creator: Eric Idle
    Summary: This is to see all possible System roles
    Last Updated By: Eric Idle
    Last Updated: 2025-02-06
    What Was Changed:
*/
print '' print '*** creating procedure sp_select_all_systemroles'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_systemroles]

AS
	BEGIN
		SELECT [SystemRoleID], [Description]
		FROM [SystemRole]
	END
GO

/*
    Creator: Eric Idle
    Summary: This is to delete a record within the UserSystemRole table
    Last Updated By: Eric Idle
    Last Updated: 2025-02-14
    What Was Changed:
*/
print '' print '*** creating procecdure sp_delete_user_systemrole_by_userid'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_systemrole_by_userid]
(
	@UserID				[int],
	@SystemRoleID		[nvarchar](50)
)
AS
	BEGIN
		DELETE FROM UserSystemRole
		WHERE UserID = @UserID
		AND SystemRoleID = @SystemRoleID;
	END
GO
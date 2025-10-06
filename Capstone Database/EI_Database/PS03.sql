print '' print '*** using database communityDb'
GO
USE [communityDb]
GO


/*
    Creator: Eric Idle
    Summary: This is to see all possible System roles
    Last Updated By: Eric Idle
    Last Updated: 2025-04-11
    What Was Changed:
*/
print '' print '*** creating procedure sp_select_all_projectroles'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_projectroles]

AS
	BEGIN
		SELECT [ProjectRoleID], [Description]
		FROM [ProjectRole]
	END
GO

/*
    Creator: Eric Idle
    Summary: This is to delete a record within the VolunteerStatusProjectRole table
    Last Updated By: Eric Idle
    Last Updated: 2025-02-14
    What Was Changed:
*/
print '' print '*** creating procecdure sp_delete_projectrole_by_userid_projectid_projectroleid'
GO
CREATE PROCEDURE [dbo].[sp_delete_projectrole_by_userid_projectid_projectroleid]
(
	@UserID				[int],
	@ProjectID			[int],
	@ProjectRoleID		[nvarchar](50)
)
AS
	BEGIN
		DELETE FROM VolunteerStatusProjectRole
		WHERE UserID = @UserID
		AND ProjectID = @ProjectID
		AND ProjectRoleID = @ProjectRoleID;
		RETURN @@ROWCOUNT
	END
GO

/*
    Creator: Eric Idle
    Summary: This is to insert a record within the VolunteerStatusProjectRole table
    Last Updated By: Eric Idle
    Last Updated: 2025-02-14
    What Was Changed:
*/
print '' print '*** creating procedure sp_insert_projectrole_by_userid_projectid_projectroleid'
GO

CREATE PROCEDURE [dbo].[sp_insert_projectrole_by_userid_projectid_projectroleid]
(
	@UserID			[int],
	@ProjectID		[int],
    @ProjectRoleID 	[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[VolunteerStatusProjectRole] ([UserID],[ProjectID],[ProjectRoleID])
		VALUES (@UserID, @ProjectID, @ProjectRoleID);
		RETURN @@ROWCOUNT
	END
GO

/*
	Creator:	Eric Idle
	Created:	2025-02-14
	Summary:	Stored Procedure for selecting all user system roles by userID
	Last Updated By: Eric Idle
    Last Updated:
    What Was Changed:
*/
print '' print '*** Create Stored Procedure sp_select_volunteer_projectroles_by_userid_projectid***'
GO
CREATE PROCEDURE [dbo].[sp_select_volunteer_projectroles_by_userid_projectid]
	(
		@UserID			[int],
		@ProjectID		[int]
	)
AS
	BEGIN
		SELECT [UserID], [ProjectID], [ProjectRoleID]
		FROM [VolunteerStatusProjectRole]
		WHERE [UserID] = @UserID
		AND [ProjectID] = @ProjectID
	END
GO
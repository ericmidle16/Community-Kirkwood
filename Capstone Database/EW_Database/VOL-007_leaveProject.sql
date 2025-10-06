/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-08
		Summary:	Creation Script for leaveProject.
		Last Updated By:    Liam Easton
		Last Updated:       01/19/2023
		What Was Changed:   ???  
	</summary> 
*/

print '' print '*** starting VOL-007_leaveProject'
print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-08
		Summary:	Stored Procedure for deactivating a volunteer from a specific project
	</summary>
*/
print '' print '*** Create Stored Procedure sp_deactivate_volunteer_by_userid_and_projectid ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_volunteer_by_userid_and_projectid]
	(
		@UserID		[int],
		@ProjectID 	[int]
	)
AS
	BEGIN
		UPDATE VolunteerStatus
		SET Approved = 0
		WHERE UserID = @UserID AND ProjectID = @ProjectID
		RETURN @@ROWCOUNT
	END
GO


/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-08
		Summary:	Stored Procedure for deleting the departed user's roles
		Last Updated By: Stan Anderson
		Last Updated: 2025/04/09
		Removed from Volunteer table as well
	</summary>
*/
print '' print '*** Create Stored Procedure sp_update_departed_user_roles_to_placeholder_user ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_roles]
	(
		@UserID		[int],
		@ProjectID 	[int]
	)
AS
	BEGIN
		DELETE FROM VolunteerStatusProjectRole
		WHERE UserID = @UserID AND ProjectID = @ProjectID;
		RETURN @@ROWCOUNT
	END
GO


/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-03-12
		Summary:	Stored Procedure for selecting a volunteer status by user and project ID
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_volunteer_status_by_user_and_project_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_volunteer_status_by_user_and_project_id]
	(
		@UserID		[int],
		@ProjectID 	[int]
	)
AS
	BEGIN
		SELECT [UserID], [ProjectID], [Approved]	
		FROM VolunteerStatus
		WHERE UserID = @UserID AND ProjectID = @ProjectID;
	END
GO
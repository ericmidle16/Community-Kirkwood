/*
Creator:	Chase Hannen
Created:	2025/02/13
Summary:	SQL script for Unassign Volunteers
Last Updated By:
			Chase Hannen
Last Updated:
			2025/03/14
What was Changed:
			Added sp_deactivate_volunteer_status_by_user_id_and_project_id
			Conformed to naming conventions
			Added Biography
*/

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO


------------------ STORED PROCEDURES --------------------
print '' print '*** creating sp_select_user_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_userID]
	(
		@UserID					[nvarchar](50)
	)
AS
	BEGIN
		SELECT	[UserID], [GivenName], [FamilyName], [PhoneNumber], [Email], [City], [State],
				[Image], [ImageMimeType], [PasswordHash], [ReactivationDate], [Suspended], 
				[ReadOnly], [Active], [RestrictionDetails], [Biography]
		FROM	[User]
		WHERE 	[UserID] = @UserID
	END
GO

print '' print '*** creating sp_select_user_by_projectID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_projectID]
	(
		@ProjectID				[int]
	)
AS
	BEGIN
		SELECT	[User].[UserID], [User].[GivenName], [User].[FamilyName], [User].[PhoneNumber], 
				[User].[Email], [User].[City], [User].[State], [User].[Image], [User].[ImageMimeType],
				[User].[PasswordHash], [User].[ReactivationDate], [User].[Suspended], [User].[ReadOnly], 
				[User].[Active], [User].[RestrictionDetails], [User].[Biography]
		FROM	[User] JOIN [VolunteerStatus] ON [User].[UserID] = [VolunteerStatus].[UserID]
		WHERE	[VolunteerStatus].[ProjectID] = @ProjectID
		AND		[VolunteerStatus].[Approved] = 1
	END
GO
		
print '' print '*** creating sp_deactivate_volunteer_status_by_user_id_and_project_id ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_volunteer_status_by_user_id_and_project_id]
	(
		@UserID					[int],
		@ProjectID				[int]
	)
AS
	BEGIN
		UPDATE	[VolunteerStatus]
		SET		[Approved] = 0
		WHERE	[UserID] = @UserID
		AND		[ProjectID]	= @ProjectID
		
		RETURN @@ROWCOUNT
	END
GO
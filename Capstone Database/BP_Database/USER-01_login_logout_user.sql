print '' print '*** starting database USER_01'
/*
    <summary>
    Creator: Brodie Pasker
    Created: 2025/02/07
    Summary: Creation of the USER_01 database.sql.
    Last Updated By:
    Last Updated:
    What Was Changed:
    </summary>
*/


print '' print '*** using database communityDb'
GO
USE [communityDb]
GO



print '' print '$$$ creating procedure sp_authenticate_user $$$'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
		@Email			[nvarchar](250),
		@PasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		SELECT	COUNT([UserID])
		FROM 	[User]
		WHERE	[Email] = @Email
		  AND 	[PasswordHash] = @PasswordHash
		  AND	[Active] = 1
	END
GO

print '' print '$$$ creating procedure sp_select_user_details_by_email $$$'
GO
CREATE PROCEDURE [dbo].[sp_select_user_details_by_email]
	(
		@Email		[nvarchar] (250)
	)
AS
	BEGIN
		SELECT [UserID], [GivenName], [FamilyName], [Biography], [PhoneNumber], [Email], [City], [State], [Image], [ImageMimeType], [PasswordHash], [ReactivationDate], 
        [Suspended], [ReadOnly], [Active], [RestrictionDetails]
		FROM	[User]
		WHERE	[Email] = @Email
	END
GO

print '' print '$$$ creating procedure sp_select_user_by_id $$$'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_id]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT [UserID], [GivenName], [FamilyName], [Biography], [PhoneNumber], [Email], [City], [State], [Image], [ImageMimeType], [PasswordHash], [ReactivationDate], 
        [Suspended], [ReadOnly], [Active], [RestrictionDetails]
		FROM	[User]
		WHERE	[UserID] = @UserID
	END
GO

print '' print '$$$ creating procedure sp_select_roles_by_UserID $$$'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_UserID]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT [UserID], [SystemRoleID]
		FROM	[UserSystemRole]
		WHERE	[UserID] = @UserID
	END
GO

print "" print "*** creating procedure sp_select_all_project_roles_by_userID"
GO
CREATE PROCEDURE [dbo].[sp_select_all_project_roles_by_userID](
    @UserID [int]
)
AS
BEGIN
    SELECT 
        [ProjectRoleID], 
        [ProjectID]
    FROM 
        [VolunteerStatusProjectRole]
    WHERE 
        [UserID] = @UserID
END
GO
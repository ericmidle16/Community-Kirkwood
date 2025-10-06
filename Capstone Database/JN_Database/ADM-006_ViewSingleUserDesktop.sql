print '' print '*** Starting  ADM-006_ViewSingleUserDesktop.sql ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/***********************starting select_user_by_email stored procedures************************/
print '' print '*** Creating procedure sp_select_user_by_email'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
(
	@Email		[nvarchar](250)
)
AS
	BEGIN
		SELECT [UserID], [GivenName], [FamilyName], [PhoneNumber], 
			[Email], [City], [State], [Image], [ImageMimeType], [ReactivationDate], [Suspended], [ReadOnly], 
			[Active], [RestrictionDetails], [Biography]
		FROM 	[User]
		WHERE	[Email] = @Email
	END
GO


print '' print '*** Creating procedure sp_update_user_status_by_userID'
GO
CREATE PROCEDURE [dbo].[sp_update_user_status_by_userID]
(
	@UserID					[int],
/*	@OldGivenName			[nvarchar] (50),
	@OldFamilyName			[nvarchar] (100),
	@OldPhoneNumber			[nvarchar] (13),
	@OldEmail				[nvarchar] (250),
	@OldCity				[nvarchar] (50),
	@OldState				[nvarchar] (50),
	@OldImage				[varbinary] (MAX)
	@OldImageMimeType		[nvarchar](50),*/
	@OldReactivationDate 	[DateTime],
	/* @OldSuspended			[bit],
	@OldReadOnly			[bit],
	@OldActive				[bit], */
	@OldRestrictionDetails  [nvarchar] (250),
/*	@OldBiography			[nvarchar] (250), 
	
	@NewGivenName		[nvarchar] (50),
	@NewFamilyName		[nvarchar] (100),
	@NewPhoneNumber	[nvarchar] (13),
	@NewEmail			[nvarchar] (250),
	@NewCity			[nvarchar] (50),
	@NewState			[nvarchar] (50),
	@NewImage			[varbinary] (MAX)
	@NewImageMimeType	[nvarchar](50),*/
	@NewReactivationDate [DateTime],
	@NewSuspended		[bit],
	@NewReadOnly		[bit],
	@NewActive			[bit],
	@NewRestrictionDetails [nvarchar] (250)
/*	@NewBiography			[nvarchar] (250) */
)
AS
	BEGIN
		UPDATE [User]
		SET	
			/*[GivenName] = @NewGivenName, 
			[FamilyName] = @NewFamilyName, 
			[PhoneNumber] = @NewPhoneNumber, 
			[Email] = @NewEmail, 
			[City] = @NewCity, 
			[State] = @NewState, 
			[Image] = @NewImage,
			[ImageMimeType] = @NewImageMimeType,*/ 
			[ReactivationDate] = @NewReactivationDate, 
			[Suspended] = @NewSuspended, 
			[ReadOnly] = @NewReadOnly, 
			[Active] = @NewActive, 
			[RestrictionDetails] = @NewRestrictionDetails
			/* [Biography] = @NewBiography */
		WHERE	@UserID = [UserID]
			/* AND @OldGivenName = [GivenName]
			AND @OldFamilyName = [FamilyName]
			AND @OldPhoneNumber = [PhoneNumber]
			AND @OldEmail = [Email]
			AND @OldCity = [City]
			AND @OldState = [State]
			AND @OldImage = [Image]
			AND @OldImageMimeType = [ImageMimeType]*/
			AND (@OldReactivationDate = [ReactivationDate] or [ReactivationDate] is NULL)
			/* AND @OldSuspended = [Suspended]
			AND @OldReadOnly = [ReadOnly]
			AND @OldActive = [Active] */
			AND @OldRestrictionDetails = [RestrictionDetails]
			/* AND @OldBiography = [Biography] */	
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating procedure sp_deactivate_user_by_userID'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_user_by_userID]
(
	@UserID			[int]
)
AS
	BEGIN
		UPDATE 		[dbo].[User]
			SET		[Active] = 0
			WHERE 	[UserID] = @UserID
	END
GO

/***********************end of user-related stored procedures**************************/
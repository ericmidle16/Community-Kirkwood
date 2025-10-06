print '' print '*** using database communityDb'
GO
USE [communityDb]
GO
print '' print '*** VOL11'
GO


/* 
	<summary>
	Creator:            Jacob McPherson
	Created:            03/04/2025
	Summary:            Stored Procedure For Updating an ExternalContact
	Last Updated By:    ???
	Last Updated:       ???
	What Was Changed:   ???  
	</summary> 
*/
PRINT '' PRINT '*** Creating Procedure sp_update_external_contact'
GO
CREATE PROCEDURE [dbo].[sp_update_external_contact]
(
    @ExternalContactID [INT],
    @ExternalContactTypeID [NVARCHAR](50),
	@ContactName [NVARCHAR](100),
	@GivenName [NVARCHAR](50),
	@FamilyName [NVARCHAR](50),
	@Email [NVARCHAR](250),
	@PhoneNumber [NVARCHAR](13),
	@JobTitle [NVARCHAR](50),
	@Address [NVARCHAR](100),
	@Description [NVARCHAR](250),
	@ExternalContactTypeID_Old [NVARCHAR](50),
	@ContactName_Old  [NVARCHAR](100),
	@GivenName_Old  [NVARCHAR](50),
	@FamilyName_Old  [NVARCHAR](50),
	@Email_Old  [NVARCHAR](250),
	@PhoneNumber_Old  [NVARCHAR](13),
	@JobTitle_Old  [NVARCHAR](50),
	@Address_Old  [NVARCHAR](100),
	@Description_Old  [NVARCHAR](250),
	@UserID [INT]
)
AS
	BEGIN
	
		UPDATE [dbo].[ExternalContact]
		SET
			[ExternalContactTypeID] = @ExternalContactTypeID,
			[ContactName] = @ContactName,
			[GivenName] = @GivenName,
			[FamilyName] = @FamilyName,
			[Email] = @Email,
			[PhoneNumber] = @PhoneNumber,
			[JobTitle] = @JobTitle,
			[Address] = @Address,
			[Description] = @Description,
			[LastUpdatedBy] = @UserID
		WHERE
			[ExternalContactID] = @ExternalContactID
				AND
			[ExternalContactTypeID] = @ExternalContactTypeID_Old
				AND
			[ContactName] = @ContactName_Old
				AND
			[GivenName] = @GivenName_Old
				AND
			[FamilyName] = @FamilyName_Old
				AND
			[Email] = @Email_Old
				AND
			[PhoneNumber] = @PhoneNumber_Old
				AND
			[JobTitle] = @JobTitle_Old
				AND
			[Address] = @Address_Old
				AND
			[Description] = @Description_Old
	
	END
GO
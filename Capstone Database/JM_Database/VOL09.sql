print '' print '*** using database communityDb'
GO
USE [communityDb]
GO
print '' print '*** VOL09'
GO




/* 
	<summary>
	Creator:            Jacob McPherson
	Created:            02/18/2025
	Summary:            Creation for ExternalContactType table
	Last Updated By:    ???
	Last Updated:       ???
	What Was Changed:   ???  
	</summary> 
*/
PRINT '' PRINT '*** creating ExternalContactType table'
GO
CREATE TABLE [dbo].[ExternalContactType] (
    [ExternalContactTypeID] [NVARCHAR](50) NOT NULL PRIMARY KEY,
    [Description] [NVARCHAR](250) NOT NULL DEFAULT ''
)
GO

/* 
	<summary>
	Creator:            Jacob McPherson
	Created:            02/18/2025
	Summary:            Creation for ExternalContact table
	Last Updated By:    ???
	Last Updated:       ???
	What Was Changed:   ???  
	</summary> 
*/
PRINT '' PRINT '*** creating ExternalContact table'
GO
CREATE TABLE [dbo].[ExternalContact] (
    [ExternalContactID] [INT] IDENTITY (100000, 1) NOT NULL PRIMARY KEY,
    [ExternalContactTypeID] [NVARCHAR](50) NOT NULL,
	[ContactName] [NVARCHAR](100) NOT NULL,
	[GivenName] [NVARCHAR](50) NULL,
	[FamilyName] [NVARCHAR](50) NULL,
	[Email] [NVARCHAR](250) NULL,
	[PhoneNumber] [NVARCHAR](13) NULL,
	[JobTitle] [NVARCHAR](50) NULL DEFAULT '',
	[Address] [NVARCHAR](100) NULL DEFAULT '',
	[Description] [NVARCHAR](250) NULL DEFAULT '',
	[AddedBy] [INT] NOT NULL,
	[LastUpdatedBy] [INT] NOT NULL,
	[Active] [BIT] NOT NULL DEFAULT 1,
	CONSTRAINT [fk_ExternalContactType_ExternalContactTypeID] FOREIGN KEY ([ExternalContactTypeID]) REFERENCES [ExternalContactType] ([ExternalContactTypeID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_User_UserID_Added] FOREIGN KEY ([AddedBy]) REFERENCES [User] ([UserID]),
	CONSTRAINT [fk_User_UserID_Updated] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [User] ([UserID])
)
GO

/* 
	<summary>
	Creator:            Jacob McPherson
	Created:            02/18/2025
	Summary:            Stored Procedure For Retreiving All ExternalContactTypes
	Last Updated By:    ???
	Last Updated:       ???
	What Was Changed:   ???  
	</summary> 
*/
PRINT '' PRINT '*** Creating Procedure sp_select_all_contact_types'
GO
CREATE PROCEDURE [dbo].[sp_select_all_contact_types]
AS
	BEGIN
	
		SELECT
			[ExternalContactTypeID],
			[Description]
		FROM [dbo].[ExternalContactType]
	
	END
GO

/* 
	<summary>
	Creator:            Jacob McPherson
	Created:            02/18/2025
	Summary:            Stored Procedure For Inserting a New ExternalContact
	Last Updated By:    ???
	Last Updated:       ???
	What Was Changed:   ???  
	</summary> 
*/
PRINT '' PRINT '*** Creating Procedure sp_insert_external_contact'
GO
CREATE PROCEDURE [dbo].[sp_insert_external_contact]
(
    @ExternalContactTypeID [NVARCHAR](50),
	@ContactName [NVARCHAR](100),
	@GivenName [NVARCHAR](50),
	@FamilyName [NVARCHAR](50),
	@Email [NVARCHAR](250),
	@PhoneNumber [NVARCHAR](13),
	@JobTitle [NVARCHAR](50),
	@Address [NVARCHAR](100),
	@Description [NVARCHAR](250),
	@AddedBy [INT]
)
AS
	BEGIN
	
		INSERT INTO [dbo].[ExternalContact]
			([ExternalContactTypeID], [ContactName], [GivenName], [FamilyName], [Email], [PhoneNumber], [JobTitle], [Address], [Description], [AddedBy], [LastUpdatedBy])
		VALUES
			(@ExternalContactTypeID, @ContactName, @GivenName, @FamilyName, @Email, @PhoneNumber, @JobTitle, @Address, @Description, @AddedBy, @AddedBy)
	
	END
GO
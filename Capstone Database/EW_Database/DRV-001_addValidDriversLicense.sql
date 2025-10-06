/*
	<summary>
	Creator:	Ellie Wacker
	Created:	2025-02-26
	Summary:	Creation Script for addValidDriversLicense.
	Last Updated By:    Liam Easton
	Last Updated:       01/19/2023
	What Was Changed:   ???  
</summary> 
*/


IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'DRV-001_addValidDriversLicense')
BEGIN 
	DROP DATABASE [DRV-001_addValidDriversLicense]
END
GO

print '' print '*** creating database DRV-001_addValidDriversLicense'
GO 
CREATE DATABASE [DRV-001_addValidDriversLicense]
GO

print '' print '*** starting DRV-001_addValidDriversLicense'
print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-26
		Summary:	Creation Script for the DocumentType table.
	</summary>
*/
print '' print '*** Create Document Table ***'
GO
CREATE TABLE [dbo].[DocumentType](
	[DocumentTypeID]	[nvarchar](50)				NOT NULL,
	[Description]		[nvarchar](250)				NOT NULL DEFAULT '',
	CONSTRAINT [pk_documentTypeid] PRIMARY KEY ([DocumentTypeID])
)
GO

/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-26
		Summary:	Creation Script for the Document table.
	</summary>
*/
print '' print '*** Create Document Table ***'
GO
CREATE TABLE [dbo].[Document](
	[DocumentID]		[int]IDENTITY(100000, 1)	NOT NULL,
	[DocumentTypeID]	[nvarchar](50)				NOT NULL,
	[Uploader]			[int]						NOT NULL,
	[ReferenceID]		[nvarchar](17)				NOT NULL,
	[FileName]			[nvarchar](200)				NOT NULL,
	[FileType]			[nvarchar](50)  			NOT NULL,
	[Artifact]			[varbinary](MAX)			NULL,
	[Description]		[nvarchar](250)				NOT NULL DEFAULT '',
	CONSTRAINT [pk_documentid] PRIMARY KEY ([DocumentID]),
    CONSTRAINT [fk_documentTypeid] FOREIGN KEY ([DocumentTypeID])
        REFERENCES [DocumentType]([DocumentTypeID])
		 ON UPDATE CASCADE,
	CONSTRAINT [fk_uploader] FOREIGN KEY ([Uploader])
        REFERENCES [User]([UserID])
)
GO


/*
    <summary>
        Creator:   Ellie Wacker
        Created:   2025-02-26
        Summary:   Stored Procedure for inserting a document into the Document table.
    </summary>
*/
PRINT '' PRINT '*** Create Stored Procedure sp_insert_document ***'
GO

CREATE PROCEDURE [dbo].[sp_insert_document]
    (
        @DocumentTypeID   NVARCHAR(50),
        @ReferenceID      NVARCHAR(17),
        @FileName         NVARCHAR(200),
        @FileType         NVARCHAR(50),
        @Artifact         VARBINARY(MAX),
        @Uploader         INT,
        @Description      NVARCHAR(250) = ''  
    )
AS
	BEGIN
		INSERT INTO [dbo].[Document] (
			[DocumentTypeID],[ReferenceID],[FileName],[FileType],[Artifact],[Uploader],[Description]
		)
		VALUES (
			@DocumentTypeID,@ReferenceID,@FileName,@FileType,@Artifact,@Uploader,@Description
		);

	RETURN @@ROWCOUNT

	END
GO

/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-28
		Summary:	Stored Procedure for updating the vehicles active field
	</summary>
*/
print '' print '*** Create Stored Procedure sp_update_vehicle_active_by_vehicleID ***'
GO
CREATE PROCEDURE [dbo].[sp_update_vehicle_active_by_vehicleID]
	(
		@VehicleID	[nvarchar](17),
		@Active 	[bit]
	)
AS
	BEGIN
		UPDATE Vehicle
		SET [Active] = @Active
		WHERE [VehicleID] = @VehicleID
		RETURN @@ROWCOUNT
	END
GO

/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-03-06
		Summary:	Stored Procedure for selecting all user system roles by userID
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_user_system_roles_by_userID***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_system_roles_by_userID]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT [UserID], [SystemRoleID]
		FROM [UserSystemRole]
		WHERE [UserID] = @UserID
	END
GO



/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-03-05
		Summary:	Stored Procedure to Add a New User System Role to the SystemRole Table.
	</summary>
*/

print '' print '*** Create Stored Procedure to Add User System Role ***'
GO

CREATE PROCEDURE [dbo].[sp_insert_user_system_role]
	@UserID			[int],
    @SystemRoleID 	[nvarchar](50)
AS
BEGIN
    INSERT INTO [dbo].[UserSystemRole] ([UserID],[SystemRoleID])
    VALUES (@UserID, @SystemRoleID)
	 RETURN @@ROWCOUNT
END
GO


/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-03-01
		Summary:	Stored Procedure for selecting a list of Documents by UserID.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_documents_by_uploader ***'
GO
CREATE PROCEDURE [dbo].[sp_select_documents_by_uploader_and_file_type]
	(
		@Uploader		[int],
		@FileType 		[nvarchar](50)
	)
AS
	BEGIN
		SELECT 	[DocumentID], [DocumentTypeID],[ReferenceID],[FileName],[FileType],[Artifact],[Uploader],[Description]
		FROM [Document]
		WHERE [Uploader] = @Uploader AND [FileType] = @FileType
	END
GO
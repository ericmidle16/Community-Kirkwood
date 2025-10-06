print '' print '*** starting USER_003.sql file'
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/02
	Summary:  Creation of the USER-003 database sql
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO


/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/02
	Summary: Creation of the SystemRole table.
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/
print '' print '*** create SystemRole table'
GO
CREATE TABLE [SystemRole] (
	[SystemRoleID]		[nvarchar](50)	NOT NULL,
	[Description]		[nvarchar](250)	NOT NULL DEFAULT '',
	
	CONSTRAINT [pk_systemroleid] PRIMARY KEY([SystemRoleID] ASC)  
)
GO
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/02
	Summary: Creation of the UserSystemRole table.
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/
print '' print '*** create UserSystemRole table'
GO
CREATE TABLE [UserSystemRole] (
	[UserID]			[int] 			NOT NULL,
	[SystemRoleID]		[nvarchar](50)	NOT NULL,
	
	CONSTRAINT [pk_User_systemroleid] PRIMARY KEY([UserID],[SystemRoleID] ASC),
	CONSTRAINT [fk_User_systemrole_userid] FOREIGN KEY ([UserID])
		REFERENCES [User]([UserID]),
	CONSTRAINT [fk_user_systemrole_systemroleid] FOREIGN KEY ([SystemRoleID])
        REFERENCES [SystemRole]([SystemRoleID])
	
)
GO

/* ============================================
     End of Creating User Tables
     Beginning of User Stored Procedures
===============================================*/
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/02
	Summary: This is the stored procedure for 
			inserting a new user into the user table.
			The newly inserted user is given the user
			role and their password is stored.
	Last Updated By: Brodie Pasker
	Last Updated: 2025/04/30
	What Was Changed: Added Image and MimeType fields for the ability to add a default profile picture
	</summary>
*/
print '' print '*** creating stored procedure sp_insert_new_user'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_user]
	(
		@GivenName 		[nvarchar](50),
		@FamilyName 	[nvarchar](50),
		@PhoneNumber 	[nvarchar](11),
		@Email 			[nvarchar](250),
		@Password 		[nvarchar](250),
		@Image 			[varbinary](MAX),
    	@ImageMimeType [nvarchar](50)
	)
AS
BEGIN

    INSERT INTO [dbo].[User] 
		([GivenName], [FamilyName], [PhoneNumber], [Email], [PasswordHash], [Image], [ImageMimeType])
    VALUES 
		(@GivenName, @FamilyName, @PhoneNumber, @Email, @Password, @Image, @ImageMimeType); 

    DECLARE @NewUserID [int];
    SET @NewUserID = SCOPE_IDENTITY();

    INSERT INTO [dbo].[UserSystemRole] 
		([UserID], [SystemRoleID])
    VALUES 
		(@NewUserID, 'User');
	SELECT @@ROWCOUNT AS RowsAffected;
   
END
GO
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/02
	Summary: This is the stored procedure for selecting 
			users by email. This stored procedure will help
			ensure that emails are only used once.
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/
print '' print '*** creating stored procedure sp_verify_email_exists'
GO
CREATE PROCEDURE sp_verify_email_exists
    @Email [nvarchar](250)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 1 FROM [User] WHERE Email = @Email;
	
END
GO
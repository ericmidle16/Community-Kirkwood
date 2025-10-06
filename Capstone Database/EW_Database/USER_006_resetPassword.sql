/* 
	<summary>
		Creator:            Ellie Wacker
		Created:            2025-04-11
		Summary:            Creation Script for resetPassword
	</summary> 
*/

print '' print '*** starting USER-006_resetPassword'
print '' print '*** using database communityDb'
GO 
USE [communityDb]
GO                                                                       


/*
    <summary>
    Creator:            Ellie Wacker
    Created:            02/02/2025
    Summary:            This is the stored procedure for updating the password with the email.
    Last Updated By:    Updaters Name
    Last Updated:       01/31/2025
    What Was Changed:   Initial Creation
    </summary>
*/
print '' print '*** creating procedure sp_update_passwordhash_by_email'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordhash_by_email]
	(
		@Email				[nvarchar](250),
		@NewPasswordHash	[nvarchar](100),
		@OldPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE  [User]
		SET		[PasswordHash] = @NewPasswordHash
		WHERE	[PasswordHash] = @OldPasswordHash
			AND	[Email] = @Email
			
		RETURN @@ROWCOUNT
	END
GO
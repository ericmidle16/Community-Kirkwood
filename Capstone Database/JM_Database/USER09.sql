print '' print '*** using database communityDb'
GO
USE [communityDb]
GO
print '' print '*** USER09'
GO


/* 
	<summary>
	Creator:            Jacob McPherson
	Created:            02/11/2025
	Summary:            Stored Procedure For Deactivating a User
	Last Updated By:    ???
	Last Updated:       ???
	What Was Changed:   ???  
	</summary> 
*/
PRINT '' PRINT '*** Creating Procedure sp_deactivate_user'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_user]
(
	@Email NVARCHAR(255),
	@PasswordHash NVARCHAR(100)
)
AS
	BEGIN
	
		UPDATE [dbo].[User]
		SET [Active] = 0
		WHERE [Email] = @Email
		AND [PasswordHash] = @PasswordHash
	
	END
GO
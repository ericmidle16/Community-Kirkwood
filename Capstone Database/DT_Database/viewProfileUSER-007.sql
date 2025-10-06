/*PRINT '' PRINT '*** creating database ViewProfile ***'
GO
CREATE DATABASE [ViewProfile]
GO:*/

print '' print '*** using database communityDB ***'
GO
USE [communityDB]
GO

print '' print '***starting viewProfileUSER-007.sql***'
GO

/*
///<summary>
/// Creator: Dat Tran
/// Created: 2025-02-05
/// Summary: This stored procedure grabs the information from the one User
that has the parameters listed in the procedure below. 
/// Last updated by:
/// Last updated: 
/// Changes:
/// </summary>
*/
print '' print '*** creating sp_select_profile_info'
GO
CREATE PROCEDURE [dbo].[sp_select_profile_info]
	(
	
	
		
		@Email			[nvarchar](250)
		
	)
AS
	BEGIN
		SELECT
			[GivenName], [FamilyName], [City], [State], [Email]
		FROM [dbo].[User]
	
		WHERE [Email] = @Email
	END
GO
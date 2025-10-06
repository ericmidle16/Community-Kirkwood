print '' print '*** using database communityDb'
GO
USE [communityDb]
GO
print '' print '*** ADM07'
GO

/* 
	<summary>
	Creator:            Jacob McPherson
	Created:            02/03/2025
	Summary:            Stored Procedure For Retreiving All Users
	Last Updated By:    ???
	Last Updated:       ???
	What Was Changed:   ???  
	</summary> 
*/
PRINT '' PRINT '*** Creating Procedure sp_select_all_users'
GO
CREATE PROCEDURE [dbo].[sp_select_all_users]
AS
	BEGIN
	
		SELECT
			[UserID],
			[GivenName],
			[FamilyName],
			[PhoneNumber],
			[Email],
			[City],
			[State],
			[Image],
			[ReactivationDate],
			[Suspended],
			[ReadOnly],
			[Active],
			[RestrictionDetails]
		FROM [dbo].[User]
	
	END
GO
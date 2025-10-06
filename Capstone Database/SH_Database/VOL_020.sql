print '' print '*** stating VOL_020.sql file'
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/23
	Summary:  Creation of the VOL_020 database sql
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/

print '' print '*** using database user'
GO
USE [communityDb]
GO


/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/23
	Summary: This stored procedure retrieves availability details 
	         for a given UserID from the Availability table.
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/

print '' print '***creating stored procedure sp_select_availability_by_user'
GO
CREATE PROCEDURE [dbo].[sp_select_availability_by_user]
	(
		@UserID [int]
	)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [AvailabilityID], 
        [UserID], 
        [IsAvailable], 
        [RepeatWeekly], 
        [StartDate], 
        [EndDate]
    FROM [dbo].[Availability]
    WHERE [UserID] = @UserID;
END
GO
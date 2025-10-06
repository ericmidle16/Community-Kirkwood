print '' print '*** starting VOL_003.sql file'
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/10
	Summary:  Creation of the VOL_003 database sql
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
	Created: 2025/02/02
	Summary:  Creation of the Availability table
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/
print '' print '***creating availability table'
GO
CREATE TABLE [dbo].[Availability] (
    [AvailabilityID] [int] IDENTITY(100000, 1) UNIQUE NOT NULL,  
    [UserID] [int] NOT NULL,  
    [IsAvailable] [bit] DEFAULT 0 NOT NULL,  
    [RepeatWeekly] [bit] DEFAULT 0 NOT NULL, 
    [StartDate] [DateTime] NOT NULL, 
    [EndDate] [DateTime] NOT NULL, 
    CONSTRAINT [pk_availabilityID] PRIMARY KEY([AvailabilityID] ASC),  
    CONSTRAINT [fk_userID] FOREIGN KEY([UserID]) REFERENCES [dbo].[User]([UserID])  
)
GO

/* ============================================
     End of Creating Availability Tables
     Beginning of Creating Stored Procedures 
===============================================*/
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/10
	Summary: This is the stored procedure for Inserting
			user avaiability into the database.
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/
print ''print '***creating stored procedure sp_insert_availability'
GO
CREATE PROCEDURE [dbo].[sp_insert_availability]
	(
		@UserID [int],               
		@IsAvailable [bit],          
		@RepeatWeekly [bit],         
		@StartDate [DateTime],       
		@EndDate [DateTime]         
	)
AS
BEGIN
    INSERT INTO [dbo].[Availability] 
	([UserID], [IsAvailable], [RepeatWeekly], [StartDate], [EndDate])
    VALUES (@UserID, @IsAvailable, @RepeatWeekly, @StartDate, @EndDate);
	
	 SELECT @@ROWCOUNT AS RowsAffected;
END
GO
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/10
	Summary: This stored procedure checks if a given user has existing availability 
         for the specified start and end dates in the Availability table.
		 If a matching record exists, the procedure returns `1`; otherwise, it returns `0`.
	Last Updated By:
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/
print ''print '***creating stored procedure sp_select_existing_availability'
GO
CREATE PROCEDURE [dbo].[sp_select_existing_availability]
	(
		@UserID [int],
		@StartDate [DateTime],
		@EndDate [DateTime]
	)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1 FROM [Availability] 
        WHERE [UserID] = @UserID 
        AND [StartDate] = @StartDate 
        AND [EndDate] = @EndDate
    )
    BEGIN
        SELECT 1;
    END
    ELSE
    BEGIN
        SELECT 0;
    END
END
GO
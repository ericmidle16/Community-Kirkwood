/*
	<summary>
	Creator:			Jackson Manternach
	Created: 			02/07/2025
	Summary:			Stored Procedure to enter in a row of data to the Event Table
	Last Updated By:	Jackson Manternach
	Last Updated:		02/07/2025
	What Was Changed:	Initial Creation
	</summary>
*/
GO
USE [communityDb]
GO

print '' print '*** creating sp_insert_event'
GO
CREATE PROCEDURE [dbo].[sp_insert_event]
(
    @EventTypeID        [nvarchar](50),
    @ProjectID    		[int],
    @StartDate			[datetime],
	@EndDate			[datetime],
	@Name				[nvarchar](50),
	@LocationID			[int],
	@VolunteersNeeded	[int],
	@UserID				[int],
	@Notes				[nvarchar](250),
	@Description		[nvarchar](250)
)
AS
BEGIN
    INSERT INTO [Event]
        ([EventTypeID], [ProjectID], [StartDate], [EndDate], [Name], [LocationID],
			[VolunteersNeeded], [UserID], [Notes], [Description])
    VALUES
        (@EventTypeID, @ProjectID, @StartDate, @EndDate, @Name, @LocationID,
			@VolunteersNeeded, @UserID, @Notes, @Description)
    RETURN @@ROWCOUNT
END
GO

/*
	<summary>
	Creator:			Jackson Manternach
	Created: 			02/07/2025
	Summary:			Stored Procedure to select all rows in the EventType table
	Last Updated By:	Jackson Manternach
	Last Updated:		02/07/2025
	What Was Changed:	Initial Creation
	</summary>
*/

print '' print '*** creating sp_select_all_eventtypes'
GO
CREATE PROCEDURE [dbo].[sp_select_all_eventtypes]
AS
	BEGIN
		SELECT  [EventTypeID]
		FROM 	[EventType]
END
GO
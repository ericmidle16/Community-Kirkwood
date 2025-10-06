GO
USE [communityDB]
GO

/*
    <summary>
    Creator:            Jackson Manternach
    Created:             02/14/2025
    Summary:            Stored Procedure to update a row in the Availability table
    Last Updated By:    Jackson Manternach
    Last Updated:        02/14/2025
    What Was Changed:    Initial Creation
    </summary>
*/

print '' print '*** creating sp_update_availability_by_id'
GO
CREATE PROCEDURE [dbo].[sp_update_availability_by_id]
(
    @AvailabilityID		[int],
    @UserID             [int],
    @IsAvailable        [bit],	
    @RepeatWeekly      	[bit],
    @StartDate          [DateTime],
    @EndDate            [DateTime]
)
AS
BEGIN
    UPDATE 	[dbo].[Availability]
    SET    	[UserID] = @UserID,
			[IsAvailable] = @IsAvailable,
			[RepeatWeekly] = @RepeatWeekly,
			[StartDate] = @StartDate,
			[EndDate] = @EndDate
    WHERE 	[AvailabilityID] = @AvailabilityID
    
    RETURN @@ROWCOUNT
END
GO


/*
    <summary>
    Creator:            Jackson Manternach
    Created:             02/14/2025
    Summary:            Stored Procedure to select availabilities by date range for a user
    Last Updated By:    Jackson Manternach
    Last Updated:        02/14/2025
    What Was Changed:    Initial Creation
    </summary>
*/

print '' print '*** creating sp_select_availabilities_by_date_range'
GO
CREATE PROCEDURE [dbo].[sp_select_availabilities_by_date_range]
(
    @UserID            [int],
    @StartDate        [DateTime],
    @EndDate        [DateTime]
)
AS
BEGIN
    SELECT     [AvailabilityID],
            [UserID],
            [IsAvailable],
            [RepeatWeekly],
            [StartDate],
            [EndDate]
    FROM     [Availability]
    WHERE     [UserID] = @UserID
    AND        [StartDate] >= @StartDate
    AND        [EndDate] <= @EndDate
END
GO

/*
    <summary>
    Creator:            Jackson Manternach
    Created:             03/07/2025
    Summary:            Stored Procedure to delete availabilities by availability id
    Last Updated By:    Jackson Manternach
    Last Updated:        03/07/2025
    What Was Changed:    Initial Creation
    </summary>
*/

print '' print '*** creating sp_delete_availability_by_user_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_availability_by_availability_id]
(
	@AvailabilityID			[int]
)
AS
BEGIN
	DELETE FROM Availability WHERE [AvailabilityID] = @AvailabilityID
END
GO	
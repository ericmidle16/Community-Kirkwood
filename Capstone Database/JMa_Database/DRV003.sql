print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

print '' print '*** creating sp_update_vehicle_by_id'
GO
CREATE PROCEDURE [dbo].[sp_update_vehicle_by_id]
(
    @VehicleID          [nvarchar](17),
    @UserID             [int],
    @Active             [bit],
    @Color              [nvarchar](20),
    @Year               [int],
    @LicensePlateNumber [nvarchar](7),
    @InsuranceStatus    [bit],
    @Make               [nvarchar](50),
    @Model              [nvarchar](50),
    @NumberOfSeats      [int],
    @TransportUtility   [nvarchar](500)
)
AS
BEGIN
    UPDATE [dbo].[Vehicle]
    SET    [UserID] = @UserID,
           [Active] = @Active,
           [Color] = @Color,
           [Year] = @Year,
           [LicensePlateNumber] = @LicensePlateNumber,
           [InsuranceStatus] = @InsuranceStatus,
           [Make] = @Make,
           [Model] = @Model,
           [NumberOfSeats] = @NumberOfSeats,
           [TransportUtility] = @TransportUtility
    WHERE  [VehicleID] = @VehicleID
    
    RETURN @@ROWCOUNT
END
GO
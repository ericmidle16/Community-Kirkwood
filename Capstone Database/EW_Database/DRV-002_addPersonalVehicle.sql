/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-14
		Summary:	Creation Script for addVehicle.
		Last Updated By:    Liam Easton
		Last Updated:       01/19/2023
		What Was Changed:   ???  
	</summary> 
*/

print '' print '*** starting DRV-001_addValidDriversLicense'
print '' print '*** using database communityDb'
GO
USE [communityDb]
GO



/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-14
		Summary:	Creation Script for the Vehicle table.
	</summary>
*/
print '' print '*** Create Vehicle Table ***'
GO
CREATE TABLE [dbo].[Vehicle](
	[VehicleID]				[nvarchar](17)	NOT NULL,
	[UserID]				[int]	    	NOT NULL,
	[Active]				[bit]			NULL DEFAULT 0,
	[Color]					[nvarchar](20)	NOT NULL DEFAULT '',
	[Year]					[int]			NOT NULL,
	[LicensePlateNumber]	[nvarchar](7)	NOT NULL,
	[InsuranceStatus]		[bit]			NOT NULL DEFAULT 1,
	[Make]					[nvarchar](50)	NOT NULL,
	[Model]					[nvarchar](50)	NOT NULL,
	[NumberOfSeats]			[int]			NOT NULL,
	[TransportUtility]		[nvarchar](500)	NOT NULL,
	
	CONSTRAINT [pk_vehicleid] PRIMARY KEY([VehicleID]),
	CONSTRAINT [fk_vehicle_userid] FOREIGN KEY([UserID])
		REFERENCES [User]([UserID])
)



/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-16
		Summary:	Stored Procedure for inserting a vehicle 
	</summary>
*/
PRINT '*** Create Stored Procedure sp_insert_vehicle ***'
GO

CREATE PROCEDURE [dbo].[sp_insert_vehicle]
    (
        @VehicleID            NVARCHAR(17),
        @UserID               INT,
        @Active               BIT,
        @Color                NVARCHAR(20),
        @Year                 INT,
        @LicensePlateNumber   NVARCHAR(7),
        @InsuranceStatus      BIT,
        @Make                 NVARCHAR(50),
        @Model                NVARCHAR(50),
        @NumberOfSeats        INT,
        @TransportUtility     NVARCHAR(500)
    )
AS
BEGIN
    INSERT INTO Vehicle (VehicleID, UserID, Active, Color, Year, LicensePlateNumber, InsuranceStatus, Make, Model, NumberOfSeats, TransportUtility)
    VALUES (@VehicleID, @UserID, @Active, @Color, @Year, @LicensePlateNumber, @InsuranceStatus, @Make, @Model, @NumberOfSeats, @TransportUtility)

    RETURN @@ROWCOUNT
END
GO


/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-16
		Summary:	Stored Procedure for selecting all vehicles 
	</summary>
*/
print '' print '*** creating procedure sp_select_all_vehicles'
GO
CREATE PROCEDURE [dbo].[sp_select_all_vehicles]
AS
	BEGIN
		SELECT [VehicleID], [UserID], [Active], [Color], [Year], [LicensePlateNumber], [InsuranceStatus], [Make], [Model], [NumberOfSeats], [TransportUtility]
		FROM	[Vehicle]  
	END
GO

/*
	<summary>
		Creator:	Jennifer Nicewanner
		Created:	2025-04-24
		Summary:	Stored Procedure for selecting a list of ACTIVE vehicles by UserID.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_active_vehicles_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_vehicles_by_userID]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT [VehicleID], [User].[UserID], [Vehicle].[Active], [Color], [Year], [LicensePlateNumber], [InsuranceStatus], [Make], [Model], [NumberOfSeats], [TransportUtility]
		FROM [Vehicle] INNER JOIN [User]
			ON [Vehicle].[UserID] = [User].[UserID]
		WHERE [Vehicle].[UserID] = @UserID
		AND [Vehicle].[Active] = 1
	END
GO
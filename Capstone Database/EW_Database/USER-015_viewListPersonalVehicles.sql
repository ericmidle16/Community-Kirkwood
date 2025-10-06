/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-21
		Summary:	Scipt for selecting a list of vehicles by UserID.
		Last Updated By:    Liam Easton
		Last Updated:       01/19/2023
		What Was Changed:   ???  
	</summary> 
*/

print '' print '*** using database communityDb'
print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	<summary>
		Creator:	Ellie Wacker
		Created:	2025-02-21
		Summary:	Stored Procedure for selecting a list of vehicles by UserID.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_vehicles_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_vehicles_by_userID]
	(
		@UserID		[int]
	)
AS
	BEGIN
		SELECT [VehicleID], [UserID], [Active], [Color], [Year], [LicensePlateNumber], [InsuranceStatus], [Make], [Model], [NumberOfSeats], [TransportUtility]
		FROM [Vehicle]
		WHERE [UserID] = @UserID
	END
GO
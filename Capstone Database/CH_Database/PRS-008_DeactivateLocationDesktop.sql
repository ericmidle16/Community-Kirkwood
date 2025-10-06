/*
Creator:	Chase Hannen
Created:	2025/04/24
Summary:	SQL script for Deactivate Location
Last Updated By:
			Chase Hannen
Last Updated:
What was Changed:
*/

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

print '' print '*** creating sp_select_all_active_projects ***'
GO
CREATE PROCEDURE [sp_select_all_active_projects]
AS
	BEGIN
		SELECT	[LocationID], [Name], [Address], [City], [State], [Zip], [Country], [Description]
		FROM	[Location]
		WHERE	[Active] = 1
	END
GO

print '' print '*** creating sp_deactivate_location_by_locationID ***'
GO
CREATE PROCEDURE [sp_deactivate_location_by_locationID] (
	@LocationID	[int]
)
AS
	BEGIN
		UPDATE	[Location]
		SET		[Active] = 0
		WHERE 	[LocationID] = @LocationID
		
		RETURN @@ROWCOUNT;
	END
GO
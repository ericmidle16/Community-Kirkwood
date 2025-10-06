print '' print ' *** Starting  PRS-012_ViewSingleLocation.sql *** '
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/***********************start of view a single location records************************/
/*
    Creator: Jennifer Nicewanner
    Summary: This is a stored procedure to display a single location from the database
    Last Updated By: Jennifer Nicewanner
    Last Updated: 2025-02-21
    What Was Changed:  Initial Creation
*/

print '' print '*** Creating procedure sp_select_location_by_locationID'
GO
CREATE PROCEDURE [dbo].[sp_select_location_by_locationID]
(
	@LocationID		[int]
)
AS
	BEGIN
		SELECT 	[Name], [Address], [City], [State], [Zip], [Country], [Image], [ImageMimeType], [Description]
		FROM 	[Location]
		WHERE	[LocationID] = @LocationID
	END
GO
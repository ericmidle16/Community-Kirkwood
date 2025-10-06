print '' print '*** starting PRS-10ViewLocations.sql'
GO
USE [communityDb]
GO

/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/04
/// Summary:  Creating Location table, sp_view_location_list, and Location data.
/// Last Updated By: Stan Anderson
/// Last Updated: 2025/02/07
/// What was Changed: Added comments. Changed LocationID into int and made Name field for Location Table.
/// </summary>
*/


print '' print '*** creating sp_view_location_list'
GO
CREATE PROCEDURE [sp_view_location_list]
AS
	BEGIN
		SELECT	[LocationID],[Name],[Address],[City],[State],[ZIP],[Country],[Description]
		FROM	[Location]
	END
GO
print '' print '*** Starting BGC-008_viewListOfBackgroundChecksDesktop.sql ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-20
		Summary:	Stored Procedure for selecting BackgroundCheck records that
					are associated with a project.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_select_backgroundChecks_by_projectID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_backgroundChecks_by_projectID]
	(
		@ProjectID		[int]
	)
AS
	BEGIN
		SELECT [BackgroundCheckID], [Investigator], u1.[GivenName], u1.[FamilyName], [BackgroundCheck].[UserID], u2.[GivenName], u2.[FamilyName], [BackgroundCheck].[ProjectID], [Project].[Name], [BackgroundCheck].[Status], [BackgroundCheck].[Description]
		FROM [BackgroundCheck]
			JOIN [Project] ON [BackgroundCheck].[ProjectID] = [Project].[ProjectID]
				JOIN [User] u1 ON [BackgroundCheck].[Investigator] = u1.[UserID]
					JOIN [User] u2 ON [BackgroundCheck].[UserID] = u2.[UserID]
		WHERE [BackgroundCheck].[ProjectID] = @ProjectID
	END
GO
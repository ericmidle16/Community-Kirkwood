print '' print '*** Starting VLD-017_ViewVolunteerList.sql ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
    Creator: Jennifer Nicewanner
    Summary: This is a stored procedure to create a list of all approved users for a project
    Last Updated By: Jennifer Nicewanner
    Last Updated: 2025-02-21
    What Was Changed:  Initial Creation
	Last Updated By: Kate Rich
	Last Updated: 2025-03-11
	What Was Changed:
		Removed @Approved parameter & set it to 1 in the WHERE clause.
*/


print '' print '*** Creating proceedure sp_select_approved_user_by_projectid'
GO
CREATE PROCEDURE [dbo].[sp_select_approved_user_by_projectid]
    (
        @ProjectID            [int]
    )
AS
    BEGIN
        SELECT [User].[UserID], [User].[GivenName], [User].[FamilyName], [User].[PhoneNumber], [User].[Email]
        FROM [Project]
		INNER JOIN [VolunteerStatus]
        ON [VolunteerStatus].[ProjectID] = [Project].[ProjectID]
		INNER JOIN [User]
        ON [VolunteerStatus].[UserID] = [User].[UserID]
        WHERE        [VolunteerStatus].[Approved] = 1 
		AND			[Project].[ProjectID] = @ProjectID
        ORDER BY    [UserID]
    END
GO


/*********************** end of view a list of approved volunteers in a project**********************/
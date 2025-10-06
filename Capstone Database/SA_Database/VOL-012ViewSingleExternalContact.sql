/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/28
/// Summary:  View single external contact
/// Last Updated By: 
/// Last Updated: 
/// What was Changed: 
/// </summary>
*/

print '' print '*** starting VOL-012ViewSingleExternalContact.sql'
GO
USE [communityDb]
GO

DROP PROCEDURE IF EXISTS [sp_view_single_external_contact];
GO
/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/28
/// Summary:  View a single external contacts
/// Last Updated By: 
/// Last Updated: 
/// What was Changed: 
/// </summary>
*/
PRINT '' PRINT '*** creating sp_single_all_external_contact'
GO
CREATE PROCEDURE [sp_view_single_external_contact] (
	@ExternalContactID		[int]
)
AS
	BEGIN
		SELECT	[ExternalContactType].[ExternalContactTypeID],[ExternalContactType].[Description],[ContactName],[FamilyName],[GivenName],[Email],[PhoneNumber],[JobTitle],[Address],[ExternalContact].[Description]
		FROM	[ExternalContact] JOIN [ExternalContactType]
		ON		[ExternalContact].[ExternalContactTypeID] = [ExternalContactType].[ExternalContactTypeID]
		WHERE	[ExternalContactID] = @ExternalContactID
			AND [Active] = 1
	END
GO
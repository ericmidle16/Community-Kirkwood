/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/24
/// Summary:  
/// Last Updated By: 
/// Last Updated: 
/// What was Changed: 
/// </summary>
*/

print '' print '*** starting VOL-013ViewAllExternalContacts.sql'
GO
USE [communityDb]
GO




/*
/// <summary>
/// Creator:  Stan Anderson
/// Created:  2025/02/24
/// Summary:  View all of the external contacts
/// Last Updated By: 
/// Last Updated: 
/// What was Changed: 
/// </summary>
*/
PRINT '' PRINT '*** creating sp_view_all_external_contacts'
GO
CREATE PROCEDURE [sp_view_all_external_contacts]
AS
	BEGIN
		SELECT [ExternalContactID],[ExternalContactTypeID],[ContactName],[FamilyName],[GivenName],[Email],[PhoneNumber],[JobTitle],[Active]
		FROM [ExternalContact]
		WHERE [Active] = 1
	END
GO




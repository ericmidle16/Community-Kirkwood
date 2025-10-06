print '' print '*** using database communityDb'
GO
USE [communityDb]
GO
print '' print '*** VOL14'
GO


/* 
	<summary>
	Creator:            Jacob McPherson
	Created:            03/11/2025
	Summary:            Stored Procedure For Deactivating an ExternalContact
	Last Updated By:    ???
	Last Updated:       ???
	What Was Changed:	Initial creation.  
	</summary> 
*/
PRINT '' PRINT '*** Creating Procedure sp_deactivate_external_contact'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_external_contact]
(
    @ExternalContactID [INT]
)
AS
	BEGIN
	
		UPDATE [dbo].[ExternalContact]
		SET [Active] = 0
		WHERE [ExternalContactID] = @ExternalContactID
			
	END
GO
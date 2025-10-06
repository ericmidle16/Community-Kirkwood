print '' print '*** using database communityDb'
GO
USE [communityDb]
GO
print '' print '*** VOL10'
GO


/* 
	<summary>
	Creator:            Jacob McPherson
	Created:            02/25/2025
	Summary:            Stored Procedure For Inserting a New ExternalContactType
	Last Updated By:    ???
	Last Updated:       ???
	What Was Changed:   ???  
	</summary> 
*/
PRINT '' PRINT '*** Creating Procedure sp_insert_external_contact_type'
GO
CREATE PROCEDURE [dbo].[sp_insert_external_contact_type]
(
    @ExternalContactTypeID [NVARCHAR](50),
	@Description [NVARCHAR](250)
)
AS
	BEGIN
	
		INSERT INTO [dbo].[ExternalContactType]
			([ExternalContactTypeID], [Description])
		VALUES
			(@ExternalContactTypeID, @Description)
	
	END
GO
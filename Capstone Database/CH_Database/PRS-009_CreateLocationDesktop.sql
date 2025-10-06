/*
Creator:	Chase Hannen
Created:	2025/02/02
Summary:	SQL script for Create Location
Last Updated By:
			Chase Hannen
Last Updated:
			2025/03/14
What was Changed:
			Changed file name to match standards
*/

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

print '' print '*** creating sp_insert_location ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_location]
	(
		@Name			[nvarchar](50),
		@Address		[nvarchar](100),
		@City			[nvarchar](100),
		@State			[nvarchar](20),
		@Zip			[nvarchar](10),
		@Country		[nvarchar](100),
		@Image			[varbinary](MAX),
		@ImageMimeType	[nvarchar](50),
		@Description	[nvarchar](250)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Location]
			([Name], [Address], [City], [State], [Zip], 
			 [Country], [Image], [ImageMimeType], [Description])
		VALUES
			(@Name, @Address, @City, @State, @Zip, 
			 @Country, @Image, @ImageMimeType, @Description)
	
		SELECT 	SCOPE_IDENTITY()
	END
GO
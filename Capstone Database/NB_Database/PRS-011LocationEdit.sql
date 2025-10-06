-- <summary>
-- Creator: Nik Bell
-- Created: 2025/02/18
-- Summary: Creation for Location table
-- Last Updated By: Chase Hannen
-- Last Updated: 2025/04/25
-- What was Changed: Changed sp_update_location_by_locationID
-- </summary>

print '' print '*** Starting PRS-011LocationEdit.sql ***'
GO
USE [communityDb]
GO

-- <summary>
-- Creator: Nik Bell
-- Created: 2025/02/18
-- Summary: Creation of sp_update_location_by_locationID

-- Last Updated By: Chase Hannen
-- Last Updated: 2025/04/24
-- What was Changed: Added Image and ImageMimeType
-- </summary>

PRINT '' PRINT '*** Creating Procedure: sp_update_location_by_locationID ***'
GO

CREATE PROCEDURE sp_update_location_by_locationID
	@LocationID INT,
	@Name NVARCHAR(50),
	@Address NVARCHAR(100),
	@City NVARCHAR(100),
	@State NVARCHAR(20),
	@Zip NVARCHAR(10),
	@Country NVARCHAR(100),
	@Image VARBINARY(MAX) NULL,
	@ImageMimeType NVARCHAR(50) NULL,
	@Description NVARCHAR(250)
AS
BEGIN
	UPDATE Location
	SET 
		Name = @Name,
		Address = @Address,
		City = @City,
		State = @State,
		Zip = @Zip,
		Country = @Country,
		Image = @Image,
		ImageMimeType = @ImageMimeType,
		Description = @Description
	WHERE LocationID = @LocationID;

	RETURN @@ROWCOUNT;
END
GO
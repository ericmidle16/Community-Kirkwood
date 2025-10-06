print '' print '*** starting database USER_08'
/*
    <summary>
    Creator: Brodie Pasker
    Created: 2025/02/07
    Summary: Creation of the USER_01 database.sql.
    Last Updated By:
    Last Updated:
    What Was Changed:
    </summary>
*/

print '' print '***  using community database communityDb'
GO
USE [communityDb]
GO

/*
    <summary>
    Creator: Brodie Pasker
    Created: 2025/02/07
    Summary: Creation of the sp_update_by_userID.
    Last Updated By: Stan Anderson
    Last Updated: 2025/04/09
    What Was Changed: Fixed the issue where if the user had no city or state, it wouldn't work
    </summary>
*/
PRINT '$$$ creating procedure sp_update_user_by_UserID $$$'
GO
CREATE PROCEDURE [dbo].[sp_update_user_by_UserID](
    @UserID INT,
    @OldGivenName [nvarchar](50),
    @OldFamilyName [nvarchar](50),
    @OldBiography [nvarchar](250),
    @OldPhoneNumber [nvarchar](13),
    @OldEmail [nvarchar](250),
    @OldCity [nvarchar](50),
    @OldState [nvarchar](50),
    @OldImage [varbinary](MAX),
    @OldImageMimeType [nvarchar](50),
    @NewGivenName [nvarchar](50),
    @NewFamilyName [nvarchar](50),
    @NewBiography [nvarchar](250),
    @NewPhoneNumber [nvarchar](13),
    @NewEmail [nvarchar](250),
    @NewCity [nvarchar](50),
    @NewState [nvarchar](50),
    @NewImage [varbinary](MAX),
    @NewImageMimeType [nvarchar](50)
)
AS
BEGIN
    -- Check if the old record exists
    IF NOT EXISTS (
        SELECT 1 
        FROM [User] 
        WHERE 
            [UserID] = @UserID
            AND [GivenName] = @OldGivenName
            AND [FamilyName] = @OldFamilyName
            AND [Biography] = @OldBiography
            AND [PhoneNumber] = @OldPhoneNumber
            AND [Email] = @OldEmail
    )
    BEGIN
        -- Raise an exception if the old record does not exist
        THROW 50000, 'The old record does not exist.', 1;
    END

    -- If the old record exists, proceed with the update
    UPDATE [User]
    SET 
        [GivenName] = @NewGivenName,
        [FamilyName] = @NewFamilyName,
        [Biography] = @NewBiography,
        [PhoneNumber] = @NewPhoneNumber,
        [Email] = @NewEmail,
        [City] = @NewCity,
        [State] = @NewState,
        [Image] = @NewImage,
        [ImageMimeType] = @NewImageMimeType
    WHERE 
        [UserID] = @UserID
        AND [GivenName] = @OldGivenName
        AND [FamilyName] = @OldFamilyName
        AND [Biography] = @OldBiography
        AND [PhoneNumber] = @OldPhoneNumber
        AND [Email] = @OldEmail
        AND ([City] = @OldCity OR ([City] IS NULL AND @OldCity IS NULL))
        AND ([State] = @OldState OR ([State] IS NULL AND @OldState IS NULL))

    -- Return the number of rows affected
    SELECT @@ROWCOUNT;
END
GO
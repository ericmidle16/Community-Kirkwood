print '' print '*** starting database USER_017 view notifications ***'
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


print '' print '*** using database communityDb ***'
GO
USE [communityDb]
GO


print '' print '*** creating sp_get_all_notifications_by_user_id ***'
GO
CREATE PROCEDURE [dbo].[sp_get_all_notifications_by_user_id]
(
    @UserID [int]
)
AS
BEGIN
    SELECT 
        [Notification].[NotificationID], 
        [Notification].[Name], 
        [Notification].[Sender], 
        [User].[GivenName], 
        [User].[FamilyName], 
        [Notification].[Receiver], 
        [Notification].[Important], 
        [Notification].[IsViewed], 
        [Notification].[Date], 
        [Notification].[Content]
    FROM [Notification]
    JOIN [User] ON [Notification].[Sender] = [User].[UserID]
    WHERE [Receiver] = @UserID
    AND [StartDate] <= GETDATE()
    AND [EndDate] >= GETDATE()
    ORDER BY [Date] DESC
END
GO

print '' print '*** creating sp_update_notification_to_viewed_by_notification_id ***'
GO
CREATE PROCEDURE [dbo].[sp_update_notification_to_viewed_by_notification_id]
(
    @NotificationID [int]
)
AS
BEGIN
    UPDATE [Notification]
    SET [IsViewed] = 1
    WHERE [NotificationID] = @NotificationID
    SELECT @@ROWCOUNT
END
GO
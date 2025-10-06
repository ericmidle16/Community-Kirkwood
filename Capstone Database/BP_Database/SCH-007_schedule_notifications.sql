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

print '' print '*** creating sp_select_notifications_by_sender_userid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_notifications_by_sender_userid]
(
    @UserID [int]
)
AS
BEGIN
    SELECT 
        [Notification].[NotificationID], 
        [Notification].[Name], 
        [Notification].[Sender],
        [Notification].[Receiver], 
        [Notification].[Important],
        [Notification].[Content],
        [Notification].[Date],
        [Notification].[StartDate],
        [Notification].[EndDate]
    FROM [Notification]
    WHERE [Sender] = @UserID
    ORDER BY [Date] DESC
END
GO


-- https://techcommunity.microsoft.com/blog/sqlserver/passing-arrays-to-t-sql-procedures-as-json/384438
print '' print '*** creating sp_insert_scheduled_notification ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_scheduled_notification]
(
    @Name [nvarchar](50),
    @Sender [int],
    @Receivers [nvarchar](max),
    @Important [bit],
    @Content [nvarchar](500),
    @StartDate [DateTime],
    @EndDate [DateTime]
)
AS
BEGIN
    SET NOCOUNT ON; -- No excess "(x rows affected) only returns rows affect with select at bottom (@@ROWCOUNT)"


    INSERT INTO [dbo].[Notification]
    (
        [Name],
        [Sender],
        [Receiver],
        [Important],
        [Content],
        [StartDate],
        [EndDate])
    SELECT
        @Name,
        @Sender,
        CAST([value] AS INT), -- each receiver ID from the JSON array
        @Important,
        @Content,
        @StartDate,
        @EndDate
    FROM OPENJSON(@Receivers) -- Receivers should be a JSON array like '[100000, 100001, 100002, 100003, 100006, 100014, 100017, etc...]'
    
    -- return number of rows inserted
    SELECT @@ROWCOUNT AS RowsAffected
END
GO
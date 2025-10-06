print '' print '** Starting PRS-007_requestVolunteer.sql'
GO

use [communityDb]
go


/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Stored Procedure for selecting a volunteers information by projectID.
	</summary>
*/

print '' print '*** create procedure sp_select_all_volunteers_by_projectId'
Go
CREATE PROCEDURE [dbo].[sp_select_all_volunteers_by_projectid](
    @ProjectID    [int]
   
)
AS
    BEGIN
        SELECT [ProjectID], [UserID], [Approved]
        FROM [VolunteerStatus]
        Where [ProjectID] = @ProjectID

    END
Go

/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Stored Procedure for selecting a volunteers information by projectID and UserID.
	</summary>
*/
print '' print '*** Creating procedure sp_insert_volunteer'

Go
CREATE PROCEDURE [dbo].[sp_insert_volunteer]
(
    @UserID    [int],
    @ProjectID [int]
    
)
AS
    BEGIN
        INSERT INTO [dbo].[VolunteerStatus]
                ([UserID], [ProjectID])
        VALUES
            ( @UserID, @ProjectID) 
        SELECT SCOPE_IDENTITY()   
    END
GO


/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Creating user Notification table
	</summary>
*/
print '' print '*** create notification database table'
Go
CREATE TABLE [dbo].[Notification] (
    [NotificationID]    [int] IDENTITY(100000, 1)	NOT NULL,
    [Name]   [nvarchar] (50) NOT NULL,
    [Sender]  [int] NULL,
    [Receiver] [int] NOT NULL,
    [Important]       [bit] NOT NULL DEFAULT 0,
    [IsViewed]        [bit] NOT NULL DEFAULT 0,
    [Date]       [DateTime] NOT null DEFAULT GETDATE(),
    [Content]      [nvarchar] (500)  null,
    [StartDate]    [DateTime] NOT NULL DEFAULT GETDATE(),
    [EndDate]      [DateTime] NOT NULL DEFAULT DATEADD(DAY,  14, GETDATE()),
    CONSTRAINT   [pk_NotificationID] PRIMARY KEY ([NotificationID] ASC),
           
)
Go

/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Stored Procedure for inserting into a notification table.
	</summary>
*/

print '' print '*** Creating procedure sp_insert_notification'

Go
CREATE PROCEDURE [dbo].[sp_insert_notification]
(
   
    @Name [nvarchar] (50),
    @Sender [int],
    @Receiver [int],
    @Important [bit],
    @IsViewed [bit],
    @Date [DateTime],
    @Content [nvarchar] (500)
    
)
AS
    BEGIN
        INSERT INTO [dbo].[Notification]
                ( [Name], [Sender], [Receiver], [Important], [IsViewed], [Date], [Content])
        VALUES
            (  @Name, @Sender, @Receiver, @Important, @IsViewed, @Date, @Content) 
        SELECT SCOPE_IDENTITY()   
    END
GO
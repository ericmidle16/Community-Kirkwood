print '' print '*** using database communityDB ***'
GO
USE [communityDB]
GO

print '' print '***starting viewUnassignedVolunteersVLD-015.sql'
GO

/*
    <summary>
    Creator:            Yousif Omer
    Created:            2025/02/08
    Summary:            This is the  for Event Type Table
	
    Last Updated By:	Dat Tran
	Last Updated: 		2025-02-11
    What Was Changed:	Added default value for Description field.
    </summary>
*/
print '' print '*** creating Event Type Table'
GO
CREATE TABLE [EventType] (
    [EventTypeID]    [nvarchar](50)        NOT NULL,
    [Description]    [nvarchar](250)        NOT NULL    DEFAULT '',

    CONSTRAINT [pk_eventtypeid] PRIMARY KEY ([EventTypeID] ASC)
)
GO


/*
    <summary>
    Creator:            Dat Tran
    Created:            2025-02-11
    Summary:            This is the Event table.
    
	Last Updated By:    Yousif Omer
    Last Updated:       2025/03/28
    What Was Changed:   Added the Active field & made the foreign key 'EventTypeID' Cascade Update.
    </summary>
*/
print '' print '*** creating Event Table'
GO
CREATE TABLE [Event] (
    [EventID]            [int]IDENTITY(100000,1)        NOT NULL,
    [EventTypeID]        [nvarchar](50)    NOT NULL,
    [ProjectID]            [int]            NOT NULL,
    [DateCreated]        [DateTime]        NOT NULL    DEFAULT GETDATE(),
    [StartDate]            [DateTime]        NOT NULL,
    [EndDate]            [DateTime]        NOT NULL,
    [Name]                [nvarchar](50)    NOT NULL,
    [LocationID]        [int]            NOT NULL,
    [Image]                [varbinary](MAX)    NULL    DEFAULT NULL,
    [ImageMimeType]        [nvarchar](50)    NULL,
    [VolunteersNeeded]    [int]            NOT NULL    DEFAULT 0,
    [UserID]            [int]            NOT NULL,
    [Notes]                [nvarchar](250)    NOT NULL    DEFAULT '',
    [Description]        [nvarchar](250)    NOT NULL    DEFAULT '',
	[Active]			[bit]							DEFAULT 1,

    CONSTRAINT [pk_eventid] PRIMARY KEY ([EventID] ASC),
    CONSTRAINT [fk_event_eventtype] FOREIGN KEY ([EventTypeID])
        REFERENCES [EventType]([EventTypeID])
			ON UPDATE CASCADE,
    CONSTRAINT [fk_event_projectid] FOREIGN KEY ([ProjectID])
        REFERENCES [Project]([ProjectID]),
    CONSTRAINT [fk_event_locationid] FOREIGN KEY ([LocationID])
        REFERENCES [Location]([LocationID]),
    CONSTRAINT [fk_event_userid] FOREIGN KEY ([UserID])
        REFERENCES [User]([UserID])
)
GO


print '' print '*** creating Task Type Table'
GO
CREATE TABLE [TaskType] (
    [TaskType]       [nvarchar](50)        NOT NULL,
    [Description]    [nvarchar](250)       NOT NULL    DEFAULT '',

    CONSTRAINT [pk_tasktype] PRIMARY KEY ([TaskType] ASC)
)
GO
print '' print '*** creating Task Table'
GO
CREATE TABLE [Task] (
    [TaskID]		[int] IDENTITY(100000, 1)   NOT NULL,
    [Name]          [nvarchar](100)        		NOT NULL	UNIQUE,
    [Description]   [nvarchar](250)        		NOT NULL    DEFAULT '',
	[TaskDate]		[date]						NOT NULL,
    [ProjectID]     [int]                		NOT NULL,
    [TaskType]      [nvarchar](50)        		NOT NULL,
    [EventID]       [int]                		NULL,
    [Active]        [bit]                		NOT NULL    DEFAULT 1,
    CONSTRAINT [pk_taskid] PRIMARY KEY ([TaskID] ASC),
    CONSTRAINT [fk_task_projectid] FOREIGN KEY ([ProjectID])
        REFERENCES [Project]([ProjectID]),
    CONSTRAINT [fk_task_tasktype] FOREIGN KEY ([TaskType])
        REFERENCES [TaskType]([TaskType]),
    CONSTRAINT [fk_task_eventid] FOREIGN KEY ([EventID])
        REFERENCES [Event]([EventID])
)
GO
print '' print '*** creating Task Assigned Table'
GO
CREATE TABLE [TaskAssigned] (
    [TaskID]        [int]        NOT NULL,
    [UserID]        [int]        NOT NULL,

    CONSTRAINT [fk_taskassigned_taskid] FOREIGN KEY ([TaskID])
        REFERENCES [Task]([TaskID]),
    CONSTRAINT [fk_taskassigned_userid] FOREIGN KEY ([UserID])
        REFERENCES [User]([UserID])
)
GO

/*
///<summary>
/// Creator: Dat Tran
/// Created: 2025-02-11
/// Summary: This stored procedure combines the data in the User table
/// with the IsAvailable field of the Availability table using a join.  
/// Last updated by: Stan Anderson
/// Last updated: 
/// Changes: Made it unassigned only. 
/// </summary>

*/
print '' print '***creating sp_view_available_users'
GO
CREATE PROCEDURE [dbo].[sp_view_available_users]
(
    @isAvailable    bit,
    @EventID    int
)
AS
 
    BEGIN
        SELECT DISTINCT    [User].[UserID],[User].[GivenName],[User].[FamilyName],[User].[City],[User].[State]
        FROM    [Event]
        JOIN    [Project]
            on     [Event].[ProjectID] = [Project].[ProjectID]
        JOIN    [VolunteerStatus]
            on    [Project].[ProjectID] = [VolunteerStatus].[ProjectID]
        LEFT JOIN    [TaskAssigned]
            on    [VolunteerStatus].[UserID] = [TaskAssigned].[UserID]
        JOIN    [User]
            on    [VolunteerStatus].[UserID] = [User].[UserID]
        JOIN    [Task]
            on    [TaskAssigned].[TaskID] = [Task].[TaskID]
        JOIN    [Availability]
            on    [User].[UserID] = [Availability].[UserID]
        WHERE    [VolunteerStatus].[Approved] = 1
            AND    [Task].[EventID] = @EventID
			AND		[TaskAssigned].[UserID] IS NULL
            --AND    [Availability].[IsAvailable] = @IsAvailable
    END
GO
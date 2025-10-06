print '' print '*** Starting EVC-002-UpdateEvent-Desktop.sql ***'
GO


print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
    <summary>
    Creator:            Yousif Omer
    Created:            2025/02/02
    Summary:            This is the stored procedure for the Update Event
	
    Last Updated By:    Kate Rich
    Last Updated:       2025-04-25
    What Was Changed:   Added the @OldEventTypeID & @NewEventTypeID parameters.
    </summary>
*/
print '' print '*** creating sp_update_event ***'
GO
	CREATE PROCEDURE [dbo].[sp_update_event]
(

    @EventID                [int]             ,
	
	@NewDateCreated		    [DateTime]        ,                 
	@NewStartDate 		    [DateTime]        ,            
	@NewEndDate 			[DateTime]        ,              
    @NewName           		[nvarchar]   (50) ,
	@NewVolunteersNeeded    [int]             ,
    @NewNotes       		[nvarchar] (250)  ,
	@NewDescription    		[nvarchar] (250)  ,
	@NewActive              [bit]             ,
   
	@OldDateCreated		    [DateTime]        ,                 
	@OldStartDate 	        [DateTime]        ,            
	@OldEndDate 			[DateTime]        ,
	@OldName           		[nvarchar]   (50) ,
	@OldVolunteersNeeded    [int]             ,
    @OldNotes       		[nvarchar] (250)  ,
	@OldDescription    		[nvarchar] (250)  ,
	@OldActive              [bit]             ,
	
	@OldEventTypeID			[nvarchar] (50)   ,
	@NewEventTypeID			[nvarchar] (50)
)
AS
BEGIN
    UPDATE [dbo].[Event]
    SET 
    DateCreated	 		= @NewDateCreated       ,		                    
	StartDate 	 		= @NewStartDate         ,		               
	EndDate      		= @NewEndDate 	        ,		             
    Name         		= @NewName              ,  		
    VolunteersNeeded    = @NewVolunteersNeeded  ,   
    Notes               = @NewNotes       		,
	Description         = @NewDescription    	,
	Active	            = @NewActive 			,
	EventTypeID			= @NewEventTypeID
    WHERE 
        [EventID]                  = @EventID
	AND [DateCreated]              = @OldDateCreated
    AND [StartDate]                = @OldStartDate
    AND [EndDate]                  = @OldEndDate
	AND [Name]                     = @OldName
	AND [VolunteersNeeded]         = @OldVolunteersNeeded
	AND [Notes]					   = @OldNotes
	AND [Description]              = @OldDescription
	AND [Active]                   = @OldActive
	AND [EventTypeID]			   = @OldEventTypeID

    RETURN @@ROWCOUNT;
END
GO
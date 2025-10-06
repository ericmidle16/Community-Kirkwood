print '' print '** Starting VOL-006_signUpToProject.sql'
GO

use [communityDb]
go


/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Stored Procedure for inserting into a notification table.
	</summary>
*/

print '' print '*** Creating procedure sp_insert_notification'

Go
CREATE PROCEDURE [dbo].[sp_insert_notifications]
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

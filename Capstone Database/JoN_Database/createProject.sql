-- <summary>
-- Creator:  Josh Nicholson
-- Created:  2025/02/07
-- Summary:  SQL file for the createProject feature's related tables and sample data
-- </summary>

print '' print '' print '*** creating the createProject feature files'
GO
USE [communityDb]
GO

print '' print '*** creating procedure sp_add_project'
GO
CREATE PROCEDURE [dbo].[sp_add_project]
    (
        @Name      				[nvarchar](50),
		@ProjectTypeID			[nvarchar](50),
        @LocationID     		[int],
		@UserID        			[int],
        @StartDate     			[date],
        @Status       			[nvarchar](25),
		@Description			[nvarchar](250),
		@AcceptsDonations		[bit],
		@PayPalAccount			[nvarchar](50),
		@AvailableFunds			[decimal](15, 2)
)
AS
    BEGIN
        INSERT INTO [dbo].[Project]
			([Name], [ProjectTypeID], [LocationID],[UserID], [StartDate], [Status], [Description], [AcceptsDonations], [PayPalAccount], [AvailableFunds])
		VALUES
			(@Name, @ProjectTypeID, @LocationID, @UserID, @StartDate, @Status, @Description, @AcceptsDonations, @PayPalAccount, @AvailableFunds)
	
		SELECT SCOPE_IDENTITY()
    END 
GO

print '' print '*** creating procedure sp_get_all_project_types'
GO
CREATE PROCEDURE [dbo].[sp_get_all_project_types]
AS
    BEGIN
        SELECT *		   
        FROM [dbo].[ProjectType]
    END 
GO
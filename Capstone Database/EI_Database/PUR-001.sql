print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
    Creator: Eric Idle
    Summary: This is creating a single expense for a certain project
    Last Updated By: Eric Idle
    Last Updated: 2025-03-16
    What Was Changed:
*/

print '' print '*** creating procedure sp_insert_expense_by_projectid'
GO
CREATE PROCEDURE [dbo].[sp_insert_expense_by_projectid]
	(
		@ProjectID			[int],
		@ExpenseTypeID		[nvarchar](50),
		@Date				[DATE],
		@Amount				[decimal](10,2),
		@Description		[nvarchar](250)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Expense]
			([ProjectID], [ExpenseTypeID], [Date], [Amount], [Description])
		VALUES
			(@ProjectID, @ExpenseTypeID, @Date, @Amount, @Description)
	END
GO
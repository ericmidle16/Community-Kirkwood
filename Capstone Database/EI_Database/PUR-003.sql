print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
    Creator: Eric Idle
    Summary: This is selecting a single expense from a certain project
    Last Updated By: Eric Idle
    Last Updated: 2025-03-4
    What Was Changed:
*/
print '' print '*** creating procedure sp_select_expenses_by_expenseid_projectid'
GO 
CREATE PROCEDURE [dbo].[sp_select_expenses_by_expenseid_projectid]
(
	@ExpenseID		[int],
	@ProjectID		[int]
)
AS
	BEGIN
		SELECT [ExpenseID], [ProjectID], [ExpenseTypeID], [Date], [Amount], [Description]
		FROM [Expense]
		WHERE	ExpenseID = @ExpenseID 
		AND		ProjectID = @ProjectID
	END
GO

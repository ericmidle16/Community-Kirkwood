print '' print '*** using database communityDb'
GO
USE [communityDb]
GO


/*
    Creator: Eric Idle
    Summary: This is the table for the ExpenseType
    Last Updated By: Eric Idle
    Last Updated: 2025-02-27
    What Was Changed:
*/
print '' print ' creating ExpenseType table '
GO
CREATE TABLE ExpenseType (
	[ExpenseTypeID]			[nvarchar](50)		UNIQUE NOT NULL,
	[Description]			[nvarchar](250)		NOT NULL DEFAULT '',

	CONSTRAINT [pk_expensetypeid] PRIMARY KEY ([ExpenseTypeID] ASC)
)
GO

/*
    Creator: Eric Idle
    Summary: This is the table for the Expenses
    Last Updated By: Eric Idle
    Last Updated: 2025-02-24
    What Was Changed:
*/
print '' print ' creating Expense table '
GO
CREATE TABLE Expense (
	[ExpenseID]			[int] 				IDENTITY(100000, 1) UNIQUE NOT NULL,
	[ProjectID]			[int]				NOT NULL,
	[ExpenseTypeID]		[nvarchar](50)		NOT NULL,
	[Date]				[DATE]				NOT NULL,
	[Amount]			[decimal](10,2)		NOT NULL,
	[Description]		[nvarchar](250)		DEFAULT ''
	
	CONSTRAINT [pk_expenseid] PRIMARY KEY ([ExpenseID] ASC),
	CONSTRAINT [fk_expensetype_expense_expensetypeid] FOREIGN KEY ([ExpenseTypeID])
		REFERENCES [ExpenseType]([ExpenseTypeID]),
	CONSTRAINT [fk_project_expense_projectid] FOREIGN KEY ([ProjectID])
		REFERENCES [Project]([ProjectID])
)
GO

/*	=======================================================================================
		End of Tables creation
		Beginning stored procedures
	======================================================================================= */

/*
    Creator: Eric Idle
    Summary: This is to see all possible expenses for a given project
    Last Updated By: Eric Idle
    Last Updated: 2025-02-24
    What Was Changed:
*/
print '' print '*** creating procedure sp_select_all_expenses_by_projectid'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_expenses_by_projectid]
(
	@ProjectID		[int]
)
AS
	BEGIN
		SELECT [ExpenseID], [ProjectID], [ExpenseTypeID], [Date], [Amount], [Description]
		FROM [Expense]
		WHERE ProjectID = @ProjectID
	END
GO

/*
    Creator: Eric Idle
    Summary: This is to see all possible expense types
    Last Updated By: Eric Idle
    Last Updated: 2025-02-26
    What Was Changed:
*/
print '' print '*** creating procedure sp_select_all_expensetypes'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_expensetypes]
AS
	BEGIN
		SELECT [ExpenseTypeID],[Description]
		FROM [ExpenseType]
	END
GO
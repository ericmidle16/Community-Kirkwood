print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
    Creator: Eric Idle
    Summary: This is the table for the Invoices
    Last Updated By: Eric Idle
    Last Updated: 2025-02-19
    What Was Changed:
*/
print '' print ' creating Invoice table '
GO
CREATE TABLE Invoice (
	[InvoiceID]			[int] IDENTITY(100000, 1)	UNIQUE NOT NULL,
	[InvoiceNumber]		[nvarchar](50)				NOT NULL,
	[ExpenseID]			[int]						NULL,
	[ProjectID]			[int]						NOT NULL,
	[InvoiceDate]		[DATE]						NOT NULL,
	[Status]			[nvarchar](25)				DEFAULT "Pending Payment" ,
	[Description]		[nvarchar](250)				DEFAULT ""
	
	CONSTRAINT [pk_invoiceid] PRIMARY KEY ([InvoiceID] ASC),
	CONSTRAINT [fk_expense_invoice_expenseid] FOREIGN KEY ([ExpenseID])
		REFERENCES [Expense]([ExpenseID]),
	CONSTRAINT [fk_project_invoice_projectid] FOREIGN KEY ([ProjectID])
		REFERENCES [Project]([ProjectID])
)
GO

/*	=======================================================================================
		End of Tables creation
		Beginning stored procedures
	======================================================================================= */

/*
    Creator: Eric Idle
    Summary: This is to see all possible invoices for a given project
    Last Updated By: Eric Idle
    Last Updated: 2025-02-19
    What Was Changed:
*/
print '' print '*** creating procedure sp_select_all_invoices_by_projectid'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_invoices_by_projectid]
(
	@ProjectID		[int]
)
AS
	BEGIN
		SELECT [InvoiceID], [InvoiceNumber], [ExpenseID], [ProjectID], [InvoiceDate], [Status], [Description]
		FROM [Invoice]
		WHERE ProjectID = @ProjectID
	END
GO
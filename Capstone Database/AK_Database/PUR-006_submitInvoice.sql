print '' print '** Starting PUR-006_submitInvoice.sql'
GO
use [communityDb]
go


/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Stored Procedure for inserting into invoice table.
	</summary>
*/
print '' print '*** Creating procedure sp_insert_invoice'
Go
CREATE PROCEDURE [dbo].[sp_insert_invoice]
(

    @InvoiceNumber  [nvarchar](50),
    @ExpenseID [int],
    @ProjectID [int],
    @InvoiceDate [DateTime],
    @Status [nvarchar](25),
    @Description [nvarchar] (250)
)
AS
    BEGIN
        INSERT INTO [dbo].[Invoice]
                ([InvoiceNumber], [ExpenseID], [ProjectID], [InvoiceDate], [Status], [Description])
        VALUES
            (@InvoiceNumber, @ExpenseID, @ProjectID, @InvoiceDate, @Status, @Description) 
          
    END
GO

/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Stored Procedure for  viewing all invoices
	</summary>
*/
print '' print '*** Creating procedure sp_view_all_invoices'
Go
CREATE PROCEDURE [dbo].[sp_view_all_invoices]
AS
    BEGIN
        SELECT [InvoiceID], [InvoiceNumber], [ExpenseID], [ProjectID], [InvoiceDate], [Status],[Description]
        FROM [Invoice]
    END
GO
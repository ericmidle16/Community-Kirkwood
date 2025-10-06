print '' print '** Starting ACC-001_changeInvoiceStatus.sql'
GO

use [communityDb]
go

/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Stored Procedure for updating invoice by InvoiceID and status
	</summary>
*/
print '' print '*** Creating procedure sp_Update_invoice_status_by_id'
Go
CREATE PROCEDURE [dbo].[sp_Update_invoice_status_by_id]
(
    @InvoiceID        [int],
    @Status         [nvarchar](25)
)
AS
    BEGIN
        UPDATE [dbo].[Invoice]
        SET [Status] = @Status
        WHERE [InvoiceID] = @InvoiceID
        RETURN @@ROWCOUNT
    END 
Go

/*
	<summary>
		Creator:	Akoi Kollie
		Created:	2025-03-19
		Summary:	Stored Procedure for updating invoice status by invoiceID
	</summary>
*/
print '' print '*** Creating procedure sp_Update_invoice_status_by_id'
Go
CREATE PROCEDURE [dbo].[sp_select_invoice_status_by_id]
(
    @InvoiceID        [int]
)
AS
    BEGIN
        
        SELECT [InvoiceID], [InvoiceNumber], [ExpenseID], [ProjectID], [InvoiceDate], [Status],[Description]
       FROM [Invoice]
        WHERE [InvoiceID] = @InvoiceID
    END 
Go
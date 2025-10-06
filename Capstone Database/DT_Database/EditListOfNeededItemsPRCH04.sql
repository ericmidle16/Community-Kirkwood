print '' print '*** using database communityDB ***'
GO
USE [communityDB]
GO
/*
///<summary>
/// Creator: Dat Tran
/// Created: 2025-02-25
/// Summary: A stored procedure to update a list of needs for a project.  
/// Last updated by: Dat Tran
/// Last updated: 
/// Changes:
/// </summary>

*/
print '' print '***starting EditListOfNeededItemsPRCH04.sql'
GO
print '' print '***creating procedure sp_edit_list_of_needed_items***'
GO
CREATE procedure [dbo].[sp_edit_list_of_needed_items](

@ItemID [int],
@ProjectID [int],
@NewName [nvarchar](50),
@NewQuantity [int],
@NewPrice [decimal](10,2),
@NewDescription [nvarchar](250),

@OldName [nvarchar](50),
@OldQuantity [int],
@OldPrice [decimal](10,2),
@OldDescription [nvarchar](250)


)
AS
	BEGIN
	
	UPDATE [Needlist]
	SET [Name] = @NewName,
		[Quantity] = @NewQuantity,
		[Price] = @NewPrice,
		[Description] = @NewDescription

		
	WHERE [ProjectID] = @ProjectID
	AND [ItemID] = @ItemID
	AND [Name] = @OldName
	AND [Quantity] = @OldQuantity
	AND [Price] = @OldPrice
	AND [Description] = @OldDescription
	RETURN @@ROWCOUNT
	
	END
GO
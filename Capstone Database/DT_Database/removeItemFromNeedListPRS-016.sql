print '' print '*** using database communityDB ***'
GO
USE [communityDB]
GO
/*
///<summary>
/// Creator: Dat Tran
/// Created: 2025-03-04
/// Summary: A stored procedure to delete an item from the need list.  
/// Last updated by: Dat Tran
/// Last updated: 
/// Changes:
/// </summary>

*/
print '' print '***starting removeItemFromNeedListPRS-016.sql'
GO

print '' print   '***creating procedure sp_remove_item_from_needlist***'
GO
CREATE procedure [dbo].[sp_remove_item_from_needlist](
@ItemId [int]

)
AS
BEGIN

DELETE FROM NeedList
    WHERE ItemId = @ItemId 
END
GO
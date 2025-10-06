print '' print '*** using database communityDB ***'
GO
USE [communityDB]
GO
/*
///<summary>
/// Creator: Dat Tran
/// Created: 2025-04-04
/// Summary: Added a stored procedure to view a single item.
/// </summary>
*/

print '' print '***starting viewNeededItemsPUR-005'
GO


print '' print '***creating procedure sp_view_needlist***'
GO
CREATE procedure [dbo].[sp_view_needlist](

@ProjectID int

)
AS
	BEGIN
	 SELECT [ProjectID], [Name], [Quantity], [Price], [Description], [IsObtained], [ItemID]
	 FROM [dbo].[Needlist]
	 WHERE [ProjectID] = @ProjectID
	END
GO

print '' print '***creating procedure sp_view_single_item'
GO
CREATE procedure [dbo].[sp_view_single_item](

@ItemID int 
)
AS 
	BEGIN 
	SELECT [ProjectID], [Name], [Quantity], [Price], [Description], [IsObtained]
	FROM [dbo].[Needlist]
	WHERE [ItemID] = @ItemID 
	END
GO
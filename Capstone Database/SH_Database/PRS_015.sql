print '' print '*** starting PRS_015.sql file'
/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/17
	Summary:  Creation of the PRS_015 database sql
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/

print '' print '*** using database user'
GO
USE [communityDb]
GO

/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/17
	Summary: Creation of the NeedList table.
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/
print '' print '***creating needlist table'
GO
CREATE TABLE [dbo].[NeedList] (
    [ItemID] 		[int] IDENTITY(100000, 1) UNIQUE 	NOT NULL,  
    [ProjectID] 	[int] 								NOT NULL,  
	[Name]			[nvarchar](50)						NOT NULL,
    [Quantity] 		[int] 								NOT NULL,  
    [Price] 		[decimal](10,2) 					NOT NULL,  
    [Description] 	[nvarchar](250) DEFAULT ''		    NOT NULL,  
	[IsObtained]	[bit]			DEFAULT 0			NOT NULL,
    CONSTRAINT [pk_itemid] PRIMARY KEY([ItemID] ASC),  
    CONSTRAINT [fk_needlist_projectid] FOREIGN KEY([ProjectID]) 
		REFERENCES [Project]([ProjectID])  
)
GO
/* ============================================
     End of Creating Project and Project related Tables
     Beginning of Stored Procedures
===============================================*/
/*
    <summary>
    Creator: Skyann Heintz
    Created: 2025/02/17
    Summary: Inserts an item to the NeedList for a selected project
    Last Updated By:
    Last Updated:
    What Was Changed:
    </summary>
*/
print ''print '***creating stored procedure sp_insert_item_to_needlist'
GO
CREATE PROCEDURE [dbo].[sp_insert_item_to_needlist]
	(
		@ProjectID [int],        
		@Name [nvarchar](50),     
		@Quantity [int],          
		@Price [decimal](10,2),  
		@Description [nvarchar](250)
	)
AS
BEGIN
    INSERT INTO [dbo].[NeedList] 
        ([ProjectID], [Name], [Quantity], [Price], [Description])
    VALUES 
        (@ProjectID, @Name, @Quantity, @Price, @Description);
    
    	 SELECT @@ROWCOUNT AS RowsAffected;
END
GO

/*
	<summary>
	Creator: Skyann Heintz
	Created: 2025/02/19
	Summary: This stored procedure checks if a project has that item name already 
	in the NeedList database. If a matching record exists, the procedure returns
	`1`; otherwise, it returns `0`.
	Last Updated By:
	Last Updated By:
	Last Updated:
	What Was Changed:
	</summary>
*/
print '' print '***creating stored procedure sp_select_item_from_needlist'
GO
CREATE PROCEDURE [dbo].[sp_select_item_from_needlist]
	(
		@ProjectID [int],
		@Name[nvarchar](50)
	)
AS
BEGIN
    SET NOCOUNT ON;

	IF EXISTS (
		SELECT 1 FROM [NeedList]
		WHERE [ProjectID] = @ProjectID
			AND [Name] = @Name
	)
	BEGIN
		SELECT 1;
	END
	 ELSE
    BEGIN
        SELECT 0;
    END
END
GO
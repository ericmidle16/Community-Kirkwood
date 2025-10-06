-- <summary>
-- Creator:  Josh Nicholson
-- Created:  2025/03/13
-- Summary:  SQL file for the createSkill feature's related tables and sample data
-- </summary>

print '' print '' print '*** creating the createSkill feature files'
GO
USE [communityDb]
GO

print '' print '*** creating procedure sp_add_skill'
GO
CREATE PROCEDURE [dbo].[sp_add_skill]
    (
        @SkillID   		[nvarchar](50),
		@Description	[nvarchar](250)   		
    )
AS
    BEGIN
        INSERT INTO [dbo].[Skill]
			([SkillID],[Description])
		VALUES
			(@SkillID, @Description)
	
		SELECT SCOPE_IDENTITY()
    END 
GO
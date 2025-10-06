-- <summary>
-- Creator:  Josh Nicholson
-- Created:  2025/02/14
-- Summary:  SQL file for the updateSkills feature's related tables and sample data
-- </summary>

print '' print '' print '*** creating the updateSkills feature files'
GO
USE [communityDb]
GO

print '' print '*** creating skill table'
GO
CREATE TABLE [dbo].[Skill]
(
	[SkillID]				[nvarchar](50)						UNIQUE 		NOT NULL,
	[Description]			[nvarchar](250)		DEFAULT ""					NOT NULL
)
GO


print '' print '*** creating user_skill table'
GO
CREATE TABLE [dbo].[UserSkill]
(
	[UserID]				[int] 							NOT NULL,
	[SkillID]				[nvarchar](50)					NOT NULL,
	CONSTRAINT [fk_userskill_userID]	FOREIGN KEY([UserID])
		REFERENCES [User]([UserID]),
	CONSTRAINT [fk_userskill_skillID]	FOREIGN KEY([SkillID])
		REFERENCES [Skill]([SkillID])
)
GO


print '' print '*** creating procedure sp_get_all_skills'
GO
CREATE PROCEDURE [dbo].[sp_get_all_skills]
AS
    BEGIN
        SELECT *		   
        FROM [dbo].[Skill]
    END 
GO

print '' print '*** creating procedure sp_get_user_skills_by_userID'
GO
CREATE PROCEDURE [dbo].[sp_get_user_skills_by_userID]
(
		@UserID		[int]
)
AS
    BEGIN
        SELECT *		   
        FROM [dbo].[UserSkill]
		WHERE [UserID] = @UserID
    END 
GO

print '' print '*** creating procedure sp_add_userskill'
GO
CREATE PROCEDURE [dbo].[sp_add_userskill]
    (
        @UserID         [int],
        @SkillID        [nvarchar](50)
    )
AS
    BEGIN 
        INSERT INTO [dbo].[UserSkill] 
            ([UserID], [SkillID])
        VALUES 
            (@UserID, @SkillID);
    END
GO

print '' print '*** creating procedure sp_remove_userskill'
GO
CREATE PROCEDURE [dbo].[sp_remove_userskill]
    (
        @UserID         [int],
        @SkillID        [nvarchar](50)
    )
AS
    BEGIN 
        DELETE FROM [dbo].[UserSkill]
        WHERE 
			[UserID] = @UserID
		AND [SkillID] = @SkillID;
    END
GO
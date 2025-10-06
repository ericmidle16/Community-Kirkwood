print '' print '*** Starting BGC-010_updateBackgroundCheckDesktop.sql ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	<summary>
		Creator:	Kate Rich
		Created:	2025-02-26
		Summary:	Stored Procedure for updating a BackgroundCheck record.
	</summary>
*/
print '' print '*** Create Stored Procedure sp_update_backgroundCheck ***'
GO
CREATE PROCEDURE [dbo].[sp_update_backgroundCheck]
	(
		@BackgroundCheckID		[int],
		@Investigator			[int],
		@UserID					[int],
		@ProjectID				[int],
		@OldStatus			[nvarchar](25),
		@NewStatus			[nvarchar](25),
		@OldDescription	[nvarchar](250),
		@NewDescription	[nvarchar](250)
	)
AS
	BEGIN
		UPDATE [BackgroundCheck]
		SET [Status] = @NewStatus,
			[Description] = @NewDescription
		WHERE [BackgroundCheckID] = @BackgroundCheckID
			AND [Investigator] = @Investigator
			AND [UserID] = @UserID
			AND [ProjectID] = @ProjectID
			AND [Status] = @OldStatus
			AND [Description] = @OldDescription
		RETURN @@ROWCOUNT
	END
GO
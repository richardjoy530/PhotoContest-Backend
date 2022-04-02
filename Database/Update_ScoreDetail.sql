CREATE PROCEDURE [dbo].[Update_ScoreDetail]
	@Id					int,
	@EntryId			int,
	@Score				int,
	@ScoreHasValue		bit = 0,
	@EntryIdHasValue	bit = 0
AS

UPDATE	[dbo].[ScoreDetail]
	SET	[EntryId]	= CASE WHEN @EntryIdHasValue	= 1 THEN @EntryId	ELSE [EntryId]	END,
		[Score]			= CASE WHEN @ScoreHasValue		= 1 THEN @Score		ELSE [Score]	END
	WHERE [Id] = @Id

RETURN 0

CREATE PROCEDURE [dbo].[Get_ScoreDetail]
	@Id				int = NULL,
	@EntryId		int = NULL,
	@Score			int = NULL,
	@CheckId		bit	= 0,
	@CheckEntryId	bit	= 0,
	@CheckScore		bit	= 0
AS

SELECT [Id]
      ,[EntryId]
      ,[Score]
  FROM [dbo].[ScoreDetail]
  WHERE (@CheckId		= 1	AND [Id] = @Id)				OR (@CheckId		= 0)
  AND	(@CheckEntryId	= 1	AND [EntryId] = @EntryId)	OR (@CheckEntryId	= 0)
  AND	(@CheckScore	= 1	AND [Score] = @Score)		OR (@CheckScore		= 0)

RETURN 0

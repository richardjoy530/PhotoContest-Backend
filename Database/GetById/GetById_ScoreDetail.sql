CREATE PROCEDURE [dbo].[GetById_ScoreDetail]
    @Id int
AS

SELECT [Id]
        , [EntryId]
        , [Score]
FROM [dbo].[ScoreDetail]
WHERE [Id] = @Id
    RETURN 0

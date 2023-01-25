CREATE PROCEDURE [dbo].[Delete_ScoreDetail]
    @Id int = NULL
AS

DELETE
FROM [dbo].[ScoreDetail]
WHERE [Id] = @Id
    RETURN 0

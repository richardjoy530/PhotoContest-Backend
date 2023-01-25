CREATE PROCEDURE [dbo].[GetById_PhotoTheme]
    @Id int
AS

SELECT [Id]
        , [Theme]
        , [ContestDate]
FROM [dbo].[PhotoTheme]
WHERE [Id] = @Id
    RETURN 0

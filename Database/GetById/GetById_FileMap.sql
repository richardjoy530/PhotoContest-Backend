CREATE PROCEDURE [dbo].[GetById_FileMap]
    @Id int
AS

SELECT [Id]
        , [FilePath]
FROM [dbo].[FileMap]
WHERE [Id] = @Id
    RETURN 0

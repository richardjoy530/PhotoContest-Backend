CREATE PROCEDURE [dbo].[Get_FileMap]
    @Id            int         = NULL,
    @FilePath      varchar(50) = NULL,
    @CheckId       bit = 0,
    @CheckFilePath bit = 0
AS

SELECT [Id],
       [FilePath]
  FROM [dbo].[FileMap]
  WHERE (@CheckId     = 1 AND [Id] = @Id)             OR (@CheckId       = 0)
  AND   (@CheckFilePath = 1 AND [FilePath] = @FilePath) OR (@CheckFilePath = 0)

RETURN 0

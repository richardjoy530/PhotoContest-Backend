CREATE PROCEDURE [dbo].[Get_FileMap]
	@Id int = NULL,
	@FilePath varchar(50) = NULL
AS

SELECT [Id],
	   [FilePath]
  FROM [dbo].[FileMap]
 WHERE [Id] = @Id OR [FilePath] = @FilePath

RETURN 0

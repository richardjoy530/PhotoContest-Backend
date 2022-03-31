CREATE PROCEDURE [dbo].[GetAll_FileMap]
AS

SELECT [Id]
      ,[FilePath]
  FROM [dbo].[FileMap]

RETURN 0

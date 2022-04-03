CREATE PROCEDURE [dbo].[GetAll_PhotoTheme]
AS

SELECT [Id]
      ,[Theme]
      ,[ContestDate]
  FROM [dbo].[PhotoTheme]

RETURN 0

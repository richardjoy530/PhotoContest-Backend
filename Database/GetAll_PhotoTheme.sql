CREATE PROCEDURE [dbo].[GetAll_PhotoTheme]
	@param1 int = 0,
	@param2 int
AS

SELECT [Id]
      ,[Theme]
      ,[ContestDate]
  FROM [dbo].[PhotoTheme]

RETURN 0

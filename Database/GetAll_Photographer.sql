CREATE PROCEDURE [dbo].[GetAll_Photographer]
AS

SELECT [Id]
      ,[UploaderName]
  FROM [dbo].[Photographer]

RETURN 0

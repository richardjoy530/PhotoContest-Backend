CREATE PROCEDURE [dbo].[GetAll_IdMap]
AS

SELECT [ReferenceId]
      ,[Id]
      ,[IdType]
  FROM [dbo].[IdMap]

RETURN 0

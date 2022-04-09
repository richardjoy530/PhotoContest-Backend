CREATE PROCEDURE [dbo].[GetById_IdMap]
    @ReferenceId    uniqueidentifier
AS

SELECT [ReferenceId]
      ,[Id]
      ,[IdType]
  FROM [dbo].[IdMap]
 WHERE [ReferenceId] = @ReferenceId

RETURN 0

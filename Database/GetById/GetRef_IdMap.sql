CREATE PROCEDURE [dbo].[GetRef_IdMap]
    @ReferenceId uniqueidentifier OUTPUT,
    @Id          int,
    @IdType      int
AS

SELECT @ReferenceId = [ReferenceId]
  FROM [dbo].[IdMap]
 WHERE [Id] = @Id AND [IdType] = @IdType

RETURN 0

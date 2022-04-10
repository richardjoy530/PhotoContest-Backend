CREATE PROCEDURE [dbo].[Get_IdMap]
    @Id               int              = NULL,
    @IdType           int              = NULL,
    @ReferenceId      uniqueidentifier = NULL,
    @CheckId          bit = 0,
    @CheckIdType      bit = 0,
    @CheckReferenceId bit = 0
AS

SELECT [ReferenceId],
       [Id],
       [IdType]
  FROM [dbo].[IdMap]
  WHERE (@CheckId          = 1 AND [Id] = @Id)                   OR (@CheckId          = 0)
  AND   (@CheckIdType      = 1 AND [IdType] = @IdType)           OR (@CheckIdType      = 0)
  AND   (@CheckReferenceId = 1 AND [ReferenceId] = @ReferenceId) OR (@CheckReferenceId = 0)
  
RETURN 0

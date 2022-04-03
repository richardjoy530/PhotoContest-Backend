CREATE PROCEDURE [dbo].[Get_IdMap]
	@ReferenceId uniqueidentifier = NULL,
	@Id int = NULL,
	@IdType int = NULL
AS

SELECT [ReferenceId],
       [Id],
       [IdType]
  FROM [dbo].[IdMap]
  WHERE ([ReferenceId] = @ReferenceId AND [IdType] = @IdType) OR ([Id] = @Id AND [IdType] = @IdType) 
  
RETURN 0

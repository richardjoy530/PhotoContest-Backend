CREATE PROCEDURE [dbo].[Get_IdMap]
	@ReferenceId uniqueidentifier,
	@Id int,
	@IdType int
AS

SELECT [ReferenceId]
      ,[Id]
      ,[IdType]
  FROM [dbo].[IdMap]
  WHERE ([ReferenceId] = @ReferenceId AND [IdType] = @IdType) OR ([Id] = @Id AND [IdType] = @IdType) 
  
RETURN 0

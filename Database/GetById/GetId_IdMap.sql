CREATE PROCEDURE [dbo].[GetId_IdMap]
    @ReferenceId    uniqueidentifier,
    @Id             int OUTPUT
AS

SELECT @Id = [Id]
FROM [dbo].[IdMap]
WHERE [ReferenceId] = @ReferenceId
    RETURN 0

CREATE PROCEDURE [dbo].[Delete_IdMap]
	@ReferenceId uniqueidentifier
AS

DELETE [dbo].[IdMap]
 WHERE [ReferenceId] = @ReferenceId

RETURN 0

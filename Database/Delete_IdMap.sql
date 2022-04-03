CREATE PROCEDURE [dbo].[Delete_IdMap]
	@ReferenceId uniqueidentifier = NULL
AS

DELETE FROM	[dbo].[IdMap]
	  WHERE [ReferenceId] = @ReferenceId

RETURN 0

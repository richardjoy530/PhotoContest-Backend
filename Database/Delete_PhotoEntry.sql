CREATE PROCEDURE [dbo].[Delete_PhotoEntry]
	@Id int
AS

DELETE FROM [dbo].[PhotoEntry]
      WHERE [Id] = @Id

RETURN 0

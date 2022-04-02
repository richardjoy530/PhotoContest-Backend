CREATE PROCEDURE [dbo].[Delete_PhotoTheme]
	@Id int
AS

DELETE FROM [dbo].[PhotoTheme]
      WHERE [Id] = @Id

RETURN 0

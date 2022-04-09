CREATE PROCEDURE [dbo].[Delete_PhotoTheme]
	@Id int = NULL
AS

DELETE FROM [dbo].[PhotoTheme]
	  WHERE [Id] = @Id

RETURN 0

CREATE PROCEDURE [dbo].[Delete_PhotoEntry]
    @Id int = NULL
AS

DELETE
FROM [dbo].[PhotoEntry]
WHERE [Id] = @Id
    RETURN 0

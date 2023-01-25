CREATE PROCEDURE [dbo].[Delete_FileMap]
    @Id int = NULL
AS

DELETE
FROM [dbo].[FileMap]
WHERE [Id] = @Id
    RETURN 0

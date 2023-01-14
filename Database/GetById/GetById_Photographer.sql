CREATE PROCEDURE [dbo].[GetById_Photographer]
    @Id int
AS

SELECT [Id]
        , [UploaderName]
FROM [dbo].[Photographer]
WHERE [Id] = @Id
    RETURN 0

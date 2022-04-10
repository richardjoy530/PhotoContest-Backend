CREATE PROCEDURE [dbo].[Detele_Photographer]
    @Id int = NULL
AS

DELETE FROM [dbo].[Photographer]
      WHERE [Id] = @Id

RETURN 0

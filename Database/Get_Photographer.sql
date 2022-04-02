CREATE PROCEDURE [dbo].[Get_Photographer]
	@Id int
AS

SELECT [Id]
      ,[UploaderName]
  FROM [dbo].[Photographer]
  WHERE [Id] = @Id

RETURN 0

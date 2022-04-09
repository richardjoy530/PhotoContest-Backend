CREATE PROCEDURE [dbo].[GetById_PhotoEntry]
    @Id int
AS

SELECT [Id]
      ,[ThemeId]
      ,[PhotographerId]
      ,[FileId]
      ,[Caption]
      ,[UploadedOn]
  FROM [dbo].[PhotoEntry]
 WHERE [Id] = @Id
  
RETURN 0

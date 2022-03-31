CREATE PROCEDURE [dbo].[GetAll_PhotoEntry]
AS

SELECT [Id]
      ,[ThemeId]
      ,[PhotographerId]
      ,[FileId]
      ,[Caption]
      ,[UploadedOn]
  FROM [dbo].[PhotoEntry]
  
RETURN 0

CREATE PROCEDURE [dbo].[Get_PhotoEntry]
	@Id int,
	@ThemeId int,
    @PhotographerId int
AS
	
SELECT [Id]
      ,[ThemeId]
      ,[PhotographerId]
      ,[FileId]
      ,[Caption]
      ,[UploadedOn]
  FROM [dbo].[PhotoEntry]
  WHERE [Id] = @Id OR [ThemeId] = @ThemeId OR [PhotographerId] = @PhotographerId

RETURN 0

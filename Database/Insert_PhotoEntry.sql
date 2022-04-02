CREATE PROCEDURE [dbo].[Insert_PhotoEntry]
	@Id int,
	@ThemeId int,
	@PhotographerId int,
	@FileId int,
	@Caption varchar(100),
	@UploadedOn datetime
AS

INSERT INTO [dbo].[PhotoEntry]
           ([Id]
           ,[ThemeId]
           ,[PhotographerId]
           ,[FileId]
           ,[Caption]
           ,[UploadedOn])
    OUTPUT [INSERTED].[Id]
    VALUES (
			@Id,
			@ThemeId,
			@PhotographerId,
			@FileId,
			@Caption,
			@UploadedOn)

RETURN 0

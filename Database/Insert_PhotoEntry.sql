CREATE PROCEDURE [dbo].[Insert_PhotoEntry]
	@Id int OUTPUT,
	@ThemeId int,
	@PhotographerId int,
	@FileId int,
	@Caption varchar(100),
	@UploadedOn datetime
AS

INSERT INTO [dbo].[PhotoEntry]
           ([ThemeId]
           ,[PhotographerId]
           ,[FileId]
           ,[Caption]
           ,[UploadedOn])
    VALUES (@ThemeId,
			@PhotographerId,
			@FileId,
			@Caption,
			@UploadedOn)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0

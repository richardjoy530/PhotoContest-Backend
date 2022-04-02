CREATE PROCEDURE [dbo].[Update_PhotoEntry]
	@Id int,
	@ThemeId int,
	@ThemeIdHasValue bit,
	@PhotographerId int,
	@PhotographerIdHasValue bit,
	@FileId int,
	@FileIdHasValue bit,
	@Caption varchar(100),
	@CaptionHasValue bit,
	@UploadedOn datetime,
	@UploadedOnHasValue bit
AS

UPDATE [dbo].[PhotoEntry]
   SET [ThemeId] = CASE WHEN @ThemeIdHasValue = 1 THEN @ThemeId ELSE [ThemeId] END
      ,[PhotographerId] = CASE WHEN @PhotographerIdHasValue = 1 THEN @PhotographerId ELSE [PhotographerId] END
      ,[FileId] = CASE WHEN @FileIdHasValue = 1 THEN @FileId ELSE [FileId] END
      ,[Caption] = CASE WHEN @CaptionHasValue = 1 THEN @Caption ELSE [Caption] END
      ,[UploadedOn] = CASE WHEN @UploadedOnHasValue = 1 THEN @UploadedOn ELSE [UploadedOn] END
	WHERE [Id] = @Id

RETURN 0

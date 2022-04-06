CREATE PROCEDURE [dbo].[Update_PhotoEntry]
	@Id						int,
	@ThemeId				int,
	@PhotographerId			int,
	@FileId					int,
	@Caption				varchar(100),
	@UploadedOn				datetime,
	@ThemeIdHasValue		bit = 1,
	@PhotographerIdHasValue	bit = 1,
	@FileIdHasValue			bit = 1,
	@CaptionHasValue		bit = 1,
	@UploadedOnHasValue		bit = 1
AS

UPDATE [dbo].[PhotoEntry]
   SET [ThemeId]		= CASE WHEN @ThemeIdHasValue		= 1 THEN @ThemeId			ELSE [ThemeId]			END
      ,[PhotographerId]	= CASE WHEN @PhotographerIdHasValue	= 1 THEN @PhotographerId	ELSE [PhotographerId]	END
      ,[FileId]			= CASE WHEN @FileIdHasValue			= 1 THEN @FileId			ELSE [FileId]			END
      ,[Caption]		= CASE WHEN @CaptionHasValue		= 1 THEN @Caption			ELSE [Caption]			END
      ,[UploadedOn]		= CASE WHEN @UploadedOnHasValue		= 1 THEN @UploadedOn		ELSE [UploadedOn]		END
	WHERE [Id] = @Id

RETURN 0

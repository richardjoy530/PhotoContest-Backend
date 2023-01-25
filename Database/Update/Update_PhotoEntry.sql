CREATE PROCEDURE [dbo].[Update_PhotoEntry]
    @Id                     int          = NULL,
    @ThemeId                int          = NULL,
    @PhotographerId         int          = NULL,
    @FileId                 int          = NULL,
    @Caption                varchar(100) = NULL,
    @UploadedOn             datetime     = NULL,
    @ThemeIdHasValue        bit = 0,
    @PhotographerIdHasValue bit = 0,
    @FileIdHasValue         bit = 0,
    @CaptionHasValue        bit = 0,
    @UploadedOnHasValue     bit = 0
AS

UPDATE [dbo].[PhotoEntry]
SET [ThemeId] = CASE WHEN @ThemeIdHasValue = 1 THEN @ThemeId ELSE [ThemeId]
END
      ,[PhotographerId] = CASE WHEN @PhotographerIdHasValue = 1 THEN @PhotographerId ELSE [PhotographerId]
END
      ,[FileId]         = CASE WHEN @FileIdHasValue         = 1 THEN @FileId         ELSE [FileId]
END
      ,[Caption]        = CASE WHEN @CaptionHasValue        = 1 THEN @Caption        ELSE [Caption]
END
      ,[UploadedOn]     = CASE WHEN @UploadedOnHasValue     = 1 THEN @UploadedOn     ELSE [UploadedOn]
END
 WHERE [Id] = @Id

RETURN 0

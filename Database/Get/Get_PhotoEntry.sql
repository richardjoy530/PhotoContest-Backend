CREATE PROCEDURE [dbo].[Get_PhotoEntry]
	@Id                     int             = NULL,
	@ThemeId                int             = NULL,
    @PhotographerId         int             = NULL,
    @FileId                 int             = NULL,
    @Caption                varchar(100)    = NULL,
    @UploadedOn             datetime        = NULL,
    @CheckId                bit = 0,
	@CheckThemeId           bit = 0,
    @CheckPhotographerId    bit = 0,
    @CheckFileId            bit = 0,
    @CheckCaption           bit = 0,
    @CheckUploadedOn        bit = 0
AS
	
SELECT [Id]
      ,[ThemeId]
      ,[PhotographerId]
      ,[FileId]
      ,[Caption]
      ,[UploadedOn]
  FROM [dbo].[PhotoEntry]
  WHERE (@CheckId		        = 1	AND [Id] = @Id)                         OR (@CheckId                = 0)
  AND	(@CheckFileId	        = 1	AND [FileId] = @FileId)                 OR (@CheckFileId            = 0)
  AND	(@CheckThemeId          = 1	AND [ThemeId] = @ThemeId)               OR (@CheckThemeId           = 0)
  AND	(@CheckCaption	        = 1	AND [Caption] = @Caption)               OR (@CheckCaption           = 0)
  AND	(@CheckUploadedOn	    = 1	AND [UploadedOn] = @UploadedOn)         OR (@CheckUploadedOn        = 0)
  AND	(@CheckPhotographerId   = 1	AND [PhotographerId] = @PhotographerId)	OR (@CheckPhotographerId    = 0)

RETURN 0

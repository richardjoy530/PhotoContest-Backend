CREATE PROCEDURE [dbo].[Get_PhotographerVoteDetails]
    @Id                     int = NULL,
    @ThemeId                int = NULL,
    @PhotographerId         int = NULL,
    @FirstId                int = NULL,
    @SecondId               int = NULL,
    @ThirdId                int = NULL,
    @CheckId                bit = 0,
    @CheckThemeId           bit = 0,
    @CheckPhotographerId    bit = 0,
    @CheckFirstId           bit = 0,
    @CheckSecondId          bit = 0,
    @CheckThirdId           bit = 0
AS

SELECT [Id]
      ,[ThemeId]
      ,[PhotographerId]
      ,[FirstId]
      ,[SecondId]
      ,[ThirdId]
  FROM [dbo].[PhotographerVoteDetails]
  WHERE (@CheckId               = 1 AND [Id] = @Id)                         OR (@CheckId                = 0)
  AND   (@CheckThemeId          = 1 AND [ThemeId] = @ThemeId)               OR (@CheckThemeId           = 0)
  AND   (@CheckPhotographerId   = 1 AND [PhotographerId] = @PhotographerId) OR (@CheckPhotographerId    = 0)
  AND   (@CheckFirstId          = 1 AND [FirstId] = @FirstId)               OR (@CheckFirstId           = 0)
  AND   (@CheckSecondId         = 1 AND [SecondId] = @SecondId)             OR (@CheckSecondId          = 0)
  AND   (@CheckThirdId          = 1 AND [ThirdId] = @ThirdId)               OR (@CheckThirdId           = 0)

RETURN 0

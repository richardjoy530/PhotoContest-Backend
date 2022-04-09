CREATE PROCEDURE [dbo].[Update_PhotoprapherVoteDetails]
	@Id                     int = NULL,
    @ThemeId                int = NULL,
    @PhotographerId         int = NULL,
    @FirstId                int = NULL,
    @SecondId               int = NULL,
    @ThirdId                int = NULL,
    @ThemeIdHasValue        bit = 0,
    @PhotographerIdHasValue bit = 0,
    @FirstIdHasValue        bit = 0,
    @SecondIdHasValue       bit = 0,
    @ThirdIdHasValue        bit = 0
AS

UPDATE [dbo].[PhotographerVoteDetails]
   SET [ThemeId] =        CASE WHEN @ThemeIdHasValue        = 1 THEN @ThemeId        ELSE [ThemeId]        END
      ,[FirstId] =        CASE WHEN @FirstIdHasValue        = 1 THEN @FirstId        ELSE [FirstId]        END
      ,[SecondId] =       CASE WHEN @SecondIdHasValue       = 1 THEN @SecondId       ELSE [SecondId]       END
      ,[ThirdId] =        CASE WHEN @ThirdIdHasValue        = 1 THEN [ThirdId]       ELSE [SecondId]       END
      ,[PhotographerId] = CASE WHEN @PhotographerIdHasValue = 1 THEN @PhotographerId ELSE [PhotographerId] END
 WHERE [Id] = @Id

RETURN 0

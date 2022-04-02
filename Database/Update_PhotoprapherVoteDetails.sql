CREATE PROCEDURE [dbo].[Update_PhotoprapherVoteDetails]
	@Id                     int,
    @ThemeId                int,
    @PhotographerId         int,
    @FirstId                int,
    @SecondId               int,
    @ThirdId                int,
    @ThemeIdHasValue        bit = 1,
    @PhotographerIdHasValue bit = 1,
    @FirstIdHasValue        bit = 1,
    @SecondIdHasValue       bit = 1,
    @ThirdIdHasValue        bit = 1
AS

UPDATE [dbo].[PhotographerVoteDetails]
   SET [ThemeId] =        CASE WHEN @ThemeIdHasValue        = 1 THEN @ThemeId        ELSE [ThemeId]        END
      ,[FirstId] =        CASE WHEN @FirstIdHasValue        = 1 THEN @FirstId        ELSE [FirstId]        END
      ,[SecondId] =       CASE WHEN @SecondIdHasValue       = 1 THEN @SecondId       ELSE [SecondId]       END
      ,[ThirdId] =        CASE WHEN @ThirdIdHasValue        = 1 THEN [ThirdId]       ELSE [SecondId]       END
      ,[PhotographerId] = CASE WHEN @PhotographerIdHasValue = 1 THEN @PhotographerId ELSE [PhotographerId] END
 WHERE [Id] = @Id

RETURN 0

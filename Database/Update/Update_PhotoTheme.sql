CREATE PROCEDURE [dbo].[Update_PhotoTheme]
	@Id						int			= NULL,
	@Theme					varchar(50)	= NULL,
	@ContestDate			datetime	= NULL,
	@ThemeHasValue			bit	= 0,
	@ContestDateHasValue	bit = 0
AS

UPDATE [dbo].[PhotoTheme]
   SET [Theme]			= CASE WHEN @ThemeHasValue			= 1 THEN @Theme			ELSE [Theme]		END
      ,[ContestDate]	= CASE WHEN @ContestDateHasValue	= 1 THEN @ContestDate	ELSE [ContestDate]	END
 WHERE [Id] = @Id

RETURN 0

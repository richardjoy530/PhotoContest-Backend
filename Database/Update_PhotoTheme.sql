CREATE PROCEDURE [dbo].[Update_PhotoTheme]
	@Id						int,
	@Theme					varchar(50),
	@ContestDate			datetime,
	@ThemeHasValue			bit,
	@ContestDateHasValue	bit
AS

UPDATE [dbo].[PhotoTheme]
   SET [Theme]			= CASE WHEN @ThemeHasValue			= 1 THEN @Theme			ELSE [Theme]		END
      ,[ContestDate]	= CASE WHEN @ContestDateHasValue	= 1 THEN @ContestDate	ELSE [ContestDate]	END
 WHERE [Id] = @Id

RETURN 0

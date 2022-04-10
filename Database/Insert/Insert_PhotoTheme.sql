CREATE PROCEDURE [dbo].[Insert_PhotoTheme]
    @Id          int OUTPUT,
    @Theme       varchar(50),
    @ContestDate datetime
AS

INSERT INTO [dbo].[PhotoTheme]
           ([Theme]
           ,[ContestDate])
     VALUES
           (@Theme
           ,@ContestDate)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0

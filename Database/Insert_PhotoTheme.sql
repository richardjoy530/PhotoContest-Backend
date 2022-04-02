CREATE PROCEDURE [dbo].[Insert_PhotoTheme]
	@Id			    int,
	@Theme		    varchar(50),
	@ContestDate    datetime
AS

INSERT INTO [dbo].[PhotoTheme]
           ([Id]
           ,[Theme]
           ,[ContestDate])
     OUTPUT [Inserted].[Id]
     VALUES
           (@Id
           ,@Theme
           ,@ContestDate)

RETURN 0

CREATE PROCEDURE [dbo].[Insert_Photographer]
	@Id int = 0,
	@UploaderName varchar(100)
AS

INSERT INTO [dbo].[Photographer]
           ([Id]
           ,[UploaderName])
        OUTPUT [INSERTED].[Id]
        VALUES
           (@Id,
            @UploaderName)

RETURN 0

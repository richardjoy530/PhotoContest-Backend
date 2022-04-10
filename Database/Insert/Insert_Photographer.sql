CREATE PROCEDURE [dbo].[Insert_Photographer]
    @Id           int OUTPUT,
    @UploaderName varchar(100)
AS

INSERT INTO [dbo].[Photographer]
           ([Id]
           ,[UploaderName])
        VALUES
           (@Id,
            @UploaderName)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0

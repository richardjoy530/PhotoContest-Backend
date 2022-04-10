CREATE PROCEDURE [dbo].[Insert_Photographer]
    @Id           int OUTPUT,
    @UploaderName varchar(100)
AS

INSERT INTO [dbo].[Photographer]
           ([UploaderName])
        VALUES
           (@UploaderName)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0

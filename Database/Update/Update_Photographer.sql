CREATE PROCEDURE [dbo].[Update_Photographer]
    @Id           int,
    @UploaderName varchar(100)
AS

UPDATE [dbo].[Photographer]
SET [UploaderName] = @UploaderName
WHERE [Id] = @Id
    RETURN 0

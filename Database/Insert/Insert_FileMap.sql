CREATE PROCEDURE [dbo].[Insert_FileMap]
    @Id       int OUTPUT,
    @FilePath varchar(50)
AS

INSERT INTO [dbo].[FileMap]
           ([FilePath])
     VALUES
           (@FilePath)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0

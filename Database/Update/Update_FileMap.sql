CREATE PROCEDURE [dbo].[Update_FileMap]
	@Id int,
	@FilePath varchar(50)
AS


UPDATE [dbo].[FileMap]
   SET [FilePath] = @FilePath
 WHERE [Id] = @Id

RETURN 0

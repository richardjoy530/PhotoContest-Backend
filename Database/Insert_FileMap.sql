CREATE PROCEDURE [dbo].[Insert_FileMap]
	@Id int,
	@FilePath varchar(50)
AS

INSERT INTO [dbo].[FileMap]
           ([Id]
           ,[FilePath])
OUTPUT [INSERTED].[Id]
     VALUES
           (@Id
           ,@FilePath)

RETURN 0

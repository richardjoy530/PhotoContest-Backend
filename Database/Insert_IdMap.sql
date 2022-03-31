CREATE PROCEDURE [dbo].[Insert_IdMap]
	@Id int,
	@ReferenceId uniqueidentifier,
    @IdType int
AS

INSERT INTO [dbo].[IdMap]
           ([ReferenceId]
           ,[Id]
           ,[IdType])
OUTPUT [INSERTED].[Id]
     VALUES
           (@ReferenceId
           ,@Id
           ,@IdType)

RETURN 0

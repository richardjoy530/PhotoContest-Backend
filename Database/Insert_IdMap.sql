CREATE PROCEDURE [dbo].[Insert_IdMap]
	@Id int,
	@ReferenceId uniqueidentifier,
    @IdType int
AS

INSERT INTO [dbo].[IdMap]
           ([ReferenceId]
           ,[Id]
           ,[IdType])
     VALUES
           (@ReferenceId
           ,@Id
           ,@IdType)

RETURN 0

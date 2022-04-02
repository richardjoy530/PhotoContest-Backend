CREATE PROCEDURE [dbo].[Insert_ScoreDetail]
	@Id         int,
	@EntryId    int,
	@Score      int
AS

INSERT INTO [dbo].[ScoreDetail]
           ([Id]
           ,[EntryId]
           ,[Score])
     OUTPUT [INSERTED].[Id]
     VALUES
           (@Id
           ,@EntryId
           ,@Score)

RETURN 0

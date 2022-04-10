CREATE PROCEDURE [dbo].[Insert_ScoreDetail]
    @Id      int OUTPUT,
    @EntryId int,
    @Score   int
AS

INSERT INTO [dbo].[ScoreDetail]
           ([Id]
           ,[EntryId]
           ,[Score])
     VALUES
           (@Id
           ,@EntryId
           ,@Score)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0

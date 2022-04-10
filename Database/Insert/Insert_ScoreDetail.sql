CREATE PROCEDURE [dbo].[Insert_ScoreDetail]
    @Id      int OUTPUT,
    @EntryId int,
    @Score   int
AS

INSERT INTO [dbo].[ScoreDetail]
           ([EntryId]
           ,[Score])
     VALUES
           (@EntryId
           ,@Score)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0

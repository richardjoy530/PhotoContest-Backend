CREATE PROCEDURE [dbo].[Insert_PhotographerVoteDetails]
    @Id             int OUTPUT,
    @ThemeId        int,
    @PhotographerId int,
    @FirstId        int,
    @SecondId       int,
    @ThirdId        int
AS

INSERT INTO [dbo].[PhotographerVoteDetails]
           ([ThemeId]
           ,[PhotographerId]
           ,[FirstId]
           ,[SecondId]
           ,[ThirdId])
     VALUES
           (@ThemeId
           ,@PhotographerId
           ,@FirstId
           ,@SecondId
           ,@ThirdId)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0

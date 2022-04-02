CREATE PROCEDURE [dbo].[Insert_PhotographerVoteDetails]
	@Id                     int,
    @ThemeId                int,
    @PhotographerId         int,
    @FirstId                int,
    @SecondId               int,
    @ThirdId                int
AS

INSERT INTO [dbo].[PhotographerVoteDetails]
           ([Id]
           ,[ThemeId]
           ,[PhotographerId]
           ,[FirstId]
           ,[SecondId]
           ,[ThirdId])
     OUTPUT [INSERTED].[Id]
     VALUES
           (@Id
           ,@ThemeId
           ,@PhotographerId
           ,@FirstId
           ,@SecondId
           ,@ThirdId)

RETURN 0

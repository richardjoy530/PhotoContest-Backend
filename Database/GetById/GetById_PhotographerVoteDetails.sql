CREATE PROCEDURE [dbo].[GetById_PhotographerVoteDetails]
    @Id int
AS

SELECT [Id]
      ,[ThemeId]
      ,[PhotographerId]
      ,[FirstId]
      ,[SecondId]
      ,[ThirdId]
  FROM [dbo].[PhotographerVoteDetails]
 WHERE [Id] = @Id

RETURN 0

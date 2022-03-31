CREATE PROCEDURE [dbo].[GetAll_PhotographerVoteDetails]
AS

SELECT [Id]
      ,[ThemeId]
      ,[PhotographerId]
      ,[FirstId]
      ,[SecondId]
      ,[ThirdId]
  FROM [dbo].[PhotographerVoteDetails]

RETURN 0

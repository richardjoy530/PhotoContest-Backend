CREATE PROCEDURE [dbo].[GetAll_ScoreDetail]
AS

SELECT [Id]
      ,[EntryId]
      ,[Score]
  FROM [dbo].[ScoreDetail]

RETURN 0

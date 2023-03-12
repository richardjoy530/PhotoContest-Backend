CREATE PROCEDURE ScoreInfo_GetById @Id int
AS

SELECT *
FROM ScoreInfo
WHERE Id = @Id
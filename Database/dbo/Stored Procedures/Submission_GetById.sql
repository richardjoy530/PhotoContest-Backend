CREATE PROCEDURE Submission_GetById @Id int
AS

SELECT *
FROM Submission
WHERE Id = @Id
CREATE PROCEDURE Contest_Delete @Id int
AS

DELETE
FROM Contest
WHERE Id = @Id
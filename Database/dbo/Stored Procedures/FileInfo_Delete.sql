CREATE PROCEDURE FileInfo_Delete @Id int
AS

DELETE
FROM FileInfo
WHERE Id = @Id
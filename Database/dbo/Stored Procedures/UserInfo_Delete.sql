CREATE PROCEDURE UserInfo_Delete @Id int
AS

DELETE
FROM UserInfo
WHERE Id = @Id
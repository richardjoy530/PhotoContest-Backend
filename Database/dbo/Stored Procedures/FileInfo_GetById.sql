CREATE PROCEDURE FileInfo_GetById
@Id int
AS

SELECT * FROM FileInfo WHERE Id = @Id And IsDeleted = 0
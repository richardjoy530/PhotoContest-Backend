CREATE PROCEDURE FileInfo_Delete
    @Id int
AS

Update FileInfo SET IsDeleted = 1 WHERE Id = @Id
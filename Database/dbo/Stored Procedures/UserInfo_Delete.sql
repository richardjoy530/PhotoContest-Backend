CREATE PROCEDURE UserInfo_Delete
    @Id int
AS

Update UserInfo SET IsDeleted = 1 WHERE Id = @Id
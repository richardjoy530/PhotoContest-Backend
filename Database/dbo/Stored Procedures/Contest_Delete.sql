CREATE PROCEDURE Contest_Delete
    @Id int
AS

Update Contest SET IsDeleted = 1 WHERE Id = @Id
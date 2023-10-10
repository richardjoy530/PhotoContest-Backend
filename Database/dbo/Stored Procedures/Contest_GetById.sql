CREATE PROCEDURE Contest_GetById
    @Id int
AS

SELECT * FROM Contest WHERE Id = @Id And IsDeleted = 0
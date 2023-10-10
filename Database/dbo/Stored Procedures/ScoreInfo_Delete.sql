CREATE PROCEDURE ScoreInfo_Delete
    @Id int
AS

Update ScoreInfo SET IsDeleted = 1 WHERE Id = @Id
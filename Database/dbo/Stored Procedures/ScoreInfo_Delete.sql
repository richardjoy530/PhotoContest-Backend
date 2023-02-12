CREATE PROCEDURE ScoreInfo_Delete
@Id int
AS

DELETE FROM ScoreInfo WHERE Id = @Id
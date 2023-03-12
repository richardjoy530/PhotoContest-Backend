CREATE PROCEDURE Submission_Delete
@Id int
AS

DELETE FROM Submission WHERE Id = @Id
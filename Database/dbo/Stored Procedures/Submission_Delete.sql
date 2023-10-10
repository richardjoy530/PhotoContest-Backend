CREATE PROCEDURE Submission_Delete
    @Id int
AS

Update Submission SET IsDeleted = 1 WHERE Id = @Id
CREATE PROCEDURE Score_Insert
    @Id             int OUTPUT,
    @SubmissionId   int,
    @Score          int
AS

INSERT INTO Score (SubmissionId, Score)
    VALUES (@SubmissionId, @Score)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0
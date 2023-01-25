CREATE PROCEDURE Score_Update
    @Id                 int,
    @SubmissionId       int = NULL,
    @Score              int = NULL,
    @UpdateSubmissionId bit = 0,
    @updateScore        bit = 0
AS

UPDATE Score SET 
    SubmissionId    = IIF(@UpdateSubmissionId   = 1, @SubmissionId, SubmissionId),
    Score           = IIF(@updateScore          = 1, @Score,        Score)
 WHERE Id = @Id
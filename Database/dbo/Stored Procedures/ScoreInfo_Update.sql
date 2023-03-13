CREATE PROCEDURE ScoreInfo_Update
    @Id                 int,
    @SubmissionId       int = NULL,
    @Score              int = NULL,
    @UpdateSubmissionId bit = 0,
    @updateScore        bit = 0
AS

UPDATE ScoreInfo SET 
    SubmissionId    = IIF(@UpdateSubmissionId   = 1, @SubmissionId, SubmissionId),
    Score           = IIF(@updateScore          = 1, @Score,        Score)
 WHERE Id = @Id And IsDeleted = 0
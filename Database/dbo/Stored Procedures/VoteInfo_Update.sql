CREATE PROCEDURE VoteInfo_Update @Id                 int,
    @FirstId            int = NULL,
    @SecondId           int = NULL,
    @ThirdId            int = NULL,
    @ContestId          int = NULL,
    @UserId             int = NULL,
    @UpdateFirstId      bit = 0,
    @UpdateSecondId     bit = 0,
    @UpdateThirdId      bit = 0,
    @UpdateContestId    bit = 0,
    @UpdateUserId       bit = 0
AS

UPDATE VoteInfo
SET FirstId   = IIF(@UpdateFirstId = 1, @FirstId, FirstId),
    SecondId  = IIF(@updateSecondId = 1, @SecondId, SecondId),
    ThirdId   = IIF(@updateThirdId = 1, @ThirdId, ThirdId),
    ContestId = IIF(@updateContestId = 1, @ContestId, ContestId),
    UserId    = IIF(@updateUserId = 1, @UserId, UserId)
WHERE Id = @Id
CREATE PROCEDURE VoteInfo_Insert
    @Id         int OUTPUT,
    @FirstId    int,
    @SecondId   int,
    @ThirdId    int,
    @ContestId  int,
    @UserId     int
AS

INSERT INTO VoteInfo (FirstId, SecondId, ThirdId, ContestId, UserId)
    VALUES (@FirstId, @SecondId, @ThirdId, @ContestId, @UserId)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0
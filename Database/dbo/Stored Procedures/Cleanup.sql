CREATE PROCEDURE Cleanup
    AS

DELETE FROM dbo.VoteInfo WHERE IsDeleted = 1
DELETE FROM dbo.ScoreInfo WHERE IsDeleted = 1
DELETE FROM dbo.Submission WHERE IsDeleted = 1

DELETE FROM dbo.FileInfo WHERE IsDeleted = 1
DELETE FROM dbo.UserInfo WHERE IsDeleted = 1
DELETE FROM dbo.Contest WHERE IsDeleted = 1



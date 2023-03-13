CREATE PROCEDURE VoteInfo_Delete
@Id int
AS

Update VoteInfo SET IsDeleted = 1 WHERE Id = @Id
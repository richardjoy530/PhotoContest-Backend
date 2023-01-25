CREATE PROCEDURE VoteInfo_Delete
@Id int
AS

DELETE FROM VoteInfo WHERE Id = @Id
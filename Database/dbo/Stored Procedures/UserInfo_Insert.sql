CREATE PROCEDURE [dbo].[UserInfo_Insert]
    @Id     int OUTPUT,
    @Name       varchar(100) = NULL,
    @RefId       varchar(100) = NULL,
	@Email varchar(100) = NULL,
	@RegisteredDate date = NULL
AS

INSERT INTO UserInfo (Name, RefId, Email, RegisteredDate) VALUES (@Name, @RefId, @Email, @RegisteredDate)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0
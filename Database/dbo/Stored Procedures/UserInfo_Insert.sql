CREATE PROCEDURE UserInfo_Insert
    @Id     int OUTPUT,
    @Name   varchar(100)
AS

INSERT INTO UserInfo (Name) VALUES (@Name)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0
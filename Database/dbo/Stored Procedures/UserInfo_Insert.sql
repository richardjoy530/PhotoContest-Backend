CREATE PROCEDURE UserInfo_Insert
    @Id     int OUTPUT,
    @Name   varchar(50),
    @RefId   varchar(100)
AS

INSERT INTO UserInfo (Name, RefId) VALUES (@Name, @RefId)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0
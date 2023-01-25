CREATE PROCEDURE UserInfo_Update
    @Id         int,
    @Name       int = NULL,
    @UpdateName bit = 0
AS

UPDATE UserInfo SET Name = IIF(@UpdateName = 1, @Name, Name) WHERE Id = @Id
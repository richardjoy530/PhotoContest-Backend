CREATE PROCEDURE UserInfo_Update
    @Id         int,
    @Name       varchar(50) = NULL,
    @RefId       varchar(100) = NULL,
    @UpdateName bit = 0,
    @UpdateRefId bit = 0
AS

UPDATE UserInfo SET Name = IIF(@UpdateName = 1, @Name, Name)
                    RefId = IIF(@UpdateRefId = 1, @RefId, RefId)
                WHERE Id = @Id

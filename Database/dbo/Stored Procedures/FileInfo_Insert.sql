CREATE PROCEDURE FileInfo_Insert
    @Id     int OUTPUT,
    @Path   varchar(50)
AS

INSERT INTO FileInfo (Path) VALUES (@Path)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0
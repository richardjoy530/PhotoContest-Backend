CREATE PROCEDURE [dbo].[FileInfo_Update]
    @Id             int,
    @Path          varchar(200) = NULL,
    @UpdatePath    bit = 0
AS

UPDATE FileInfo
SET Path = IIF(@UpdatePath = 1, @Path, Path)
WHERE Id = @Id
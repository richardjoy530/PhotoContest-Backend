CREATE PROCEDURE Submission_Insert
    @Id             int OUTPUT,
    @ContestId      int,
    @FileInfoId     int,
    @Caption        varchar(100),
    @UploadedOn     datetime
AS

INSERT INTO Submission (ContestId, FileInfoId, Caption, UploadedOn)
    VALUES (@ContestId, @FileInfoId, @Caption, @UploadedOn)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0
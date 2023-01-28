CREATE PROCEDURE Submission_Insert
    @Id             int OUTPUT,
    @ContestId      int,
    @FileInfoId     int,
    @Caption        varchar(100),
    @UploadedOn     datetime,
    @UserId         int
AS

INSERT INTO Submission (ContestId, FileInfoId, Caption, UploadedOn, UserId)
    VALUES (@ContestId, @FileInfoId, @Caption, @UploadedOn, @UserId)

SELECT @Id = SCOPE_IDENTITY();

RETURN 0
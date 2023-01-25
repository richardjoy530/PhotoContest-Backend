CREATE PROCEDURE Submission_Update
    @Id                 int,
    @ContestId          int = NULL,
    @FileInfoId         int = NULL,
    @Caption            varchar(100)    = NULL,
    @UploadedOn         datetime        = NULL,
    @UpdateContestId    bit = 0,
    @UpdateFileInfoId   bit = 0,
    @UpdateCaption      bit = 0,
    @UpdateUploadedOn   bit = 0
AS

UPDATE Submission SET 
    ContestId   = IIF(@UpdateContestId  = 1, @ContestId,    ContestId),
    FileInfoId  = IIF(@updateFileInfoId = 1, @FileInfoId,   FileInfoId),
    Caption     = IIF(@UpdateCaption    = 1, @Caption,      Caption),
    UploadedOn  = IIF(@UpdateUploadedOn = 1, @UploadedOn,   UploadedOn)
 WHERE Id = @Id
CREATE PROCEDURE [dbo].[Submission_Update]
    @Id                 int,
    @ContestId          int = NULL,
    @FileInfoId         int = NULL,
    @Caption            varchar(100)    = NULL,
    @RefId            varchar(100)    = NULL,
    @UploadedOn         datetime        = NULL,
    @UserId             int             = NULL,
    @UpdateContestId    bit = 0,
    @UpdateFileInfoId   bit = 0,
    @UpdateCaption      bit = 0,
    @UpdateUploadedOn   bit = 0,
    @UpdateRefId   bit = 0,
    @UpdateUserId   bit = 0
AS

UPDATE Submission SET 
    ContestId   = IIF(@UpdateContestId  = 1, @ContestId,    ContestId),
    FileInfoId  = IIF(@updateFileInfoId = 1, @FileInfoId,   FileInfoId),
    Caption     = IIF(@UpdateCaption    = 1, @Caption,      Caption),
    RefId     = IIF(@UpdateRefId    = 1, @RefId,      RefId),
    UploadedOn  = IIF(@UpdateUploadedOn = 1, @UploadedOn,   UploadedOn),
    UserId  = IIF(@UpdateUserId = 1, @UserId,   UserId)
 WHERE Id = @Id And IsDeleted = 0
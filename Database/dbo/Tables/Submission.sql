CREATE TABLE [dbo].[Submission] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [ContestId]  INT           NOT NULL,
    [FileInfoId] INT           NOT NULL,
    [Caption]    VARCHAR (100) NOT NULL,
    [UploadedOn] DATETIME      DEFAULT (sysdatetime()) NOT NULL,
    CONSTRAINT [PK_Submission] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContestId_Contest_Submission] FOREIGN KEY ([ContestId]) REFERENCES [dbo].[Contest] ([Id]),
    CONSTRAINT [FK_FileId_FileInfo] FOREIGN KEY ([FileInfoId]) REFERENCES [dbo].[FileInfo] ([Id])
);


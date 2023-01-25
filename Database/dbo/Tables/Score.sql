CREATE TABLE [dbo].[Score] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [SubmissionId] INT NOT NULL,
    [Score]        INT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubmissionId_Submission] FOREIGN KEY ([SubmissionId]) REFERENCES [dbo].[Submission] ([Id])
);


CREATE TABLE [dbo].[ScoreInfo]
(
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [SubmissionId] INT                 NOT NULL,
    [Score]        INT                 NOT NULL,
    [IsDeleted]    BIT DEFAULT ((0))   NOT NULL,
    
    CONSTRAINT [PK_Score] 
        PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubmissionId_Submission] 
        FOREIGN KEY ([SubmissionId]) REFERENCES [dbo].[Submission] ([Id])
);


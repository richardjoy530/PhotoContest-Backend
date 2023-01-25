CREATE TABLE [dbo].[VoteInfo] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [FirstId]   INT NULL,
    [SecondId]  INT NULL,
    [ThirdId]   INT NULL,
    [ContestId] INT NOT NULL,
    [UserId]    INT NOT NULL,
    CONSTRAINT [PK_VoteInfo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContestId_VoteInfo_Contest] FOREIGN KEY ([ContestId]) REFERENCES [dbo].[Contest] ([Id]),
    CONSTRAINT [FK_FirstId_Submission] FOREIGN KEY ([FirstId]) REFERENCES [dbo].[Submission] ([Id]),
    CONSTRAINT [FK_SecondId_Submission] FOREIGN KEY ([SecondId]) REFERENCES [dbo].[Submission] ([Id]),
    CONSTRAINT [FK_ThirdId_Submission] FOREIGN KEY ([ThirdId]) REFERENCES [dbo].[Submission] ([Id]),
    CONSTRAINT [FK_UserId_UserInfo] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserInfo] ([Id])
);


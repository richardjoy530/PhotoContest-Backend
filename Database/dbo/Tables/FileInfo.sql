CREATE TABLE [dbo].[FileInfo] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Path] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_FileInfo] PRIMARY KEY CLUSTERED ([Id] ASC)
);


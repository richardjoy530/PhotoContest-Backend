CREATE TABLE [dbo].[FileInfo] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Path]      VARCHAR (200) NOT NULL,
    [IsDeleted] BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FileInfo] PRIMARY KEY CLUSTERED ([Id] ASC)
);


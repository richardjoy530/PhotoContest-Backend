CREATE TABLE [dbo].[UserInfo] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    [RefId] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[UserInfo] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [RefId]            VARCHAR (100) NULL,
    [Email]            VARCHAR (100) NULL,
    [RegistrationDate] DATE          NULL,
    CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UserInfo_pk] UNIQUE NONCLUSTERED ([RefId] ASC),
    CONSTRAINT [UserInfo_pk2] UNIQUE NONCLUSTERED ([RefId] ASC)
);


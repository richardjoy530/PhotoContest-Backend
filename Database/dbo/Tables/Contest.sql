CREATE TABLE [dbo].[Contest] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Theme]     VARCHAR (100) NOT NULL,
    [EndDate]   DATETIME      NOT NULL,
    [IsDeleted] BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Contest] PRIMARY KEY CLUSTERED ([Id] ASC)
);


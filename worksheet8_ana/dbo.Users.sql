CREATE TABLE [dbo].[Users] (
    [Id]          INT           NOT NULL,
    [Login]       VARCHAR (20)  NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Password]    VARCHAR (64)  NOT NULL,
    [Description] TEXT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


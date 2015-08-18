-- 
-- Migration version: 20150817234739
-- Filename: 20150817234739_CreateTableTodos_up.sql
-- 

CREATE TABLE [dbo].[Todos] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [UserId]  UNIQUEIDENTIFIER NOT NULL,
    [Content] NVARCHAR (MAX)   NOT NULL,
	[DateCreated] DATETIME NOT NULL DEFAULT(GetUtcDate())
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);
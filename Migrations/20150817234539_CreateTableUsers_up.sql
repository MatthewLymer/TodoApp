-- 
-- Migration version: 20150817234539
-- Filename: 20150817234539_CreateTableUsers_up.sql
-- 

CREATE TABLE [dbo].[Users] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [Email] VARCHAR (64)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)
);
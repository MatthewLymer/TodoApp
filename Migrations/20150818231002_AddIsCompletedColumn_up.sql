-- 
-- Migration version: 20150818231002
-- Filename: 20150818231002_AddIsCompletedColumn_up.sql
-- 

ALTER TABLE Todos ADD IsCompleted BIT DEFAULT 0
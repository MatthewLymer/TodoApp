-- 
-- Migration version: 20150817235444
-- Filename: 20150817235444_AddTodoSeedData_up.sql
-- 

INSERT INTO Todos(Id, UserId, Content) VALUES(NEWID(), 'f5ff7c13-c0dc-42fa-9032-face91419fe6', 'Wash the windows')
INSERT INTO Todos(Id, UserId, Content) VALUES(NEWID(), 'f5ff7c13-c0dc-42fa-9032-face91419fe6', 'Mow the lawn')
INSERT INTO Todos(Id, UserId, Content) VALUES(NEWID(), 'f5ff7c13-c0dc-42fa-9032-face91419fe6', 'Buy cat food')
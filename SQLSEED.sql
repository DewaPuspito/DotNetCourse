-- CREATE DATABASE DotNetCourseDatabase;
-- GO
 
USE DotNetCourseDatabase;
GO
 
-- CREATE SCHEMA TutorialAppSchema;
-- GO

CREATE TABLE TutorialAppSchema.Users
(
    UserId INT IDENTITY(1, 1) PRIMARY KEY
    , FirstName NVARCHAR(50)
    , LastName NVARCHAR(50)
    , Email NVARCHAR(50) UNIQUE
    , Gender NVARCHAR(50)
    , Active BIT
);

CREATE TABLE TutorialAppSchema.UserSalary
(
    UserId INT UNIQUE
    , Salary DECIMAL(18, 4)
);

CREATE TABLE TutorialAppSchema.UserJobInfo
(
    UserId INT UNIQUE
    , JobTitle NVARCHAR(50)
    , Department NVARCHAR(50),
);

SELECT  [UserId]
        , [FirstName]
        , [LastName]
        , [Email]
        , [Gender]
        , [Active]
  FROM  TutorialAppSchema.Users;

SELECT  [UserId]
        , [Salary]
  FROM  TutorialAppSchema.UserSalary;

SELECT  [UserId]
        , [JobTitle]
        , [Department]
  FROM  TutorialAppSchema.UserJobInfo;
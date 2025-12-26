 
USE DotNetCourseDatabase;
GO

SELECT [Users].[UserId],
[Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
[Users].[Email],
[Users].[Gender],
[Users].[Active] FROM TutorialAppSchema.Users AS Users
WHERE Users.Active = 0
ORDER BY Users.UserId DESC

SELECT [Users].[UserId],
[Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
[UserJobInfo].[JobTitle],
[UserJobInfo].[Department],     
[UserSalary].[Salary],
[Users].[Email],
[Users].[Gender],
[Users].[Active]

FROM TutorialAppSchema.Users AS Users
--INNER JOIN
JOIN TutorialAppSchema.UserSalary AS UserSalary
ON UserSalary.UserId = Users.UserId 
LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
ON UserJobInfo.UserId = Users.UserId 
WHERE Users.Active = 1
ORDER BY Users.UserId ASC

-- DELETE FROM TutorialAppSchema.UserSalary WHERE UserId BETWEEN 250 AND 750    

SELECT [UserSalary].[UserId],
[UserSalary].[Salary]
FROM TutorialAppSchema.UserSalary AS UserSalary
WHERE EXISTS (
    SELECT * FROM TutorialAppSchema.UserJobInfo AS UserJobInfo 
WHERE UserJobInfo.UserId = UserSalary.UserId)
AND UserId <> 7

SELECT [UserId],
[Salary] FROM TutorialAppSchema.UserSalary
-- UNION -- Distinct between two datasets
UNION ALL
SELECT [UserId],
[Salary] FROM TutorialAppSchema.UserSalary


SELECT [Users].[UserId],
[Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
[UserJobInfo].[JobTitle],
[UserJobInfo].[Department],     
[UserSalary].[Salary],
[Users].[Email],
[Users].[Gender],
[Users].[Active]

FROM TutorialAppSchema.Users AS Users
--INNER JOIN
JOIN TutorialAppSchema.UserSalary AS UserSalary
ON UserSalary.UserId = Users.UserId 
LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
ON UserJobInfo.UserId = Users.UserId 
WHERE Users.Active = 1
ORDER BY Users.UserId ASC

-- CREATE CLUSTERED INDEX cix_UserSalary_UserId ON TutorialAppSchema.UserSalary(UserId)

-- CREATE NONCLUSTERED INDEX ix_UserJobInfo_JobTitle ON TutorialAppSchema.UserJobInfo(JobTitle) INCLUDE (Department)

CREATE NONCLUSTERED INDEX fix_Users_Active ON TutorialAppSchema.Users(Active) 
INCLUDE ([Email], [FirstName], [LastName]) --Also includes UserId because it is our clustered index
WHERE Active = 1






SELECT ISNULL
([UserJobInfo].[Department], 'No Department Listed') AS Department, 
SUM([UserSalary].[Salary]) AS Salary,
MIN([UserSalary].[Salary]) AS MINSalary,
MAX([UserSalary].[Salary]) AS MAXSalary,
AVG([UserSalary].[Salary]) AS AVGSalary,
COUNT(*) AS PeopleInDepartment,
STRING_AGG(Users.UserId, ', ') AS UserIds

FROM TutorialAppSchema.Users AS Users
--INNER JOIN
JOIN TutorialAppSchema.UserSalary AS UserSalary
ON UserSalary.UserId = Users.UserId 
LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
ON UserJobInfo.UserId = Users.UserId 
WHERE Users.Active = 1
GROUP BY [UserJobInfo].[Department]
ORDER BY ISNULL ([UserJobInfo].[Department], 'No Department Listed') ASC








SELECT [Users].[UserId],
[Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
[UserJobInfo].[JobTitle],
[UserJobInfo].[Department],  
DepartmentAverage.AVGSalary,   
[UserSalary].[Salary],
[Users].[Email],
[Users].[Gender],
[Users].[Active]

FROM TutorialAppSchema.Users AS Users
--INNER JOIN
JOIN TutorialAppSchema.UserSalary AS UserSalary
ON UserSalary.UserId = Users.UserId 
LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
ON UserJobInfo.UserId = Users.UserId 
-- OUTER APPLY ( --Similar to LEFT JOIN
CROSS APPLY ( -- Similar to JOIN
SELECT ISNULL ([UserJobInfo2].[Department], 'No Department Listed') AS Department, 
AVG([UserSalary2].[Salary]) AS AVGSalary
FROM TutorialAppSchema.UserSalary AS UserSalary2
LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2
ON UserJobInfo2.UserId = UserSalary2.UserId 
WHERE [UserJobInfo2].[Department] = [UserJobInfo2].[Department]
GROUP BY [UserJobInfo2].[Department]
) AS DepartmentAverage
WHERE Users.Active = 1
AND DepartmentAverage.AVGSalary IS NOT NULL
ORDER BY Users.UserId DESC 

SELECT GETDATE()

SELECT DATEADD(YEAR, -5, GETDATE())

SELECT DATEDIFF(MINUTE, GETDATE(), DATEADD(YEAR, -5, GETDATE())) --Returns Positive

SELECT DATEDIFF(MINUTE, DATEADD(YEAR, -5, GETDATE()), GETDATE()) --Returns Negative


ALTER TABLE TutorialAppSchema.UserSalary ADD AVGSalary DECIMAL(18,4)

SELECT * FROM TutorialAppSchema.UserSalary

UPDATE UserSalary
    SET UserSalary.AVGSalary = DepartmentAverage.AVGSalary
    FROM TutorialAppSchema.UserSalary AS UserSalary
    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
ON UserJobInfo.UserId = UserSalary.UserId 
CROSS APPLY ( -- Similar to JOIN
SELECT ISNULL ([UserJobInfo2].[Department], 'No Department Listed') AS Department, 
AVG([UserSalary2].[Salary]) AS AVGSalary
FROM TutorialAppSchema.UserSalary AS UserSalary2
LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2
ON UserJobInfo2.UserId = UserSalary2.UserId 
WHERE ISNULL([UserJobInfo2].[Department], 'No Department Listed') = ISNULL([UserJobInfo].[Department], 'No Department Listed')
GROUP BY [UserJobInfo2].[Department]
) AS DepartmentAverage
use DotNetCourseDatabase
GO

ALTER PROCEDURE TutorialAppSchema.spUsers_Get
-- EXEC TutorialAppSchema.spUsers_Get @UserId=10
    @UserId INT = NULL
    , @Active BIT = NULL
AS
BEGIN
    -- IF OBJECT_ID('tempdb..#AverageDeptSalary', 'U') IS NOT NULL
    --     BEGIN
    --         DROP TABLE #AverageDeptSalary
    --     END

    DROP TABLE IF EXISTS #AverageDeptSalary

    SELECT UserJobInfo.Department
        , AVG(UserSalary.Salary) AVGSalary
        INTO #AverageDeptSalary
    FROM TutorialAppSchema.Users AS Users
        LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON UserSalary.UserId = Users.UserId
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
            ON UserJobInfo.UserId = Users.UserId
        GROUP BY UserJobInfo.Department

    CREATE CLUSTERED INDEX cix_AverageDeptSalary_Department ON #AverageDeptSalary(Department)

    SELECT [Users].[UserId],
        [Users].[FirstName],
        [Users].[LastName],
        [Users].[Email],
        [Users].[Gender],
        [Users].[Active],
        UserSalary.Salary,
        UserJobInfo.Department,
        UserJobInfo.JobTitle,
        AVGSalary.AVGSalary
    FROM TutorialAppSchema.Users AS Users
        LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON UserSalary.UserId = Users.UserId
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
            ON UserJobInfo.UserId = Users.UserId
        LEFT JOIN #AverageDeptSalary as AVGSalary
            ON AVGSalary.Department = UserJobInfo.Department
        -- OUTER APPLY (
        -- SELECT UserJobInfo2.Department
        --     , AVG(UserSalary2.Salary) AVGSalary
        -- FROM TutorialAppSchema.Users AS Users
        --     LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary2
        --         ON UserSalary2.UserId = Users.UserId
        --     LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2 
        --         ON UserJobInfo2.UserId = Users.UserId
        --     WHERE UserJobInfo2.Department = UserJobInfo.Department
        --     GROUP BY UserJobInfo2.Department
        -- ) AS AVGSalary
        WHERE Users.UserId = ISNULL(@UserId, Users.UserId)
            AND ISNULL(Users.Active, 0)= COALESCE(@Active, Users.Active, 0)
END
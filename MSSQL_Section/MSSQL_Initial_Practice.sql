-- CREATE DATABASE DotNetCourseDatabase;
-- GO
 
USE DotNetCourseDatabase;
GO
 
-- CREATE SCHEMA TutorialAppSchema;
-- GO

-- CREATE TABLE TutorialAppSchema.Computer (
--     ComputerId INT IDENTITY(1,1) PRIMARY KEY,
--     Motherboard NVARCHAR(255),
--     CPUCores INT,
--     HasWiFi BIT,
--     HasLTE BIT,
--     ReleaseDate DATETIME,
--     Price DECIMAL(18, 4),
--     VideoCard NVARCHAR(255),
-- );
-- GO

SELECT * FROM TutorialAppSchema.Computer;

-- SET IDENTITY_INSERT TutorialAppSchema.Computer ON;
-- SET IDENTITY_INSERT TutorialAppSchema.Computer OFF;

INSERT INTO TutorialAppSchema.Computer (
    [Motherboard],
    [CPUCores],
    [HasWiFi],
    [HasLTE],
    [ReleaseDate],
    [Price],
    [VideoCard]
) VALUES (
    'ASUS ROG STRIX B550-F', 8, 1, 0, '2021-05-15', 1499.99, 'NVIDIA GeForce RTX 3080'
)

DELETE FROM TutorialAppSchema.Computer
WHERE ComputerId = 1;

UPDATE TutorialAppSchema.Computer SET CPUCores = 4

UPDATE TutorialAppSchema.Computer SET CPUCores = 8
WHERE ComputerId = 2;

UPDATE TutorialAppSchema.Computer SET CPUCores = NULL
WHERE ReleaseDate > '2020-01-01';

TRUNCATE TABLE TutorialAppSchema.Computer;

SELECT [ComputerId],
[Motherboard],
ISNULL ([CPUCores], 4) AS CPUCores,
[HasWiFi],
[HasLTE],
[ReleaseDate],
[Price],
[VideoCard] FROM TutorialAppSchema.Computer
ORDER BY ReleaseDate DESC;
GO
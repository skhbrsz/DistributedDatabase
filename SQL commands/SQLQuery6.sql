USE DistributedDatabase;
GO

UPDATE Furniture
SET
    Price = 19000.00,
    UpdatedAt = SYSUTCDATETIME()
WHERE Name = N'Modern Sofa';
GO

SELECT * FROM Furniture
WHERE Name = N'Modern Sofa';
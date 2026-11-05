USE DistributedDatabase;
GO

UPDATE Furniture
SET Price = 5000.00,
    UpdatedAt = SYSUTCDATETIME()
WHERE Id = 5;
GO

SELECT * FROM Furniture WHERE Id = 5;
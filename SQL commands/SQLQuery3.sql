USE DistributedDatabase;
GO

UPDATE Furniture
SET Price = 4500.00
WHERE Id = 5;
GO

SELECT Id, Name, Price
FROM Furniture
WHERE Id = 5;
GO
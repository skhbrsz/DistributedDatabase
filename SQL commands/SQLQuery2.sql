USE DistributedDatabase;
GO

INSERT INTO Furniture (Name, Category, Material, Color, Price)
VALUES
('Modern Sofa', 'Sofa', 'Velvet', 'Grey', 18500.00),
('Dining Table', 'Table', 'Oak', 'Natural', 12000.00),
('Office Chair', 'Chair', 'Leather', 'Black', 6500.00),
('Wardrobe', 'Wardrobe', 'MDF', 'White', 15000.00),
('Coffee Table', 'Table', 'Wood', 'Brown', 4200.00);
GO

SELECT * FROM Furniture;
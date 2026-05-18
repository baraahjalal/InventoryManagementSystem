-- ============================================================
-- Inventory Management System
-- File        : SeedData.sql
-- Description : Initial data for all 9 tables
-- Run order   : 3rd  (after CreateDatabase.sql AND Triggers.sql)
-- ============================================================
-- NOTE ON AUDITLOG:
--   Because Triggers.sql runs before this file, the triggers
--   are active during seeding. This means AuditLog will
--   automatically receive entries for every INSERT below —
--   which is intentional and demonstrates the triggers work.
-- ============================================================
-- INSERT ORDER (respects Foreign Key dependencies):
--   1. Categories
--   2. Users
--   3. Suppliers
--   4. StorageZones
--   5. Products
--   6. ProductSpecifications
--   7. StockMovements   ← triggers write to AuditLog here
--   8. ProductItems     ← generated via WHILE loops
-- ============================================================

USE InventoryDB;
GO

-- ============================================================
-- 1. CATEGORIES
-- ============================================================
INSERT INTO Categories (CategoryName) VALUES
    ('Laptops'),
    ('Phones'),
    ('Printers'),
    ('Monitors'),
    ('Accessories');
GO

-- ============================================================
-- 2. USERS
-- Trigger tr_Users_Audit fires for each row → 5 AuditLog entries
-- ============================================================
INSERT INTO Users (Username, Password, Role, IsAdmin) VALUES
    ('admin.super',  '123', 'System Administrator', 1),
    ('ahmed.clerk',  '123', 'Employee',              0),
    ('sara.reports', '123', 'Employee',              0),
    ('omar.proc',    '123', 'Employee',              0),
    ('tariq.sales',  '123', 'Employee',              0);
GO

-- ============================================================
-- 3. SUPPLIERS  (no ContactPerson by design)
-- Trigger tr_Suppliers_Audit fires for each row
-- ============================================================
INSERT INTO Suppliers (SupplierName, Phone, Email, IsActive) VALUES
    ('TechSource Inc.',     '555-010100', 'contact@techsource.com',  1),
    ('Global Electronics',  '555-020200', 'contact@globalelec.com',  1),
    ('Office Depot',        '555-030300', 'contact@officedepot.com', 1),
    ('Samsung Electronics', '555-050500', 'contact@samsung.com',     1),
    ('Mega Traders',        '555-040400', 'contact@megatraders.com', 1);
GO

-- ============================================================
-- 4. STORAGE ZONES  (one zone per category)
-- ============================================================
INSERT INTO StorageZones (ZoneName, CategoryName) VALUES
    ('Aisle A-1: Laptops',     'Laptops'),
    ('Aisle B-2: Phones',      'Phones'),
    ('Aisle C-3: Accessories', 'Accessories'),
    ('Aisle D-4: Printers',    'Printers'),
    ('Aisle E-5: Monitors',    'Monitors');
GO

-- ============================================================
-- 5. PRODUCTS  (no Quantity column — derived from ProductItems)
-- Trigger tr_Products_Audit fires for each row
-- ============================================================
INSERT INTO Products (SerialNumber, ProductName, CategoryName, Price) VALUES
    ('APP-MBP-2023',   'MacBook Pro 14" M3',          'Laptops',     1999.00),
    ('APP-IPH-2023',   'iPhone 15 Pro Max',            'Phones',      1199.00),
    ('SAM-S24U-001',   'Samsung Galaxy S24 Ultra',     'Phones',      1299.00),
    ('PRN-HP-2024',    'HP LaserJet Pro',              'Printers',     450.00),
    ('DEL-XPS-1590',   'Dell XPS 15',                  'Laptops',     1750.00),
    ('LEN-TPX1-G10',   'Lenovo ThinkPad X1 Carbon',    'Laptops',     1600.00),
    ('MON-LG-27GN',    'LG UltraGear 27"',             'Monitors',     350.00),
    ('MON-DEL-U32',    'Dell UltraSharp 32"',          'Monitors',     750.00),
    ('ACC-LOG-MX3S',   'Logitech MX Master 3S',        'Accessories',   99.00),
    ('ACC-KEY-K8P',    'Keychron K8 Pro',              'Accessories',  110.00),
    ('PRN-EPS-L3250',  'Epson EcoTank L3250',          'Printers',     220.00),
    ('APP-MBA-2022',   'MacBook Air M2',               'Laptops',     1099.00);
GO

-- ============================================================
-- 6. PRODUCT SPECIFICATIONS
-- PK = (ProductSerial, SpecKey) — one value per spec per product
-- ============================================================
INSERT INTO ProductSpecifications (ProductSerial, SpecKey, SpecValue) VALUES
    -- Laptops
    ('APP-MBP-2023',  'Processor',      'Apple M3'),
    ('APP-MBP-2023',  'RAM',            '16GB'),
    ('APP-MBA-2022',  'Processor',      'Apple M2'),
    ('APP-MBA-2022',  'RAM',            '8GB'),
    ('DEL-XPS-1590',  'Processor',      'Intel Core i7'),
    ('DEL-XPS-1590',  'RAM',            '32GB'),
    ('LEN-TPX1-G10',  'Processor',      'Intel Core i7'),
    ('LEN-TPX1-G10',  'RAM',            '16GB'),
    -- Phones
    ('APP-IPH-2023',  'Storage',        '256GB'),
    ('APP-IPH-2023',  'Color',          'Titanium'),
    ('SAM-S24U-001',  'Storage',        '512GB'),
    ('SAM-S24U-001',  'Color',          'Phantom Black'),
    -- Printers
    ('PRN-HP-2024',   'Type',           'Laser'),
    ('PRN-HP-2024',   'Color Support',  'Monochrome'),
    ('PRN-EPS-L3250', 'Type',           'Inkjet'),
    ('PRN-EPS-L3250', 'Color Support',  'Color'),
    -- Monitors
    ('MON-LG-27GN',   'Resolution',     '1440p'),
    ('MON-LG-27GN',   'Panel Type',     'IPS'),
    ('MON-DEL-U32',   'Resolution',     '4K'),
    ('MON-DEL-U32',   'Panel Type',     'IPS'),
    -- Accessories
    ('ACC-LOG-MX3S',  'Type',           'Mouse'),
    ('ACC-LOG-MX3S',  'Connection',     'Wireless'),
    ('ACC-KEY-K8P',   'Type',           'Keyboard'),
    ('ACC-KEY-K8P',   'Connection',     'Bluetooth');
GO

-- ============================================================
-- 7. STOCK MOVEMENTS  (initial stock-in batches)
-- IDENTITY generates MovementId automatically (1..11)
-- Trigger tr_StockMovements_Audit fires for each row,
-- writing one AuditLog entry per movement.
-- ============================================================
INSERT INTO StockMovements
    (ProductSerial, MovementType, QuantityChanged, MovementDate,
     Username, Notes, WarrantyMonths, SupplierName)
VALUES
    ('APP-MBP-2023',  'StockIn', 24, DATEADD(DAY,-10,GETDATE()), 'ahmed.clerk',  'Initial MacBook Pro batch',         24, 'TechSource Inc.'),
    ('APP-IPH-2023',  'StockIn',  8, DATEADD(DAY, -7,GETDATE()), 'ahmed.clerk',  'iPhone 15 Pro Max shipment',        12, 'Global Electronics'),
    ('SAM-S24U-001',  'StockIn', 15, DATEADD(DAY, -6,GETDATE()), 'ahmed.clerk',  'Samsung Galaxy S24 batch',          12, 'Samsung Electronics'),
    ('PRN-HP-2024',   'StockIn',  5, DATEADD(DAY,-20,GETDATE()), 'ahmed.clerk',  'HP LaserJet initial stock',         12, 'Office Depot'),
    ('DEL-XPS-1590',  'StockIn', 12, DATEADD(DAY, -8,GETDATE()), 'ahmed.clerk',  'Dell XPS 15 shipment',              24, 'TechSource Inc.'),
    ('LEN-TPX1-G10',  'StockIn',  5, DATEADD(DAY, -9,GETDATE()), 'ahmed.clerk',  'Lenovo ThinkPad batch',             24, 'TechSource Inc.'),
    ('MON-LG-27GN',   'StockIn', 30, DATEADD(DAY,-12,GETDATE()), 'omar.proc',    'LG UltraGear initial stock',        12, 'Office Depot'),
    ('MON-DEL-U32',   'StockIn', 10, DATEADD(DAY,-11,GETDATE()), 'omar.proc',    'Dell UltraSharp initial stock',     12, 'Global Electronics'),
    ('ACC-LOG-MX3S',  'StockIn', 50, DATEADD(DAY,-15,GETDATE()), 'omar.proc',    'Logitech MX Master bulk order',      6, 'TechSource Inc.'),
    ('ACC-KEY-K8P',   'StockIn', 20, DATEADD(DAY,-14,GETDATE()), 'omar.proc',    'Keychron K8 Pro batch',              6, 'TechSource Inc.'),
    ('PRN-EPS-L3250', 'StockIn', 18, DATEADD(DAY,-13,GETDATE()), 'ahmed.clerk',  'Epson EcoTank batch',               12, 'Office Depot'),
    ('APP-MBA-2022',  'StockIn', 40, DATEADD(DAY,-10,GETDATE()), 'ahmed.clerk',  'MacBook Air M2 shipment',           24, 'TechSource Inc.');
GO

-- ============================================================
-- 8. PRODUCT ITEMS
-- ItemSerialNumber = ProductSerial + '-' + SequenceIndex (2 digits)
-- e.g.  APP-MBP-2023-01 ... APP-MBP-2023-24
-- Each item is linked to its BatchMovementId so per-item
-- warranty can be resolved without duplication.
--
-- MovementId values assigned by IDENTITY above:
--   1=APP-MBP-2023  2=APP-IPH-2023  3=SAM-S24U-001
--   4=PRN-HP-2024   5=DEL-XPS-1590  6=LEN-TPX1-G10
--   7=MON-LG-27GN   8=MON-DEL-U32   9=ACC-LOG-MX3S
--  10=ACC-KEY-K8P  11=PRN-EPS-L3250 12=APP-MBA-2022
-- ============================================================

DECLARE @serial  VARCHAR(50);
DECLARE @batch   INT;
DECLARE @qty     INT;
DECLARE @i       INT;
DECLARE @padded  VARCHAR(2);

-- Helper: builds items for one product/batch
-- APP-MBP-2023 — 24 units — MovementId 1
SELECT @serial='APP-MBP-2023', @batch=1, @qty=24, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- APP-IPH-2023 — 8 units — MovementId 2
SELECT @serial='APP-IPH-2023', @batch=2, @qty=8, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- SAM-S24U-001 — 15 units — MovementId 3
SELECT @serial='SAM-S24U-001', @batch=3, @qty=15, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- PRN-HP-2024 — 5 units — MovementId 4
SELECT @serial='PRN-HP-2024', @batch=4, @qty=5, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- DEL-XPS-1590 — 12 units — MovementId 5
SELECT @serial='DEL-XPS-1590', @batch=5, @qty=12, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- LEN-TPX1-G10 — 5 units — MovementId 6
SELECT @serial='LEN-TPX1-G10', @batch=6, @qty=5, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- MON-LG-27GN — 30 units — MovementId 7
SELECT @serial='MON-LG-27GN', @batch=7, @qty=30, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- MON-DEL-U32 — 10 units — MovementId 8
SELECT @serial='MON-DEL-U32', @batch=8, @qty=10, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- ACC-LOG-MX3S — 50 units — MovementId 9
SELECT @serial='ACC-LOG-MX3S', @batch=9, @qty=50, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- ACC-KEY-K8P — 20 units — MovementId 10
SELECT @serial='ACC-KEY-K8P', @batch=10, @qty=20, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- PRN-EPS-L3250 — 18 units — MovementId 11
SELECT @serial='PRN-EPS-L3250', @batch=11, @qty=18, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END

-- APP-MBA-2022 — 40 units — MovementId 12
SELECT @serial='APP-MBA-2022', @batch=12, @qty=40, @i=1;
WHILE @i <= @qty
BEGIN
    SET @padded = RIGHT('0' + CAST(@i AS VARCHAR(2)), 2);
    INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId)
    VALUES (@serial + '-' + @padded, @serial, 1, @batch);
    SET @i += 1;
END
GO

-- ============================================================
-- VERIFICATION QUERIES
-- Run these after seeding to confirm everything is correct
-- ============================================================

PRINT '--- [1] Products with computed quantities (from vw_ProductStock) ---';
SELECT
    SerialNumber,
    ProductName,
    CategoryName,
    Price,
    Quantity,
    StockStatus
FROM vw_ProductStock
ORDER BY CategoryName, ProductName;
GO

PRINT '--- [2] AuditLog — auto-created by triggers during seeding ---';
SELECT TOP 20
    LogId,
    LogTimestamp,
    ActionType,
    Description,
    Username
FROM AuditLog
ORDER BY LogId DESC;
GO

PRINT '--- [3] ProductItems sample — first 5 items per product ---';
SELECT
    pi.ItemSerialNumber,
    pi.ProductSerial,
    pi.IsInStock,
    sm.MovementType,
    sm.WarrantyMonths
FROM ProductItems pi
JOIN StockMovements sm ON sm.MovementId = pi.BatchMovementId
ORDER BY pi.ProductSerial, pi.ItemSerialNumber
GO

PRINT '============================================================';
PRINT 'Seed data loaded successfully.';
PRINT 'Total products  : 12';
PRINT 'Total items     : 237';
PRINT 'Total movements : 12';
PRINT 'AuditLog entries: auto-generated by triggers';
PRINT '============================================================';
GO

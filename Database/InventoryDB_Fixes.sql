-- ================================================================
-- InventoryDB_Fixes.sql
-- Date   : 2026-05-18
-- Purpose: Cleanup test data + add spec-key templates for all
--          real categories so FrmAddProduct works out of the box.
--
-- Run this script ONCE on the live InventoryDB from SSMS.
-- Safe to run repeatedly (uses IF NOT EXISTS / WHERE checks).
-- ================================================================

USE [InventoryDB];
GO

-- ================================================================
-- 1. ADD SPEC KEY TEMPLATES FOR REAL CATEGORIES
--    These power the spec DGV in FrmAddProduct.
--    Using IF NOT EXISTS so re-running won't cause duplicates.
-- ================================================================

-- Laptops
IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Laptops' AND SpecKey = 'Processor')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Laptops', 'Processor');

IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Laptops' AND SpecKey = 'RAM')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Laptops', 'RAM');

IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Laptops' AND SpecKey = 'Storage')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Laptops', 'Storage');

-- Phones
IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Phones' AND SpecKey = 'Storage')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Phones', 'Storage');

IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Phones' AND SpecKey = 'Color')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Phones', 'Color');

-- Monitors
IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Monitors' AND SpecKey = 'Resolution')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Monitors', 'Resolution');

IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Monitors' AND SpecKey = 'Panel Type')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Monitors', 'Panel Type');

-- Printers
IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Printers' AND SpecKey = 'Type')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Printers', 'Type');

IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Printers' AND SpecKey = 'Color Support')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Printers', 'Color Support');

-- Accessories
IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Accessories' AND SpecKey = 'Type')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Accessories', 'Type');

IF NOT EXISTS (SELECT 1 FROM CategorySpecTemplates WHERE CategoryName = 'Accessories' AND SpecKey = 'Connection')
    INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) VALUES ('Accessories', 'Connection');

GO
PRINT '✔ Step 1 complete: Spec templates added for all real categories.';
GO

-- ================================================================
-- 2. FIX PRODUCT NAME CHANGED DURING TESTING
--    ACC-KEY-K8P was renamed to "فلللل" — restore it.
-- ================================================================

UPDATE Products
SET    ProductName = 'Keychron K8 Pro'
WHERE  SerialNumber = 'ACC-KEY-K8P'
  AND  ProductName  <> 'Keychron K8 Pro';

GO
PRINT '✔ Step 2 complete: ACC-KEY-K8P name restored to Keychron K8 Pro.';
GO

-- ================================================================
-- 3. REMOVE TEST PRODUCT: 32432 (جهاز) — Category: Accessories
--    Order: ProductItems → ProductSpecifications
--           → StockMovements → Products
-- ================================================================

DELETE FROM ProductItems         WHERE ProductSerial = '32432';
DELETE FROM ProductSpecifications WHERE ProductSerial = '32432';
DELETE FROM StockMovements        WHERE ProductSerial = '32432';
DELETE FROM Products              WHERE SerialNumber  = '32432';

GO
PRINT '✔ Step 3 complete: Test product 32432 removed.';
GO

-- ================================================================
-- 4. REMOVE TEST CATEGORY: 234
--    Products in this category: 24224ل
--    Order: ProductItems → ProductSpecifications → StockMovements
--           → Products → StorageZones
--           → Categories  (CategorySpecTemplates CASCADE deletes)
-- ================================================================

DELETE FROM ProductItems          WHERE ProductSerial  = N'24224ل';
DELETE FROM ProductSpecifications  WHERE ProductSerial  = N'24224ل';
DELETE FROM StockMovements         WHERE ProductSerial  = N'24224ل';
DELETE FROM Products               WHERE SerialNumber   = N'24224ل';
DELETE FROM StorageZones           WHERE CategoryName   = N'234';
-- CategorySpecTemplates (صقثص, صقثصثق) cascade-delete automatically
DELETE FROM Categories             WHERE CategoryName   = N'234';

GO
PRINT '✔ Step 4 complete: Test category "234" and all its data removed.';
GO

-- ================================================================
-- 5. REMOVE TEST CATEGORY: hh  (no products — zone only)
-- ================================================================

DELETE FROM StorageZones WHERE CategoryName = N'hh';
DELETE FROM Categories   WHERE CategoryName = N'hh';

GO
PRINT '✔ Step 5 complete: Test category "hh" removed.';
GO

-- ================================================================
-- VERIFICATION — run this after to confirm everything is clean
-- ================================================================
PRINT '--- Verification ---';

SELECT 'CategorySpecTemplates' AS [Table], CategoryName, SpecKey
FROM   CategorySpecTemplates
ORDER  BY CategoryName, SpecKey;

SELECT 'Categories' AS [Table], CategoryName
FROM   Categories
ORDER  BY CategoryName;

SELECT 'Products' AS [Table], SerialNumber, ProductName, CategoryName
FROM   Products
ORDER  BY CategoryName, SerialNumber;

GO

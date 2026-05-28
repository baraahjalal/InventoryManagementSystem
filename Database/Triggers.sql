-- ============================================================
-- Inventory Management System
-- File        : Triggers.sql
-- Description : 4 AFTER triggers that auto-populate AuditLog
-- Run order   : 2nd  (after CreateDatabase.sql, before SeedData.sql)
-- ============================================================
-- DESIGN PRINCIPLE:
--   C# code NEVER writes to AuditLog directly.
--   Every audit entry is created automatically at the
--   database level when a relevant operation occurs.
--   This guarantees consistency even if the DB is accessed
--   from a different client in the future.
-- ============================================================

USE InventoryDB;
GO

-- ============================================================
-- TRIGGER 1: tr_StockMovements_Audit
-- Table  : StockMovements
-- Event  : AFTER INSERT
-- WHY #1 : This is the most important trigger — every stock
--          operation (in/out/restock/return) is recorded.
-- WHY #2 : Username is already on the inserted row, so the
--          trigger knows exactly who performed the operation.
-- Sample log:
--   ActionType  = 'STOCK STOCKIN'
--   Description = 'Product [APP-MBP-2023]: +10 units. Supplier: TechSource Inc.'
--   Username    = 'ahmed.clerk'
-- ============================================================
CREATE TRIGGER tr_StockMovements_Audit
ON StockMovements
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ActionType, Description, Username)
    SELECT
        'STOCK ' + UPPER(i.MovementType),
        'Product [' + i.ProductSerial + ']: '
            + CAST(i.QuantityChanged AS VARCHAR(10)) + ' units'
            + ISNULL('. Supplier: '  + i.SupplierName, '')
            + ISNULL('. Notes: '     + i.Notes,        ''),
        ISNULL(i.Username, 'System')
    FROM inserted i;
END;
GO

-- ============================================================
-- TRIGGER 2: tr_Products_Audit
-- Table  : Products
-- Event  : AFTER INSERT, DELETE
-- WHY    : Tracks product catalogue changes — when a new
--          product is registered or an existing one removed.
-- NOTE   : Username is recorded as 'System' because the
--          Products table does not carry a Username column.
--          The logged-in user context is managed at app level.
-- Sample logs:
--   'PRODUCT ADDED'   → 'New product: [APP-MBP-2023] MacBook Pro 14" M3'
--   'PRODUCT DELETED' → 'Product removed: [APP-MBP-2023] MacBook Pro 14" M3'
-- ============================================================
CREATE TRIGGER tr_Products_Audit
ON Products
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- INSERT branch
    IF EXISTS (SELECT 1 FROM inserted)
       AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog (ActionType, Description, Username)
        SELECT
            'PRODUCT ADDED',
            'New product: [' + i.SerialNumber + '] ' + i.ProductName
                + ' | Category: ' + i.CategoryName
                + ' | Price: '    + CAST(i.Price AS VARCHAR(20)),
            'System'
        FROM inserted i;
    END

    -- DELETE branch
    IF EXISTS (SELECT 1 FROM deleted)
       AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO AuditLog (ActionType, Description, Username)
        SELECT
            'PRODUCT DELETED',
            'Product removed: [' + d.SerialNumber + '] ' + d.ProductName,
            'System'
        FROM deleted d;
    END
END;
GO

-- ============================================================
-- TRIGGER 3: tr_Users_Audit
-- Table  : Users
-- Event  : AFTER INSERT, DELETE
-- WHY    : Security-critical — every account creation or
--          removal must be traceable for compliance.
-- Sample logs:
--   'USER ADDED'   → 'New user: ahmed.clerk (Employee)'
--   'USER DELETED' → 'User removed: ahmed.clerk (Employee)'
-- ============================================================
CREATE TRIGGER tr_Users_Audit
ON Users
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- INSERT branch
    IF EXISTS (SELECT 1 FROM inserted)
       AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog (ActionType, Description, Username)
        SELECT
            'USER ADDED',
            'New user: ' + i.Username + ' (' + i.Role + ')',
            'System'
        FROM inserted i;
    END

    -- DELETE branch
    IF EXISTS (SELECT 1 FROM deleted)
       AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO AuditLog (ActionType, Description, Username)
        SELECT
            'USER DELETED',
            'User removed: ' + d.Username + ' (' + d.Role + ')',
            'System'
        FROM deleted d;
    END
END;
GO

-- ============================================================
-- TRIGGER 4: tr_Suppliers_Audit
-- Table  : Suppliers
-- Event  : AFTER INSERT, DELETE
-- WHY    : Supplier changes affect procurement decisions —
--          knowing when a supplier was added or deactivated
--          is important for operational auditing.
-- Sample logs:
--   'SUPPLIER ADDED'   → 'New supplier: TechSource Inc. | Phone: 555-010100'
--   'SUPPLIER DELETED' → 'Supplier removed: TechSource Inc.'
-- ============================================================
CREATE TRIGGER tr_Suppliers_Audit
ON Suppliers
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- INSERT branch
    IF EXISTS (SELECT 1 FROM inserted)
       AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog (ActionType, Description, Username)
        SELECT
            'SUPPLIER ADDED',
            'New supplier: ' + i.SupplierName
                + ISNULL(' | Phone: ' + i.Phone, '')
                + ISNULL(' | Email: ' + i.Email, ''),
            'System'
        FROM inserted i;
    END

    -- DELETE branch
    IF EXISTS (SELECT 1 FROM deleted)
       AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO AuditLog (ActionType, Description, Username)
        SELECT
            'SUPPLIER DELETED',
            'Supplier removed: ' + d.SupplierName,
            'System'
        FROM deleted d;
    END
END;
GO

-- ============================================================
-- Verify all 4 triggers were created
-- ============================================================
SELECT
    t.name   AS TriggerName,
    OBJECT_NAME(t.parent_id) AS OnTable,
    t.is_disabled
FROM sys.triggers t
WHERE t.parent_class = 1
ORDER BY OBJECT_NAME(t.parent_id);
GO

PRINT '============================================================';
PRINT 'Triggers created — AuditLog will auto-populate on all';
PRINT 'StockMovements, Products, Users, and Suppliers changes.';
PRINT 'Run SeedData.sql next.';
PRINT '============================================================';
GO

-- ============================================================
-- Inventory Management System
-- File        : CreateDatabase.sql
-- Description : Creates InventoryDB with 9 tables + 1 view
-- Run order   : 1st  (before Triggers.sql and SeedData.sql)
-- Branch      : feature/sql-server-final
-- ============================================================
-- PRIMARY KEY POLICY (per professor requirement):
--   Natural key used wherever a unique attribute exists.
--   IDENTITY used ONLY for StockMovements and AuditLog
--   (transaction/event logs with no natural key).
-- ============================================================

USE master;
GO

-- Drop and recreate for a clean slate
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'InventoryDB')
BEGIN
    ALTER DATABASE InventoryDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE InventoryDB;
END
GO

CREATE DATABASE InventoryDB;
GO

USE InventoryDB;
GO

-- ============================================================
-- TABLE 1: Users
-- PK : Username  (natural key)
-- WHY: Username is always unique. It is referenced directly
--      in StockMovements and AuditLog — using it as PK avoids
--      an unnecessary surrogate ID.
-- ============================================================
CREATE TABLE Users (
    Username     VARCHAR(50)    NOT NULL,
    Password     VARCHAR(255)   NOT NULL,
    Role         VARCHAR(30)    NOT NULL
        CONSTRAINT CK_Users_Role
            CHECK (Role IN ('System Administrator', 'Employee')),
    IsAdmin      BIT            NOT NULL DEFAULT 0,
    ProfilePhoto VARBINARY(MAX) NULL,
    CONSTRAINT PK_Users PRIMARY KEY (Username)
);
GO

-- ============================================================
-- TABLE 2: Categories
-- PK : CategoryName  (natural key)
-- WHY: Category names are unique and stable.
--      Every form references categories by name, not by a
--      surrogate number — making it the natural identifier.
-- ============================================================
CREATE TABLE Categories (
    CategoryName VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Categories PRIMARY KEY (CategoryName)
);
GO

-- ============================================================
-- TABLE 3: Suppliers
-- PK : SupplierName  (natural key)
-- WHY: The original code enforces uniqueness on supplier name
--      explicitly in ValidateForm(). Name is the real-world
--      identity of a supplier company.
-- NOTE: ContactPerson removed — that is CRM data, not inventory.
-- ============================================================
CREATE TABLE Suppliers (
    SupplierName VARCHAR(150) NOT NULL,
    Phone        VARCHAR(20)  NULL,
    Email        VARCHAR(100) NULL,
    IsActive     BIT          NOT NULL DEFAULT 1,
    CONSTRAINT PK_Suppliers PRIMARY KEY (SupplierName)
);
GO

-- ============================================================
-- TABLE 4: StorageZones
-- PK : ZoneName  (natural key)
-- WHY: Zone names such as "Aisle A-1: Laptops" are unique
--      and self-describing — no surrogate needed.
-- FK : CategoryName → Categories
-- ============================================================
CREATE TABLE StorageZones (
    ZoneName     VARCHAR(100) NOT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    CONSTRAINT PK_StorageZones PRIMARY KEY (ZoneName),
    CONSTRAINT FK_Zone_Category
        FOREIGN KEY (CategoryName) REFERENCES Categories (CategoryName)
);
GO

-- ============================================================
-- TABLE 5: Products
-- PK : SerialNumber  (natural key)
-- WHY: e.g. "APP-MBP-2023" is the real-world product identity
--      used in shipments, labels, and every form display.
-- NOTE: No Quantity column — quantity is DERIVED from
--       ProductItems via vw_ProductStock. Storing it would
--       create redundancy and risk inconsistency.
-- FK : CategoryName → Categories
-- ============================================================
CREATE TABLE Products (
    SerialNumber VARCHAR(50)   NOT NULL,
    ProductName  NVARCHAR(200) NOT NULL,
    CategoryName VARCHAR(100)  NOT NULL,
    Price        DECIMAL(10,2) NOT NULL,
    CONSTRAINT PK_Products PRIMARY KEY (SerialNumber),
    CONSTRAINT FK_Product_Category
        FOREIGN KEY (CategoryName) REFERENCES Categories (CategoryName)
);
GO

-- ============================================================
-- TABLE 6: ProductSpecifications
-- PK : (ProductSerial, SpecKey)  — composite natural key
-- WHY: A product has exactly ONE value per specification key.
--      e.g. (APP-MBP-2023, RAM, 16GB) is unique by definition.
--      The pair (serial + key) is the natural identifier.
-- FK : ProductSerial → Products  (CASCADE on delete)
-- ============================================================
CREATE TABLE ProductSpecifications (
    ProductSerial VARCHAR(50)   NOT NULL,
    SpecKey       VARCHAR(50)   NOT NULL,
    SpecValue     NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_ProductSpecs
        PRIMARY KEY (ProductSerial, SpecKey),
    CONSTRAINT FK_Specs_Product
        FOREIGN KEY (ProductSerial) REFERENCES Products (SerialNumber)
        ON DELETE CASCADE
);
GO

-- ============================================================
-- TABLE 7: StockMovements
-- PK : MovementId  — IDENTITY  (justified exception)
-- WHY: Transaction log. The same product can be stocked in
--      hundreds of times. No combination of columns is
--      naturally unique. This is one of only two tables
--      where IDENTITY is the correct design choice.
-- FK : ProductSerial → Products
--      Username      → Users      (SET NULL on delete)
--      SupplierName  → Suppliers  (SET NULL on delete)
-- ============================================================
CREATE TABLE StockMovements (
    MovementId      INT           NOT NULL IDENTITY(1,1),
    ProductSerial   VARCHAR(50)   NOT NULL,
    MovementType    VARCHAR(20)   NOT NULL
        CONSTRAINT CK_SM_Type
            CHECK (MovementType IN
                ('StockIn', 'StockOut', 'Restock', 'ReturnToSupplier')),
    QuantityChanged INT           NOT NULL,
    MovementDate    DATETIME      NOT NULL DEFAULT GETDATE(),
    Username        VARCHAR(50)   NULL,
    Notes           NVARCHAR(500) NULL,
    WarrantyMonths  INT           NULL,
    SupplierName    VARCHAR(150)  NULL,
    CONSTRAINT PK_StockMovements PRIMARY KEY (MovementId),
    CONSTRAINT FK_SM_Product
        FOREIGN KEY (ProductSerial)  REFERENCES Products  (SerialNumber),
    CONSTRAINT FK_SM_User
        FOREIGN KEY (Username)       REFERENCES Users     (Username)
        ON DELETE SET NULL,
    CONSTRAINT FK_SM_Supplier
        FOREIGN KEY (SupplierName)   REFERENCES Suppliers (SupplierName)
        ON DELETE SET NULL
);
GO

-- ============================================================
-- TABLE 8: ProductItems
-- PK : ItemSerialNumber  (natural key)
-- WHY: e.g. "APP-MBP-2023-01" is globally unique by
--      construction (product serial + sequence index).
-- FK : ProductSerial    → Products
--      BatchMovementId  → StockMovements  (SET NULL on delete)
--      BatchMovementId links each physical unit back to the
--      exact stock-in batch it arrived with, enabling
--      per-item warranty lookup.
-- ============================================================
CREATE TABLE ProductItems (
    ItemSerialNumber VARCHAR(80) NOT NULL,
    ProductSerial    VARCHAR(50) NOT NULL,
    IsInStock        BIT         NOT NULL DEFAULT 1,
    DateAdded        DATETIME    NOT NULL DEFAULT GETDATE(),
    DateRemoved      DATETIME    NULL,
    BatchMovementId  INT         NULL,
    CONSTRAINT PK_ProductItems PRIMARY KEY (ItemSerialNumber),
    CONSTRAINT FK_PI_Product
        FOREIGN KEY (ProductSerial)   REFERENCES Products       (SerialNumber),
    CONSTRAINT FK_PI_Movement
        FOREIGN KEY (BatchMovementId) REFERENCES StockMovements (MovementId)
        ON DELETE SET NULL
);
GO

-- ============================================================
-- TABLE 9: AuditLog
-- PK : LogId  — IDENTITY  (justified exception)
-- WHY: Event log. The same user can trigger multiple events
--      within the same millisecond. No natural key exists.
-- RULE: This table is populated ONLY by SQL Triggers.
--       C# code must NEVER write to this table directly.
-- ============================================================
CREATE TABLE AuditLog (
    LogId        INT           NOT NULL IDENTITY(1,1),
    LogTimestamp DATETIME      NOT NULL DEFAULT GETDATE(),
    ActionType   VARCHAR(50)   NOT NULL,
    Description  NVARCHAR(500) NOT NULL,
    Username     VARCHAR(50)   NOT NULL,
    CONSTRAINT PK_AuditLog PRIMARY KEY (LogId)
);
GO

-- ============================================================
-- VIEW: vw_ProductStock
-- Purpose : Computes Quantity and StockStatus from ProductItems.
--           All forms needing product quantities read from here.
--           Storing Quantity in Products would be redundant;
--           this view is the single source of truth.
-- ============================================================
CREATE VIEW vw_ProductStock AS
SELECT
    p.SerialNumber,
    p.ProductName,
    p.CategoryName,
    p.Price,
    COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END)          AS Quantity,
    CASE
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) = 0
            THEN 'Out of Stock'
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) <= 10
            THEN 'Low Stock'
        ELSE 'In Stock'
    END                                                    AS StockStatus
FROM Products p
LEFT JOIN ProductItems pi ON pi.ProductSerial = p.SerialNumber
GROUP BY
    p.SerialNumber,
    p.ProductName,
    p.CategoryName,
    p.Price;
GO

-- ============================================================
-- Quick sanity check
-- ============================================================
SELECT
    t.name      AS TableName,
    c.name      AS PKColumn,
    c.system_type_id
FROM sys.tables t
JOIN sys.indexes i       ON i.object_id = t.object_id AND i.is_primary_key = 1
JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
JOIN sys.columns c       ON c.object_id = t.object_id AND c.column_id = ic.column_id
ORDER BY t.name, ic.key_ordinal;
GO

PRINT '============================================================';
PRINT 'InventoryDB created — 9 tables + vw_ProductStock.';
PRINT 'Run Triggers.sql next, then SeedData.sql.';
PRINT '============================================================';
GO

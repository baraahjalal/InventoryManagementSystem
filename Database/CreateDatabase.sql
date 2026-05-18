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
-- ENCODING NOTE:
--   All text columns use NVARCHAR to support full Unicode
--   (Arabic, English, and all other languages).
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

CREATE DATABASE InventoryDB
    COLLATE Arabic_CI_AI;   -- supports Arabic + English, case-insensitive
GO

USE InventoryDB;
GO

-- ============================================================
-- TABLE 1: Users
-- PK : Username  (natural key)
-- ============================================================
CREATE TABLE Users (
    Username     NVARCHAR(50)   NOT NULL,
    Password     NVARCHAR(255)  NOT NULL,
    Role         NVARCHAR(30)   NOT NULL
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
-- ============================================================
CREATE TABLE Categories (
    CategoryName NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_Categories PRIMARY KEY (CategoryName)
);
GO

-- ============================================================
-- TABLE 3: Suppliers
-- PK : SupplierName  (natural key)
-- ============================================================
CREATE TABLE Suppliers (
    SupplierName NVARCHAR(150) NOT NULL,
    Phone        NVARCHAR(20)  NULL,
    Email        NVARCHAR(100) NULL,
    IsActive     BIT           NOT NULL DEFAULT 1,
    CONSTRAINT PK_Suppliers PRIMARY KEY (SupplierName)
);
GO

-- ============================================================
-- TABLE 4: StorageZones
-- PK : ZoneName  (natural key)
-- FK : CategoryName → Categories
-- ============================================================
CREATE TABLE StorageZones (
    ZoneName     NVARCHAR(100) NOT NULL,
    CategoryName NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_StorageZones PRIMARY KEY (ZoneName),
    CONSTRAINT FK_Zone_Category
        FOREIGN KEY (CategoryName) REFERENCES Categories (CategoryName)
);
GO

-- ============================================================
-- TABLE 5: Products
-- PK : SerialNumber  (natural key)
-- NOTE: No Quantity column — quantity is DERIVED from
--       ProductItems via vw_ProductStock.
-- FK : CategoryName → Categories
-- ============================================================
CREATE TABLE Products (
    SerialNumber NVARCHAR(50)   NOT NULL,
    ProductName  NVARCHAR(200)  NOT NULL,
    CategoryName NVARCHAR(100)  NOT NULL,
    Price        DECIMAL(10,2)  NOT NULL,
    CONSTRAINT PK_Products PRIMARY KEY (SerialNumber),
    CONSTRAINT FK_Product_Category
        FOREIGN KEY (CategoryName) REFERENCES Categories (CategoryName)
);
GO

-- ============================================================
-- TABLE 6: ProductSpecifications
-- PK : (ProductSerial, SpecKey)  — composite natural key
-- FK : ProductSerial → Products  (CASCADE on delete)
-- ============================================================
CREATE TABLE ProductSpecifications (
    ProductSerial NVARCHAR(50)  NOT NULL,
    SpecKey       NVARCHAR(100) NOT NULL,
    SpecValue     NVARCHAR(200) NOT NULL,
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
-- FK : ProductSerial → Products
--      Username      → Users      (SET NULL on delete)
--      SupplierName  → Suppliers  (SET NULL on delete)
-- ============================================================
CREATE TABLE StockMovements (
    MovementId      INT            NOT NULL IDENTITY(1,1),
    ProductSerial   NVARCHAR(50)   NOT NULL,
    MovementType    NVARCHAR(20)   NOT NULL
        CONSTRAINT CK_SM_Type
            CHECK (MovementType IN
                ('StockIn', 'StockOut', 'Restock', 'ReturnToSupplier')),
    QuantityChanged INT            NOT NULL,
    MovementDate    DATETIME       NOT NULL DEFAULT GETDATE(),
    Username        NVARCHAR(50)   NULL,
    Notes           NVARCHAR(500)  NULL,
    WarrantyMonths  INT            NULL,
    SupplierName    NVARCHAR(150)  NULL,
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
-- FK : ProductSerial    → Products
--      BatchMovementId  → StockMovements  (SET NULL on delete)
-- ============================================================
CREATE TABLE ProductItems (
    ItemSerialNumber NVARCHAR(80) NOT NULL,
    ProductSerial    NVARCHAR(50) NOT NULL,
    IsInStock        BIT          NOT NULL DEFAULT 1,
    DateAdded        DATETIME     NOT NULL DEFAULT GETDATE(),
    DateRemoved      DATETIME     NULL,
    BatchMovementId  INT          NULL,
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
-- RULE: This table is populated ONLY by SQL Triggers.
--       C# code must NEVER write to this table directly.
-- ============================================================
CREATE TABLE AuditLog (
    LogId        INT            NOT NULL IDENTITY(1,1),
    LogTimestamp DATETIME       NOT NULL DEFAULT GETDATE(),
    ActionType   NVARCHAR(50)   NOT NULL,
    Description  NVARCHAR(500)  NOT NULL,
    Username     NVARCHAR(50)   NOT NULL,
    CONSTRAINT PK_AuditLog PRIMARY KEY (LogId)
);
GO

-- ============================================================
-- TABLE 10: CategorySpecTemplates
-- PK : (CategoryName, SpecKey) — composite natural key
-- FK : CategoryName → Categories  (CASCADE on delete)
-- PURPOSE : Defines which specification keys are expected for
--           each category. Used to pre-fill FrmAddProduct.
-- ============================================================
CREATE TABLE CategorySpecTemplates (
    CategoryName NVARCHAR(100) NOT NULL,
    SpecKey      NVARCHAR(50)  NOT NULL,
    CONSTRAINT PK_CategorySpecTemplates PRIMARY KEY (CategoryName, SpecKey),
    CONSTRAINT FK_CST_Category
        FOREIGN KEY (CategoryName) REFERENCES Categories (CategoryName)
        ON DELETE CASCADE
);
GO

-- ============================================================
-- VIEW: vw_ProductStock
-- Purpose : Computes Quantity and StockStatus from ProductItems.
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
JOIN sys.indexes i        ON i.object_id = t.object_id AND i.is_primary_key = 1
JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
JOIN sys.columns c        ON c.object_id = t.object_id AND c.column_id = ic.column_id
ORDER BY t.name, ic.key_ordinal;
GO

PRINT '============================================================';
PRINT 'InventoryDB created — 9 tables + vw_ProductStock.';
PRINT 'All text columns use NVARCHAR (Unicode / Arabic support).';
PRINT 'Run Triggers.sql next, then SeedData.sql.';
PRINT '============================================================';
GO

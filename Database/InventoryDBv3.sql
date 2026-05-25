-- ============================================================
--  InventoryDBv3 â Complete Database Creation Script
--  Fresh / Clean â includes Tables, Views, Triggers, Indexes
--  Target: SQL Server 2019+
-- ============================================================
--  DESIGN PRINCIPLES:
--   1. PKs are real integers wherever possible (no string PKs)
--   2. AuditLog is populated ONLY via Triggers â never by C#
--   3. vw_ProductStock computes Quantity / StockStatus on-the-fly
-- ============================================================

USE master;
GO

-- Create fresh DB (does NOT touch InventoryDB or earlier copies)
IF DB_ID('InventoryDBv3') IS NOT NULL
BEGIN
    ALTER DATABASE InventoryDBv3 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE InventoryDBv3;
END
GO

CREATE DATABASE InventoryDBv3;
GO

USE InventoryDBv3;
GO


-- ============================================================
--  SEQUENCE â for ProductItems IDs
-- ============================================================
CREATE SEQUENCE seq_ProductItems
    AS INT
    START WITH 1
    INCREMENT BY 1
    NO CYCLE;
GO


-- ============================================================
--  TABLE: Users
--  PK: EmployeeID (real integer â official employee number)
-- ============================================================
CREATE TABLE Users (
    EmployeeID    INT             NOT NULL,
    Username      NVARCHAR(255)   NOT NULL,
    [Password]    NVARCHAR(255)   NOT NULL,
    [Role]        NVARCHAR(255)   NOT NULL,
    IsAdmin       BIT             NOT NULL CONSTRAINT DF_Users_IsAdmin DEFAULT (0),
    ProfilePhoto  VARBINARY(MAX)  NULL,

    CONSTRAINT PK_Users           PRIMARY KEY (EmployeeID),
    CONSTRAINT UQ_Users_Username  UNIQUE      (Username),
    CONSTRAINT CK_Users_Role      CHECK       ([Role] IN ('Employee', 'System Administrator'))
);
GO


-- ============================================================
--  TABLE: Suppliers
--  PK: SupplierTaxNumber (real integer â tax registration number)
-- ============================================================
CREATE TABLE Suppliers (
    SupplierTaxNumber  INT            NOT NULL,
    SupplierName       NVARCHAR(255)  NOT NULL,
    Phone              NVARCHAR(255)  NULL,
    Email              NVARCHAR(255)  NULL,
    IsActive           BIT            NOT NULL CONSTRAINT DF_Suppliers_IsActive DEFAULT (1),

    CONSTRAINT PK_Suppliers           PRIMARY KEY (SupplierTaxNumber),
    CONSTRAINT UQ_Suppliers_Name      UNIQUE      (SupplierName)
);
GO


-- ============================================================
--  TABLE: Categories
--  PK: CategoryID (auto-increment)
-- ============================================================
CREATE TABLE Categories (
    CategoryID    INT            IDENTITY(1,1) NOT NULL,
    CategoryName  NVARCHAR(255)  NOT NULL,

    CONSTRAINT PK_Categories      PRIMARY KEY (CategoryID),
    CONSTRAINT UQ_Categories_Name UNIQUE      (CategoryName)
);
GO


-- ============================================================
--  TABLE: StorageZones
--  PK: ZoneID (auto-increment)
--  FK: CategoryID -> Categories
-- ============================================================
CREATE TABLE StorageZones (
    ZoneID       INT            IDENTITY(1,1) NOT NULL,
    ZoneName     NVARCHAR(255)  NOT NULL,
    CategoryID   INT            NOT NULL,

    CONSTRAINT PK_StorageZones        PRIMARY KEY (ZoneID),
    CONSTRAINT UQ_StorageZones_Name   UNIQUE      (ZoneName),
    CONSTRAINT FK_Zone_Category       FOREIGN KEY (CategoryID)
        REFERENCES Categories(CategoryID)
        ON DELETE CASCADE
);
GO


-- ============================================================
--  TABLE: Products
--  PK: ProductID (real integer serial number)
--  FK: CategoryID -> Categories
-- ============================================================
CREATE TABLE Products (
    ProductID    INT            NOT NULL,
    ProductName  NVARCHAR(255)  NOT NULL,
    CategoryID   INT            NOT NULL,
    Price        DECIMAL(18,2)  NOT NULL,

    CONSTRAINT PK_Products         PRIMARY KEY (ProductID),
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID)
        REFERENCES Categories(CategoryID)
);
GO


-- ============================================================
--  TABLE: CategorySpecTemplates
--  PK: TemplateID (auto-increment)
--  FK: CategoryID -> Categories
-- ============================================================
CREATE TABLE CategorySpecTemplates (
    TemplateID   INT            IDENTITY(1,1) NOT NULL,
    CategoryID   INT            NOT NULL,
    SpecKey      NVARCHAR(255)  NOT NULL,

    CONSTRAINT PK_CategorySpecTemplates  PRIMARY KEY (TemplateID),
    CONSTRAINT UQ_CST_Category_SpecKey   UNIQUE      (CategoryID, SpecKey),
    CONSTRAINT FK_CST_Category           FOREIGN KEY (CategoryID)
        REFERENCES Categories(CategoryID)
        ON DELETE CASCADE
);
GO


-- ============================================================
--  TABLE: ProductSpecifications
--  PK: SpecID (auto-increment)
--  FK: ProductID -> Products
-- ============================================================
CREATE TABLE ProductSpecifications (
    SpecID       INT            IDENTITY(1,1) NOT NULL,
    ProductID    INT            NOT NULL,
    SpecKey      NVARCHAR(255)  NOT NULL,
    SpecValue    NVARCHAR(255)  NOT NULL,

    CONSTRAINT PK_ProductSpecifications  PRIMARY KEY (SpecID),
    CONSTRAINT UQ_PS_Product_SpecKey     UNIQUE      (ProductID, SpecKey),
    CONSTRAINT FK_Specs_Product          FOREIGN KEY (ProductID)
        REFERENCES Products(ProductID)
        ON DELETE CASCADE
);
GO


-- ============================================================
--  TABLE: StockMovements
--  PK: MovementID (auto-increment)
--  FKs: ProductID, EmployeeID, SupplierTaxNumber
-- ============================================================
CREATE TABLE StockMovements (
    MovementID         INT            IDENTITY(1,1) NOT NULL,
    ProductID          INT            NOT NULL,
    MovementType       NVARCHAR(255)  NOT NULL,
    QuantityChanged    INT            NOT NULL,
    MovementDate       DATETIME       NOT NULL CONSTRAINT DF_SM_MovementDate DEFAULT (GETDATE()),
    EmployeeID         INT            NULL,
    SupplierTaxNumber  INT            NULL,
    WarrantyMonths     INT            NULL,
    Notes              NVARCHAR(255)  NULL,

    CONSTRAINT PK_StockMovements   PRIMARY KEY (MovementID),
    CONSTRAINT CK_SM_Type          CHECK (MovementType IN ('StockIn','StockOut','Restock','ReturnToSupplier')),

    CONSTRAINT FK_SM_Product       FOREIGN KEY (ProductID)
        REFERENCES Products(ProductID),
    CONSTRAINT FK_SM_User          FOREIGN KEY (EmployeeID)
        REFERENCES Users(EmployeeID)
        ON DELETE SET NULL,
    CONSTRAINT FK_SM_Supplier      FOREIGN KEY (SupplierTaxNumber)
        REFERENCES Suppliers(SupplierTaxNumber)
        ON DELETE SET NULL
);
GO


-- ============================================================
--  TABLE: ProductItems
--  PK: ItemID (generated via SEQUENCE)
--  FKs: ProductID, BatchMovementID
-- ============================================================
CREATE TABLE ProductItems (
    ItemID           INT       NOT NULL,
    ProductID        INT       NOT NULL,
    IsInStock        BIT       NOT NULL CONSTRAINT DF_PI_IsInStock DEFAULT (1),
    DateAdded        DATETIME  NOT NULL CONSTRAINT DF_PI_DateAdded DEFAULT (GETDATE()),
    DateRemoved      DATETIME  NULL,
    BatchMovementID  INT       NULL,

    CONSTRAINT PK_ProductItems    PRIMARY KEY (ItemID),
    CONSTRAINT FK_PI_Product      FOREIGN KEY (ProductID)
        REFERENCES Products(ProductID),
    CONSTRAINT FK_PI_Movement     FOREIGN KEY (BatchMovementID)
        REFERENCES StockMovements(MovementID)
        ON DELETE SET NULL
);
GO


-- ============================================================
--  TABLE: AuditLog
--  PK: LogID (auto-increment)
--  No FK on Username â can include 'System' entries
--  IMPORTANT: This table is populated ONLY by triggers below.
--             C# code MUST NOT write to it directly.
-- ============================================================
CREATE TABLE AuditLog (
    LogID         INT            IDENTITY(1,1) NOT NULL,
    LogTimestamp  DATETIME       NOT NULL CONSTRAINT DF_AL_LogTimestamp DEFAULT (GETDATE()),
    ActionType    NVARCHAR(255)  NOT NULL,
    [Description] NVARCHAR(255)  NOT NULL,
    Username      NVARCHAR(255)  NOT NULL,

    CONSTRAINT PK_AuditLog PRIMARY KEY (LogID)
);
GO


-- ============================================================
--  VIEW: vw_ProductStock
--  Computes Quantity + StockStatus from ProductItems
-- ============================================================
CREATE VIEW vw_ProductStock AS
SELECT
    p.ProductID,
    p.ProductName,
    p.CategoryID,
    c.CategoryName,
    p.Price,
    COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END)              AS Quantity,
    CASE
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) = 0
            THEN 'Out of Stock'
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) <= 10
            THEN 'Low Stock'
        ELSE 'In Stock'
    END                                                       AS StockStatus
FROM Products p
INNER JOIN Categories c    ON c.CategoryID  = p.CategoryID
LEFT  JOIN ProductItems pi ON pi.ProductID  = p.ProductID
GROUP BY
    p.ProductID,
    p.ProductName,
    p.CategoryID,
    c.CategoryName,
    p.Price;
GO


-- ============================================================
--  INDEXES (for performance)
-- ============================================================
CREATE INDEX IX_Products_CategoryID         ON Products(CategoryID);
CREATE INDEX IX_StorageZones_CategoryID     ON StorageZones(CategoryID);
CREATE INDEX IX_CST_CategoryID              ON CategorySpecTemplates(CategoryID);
CREATE INDEX IX_PS_ProductID                ON ProductSpecifications(ProductID);
CREATE INDEX IX_SM_ProductID                ON StockMovements(ProductID);
CREATE INDEX IX_SM_EmployeeID               ON StockMovements(EmployeeID);
CREATE INDEX IX_SM_SupplierTaxNumber        ON StockMovements(SupplierTaxNumber);
CREATE INDEX IX_SM_MovementDate             ON StockMovements(MovementDate);
CREATE INDEX IX_PI_ProductID                ON ProductItems(ProductID);
CREATE INDEX IX_PI_BatchMovementID          ON ProductItems(BatchMovementID);
CREATE INDEX IX_AL_LogTimestamp             ON AuditLog(LogTimestamp);
GO


-- ============================================================
-- ============================================================
--  TRIGGERS â Auto-populate AuditLog
--  C# code NEVER writes to AuditLog directly.
-- ============================================================
-- ============================================================


-- ============================================================
--  TRIGGER 1: tr_StockMovements_Audit
-- ============================================================
CREATE TRIGGER tr_StockMovements_Audit
ON StockMovements
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ActionType, [Description], Username)
    SELECT
        'STOCK ' + UPPER(i.MovementType),
        'Product [' + CAST(i.ProductID AS VARCHAR(20)) + ']: '
            + CAST(i.QuantityChanged AS VARCHAR(10)) + ' units'
            + ISNULL('. Supplier: '  + s.SupplierName, '')
            + ISNULL('. Notes: '     + i.Notes,        ''),
        ISNULL(u.Username, 'System')
    FROM inserted i
    LEFT JOIN Users     u ON u.EmployeeID        = i.EmployeeID
    LEFT JOIN Suppliers s ON s.SupplierTaxNumber = i.SupplierTaxNumber;
END;
GO


-- ============================================================
--  TRIGGER 2: tr_Products_Audit
-- ============================================================
CREATE TRIGGER tr_Products_Audit
ON Products
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted)
       AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT
            'PRODUCT ADDED',
            'New product: [' + CAST(i.ProductID AS VARCHAR(20)) + '] ' + i.ProductName
                + ' | Category: ' + ISNULL(c.CategoryName, 'Unknown')
                + ' | Price: '    + CAST(i.Price AS VARCHAR(20)),
            'System'
        FROM inserted i
        LEFT JOIN Categories c ON c.CategoryID = i.CategoryID;
    END

    IF EXISTS (SELECT 1 FROM deleted)
       AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT
            'PRODUCT DELETED',
            'Product removed: [' + CAST(d.ProductID AS VARCHAR(20)) + '] ' + d.ProductName,
            'System'
        FROM deleted d;
    END
END;
GO


-- ============================================================
--  TRIGGER 3: tr_Users_Audit
-- ============================================================
CREATE TRIGGER tr_Users_Audit
ON Users
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted)
       AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT
            'USER ADDED',
            'New user: ' + i.Username + ' (' + i.[Role] + ') - EmployeeID: '
                + CAST(i.EmployeeID AS VARCHAR(20)),
            'System'
        FROM inserted i;
    END

    IF EXISTS (SELECT 1 FROM deleted)
       AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT
            'USER DELETED',
            'User removed: ' + d.Username + ' (' + d.[Role] + ')',
            'System'
        FROM deleted d;
    END
END;
GO


-- ============================================================
--  TRIGGER 4: tr_Suppliers_Audit
-- ============================================================
CREATE TRIGGER tr_Suppliers_Audit
ON Suppliers
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted)
       AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT
            'SUPPLIER ADDED',
            'New supplier: ' + i.SupplierName
                + ' | TaxNumber: ' + CAST(i.SupplierTaxNumber AS VARCHAR(20))
                + ISNULL(' | Phone: ' + i.Phone, '')
                + ISNULL(' | Email: ' + i.Email, ''),
            'System'
        FROM inserted i;
    END

    IF EXISTS (SELECT 1 FROM deleted)
       AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT
            'SUPPLIER DELETED',
            'Supplier removed: ' + d.SupplierName,
            'System'
        FROM deleted d;
    END
END;
GO


-- ============================================================
--  Verification
-- ============================================================
PRINT '============================================================';
PRINT 'InventoryDBv3 created successfully!';
PRINT '  * 10 tables';
PRINT '  * 1 view  (vw_ProductStock)';
PRINT '  * 4 triggers (auto AuditLog)';
PRINT '  * 1 sequence (seq_ProductItems)';
PRINT '  * 11 indexes';
PRINT '============================================================';
GO

SELECT
    t.name                   AS TriggerName,
    OBJECT_NAME(t.parent_id) AS OnTable,
    t.is_disabled
FROM sys.triggers t
WHERE t.parent_class = 1
ORDER BY OBJECT_NAME(t.parent_id);
GO

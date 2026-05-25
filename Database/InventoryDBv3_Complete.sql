-- ============================================================================
--  InventoryDBv3_Complete.sql
-- ============================================================================
--  Complete database creation script for InventoryDBv3
--  Last updated: May 2026
--
--  Contains:
--    * 10 Tables
--    * 1  Sequence  (seq_ProductItems)
--    * 1  View      (vw_ProductStock)
--    * 4  Triggers  (auto-populate AuditLog)
--    * 11 Indexes
--    * Seed Data    (4 users, 4 suppliers, 3 categories, 4 zones,
--                    10 products, 133 product items, 18 stock movements)
--
-- ============================================================================
--  DESIGN PRINCIPLES:
-- ============================================================================
--    1. PKs are real integers — not auto-increment — where the value has
--       business meaning:
--         Users.EmployeeID          — official 9-digit employee number
--         Suppliers.SupplierTaxNumber — tax registration number
--         Products.ProductSerialNumber — manufacturer serial number (6 digits)
--
--    2. Auto-increment IDENTITY PKs for internal reference tables:
--         Categories, StorageZones, CategorySpecTemplates,
--         ProductSpecifications, StockMovements, AuditLog
--
--    3. ProductItems.ItemID comes from SEQUENCE (seq_ProductItems)
--       — each individual unit needs a unique internal tracking ID
--
--    4. AuditLog is written ONLY by Triggers — C# code MUST NOT insert directly
--
--    5. vw_ProductStock computes Quantity / StockStatus on-the-fly
--       from ProductItems — never stored
--
-- ============================================================================
--  IMPORTANT NOTES FOR CLAUDE CODE:
-- ============================================================================
--    * ProductSerialNumber is the manufacturer serial entered manually by the
--      user (e.g. 100501, 100502 ...) — NOT auto-increment
--
--    * EmployeeID is the official employee number entered manually
--      (e.g. 199012450, 200234781 ...)
--
--    * SupplierTaxNumber is the supplier tax registration number entered
--      manually (e.g. 218551001, 218551002 ...)
--
--    * CategoryID = 1 (Computers), 2 (Printers), 3 (Smartphones)
--
--    * Each StockIn / Restock movement generates N ProductItems rows
--      (N = QuantityChanged)
--    * Each StockOut / ReturnToSupplier sets IsInStock = 0 on existing rows
--    * vw_ProductStock counts IsInStock = 1 rows for current quantity
-- ============================================================================

USE master;
GO

SET NOCOUNT ON;
GO


-- ============================================================================
--  Drop existing database if present (clean install)
-- ============================================================================
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


-- ============================================================================
--  SEQUENCE: seq_ProductItems
-- ============================================================================
CREATE SEQUENCE seq_ProductItems
    AS INT
    START WITH 1
    INCREMENT BY 1
    NO CYCLE;
GO


-- ============================================================================
--  TABLES
-- ============================================================================

-- ----------------------------------------------------------------
--  Users  (PK: EmployeeID — real employee number, entered manually)
-- ----------------------------------------------------------------
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

-- ----------------------------------------------------------------
--  Suppliers  (PK: SupplierTaxNumber — tax registration number)
-- ----------------------------------------------------------------
CREATE TABLE Suppliers (
    SupplierTaxNumber  INT            NOT NULL,
    SupplierName       NVARCHAR(255)  NOT NULL,
    Phone              NVARCHAR(255)  NULL,
    Email              NVARCHAR(255)  NULL,
    IsActive           BIT            NOT NULL CONSTRAINT DF_Suppliers_IsActive DEFAULT (1),

    CONSTRAINT PK_Suppliers       PRIMARY KEY (SupplierTaxNumber),
    CONSTRAINT UQ_Suppliers_Name  UNIQUE      (SupplierName)
);
GO

-- ----------------------------------------------------------------
--  Categories  (PK: CategoryID — auto-increment)
-- ----------------------------------------------------------------
CREATE TABLE Categories (
    CategoryID    INT            IDENTITY(1,1) NOT NULL,
    CategoryName  NVARCHAR(255)  NOT NULL,

    CONSTRAINT PK_Categories      PRIMARY KEY (CategoryID),
    CONSTRAINT UQ_Categories_Name UNIQUE      (CategoryName)
);
GO

-- ----------------------------------------------------------------
--  StorageZones  (PK: ZoneID — auto-increment, FK: CategoryID)
-- ----------------------------------------------------------------
CREATE TABLE StorageZones (
    ZoneID       INT            IDENTITY(1,1) NOT NULL,
    ZoneName     NVARCHAR(255)  NOT NULL,
    CategoryID   INT            NOT NULL,

    CONSTRAINT PK_StorageZones      PRIMARY KEY (ZoneID),
    CONSTRAINT UQ_StorageZones_Name UNIQUE      (ZoneName),
    CONSTRAINT FK_Zone_Category     FOREIGN KEY (CategoryID)
        REFERENCES Categories(CategoryID)
        ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  Products  (PK: ProductSerialNumber — real serial, entered manually)
--
--  NOTE: formerly named ProductID — renamed in latest schema revision.
--        All Models and DAL queries use ProductSerialNumber.
-- ----------------------------------------------------------------
CREATE TABLE Products (
    ProductSerialNumber  INT            NOT NULL,
    ProductName          NVARCHAR(255)  NOT NULL,
    CategoryID           INT            NOT NULL,
    Price                DECIMAL(18,2)  NOT NULL,

    CONSTRAINT PK_Products         PRIMARY KEY (ProductSerialNumber),
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID)
        REFERENCES Categories(CategoryID)
);
GO

-- ----------------------------------------------------------------
--  CategorySpecTemplates  (PK: TemplateID — auto-increment)
-- ----------------------------------------------------------------
CREATE TABLE CategorySpecTemplates (
    TemplateID   INT            IDENTITY(1,1) NOT NULL,
    CategoryID   INT            NOT NULL,
    SpecKey      NVARCHAR(255)  NOT NULL,

    CONSTRAINT PK_CategorySpecTemplates PRIMARY KEY (TemplateID),
    CONSTRAINT UQ_CST_Category_SpecKey  UNIQUE      (CategoryID, SpecKey),
    CONSTRAINT FK_CST_Category          FOREIGN KEY (CategoryID)
        REFERENCES Categories(CategoryID)
        ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  ProductSpecifications  (PK: SpecID — auto-increment,
--                          FK: ProductSerialNumber)
-- ----------------------------------------------------------------
CREATE TABLE ProductSpecifications (
    SpecID               INT            IDENTITY(1,1) NOT NULL,
    ProductSerialNumber  INT            NOT NULL,
    SpecKey              NVARCHAR(255)  NOT NULL,
    SpecValue            NVARCHAR(255)  NOT NULL,

    CONSTRAINT PK_ProductSpecifications PRIMARY KEY (SpecID),
    CONSTRAINT UQ_PS_Product_SpecKey    UNIQUE      (ProductSerialNumber, SpecKey),
    CONSTRAINT FK_Specs_Product         FOREIGN KEY (ProductSerialNumber)
        REFERENCES Products(ProductSerialNumber)
        ON DELETE CASCADE
);
GO

-- ----------------------------------------------------------------
--  StockMovements  (PK: MovementID — auto-increment,
--                   FK: ProductSerialNumber, EmployeeID, SupplierTaxNumber)
-- ----------------------------------------------------------------
CREATE TABLE StockMovements (
    MovementID           INT            IDENTITY(1,1) NOT NULL,
    ProductSerialNumber  INT            NOT NULL,
    MovementType         NVARCHAR(255)  NOT NULL,
    QuantityChanged      INT            NOT NULL,
    MovementDate         DATETIME       NOT NULL CONSTRAINT DF_SM_MovementDate DEFAULT (GETDATE()),
    EmployeeID           INT            NULL,
    SupplierTaxNumber    INT            NULL,
    WarrantyMonths       INT            NULL,
    Notes                NVARCHAR(255)  NULL,

    CONSTRAINT PK_StockMovements   PRIMARY KEY (MovementID),
    CONSTRAINT CK_SM_Type          CHECK (MovementType IN ('StockIn','StockOut','Restock','ReturnToSupplier')),

    CONSTRAINT FK_SM_Product       FOREIGN KEY (ProductSerialNumber)
        REFERENCES Products(ProductSerialNumber),
    CONSTRAINT FK_SM_User          FOREIGN KEY (EmployeeID)
        REFERENCES Users(EmployeeID)
        ON DELETE SET NULL,
    CONSTRAINT FK_SM_Supplier      FOREIGN KEY (SupplierTaxNumber)
        REFERENCES Suppliers(SupplierTaxNumber)
        ON DELETE SET NULL
);
GO

-- ----------------------------------------------------------------
--  ProductItems  (PK: ItemID — from seq_ProductItems sequence,
--                 FK: ProductSerialNumber, BatchMovementID)
--
--  NOTE: C# code must use SELECT NEXT VALUE FOR seq_ProductItems
--        to obtain ItemID before inserting. Never use IDENTITY here.
-- ----------------------------------------------------------------
CREATE TABLE ProductItems (
    ItemID               INT       NOT NULL,
    ProductSerialNumber  INT       NOT NULL,
    IsInStock            BIT       NOT NULL CONSTRAINT DF_PI_IsInStock DEFAULT (1),
    DateAdded            DATETIME  NOT NULL CONSTRAINT DF_PI_DateAdded DEFAULT (GETDATE()),
    DateRemoved          DATETIME  NULL,
    BatchMovementID      INT       NULL,

    CONSTRAINT PK_ProductItems   PRIMARY KEY (ItemID),
    CONSTRAINT FK_PI_Product     FOREIGN KEY (ProductSerialNumber)
        REFERENCES Products(ProductSerialNumber),
    CONSTRAINT FK_PI_Movement    FOREIGN KEY (BatchMovementID)
        REFERENCES StockMovements(MovementID)
        ON DELETE SET NULL
);
GO

-- ----------------------------------------------------------------
--  AuditLog  (PK: LogID — auto-increment, no FK on Username)
--
--  WARNING: Populated ONLY by Triggers — never by C# code directly.
-- ----------------------------------------------------------------
CREATE TABLE AuditLog (
    LogID         INT            IDENTITY(1,1) NOT NULL,
    LogTimestamp  DATETIME       NOT NULL CONSTRAINT DF_AL_LogTimestamp DEFAULT (GETDATE()),
    ActionType    NVARCHAR(255)  NOT NULL,
    [Description] NVARCHAR(255)  NOT NULL,
    Username      NVARCHAR(255)  NOT NULL,

    CONSTRAINT PK_AuditLog PRIMARY KEY (LogID)
);
GO


-- ============================================================================
--  INDEXES
-- ============================================================================
CREATE INDEX IX_Products_CategoryID         ON Products(CategoryID);
CREATE INDEX IX_StorageZones_CategoryID     ON StorageZones(CategoryID);
CREATE INDEX IX_CST_CategoryID              ON CategorySpecTemplates(CategoryID);
CREATE INDEX IX_PS_ProductSerialNumber      ON ProductSpecifications(ProductSerialNumber);
CREATE INDEX IX_SM_ProductSerialNumber      ON StockMovements(ProductSerialNumber);
CREATE INDEX IX_SM_EmployeeID               ON StockMovements(EmployeeID);
CREATE INDEX IX_SM_SupplierTaxNumber        ON StockMovements(SupplierTaxNumber);
CREATE INDEX IX_SM_MovementDate             ON StockMovements(MovementDate);
CREATE INDEX IX_PI_ProductSerialNumber      ON ProductItems(ProductSerialNumber);
CREATE INDEX IX_PI_BatchMovementID          ON ProductItems(BatchMovementID);
CREATE INDEX IX_AL_LogTimestamp             ON AuditLog(LogTimestamp);
GO


-- ============================================================================
--  VIEW: vw_ProductStock
-- ============================================================================
CREATE VIEW vw_ProductStock AS
SELECT
    p.ProductSerialNumber,
    p.ProductName,
    p.CategoryID,
    c.CategoryName,
    p.Price,
    COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) AS Quantity,
    CASE
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) = 0  THEN 'Out of Stock'
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) <= 10 THEN 'Low Stock'
        ELSE 'In Stock'
    END AS StockStatus
FROM Products p
INNER JOIN Categories   c  ON c.CategoryID          = p.CategoryID
LEFT  JOIN ProductItems pi ON pi.ProductSerialNumber = p.ProductSerialNumber
GROUP BY
    p.ProductSerialNumber, p.ProductName, p.CategoryID, c.CategoryName, p.Price;
GO


-- ============================================================================
--  TRIGGERS  —  Auto-populate AuditLog
--  C# code MUST NOT write to AuditLog directly.
-- ============================================================================

-- Trigger 1: StockMovements
CREATE TRIGGER tr_StockMovements_Audit
ON StockMovements
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO AuditLog (ActionType, [Description], Username)
    SELECT
        'STOCK ' + UPPER(i.MovementType),
        'Product [' + CAST(i.ProductSerialNumber AS VARCHAR(20)) + ']: '
            + CAST(i.QuantityChanged AS VARCHAR(10)) + ' units'
            + ISNULL('. Supplier: '  + s.SupplierName, '')
            + ISNULL('. Notes: '     + i.Notes,        ''),
        ISNULL(u.Username, 'System')
    FROM inserted i
    LEFT JOIN Users     u ON u.EmployeeID        = i.EmployeeID
    LEFT JOIN Suppliers s ON s.SupplierTaxNumber = i.SupplierTaxNumber;
END;
GO

-- Trigger 2: Products
CREATE TRIGGER tr_Products_Audit
ON Products
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT
            'PRODUCT ADDED',
            'New product: [' + CAST(i.ProductSerialNumber AS VARCHAR(20)) + '] ' + i.ProductName
                + ' | Category: ' + ISNULL(c.CategoryName, 'Unknown')
                + ' | Price: '    + CAST(i.Price AS VARCHAR(20)),
            'System'
        FROM inserted i
        LEFT JOIN Categories c ON c.CategoryID = i.CategoryID;
    END
    IF EXISTS (SELECT 1 FROM deleted) AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT
            'PRODUCT DELETED',
            'Product removed: [' + CAST(d.ProductSerialNumber AS VARCHAR(20)) + '] ' + d.ProductName,
            'System'
        FROM deleted d;
    END
END;
GO

-- Trigger 3: Users
CREATE TRIGGER tr_Users_Audit
ON Users
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT
            'USER ADDED',
            'New user: ' + i.Username + ' (' + i.[Role] + ') - EmployeeID: '
                + CAST(i.EmployeeID AS VARCHAR(20)),
            'System'
        FROM inserted i;
    END
    IF EXISTS (SELECT 1 FROM deleted) AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT 'USER DELETED', 'User removed: ' + d.Username + ' (' + d.[Role] + ')', 'System'
        FROM deleted d;
    END
END;
GO

-- Trigger 4: Suppliers
CREATE TRIGGER tr_Suppliers_Audit
ON Suppliers
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
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
    IF EXISTS (SELECT 1 FROM deleted) AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO AuditLog (ActionType, [Description], Username)
        SELECT 'SUPPLIER DELETED', 'Supplier removed: ' + d.SupplierName, 'System'
        FROM deleted d;
    END
END;
GO


-- ============================================================================
--  SEED DATA
--  Inserted after triggers exist so AuditLog is auto-populated.
-- ============================================================================

-- Users (4)
INSERT INTO Users (EmployeeID, Username, [Password], [Role], IsAdmin) VALUES
    (199012450, N'suad.admin',   N'Admin@2026',  N'System Administrator', 1),
    (200234781, N'ahmed.clerk',  N'123',         N'Employee',             0),
    (199856923, N'fatima.clerk', N'Fatima@2026', N'Employee',             0),
    (200145678, N'omar.clerk',   N'Omar@2026',   N'Employee',             0);
GO

-- Suppliers (4)
INSERT INTO Suppliers (SupplierTaxNumber, SupplierName, Phone, Email, IsActive) VALUES
    (218551001, N'Golden Tech Company',    N'0913456789', N'sales@goldentech.ly',    1),
    (218551002, N'Digital Horizon Office', N'0925678901', N'info@digitalhorizon.ly', 1),
    (218551003, N'Al-Noor Computers',      N'0911234567', N'orders@alnoor-pc.ly',    1),
    (218551004, N'Modern Electronics',     N'0934567890', N'contact@modern-e.ly',    1);
GO

-- Categories (3)
SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (CategoryID, CategoryName) VALUES
    (1, N'Computers'),
    (2, N'Printers'),
    (3, N'Smartphones');
SET IDENTITY_INSERT Categories OFF;
GO

-- StorageZones (4)
SET IDENTITY_INSERT StorageZones ON;
INSERT INTO StorageZones (ZoneID, ZoneName, CategoryID) VALUES
    (1, N'Computers Zone - Shelf A', 1),
    (2, N'Computers Zone - Shelf B', 1),
    (3, N'Printers Zone - Shelf C',  2),
    (4, N'Smartphones Zone - Shelf D', 3);
SET IDENTITY_INSERT StorageZones OFF;
GO

-- CategorySpecTemplates (15)
SET IDENTITY_INSERT CategorySpecTemplates ON;
INSERT INTO CategorySpecTemplates (TemplateID, CategoryID, SpecKey) VALUES
    (1,  1, N'Screen'),
    (2,  1, N'Processor'),
    (3,  1, N'RAM'),
    (4,  1, N'Storage'),
    (5,  1, N'Battery'),
    (6,  2, N'Type'),
    (7,  2, N'Speed'),
    (8,  2, N'Connectivity'),
    (9,  2, N'Paper Size'),
    (10, 2, N'Print Method'),
    (11, 3, N'Screen'),
    (12, 3, N'Processor'),
    (13, 3, N'Storage'),
    (14, 3, N'Camera'),
    (15, 3, N'Battery');
SET IDENTITY_INSERT CategorySpecTemplates OFF;
GO

-- Products (10)
INSERT INTO Products (ProductSerialNumber, ProductName, CategoryID, Price) VALUES
    (100501, N'Dell Latitude 5540',     1, 3200.00),
    (100502, N'HP EliteBook 840 G10',   1, 3750.00),
    (100503, N'Lenovo ThinkPad E15',    1, 2900.00),
    (100504, N'Asus VivoBook 15',       1, 2100.00),
    (100505, N'HP LaserJet Pro M404dn', 2,  980.00),
    (100506, N'Canon PIXMA G3420',      2,  520.00),
    (100507, N'Epson EcoTank L3250',    2,  610.00),
    (100508, N'Samsung Galaxy A55',     3, 1450.00),
    (100509, N'iPhone 15',              3, 4800.00),
    (100510, N'Xiaomi Redmi Note 13',   3,  870.00);
GO

-- ProductSpecifications (50 — 5 per product)
INSERT INTO ProductSpecifications (ProductSerialNumber, SpecKey, SpecValue) VALUES
    (100501, N'Screen',    N'15.6" FHD IPS'),
    (100501, N'Processor', N'Intel Core i7-1355U'),
    (100501, N'RAM',       N'16 GB DDR4'),
    (100501, N'Storage',   N'512 GB SSD NVMe'),
    (100501, N'Battery',   N'54 Wh'),
    (100502, N'Screen',    N'14" FHD IPS'),
    (100502, N'Processor', N'Intel Core i7-1365U'),
    (100502, N'RAM',       N'16 GB DDR5'),
    (100502, N'Storage',   N'512 GB SSD NVMe'),
    (100502, N'Battery',   N'51 Wh'),
    (100503, N'Screen',    N'15.6" FHD IPS'),
    (100503, N'Processor', N'Intel Core i5-1235U'),
    (100503, N'RAM',       N'8 GB DDR4'),
    (100503, N'Storage',   N'256 GB SSD'),
    (100503, N'Battery',   N'45 Wh'),
    (100504, N'Screen',    N'15.6" FHD'),
    (100504, N'Processor', N'Intel Core i3-1215U'),
    (100504, N'RAM',       N'8 GB DDR4'),
    (100504, N'Storage',   N'512 GB SSD'),
    (100504, N'Battery',   N'37 Wh'),
    (100505, N'Type',         N'Monochrome Laser'),
    (100505, N'Speed',        N'38 ppm'),
    (100505, N'Connectivity', N'USB / Ethernet'),
    (100505, N'Paper Size',   N'A4 / Letter'),
    (100505, N'Print Method', N'Single-sided'),
    (100506, N'Type',         N'Color Inkjet'),
    (100506, N'Speed',        N'10 ppm'),
    (100506, N'Connectivity', N'USB / WiFi'),
    (100506, N'Paper Size',   N'A4 / Letter'),
    (100506, N'Print Method', N'Ink Tank'),
    (100507, N'Type',         N'Color Inkjet'),
    (100507, N'Speed',        N'10 ppm'),
    (100507, N'Connectivity', N'USB / WiFi'),
    (100507, N'Paper Size',   N'A4 / Letter'),
    (100507, N'Print Method', N'EcoTank'),
    (100508, N'Screen',    N'6.6" Super AMOLED'),
    (100508, N'Processor', N'Exynos 1480'),
    (100508, N'Storage',   N'128 GB / 8 GB RAM'),
    (100508, N'Camera',    N'50 MP + 12 MP + 5 MP'),
    (100508, N'Battery',   N'5000 mAh'),
    (100509, N'Screen',    N'6.1" Super Retina XDR OLED'),
    (100509, N'Processor', N'Apple A16 Bionic'),
    (100509, N'Storage',   N'128 GB / 6 GB RAM'),
    (100509, N'Camera',    N'48 MP Main + 12 MP Ultra Wide'),
    (100509, N'Battery',   N'3877 mAh'),
    (100510, N'Screen',    N'6.67" AMOLED'),
    (100510, N'Processor', N'Snapdragon 685'),
    (100510, N'Storage',   N'128 GB / 6 GB RAM'),
    (100510, N'Camera',    N'108 MP + 8 MP + 2 MP'),
    (100510, N'Battery',   N'5000 mAh');
GO

-- StockMovements (18: 10 StockIn, 5 StockOut, 2 Restock, 1 ReturnToSupplier)
SET IDENTITY_INSERT StockMovements ON;
INSERT INTO StockMovements
    (MovementID, ProductSerialNumber, MovementType, QuantityChanged, MovementDate, EmployeeID, SupplierTaxNumber, WarrantyMonths, Notes)
VALUES
    (1,  100501, N'StockIn',          15, '2026-01-10T09:00:00', 200234781, 218551003, 24, N'Opening batch - Dell Latitude 5540'),
    (2,  100502, N'StockIn',          10, '2026-01-10T09:30:00', 200234781, 218551003, 24, N'Opening batch - HP EliteBook 840 G10'),
    (3,  100503, N'StockIn',          12, '2026-01-11T10:00:00', 199856923, 218551001, 24, N'Opening batch - Lenovo ThinkPad E15'),
    (4,  100504, N'StockIn',          20, '2026-01-11T10:30:00', 199856923, 218551001, 12, N'Opening batch - Asus VivoBook 15'),
    (5,  100505, N'StockIn',           8, '2026-01-12T08:00:00', 200145678, 218551002, 12, N'Opening batch - HP LaserJet Pro M404dn'),
    (6,  100506, N'StockIn',          15, '2026-01-12T08:30:00', 200145678, 218551002, 12, N'Opening batch - Canon PIXMA G3420'),
    (7,  100507, N'StockIn',          10, '2026-01-12T09:00:00', 200234781, 218551004, 12, N'Opening batch - Epson EcoTank L3250'),
    (8,  100508, N'StockIn',          25, '2026-01-13T09:00:00', 199856923, 218551004, 12, N'Opening batch - Samsung Galaxy A55'),
    (9,  100509, N'StockIn',          10, '2026-01-13T09:30:00', 199856923, 218551004, 12, N'Opening batch - iPhone 15'),
    (10, 100510, N'StockIn',          30, '2026-01-13T10:00:00', 200145678, 218551001, 12, N'Opening batch - Xiaomi Redmi Note 13'),
    (11, 100501, N'StockOut',          3, '2026-02-05T11:00:00', 200234781, NULL,      NULL, N'Sale - Dell Latitude 5540'),
    (12, 100508, N'StockOut',          7, '2026-02-10T13:00:00', 199856923, NULL,      NULL, N'Sale - Samsung Galaxy A55'),
    (13, 100509, N'StockOut',          2, '2026-02-15T14:00:00', 200234781, NULL,      NULL, N'Sale - iPhone 15'),
    (14, 100510, N'StockOut',          5, '2026-03-01T10:00:00', 200145678, NULL,      NULL, N'Sale - Xiaomi Redmi Note 13'),
    (15, 100506, N'StockOut',          3, '2026-03-05T09:00:00', 199856923, NULL,      NULL, N'Sale - Canon PIXMA G3420'),
    (16, 100503, N'Restock',           8, '2026-03-20T08:00:00', 200234781, 218551001, 24, N'Restock - Lenovo ThinkPad E15'),
    (17, 100509, N'Restock',           5, '2026-04-01T09:00:00', 199856923, 218551004, 12, N'Restock - iPhone 15'),
    (18, 100504, N'ReturnToSupplier',  2, '2026-03-15T10:00:00', 200145678, 218551001, NULL, N'Return defective units - Asus VivoBook 15');
SET IDENTITY_INSERT StockMovements OFF;
GO

-- ProductItems (133 individual units)
DECLARE @i INT;

-- Movement 1: Dell Latitude x15  (first 3 sold via Movement 11)
SET @i = 1;
WHILE @i <= 15
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100501,
            CASE WHEN @i <= 3 THEN 0 ELSE 1 END,
            '2026-01-10T09:00:00',
            CASE WHEN @i <= 3 THEN '2026-02-05T11:00:00' ELSE NULL END, 1);
    SET @i = @i + 1;
END;

-- Movement 2: HP EliteBook x10
SET @i = 1;
WHILE @i <= 10
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100502, 1, '2026-01-10T09:30:00', NULL, 2);
    SET @i = @i + 1;
END;

-- Movement 3: Lenovo ThinkPad x12
SET @i = 1;
WHILE @i <= 12
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100503, 1, '2026-01-11T10:00:00', NULL, 3);
    SET @i = @i + 1;
END;

-- Movement 4: Asus VivoBook x20  (first 2 returned via Movement 18)
SET @i = 1;
WHILE @i <= 20
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100504,
            CASE WHEN @i <= 2 THEN 0 ELSE 1 END,
            '2026-01-11T10:30:00',
            CASE WHEN @i <= 2 THEN '2026-03-15T10:00:00' ELSE NULL END, 4);
    SET @i = @i + 1;
END;

-- Movement 5: HP LaserJet x8
SET @i = 1;
WHILE @i <= 8
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100505, 1, '2026-01-12T08:00:00', NULL, 5);
    SET @i = @i + 1;
END;

-- Movement 6: Canon PIXMA x15  (first 3 sold via Movement 15)
SET @i = 1;
WHILE @i <= 15
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100506,
            CASE WHEN @i <= 3 THEN 0 ELSE 1 END,
            '2026-01-12T08:30:00',
            CASE WHEN @i <= 3 THEN '2026-03-05T09:00:00' ELSE NULL END, 6);
    SET @i = @i + 1;
END;

-- Movement 7: Epson EcoTank x10
SET @i = 1;
WHILE @i <= 10
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100507, 1, '2026-01-12T09:00:00', NULL, 7);
    SET @i = @i + 1;
END;

-- Movement 8: Samsung Galaxy A55 x25  (first 7 sold via Movement 12)
SET @i = 1;
WHILE @i <= 25
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100508,
            CASE WHEN @i <= 7 THEN 0 ELSE 1 END,
            '2026-01-13T09:00:00',
            CASE WHEN @i <= 7 THEN '2026-02-10T13:00:00' ELSE NULL END, 8);
    SET @i = @i + 1;
END;

-- Movement 9: iPhone 15 x10  (first 2 sold via Movement 13)
SET @i = 1;
WHILE @i <= 10
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100509,
            CASE WHEN @i <= 2 THEN 0 ELSE 1 END,
            '2026-01-13T09:30:00',
            CASE WHEN @i <= 2 THEN '2026-02-15T14:00:00' ELSE NULL END, 9);
    SET @i = @i + 1;
END;

-- Movement 10: Xiaomi Redmi x30  (first 5 sold via Movement 14)
SET @i = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100510,
            CASE WHEN @i <= 5 THEN 0 ELSE 1 END,
            '2026-01-13T10:00:00',
            CASE WHEN @i <= 5 THEN '2026-03-01T10:00:00' ELSE NULL END, 10);
    SET @i = @i + 1;
END;

-- Movement 16 (Restock): Lenovo ThinkPad +8
SET @i = 1;
WHILE @i <= 8
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100503, 1, '2026-03-20T08:00:00', NULL, 16);
    SET @i = @i + 1;
END;

-- Movement 17 (Restock): iPhone 15 +5
SET @i = 1;
WHILE @i <= 5
BEGIN
    INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID)
    VALUES (NEXT VALUE FOR seq_ProductItems, 100509, 1, '2026-04-01T09:00:00', NULL, 17);
    SET @i = @i + 1;
END;
GO


-- ============================================================================
--  Verification
-- ============================================================================
PRINT '============================================================';
PRINT ' InventoryDBv3 created successfully!';
PRINT '============================================================';

SELECT 'Categories'           AS [Table], COUNT(*) AS [Count] FROM Categories
UNION ALL SELECT 'StorageZones',          COUNT(*) FROM StorageZones
UNION ALL SELECT 'Users',                 COUNT(*) FROM Users
UNION ALL SELECT 'Suppliers',             COUNT(*) FROM Suppliers
UNION ALL SELECT 'Products',              COUNT(*) FROM Products
UNION ALL SELECT 'ProductSpecifications', COUNT(*) FROM ProductSpecifications
UNION ALL SELECT 'CategorySpecTemplates', COUNT(*) FROM CategorySpecTemplates
UNION ALL SELECT 'StockMovements',        COUNT(*) FROM StockMovements
UNION ALL SELECT 'ProductItems',          COUNT(*) FROM ProductItems
UNION ALL SELECT 'AuditLog',              COUNT(*) FROM AuditLog;

SELECT * FROM vw_ProductStock ORDER BY ProductSerialNumber;

SELECT t.name AS TriggerName, OBJECT_NAME(t.parent_id) AS OnTable,
       CASE WHEN t.is_disabled = 0 THEN 'Active' ELSE 'Disabled' END AS [Status]
FROM sys.triggers t WHERE t.parent_class = 1 ORDER BY OBJECT_NAME(t.parent_id);
GO

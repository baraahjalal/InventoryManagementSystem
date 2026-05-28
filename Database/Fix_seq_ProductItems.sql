-- ItemID is built in the app: ProductSerialNumber + 2/3-digit suffix (NOT global seq_ProductItems).
-- Run once on InventoryDBv3 to remove the old global-sequence default on ItemID.
USE [InventoryDBv3];
GO

DECLARE @dfName NVARCHAR(256);
SELECT @dfName = dc.name
FROM sys.default_constraints dc
JOIN sys.columns c
  ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
WHERE dc.parent_object_id = OBJECT_ID(N'dbo.ProductItems')
  AND c.name = N'ItemID';

IF @dfName IS NOT NULL
    EXEC(N'ALTER TABLE dbo.ProductItems DROP CONSTRAINT [' + @dfName + N']');
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.default_constraints dc
    JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
    WHERE dc.parent_object_id = OBJECT_ID(N'dbo.ProductItems') AND c.name = N'DateAdded'
)
BEGIN
    ALTER TABLE [dbo].[ProductItems]
    ADD CONSTRAINT [DF_ProductItems_DateAdded] DEFAULT (GETDATE()) FOR [DateAdded];
END
GO

PRINT N'ItemID: app assigns ProductSerial + suffix (e.g. 10050101). Global seq_ProductItems default removed.';
GO

-- ============================================================
-- sp_PurgeOldData
-- Permanently deletes old records from AuditLog and ProductItems.
-- @OlderThanYears : delete records older than this many years
-- @AuditLogDeleted    OUTPUT : rows deleted from AuditLog
-- @ProductItemsDeleted OUTPUT : rows deleted from ProductItems
-- ============================================================
CREATE OR ALTER PROCEDURE sp_PurgeOldData
    @OlderThanYears      INT,
    @AuditLogDeleted     INT OUTPUT,
    @ProductItemsDeleted INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM AuditLog
    WHERE LogTimestamp < DATEADD(YEAR, -@OlderThanYears, GETDATE());
    SET @AuditLogDeleted = @@ROWCOUNT;

    DELETE FROM ProductItems
    WHERE IsInStock    = 0
      AND DateRemoved IS NOT NULL
      AND DateRemoved  < DATEADD(YEAR, -@OlderThanYears, GETDATE());
    SET @ProductItemsDeleted = @@ROWCOUNT;
END;

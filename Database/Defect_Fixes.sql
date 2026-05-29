-- ============================================================
-- DEFECT-01: Widen Password column to nvarchar(256)
-- (SHA-256 hex output is 64 chars; current column is nvarchar(255)
--  which already fits, but we align to the spec of 256.)
-- Run against InventoryDBv3
-- ============================================================
ALTER TABLE [dbo].[Users]
    ALTER COLUMN [Password] NVARCHAR(256) NOT NULL;
GO

-- ============================================================
-- DEFECT-05: Audit trigger for UPDATE on Users
-- Logs password, role, and admin-flag changes to AuditLog.
-- ============================================================
IF OBJECT_ID('dbo.tr_Users_Audit_Update', 'TR') IS NOT NULL
    DROP TRIGGER dbo.tr_Users_Audit_Update;
GO

CREATE TRIGGER [dbo].[tr_Users_Audit_Update]
ON [dbo].[Users]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Password) OR UPDATE(Role) OR UPDATE(IsAdmin)
    BEGIN
        INSERT INTO AuditLog (LogTimestamp, ActionType, [Description], Username)
        SELECT
            GETDATE(),
            'USER MODIFIED',
            'User account updated: ' + COALESCE(i.Username, 'Unknown'),
            SYSTEM_USER
        FROM inserted i;
    END
END;
GO

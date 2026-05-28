-- ============================================================
-- DataRetentionSettings table
-- Stores the admin's automatic data retention configuration.
-- Only ever contains one row (Id = 1).
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DataRetentionSettings')
BEGIN
    CREATE TABLE DataRetentionSettings (
        Id                 INT           NOT NULL PRIMARY KEY DEFAULT 1,
        RetentionYears     INT           NOT NULL DEFAULT 3,
        WarnIntervalMonths INT           NOT NULL DEFAULT 3,
        IsEnabled          BIT           NOT NULL DEFAULT 1,
        LastWarnDate       DATETIME      NULL
    );

    INSERT INTO DataRetentionSettings (Id, RetentionYears, WarnIntervalMonths, IsEnabled, LastWarnDate)
    VALUES (1, 3, 3, 1, NULL);
END;

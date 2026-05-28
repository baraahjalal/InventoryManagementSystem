using System;
using System.Data;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Properties;

namespace InventoryManagementSystem.DAL
{
    public static class DataMaintenanceRepository
    {
        // ── Retention settings ────────────────────────────────────────────────

        public static RetentionSettings GetRetentionSettings()
        {
            DateTime? lastWarn = null;
            if (DateTime.TryParse(Settings.Default.LastWarnDate, out DateTime parsed))
                lastWarn = parsed;

            return new RetentionSettings
            {
                RetentionYears     = Settings.Default.RetentionYears,
                WarnIntervalMonths = Settings.Default.WarnIntervalMonths,
                IsEnabled          = Settings.Default.RetentionEnabled,
                LastWarnDate       = lastWarn
            };
        }

        public static void SaveRetentionSettings(int retentionYears, int warnIntervalMonths, bool isEnabled)
        {
            Settings.Default.RetentionYears     = retentionYears;
            Settings.Default.WarnIntervalMonths = warnIntervalMonths;
            Settings.Default.RetentionEnabled   = isEnabled;
            Settings.Default.Save();
        }

        public static void UpdateLastWarnDate()
        {
            Settings.Default.LastWarnDate = DateTime.Now.ToString("o");
            Settings.Default.Save();
        }

        // ── Auto-purge ────────────────────────────────────────────────────────

        public static void PurgeOldData(int olderThanYears, out int auditDeleted, out int itemsDeleted)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_PurgeOldData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OlderThanYears", olderThanYears);

                    var pAudit = cmd.Parameters.Add("@AuditLogDeleted",     SqlDbType.Int);
                    pAudit.Direction = ParameterDirection.Output;

                    var pItems = cmd.Parameters.Add("@ProductItemsDeleted", SqlDbType.Int);
                    pItems.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    auditDeleted = (int)pAudit.Value;
                    itemsDeleted = (int)pItems.Value;
                }
            }
        }
    }
}

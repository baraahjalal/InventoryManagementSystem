using System;
using System.Data;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class DataMaintenanceRepository
    {
        // ── Retention settings ────────────────────────────────────────────────

        public static RetentionSettings GetRetentionSettings()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT RetentionYears, WarnIntervalMonths, IsEnabled, LastWarnDate " +
                    "FROM DataRetentionSettings WHERE Id = 1", conn))
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return new RetentionSettings();
                    return new RetentionSettings
                    {
                        RetentionYears     = r.GetInt32(0),
                        WarnIntervalMonths = r.GetInt32(1),
                        IsEnabled          = r.GetBoolean(2),
                        LastWarnDate       = r.IsDBNull(3) ? (DateTime?)null : r.GetDateTime(3)
                    };
                }
            }
        }

        public static void SaveRetentionSettings(int retentionYears, int warnIntervalMonths, bool isEnabled)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE DataRetentionSettings " +
                    "SET RetentionYears = @ry, WarnIntervalMonths = @wim, IsEnabled = @en " +
                    "WHERE Id = 1", conn))
                {
                    cmd.Parameters.AddWithValue("@ry",  retentionYears);
                    cmd.Parameters.AddWithValue("@wim", warnIntervalMonths);
                    cmd.Parameters.AddWithValue("@en",  isEnabled);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateLastWarnDate()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE DataRetentionSettings SET LastWarnDate = GETDATE() WHERE Id = 1", conn))
                    cmd.ExecuteNonQuery();
            }
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

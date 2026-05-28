using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class AuditLogRepository
    {
        // ── Legacy methods (kept for compatibility) ───────────────────────────

        public static List<AuditLogEntry> GetAll()
        {
            var list = new List<AuditLogEntry>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT LogId, LogTimestamp, ActionType, Description, Username " +
                    "FROM AuditLog ORDER BY LogTimestamp DESC", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapEntry(r));
            }
            return list;
        }

        public static List<AuditLogEntry> GetByUser(string username)
        {
            var list = new List<AuditLogEntry>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT LogId, LogTimestamp, ActionType, Description, Username " +
                    "FROM AuditLog WHERE Username = @u ORDER BY LogTimestamp DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapEntry(r));
                }
            }
            return list;
        }

        public static List<AuditLogEntry> GetByDateRange(DateTime from, DateTime to)
        {
            var list = new List<AuditLogEntry>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT LogId, LogTimestamp, ActionType, Description, Username " +
                    "FROM AuditLog WHERE LogTimestamp BETWEEN @from AND @to " +
                    "ORDER BY LogTimestamp DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to",   to);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapEntry(r));
                }
            }
            return list;
        }

        // ── Paginated query ───────────────────────────────────────────────────

        public static List<AuditLogEntry> GetPaged(int page, int pageSize,
            string actionTypeKey = null, string search = null,
            DateTime? from = null, DateTime? to = null)
        {
            var list   = new List<AuditLogEntry>();
            int offset = (page - 1) * pageSize;
            string where = BuildWhereClause(actionTypeKey, search, from, to);
            string sql =
                "SELECT LogId, LogTimestamp, ActionType, Description, Username " +
                "FROM AuditLog " + where +
                " ORDER BY LogTimestamp DESC " +
                "OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    ApplyFilterParams(cmd, actionTypeKey, search, from, to);
                    cmd.Parameters.AddWithValue("@offset",   offset);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapEntry(r));
                }
            }
            return list;
        }

        public static int GetCount(string actionTypeKey = null, string search = null,
            DateTime? from = null, DateTime? to = null)
        {
            string where = BuildWhereClause(actionTypeKey, search, from, to);
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT COUNT(1) FROM AuditLog " + where, conn))
                {
                    ApplyFilterParams(cmd, actionTypeKey, search, from, to);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        // ── Maintenance helpers ───────────────────────────────────────────────

        public static DateTime? GetOldestLogDate()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT MIN(LogTimestamp) FROM AuditLog", conn))
                {
                    var result = cmd.ExecuteScalar();
                    return (result == null || result == DBNull.Value) ? (DateTime?)null : (DateTime)result;
                }
            }
        }

        public static bool HasRecordsOlderThan(int years)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM AuditLog " +
                    "WHERE LogTimestamp < DATEADD(YEAR, -@y, GETDATE())", conn))
                {
                    cmd.Parameters.AddWithValue("@y", years);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public static int CountOlderThan(int years)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM AuditLog " +
                    "WHERE LogTimestamp < DATEADD(YEAR, -@y, GETDATE())", conn))
                {
                    cmd.Parameters.AddWithValue("@y", years);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        // ── Private helpers ───────────────────────────────────────────────────

        private static string BuildWhereClause(string actionTypeKey, string search, DateTime? from, DateTime? to)
        {
            var conds = new List<string>();

            if (from.HasValue) conds.Add("LogTimestamp >= @from");
            if (to.HasValue)   conds.Add("LogTimestamp <= @to");
            if (!string.IsNullOrEmpty(search))
                conds.Add("(Description LIKE @search OR Username LIKE @search)");

            if (!string.IsNullOrEmpty(actionTypeKey))
                conds.Add("ActionType = @actionType");

            return conds.Count > 0 ? "WHERE " + string.Join(" AND ", conds) : "";
        }

        private static void ApplyFilterParams(SqlCommand cmd, string actionTypeKey, string search,
            DateTime? from, DateTime? to)
        {
            if (!string.IsNullOrEmpty(actionTypeKey))
                cmd.Parameters.AddWithValue("@actionType", actionTypeKey);
            if (from.HasValue) cmd.Parameters.AddWithValue("@from", from.Value);
            if (to.HasValue)   cmd.Parameters.AddWithValue("@to",   to.Value);
            if (!string.IsNullOrEmpty(search))
                cmd.Parameters.AddWithValue("@search", "%" + search + "%");
        }

        private static AuditLogEntry MapEntry(SqlDataReader r)
        {
            return new AuditLogEntry
            {
                LogId        = r.GetInt32(0),
                LogTimestamp = r.GetDateTime(1),
                ActionType   = r.GetString(2),
                Description  = r.GetString(3),
                Username     = r.GetString(4)
            };
        }
    }
}

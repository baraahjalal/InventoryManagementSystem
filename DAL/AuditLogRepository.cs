using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class AuditLogRepository
    {
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
                    cmd.Parameters.AddWithValue("@to", to);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapEntry(r));
                }
            }
            return list;
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

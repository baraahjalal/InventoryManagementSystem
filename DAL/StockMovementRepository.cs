using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class StockMovementRepository
    {
        // Returns the auto-generated MovementId (needed to link ProductItems via BatchMovementId)
        public static int Add(StockMovement m)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "INSERT INTO StockMovements " +
                    "(ProductSerial, MovementType, QuantityChanged, Username, Notes, WarrantyMonths, SupplierName) " +
                    "VALUES (@ps, @mt, @qc, @u, @n, @wm, @sn); " +
                    "SELECT SCOPE_IDENTITY();", conn))
                {
                    cmd.Parameters.AddWithValue("@ps", m.ProductSerial);
                    cmd.Parameters.AddWithValue("@mt", m.MovementType);
                    cmd.Parameters.AddWithValue("@qc", m.QuantityChanged);
                    cmd.Parameters.AddWithValue("@u",  (object)m.Username       ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@n",  (object)m.Notes          ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@wm", (object)m.WarrantyMonths ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@sn", (object)m.SupplierName   ?? DBNull.Value);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static List<StockMovement> GetAll()
        {
            var list = new List<StockMovement>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT MovementId, ProductSerial, MovementType, QuantityChanged, MovementDate, " +
                    "Username, Notes, WarrantyMonths, SupplierName " +
                    "FROM StockMovements ORDER BY MovementDate DESC", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapMovement(r));
            }
            return list;
        }

        public static List<StockMovement> GetByProduct(string productSerial)
        {
            var list = new List<StockMovement>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT MovementId, ProductSerial, MovementType, QuantityChanged, MovementDate, " +
                    "Username, Notes, WarrantyMonths, SupplierName " +
                    "FROM StockMovements WHERE ProductSerial = @ps ORDER BY MovementDate DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@ps", productSerial);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapMovement(r));
                }
            }
            return list;
        }

        private static StockMovement MapMovement(SqlDataReader r)
        {
            return new StockMovement
            {
                MovementId      = r.GetInt32(0),
                ProductSerial   = r.GetString(1),
                MovementType    = r.GetString(2),
                QuantityChanged = r.GetInt32(3),
                MovementDate    = r.GetDateTime(4),
                Username        = r.IsDBNull(5) ? null : r.GetString(5),
                Notes           = r.IsDBNull(6) ? null : r.GetString(6),
                WarrantyMonths  = r.IsDBNull(7) ? (int?)null : r.GetInt32(7),
                SupplierName    = r.IsDBNull(8) ? null : r.GetString(8)
            };
        }
    }
}

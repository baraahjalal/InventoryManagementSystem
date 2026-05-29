using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class StockMovementRepository
    {
        // Returns the auto-generated MovementID (needed to link ProductItems via BatchMovementId)
        public static int Add(StockMovement m)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                return Add(m, conn, null);
            }
        }

        public static int Add(StockMovement m, SqlConnection conn, SqlTransaction tran)
        {
            using (var cmd = new SqlCommand(
                "INSERT INTO StockMovements " +
                "(ProductSerialNumber, MovementType, QuantityChanged, EmployeeID, Notes, WarrantyMonths, SupplierTaxNumber) " +
                "VALUES (@pid, @mt, @qc, @eid, @n, @wm, @tax); " +
                "SELECT SCOPE_IDENTITY();", conn, tran))
            {
                cmd.Parameters.AddWithValue("@pid", m.ProductSerialNumber);
                cmd.Parameters.AddWithValue("@mt",  m.MovementType);
                cmd.Parameters.AddWithValue("@qc",  m.QuantityChanged);
                cmd.Parameters.AddWithValue("@eid", (object)m.EmployeeID        ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@n",   (object)m.Notes             ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@wm",  (object)m.WarrantyMonths    ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tax", (object)m.SupplierTaxNumber ?? DBNull.Value);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static List<StockMovement> GetAll()
        {
            var list = new List<StockMovement>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT MovementID, ProductSerialNumber, MovementType, QuantityChanged, MovementDate, " +
                    "EmployeeID, Notes, WarrantyMonths, SupplierTaxNumber " +
                    "FROM StockMovements ORDER BY MovementDate DESC", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapMovement(r));
            }
            return list;
        }

        public static List<StockMovement> GetByProduct(int productId)
        {
            var list = new List<StockMovement>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT MovementID, ProductSerialNumber, MovementType, QuantityChanged, MovementDate, " +
                    "EmployeeID, Notes, WarrantyMonths, SupplierTaxNumber " +
                    "FROM StockMovements WHERE ProductSerialNumber = @pid ORDER BY MovementDate DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", productId);
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
                MovementId        = r.GetInt32(0),
                ProductSerialNumber         = r.GetInt32(1),
                MovementType      = r.GetString(2),
                QuantityChanged   = r.GetInt32(3),
                MovementDate      = r.GetDateTime(4),
                EmployeeID        = r.IsDBNull(5) ? (int?)null : r.GetInt32(5),
                Notes             = r.IsDBNull(6) ? null       : r.GetString(6),
                WarrantyMonths    = r.IsDBNull(7) ? (int?)null : r.GetInt32(7),
                SupplierTaxNumber = r.IsDBNull(8) ? (int?)null : r.GetInt32(8)
            };
        }
    }
}

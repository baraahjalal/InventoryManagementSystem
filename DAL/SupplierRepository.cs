using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class SupplierRepository
    {
        public static List<Supplier> GetAll()
        {
            var list = new List<Supplier>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT SupplierName, Phone, Email, IsActive " +
                    "FROM Suppliers ORDER BY SupplierName", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapSupplier(r));
            }
            return list;
        }

        public static List<Supplier> GetActive()
        {
            var list = new List<Supplier>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT SupplierName, Phone, Email, IsActive " +
                    "FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapSupplier(r));
            }
            return list;
        }

        public static void Add(Supplier s)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "INSERT INTO Suppliers (SupplierName, Phone, Email, IsActive) " +
                    "VALUES (@n, @ph, @e, @a)", conn))
                {
                    cmd.Parameters.AddWithValue("@n",  s.SupplierName);
                    cmd.Parameters.AddWithValue("@ph", (object)s.Phone  ?? System.DBNull.Value);
                    cmd.Parameters.AddWithValue("@e",  (object)s.Email  ?? System.DBNull.Value);
                    cmd.Parameters.AddWithValue("@a",  s.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update(Supplier s)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE Suppliers SET Phone = @ph, Email = @e, IsActive = @a " +
                    "WHERE SupplierName = @n", conn))
                {
                    cmd.Parameters.AddWithValue("@ph", (object)s.Phone  ?? System.DBNull.Value);
                    cmd.Parameters.AddWithValue("@e",  (object)s.Email  ?? System.DBNull.Value);
                    cmd.Parameters.AddWithValue("@a",  s.IsActive);
                    cmd.Parameters.AddWithValue("@n",  s.SupplierName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string supplierName)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM Suppliers WHERE SupplierName = @n", conn))
                {
                    cmd.Parameters.AddWithValue("@n", supplierName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool Exists(string supplierName)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Suppliers WHERE SupplierName = @n", conn))
                {
                    cmd.Parameters.AddWithValue("@n", supplierName);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        private static Supplier MapSupplier(SqlDataReader r)
        {
            return new Supplier
            {
                SupplierName = r.GetString(0),
                Phone        = r.IsDBNull(1) ? null : r.GetString(1),
                Email        = r.IsDBNull(2) ? null : r.GetString(2),
                IsActive     = r.GetBoolean(3)
            };
        }
    }
}

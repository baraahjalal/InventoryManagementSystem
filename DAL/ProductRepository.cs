using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class ProductRepository
    {
        public static List<Product> GetAll()
        {
            var list = new List<Product>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT SerialNumber, ProductName, CategoryName, Price, Quantity, StockStatus " +
                    "FROM vw_ProductStock ORDER BY ProductName", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapProduct(r));
            }
            return list;
        }

        public static List<Product> GetByCategory(string categoryName)
        {
            var list = new List<Product>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT SerialNumber, ProductName, CategoryName, Price, Quantity, StockStatus " +
                    "FROM vw_ProductStock WHERE CategoryName = @c ORDER BY ProductName", conn))
                {
                    cmd.Parameters.AddWithValue("@c", categoryName);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapProduct(r));
                }
            }
            return list;
        }

        public static Product GetBySerial(string serial)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT SerialNumber, ProductName, CategoryName, Price, Quantity, StockStatus " +
                    "FROM vw_ProductStock WHERE SerialNumber = @s", conn))
                {
                    cmd.Parameters.AddWithValue("@s", serial);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) return null;
                        return MapProduct(r);
                    }
                }
            }
        }

        public static void Add(Product p)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "INSERT INTO Products (SerialNumber, ProductName, CategoryName, Price) " +
                    "VALUES (@s, @n, @c, @pr)", conn))
                {
                    cmd.Parameters.AddWithValue("@s",  p.SerialNumber);
                    cmd.Parameters.AddWithValue("@n",  p.ProductName);
                    cmd.Parameters.AddWithValue("@c",  p.CategoryName);
                    cmd.Parameters.AddWithValue("@pr", p.Price);
                    cmd.ExecuteNonQuery();
                }

                foreach (var spec in p.Specifications)
                    AddSpecification(conn, p.SerialNumber, spec.SpecKey, spec.SpecValue);
            }
        }

        public static void Delete(string serial)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM Products WHERE SerialNumber = @s", conn))
                {
                    cmd.Parameters.AddWithValue("@s", serial);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool Exists(string serial)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Products WHERE SerialNumber = @s", conn))
                {
                    cmd.Parameters.AddWithValue("@s", serial);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public static List<ProductSpecification> GetSpecifications(string productSerial)
        {
            var list = new List<ProductSpecification>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ProductSerial, SpecKey, SpecValue FROM ProductSpecifications " +
                    "WHERE ProductSerial = @s ORDER BY SpecKey", conn))
                {
                    cmd.Parameters.AddWithValue("@s", productSerial);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(new ProductSpecification
                            {
                                ProductSerial = r.GetString(0),
                                SpecKey       = r.GetString(1),
                                SpecValue     = r.GetString(2)
                            });
                }
            }
            return list;
        }

        private static void AddSpecification(SqlConnection conn, string serial, string key, string value)
        {
            using (var cmd = new SqlCommand(
                "INSERT INTO ProductSpecifications (ProductSerial, SpecKey, SpecValue) " +
                "VALUES (@s, @k, @v)", conn))
            {
                cmd.Parameters.AddWithValue("@s", serial);
                cmd.Parameters.AddWithValue("@k", key);
                cmd.Parameters.AddWithValue("@v", value);
                cmd.ExecuteNonQuery();
            }
        }

        private static Product MapProduct(SqlDataReader r)
        {
            return new Product
            {
                SerialNumber = r.GetString(0),
                ProductName  = r.GetString(1),
                CategoryName = r.GetString(2),
                Price        = r.GetDecimal(3),
                Quantity     = r.GetInt32(4),
                StockStatus  = r.GetString(5)
            };
        }
    }
}

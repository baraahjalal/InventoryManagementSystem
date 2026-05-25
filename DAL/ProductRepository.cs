using System;
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
                    "SELECT ProductID, ProductName, CategoryID, CategoryName, Price, Quantity, StockStatus " +
                    "FROM vw_ProductStock ORDER BY ProductName", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapProduct(r));
            }
            return list;
        }

        public static List<Product> GetByCategory(int categoryId)
        {
            var list = new List<Product>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ProductID, ProductName, CategoryID, CategoryName, Price, Quantity, StockStatus " +
                    "FROM vw_ProductStock WHERE CategoryID = @cid ORDER BY ProductName", conn))
                {
                    cmd.Parameters.AddWithValue("@cid", categoryId);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapProduct(r));
                }
            }
            return list;
        }

        public static Product GetById(int productId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ProductID, ProductName, CategoryID, CategoryName, Price, Quantity, StockStatus " +
                    "FROM vw_ProductStock WHERE ProductID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
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
                    "INSERT INTO Products (ProductID, ProductName, CategoryID, Price) " +
                    "VALUES (@id, @n, @cid, @pr)", conn))
                {
                    cmd.Parameters.AddWithValue("@id",  p.ProductID);
                    cmd.Parameters.AddWithValue("@n",   p.ProductName);
                    cmd.Parameters.AddWithValue("@cid", p.CategoryID);
                    cmd.Parameters.AddWithValue("@pr",  p.Price);
                    cmd.ExecuteNonQuery();
                }

                foreach (var spec in p.Specifications)
                    AddSpecification(conn, p.ProductID, spec.SpecKey, spec.SpecValue);
            }
        }

        public static void Update(Product p)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE Products SET ProductName = @n, Price = @pr WHERE ProductID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@n",  p.ProductName);
                    cmd.Parameters.AddWithValue("@pr", p.Price);
                    cmd.Parameters.AddWithValue("@id", p.ProductID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int productId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM Products WHERE ProductID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool Exists(int productId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Products WHERE ProductID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public static List<ProductSpecification> GetSpecifications(int productId)
        {
            var list = new List<ProductSpecification>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT SpecID, ProductID, SpecKey, SpecValue FROM ProductSpecifications " +
                    "WHERE ProductID = @id ORDER BY SpecKey", conn))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(new ProductSpecification
                            {
                                SpecID    = r.GetInt32(0),
                                ProductID = r.GetInt32(1),
                                SpecKey   = r.GetString(2),
                                SpecValue = r.GetString(3)
                            });
                }
            }
            return list;
        }

        private static void AddSpecification(SqlConnection conn, int productId, string key, string value)
        {
            using (var cmd = new SqlCommand(
                "INSERT INTO ProductSpecifications (ProductID, SpecKey, SpecValue) " +
                "VALUES (@id, @k, @v)", conn))
            {
                cmd.Parameters.AddWithValue("@id", productId);
                cmd.Parameters.AddWithValue("@k",  key);
                cmd.Parameters.AddWithValue("@v",  value);
                cmd.ExecuteNonQuery();
            }
        }

        private static Product MapProduct(SqlDataReader r)
        {
            return new Product
            {
                ProductID    = r.GetInt32(0),
                ProductName  = r.GetString(1),
                CategoryID   = r.GetInt32(2),
                CategoryName = r.GetString(3),
                Price        = r.GetDecimal(4),
                Quantity     = r.GetInt32(5),
                StockStatus  = r.GetString(6)
            };
        }
    }
}

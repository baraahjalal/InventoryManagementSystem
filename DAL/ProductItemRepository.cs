using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class ProductItemRepository
    {
        public static List<ProductItem> GetAvailable(int productId)
        {
            var list = new List<ProductItem>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ItemID, ProductSerialNumber, IsInStock, DateAdded, DateRemoved, BatchMovementID " +
                    "FROM ProductItems WHERE ProductSerialNumber = @pid AND IsInStock = 1 " +
                    "ORDER BY DateAdded", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", productId);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapItem(r));
                }
            }
            return list;
        }

        // Gets the next ItemID from the DB sequence
        public static int NextItemId(SqlConnection conn)
        {
            using (var cmd = new SqlCommand("SELECT NEXT VALUE FOR seq_ProductItems", conn))
                return (int)cmd.ExecuteScalar();
        }

        public static void AddBatch(List<ProductItem> items)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                foreach (var item in items)
                {
                    int itemId = NextItemId(conn);
                    using (var cmd = new SqlCommand(
                        "INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, BatchMovementID) " +
                        "VALUES (@iid, @pid, 1, @bm)", conn))
                    {
                        cmd.Parameters.AddWithValue("@iid", itemId);
                        cmd.Parameters.AddWithValue("@pid", item.ProductSerialNumber);
                        cmd.Parameters.AddWithValue("@bm",  (object)item.BatchMovementId ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void MarkRemoved(int itemId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE ProductItems SET IsInStock = 0, DateRemoved = GETDATE() " +
                    "WHERE ItemID = @iid", conn))
                {
                    cmd.Parameters.AddWithValue("@iid", itemId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Remove oldest N units (FIFO)
        public static void MarkRemovedBatch(int productId, int quantity)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE TOP (@q) ProductItems " +
                    "SET IsInStock = 0, DateRemoved = GETDATE() " +
                    "WHERE ProductSerialNumber = @pid AND IsInStock = 1", conn))
                {
                    cmd.Parameters.AddWithValue("@q",   quantity);
                    cmd.Parameters.AddWithValue("@pid", productId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int CountInStock(int productId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM ProductItems WHERE ProductSerialNumber = @pid AND IsInStock = 1", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", productId);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static int CountAll(int productId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM ProductItems WHERE ProductSerialNumber = @pid", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", productId);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private static ProductItem MapItem(SqlDataReader r)
        {
            return new ProductItem
            {
                ItemID          = r.GetInt32(0),
                ProductSerialNumber       = r.GetInt32(1),
                IsInStock       = r.GetBoolean(2),
                DateAdded       = r.GetDateTime(3),
                DateRemoved     = r.IsDBNull(4) ? (DateTime?)null : r.GetDateTime(4),
                BatchMovementId = r.IsDBNull(5) ? (int?)null    : r.GetInt32(5)
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class ProductItemRepository
    {
        public static List<ProductItem> GetAvailable(string productSerial)
        {
            var list = new List<ProductItem>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ItemSerialNumber, ProductSerial, IsInStock, DateAdded, DateRemoved, BatchMovementId " +
                    "FROM ProductItems WHERE ProductSerial = @ps AND IsInStock = 1 " +
                    "ORDER BY DateAdded", conn))
                {
                    cmd.Parameters.AddWithValue("@ps", productSerial);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapItem(r));
                }
            }
            return list;
        }

        public static void AddBatch(List<ProductItem> items)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                foreach (var item in items)
                {
                    using (var cmd = new SqlCommand(
                        "INSERT INTO ProductItems (ItemSerialNumber, ProductSerial, IsInStock, BatchMovementId) " +
                        "VALUES (@isn, @ps, 1, @bm)", conn))
                    {
                        cmd.Parameters.AddWithValue("@isn", item.ItemSerialNumber);
                        cmd.Parameters.AddWithValue("@ps",  item.ProductSerial);
                        cmd.Parameters.AddWithValue("@bm",  (object)item.BatchMovementId ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Remove a single specific unit by its item serial number
        public static void MarkRemoved(string itemSerialNumber)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE ProductItems SET IsInStock = 0, DateRemoved = GETDATE() " +
                    "WHERE ItemSerialNumber = @isn", conn))
                {
                    cmd.Parameters.AddWithValue("@isn", itemSerialNumber);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        // Remove the oldest N units of a product (FIFO, for stock-out / return-to-supplier)
        public static void MarkRemovedBatch(string productSerial, int quantity)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE TOP (@q) ProductItems " +
                    "SET IsInStock = 0, DateRemoved = GETDATE() " +
                    "WHERE ProductSerial = @ps AND IsInStock = 1", conn))
                {
                    cmd.Parameters.AddWithValue("@q",  quantity);
                    cmd.Parameters.AddWithValue("@ps", productSerial);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int CountInStock(string productSerial)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM ProductItems WHERE ProductSerial = @ps AND IsInStock = 1", conn))
                {
                    cmd.Parameters.AddWithValue("@ps", productSerial);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static int CountAll(string productSerial)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM ProductItems WHERE ProductSerial = @ps", conn))
                {
                    cmd.Parameters.AddWithValue("@ps", productSerial);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private static ProductItem MapItem(SqlDataReader r)
        {
            return new ProductItem
            {
                ItemSerialNumber = r.GetString(0),
                ProductSerial    = r.GetString(1),
                IsInStock        = r.GetBoolean(2),
                DateAdded        = r.GetDateTime(3),
                DateRemoved      = r.IsDBNull(4) ? (DateTime?)null : r.GetDateTime(4),
                BatchMovementId  = r.IsDBNull(5) ? (int?)null    : r.GetInt32(5)
            };
        }
    }
}

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

        // ItemID = ProductSerialNumber + suffix (2 or 3 digits). NOT global identity/sequence.
        // Example: product 100501 → 10050101, 10050102 … Removed units get their number back on restock.
        public static int GetSuffixDigitCount(int productSerialNumber)
        {
            return productSerialNumber >= 100000 ? 2 : 3;
        }

        public static int ComposeItemId(int productSerialNumber, int suffix)
        {
            int mult = (int)Math.Pow(10, GetSuffixDigitCount(productSerialNumber));
            return productSerialNumber * mult + suffix;
        }

        public static bool TryParseItemId(int itemId, int productSerialNumber, out int suffix)
        {
            int mult = (int)Math.Pow(10, GetSuffixDigitCount(productSerialNumber));
            if (itemId < productSerialNumber * mult + 1 || itemId >= productSerialNumber * mult + mult)
            {
                suffix = 0;
                return false;
            }
            suffix = itemId - productSerialNumber * mult;
            return true;
        }

        /// <summary>Preview serials that Stock In would assign (reactivate removed first, then new suffixes).</summary>
        public static List<int> PreviewNextItemIds(int productSerialNumber, int quantity)
        {
            var result = new List<int>();
            if (quantity <= 0) return result;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var removed = GetRemovedItemIds(conn, null, productSerialNumber, quantity);
                result.AddRange(removed);
                int needNew = quantity - removed.Count;
                if (needNew > 0)
                {
                    // Track already-claimed IDs to avoid showing duplicates in preview
                    var claimed  = new System.Collections.Generic.HashSet<int>(result);
                    int mult     = (int)Math.Pow(10, GetSuffixDigitCount(productSerialNumber));
                    int baseId   = productSerialNumber * mult;
                    int found    = 0;

                    for (int suffix = 1; suffix < mult && found < needNew; suffix++)
                    {
                        int candidateId = baseId + suffix;
                        if (claimed.Contains(candidateId)) continue;
                        using (var cmd = new SqlCommand(
                            "SELECT COUNT(1) FROM ProductItems WHERE ItemID = @id", conn))
                        {
                            cmd.Parameters.AddWithValue("@id", candidateId);
                            if ((int)cmd.ExecuteScalar() == 0)
                            {
                                result.Add(candidateId);
                                claimed.Add(candidateId);
                                found++;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static void AddBatch(List<ProductItem> items)
        {
            if (items == null || items.Count == 0) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in items)
                        {
                            if (TryReactivateOne(conn, tran, item))
                                continue;

                            int itemId = FindLowestUnusedItemId(conn, tran, item.ProductSerialNumber);
                            InsertNew(conn, tran, itemId, item);
                        }
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // Overload used when the caller manages the transaction (no inner transaction created)
        public static void AddBatch(List<ProductItem> items, SqlConnection conn, SqlTransaction tran)
        {
            if (items == null || items.Count == 0) return;
            foreach (var item in items)
            {
                if (TryReactivateOne(conn, tran, item)) continue;
                int itemId = FindLowestUnusedItemId(conn, tran, item.ProductSerialNumber);
                InsertNew(conn, tran, itemId, item);
            }
        }

        private static List<int> GetRemovedItemIds(SqlConnection conn, SqlTransaction tran,
            int productSerialNumber, int maxCount)
        {
            var ids = new List<int>();
            using (var cmd = new SqlCommand(
                "SELECT TOP (@n) ItemID FROM ProductItems " +
                "WHERE ProductSerialNumber = @pid AND IsInStock = 0 " +
                "ORDER BY ItemID", conn, tran))
            {
                cmd.Parameters.AddWithValue("@n", maxCount);
                cmd.Parameters.AddWithValue("@pid", productSerialNumber);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        ids.Add(r.GetInt32(0));
            }
            return ids;
        }

        private static bool TryReactivateOne(SqlConnection conn, SqlTransaction tran, ProductItem item)
        {
            using (var find = new SqlCommand(
                "SELECT TOP 1 ItemID FROM ProductItems " +
                "WHERE ProductSerialNumber = @pid AND IsInStock = 0 " +
                "ORDER BY ItemID", conn, tran))
            {
                find.Parameters.AddWithValue("@pid", item.ProductSerialNumber);
                var found = find.ExecuteScalar();
                if (found == null || found == DBNull.Value)
                    return false;

                int itemId = (int)found;
                using (var upd = new SqlCommand(
                    "UPDATE ProductItems SET IsInStock = 1, DateRemoved = NULL, " +
                    "DateAdded = GETDATE(), BatchMovementID = @bm WHERE ItemID = @iid", conn, tran))
                {
                    upd.Parameters.AddWithValue("@iid", itemId);
                    upd.Parameters.AddWithValue("@bm", (object)item.BatchMovementId ?? DBNull.Value);
                    upd.ExecuteNonQuery();
                }
                return true;
            }
        }

        private static int FindLowestUnusedItemId(SqlConnection conn, SqlTransaction tran, int productSerialNumber)
        {
            int mult = (int)Math.Pow(10, GetSuffixDigitCount(productSerialNumber));
            int baseId = productSerialNumber * mult;

            using (var cmd = new SqlCommand(
                "SELECT TOP 1 n.n FROM (" +
                "  SELECT TOP (@max) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n " +
                "  FROM sys.all_objects) n " +
                "WHERE NOT EXISTS (SELECT 1 FROM ProductItems WHERE ItemID = @base + n.n) " +
                "ORDER BY n.n", conn, tran))
            {
                cmd.Parameters.AddWithValue("@max", mult - 1);
                cmd.Parameters.AddWithValue("@base", baseId);
                var result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    throw new InvalidOperationException(
                        $"No available ItemID suffix for product {productSerialNumber} (max {mult - 1} units).");
                return baseId + Convert.ToInt32(result);
            }
        }

        private static void InsertNew(SqlConnection conn, SqlTransaction tran, int itemId, ProductItem item)
        {
            using (var cmd = new SqlCommand(
                "INSERT INTO ProductItems (ItemID, ProductSerialNumber, IsInStock, BatchMovementID) " +
                "VALUES (@iid, @pid, 1, @bm)", conn, tran))
            {
                cmd.Parameters.AddWithValue("@iid", itemId);
                cmd.Parameters.AddWithValue("@pid", item.ProductSerialNumber);
                cmd.Parameters.AddWithValue("@bm", (object)item.BatchMovementId ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public static void MarkRemoved(int itemId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MarkRemoved(itemId, conn, null);
            }
        }

        public static void MarkRemoved(int itemId, SqlConnection conn, SqlTransaction tran)
        {
            using (var cmd = new SqlCommand(
                "UPDATE ProductItems SET IsInStock = 0, DateRemoved = GETDATE() " +
                "WHERE ItemID = @iid", conn, tran))
            {
                cmd.Parameters.AddWithValue("@iid", itemId);
                cmd.ExecuteNonQuery();
            }
        }

        // Remove oldest N units (FIFO)
        public static void MarkRemovedBatch(int productId, int quantity)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MarkRemovedBatch(productId, quantity, conn, null);
            }
        }

        public static void MarkRemovedBatch(int productId, int quantity, SqlConnection conn, SqlTransaction tran)
        {
            using (var cmd = new SqlCommand(
                "UPDATE TOP (@q) ProductItems " +
                "SET IsInStock = 0, DateRemoved = GETDATE() " +
                "WHERE ProductSerialNumber = @pid AND IsInStock = 1", conn, tran))
            {
                cmd.Parameters.AddWithValue("@q",   quantity);
                cmd.Parameters.AddWithValue("@pid", productId);
                cmd.ExecuteNonQuery();
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

        public static DateTime? GetOldestRemovedDate()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT MIN(DateRemoved) FROM ProductItems WHERE IsInStock=0 AND DateRemoved IS NOT NULL", conn))
                {
                    var result = cmd.ExecuteScalar();
                    return (result == null || result == DBNull.Value) ? (DateTime?)null : (DateTime)result;
                }
            }
        }

        public static bool HasRemovedItemsOlderThan(int years)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM ProductItems " +
                    "WHERE IsInStock = 0 AND DateRemoved IS NOT NULL " +
                    "AND DateRemoved < DATEADD(YEAR, -@y, GETDATE())", conn))
                {
                    cmd.Parameters.AddWithValue("@y", years);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public static int CountRemovedOlderThan(int years)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM ProductItems " +
                    "WHERE IsInStock = 0 AND DateRemoved IS NOT NULL " +
                    "AND DateRemoved < DATEADD(YEAR, -@y, GETDATE())", conn))
                {
                    cmd.Parameters.AddWithValue("@y", years);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static bool AreItemsFromSupplier(List<int> itemIds, int supplierTaxNum)
        {
            if (itemIds == null || itemIds.Count == 0) return true;
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string ids = string.Join(",", itemIds);
                using (var cmd = new SqlCommand(
                    $"SELECT COUNT(1) FROM ProductItems p LEFT JOIN StockMovements m ON p.BatchMovementID = m.MovementID " +
                    $"WHERE p.ItemID IN ({ids}) AND m.SupplierTaxNumber IS NOT NULL AND m.SupplierTaxNumber != @tax", conn))
                {
                    cmd.Parameters.AddWithValue("@tax", supplierTaxNum);
                    return (int)cmd.ExecuteScalar() == 0;
                }
            }
        }

        public static bool AreOldestItemsFromSupplier(int productId, int quantity, int supplierTaxNum)
        {
            if (quantity <= 0) return true;
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM (" +
                    "  SELECT TOP (@q) p.BatchMovementID FROM ProductItems p " +
                    "  WHERE p.ProductSerialNumber = @pid AND p.IsInStock = 1 ORDER BY p.DateAdded" +
                    ") AS topItems " +
                    "LEFT JOIN StockMovements m ON topItems.BatchMovementID = m.MovementID " +
                    "WHERE m.SupplierTaxNumber IS NOT NULL AND m.SupplierTaxNumber != @tax", conn))
                {
                    cmd.Parameters.AddWithValue("@q", quantity);
                    cmd.Parameters.AddWithValue("@pid", productId);
                    cmd.Parameters.AddWithValue("@tax", supplierTaxNum);
                    return (int)cmd.ExecuteScalar() == 0;
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

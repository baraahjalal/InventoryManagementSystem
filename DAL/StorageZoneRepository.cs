using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class StorageZoneRepository
    {
        public static List<StorageZone> GetAll()
        {
            var list = new List<StorageZone>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ZoneName, CategoryName FROM StorageZones ORDER BY ZoneName", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new StorageZone
                        {
                            ZoneName     = r.GetString(0),
                            CategoryName = r.GetString(1)
                        });
            }
            return list;
        }

        public static List<StorageZone> GetByCategory(string categoryName)
        {
            var list = new List<StorageZone>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ZoneName, CategoryName FROM StorageZones " +
                    "WHERE CategoryName = @c ORDER BY ZoneName", conn))
                {
                    cmd.Parameters.AddWithValue("@c", categoryName);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(new StorageZone
                            {
                                ZoneName     = r.GetString(0),
                                CategoryName = r.GetString(1)
                            });
                }
            }
            return list;
        }

        public static void Add(StorageZone z)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "INSERT INTO StorageZones (ZoneName, CategoryName) VALUES (@z, @c)", conn))
                {
                    cmd.Parameters.AddWithValue("@z", z.ZoneName);
                    cmd.Parameters.AddWithValue("@c", z.CategoryName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string zoneName)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM StorageZones WHERE ZoneName = @z", conn))
                {
                    cmd.Parameters.AddWithValue("@z", zoneName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

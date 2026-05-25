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
                    "SELECT ZoneID, ZoneName, CategoryID FROM StorageZones ORDER BY ZoneName", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapZone(r));
            }
            return list;
        }

        public static List<StorageZone> GetByCategory(int categoryId)
        {
            var list = new List<StorageZone>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ZoneID, ZoneName, CategoryID FROM StorageZones " +
                    "WHERE CategoryID = @cid ORDER BY ZoneName", conn))
                {
                    cmd.Parameters.AddWithValue("@cid", categoryId);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(MapZone(r));
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
                    "INSERT INTO StorageZones (ZoneName, CategoryID) VALUES (@z, @cid)", conn))
                {
                    cmd.Parameters.AddWithValue("@z",   z.ZoneName);
                    cmd.Parameters.AddWithValue("@cid", z.CategoryID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int zoneId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM StorageZones WHERE ZoneID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", zoneId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static StorageZone MapZone(SqlDataReader r)
        {
            return new StorageZone
            {
                ZoneID     = r.GetInt32(0),
                ZoneName   = r.GetString(1),
                CategoryID = r.GetInt32(2)
            };
        }
    }
}

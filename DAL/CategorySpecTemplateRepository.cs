using System.Collections.Generic;
using System.Data.SqlClient;

namespace InventoryManagementSystem.DAL
{
    public static class CategorySpecTemplateRepository
    {
        public static List<string> GetByCategory(int categoryId)
        {
            var list = new List<string>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT SpecKey FROM CategorySpecTemplates " +
                    "WHERE CategoryID = @cid ORDER BY SpecKey", conn))
                {
                    cmd.Parameters.AddWithValue("@cid", categoryId);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(r.GetString(0));
                }
            }
            return list;
        }

        public static void Add(int categoryId, string specKey)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "INSERT INTO CategorySpecTemplates (CategoryID, SpecKey) " +
                    "VALUES (@cid, @sk)", conn))
                {
                    cmd.Parameters.AddWithValue("@cid", categoryId);
                    cmd.Parameters.AddWithValue("@sk",  specKey);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteByCategory(int categoryId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM CategorySpecTemplates WHERE CategoryID = @cid", conn))
                {
                    cmd.Parameters.AddWithValue("@cid", categoryId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

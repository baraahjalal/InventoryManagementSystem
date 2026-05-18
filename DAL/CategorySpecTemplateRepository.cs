using System.Collections.Generic;
using System.Data.SqlClient;

namespace InventoryManagementSystem.DAL
{
    public static class CategorySpecTemplateRepository
    {
        public static List<string> GetByCategory(string categoryName)
        {
            var list = new List<string>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT SpecKey FROM CategorySpecTemplates " +
                    "WHERE CategoryName = @cn ORDER BY SpecKey", conn))
                {
                    cmd.Parameters.AddWithValue("@cn", categoryName);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(r.GetString(0));
                }
            }
            return list;
        }

        public static void Add(string categoryName, string specKey)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) " +
                    "VALUES (@cn, @sk)", conn))
                {
                    cmd.Parameters.AddWithValue("@cn", categoryName);
                    cmd.Parameters.AddWithValue("@sk", specKey);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteByCategory(string categoryName)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM CategorySpecTemplates WHERE CategoryName = @cn", conn))
                {
                    cmd.Parameters.AddWithValue("@cn", categoryName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

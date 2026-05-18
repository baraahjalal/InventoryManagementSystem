using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class CategoryRepository
    {
        public static List<Category> GetAll()
        {
            var list = new List<Category>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT CategoryName FROM Categories ORDER BY CategoryName", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new Category { CategoryName = r.GetString(0) });
            }
            return list;
        }

        public static void Add(string categoryName)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "INSERT INTO Categories (CategoryName) VALUES (@n)", conn))
                {
                    cmd.Parameters.AddWithValue("@n", categoryName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string categoryName)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM Categories WHERE CategoryName = @n", conn))
                {
                    cmd.Parameters.AddWithValue("@n", categoryName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool Exists(string categoryName)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Categories WHERE CategoryName = @n", conn))
                {
                    cmd.Parameters.AddWithValue("@n", categoryName);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }
    }
}

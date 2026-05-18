using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class UserRepository
    {
        public static User Authenticate(string username, string password)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT Username, Password, Role, IsAdmin, ProfilePhoto " +
                    "FROM Users WHERE Username = @u AND Password = @p", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) return null;
                        return MapUser(r);
                    }
                }
            }
        }

        public static List<User> GetAll()
        {
            var list = new List<User>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT Username, Password, Role, IsAdmin, ProfilePhoto " +
                    "FROM Users ORDER BY Username", conn))
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(MapUser(r));
            }
            return list;
        }

        public static void Add(User u)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "INSERT INTO Users (Username, Password, Role, IsAdmin, ProfilePhoto) " +
                    "VALUES (@u, @p, @r, @a, @ph)", conn))
                {
                    cmd.Parameters.AddWithValue("@u",  u.Username);
                    cmd.Parameters.AddWithValue("@p",  u.Password);
                    cmd.Parameters.AddWithValue("@r",  u.Role);
                    cmd.Parameters.AddWithValue("@a",  u.IsAdmin);
                    cmd.Parameters.AddWithValue("@ph", (object)u.ProfilePhoto ?? System.DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update(User u)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE Users SET Password = @p, Role = @r, IsAdmin = @a, ProfilePhoto = @ph " +
                    "WHERE Username = @u", conn))
                {
                    cmd.Parameters.AddWithValue("@p",  u.Password);
                    cmd.Parameters.AddWithValue("@r",  u.Role);
                    cmd.Parameters.AddWithValue("@a",  u.IsAdmin);
                    cmd.Parameters.AddWithValue("@ph", (object)u.ProfilePhoto ?? System.DBNull.Value);
                    cmd.Parameters.AddWithValue("@u",  u.Username);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string username)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM Users WHERE Username = @u", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool Exists(string username)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Users WHERE Username = @u", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        private static User MapUser(SqlDataReader r)
        {
            return new User
            {
                Username     = r.GetString(0),
                Password     = r.GetString(1),
                Role         = r.GetString(2),
                IsAdmin      = r.GetBoolean(3),
                ProfilePhoto = r.IsDBNull(4) ? null : (byte[])r[4]
            };
        }
    }
}

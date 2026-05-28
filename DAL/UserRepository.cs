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
                    "SELECT EmployeeID, Username, Password, Role, IsAdmin, ProfilePhoto " +
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
                    "SELECT EmployeeID, Username, Password, Role, IsAdmin, ProfilePhoto " +
                    "FROM Users ORDER BY EmployeeID", conn))
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
                    "INSERT INTO Users (EmployeeID, Username, Password, Role, IsAdmin, ProfilePhoto) " +
                    "VALUES (@id, @u, @p, @r, @a, @ph)", conn))
                {
                    cmd.Parameters.AddWithValue("@id", u.EmployeeID);
                    cmd.Parameters.AddWithValue("@u",  u.Username);
                    cmd.Parameters.AddWithValue("@p",  u.Password);
                    cmd.Parameters.AddWithValue("@r",  u.Role);
                    cmd.Parameters.AddWithValue("@a",  u.IsAdmin);
                    var addPhoto = cmd.Parameters.Add("@ph", System.Data.SqlDbType.VarBinary, -1);
                    addPhoto.Value = (object)u.ProfilePhoto ?? System.DBNull.Value;
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
                    "UPDATE Users SET Username = @u, Password = @p, Role = @r, IsAdmin = @a, ProfilePhoto = @ph " +
                    "WHERE EmployeeID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@u",  u.Username);
                    cmd.Parameters.AddWithValue("@p",  u.Password);
                    cmd.Parameters.AddWithValue("@r",  u.Role);
                    cmd.Parameters.AddWithValue("@a",  u.IsAdmin);
                    var updPhoto = cmd.Parameters.Add("@ph", System.Data.SqlDbType.VarBinary, -1);
                    updPhoto.Value = (object)u.ProfilePhoto ?? System.DBNull.Value;
                    cmd.Parameters.AddWithValue("@id", u.EmployeeID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int employeeId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM Users WHERE EmployeeID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", employeeId);
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

        public static bool EmployeeIdExists(int employeeId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Users WHERE EmployeeID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", employeeId);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        private static User MapUser(SqlDataReader r)
        {
            return new User
            {
                EmployeeID   = r.GetInt32(0),
                Username     = r.GetString(1),
                Password     = r.GetString(2),
                Role         = r.GetString(3),
                IsAdmin      = r.GetBoolean(4),
                ProfilePhoto = r.IsDBNull(5) ? null : (byte[])r[5]
            };
        }
    }
}

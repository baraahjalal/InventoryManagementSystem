using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class DatabaseHelper
    {
        private const string ConnStr =
            @"Server=(localdb)\MSSQLLocalDB;Database=InventoryDB;Trusted_Connection=True;";

        public static SqlConnection GetConnection() => new SqlConnection(ConnStr);

        public static User CurrentUser { get; set; }
    }
}

using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class DatabaseHelper
    {
        private const string ConnStr =
           "Data Source=BARAAH-PC;Initial Catalog=InventoryDB;Integrated Security=true;";

        public static SqlConnection GetConnection() => new SqlConnection(ConnStr);

        public static User CurrentUser { get; set; }
    }
}

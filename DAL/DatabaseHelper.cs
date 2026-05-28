using System.Data.SqlClient;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public static class DatabaseHelper
    {
        private const string ConnStr =
           "Data Source=.;Initial Catalog=InventoryDBv3;Integrated Security=true;";

        public static string DataSource     => ".";
        public static string InitialCatalog => "InventoryDBv3";

        public static SqlConnection GetConnection() => new SqlConnection(ConnStr);

        public static User CurrentUser { get; set; }
    }
}

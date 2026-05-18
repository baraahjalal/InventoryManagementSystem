namespace InventoryManagementSystem.Models
{
    public class User
    {
        public string Username     { get; set; }
        public string Password     { get; set; }
        public string Role         { get; set; }
        public bool   IsAdmin      { get; set; }
        public byte[] ProfilePhoto { get; set; }
    }
}

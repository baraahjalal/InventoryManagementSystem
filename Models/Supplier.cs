namespace InventoryManagementSystem.Models
{
    public class Supplier
    {
        public int    SupplierTaxNumber { get; set; }
        public string SupplierName      { get; set; }
        public string Phone             { get; set; }
        public string Email             { get; set; }
        public bool   IsActive          { get; set; }
    }
}

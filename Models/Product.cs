using System.Collections.Generic;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public string SerialNumber  { get; set; }
        public string ProductName   { get; set; }
        public string CategoryName  { get; set; }
        public decimal Price        { get; set; }
        public int    Quantity      { get; set; }
        public string StockStatus   { get; set; }
        public List<ProductSpecification> Specifications { get; set; } = new List<ProductSpecification>();
    }

    public class ProductSpecification
    {
        public string ProductSerial { get; set; }
        public string SpecKey       { get; set; }
        public string SpecValue     { get; set; }
    }
}

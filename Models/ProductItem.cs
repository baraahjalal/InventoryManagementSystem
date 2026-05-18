using System;

namespace InventoryManagementSystem.Models
{
    public class ProductItem
    {
        public string    ItemSerialNumber { get; set; }
        public string    ProductSerial    { get; set; }
        public bool      IsInStock        { get; set; }
        public DateTime  DateAdded        { get; set; }
        public DateTime? DateRemoved      { get; set; }
        public int?      BatchMovementId  { get; set; }
    }
}

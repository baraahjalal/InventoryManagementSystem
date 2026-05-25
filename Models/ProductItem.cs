using System;

namespace InventoryManagementSystem.Models
{
    public class ProductItem
    {
        public int       ItemID          { get; set; }
        public int       ProductSerialNumber { get; set; }
        public bool      IsInStock       { get; set; }
        public DateTime  DateAdded       { get; set; }
        public DateTime? DateRemoved     { get; set; }
        public int?      BatchMovementId { get; set; }
    }
}

using System;

namespace InventoryManagementSystem.Models
{
    public class StockMovement
    {
        public int      MovementId        { get; set; }
        public int      ProductSerialNumber { get; set; }
        public string   MovementType      { get; set; }
        public int      QuantityChanged   { get; set; }
        public DateTime MovementDate      { get; set; }
        public int?     EmployeeID        { get; set; }
        public string   Notes             { get; set; }
        public int?     WarrantyMonths    { get; set; }
        public int?     SupplierTaxNumber { get; set; }
    }
}

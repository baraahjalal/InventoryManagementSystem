using System;

namespace InventoryManagementSystem.Models
{
    public class AuditLogEntry
    {
        public int      LogId        { get; set; }
        public DateTime LogTimestamp { get; set; }
        public string   ActionType   { get; set; }
        public string   Description  { get; set; }
        public string   Username     { get; set; }
    }
}

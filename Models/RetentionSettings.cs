using System;

namespace InventoryManagementSystem.Models
{
    public class RetentionSettings
    {
        public int       RetentionYears     { get; set; } = 3;
        public int       WarnIntervalMonths { get; set; } = 3;
        public bool      IsEnabled          { get; set; } = true;
        public DateTime? LastWarnDate       { get; set; }
    }
}

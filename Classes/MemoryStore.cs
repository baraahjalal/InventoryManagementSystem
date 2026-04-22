using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSystem
{
    #region Models

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // RBAC Permissions
        public bool IsAdmin { get; set; }
        public bool CanInsert { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanManageUsers { get; set; }
        public bool CanPrint { get; set; }
        public bool CanManageSettings { get; set; }
    }

    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string SerialNumber { get; set; }
        public int? SupplierId { get; set; } // Nullable foreign key

        // Computed Read-Only property
        public string Status
        {
            get
            {
                if (Quantity <= 0) return "Out of Stock";
                if (Quantity <= 10) return "Low Stock";
                return "In Stock";
            }
        }
    }

    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }  // Foreign Key
        public string Type { get; set; }    // "STOCK IN", "STOCK OUT", "RESTOCK"
        public int QuantityChanged { get; set; }
        public DateTime Timestamp { get; set; }
        public int? UserId { get; set; }    // Foreign Key
        public string Notes { get; set; }
    }

    public class SystemLog
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
    }

    #endregion

    public static class MemoryStore
    {
        // Session State
        public static User CurrentUser { get; set; }

        // Data Tables (Lists)
        public static List<User> Users { get; private set; } = new List<User>();
        public static List<Supplier> Suppliers { get; private set; } = new List<Supplier>();
        public static List<Product> Products { get; private set; } = new List<Product>();
        public static List<StockMovement> StockMovements { get; private set; } = new List<StockMovement>();
        public static List<SystemLog> AuditLogs { get; private set; } = new List<SystemLog>();

        // Static constructor initializes dummy data
        static MemoryStore()
        {
            SeedData();
        }

        #region Helper Methods (Acts like an ORM / Database context)

        /// <summary>
        /// Logs an action to the AuditLog system automatically
        /// </summary>
        public static void LogAction(string actionType, string description)
        {
            int newId = AuditLogs.Count > 0 ? AuditLogs.Max(a => a.Id) + 1 : 1;
            AuditLogs.Add(new SystemLog
            {
                Id = newId,
                Timestamp = DateTime.Now,
                ActionType = actionType,
                Description = description,
                Username = CurrentUser != null ? CurrentUser.Username : "System"
            });
        }

        /// <summary>
        /// Records a stock movement and updates the product's quantity safely
        /// </summary>
        public static bool PerformStockMovement(int productId, int quantityChange, string movementType, string notes = "")
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) return false;

            // Prevent negative stock
            if (product.Quantity + quantityChange < 0) return false;

            // Update Product
            product.Quantity += quantityChange;

            // Generate Movement Record
            int newId = StockMovements.Count > 0 ? StockMovements.Max(m => m.Id) + 1 : 1;
            StockMovements.Add(new StockMovement
            {
                Id = newId,
                ProductId = productId,
                QuantityChanged = quantityChange,
                Type = movementType,
                Timestamp = DateTime.Now,
                UserId = CurrentUser?.Id,
                Notes = notes
            });

            LogAction(movementType, $"Product '{product.Name}' {(quantityChange > 0 ? "increased" : "decreased")} by {Math.Abs(quantityChange)}.");
            return true;
        }

        #endregion

        #region Seed Initial Data
        private static void SeedData()
        {
            // Seed Users
            Users.AddRange(new[]
            {
                new User { Id = 1, Username = "admin.super", Password = "123", Role = "System Administrator", IsAdmin = true, CanInsert = true, CanUpdate = true, CanManageUsers = true, CanPrint = true, CanManageSettings = true },
                new User { Id = 2, Username = "john.doe", Password = "123", Role = "Inventory Manager", IsAdmin = false, CanInsert = true, CanUpdate = true, CanManageUsers = false, CanPrint = true, CanManageSettings = false },
                new User { Id = 3, Username = "sarah.smith", Password = "123", Role = "Sales Representative", IsAdmin = false, CanInsert = false, CanUpdate = false, CanManageUsers = false, CanPrint = true, CanManageSettings = false }
            });

            // Seed Suppliers
            Suppliers.AddRange(new[]
            {
                new Supplier { Id = 1, Name = "TechSource Inc.", ContactPerson = "Arthur Morgan", Phone = "555-0101", Email = "arthur@techsource.com", IsActive = true },
                new Supplier { Id = 2, Name = "Global Electronics", ContactPerson = "Harvey Specter", Phone = "555-0202", Email = "harvey@globalelec.com", IsActive = true }
            });

            // Seed Products
            Products.AddRange(new[]
            {
                new Product { Id = 101, Name = "MacBook Pro 14\" M3", Category = "Laptops", Quantity = 24, Price = 1999m, SerialNumber = "APP-MBP-2023", SupplierId = 1 },
                new Product { Id = 102, Name = "iPhone 15 Pro Max", Category = "Phones", Quantity = 8, Price = 1199m, SerialNumber = "APP-IPH-2023", SupplierId = 2 },
                new Product { Id = 104, Name = "HP LaserJet Pro", Category = "Printers", Quantity = 0, Price = 450m, SerialNumber = "PRN-HP-2024", SupplierId = 1 }
            });

            // Seed initial movements
            StockMovements.AddRange(new[]
            {
                new StockMovement { Id = 1, ProductId = 101, Type = "STOCK IN", QuantityChanged = 24, Timestamp = DateTime.Now.AddDays(-2), UserId = 2 },
                new StockMovement { Id = 2, ProductId = 102, Type = "STOCK IN", QuantityChanged = 12, Timestamp = DateTime.Now.AddDays(-3), UserId = 2 },
                new StockMovement { Id = 3, ProductId = 102, Type = "STOCK OUT", QuantityChanged = -4, Timestamp = DateTime.Now.AddDays(-1), UserId = 3 }
            });

            // Seed Initial Logs
            AuditLogs.Add(new SystemLog { Id = 1, Timestamp = DateTime.Now.AddDays(-5), ActionType = "SYSTEM START", Description = "System initialized.", Username = "System" });
        }
        #endregion
    }
}
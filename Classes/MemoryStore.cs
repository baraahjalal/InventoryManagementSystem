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
        public System.Drawing.Image ProfilePicture { get; set; }

        public bool IsAdmin { get; set; } // Super User override

         }

    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Category { get; set; }
        public List<string> SuppliedProducts { get; set; } = new List<string>();
    }

    public class CategoryTemplate
    {
        public string CategoryName { get; set; }
        // Key: Filter Name (e.g., "RAM"), Value: Available Options (e.g., ["8GB", "16GB", "32GB"])
        public Dictionary<string, List<string>> AvailableFilters { get; set; } = new Dictionary<string, List<string>>();
    }

    /// <summary>
    /// Represents a single physical item (unit) of a product, each with its own unique serial number.
    /// </summary>
    public class ProductItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }       // Foreign Key to Product
        public string ItemSerialNumber { get; set; } // e.g., "PRD-001-01"
        public bool IsInStock { get; set; }       // true = available, false = dispatched/sold
        public DateTime DateAdded { get; set; }
        public DateTime? DateRemoved { get; set; } // null if still in stock
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string SerialNumber { get; set; } // Product-level serial (Primary Serial)
        public int? SupplierId { get; set; } // Nullable foreign key

        // Dynamic specifications to hold varied data (Key = Processor, Value = Apple M3)
        public Dictionary<string, string> Specifications { get; set; } = new Dictionary<string, string>();

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
        public List<string> AffectedItemSerials { get; set; } = new List<string>(); // Track which items were involved
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
        public static List<ProductItem> ProductItems { get; private set; } = new List<ProductItem>();
        public static List<StockMovement> StockMovements { get; private set; } = new List<StockMovement>();
        public static List<SystemLog> AuditLogs { get; private set; } = new List<SystemLog>();

        // Store Category Templates
        public static List<CategoryTemplate> CategoryTemplates { get; private set; } = new List<CategoryTemplate>();

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
        /// Generates item-level serial numbers derived from a product's primary serial.
        /// Format: {ProductSerial}-{SequenceNumber:D2}
        /// Example: PRD-001 → PRD-001-01, PRD-001-02, PRD-001-03
        /// </summary>
        public static List<string> GenerateItemSerials(string productSerial, int startIndex, int count)
        {
            var serials = new List<string>();
            for (int i = 0; i < count; i++)
            {
                serials.Add($"{productSerial}-{(startIndex + i):D2}");
            }
            return serials;
        }

        /// <summary>
        /// Gets the next available item index for a given product serial (to avoid duplicates).
        /// </summary>
        public static int GetNextItemIndex(int productId)
        {
            var existingItems = ProductItems.Where(pi => pi.ProductId == productId).ToList();
            if (existingItems.Count == 0) return 1;

            // Parse the highest index from existing item serials
            int maxIndex = 0;
            foreach (var item in existingItems)
            {
                string serial = item.ItemSerialNumber;
                int lastDash = serial.LastIndexOf('-');
                if (lastDash >= 0 && int.TryParse(serial.Substring(lastDash + 1), out int idx))
                {
                    if (idx > maxIndex) maxIndex = idx;
                }
            }
            return maxIndex + 1;
        }

        /// <summary>
        /// Gets all in-stock item serials for a given product.
        /// </summary>
        public static List<ProductItem> GetAvailableItems(int productId)
        {
            return ProductItems.Where(pi => pi.ProductId == productId && pi.IsInStock).ToList();
        }

        /// <summary>
        /// Records a stock movement and updates the product's quantity safely.
        /// For STOCK IN: automatically generates item-level serial numbers.
        /// For STOCK OUT: marks specified items as dispatched.
        /// </summary>
        public static bool PerformStockMovement(int productId, int quantityChange, string movementType, string notes = "", List<string> selectedItemSerials = null)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) return false;

            // Prevent negative stock
            if (product.Quantity + quantityChange < 0) return false;

            var affectedSerials = new List<string>();

            if (quantityChange > 0) // STOCK IN
            {
                // Generate item-level serial numbers for the incoming quantity
                int startIndex = GetNextItemIndex(productId);
                var newSerials = GenerateItemSerials(product.SerialNumber, startIndex, quantityChange);

                int newItemId = ProductItems.Count > 0 ? ProductItems.Max(pi => pi.Id) + 1 : 1;
                foreach (var serial in newSerials)
                {
                    ProductItems.Add(new ProductItem
                    {
                        Id = newItemId++,
                        ProductId = productId,
                        ItemSerialNumber = serial,
                        IsInStock = true,
                        DateAdded = DateTime.Now
                    });
                }

                affectedSerials = newSerials;
            }
            else if (quantityChange < 0) // STOCK OUT
            {
                int outQty = Math.Abs(quantityChange);

                if (selectedItemSerials != null && selectedItemSerials.Count > 0)
                {
                    // Dispatch specific selected items
                    foreach (var serial in selectedItemSerials)
                    {
                        var item = ProductItems.FirstOrDefault(pi =>
                            pi.ProductId == productId &&
                            pi.ItemSerialNumber == serial &&
                            pi.IsInStock);
                        if (item != null)
                        {
                            item.IsInStock = false;
                            item.DateRemoved = DateTime.Now;
                            affectedSerials.Add(serial);
                        }
                    }
                }
                else
                {
                    // Auto-select oldest available items (FIFO)
                    var availableItems = ProductItems
                        .Where(pi => pi.ProductId == productId && pi.IsInStock)
                        .OrderBy(pi => pi.DateAdded)
                        .Take(outQty)
                        .ToList();

                    foreach (var item in availableItems)
                    {
                        item.IsInStock = false;
                        item.DateRemoved = DateTime.Now;
                        affectedSerials.Add(item.ItemSerialNumber);
                    }
                }
            }

            // Update Product quantity
            product.Quantity += quantityChange;

            // Generate Movement Record
            int newMovementId = StockMovements.Count > 0 ? StockMovements.Max(m => m.Id) + 1 : 1;
            StockMovements.Add(new StockMovement
            {
                Id = newMovementId,
                ProductId = productId,
                QuantityChanged = quantityChange,
                Type = movementType,
                Timestamp = DateTime.Now,
                UserId = CurrentUser?.Id,
                Notes = notes,
                AffectedItemSerials = affectedSerials
            });

            LogAction(movementType, $"Product '{product.Name}' {(quantityChange > 0 ? "increased" : "decreased")} by {Math.Abs(quantityChange)}. Items: [{string.Join(", ", affectedSerials.Take(5))}{(affectedSerials.Count > 5 ? "..." : "")}]");
            return true;
        }

        #endregion

        #region Seed Initial Data
        private static void SeedData()
        {
            // Seed Templates
            CategoryTemplates.AddRange(new[]
            {
                new CategoryTemplate {
                    CategoryName = "Laptops",
                    AvailableFilters = new Dictionary<string, List<string>> {
                        { "Processor", new List<string> { "Apple M3", "Apple M2", "Intel Core i7", "Intel Core i9", "AMD Ryzen 7", "AMD Ryzen 9" } },
                        { "RAM", new List<string> { "8GB", "16GB", "32GB", "64GB" } }
                    }
                },
                new CategoryTemplate {
                    CategoryName = "Phones",
                    AvailableFilters = new Dictionary<string, List<string>> {
                        { "Storage", new List<string> { "128GB", "256GB", "512GB", "1TB" } },
                        { "Color", new List<string> { "Space Black", "Titanium", "Silver", "Phantom Black", "Cream" } }
                    }
                },
                new CategoryTemplate {
                    CategoryName = "Printers",
                    AvailableFilters = new Dictionary<string, List<string>> {
                        { "Type", new List<string> { "Laser", "Inkjet", "Thermal" } },
                        { "Color Support", new List<string> { "Monochrome", "Color" } }
                    }
                },
                new CategoryTemplate {
                    CategoryName = "Monitors",
                    AvailableFilters = new Dictionary<string, List<string>> {
                        { "Resolution", new List<string> { "1080p", "1440p", "4K", "8K" } },
                        { "Panel Type", new List<string> { "IPS", "VA", "OLED", "TN" } }
                    }
                },
                new CategoryTemplate {
                    CategoryName = "Accessories",
                    AvailableFilters = new Dictionary<string, List<string>> {
                        { "Type", new List<string> { "Mouse", "Keyboard", "Headset", "Webcam" } },
                        { "Connection", new List<string> { "Wireless", "Wired", "Bluetooth" } }
                    }
                }
            });

            // Simplified RBAC: only System Administrator and Employee
            Users.AddRange(new[]
            {
                new User { Id = 1, Username = "admin.super", Password = "123", Role = "System Administrator", IsAdmin = true },
                new User { Id = 2, Username = "ahmed.clerk", Password = "123", Role = "Employee", IsAdmin = false },
                new User { Id = 3, Username = "sara.reports", Password = "123", Role = "Employee", IsAdmin = false },
                new User { Id = 4, Username = "omar.proc", Password = "123", Role = "Employee", IsAdmin = false },
                new User { Id = 5, Username = "fatima.audit", Password = "123", Role = "Employee", IsAdmin = false },
                new User { Id = 6, Username = "tariq.sales", Password = "123", Role = "Employee", IsAdmin = false }
            });

            // Seed Suppliers (Address removed)
            Suppliers.AddRange(new[]
            {
                new Supplier { Id = 1, Name = "TechSource Inc.", ContactPerson = "Arthur Morgan", Phone = "555-0101", Email = "arthur@techsource.com", IsActive = true, Category = "Distributor", SuppliedProducts = new List<string> { "Laptops", "Accessories" } },
                new Supplier { Id = 2, Name = "Global Electronics", ContactPerson = "Harvey Specter", Phone = "555-0202", Email = "harvey@globalelec.com", IsActive = true, Category = "Manufacturer", SuppliedProducts = new List<string> { "Phones", "Monitors" } },
            });

            // Seed Products
            Products.AddRange(new[]
            {
                new Product { Id = 101, Name = "MacBook Pro 14\" M3", Category = "Laptops", Quantity = 24, Price = 1999m, SerialNumber = "APP-MBP-2023", SupplierId = 1, Specifications = new Dictionary<string, string> { { "Processor", "Apple M3" }, { "RAM", "16GB" } } },
                new Product { Id = 102, Name = "iPhone 15 Pro Max", Category = "Phones", Quantity = 8, Price = 1199m, SerialNumber = "APP-IPH-2023", SupplierId = 2, Specifications = new Dictionary<string, string> { { "Storage", "256GB" }, { "Color", "Titanium" } } },
                new Product { Id = 103, Name = "Samsung Galaxy S24 Ultra", Category = "Phones", Quantity = 15, Price = 1299m, SerialNumber = "SAM-S24U-001", SupplierId = 5, Specifications = new Dictionary<string, string> { { "Storage", "512GB" }, { "Color", "Phantom Black" } } },
                new Product { Id = 104, Name = "HP LaserJet Pro", Category = "Printers", Quantity = 0, Price = 450m, SerialNumber = "PRN-HP-2024", SupplierId = 1, Specifications = new Dictionary<string, string> { { "Type", "Laser" }, { "Color Support", "Monochrome" } } },
                new Product { Id = 105, Name = "Dell XPS 15", Category = "Laptops", Quantity = 12, Price = 1750m, SerialNumber = "DEL-XPS-1590", SupplierId = 5, Specifications = new Dictionary<string, string> { { "Processor", "Intel Core i7" }, { "RAM", "32GB" } } },
                new Product { Id = 106, Name = "Lenovo ThinkPad X1 Carbon", Category = "Laptops", Quantity = 5, Price = 1600m, SerialNumber = "LEN-TPX1-G10", SupplierId = 1, Specifications = new Dictionary<string, string> { { "Processor", "Intel Core i7" }, { "RAM", "16GB" } } },
                new Product { Id = 107, Name = "LG UltraGear 27\"", Category = "Monitors", Quantity = 30, Price = 350m, SerialNumber = "MON-LG-27GN", SupplierId = 3, Specifications = new Dictionary<string, string> { { "Resolution", "1440p" }, { "Panel Type", "IPS" } } },
                new Product { Id = 108, Name = "Dell UltraSharp 32\"", Category = "Monitors", Quantity = 10, Price = 750m, SerialNumber = "MON-DEL-U32", SupplierId = 5, Specifications = new Dictionary<string, string> { { "Resolution", "4K" }, { "Panel Type", "IPS" } } },
                new Product { Id = 109, Name = "Logitech MX Master 3S", Category = "Accessories", Quantity = 50, Price = 99m, SerialNumber = "ACC-LOG-MX3S", SupplierId = 1, Specifications = new Dictionary<string, string> { { "Type", "Mouse" }, { "Connection", "Wireless" } } },
                new Product { Id = 110, Name = "Keychron K8 Pro", Category = "Accessories", Quantity = 20, Price = 110m, SerialNumber = "ACC-KEY-K8P", SupplierId = 1, Specifications = new Dictionary<string, string> { { "Type", "Keyboard" }, { "Connection", "Bluetooth" } } },
                new Product { Id = 111, Name = "Epson EcoTank L3250", Category = "Printers", Quantity = 18, Price = 220m, SerialNumber = "PRN-EPS-L3250", SupplierId = 3, Specifications = new Dictionary<string, string> { { "Type", "Inkjet" }, { "Color Support", "Color" } } },
                new Product { Id = 112, Name = "Google Pixel 8 Pro", Category = "Phones", Quantity = 7, Price = 999m, SerialNumber = "GOO-PIX8-PRO", SupplierId = 2, Specifications = new Dictionary<string, string> { { "Storage", "256GB" }, { "Color", "Silver" } } },
                new Product { Id = 113, Name = "MacBook Air M2", Category = "Laptops", Quantity = 40, Price = 1099m, SerialNumber = "APP-MBA-2022", SupplierId = 1, Specifications = new Dictionary<string, string> { { "Processor", "Apple M2" }, { "RAM", "8GB" } } },
                new Product { Id = 114, Name = "Samsung Odyssey G9 49\"", Category = "Monitors", Quantity = 2, Price = 1499m, SerialNumber = "MON-SAM-G9", SupplierId = 2, Specifications = new Dictionary<string, string> { { "Resolution", "1440p" }, { "Panel Type", "OLED" } } }
            });

            // Seed Product Items (item-level serial numbers for existing stock)
            SeedProductItems();

            // Seed initial movements
            StockMovements.AddRange(new[]
            {
                new StockMovement { Id = 1, ProductId = 101, Type = "STOCK IN", QuantityChanged = 30, Timestamp = DateTime.Now.AddDays(-10), UserId = 2, Notes = "Initial batch from TechSource" },
                new StockMovement { Id = 2, ProductId = 101, Type = "STOCK OUT", QuantityChanged = -6, Timestamp = DateTime.Now.AddDays(-2), UserId = 6, Notes = "Sold to corporate client" },
                new StockMovement { Id = 3, ProductId = 102, Type = "STOCK IN", QuantityChanged = 12, Timestamp = DateTime.Now.AddDays(-7), UserId = 2, Notes = "New iPhone release shipment" },
                new StockMovement { Id = 4, ProductId = 102, Type = "STOCK OUT", QuantityChanged = -4, Timestamp = DateTime.Now.AddDays(-1), UserId = 3, Notes = "Retail sales" },
                new StockMovement { Id = 5, ProductId = 109, Type = "STOCK IN", QuantityChanged = 60, Timestamp = DateTime.Now.AddDays(-15), UserId = 4, Notes = "Bulk accessory order" },
                new StockMovement { Id = 6, ProductId = 109, Type = "STOCK OUT", QuantityChanged = -10, Timestamp = DateTime.Now.AddDays(-5), UserId = 6, Notes = "Store display and sales" },
                new StockMovement { Id = 7, ProductId = 104, Type = "STOCK IN", QuantityChanged = 5, Timestamp = DateTime.Now.AddDays(-20), UserId = 2, Notes = "Printers restock" },
                new StockMovement { Id = 8, ProductId = 104, Type = "STOCK OUT", QuantityChanged = -5, Timestamp = DateTime.Now.AddDays(-3), UserId = 6, Notes = "All printers sold out" },
                new StockMovement { Id = 9, ProductId = 114, Type = "STOCK IN", QuantityChanged = 2, Timestamp = DateTime.Now.AddDays(-1), UserId = 4, Notes = "Special order for VIP" },
                new StockMovement { Id = 10, ProductId = 105, Type = "STOCK IN", QuantityChanged = 12, Timestamp = DateTime.Now.AddDays(-8), UserId = 2, Notes = "Dell shipment arrived" }
            });

            // Seed Initial Logs
            AuditLogs.AddRange(new[]
            {
                new SystemLog { Id = 1, Timestamp = DateTime.Now.AddDays(-20), ActionType = "SYSTEM START", Description = "System initialized.", Username = "System" },
                new SystemLog { Id = 2, Timestamp = DateTime.Now.AddDays(-19), ActionType = "USER CREATED", Description = "User 'ahmed.clerk' was created.", Username = "admin.super" },
                new SystemLog { Id = 3, Timestamp = DateTime.Now.AddDays(-15), ActionType = "PRODUCT ADDED", Description = "Product 'Logitech MX Master 3S' added to inventory.", Username = "omar.proc" },
                new SystemLog { Id = 4, Timestamp = DateTime.Now.AddDays(-10), ActionType = "SUPPLIER ADDED", Description = "Supplier 'Mega Traders' registered.", Username = "admin.super" },
                new SystemLog { Id = 5, Timestamp = DateTime.Now.AddDays(-1), ActionType = "STOCK ALERT", Description = "Product 'HP LaserJet Pro' reached zero quantity.", Username = "System" },
                new SystemLog { Id = 6, Timestamp = DateTime.Now.AddMinutes(-30), ActionType = "USER LOGIN", Description = "User 'tariq.sales' logged in.", Username = "tariq.sales" }
            });
        }

        /// <summary>
        /// Seeds ProductItem records for each product's current quantity,
        /// creating item-level serial numbers (e.g., APP-MBP-2023-01, APP-MBP-2023-02, etc.)
        /// </summary>
        private static void SeedProductItems()
        {
            int itemId = 1;
            foreach (var product in Products)
            {
                for (int i = 1; i <= product.Quantity; i++)
                {
                    ProductItems.Add(new ProductItem
                    {
                        Id = itemId++,
                        ProductId = product.Id,
                        ItemSerialNumber = $"{product.SerialNumber}-{i:D2}",
                        IsInStock = true,
                        DateAdded = DateTime.Now.AddDays(-10) // Simulated initial stock date
                    });
                }
            }
        }
        #endregion
    }
}
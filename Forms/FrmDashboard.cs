using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
            EnableDoubleBuffered(dgvRecentActions);

            // Wire up the color formatting for the grid cells
            dgvRecentActions.CellFormatting += DgvRecentActions_CellFormatting;

            // Make low-stock panel clickable
            pnlLowStock.Cursor = Cursors.Hand;
            pnlLowStock.Click += PnlLowStock_Click;
            lblLowStockNum.Click += PnlLowStock_Click;
            lblLowStockTitle.Click += PnlLowStock_Click;
            lblLowStockDesc.Click += PnlLowStock_Click;

            // Make total-products panel clickable (open products and show all)
            pnlTotalProducts.Cursor = Cursors.Hand;
            pnlTotalProducts.Click += PnlTotalProducts_Click;
            lblTotalNum.Click += PnlTotalProducts_Click;
            lblTotalTitle.Click += PnlTotalProducts_Click;
            lblTotalDesc.Click += PnlTotalProducts_Click;
        }

        private void EnableDoubleBuffered(DataGridView dgv)
        {
            // Reduces flickering when loading/scrolling the grid
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgv,
                new object[] { true }
            );
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            SetupGridStyle();
            LoadDashboardMetrics();
            LoadRecentMovements();
        }

        private void LoadDashboardMetrics()
        {
            // 1. Calculate Total Products
            int totalProducts = MemoryStore.Products.Count;
            lblTotalNum.Text = totalProducts.ToString("N0"); // "N0" adds commas e.g., 1,000

            // 2. Calculate Low Stock alerts (items where quantity is 10 or less)
            int lowStockCount = MemoryStore.Products.Count(p => p.Quantity <= 10);
            lblLowStockNum.Text = lowStockCount.ToString("N0");
        }

        private void LoadRecentMovements()
        {
            // Clear existing rows
            dgvRecentActions.Rows.Clear();
            dgvRecentActions.AutoGenerateColumns = false;

            // Fetch the 10 most recent movements from our 'Database'
            var recentMovements = MemoryStore.StockMovements
                .OrderByDescending(m => m.Timestamp)
                .Take(10)
                .ToList();

            foreach (var movement in recentMovements)
            {
                // Relational lookup: Find the matching Product and User based on the IDs
                var product = MemoryStore.Products.FirstOrDefault(p => p.Id == movement.ProductId);
                var user = MemoryStore.Users.FirstOrDefault(u => u.Id == movement.UserId);

                // Safely format the names in case a record was deleted
                string productDetails = product != null
                    ? $"{product.Name} (SN: {product.SerialNumber})"
                    : "Unknown/Deleted Product";

                string operatorName = user != null
                    ? user.Username
                    : "System"; // Fallback to System if no user ID exists

                // Format quantity to show a '+' for additions, and handle Low Stock alerts specially
                string formattedQuantity = movement.QuantityChanged > 0
                    ? $"+{movement.QuantityChanged}"
                    : movement.QuantityChanged.ToString();

                if (movement.Type == "LOW STOCK" && product != null)
                {
                    formattedQuantity = product.Quantity.ToString(); // Just show the current quantity
                }

                // Add the assembled row to the DataGridView
                dgvRecentActions.Rows.Add(
                    productDetails,
                    movement.Type,
                    formattedQuantity,
                    movement.Timestamp.ToString("MMM dd, hh:mm tt"),
                    operatorName
                );
            }

            // Remove the default blue selection highlight for a cleaner look
            dgvRecentActions.ClearSelection();
        }

        private void SetupGridStyle()
        {
            // Professional minimal styling for the Dashboard Grid
            dgvRecentActions.EnableHeadersVisualStyles = false;

            // Header Style
            dgvRecentActions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvRecentActions.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 114, 128);
            dgvRecentActions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvRecentActions.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvRecentActions.ColumnHeadersHeight = 50;
            dgvRecentActions.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Row Style
            dgvRecentActions.DefaultCellStyle.BackColor = Color.White;
            dgvRecentActions.DefaultCellStyle.ForeColor = Color.FromArgb(55, 65, 81);
            dgvRecentActions.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgvRecentActions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            dgvRecentActions.DefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 24, 39);
            dgvRecentActions.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvRecentActions.RowTemplate.Height = 45;

            // General Grid Frame
            dgvRecentActions.BackgroundColor = Color.White;
            dgvRecentActions.BorderStyle = BorderStyle.None;
            dgvRecentActions.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecentActions.GridColor = Color.FromArgb(229, 231, 235);
            dgvRecentActions.RowHeadersVisible = false;
            dgvRecentActions.AllowUserToAddRows = false;
            dgvRecentActions.AllowUserToResizeRows = false;
            dgvRecentActions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentActions.MultiSelect = false;
        }

        private void DgvRecentActions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            string columnName = dgvRecentActions.Columns[e.ColumnIndex].Name;

            // Apply dynamic colors to the "Type" column
            if (columnName == "Type")
            {
                string statusType = e.Value.ToString();
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);

                switch (statusType)
                {
                    case "STOCK IN":
                    case "RESTOCK":
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); // Green
                        break;
                    case "STOCK OUT":
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246); // Blue
                        break;
                    case "LOW STOCK":
                        e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68); // Red
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128); // Gray
                        break;
                }
            }

            // Bold the Quantity column to make it stand out
            if (columnName == "Quantity")
            {
                e.CellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            }
        }

        private void PnlLowStock_Click(object sender, EventArgs e)
        {
            // Find the main form containing this dashboard
            var mainForm = this.TopLevelControl as FrmMain ?? this.Parent as FrmMain;
            if (mainForm == null)
            {
                // Traverse up the parent chain as a fallback
                Control p = this.Parent;
                while (p != null && !(p is FrmMain)) p = p.Parent;
                mainForm = p as FrmMain;
            }

            if (mainForm == null)
            {
                // As a last resort, try Application.OpenForms
                foreach (Form f in Application.OpenForms)
                {
                    if (f is FrmMain)
                    {
                        mainForm = (FrmMain)f;
                        break;
                    }
                }
            }

            if (mainForm != null)
            {
                // Create products form and enable low-stock filter before showing
                var productsForm = new FrmProducts();
                // Use reflection to set internal flag if property exists
                var prop = productsForm.GetType().GetProperty("ShowLowStockFilter");
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(productsForm, true);
                }

                // Open inside main
                mainForm.OpenChildForm(productsForm);
            }
            else
            {
                // Fallback: open standalone
                var productsForm = new FrmProducts();
                var prop = productsForm.GetType().GetProperty("ShowLowStockFilter");
                if (prop != null && prop.CanWrite) prop.SetValue(productsForm, true);
                productsForm.Show();
            }
        }

        private void PnlTotalProducts_Click(object sender, EventArgs e)
        {
            // Similar logic to open products but ensure all products are shown
            var mainForm = this.TopLevelControl as FrmMain ?? this.Parent as FrmMain;
            if (mainForm == null)
            {
                Control p = this.Parent;
                while (p != null && !(p is FrmMain)) p = p.Parent;
                mainForm = p as FrmMain;
            }

            if (mainForm == null)
            {
                foreach (Form f in Application.OpenForms)
                {
                    if (f is FrmMain)
                    {
                        mainForm = (FrmMain)f;
                        break;
                    }
                }
            }

            if (mainForm != null)
            {
                var productsForm = new FrmProducts();
                var propForce = productsForm.GetType().GetProperty("ForceShowAll");
                if (propForce != null && propForce.CanWrite)
                {
                    propForce.SetValue(productsForm, true);
                }

                // Also ensure low-stock flag is not set
                var propLow = productsForm.GetType().GetProperty("ShowLowStockFilter");
                if (propLow != null && propLow.CanWrite)
                {
                    propLow.SetValue(productsForm, false);
                }

                mainForm.OpenChildForm(productsForm);
            }
            else
            {
                var productsForm = new FrmProducts();
                var propForce = productsForm.GetType().GetProperty("ForceShowAll");
                if (propForce != null && propForce.CanWrite) propForce.SetValue(productsForm, true);
                var propLow = productsForm.GetType().GetProperty("ShowLowStockFilter");
                if (propLow != null && propLow.CanWrite) propLow.SetValue(productsForm, false);
                productsForm.Show();
            }
        }

        private void pnlTotalProducts_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
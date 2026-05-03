using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmStockOut : Form
    {
        public FrmStockOut()
        {
            InitializeComponent();
            this.Load += FrmStockOut_Load;
            btnExecuteStockOut.Click += BtnExecuteStockOut_Click;
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
            clbSerialNumbers.ItemCheck += ClbSerialNumbers_ItemCheck;
        }

        private void FrmStockOut_Load(object sender, EventArgs e)
        {
            // Set System ID
            lblSystemID.Text = "SYSTEM USER: " + (MemoryStore.CurrentUser?.Username?.ToUpper() ?? "UNKNOWN");

            RefreshData();
        }

        public void RefreshData()
        {
            LoadProducts();
            
            if (cmbOutReason.Items.Count > 0)
                cmbOutReason.SelectedIndex = 0;
            
            txtRecipient.Clear();
            txtWarrantyStatus.Clear();
        }

        private void LoadProducts()
        {
            // Only load products that are in stock for stock out
            var products = MemoryStore.Products.Where(p => p.Quantity > 0).ToList();
            
            cmbProduct.DataSource = null;
            cmbProduct.DisplayMember = "Name";
            cmbProduct.ValueMember = "Id";
            cmbProduct.DataSource = products;
            cmbProduct.SelectedIndex = -1;
            
            ResetProductDetails();
        }

        private void ResetProductDetails()
        {
            lblStockStatus.Text = "In Stock: 0";
            lblStockStatus.ForeColor = Color.FromArgb(220, 38, 38); // Red
            
            numQty.Minimum = 0; // Set Minimum first to avoid ArgumentOutOfRangeException
            numQty.Maximum = 0;
            numQty.Value = 0;
            numQty.Enabled = false;
            
            clbSerialNumbers.Items.Clear();
            lblWarrantyInfo.Text = "Warranty Expires: --/--/----";
        }

        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex == -1 || cmbProduct.SelectedItem == null)
            {
                ResetProductDetails();
                return;
            }

            var product = (Product)cmbProduct.SelectedItem;
            
            lblStockStatus.Text = $"In Stock: {product.Quantity}";
            lblStockStatus.ForeColor = product.Quantity > 10 ? Color.Green : Color.FromArgb(220, 38, 38);
            
            numQty.Enabled = true;
            numQty.Maximum = product.Quantity;
            numQty.Minimum = 1;
            numQty.Value = 1;

            // Mocking Serial Numbers based on quantity and actual product serial number prefix
            clbSerialNumbers.Items.Clear();
            for (int i = 1; i <= Math.Min(product.Quantity, 50); i++) // Max 50 for UI performance
            {
                clbSerialNumbers.Items.Add($"{product.SerialNumber}-{(product.Quantity - i + 1):D3}");
            }
            
            lblWarrantyInfo.Text = $"Warranty Expires: {DateTime.Now.AddYears(1):dd/MM/yyyy}";
        }

        private void ClbSerialNumbers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Calculate how many items will be checked after this event
            int checkedCount = clbSerialNumbers.CheckedItems.Count;
            if (e.NewValue == CheckState.Checked) checkedCount++;
            if (e.NewValue == CheckState.Unchecked) checkedCount--;

            if (checkedCount > 0 && checkedCount <= numQty.Maximum)
            {
                numQty.Value = checkedCount;
            }
        }

        private void BtnExecuteStockOut_Click(object sender, EventArgs e)
        {
            // 1. Validation
            if (cmbProduct.SelectedValue == null)
            {
                MessageBox.Show("Please select a product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbOutReason.SelectedItem == null)
            {
                MessageBox.Show("Please select a transaction type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRecipient.Text))
            {
                MessageBox.Show("Please enter the recipient or customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quantity = (int)numQty.Value;
            var product = (Product)cmbProduct.SelectedItem;

            if (quantity <= 0 || quantity > product.Quantity)
            {
                MessageBox.Show($"Quantity must be between 1 and {product.Quantity}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Prepare Data
            int productId = product.Id;
            string reason = cmbOutReason.SelectedItem.ToString();
            
            string notes = $"Reason: {reason} | Recipient: {txtRecipient.Text.Trim()}";
            
            if (!string.IsNullOrWhiteSpace(txtWarrantyStatus.Text))
            {
                notes += $" | Warranty Info: {txtWarrantyStatus.Text.Trim()}";
            }

            var checkedSerials = clbSerialNumbers.CheckedItems.Cast<string>().ToList();
            if (checkedSerials.Any())
            {
                notes += $" | Serials: {string.Join(", ", checkedSerials)}";
            }

            // 3. Execute Business Logic
            // Stock out decreases quantity, so pass negative quantity
            bool success = MemoryStore.PerformStockMovement(productId, -quantity, "STOCK OUT", notes);

            // 4. Handle Result
            if (success)
            {
                MessageBox.Show("Stock Out operation recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // If product quantity becomes 0, we might need to log a low stock warning or it's handled by system
                if (product.Quantity <= 10)
                {
                    MemoryStore.LogAction("LOW STOCK ALERT", $"Product '{product.Name}' is running low (Current: {product.Quantity}).");
                    if (product.Quantity <= 5)
                    {
                        MessageBox.Show($"Warning: Stock for {product.Name} is running low ({product.Quantity} left).", "Low Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                RefreshData(); // Reloads products and clears form
            }
            else
            {
                MessageBox.Show("Failed to record Stock Out operation. Please check product details and inventory levels.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

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

        /// <summary>
        /// When a product is selected, load its actual available item-level serial numbers
        /// from the ProductItems store (only items that are IsInStock = true).
        /// </summary>
        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex == -1 || cmbProduct.SelectedItem == null)
            {
                ResetProductDetails();
                return;
            }

            var product = (Product)cmbProduct.SelectedItem;
            
            // Get actual available items from the ProductItems store
            var availableItems = MemoryStore.GetAvailableItems(product.Id);
            int availableCount = availableItems.Count;

            lblStockStatus.Text = $"In Stock: {availableCount} items";
            lblStockStatus.ForeColor = availableCount > 10 ? Color.Green : Color.FromArgb(220, 38, 38);
            
            numQty.Enabled = availableCount > 0;
            numQty.Maximum = availableCount;
            numQty.Minimum = availableCount > 0 ? 1 : 0;
            numQty.Value = availableCount > 0 ? 1 : 0;

            // Load real item-level serial numbers into the CheckedListBox
            clbSerialNumbers.Items.Clear();
            foreach (var item in availableItems.OrderBy(pi => pi.ItemSerialNumber))
            {
                clbSerialNumbers.Items.Add(item.ItemSerialNumber);
            }
            
            lblWarrantyInfo.Text = $"Product Serial: {product.SerialNumber} | Warranty: {DateTime.Now.AddYears(1):dd/MM/yyyy}";
        }

        /// <summary>
        /// Sync the quantity spinner with the number of checked serial numbers.
        /// Users select individual items (serial numbers) they want to dispatch.
        /// </summary>
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

            var product = (Product)cmbProduct.SelectedItem;

            // Get selected serial numbers (items to dispatch)
            var selectedSerials = clbSerialNumbers.CheckedItems.Cast<string>().ToList();
            
            int quantity;
            if (selectedSerials.Count > 0)
            {
                // Use the count of selected serials as the quantity
                quantity = selectedSerials.Count;
            }
            else
            {
                // If no specific serials selected, use the numeric value (FIFO auto-selection)
                quantity = (int)numQty.Value;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Please select at least one item to dispatch.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var availableItems = MemoryStore.GetAvailableItems(product.Id);
            if (quantity > availableItems.Count)
            {
                MessageBox.Show($"Cannot dispatch {quantity} items. Only {availableItems.Count} available.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // 3. Execute Business Logic (pass selected serials for item-level tracking)
            bool success = MemoryStore.PerformStockMovement(
                productId, 
                -quantity, 
                "STOCK OUT", 
                notes, 
                selectedSerials.Count > 0 ? selectedSerials : null
            );

            // 4. Handle Result
            if (success)
            {
                // Build dispatch confirmation with dispatched serial details
                string serialDetails = selectedSerials.Count > 0
                    ? $"\n\nDispatched Items:\n{string.Join("\n", selectedSerials.Take(10))}{(selectedSerials.Count > 10 ? $"\n... and {selectedSerials.Count - 10} more" : "")}"
                    : $"\n\n{quantity} item(s) dispatched via FIFO auto-selection.";

                MessageBox.Show($"Stock Out operation recorded successfully.{serialDetails}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Check low stock warning
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

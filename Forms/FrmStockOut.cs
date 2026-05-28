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
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        // Holds the product ID passed in from FrmProducts right-click; -1 means normal (no preselection)
        private int _preselectedProductId = -1;

        /// <summary>Standard constructor — no preselection.</summary>
        public FrmStockOut()
        {
            InitializeComponent();
            this.Load += FrmStockOut_Load;
            btnExecuteStockOut.Click += BtnExecuteStockOut_Click;
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
            clbSerialNumbers.ItemCheck += ClbSerialNumbers_ItemCheck;
        }

        /// <summary>
        /// Constructor used when launched via right-click from FrmProducts.
        /// The specified product will be pre-selected in cmbProduct on load.
        /// </summary>
        public FrmStockOut(int productId) : this()
        {
            _preselectedProductId = productId;
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

            // Pre-select product when launched via right-click from FrmProducts
            if (_preselectedProductId > 0)
            {
                cmbProduct.SelectedValue = _preselectedProductId;
                // Disable the combo to make it clear this was an intentional pre-selection
                cmbProduct.Enabled = false;
            }
            else
            {
                cmbProduct.Enabled = true;
            }

            if (cmbOutReason.Items.Count > 0)
                cmbOutReason.SelectedIndex = 0;

            txtRecipient.Clear();
            ResetWarrantyCard();
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
            ResetWarrantyCard();
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

            // Show initial warranty info (no items selected yet)
            UpdateWarrantyDisplay(new List<string>(), product);
        }

        // ─────────────────────────────────────────────────────────────────
        // Warranty Card Helpers
        // ─────────────────────────────────────────────────────────────────

        /// <summary>Resets the warranty card to its default "awaiting" state.</summary>
        private void ResetWarrantyCard()
        {
            pnlWarrantyCard.BackColor  = Color.FromArgb(240, 244, 255);  // blue tint
            lblWarrantyTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblWarrantyDuration.Text      = "—";
            lblWarrantyDuration.ForeColor = Color.FromArgb(148, 163, 184);
            lblWarrantyExpiry.Text     = "Select a product to begin";
            lblWarrantyExpiry.ForeColor= Color.FromArgb(148, 163, 184);
            txtWarrantyInfo.Text       = "";
        }

        /// <summary>
        /// Resolves and displays warranty for the selected serials.
        /// • No selection   → prompts user to select items
        /// • Uniform warranty → GREEN card  — shows duration + expiry date
        /// • Mixed warranties  → AMBER card  — "Varies per batch" + per-item summary
        /// • No warranty data  → GREY  card  — "No Warranty"
        /// </summary>
        private void UpdateWarrantyDisplay(List<string> selectedSerials, Product product = null)
        {
            if (product == null)
            {
                if (cmbProduct.SelectedItem is Product p) product = p;
                else { ResetWarrantyCard(); return; }
            }

            // Right-side label always shows the product serial
            txtWarrantyInfo.Text = $"Product Serial:\r\n{product.SerialNumber}";

            // ── No items selected yet ────────────────────────────────────
            if (selectedSerials == null || selectedSerials.Count == 0)
            {
                pnlWarrantyCard.BackColor     = Color.FromArgb(240, 244, 255); // blue tint
                lblWarrantyDuration.Text      = "—";
                lblWarrantyDuration.ForeColor = Color.FromArgb(100, 116, 139);
                lblWarrantyExpiry.Text        = "Select items below to view warranty";
                lblWarrantyExpiry.ForeColor   = Color.FromArgb(100, 116, 139);
                return;
            }

            // Resolve warranty months for each selected item via its batch
            var warrantyResults = selectedSerials
                .Select(serial => new
                {
                    Serial = serial,
                    Months = MemoryStore.GetItemWarrantyMonths(product.Id, serial)
                })
                .ToList();

            var distinctMonths = warrantyResults.Select(r => r.Months).Distinct().ToList();

            if (distinctMonths.Count == 1)
            {
                int? months = distinctMonths[0];

                if (months.HasValue && months.Value > 0)
                {
                    // ── GREEN: Active warranty ────────────────────────────
                    DateTime expiryDate = DateTime.Now.AddMonths(months.Value);
                    pnlWarrantyCard.BackColor     = Color.FromArgb(240, 253, 244); // green tint
                    lblWarrantyDuration.Text      = $"{months.Value} Months";
                    lblWarrantyDuration.ForeColor = Color.FromArgb(21, 128, 61);   // green
                    lblWarrantyExpiry.Text        = $"Expires: {expiryDate:dd MMM yyyy}";
                    lblWarrantyExpiry.ForeColor   = Color.FromArgb(22, 101, 52);
                    txtWarrantyInfo.Text         += $"\r\nWarranty Expires:\r\n{expiryDate:dd MMM yyyy}";
                }
                else
                {
                    // ── GREY: No warranty ─────────────────────────────────
                    pnlWarrantyCard.BackColor     = Color.FromArgb(249, 250, 251); // grey
                    lblWarrantyDuration.Text      = "No Warranty";
                    lblWarrantyDuration.ForeColor = Color.FromArgb(107, 114, 128);
                    lblWarrantyExpiry.Text        = "No warranty recorded for this batch";
                    lblWarrantyExpiry.ForeColor   = Color.FromArgb(156, 163, 175);
                }
            }
            else
            {
                // ── AMBER: Mixed warranties from different batches ────────
                pnlWarrantyCard.BackColor     = Color.FromArgb(255, 251, 235); // amber tint
                lblWarrantyDuration.Text      = "Varies";
                lblWarrantyDuration.ForeColor = Color.FromArgb(180, 83, 9);   // amber
                lblWarrantyExpiry.Text        = "Items sourced from multiple batches";
                lblWarrantyExpiry.ForeColor   = Color.FromArgb(146, 64, 14);

                // Build per-item detail on the right side
                var sb = new StringBuilder();
                sb.AppendLine($"Product Serial:");
                sb.AppendLine(product.SerialNumber);
                sb.AppendLine();
                sb.AppendLine("Per-item Warranty:");
                foreach (var r in warrantyResults)
                {
                    string w = r.Months.HasValue && r.Months.Value > 0
                        ? $"{r.Months.Value} Months"
                        : "No warranty";
                    sb.AppendLine($"{r.Serial}  →  {w}");
                }
                txtWarrantyInfo.Text = sb.ToString().TrimEnd();
            }
        }

        /// <summary>
        /// Sync the quantity spinner with the number of checked serial numbers.
        /// Also refreshes warranty display after the check state settles.
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

            // Warranty refresh must run after the CheckedItems list is updated
            // (ItemCheck fires before the state actually changes, so use BeginInvoke)
            this.BeginInvoke(new Action(() =>
            {
                var selected = clbSerialNumbers.CheckedItems.Cast<string>().ToList();
                UpdateWarrantyDisplay(selected);

            }));
        }

        private void BtnExecuteStockOut_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            bool isValid = true;
            string errorMsg;

            // Product
            if (cmbProduct.SelectedValue == null)
            { _errorProvider.SetError(cmbProduct, "Please select a product."); isValid = false; }
            else
              _errorProvider.SetError(cmbProduct, string.Empty);

            // Reason
            if (cmbOutReason.SelectedItem == null)
            { _errorProvider.SetError(cmbOutReason, "Please select a transaction type."); isValid = false; }
            else
              _errorProvider.SetError(cmbOutReason, string.Empty);

            // Recipient
            if (!ValidationHelper.IsRequired(txtRecipient.Text, out errorMsg))
            { _errorProvider.SetError(txtRecipient, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(txtRecipient.Text.Trim(), 2, 100, out errorMsg))
            { _errorProvider.SetError(txtRecipient, errorMsg); isValid = false; }
            else
              _errorProvider.SetError(txtRecipient, string.Empty);

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product = (Product)cmbProduct.SelectedItem;

            // Get selected serial numbers (items to dispatch)
            var selectedSerials = clbSerialNumbers.CheckedItems.Cast<string>().ToList();

            int quantity = selectedSerials.Count > 0 ? selectedSerials.Count : (int)numQty.Value;

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

            // Append warranty info from the auto-detected card
            string warrantyLabel = lblWarrantyDuration.Text;
            if (!string.IsNullOrWhiteSpace(warrantyLabel) && warrantyLabel != "—")
            {
                notes += $" | Warranty: {warrantyLabel}";
            }

            // 3. Execute Business Logic (pass selected serials for item-level tracking)
            bool success = MemoryStore.PerformStockMovement(
                productId, 
                -quantity, 
                StockMovementType.StockOut, 
                notes, 
                selectedSerials.Count > 0 ? selectedSerials : null,
                null,
                null
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem
{
    public partial class FrmStockOut : Form
    {
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        private string _preselectedProductSerial = null;

        /// <summary>Standard constructor — no preselection.</summary>
        public FrmStockOut()
        {
            InitializeComponent();
            this.Load += FrmStockOut_Load;
            btnExecuteStockOut.Click += BtnExecuteStockOut_Click;
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
            clbSerialNumbers.ItemCheck += ClbSerialNumbers_ItemCheck;
        }

        /// <summary>Constructor used when launched via right-click from FrmProducts.</summary>
        public FrmStockOut(string productSerial) : this()
        {
            _preselectedProductSerial = productSerial;
        }

        private void FrmStockOut_Load(object sender, EventArgs e)
        {
            lblSystemID.Text = "SYSTEM USER: " + (DatabaseHelper.CurrentUser?.Username?.ToUpper() ?? "UNKNOWN");
            RefreshData();
        }

        public void RefreshData()
        {
            LoadProducts();

            if (!string.IsNullOrEmpty(_preselectedProductSerial))
            {
                cmbProduct.SelectedValue = _preselectedProductSerial;
                cmbProduct.Enabled       = false;
            }
            else
            {
                cmbProduct.Enabled = true;
            }

            if (cmbOutReason.Items.Count > 0) cmbOutReason.SelectedIndex = 0;
            txtRecipient.Clear();
            ResetWarrantyCard();
        }

        private void LoadProducts()
        {
            var products = ProductRepository.GetAll().Where(p => p.Quantity > 0).ToList();

            cmbProduct.DataSource    = null;
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember   = "SerialNumber";
            cmbProduct.DataSource    = products;
            cmbProduct.SelectedIndex = -1;

            ResetProductDetails();
        }

        private void ResetProductDetails()
        {
            lblStockStatus.Text      = "In Stock: 0";
            lblStockStatus.ForeColor = Color.FromArgb(220, 38, 38);

            numQty.Minimum = 0;
            numQty.Maximum = 0;
            numQty.Value   = 0;
            numQty.Enabled = false;

            clbSerialNumbers.Items.Clear();
            ResetWarrantyCard();
        }

        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex == -1 || cmbProduct.SelectedItem == null)
            {
                ResetProductDetails();
                return;
            }

            var product        = (Product)cmbProduct.SelectedItem;
            var availableItems = ProductItemRepository.GetAvailable(product.SerialNumber);
            int count          = availableItems.Count;

            lblStockStatus.Text      = $"In Stock: {count} items";
            lblStockStatus.ForeColor = count > 10 ? Color.Green : Color.FromArgb(220, 38, 38);

            numQty.Enabled = count > 0;
            numQty.Maximum = count;
            numQty.Minimum = count > 0 ? 1 : 0;
            numQty.Value   = count > 0 ? 1 : 0;

            clbSerialNumbers.Items.Clear();
            foreach (var item in availableItems.OrderBy(i => i.ItemSerialNumber))
                clbSerialNumbers.Items.Add(item.ItemSerialNumber);

            UpdateWarrantyDisplay(new List<string>(), product);
        }

        private void ResetWarrantyCard()
        {
            pnlWarrantyCard.BackColor     = Color.FromArgb(240, 244, 255);
            lblWarrantyTitle.ForeColor    = Color.FromArgb(100, 116, 139);
            lblWarrantyDuration.Text      = "—";
            lblWarrantyDuration.ForeColor = Color.FromArgb(148, 163, 184);
            lblWarrantyExpiry.Text        = "Select a product to begin";
            lblWarrantyExpiry.ForeColor   = Color.FromArgb(148, 163, 184);
            txtWarrantyInfo.Text          = "";
        }

        private void UpdateWarrantyDisplay(List<string> selectedSerials, Product product = null)
        {
            if (product == null)
            {
                if (cmbProduct.SelectedItem is Product p) product = p;
                else { ResetWarrantyCard(); return; }
            }

            txtWarrantyInfo.Text = $"Product Serial:\r\n{product.SerialNumber}";

            if (selectedSerials == null || selectedSerials.Count == 0)
            {
                pnlWarrantyCard.BackColor     = Color.FromArgb(240, 244, 255);
                lblWarrantyDuration.Text      = "—";
                lblWarrantyDuration.ForeColor = Color.FromArgb(100, 116, 139);
                lblWarrantyExpiry.Text        = "Select items below to view warranty";
                lblWarrantyExpiry.ForeColor   = Color.FromArgb(100, 116, 139);
                return;
            }

            // Resolve warranty for each item via its batch movement
            var warrantyResults = selectedSerials.Select(serial =>
            {
                var item = ProductItemRepository.GetAvailable(product.SerialNumber)
                    .FirstOrDefault(i => i.ItemSerialNumber == serial);
                int? months = null;
                if (item?.BatchMovementId.HasValue == true)
                {
                    var movements = StockMovementRepository.GetByProduct(product.SerialNumber);
                    months = movements.FirstOrDefault(m => m.MovementId == item.BatchMovementId.Value)?.WarrantyMonths;
                }
                return new { Serial = serial, Months = months };
            }).ToList();

            var distinct = warrantyResults.Select(r => r.Months).Distinct().ToList();

            if (distinct.Count == 1)
            {
                int? months = distinct[0];
                if (months.HasValue && months.Value > 0)
                {
                    DateTime expiry               = DateTime.Now.AddMonths(months.Value);
                    pnlWarrantyCard.BackColor     = Color.FromArgb(240, 253, 244);
                    lblWarrantyDuration.Text      = $"{months.Value} Months";
                    lblWarrantyDuration.ForeColor = Color.FromArgb(21, 128, 61);
                    lblWarrantyExpiry.Text        = $"Expires: {expiry:dd MMM yyyy}";
                    lblWarrantyExpiry.ForeColor   = Color.FromArgb(22, 101, 52);
                    txtWarrantyInfo.Text         += $"\r\nWarranty Expires:\r\n{expiry:dd MMM yyyy}";
                }
                else
                {
                    pnlWarrantyCard.BackColor     = Color.FromArgb(249, 250, 251);
                    lblWarrantyDuration.Text      = "No Warranty";
                    lblWarrantyDuration.ForeColor = Color.FromArgb(107, 114, 128);
                    lblWarrantyExpiry.Text        = "No warranty recorded for this batch";
                    lblWarrantyExpiry.ForeColor   = Color.FromArgb(156, 163, 175);
                }
            }
            else
            {
                pnlWarrantyCard.BackColor     = Color.FromArgb(255, 251, 235);
                lblWarrantyDuration.Text      = "Varies";
                lblWarrantyDuration.ForeColor = Color.FromArgb(180, 83, 9);
                lblWarrantyExpiry.Text        = "Items sourced from multiple batches";
                lblWarrantyExpiry.ForeColor   = Color.FromArgb(146, 64, 14);

                var sb = new StringBuilder();
                sb.AppendLine("Product Serial:");
                sb.AppendLine(product.SerialNumber);
                sb.AppendLine();
                sb.AppendLine("Per-item Warranty:");
                foreach (var r in warrantyResults)
                {
                    string w = r.Months.HasValue && r.Months.Value > 0
                        ? $"{r.Months.Value} Months" : "No warranty";
                    sb.AppendLine($"{r.Serial}  →  {w}");
                }
                txtWarrantyInfo.Text = sb.ToString().TrimEnd();
            }
        }

        private void ClbSerialNumbers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int checkedCount = clbSerialNumbers.CheckedItems.Count;
            if (e.NewValue == CheckState.Checked)   checkedCount++;
            if (e.NewValue == CheckState.Unchecked) checkedCount--;

            if (checkedCount > 0 && checkedCount <= numQty.Maximum)
                numQty.Value = checkedCount;

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

            if (cmbProduct.SelectedValue == null)
            { _errorProvider.SetError(cmbProduct, "Please select a product."); isValid = false; }
            else _errorProvider.SetError(cmbProduct, string.Empty);

            if (cmbOutReason.SelectedItem == null)
            { _errorProvider.SetError(cmbOutReason, "Please select a transaction type."); isValid = false; }
            else _errorProvider.SetError(cmbOutReason, string.Empty);

            if (!ValidationHelper.IsRequired(txtRecipient.Text, out errorMsg))
            { _errorProvider.SetError(txtRecipient, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(txtRecipient.Text.Trim(), 2, 100, out errorMsg))
            { _errorProvider.SetError(txtRecipient, errorMsg); isValid = false; }
            else _errorProvider.SetError(txtRecipient, string.Empty);

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product  = (Product)cmbProduct.SelectedItem;
            var selected = clbSerialNumbers.CheckedItems.Cast<string>().ToList();
            int quantity = selected.Count > 0 ? selected.Count : (int)numQty.Value;

            if (quantity <= 0)
            {
                MessageBox.Show("Please select at least one item to dispatch.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int available = ProductItemRepository.CountInStock(product.SerialNumber);
            if (quantity > available)
            {
                MessageBox.Show($"Cannot dispatch {quantity} items. Only {available} available.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reason  = cmbOutReason.SelectedItem.ToString();
            string notes   = $"Reason: {reason} | Recipient: {txtRecipient.Text.Trim()} | Warranty: {lblWarrantyDuration.Text}";

            // 1. Record StockMovement
            var movement = new StockMovement
            {
                ProductSerial   = product.SerialNumber,
                MovementType    = "StockOut",
                QuantityChanged = quantity,
                Username        = DatabaseHelper.CurrentUser?.Username,
                Notes           = notes
            };
            StockMovementRepository.Add(movement);

            // 2. Mark items as removed (specific serials if chosen, otherwise FIFO)
            if (selected.Count > 0)
                foreach (var serial in selected)
                    ProductItemRepository.MarkRemoved(serial);
            else
                ProductItemRepository.MarkRemovedBatch(product.SerialNumber, quantity);

            string details = selected.Count > 0
                ? $"\n\nDispatched:\n{string.Join("\n", selected.Take(10))}{(selected.Count > 10 ? $"\n... and {selected.Count - 10} more" : "")}"
                : $"\n\n{quantity} item(s) dispatched via FIFO.";

            MessageBox.Show($"Stock Out recorded successfully.{details}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Low stock warning
            int remaining = ProductItemRepository.CountInStock(product.SerialNumber);
            if (remaining <= 5)
                MessageBox.Show($"⚠ Warning: Stock for '{product.ProductName}' is running low ({remaining} left).", "Low Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            RefreshData();
        }
    }
}

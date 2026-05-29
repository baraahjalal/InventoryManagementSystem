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
        private int? _preselectedProductId = null;
        private string _movementType = "StockOut";

        public FrmStockOut()
        {
            InitializeComponent();
            this.Load += FrmStockOut_Load;
            btnExecuteStockOut.Click += BtnExecuteStockOut_Click;
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
            clbSerialNumbers.ItemCheck += ClbSerialNumbers_ItemCheck;
        }

        public FrmStockOut(int productId) : this()
        {
            _preselectedProductId = productId;
        }

        public FrmStockOut(int productId, string movementType) : this(productId)
        {
            _movementType = movementType;
        }

        private void FrmStockOut_Load(object sender, EventArgs e)
        {
            lblSystemID.Text = "SYSTEM USER: " + (DatabaseHelper.CurrentUser?.Username?.ToUpper() ?? "UNKNOWN");
            InitModeToggle();
            RefreshData();
        }

        private void InitModeToggle()
        {
            rbStockOut.Checked          = (_movementType == "StockOut");
            rbReturnToSupplier.Checked  = (_movementType == "ReturnToSupplier");
            rbStockOut.CheckedChanged         += (s, e) => { if (rbStockOut.Checked)         ApplyMode("StockOut"); };
            rbReturnToSupplier.CheckedChanged += (s, e) => { if (rbReturnToSupplier.Checked) ApplyMode("ReturnToSupplier"); };
            ApplyMode(_movementType);
        }

        private void ApplyMode(string mode)
        {
            _movementType = mode;
            bool isReturn = (mode == "ReturnToSupplier");

            lblHeader.Text    = isReturn ? "Return to Supplier"                           : "Inventory Outbound";
            lblSubHeader.Text = isReturn ? "Send defective or excess units back to supplier." : "Track and manage hardware distribution with precision.";
            btnExecuteStockOut.Text = isReturn ? "CONFIRM RETURN TO SUPPLIER" : "CONFIRM STOCK OUT";

            lblReturnSupplier.Visible  = isReturn;
            cmbReturnSupplier.Visible  = isReturn;

            if (isReturn && cmbReturnSupplier.Items.Count == 0)
                LoadReturnSuppliers();
        }

        private void LoadReturnSuppliers()
        {
            var suppliers = SupplierRepository.GetActive();
            cmbReturnSupplier.DataSource    = null;
            cmbReturnSupplier.DisplayMember = "SupplierName";
            cmbReturnSupplier.ValueMember   = "SupplierTaxNumber";
            cmbReturnSupplier.DataSource    = suppliers;
            if (suppliers.Count > 0) cmbReturnSupplier.SelectedIndex = 0;
            else                     cmbReturnSupplier.SelectedIndex = -1;
        }

        public void RefreshData()
        {
            LoadProducts();

            if (_preselectedProductId.HasValue)
            {
                cmbProduct.SelectedValue = _preselectedProductId.Value;
                cmbProduct.Enabled       = false;
            }
            else
            {
                cmbProduct.Enabled = true;
            }

            ResetWarrantyCard();
        }

        private void LoadProducts()
        {
            var products = ProductRepository.GetAll().Where(p => p.Quantity > 0).ToList();

            cmbProduct.DataSource    = null;
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember   = "ProductSerialNumber";
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
            var availableItems = ProductItemRepository.GetAvailable(product.ProductSerialNumber);
            int count          = availableItems.Count;

            lblStockStatus.Text      = $"In Stock: {count} items";
            lblStockStatus.ForeColor = count > 10 ? Color.Green : Color.FromArgb(220, 38, 38);

            numQty.Enabled = count > 0;
            numQty.Maximum = count;
            numQty.Minimum = count > 0 ? 1 : 0;
            numQty.Value   = count > 0 ? 1 : 0;

            clbSerialNumbers.Items.Clear();
            foreach (var item in availableItems.OrderBy(i => i.ItemID))
                clbSerialNumbers.Items.Add(item.ItemID);

            UpdateWarrantyDisplay(new List<int>(), product);
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

        private void UpdateWarrantyDisplay(List<int> selectedItemIds, Product product = null)
        {
            if (product == null)
            {
                if (cmbProduct.SelectedItem is Product p) product = p;
                else { ResetWarrantyCard(); return; }
            }

            txtWarrantyInfo.Text = $"Product ID:\r\n{product.ProductSerialNumber}";

            if (selectedItemIds == null || selectedItemIds.Count == 0)
            {
                pnlWarrantyCard.BackColor     = Color.FromArgb(240, 244, 255);
                lblWarrantyDuration.Text      = "—";
                lblWarrantyDuration.ForeColor = Color.FromArgb(100, 116, 139);
                lblWarrantyExpiry.Text        = "Select items below to view warranty";
                lblWarrantyExpiry.ForeColor   = Color.FromArgb(100, 116, 139);
                return;
            }

            var warrantyResults = selectedItemIds.Select(itemId =>
            {
                var item = ProductItemRepository.GetAvailable(product.ProductSerialNumber)
                    .FirstOrDefault(i => i.ItemID == itemId);
                int? months = null;
                if (item?.BatchMovementId.HasValue == true)
                {
                    var movements = StockMovementRepository.GetByProduct(product.ProductSerialNumber);
                    months = movements.FirstOrDefault(m => m.MovementId == item.BatchMovementId.Value)?.WarrantyMonths;
                }
                return new { ItemId = itemId, Months = months };
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
                sb.AppendLine("Product ID:");
                sb.AppendLine(product.ProductSerialNumber.ToString());
                sb.AppendLine();
                sb.AppendLine("Per-item Warranty:");
                foreach (var r in warrantyResults)
                {
                    string w = r.Months.HasValue && r.Months.Value > 0
                        ? $"{r.Months.Value} Months" : "No warranty";
                    sb.AppendLine($"ItemID {r.ItemId}  →  {w}");
                }
                txtWarrantyInfo.Text = sb.ToString().TrimEnd();
            }
        }

        private void ClbSerialNumbers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int checkedCount = clbSerialNumbers.CheckedItems.Count;
            if (e.NewValue == CheckState.Checked)   checkedCount++;
            if (e.NewValue == CheckState.Unchecked) checkedCount--;

            if (checkedCount >= 0 && checkedCount <= numQty.Maximum)
                numQty.Value = Math.Max(checkedCount, numQty.Minimum);

            this.BeginInvoke(new Action(() =>
            {
                var selected = clbSerialNumbers.CheckedItems.Cast<int>().ToList();
                UpdateWarrantyDisplay(selected);
            }));
        }

        private void BtnExecuteStockOut_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            bool isValid = true;

            if (cmbProduct.SelectedValue == null)
            { _errorProvider.SetError(cmbProduct, "Please select a product."); isValid = false; }
            else _errorProvider.SetError(cmbProduct, string.Empty);

            bool isReturn = (_movementType == "ReturnToSupplier");
            if (isReturn && cmbReturnSupplier.SelectedValue == null)
            { _errorProvider.SetError(cmbReturnSupplier, "Please select a supplier for the return."); isValid = false; }
            else _errorProvider.SetError(cmbReturnSupplier, string.Empty);

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product     = (Product)cmbProduct.SelectedItem;
            var selectedIds = clbSerialNumbers.CheckedItems.Cast<int>().ToList();
            int quantity    = selectedIds.Count > 0 ? selectedIds.Count : (int)numQty.Value;

            if (quantity <= 0)
            {
                string action = isReturn ? "return" : "dispatch";
                MessageBox.Show($"Please select at least one item to {action}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int available = ProductItemRepository.CountInStock(product.ProductSerialNumber);
            if (quantity > available)
            {
                string action = isReturn ? "return" : "dispatch";
                MessageBox.Show($"Cannot {action} {quantity} items. Only {available} available.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? supplierTaxNum = null;
            if (isReturn && cmbReturnSupplier.SelectedItem is Supplier returnSupplier)
                supplierTaxNum = returnSupplier.SupplierTaxNumber;

            string notes = isReturn
                ? $"Return to Supplier | Warranty: {lblWarrantyDuration.Text}"
                : $"Warranty: {lblWarrantyDuration.Text}";

            var movement = new StockMovement
            {
                ProductSerialNumber = product.ProductSerialNumber,
                MovementType        = _movementType,
                QuantityChanged     = quantity,
                EmployeeID          = DatabaseHelper.CurrentUser?.EmployeeID,
                Notes               = notes,
                SupplierTaxNumber   = supplierTaxNum
            };
            using (var conn = DAL.DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        StockMovementRepository.Add(movement, conn, tran);
                        if (selectedIds.Count > 0)
                            foreach (var itemId in selectedIds)
                                ProductItemRepository.MarkRemoved(itemId, conn, tran);
                        else
                            ProductItemRepository.MarkRemovedBatch(product.ProductSerialNumber, quantity, conn, tran);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }

            string actionLabel = isReturn ? "Return to Supplier" : "Stock Out";
            string details = selectedIds.Count > 0
                ? $"\n\nItem IDs:\n{string.Join(", ", selectedIds.Take(10))}{(selectedIds.Count > 10 ? $"\n... and {selectedIds.Count - 10} more" : "")}"
                : $"\n\n{quantity} item(s) processed via FIFO.";

            MessageBox.Show($"{actionLabel} recorded successfully.{details}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int remaining = ProductItemRepository.CountInStock(product.ProductSerialNumber);
            if (remaining <= 10)
                MessageBox.Show($"Warning: Stock for '{product.ProductName}' is running low ({remaining} left).", "Low Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            RefreshData();
        }
    }
}

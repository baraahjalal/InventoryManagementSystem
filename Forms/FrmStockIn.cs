using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem.Classes;

namespace InventoryManagementSystem
{
    public partial class FrmStockIn : Form
    {
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        public FrmStockIn()
        {
            InitializeComponent();
            this.Load += FrmStockIn_Load;
            btnExecute.Click += BtnExecute_Click;
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
            numQuantity.ValueChanged += NumQuantity_ValueChanged;
        }

        private void FrmStockIn_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            LoadProducts();
            
            if (cmbStorageZone.Items.Count > 0)
                cmbStorageZone.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            var productList = MemoryStore.Products.ToList();
            
            cmbProduct.SelectedIndexChanged -= CmbProduct_SelectedIndexChanged;
            cmbProduct.DataSource = null;
            cmbProduct.DisplayMember = "Name";
            cmbProduct.ValueMember = "Id";
            cmbProduct.DataSource = productList;
            cmbProduct.SelectedIndex = -1;
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
        }

        /// <summary>
        /// When a product is selected, show its primary serial and preview what item serials will be generated.
        /// </summary>
        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSerialPreview();
            
            // Reload Storage Zones based on selected product category
            cmbStorageZone.DataSource = null;
            if (cmbProduct.SelectedItem is Product product)
            {
                var zones = MemoryStore.StorageZones.Where(z => z.TargetCategoryId == product.CategoryId).ToList();
                cmbStorageZone.DataSource = zones;
                cmbStorageZone.DisplayMember = "Name";
                cmbStorageZone.ValueMember = "Id";
                if (zones.Count > 0)
                    cmbStorageZone.SelectedIndex = 0;

                LoadSuppliersForProduct(product.Id);
            }
        }

        private void LoadSuppliersForProduct(int productId)
        {
            var supplierIds = MemoryStore.ProductSuppliers.Where(ps => ps.ProductId == productId).Select(ps => ps.SupplierId).ToList();
            var suppliers = MemoryStore.Suppliers.Where(s => supplierIds.Contains(s.Id) && s.IsActive).ToList();

            cmbSupplier.DataSource = null;
            cmbSupplier.DisplayMember = "Name";
            cmbSupplier.ValueMember = "Id";
            cmbSupplier.DataSource = suppliers;
            if (suppliers.Count > 0)
                cmbSupplier.SelectedIndex = 0;
            else
                cmbSupplier.SelectedIndex = -1;
        }

        /// <summary>
        /// When quantity changes, regenerate the serial number preview.
        /// </summary>
        private void NumQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateSerialPreview();
        }

        /// <summary>
        /// Shows a preview of the item-level serial numbers that will be auto-generated.
        /// </summary>
        private void UpdateSerialPreview()
        {
            txtSerialNumbers.Clear();

            if (cmbProduct.SelectedIndex == -1 || cmbProduct.SelectedItem == null)
                return;

            var product = (Product)cmbProduct.SelectedItem;
            int qty = (int)numQuantity.Value;

            if (qty <= 0)
            {
                txtSerialNumbers.Text = $"Product Serial: {product.SerialNumber}\r\n\r\n(Enter quantity to preview item serials)";
                return;
            }

            // Calculate the next available index for this product
            int startIndex = MemoryStore.GetNextItemIndex(product.Id);
            var previewSerials = MemoryStore.GenerateItemSerials(product.SerialNumber, startIndex, qty);

            var sb = new StringBuilder();
            sb.AppendLine($"Product Serial: {product.SerialNumber}");
            sb.AppendLine($"Items to be generated ({qty}):");
            sb.AppendLine("─────────────────────────");
            foreach (var serial in previewSerials)
            {
                sb.AppendLine($"  ► {serial}");
            }
            txtSerialNumbers.Text = sb.ToString();
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            bool isValid = true;
            string errorMsg;

            // Supplier
            if (cmbSupplier.SelectedValue == null)
            { _errorProvider.SetError(cmbSupplier, "Please select a supplier."); isValid = false; }
            else
              _errorProvider.SetError(cmbSupplier, string.Empty);

            // Product
            if (cmbProduct.SelectedValue == null)
            { _errorProvider.SetError(cmbProduct, "Please select a product."); isValid = false; }
            else
              _errorProvider.SetError(cmbProduct, string.Empty);

            // Quantity
            int quantity = (int)numQuantity.Value;
            if (quantity <= 0)
            { _errorProvider.SetError(numQuantity, "Quantity must be greater than zero."); isValid = false; }
            else
              _errorProvider.SetError(numQuantity, string.Empty);

            // Order Number
            if (!ValidationHelper.IsRequired(txtOrderNumber.Text, out errorMsg))
            { _errorProvider.SetError(txtOrderNumber, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(txtOrderNumber.Text.Trim(), 2, 50, out errorMsg))
            { _errorProvider.SetError(txtOrderNumber, errorMsg); isValid = false; }
            else
              _errorProvider.SetError(txtOrderNumber, string.Empty);

            // Storage Zone
            if (cmbStorageZone.SelectedValue == null)
            { _errorProvider.SetError(cmbStorageZone, "Please resolve missing Storage Zone."); isValid = false; }
            else
              _errorProvider.SetError(cmbStorageZone, string.Empty);

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Prepare Data
            int productId = (int)cmbProduct.SelectedValue;
            var product   = MemoryStore.Products.FirstOrDefault(p => p.Id == productId);

            string notes = $"PO: {txtOrderNumber.Text.Trim()} | Zone: {((StorageZone)cmbStorageZone.SelectedItem).Name} | Warranty: {txtWarrantyInfo.Text.Trim()}";

            int? warrantyDuration = null;
            if (int.TryParse(txtWarrantyInfo.Text.Trim(), out int duration))
            {
                warrantyDuration = duration;
            }

            // 3. Execute Business Logic (serial numbers are auto-generated in PerformStockMovement)
            bool success = MemoryStore.PerformStockMovement(productId, quantity, "STOCK IN", notes, null, warrantyDuration);

            // 4. Handle Result
            if (success)
            {
                // Show confirmation with generated serials
                var generatedItems = MemoryStore.ProductItems
                    .Where(pi => pi.ProductId == productId && pi.IsInStock)
                    .OrderByDescending(pi => pi.Id)
                    .Take(quantity)
                    .Select(pi => pi.ItemSerialNumber)
                    .ToList();

                string serialInfo = generatedItems.Count > 0
                    ? $"\n\nGenerated Item Serials:\n{string.Join("\n", generatedItems.Take(10))}{(generatedItems.Count > 10 ? $"\n... and {generatedItems.Count - 10} more" : "")}"
                    : "";

                MessageBox.Show($"Stock In operation recorded successfully.{serialInfo}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            else
            {
                MessageBox.Show("Failed to record Stock In operation. Please check product details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            cmbSupplier.SelectedIndex = -1;
            cmbProduct.SelectedIndex = -1;
            txtOrderNumber.Clear();
            numQuantity.Value = 0;
            cmbStorageZone.DataSource = null;
            txtSerialNumbers.Clear();
            txtWarrantyInfo.Clear();
        }
    }
}

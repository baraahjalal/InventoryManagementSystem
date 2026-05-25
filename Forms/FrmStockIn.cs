using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem
{
    public partial class FrmStockIn : Form
    {
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        private int? _preselectedProductId = null;

        public FrmStockIn()
        {
            InitializeComponent();
            this.Load += FrmStockIn_Load;
            btnExecute.Click += BtnExecute_Click;
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
            numQuantity.ValueChanged += NumQuantity_ValueChanged;
        }

        public FrmStockIn(int productId) : this()
        {
            _preselectedProductId = productId;
        }

        private void FrmStockIn_Load(object sender, EventArgs e) => RefreshData();

        public void RefreshData()
        {
            LoadProducts();

            if (_preselectedProductId.HasValue)
                cmbProduct.SelectedValue = _preselectedProductId.Value;

            if (cmbStorageZone.Items.Count > 0)
                cmbStorageZone.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            var products = ProductRepository.GetAll();

            cmbProduct.SelectedIndexChanged -= CmbProduct_SelectedIndexChanged;
            cmbProduct.DataSource    = null;
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember   = "ProductSerialNumber";
            cmbProduct.DataSource    = products;
            cmbProduct.SelectedIndex = -1;
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
        }

        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSerialPreview();

            cmbStorageZone.DataSource = null;
            if (cmbProduct.SelectedItem is Product product)
            {
                var zones = StorageZoneRepository.GetByCategory(product.CategoryID);
                cmbStorageZone.DataSource    = zones;
                cmbStorageZone.DisplayMember = "ZoneName";
                cmbStorageZone.ValueMember   = "ZoneID";
                if (zones.Count > 0) cmbStorageZone.SelectedIndex = 0;

                LoadActiveSuppliers();
            }
        }

        private void LoadActiveSuppliers()
        {
            var suppliers = SupplierRepository.GetActive();
            cmbSupplier.DataSource    = null;
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember   = "SupplierTaxNumber";
            cmbSupplier.DataSource    = suppliers;

            if (suppliers.Count > 0) cmbSupplier.SelectedIndex = 0;
            else                     cmbSupplier.SelectedIndex = -1;
        }

        private void NumQuantity_ValueChanged(object sender, EventArgs e) => UpdateSerialPreview();

        private void UpdateSerialPreview()
        {
            txtSerialNumbers.Clear();
            if (cmbProduct.SelectedIndex == -1 || cmbProduct.SelectedItem == null) return;

            var product = (Product)cmbProduct.SelectedItem;
            int qty     = (int)numQuantity.Value;

            if (qty <= 0)
            {
                txtSerialNumbers.Text = $"Product ID: {product.ProductSerialNumber}\r\n\r\n(Enter quantity to preview item IDs)";
                return;
            }

            int existingCount = ProductItemRepository.CountAll(product.ProductSerialNumber);
            var sb = new StringBuilder();
            sb.AppendLine($"Product ID: {product.ProductSerialNumber}");
            sb.AppendLine($"Items to be generated ({qty}):");
            sb.AppendLine("─────────────────────────");
            sb.AppendLine($"  IDs will be assigned from sequence seq_ProductItems");
            sb.AppendLine($"  (current total existing items: {existingCount})");
            txtSerialNumbers.Text = sb.ToString();
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            bool isValid = true;

            if (cmbSupplier.SelectedValue == null)
            { _errorProvider.SetError(cmbSupplier, "Please select a supplier."); isValid = false; }
            else _errorProvider.SetError(cmbSupplier, string.Empty);

            if (cmbProduct.SelectedValue == null)
            { _errorProvider.SetError(cmbProduct, "Please select a product."); isValid = false; }
            else _errorProvider.SetError(cmbProduct, string.Empty);

            int quantity = (int)numQuantity.Value;
            if (quantity <= 0)
            { _errorProvider.SetError(numQuantity, "Quantity must be greater than zero."); isValid = false; }
            else _errorProvider.SetError(numQuantity, string.Empty);

            if (cmbStorageZone.SelectedValue == null)
            { _errorProvider.SetError(cmbStorageZone, "Please resolve missing Storage Zone."); isValid = false; }
            else _errorProvider.SetError(cmbStorageZone, string.Empty);

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product          = (Product)cmbProduct.SelectedItem;
            var supplier         = cmbSupplier.SelectedItem as Supplier;
            int? supplierTaxNum  = supplier?.SupplierTaxNumber;
            string zoneName      = (cmbStorageZone.SelectedItem as StorageZone)?.ZoneName ?? "";
            int warrantyVal      = (int)numWarrantyMonths.Value;
            int? warranty        = warrantyVal > 0 ? warrantyVal : (int?)null;

            string notes = $"Zone: {zoneName} | Warranty: {warrantyVal} Months";

            var movement = new StockMovement
            {
                ProductSerialNumber         = product.ProductSerialNumber,
                MovementType      = "StockIn",
                QuantityChanged   = quantity,
                EmployeeID        = DatabaseHelper.CurrentUser?.EmployeeID,
                Notes             = notes,
                WarrantyMonths    = warranty,
                SupplierTaxNumber = supplierTaxNum
            };
            int movementId = StockMovementRepository.Add(movement);

            var newItems = new List<ProductItem>();
            for (int i = 0; i < quantity; i++)
                newItems.Add(new ProductItem
                {
                    ProductSerialNumber       = product.ProductSerialNumber,
                    BatchMovementId = movementId
                });
            ProductItemRepository.AddBatch(newItems);

            MessageBox.Show(
                $"Stock In recorded successfully.\n\nGenerated {quantity} item(s) for Product ID [{product.ProductSerialNumber}].",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
        }

        private void ClearForm()
        {
            cmbSupplier.SelectedIndex  = -1;
            cmbProduct.SelectedIndex   = -1;
            numQuantity.Value          = 0;
            cmbStorageZone.DataSource  = null;
            txtSerialNumbers.Clear();
            numWarrantyMonths.Value    = 12;
        }
    }
}

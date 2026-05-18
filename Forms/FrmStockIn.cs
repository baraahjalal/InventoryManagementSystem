using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem
{
    public partial class FrmStockIn : Form
    {
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        private string _preselectedProductSerial = null;

        /// <summary>Standard constructor — no preselection.</summary>
        public FrmStockIn()
        {
            InitializeComponent();
            this.Load += FrmStockIn_Load;
            btnExecute.Click += BtnExecute_Click;
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
            numQuantity.ValueChanged += NumQuantity_ValueChanged;
        }

        /// <summary>Constructor used when launched via right-click from FrmProducts.</summary>
        public FrmStockIn(string productSerial) : this()
        {
            _preselectedProductSerial = productSerial;
        }

        private void FrmStockIn_Load(object sender, EventArgs e) => RefreshData();

        public void RefreshData()
        {
            LoadProducts();

            if (!string.IsNullOrEmpty(_preselectedProductSerial))
                cmbProduct.SelectedValue = _preselectedProductSerial;

            if (cmbStorageZone.Items.Count > 0)
                cmbStorageZone.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            var products = ProductRepository.GetAll();

            cmbProduct.SelectedIndexChanged -= CmbProduct_SelectedIndexChanged;
            cmbProduct.DataSource    = null;
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember   = "SerialNumber";
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
                var zones = StorageZoneRepository.GetByCategory(product.CategoryName);
                cmbStorageZone.DataSource    = zones;
                cmbStorageZone.DisplayMember = "ZoneName";
                cmbStorageZone.ValueMember   = "ZoneName";
                if (zones.Count > 0) cmbStorageZone.SelectedIndex = 0;

                LoadActiveSuppliers();
            }
        }

        private void LoadActiveSuppliers()
        {
            var suppliers = SupplierRepository.GetActive();
            cmbSupplier.DataSource    = null;
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember   = "SupplierName";
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
                txtSerialNumbers.Text = $"Product Serial: {product.SerialNumber}\r\n\r\n(Enter quantity to preview item serials)";
                return;
            }

            // Count existing items to figure out next index
            int existingCount = ProductItemRepository.CountInStock(product.SerialNumber);
            var sb = new StringBuilder();
            sb.AppendLine($"Product Serial: {product.SerialNumber}");
            sb.AppendLine($"Items to be generated ({qty}):");
            sb.AppendLine("─────────────────────────");
            for (int i = 1; i <= qty; i++)
                sb.AppendLine($"  ► {product.SerialNumber}-{(existingCount + i):D2}");
            txtSerialNumbers.Text = sb.ToString();
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            bool isValid = true;
            string errorMsg;

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

            if (!ValidationHelper.IsRequired(txtOrderNumber.Text, out errorMsg))
            { _errorProvider.SetError(txtOrderNumber, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(txtOrderNumber.Text.Trim(), 2, 50, out errorMsg))
            { _errorProvider.SetError(txtOrderNumber, errorMsg); isValid = false; }
            else _errorProvider.SetError(txtOrderNumber, string.Empty);

            if (cmbStorageZone.SelectedValue == null)
            { _errorProvider.SetError(cmbStorageZone, "Please resolve missing Storage Zone."); isValid = false; }
            else _errorProvider.SetError(cmbStorageZone, string.Empty);

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product      = (Product)cmbProduct.SelectedItem;
            string supplier  = cmbSupplier.SelectedValue?.ToString();
            string zoneName  = cmbStorageZone.SelectedValue?.ToString();
            int warrantyVal  = (int)numWarrantyMonths.Value;
            int? warranty    = warrantyVal > 0 ? warrantyVal : (int?)null;

            string notes = $"PO: {txtOrderNumber.Text.Trim()} | Zone: {zoneName} | Warranty: {warrantyVal} Months";

            // 1. Insert StockMovement record
            var movement = new StockMovement
            {
                ProductSerial   = product.SerialNumber,
                MovementType    = "StockIn",
                QuantityChanged = quantity,
                Username        = DatabaseHelper.CurrentUser?.Username,
                Notes           = notes,
                WarrantyMonths  = warranty,
                SupplierName    = supplier
            };
            int movementId = StockMovementRepository.Add(movement);

            // 2. Generate item-level serial numbers and insert into ProductItems
            int existingCount = ProductItemRepository.CountInStock(product.SerialNumber);
            var newItems = new List<ProductItem>();
            for (int i = 1; i <= quantity; i++)
            {
                newItems.Add(new ProductItem
                {
                    ItemSerialNumber = $"{product.SerialNumber}-{(existingCount + i):D2}",
                    ProductSerial    = product.SerialNumber,
                    BatchMovementId  = movementId
                });
            }
            ProductItemRepository.AddBatch(newItems);

            MessageBox.Show(
                $"Stock In recorded successfully.\n\nGenerated {quantity} item(s) for [{product.SerialNumber}].",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
        }

        private void ClearForm()
        {
            cmbSupplier.SelectedIndex  = -1;
            cmbProduct.SelectedIndex   = -1;
            txtOrderNumber.Clear();
            numQuantity.Value          = 0;
            cmbStorageZone.DataSource  = null;
            txtSerialNumbers.Clear();
            numWarrantyMonths.Value    = 12;
        }
    }
}

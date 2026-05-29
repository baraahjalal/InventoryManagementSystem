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
        private int? _preselectedProductId = null;
        private string _movementType = "StockIn";

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

        public FrmStockIn(int productId, string movementType) : this(productId)
        {
            _movementType = movementType;
        }

        private void FrmStockIn_Load(object sender, EventArgs e)
        {
            InitModeToggle();
            RefreshData();
        }

        private void InitModeToggle()
        {
            rbStockIn.Checked  = (_movementType == "StockIn");
            rbRestock.Checked  = (_movementType == "Restock");
            rbStockIn.CheckedChanged  += (s, e) => { if (rbStockIn.Checked)  { ApplyMode("StockIn"); LoadProducts(); } };
            rbRestock.CheckedChanged  += (s, e) => { if (rbRestock.Checked)  { ApplyMode("Restock"); LoadProducts(); } };
            ApplyMode(_movementType);
        }

        private void ApplyMode(string mode)
        {
            _movementType = mode;
            if (mode == "Restock")
            {
                lblHeader.Text    = "Restock Product";
                lblSubHeader.Text = "Replenish existing product stock from a supplier.";
                btnExecute.Text   = "Confirm Restock";
            }
            else
            {
                lblHeader.Text    = "Stock In Receipt";
                lblSubHeader.Text = "Record incoming inventory, supplier details, and physical placements.";
                btnExecute.Text   = "Register Stock Entry";
            }
        }

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

            if (_movementType == "Restock")
            {
                // Only allow restocking products that have been stocked before
                products = products.Where(p => ProductItemRepository.CountAll(p.ProductSerialNumber) > 0).ToList();
            }

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

                if (_movementType == "Restock")
                {
                    var movements = StockMovementRepository.GetByProduct(product.ProductSerialNumber);
                    var lastStockIn = movements.FirstOrDefault(m => m.MovementType == "StockIn" || m.MovementType == "Restock");
                    if (lastStockIn != null && lastStockIn.SupplierTaxNumber.HasValue)
                    {
                        cmbSupplier.SelectedValue = lastStockIn.SupplierTaxNumber.Value;
                    }
                }
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
            int current = ProductItemRepository.CountAll(product.ProductSerialNumber);

            if (qty <= 0)
            {
                txtSerialNumbers.Text = $"Current Stock:   {current} items\r\n\r\n(Enter quantity to see updated total)";
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Product ID:      {product.ProductSerialNumber}");
            sb.AppendLine($"Current Stock:   {current} items");
            sb.AppendLine($"Adding:          +{qty} items");
            sb.AppendLine("─────────────────────");
            sb.AppendLine("New unit serials (ItemID):");
            foreach (int id in ProductItemRepository.PreviewNextItemIds(product.ProductSerialNumber, qty))
                sb.AppendLine($"  ► {id}");
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
                ProductSerialNumber = product.ProductSerialNumber,
                MovementType        = _movementType,
                QuantityChanged     = quantity,
                EmployeeID          = DatabaseHelper.CurrentUser?.EmployeeID,
                Notes               = notes,
                WarrantyMonths      = warranty,
                SupplierTaxNumber   = supplierTaxNum
            };
            var newItems = new List<ProductItem>();
            using (var conn = DAL.DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int movementId = StockMovementRepository.Add(movement, conn, tran);
                        for (int i = 0; i < quantity; i++)
                            newItems.Add(new ProductItem
                            {
                                ProductSerialNumber = product.ProductSerialNumber,
                                BatchMovementId     = movementId
                            });
                        ProductItemRepository.AddBatch(newItems, conn, tran);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        if (ex is InvalidOperationException)
                            MessageBox.Show("تجاوزت الحد الأقصى للوحدات المتاحة لهذا المنتج.",
                                "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                            MessageBox.Show("حدث خطأ غير متوقع أثناء العملية.",
                                "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            string actionLabel = _movementType == "Restock" ? "Restock" : "Stock In";
            MessageBox.Show(
                $"{actionLabel} recorded successfully.\n\nGenerated {quantity} item(s) for Product ID [{product.ProductSerialNumber}].",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
        }

        private void ClearForm()
        {
            cmbSupplier.SelectedIndex  = -1;
            cmbProduct.SelectedIndex   = -1;
            numQuantity.Minimum        = 0;
            numQuantity.Value          = 0;
            cmbStorageZone.DataSource  = null;
            txtSerialNumbers.Clear();
            numWarrantyMonths.Value    = 12;
        }

        private void rbStockIn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbRestock_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

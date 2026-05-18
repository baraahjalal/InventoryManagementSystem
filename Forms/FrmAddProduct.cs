using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Forms
{
    public partial class FrmAddProduct : Form
    {
        private readonly Dictionary<string, ComboBox> _specSelectors = new Dictionary<string, ComboBox>();
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FrmAddProduct()
        {
            InitializeComponent();

            btnSave.Click   -= BtnSave_Click;
            btnSave.Click   += BtnSave_Click;
            btnCancel.Click -= BtnCancel_Click;
            btnCancel.Click += BtnCancel_Click;
            cmbCategory.SelectedIndexChanged -= CmbCategory_SelectedIndexChanged;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;

            txtPrice.KeyPress    += ValidationHelper.AllowOnlyDecimals;
            txtQuantity.KeyPress += ValidationHelper.AllowOnlyDigits;

            LoadCategories();
        }

        private void LoadCategories()
        {
            cmbCategory.BeginUpdate();
            try
            {
                var cats = CategoryRepository.GetAll();
                cmbCategory.DataSource    = cats;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember   = "CategoryName";
                if (cmbCategory.Items.Count > 0)
                    cmbCategory.SelectedIndex = 0;
            }
            finally
            {
                cmbCategory.EndUpdate();
            }
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            flpDynamicSpecs.Controls.Clear();
            _specSelectors.Clear();

            // No CategoryTemplates in new schema — specs are free-form text
            // Load all active suppliers for the combo
            LoadAllActiveSuppliers();
        }

        private void LoadAllActiveSuppliers()
        {
            var suppliers = SupplierRepository.GetActive();
            ((ListBox)clbSuppliers).DataSource    = suppliers;
            ((ListBox)clbSuppliers).DisplayMember = "SupplierName";
            ((ListBox)clbSuppliers).ValueMember   = "SupplierName";
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out var name, out var price, out var qty,
                                out var serial, out var categoryName, out var selectedSuppliers))
                return;

            if (ProductRepository.Exists(serial))
            {
                MessageBox.Show("Serial Number already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build specifications from any dynamic controls added
            var specs = new List<ProductSpecification>();
            foreach (var kvp in _specSelectors)
            {
                string val = kvp.Value.SelectedItem?.ToString();
                if (!string.IsNullOrWhiteSpace(val))
                    specs.Add(new ProductSpecification { ProductSerial = serial, SpecKey = kvp.Key, SpecValue = val });
            }

            var newProd = new Product
            {
                SerialNumber  = serial,
                ProductName   = name,
                Price         = price,
                CategoryName  = categoryName,
                Specifications = specs
            };

            ProductRepository.Add(newProd);

            // If initial quantity > 0, record a StockIn movement and generate items
            if (qty > 0)
            {
                string supplierName = selectedSuppliers.FirstOrDefault();
                var movement = new StockMovement
                {
                    ProductSerial   = serial,
                    MovementType    = "StockIn",
                    QuantityChanged = qty,
                    Username        = DatabaseHelper.CurrentUser?.Username,
                    Notes           = "Initial stock on product creation",
                    SupplierName    = supplierName
                };
                int movementId = StockMovementRepository.Add(movement);

                var items = new List<ProductItem>();
                for (int i = 1; i <= qty; i++)
                    items.Add(new ProductItem
                    {
                        ItemSerialNumber = $"{serial}-{i:D2}",
                        ProductSerial    = serial,
                        BatchMovementId  = movementId
                    });
                ProductItemRepository.AddBatch(items);
            }

            MessageBox.Show("Product created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateInputs(out string name, out decimal price, out int qty,
                                    out string serial, out string categoryName,
                                    out List<string> selectedSuppliers)
        {
            name      = txtName.Text.Trim();
            serial    = txtSerialNumber.Text.Trim();
            categoryName = cmbCategory.SelectedItem is Models.Category c ? c.CategoryName : string.Empty;

            selectedSuppliers = new List<string>();
            foreach (var item in clbSuppliers.CheckedItems)
                if (item is Models.Supplier s) selectedSuppliers.Add(s.SupplierName);

            price = 0m;
            qty   = 0;

            _errorProvider.Clear();
            bool   isValid = true;
            string errorMsg;

            // Name
            if (!ValidationHelper.IsRequired(name, out errorMsg))
            { _errorProvider.SetError(txtName, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(name, 2, 200, out errorMsg))
            { _errorProvider.SetError(txtName, errorMsg); isValid = false; }
            else _errorProvider.SetError(txtName, string.Empty);

            // Serial Number (required)
            if (!ValidationHelper.IsRequired(serial, out errorMsg))
            { _errorProvider.SetError(txtSerialNumber, errorMsg); isValid = false; }
            else _errorProvider.SetError(txtSerialNumber, string.Empty);

            // Category
            if (string.IsNullOrEmpty(categoryName))
            { _errorProvider.SetError(cmbCategory, "Please select a category."); isValid = false; }
            else _errorProvider.SetError(cmbCategory, string.Empty);

            // Price
            string priceText = txtPrice.Text.Trim();
            if (!ValidationHelper.IsRequired(priceText, out errorMsg))
            { _errorProvider.SetError(txtPrice, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidDecimal(priceText, out errorMsg))
            { _errorProvider.SetError(txtPrice, errorMsg); isValid = false; }
            else { price = decimal.Parse(priceText); _errorProvider.SetError(txtPrice, string.Empty); }

            // Quantity (optional — 0 is acceptable)
            string qtyText = txtQuantity.Text.Trim();
            if (string.IsNullOrWhiteSpace(qtyText)) qtyText = "0";
            if (!ValidationHelper.IsValidInteger(qtyText, out errorMsg))
            { _errorProvider.SetError(txtQuantity, errorMsg); isValid = false; }
            else { qty = int.Parse(qtyText); _errorProvider.SetError(txtQuantity, string.Empty); }

            if (!isValid)
                MessageBox.Show("Please correct the highlighted errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return isValid;
        }
    }
}

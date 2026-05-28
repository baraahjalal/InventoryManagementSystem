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
            // ProductSerialNumber is a plain integer entered by the user
            txtSerialNumber.KeyPress += ValidationHelper.AllowOnlyDigits;

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
                cmbCategory.ValueMember   = "CategoryID";
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
            dgvProductSpecs.Rows.Clear();
            if (cmbCategory.SelectedItem is Models.Category cat)
            {
                var keys = CategorySpecTemplateRepository.GetByCategory(cat.CategoryID);
                foreach (var key in keys)
                    dgvProductSpecs.Rows.Add(key, "");
            }
           // LoadAllActiveSuppliers();
        }

        //private void LoadAllActiveSuppliers()
        //{
        //    var suppliers = SupplierRepository.GetActive();
        //    ((ListBox)clbSuppliers).DataSource    = suppliers;
        //    ((ListBox)clbSuppliers).DisplayMember = "SupplierName";
        //    ((ListBox)clbSuppliers).ValueMember   = "SupplierTaxNumber";
        //}

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out var name, out var price, out var qty,
                                out var productId, out var categoryId, out var selectedSupplierTaxNum))
                return;

            if (ProductRepository.Exists(productId))
            {
                MessageBox.Show("Product ID already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var specs = new List<ProductSpecification>();
            foreach (DataGridViewRow row in dgvProductSpecs.Rows)
            {
                if (row.IsNewRow) continue;
                string key = row.Cells["colSpecKey"].Value?.ToString();
                string val = row.Cells["colSpecValue"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(val))
                    specs.Add(new ProductSpecification { ProductSerialNumber = productId, SpecKey = key, SpecValue = val });
            }

            var newProd = new Product
            {
                ProductSerialNumber    = productId,
                ProductName  = name,
                Price        = price,
                CategoryID   = categoryId,
                Specifications = specs
            };

            ProductRepository.Add(newProd);

            if (qty > 0)
            {
                var movement = new StockMovement
                {
                    ProductSerialNumber         = productId,
                    MovementType      = "StockIn",
                    QuantityChanged   = qty,
                    EmployeeID        = DatabaseHelper.CurrentUser?.EmployeeID,
                    Notes             = "Initial stock on product creation",
                    SupplierTaxNumber = selectedSupplierTaxNum
                };
                int movementId = StockMovementRepository.Add(movement);

                var items = new List<ProductItem>();
                for (int i = 0; i < qty; i++)
                    items.Add(new ProductItem
                    {
                        ProductSerialNumber       = productId,
                        BatchMovementId = movementId
                    });
                ProductItemRepository.AddBatch(items);
            }

            MessageBox.Show("Product created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateInputs(out string name, out decimal price, out int qty,
                                    out int productId, out int categoryId,
                                    out int? selectedSupplierTaxNum)
        {
            name     = txtName.Text.Trim();
            categoryId = cmbCategory.SelectedItem is Models.Category c ? c.CategoryID : 0;

            selectedSupplierTaxNum = null;
         

            productId = 0;
            price     = 0m;
            qty       = 0;

            _errorProvider.Clear();
            bool   isValid = true;
            string errorMsg;

            if (!ValidationHelper.IsRequired(name, out errorMsg))
            { _errorProvider.SetError(txtName, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(name, 2, 200, out errorMsg))
            { _errorProvider.SetError(txtName, errorMsg); isValid = false; }
            else _errorProvider.SetError(txtName, string.Empty);

            string idText = txtSerialNumber.Text.Trim();
            if (!ValidationHelper.IsRequired(idText, out errorMsg))
            { _errorProvider.SetError(txtSerialNumber, errorMsg); isValid = false; }
            else if (!int.TryParse(idText, out productId) || productId <= 0)
            { _errorProvider.SetError(txtSerialNumber, "Product ID must be a positive integer."); isValid = false; }
            else _errorProvider.SetError(txtSerialNumber, string.Empty);

            if (categoryId == 0)
            { _errorProvider.SetError(cmbCategory, "Please select a category."); isValid = false; }
            else _errorProvider.SetError(cmbCategory, string.Empty);

            string priceText = txtPrice.Text.Trim();
            if (!ValidationHelper.IsRequired(priceText, out errorMsg))
            { _errorProvider.SetError(txtPrice, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidDecimal(priceText, out errorMsg))
            { _errorProvider.SetError(txtPrice, errorMsg); isValid = false; }
            else { price = decimal.Parse(priceText); _errorProvider.SetError(txtPrice, string.Empty); }

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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace InventoryManagementSystem.Forms
{
    public partial class FrmAddProduct : Form
    {
        private readonly Dictionary<string, ComboBox> _specSelectors = new Dictionary<string, ComboBox>();
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FrmAddProduct()
        {
            InitializeComponent();

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;

            // KeyPress restrictions
            txtPrice.KeyPress    += ValidationHelper.AllowOnlyDecimals;
            txtQuantity.KeyPress += ValidationHelper.AllowOnlyDigits;

            LoadCategories();
        }

        private void LoadCategories()
        {
            cmbCategory.BeginUpdate();
            try
            {
                cmbCategory.DataSource = MemoryStore.Categories.ToList();
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "Id";

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

            if (cmbCategory.SelectedItem is Category selectedCat)
            {
                LoadSuppliersForCategory(selectedCat.Id);

                var template = MemoryStore.CategoryTemplates.FirstOrDefault(t => t.CategoryId == selectedCat.Id);
                if (template == null) return;

                foreach (var filter in template.AvailableFilters)
                {
                    var lbl = new Label
                    {
                        Text = filter.Key + ":",
                        Width = 120,
                        Margin = new Padding(0, 6, 6, 0),
                        ForeColor = Color.FromArgb(100, 100, 100),
                        Font = new Font("Segoe UI", 9.5F)
                    };
                    var cmb = new ComboBox
                    {
                        Width = 150,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Font = new Font("Segoe UI", 9.5F)
                    };
                    foreach (var opt in filter.Value) cmb.Items.Add(opt);
                    if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;

                    var panel = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = false,
                        Margin = new Padding(0, 0, 0, 8)
                    };
                    panel.Controls.Add(lbl);
                    panel.Controls.Add(cmb);
                    flpDynamicSpecs.Controls.Add(panel);

                    _specSelectors.Add(filter.Key, cmb);
                }
            }
        }

        private void LoadSuppliersForCategory(int categoryId)
        {
            var supplierIds = MemoryStore.SupplierCategories.Where(sc => sc.CategoryId == categoryId).Select(sc => sc.SupplierId).ToList();
            var validSuppliers = MemoryStore.Suppliers.Where(s => supplierIds.Contains(s.Id) && s.IsActive).ToList();
            
            ((ListBox)clbSuppliers).DataSource = validSuppliers;
            ((ListBox)clbSuppliers).DisplayMember = "Name";
            ((ListBox)clbSuppliers).ValueMember = "Id";
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out var name, out var price, out var qty, out var serial, out var categoryId, out var selectedSuppliers))
                return;

            // Unique Serial check (if provided)
            if (!string.IsNullOrWhiteSpace(serial))
            {
                bool serialExists = MemoryStore.Products.Any(p =>
                    !string.IsNullOrWhiteSpace(p.SerialNumber) &&
                    p.SerialNumber.Equals(serial, StringComparison.OrdinalIgnoreCase));

                if (serialExists)
                {
                    MessageBox.Show("Serial Number already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var newProd = new Product
            {
                Id = MemoryStore.Products.Count > 0 ? MemoryStore.Products.Max(p => p.Id) + 1 : 1,
                Name = name,
                Price = price,
                Quantity = 0,
                CategoryId = categoryId,
                SerialNumber = serial,
                Specifications = new Dictionary<string, string>()
            };

            foreach (var spec in _specSelectors)
            {
                newProd.Specifications[spec.Key] = spec.Value.SelectedItem?.ToString();
            }

            MemoryStore.Products.Add(newProd);

            foreach (var supId in selectedSuppliers)
            {
                MemoryStore.ProductSuppliers.Add(new ProductSupplier { ProductId = newProd.Id, SupplierId = supId });
            }

            // Apply initial quantity through stock movement (so audit trail is consistent)
            if (qty > 0 && selectedSuppliers.Count > 0)
            {
                MemoryStore.PerformStockMovement(newProd.Id, qty, StockMovementType.StockIn, "Initial stock", null, null, selectedSuppliers.FirstOrDefault());
            }

            MemoryStore.LogAction("PRODUCT CREATED", $"New product created: ID {newProd.Id} - '{newProd.Name}' (Category ID: {newProd.CategoryId}).");

            MessageBox.Show("Product created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateInputs(out string name, out decimal price, out int qty, out string serial, out int categoryId, out List<int> selectedSuppliers)
        {
            name     = txtName.Text.Trim();
            serial   = txtSerialNumber.Text.Trim();
            categoryId = cmbCategory.SelectedItem is Category c ? c.Id : 0;
            
            selectedSuppliers = new List<int>();
            foreach (var checkedItem in clbSuppliers.CheckedItems)
            {
                if (checkedItem is Supplier s) selectedSuppliers.Add(s.Id);
            }
            
            price = 0m;
            qty   = 0;

            _errorProvider.Clear();
            bool isValid = true;
            string errorMsg;

            // Name
            if (!ValidationHelper.IsRequired(name, out errorMsg))
            { _errorProvider.SetError(txtName, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(name, 2, 100, out errorMsg))
            { _errorProvider.SetError(txtName, errorMsg); isValid = false; }
            else
              _errorProvider.SetError(txtName, string.Empty);

            // Category
            if (categoryId == 0)
            { _errorProvider.SetError(cmbCategory, "Please select a category."); isValid = false; }
            else
              _errorProvider.SetError(cmbCategory, string.Empty);

            // Suppliers
            if (selectedSuppliers.Count == 0)
            { _errorProvider.SetError(clbSuppliers, "Please select at least one supplier."); isValid = false; }
            else
              _errorProvider.SetError(clbSuppliers, string.Empty);

            // Price
            string priceText = txtPrice.Text.Trim();
            if (!ValidationHelper.IsRequired(priceText, out errorMsg))
            { _errorProvider.SetError(txtPrice, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidDecimal(priceText, out errorMsg))
            { _errorProvider.SetError(txtPrice, errorMsg); isValid = false; }
            else
            { price = decimal.Parse(priceText); _errorProvider.SetError(txtPrice, string.Empty); }

            // Quantity
            string qtyText = txtQuantity.Text.Trim();
            if (!ValidationHelper.IsRequired(qtyText, out errorMsg))
            { _errorProvider.SetError(txtQuantity, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidInteger(qtyText, out errorMsg))
            { _errorProvider.SetError(txtQuantity, errorMsg); isValid = false; }
            else
            { qty = int.Parse(qtyText); _errorProvider.SetError(txtQuantity, string.Empty); }

            if (!isValid)
                MessageBox.Show("Please correct the highlighted errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return isValid;
        }
    }
}

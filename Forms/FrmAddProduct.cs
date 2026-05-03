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

        public FrmAddProduct()
        {
            InitializeComponent();

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;

            LoadCategories();
        }

        private void LoadCategories()
        {
            cmbCategory.BeginUpdate();
            try
            {
                cmbCategory.Items.Clear();
                foreach (var template in MemoryStore.CategoryTemplates)
                {
                    if (!string.IsNullOrWhiteSpace(template?.CategoryName))
                        cmbCategory.Items.Add(template.CategoryName);
                }

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

            var template = MemoryStore.CategoryTemplates.FirstOrDefault(t => t.CategoryName == cmbCategory.SelectedItem?.ToString());
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
                    Width = flpDynamicSpecs.ClientSize.Width - 20,
                    Height = 32,
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out var name, out var price, out var qty, out var serial, out var category))
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
                Category = category,
                SerialNumber = serial,
                Specifications = new Dictionary<string, string>()
            };

            foreach (var spec in _specSelectors)
            {
                newProd.Specifications[spec.Key] = spec.Value.SelectedItem?.ToString();
            }

            MemoryStore.Products.Add(newProd);

            // Apply initial quantity through stock movement (so audit trail is consistent)
            try
            {
                if (qty > 0)
                    MemoryStore.PerformStockMovement(newProd.Id, qty, "STOCK IN", "Initial stock");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Rollback product add if movement not allowed
                MemoryStore.Products.Remove(newProd);
                MessageBox.Show(ex.Message, "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MemoryStore.LogAction("PRODUCT CREATED", $"New product created: ID {newProd.Id} - '{newProd.Name}' (Category: {newProd.Category}).");

            MessageBox.Show("Product created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateInputs(out string name, out decimal price, out int qty, out string serial, out string category)
        {
            name = txtName.Text.Trim();
            serial = txtSerialNumber.Text.Trim();
            category = cmbCategory.SelectedItem?.ToString();
            price = 0m;
            qty = 0;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Product Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.DroppedDown = true;
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out qty) || qty < 0)
            {
                MessageBox.Show("Please enter a valid initial quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem
{
    public partial class FrmProducts : Form
    {
        private List<Product> _allProducts;
        private Product _selectedProduct;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        private ContextMenuStrip _ctxProductMenu;

        private Dictionary<string, ComboBox> _dynamicFilters = new Dictionary<string, ComboBox>();
        private FlowLayoutPanel _flpDynamicFilters;

        public bool ShowLowStockFilter { get; set; } = false;
        public bool ForceShowAll       { get; set; } = false;

        public FrmProducts()
        {
            InitializeComponent();

            _flpDynamicFilters = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize      = true,
                WrapContents  = false,
                Location      = new System.Drawing.Point(485, 28),
                Height        = 35
            };
            pnlHeader.Controls.Add(_flpDynamicFilters);

            lblProcessor.Visible       = false;
            cmbFilterProcessor.Visible = false;
            lblRAM.Visible             = false;
            cmbFilterRAM.Visible       = false;

            this.Load += FrmProducts_Load;
            dgvProducts.SelectionChanged         += DgvProducts_SelectionChanged;
            btnEdit.Click                        += BtnEdit_Click;
            txtSearch.TextChanged                += FilterData;
            cmbCategory.SelectedIndexChanged     += CmbCategory_SelectedIndexChanged;
            btnAddProduct.Click                  += BtnAddProduct_Click;
            btnClearSelection.Click              += BtnClearSelection_Click;

            InitContextMenu();
            EnableDoubleBuffered(dgvProducts);
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            using (Forms.FrmAddProduct frmAdd = new Forms.FrmAddProduct())
            {
                if (frmAdd.ShowDialog() == DialogResult.OK)
                    RefreshData();
            }
        }

        private void EnableDoubleBuffered(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgv, new object[] { true });
        }

        private void FrmProducts_Load(object sender, EventArgs e)
        {
            txtProdSpec.ReadOnly  = true;
            txtProdSpec.BackColor = System.Drawing.SystemColors.Window;
            txtProdPrice.KeyPress += ValidationHelper.AllowOnlyDecimals;
            LoadCategories();
            RefreshData();
        }

        private void LoadCategories()
        {
            cmbCategory.SelectedIndexChanged -= CmbCategory_SelectedIndexChanged;

            var allItem = new Models.Category { CategoryName = "All Categories" };
            var categories = new List<Models.Category> { allItem };
            categories.AddRange(CategoryRepository.GetAll());

            cmbCategory.DataSource    = null;
            cmbCategory.DataSource    = categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember   = "CategoryName";

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;

            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void RefreshData()
        {
            _allProducts = ProductRepository.GetAll();

            if (ShowLowStockFilter)
            {
                _allProducts    = _allProducts.Where(p => p.Quantity <= 10).ToList();
                ShowLowStockFilter = false;
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            }

            if (ForceShowAll)
            {
                ForceShowAll = false;
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            }

            ApplyFilters();
        }

        private void FilterData(object sender, EventArgs e) => ApplyFilters();

        private void ApplyFilters()
        {
            if (_allProducts == null) return;

            var filtered = _allProducts.AsEnumerable();

            // Category filter
            if (cmbCategory.SelectedItem is Models.Category selectedCat
                && selectedCat.CategoryName != "All Categories")
            {
                filtered = filtered.Where(p => p.CategoryName == selectedCat.CategoryName);
            }

            // Text search
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filtered = filtered.Where(p =>
                    p.ProductName.ToLower().Contains(searchText) ||
                    p.SerialNumber.ToLower().Contains(searchText));
            }

            LoadGridData(filtered.ToList());
        }

        private void LoadGridData(List<Product> productsToDisplay)
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Rows.Clear();

            foreach (var p in productsToDisplay)
            {
                dgvProducts.Rows.Add(
                    p.SerialNumber,
                    p.ProductName,
                    p.CategoryName,
                    p.Quantity.ToString(),
                    $"${p.Price:0.00}",
                    p.StockStatus
                );
            }

            if (productsToDisplay.Count == 0) ClearDetails();
        }

        private void DgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                var row = dgvProducts.SelectedRows[0];
                if (row.Cells["colID"].Value != null)
                {
                    string serial = row.Cells["colID"].Value.ToString();
                    _selectedProduct = _allProducts?.FirstOrDefault(p => p.SerialNumber == serial);
                    ShowProductDetails();
                }
            }
            else
            {
                ClearDetails();
            }
        }

        private void ShowProductDetails()
        {
            if (_selectedProduct == null) return;

            txtProdName.Text  = _selectedProduct.ProductName;
            txtProdPrice.Text = _selectedProduct.Price.ToString("0.00");

            var specs = ProductRepository.GetSpecifications(_selectedProduct.SerialNumber);
            var items = ProductItemRepository.GetAvailable(_selectedProduct.SerialNumber);

            var sb = new StringBuilder();
            sb.AppendLine($"Category: {_selectedProduct.CategoryName}");
            sb.AppendLine($"Product Serial: {_selectedProduct.SerialNumber}");
            sb.AppendLine();

            if (specs.Count > 0)
            {
                foreach (var s in specs)
                    sb.AppendLine($"{s.SpecKey}: {s.SpecValue}");
                sb.AppendLine();
            }

            sb.AppendLine("── Item Tracking ──");
            sb.AppendLine($"In Stock: {items.Count}");

            if (items.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Available Items:");
                foreach (var item in items.Take(15).OrderBy(i => i.ItemSerialNumber))
                    sb.AppendLine($"  ► {item.ItemSerialNumber}");
                if (items.Count > 15)
                    sb.AppendLine($"  ... and {items.Count - 15} more");
            }

            txtProdSpec.Text = sb.ToString();
            btnEdit.Enabled  = true;
        }

        private void ClearDetails()
        {
            _selectedProduct  = null;
            txtProdName.Text  = string.Empty;
            txtProdPrice.Text = string.Empty;
            txtProdSpec.Text  = string.Empty;
            btnEdit.Enabled   = false;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedProduct == null) return;

            _errorProvider.Clear();
            bool isValid = true;
            string errorMsg;

            string nameText = txtProdName.Text.Trim();
            if (!ValidationHelper.IsRequired(nameText, out errorMsg))
            { _errorProvider.SetError(txtProdName, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(nameText, 2, 200, out errorMsg))
            { _errorProvider.SetError(txtProdName, errorMsg); isValid = false; }
            else
              _errorProvider.SetError(txtProdName, string.Empty);

            decimal newPrice = 0;
            string priceText = txtProdPrice.Text.Trim();
            if (!ValidationHelper.IsRequired(priceText, out errorMsg))
            { _errorProvider.SetError(txtProdPrice, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidDecimal(priceText, out errorMsg))
            { _errorProvider.SetError(txtProdPrice, errorMsg); isValid = false; }
            else
            { newPrice = decimal.Parse(priceText); _errorProvider.SetError(txtProdPrice, string.Empty); }

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedProduct.ProductName = nameText;
            _selectedProduct.Price       = newPrice;
            ProductRepository.Update(_selectedProduct);

            MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshData();
        }

        private void pnlGridContainer_Paint(object sender, PaintEventArgs e) { }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            using (var frmAdd = new Forms.FrmAddCategory())
            {
                if (frmAdd.ShowDialog() == DialogResult.OK)
                {
                    LoadCategories();
                    var cats = (List<Models.Category>)cmbCategory.DataSource;
                    var newCat = cats?.FirstOrDefault(c =>
                        c.CategoryName.Equals(frmAdd.CreatedCategoryName, StringComparison.OrdinalIgnoreCase));
                    if (newCat != null)
                        cmbCategory.SelectedItem = newCat;

                    MessageBox.Show($"Category '{frmAdd.CreatedCategoryName}' added successfully.", "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnClearSelection_Click(object sender, EventArgs e)
        {
            txtProdName.Text  = string.Empty;
            txtProdPrice.Text = string.Empty;
            txtProdSpec.Text  = string.Empty;
            txtSearch.Text    = string.Empty;
            dgvProducts.ClearSelection();
            _selectedProduct  = null;
            btnEdit.Enabled   = false;
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
        }

        // ── Context Menu ────────────────────────────────────────────────
        private void InitContextMenu()
        {
            _ctxProductMenu      = new ContextMenuStrip();
            _ctxProductMenu.Font = new System.Drawing.Font("Segoe UI", 9.5F);

            var mnuStockIn  = new ToolStripMenuItem("📦   Perform Stock IN");
            var mnuStockOut = new ToolStripMenuItem("📤   Perform Stock OUT");

            mnuStockIn.Click  += (s, e) => OpenStockForm(isStockIn: true);
            mnuStockOut.Click += (s, e) => OpenStockForm(isStockIn: false);

            _ctxProductMenu.Items.AddRange(new ToolStripItem[] { mnuStockIn, new ToolStripSeparator(), mnuStockOut });
            dgvProducts.CellMouseDown += DgvProducts_CellMouseDown;
        }

        private void DgvProducts_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right) return;
            if (e.RowIndex < 0) return;

            dgvProducts.ClearSelection();
            dgvProducts.Rows[e.RowIndex].Selected = true;

            int safeCol = e.ColumnIndex >= 0 ? e.ColumnIndex : 0;
            dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[safeCol];

            if (_selectedProduct == null) return;
            _ctxProductMenu.Show(dgvProducts, dgvProducts.PointToClient(System.Windows.Forms.Cursor.Position));
        }

        private void OpenStockForm(bool isStockIn)
        {
            if (_selectedProduct == null) return;
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain == null) return;

            string serial = _selectedProduct.SerialNumber;
            Form stockForm = isStockIn
                ? (Form)new FrmStockIn(serial)
                : (Form)new FrmStockOut(serial);

            frmMain.OpenChildForm(stockForm);
        }
    }
}

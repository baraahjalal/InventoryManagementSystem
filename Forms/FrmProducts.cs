using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace InventoryManagementSystem
{
    public partial class FrmProducts : Form
    {
        private List<Product> _allProducts;
        private Product _selectedProduct;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        private ContextMenuStrip _ctxProductMenu;

        // Holds our newly created dynamic combos mapped to their filter name
        private Dictionary<string, ComboBox> _dynamicFilters = new Dictionary<string, ComboBox>();
        private FlowLayoutPanel _flpDynamicFilters;

        // Public flag to request the form show only low-stock items when opened
        public bool ShowLowStockFilter { get; set; } = false;

        // Public flag to force showing all products (used by Total Products click)
        public bool ForceShowAll { get; set; } = false;

        public FrmProducts()
        {
            InitializeComponent();

            // Initialize the FlowLayoutPanel for dynamic filters
            _flpDynamicFilters = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Location = new System.Drawing.Point(485, 28),
                Height = 35
            };
            pnlHeader.Controls.Add(_flpDynamicFilters);

            // Hide old static filters to make room for the dynamic ones
            lblProcessor.Visible = false;
            cmbFilterProcessor.Visible = false;
            lblRAM.Visible = false;
            cmbFilterRAM.Visible = false;

            this.Load += FrmProducts_Load;

            // UI Events
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;
            btnEdit.Click += BtnEdit_Click;
            txtSearch.TextChanged += FilterData;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;
            btnAddProduct.Click += BtnAddProduct_Click;
            btnClearSelection.Click += BtnClearSelection_Click;

            InitContextMenu();
            EnableDoubleBuffered(dgvProducts);
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            using (Forms.FrmAddProduct frmAddProduct = new Forms.FrmAddProduct())
            {
                if (frmAddProduct.ShowDialog() == DialogResult.OK)
                {
                    RefreshData();
                }
            }
        }

        private void EnableDoubleBuffered(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgv,
                new object[] { true }
            );
        }

        private void FrmProducts_Load(object sender, EventArgs e)
        {
            // Load Dynamic Categories
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");
            foreach (var template in MemoryStore.CategoryTemplates)
            {
                cmbCategory.Items.Add(MemoryStore.Categories.FirstOrDefault(c => c.Id == template.CategoryId)?.Name ?? "");
            }

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0; // Default to "All Categories"

            // KeyPress restriction on price edit field
            txtProdPrice.KeyPress += ValidationHelper.AllowOnlyDecimals;

            RefreshData();
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateDynamicFilters(cmbCategory.SelectedItem?.ToString());
            ApplyFilters();
        }

        private void GenerateDynamicFilters(string selectedCategory)
        {
            _flpDynamicFilters.Controls.Clear();
            _dynamicFilters.Clear();

            var template = MemoryStore.CategoryTemplates.FirstOrDefault(t => MemoryStore.Categories.FirstOrDefault(c => c.Id == t.CategoryId)?.Name == selectedCategory);

            if (template == null) return; // "All Categories" or category without filters

            foreach (var filter in template.AvailableFilters)
            {
                // Create Label
                Label lbl = new Label
                {
                    Text = filter.Key + ":",
                    AutoSize = true,
                    Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.White,
                    Margin = new Padding(0, 3, 6, 0)
                };

                // Create ComboBox
                ComboBox cmb = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new System.Drawing.Font("Segoe UI", 9.5F),
                    Width = 110, // Reduced width to prevent overlapping the search box
                    Margin = new Padding(0, 0, 15, 0) // Reduced margin to ensure filters fit when sidebar opens
                };

                cmb.Items.Add("All");
                foreach (var option in filter.Value) cmb.Items.Add(option);
                cmb.SelectedIndex = 0;

                // When changed -> Trigger global filtering
                cmb.SelectedIndexChanged += FilterData;

                _flpDynamicFilters.Controls.Add(lbl);
                _flpDynamicFilters.Controls.Add(cmb);
                _dynamicFilters.Add(filter.Key, cmb); // Save for access when filtering
            }
        }

        /// <summary>
        /// Retrieves the latest products from the MemoryStore and displays them.
        /// </summary
        private void RefreshData()
        {
            _allProducts = MemoryStore.Products.ToList();

            // If this form was requested to show only low-stock items, apply that filter immediately
            if (ShowLowStockFilter)
            {
                _allProducts = _allProducts.Where(p => p.Quantity <= 10).ToList();
                // Reset flag so subsequent refreshes behave normally
                ShowLowStockFilter = false;
                cmbCategory.SelectedIndex = 0; // set category to All to avoid hiding items
                GenerateDynamicFilters(null);
            }

            // If this form was requested to force showing all products, ignore other filters
            if (ForceShowAll)
            {
                // Ensure category is set to All and dynamic filters cleared so all products are visible
                ForceShowAll = false; // reset immediately
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
                GenerateDynamicFilters(null);
                // _allProducts already contains full list from MemoryStore
            }

            ApplyFilters();
        }

        /// <summary>
        /// Applies search and dropdown filters to the loaded product list
        /// </summary>
        private void FilterData(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (_allProducts == null) return;

            var filteredList = _allProducts.AsEnumerable();

            // 1. Static Category Filter
            string selectedCategory = cmbCategory.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All Categories")
            {
                var catId = MemoryStore.Categories.FirstOrDefault(c => c.Name.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))?.Id;
                if (catId.HasValue)
                {
                    filteredList = filteredList.Where(p => p.CategoryId == catId.Value);
                }
            }

            // 2. Dynamic Attribute Filters
            foreach (var dynamicFilter in _dynamicFilters)
            {
                string filterName = dynamicFilter.Key;
                if (dynamicFilter.Value.SelectedItem == null) continue;

                string selectedVal = dynamicFilter.Value.SelectedItem.ToString();

                if (selectedVal != "All")
                {
                    filteredList = filteredList.Where(p =>
                        p.Specifications.ContainsKey(filterName) &&
                        p.Specifications[filterName].Equals(selectedVal, StringComparison.OrdinalIgnoreCase)
                    );
                }
            }

            // 3. Text Search Filter (Name or ID)
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredList = filteredList.Where(p => p.Name.ToLower().Contains(searchText) || p.Id.ToString().Contains(searchText));
            }

            LoadGridData(filteredList.ToList());
        }

        private void LoadGridData(List<Product> productsToDisplay)
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Rows.Clear();

            foreach (var product in productsToDisplay)
            {
                dgvProducts.Rows.Add(
                    product.Id.ToString(),
                    product.Name,
                    MemoryStore.Categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name ?? "",
                    product.Quantity.ToString(),
                    $"${product.Price:0.00}",
                    product.Status
                );
            }

            // Clear selection details if list is empty
            if (productsToDisplay.Count == 0)
                ClearDetails();
        }

        private void DgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                // Retrieve the configured ID mapped to the 0th cell
                int prodId;
                if (int.TryParse(dgvProducts.SelectedRows[0].Cells["colID"].Value.ToString(), out prodId))
                {
                    _selectedProduct = MemoryStore.Products.FirstOrDefault(p => p.Id == prodId);
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
            if (_selectedProduct != null)
            {
                txtProdName.Text = _selectedProduct.Name;
                txtProdPrice.Text = _selectedProduct.Price.ToString("0.00");

                // Generate specs list
                var specDisplay = string.Join("\r\n", _selectedProduct.Specifications.Select(x => $"{x.Key}: {x.Value}"));

                // Get item-level serial numbers from ProductItems store
                var availableItems = MemoryStore.GetAvailableItems(_selectedProduct.Id);
                var allItems = MemoryStore.ProductItems.Where(pi => pi.ProductId == _selectedProduct.Id).ToList();

                var sb = new System.Text.StringBuilder();
                sb.AppendLine($"Category: {MemoryStore.Categories.FirstOrDefault(c => c.Id == _selectedProduct.CategoryId)?.Name ?? ""}");
                sb.AppendLine($"Product Serial: {_selectedProduct.SerialNumber}");
                sb.AppendLine($"Supplier ID: {string.Join(", ", MemoryStore.ProductSuppliers.Where(ps => ps.ProductId == _selectedProduct.Id).Select(ps => ps.SupplierId))}");
                sb.AppendLine();
                sb.AppendLine(specDisplay);
                sb.AppendLine();
                sb.AppendLine($"── Item Tracking ──");
                sb.AppendLine($"Total Items: {allItems.Count} | In Stock: {availableItems.Count} | Dispatched: {allItems.Count - availableItems.Count}");
                
                if (availableItems.Count > 0)
                {
                    sb.AppendLine();
                    sb.AppendLine("Available Items:");
                    foreach (var item in availableItems.Take(15).OrderBy(i => i.ItemSerialNumber))
                    {
                        sb.AppendLine($"  ► {item.ItemSerialNumber}");
                    }
                    if (availableItems.Count > 15)
                    {
                        sb.AppendLine($"  ... and {availableItems.Count - 15} more");
                    }
                }

                txtProdSpec.Text = sb.ToString();

                btnEdit.Enabled = true;
            }
        }

        private void ClearDetails()
        {
            _selectedProduct = null;
            txtProdName.Text = string.Empty;
            txtProdPrice.Text = string.Empty;
            txtProdSpec.Text = string.Empty;
            btnEdit.Enabled = false;
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
            else if (!ValidationHelper.IsValidLength(nameText, 2, 100, out errorMsg))
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

            // Update in MemoryStore
            _selectedProduct.Name  = nameText;
            _selectedProduct.Price = newPrice;

            MemoryStore.LogAction("PRODUCT UPDATED", $"Details updated for Product ID {_selectedProduct.Id} - {_selectedProduct.Name}.");

            MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Re-render DataGrid
            RefreshData();
        }

        private void pnlGridContainer_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            // Placeholder interaction setup matching existing framework
            using (var frmAdd = new Forms.FrmAddCategory())
            {
                if (frmAdd.ShowDialog() == DialogResult.OK)
                {
                    string newCategory = frmAdd.CreatedCategoryName;
                    var newFilters = frmAdd.CreatedFilters;

                    // Add template to memory store if it doesn't exist
                    var catId = MemoryStore.Categories.FirstOrDefault(c => c.Name.Equals(newCategory, StringComparison.OrdinalIgnoreCase))?.Id;
                    if (catId == null)
                    {
                        int newCatId = MemoryStore.Categories.Count > 0 ? MemoryStore.Categories.Max(c => c.Id) + 1 : 1;
                        MemoryStore.Categories.Add(new Category { Id = newCatId, Name = newCategory });
                        catId = newCatId;
                    }

                    if (!MemoryStore.CategoryTemplates.Any(t => t.CategoryId == catId.Value))
                    {
                        MemoryStore.CategoryTemplates.Add(new CategoryTemplate
                        {
                            CategoryId = catId.Value,
                            AvailableFilters = newFilters
                        });
                    }

                    if (!cmbCategory.Items.Contains(newCategory))
                    {
                        cmbCategory.Items.Add(newCategory);
                    }

                    MessageBox.Show($"Category '{newCategory}' with {newFilters.Count} filters added successfully.", "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnClearSelection_Click(object sender, EventArgs e)
        {
            // Clear all textboxes
            txtProdName.Text = string.Empty;
            txtProdPrice.Text = string.Empty;
            txtProdSpec.Text = string.Empty;
            txtSearch.Text = string.Empty;

            // Clear DataGridView selection
            dgvProducts.ClearSelection();
            _selectedProduct = null;
            btnEdit.Enabled = false;

            // Reset static category filter
            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;

            // Reset all dynamic filters to 'All'
            foreach (var cmb in _dynamicFilters.Values)
            {
                if (cmb.Items.Count > 0)
                    cmb.SelectedIndex = 0;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Right-Click Context Menu – Stock IN / OUT shortcut from product list
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Builds the ContextMenuStrip programmatically and wires CellMouseDown.
        /// Kept out of the Designer file intentionally to avoid .resx churn.
        /// </summary>
        private void InitContextMenu()
        {
            _ctxProductMenu = new ContextMenuStrip();
            _ctxProductMenu.Font = new System.Drawing.Font("Segoe UI", 9.5F);

            var mnuStockIn  = new ToolStripMenuItem("📦   Perform Stock IN");
            var mnuStockOut = new ToolStripMenuItem("📤   Perform Stock OUT");

            mnuStockIn.Click  += (s, e) => OpenStockForm(isStockIn: true);
            mnuStockOut.Click += (s, e) => OpenStockForm(isStockIn: false);

            _ctxProductMenu.Items.AddRange(new ToolStripItem[] { mnuStockIn, new ToolStripSeparator(), mnuStockOut });

            dgvProducts.CellMouseDown += DgvProducts_CellMouseDown;
        }

        /// <summary>
        /// Selects the right-clicked row and shows the context menu.
        /// Clicking the header row (RowIndex &lt; 0) is ignored.
        /// </summary>
        private void DgvProducts_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (e.RowIndex < 0) return; // header row – do nothing

            // Programmatically select the right-clicked row so _selectedProduct is populated
            dgvProducts.ClearSelection();
            dgvProducts.Rows[e.RowIndex].Selected = true;

            int safeColumnIndex = e.ColumnIndex >= 0 ? e.ColumnIndex : 0;
            dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[safeColumnIndex];

            // Guard: only show menu when a valid product row is selected
            if (_selectedProduct == null) return;

            _ctxProductMenu.Show(dgvProducts, dgvProducts.PointToClient(Cursor.Position));
        }

        /// <summary>
        /// Opens FrmStockIn or FrmStockOut with the currently selected product
        /// pre-selected in the form's product ComboBox.
        /// Uses FrmMain.OpenChildForm so the target form is embedded inside
        /// pnlMainContent — exactly the same way sidebar navigation works.
        /// </summary>
        private void OpenStockForm(bool isStockIn)
        {
            if (_selectedProduct == null) return;

            // Walk up to FrmMain (this form is itself a child embedded inside it)
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain == null) return;

            int productId = _selectedProduct.Id;

            Form stockForm = isStockIn
                ? (Form)new FrmStockIn(productId)
                : (Form)new FrmStockOut(productId);

            frmMain.OpenChildForm(stockForm);
        }

    }
}

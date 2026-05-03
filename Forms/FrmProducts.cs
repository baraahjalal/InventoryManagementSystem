using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace InventoryManagementSystem
{
    public partial class FrmProducts : Form
    {
        private List<Product> _allProducts;
        private Product _selectedProduct;

        // Holds our newly created dynamic combos mapped to their filter name
        private Dictionary<string, ComboBox> _dynamicFilters = new Dictionary<string, ComboBox>();
        private FlowLayoutPanel _flpDynamicFilters;

        private const int DynamicFiltersRightPadding = 20;
        private const int DynamicFiltersTop = 20;
        private const int DynamicFiltersHeight = 50;
        private const int DynamicFiltersGapFromSearch = 12;

        // Theme colors local to this form (do not affect other forms)
        private static class Theme
        {
            // Base primary (user requested)
            public static readonly Color Base = Color.FromArgb(15, 23, 42); // #0F172A
            // Header / Top Bar (vivid, attention-grabbing, complements base)
            public static readonly Color Header = Color.FromArgb(37, 99, 235); // #2563EB
            // Secondary / muted text
            public static readonly Color Secondary = Color.FromArgb(100, 116, 139); // #64748B
            // Page background
            public static readonly Color PageBackground = Color.FromArgb(248, 250, 252); // #F8FAFC
            // Card background
            public static readonly Color CardBackground = Color.White;
            // Borders / dividers
            public static readonly Color Divider = Color.FromArgb(230, 234, 240); // #E6EAF0
            // Success / Danger
            public static readonly Color Success = Color.FromArgb(16, 185, 129); // #10B981
            public static readonly Color Danger = Color.FromArgb(239, 68, 68); // #EF4444
            // Grid header accent
            public static readonly Color GridHeader = Color.FromArgb(30, 64, 175); // #1E40AF
        }

        // Public flag to request the form show only low-stock items when opened
        public bool ShowLowStockFilter { get; set; } = false;

        // Public flag to force showing all products (used by Total Products click)
        public bool ForceShowAll { get; set; } = false;

        public FrmProducts()
        {
            InitializeComponent();
            SetupDynamicFilterContainer();

            // Apply theme to controls in this form only
            ApplyTheme();

            this.Load += FrmProducts_Load;

            // UI Events
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;
            btnEdit.Click += BtnEdit_Click;
            txtSearch.TextChanged += FilterData;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;
            btnAddProduct.Click += BtnAddProduct_Click;
            btnClearSelection.Click += BtnClearSelection_Click;

            pnlHeader.Resize += (s, e) => LayoutHeader();

            EnableDoubleBuffered(dgvProducts);
        }

        private void ApplyTheme()
        {
            // Overall backgrounds
            this.BackColor = Theme.PageBackground;

            // Header
            if (pnlHeader != null)
            {
                pnlHeader.BackColor = Theme.Header; // eye-catching header
                try { pnlHeader.Padding = new Padding(24, 18, 24, 0); } catch { }
            }

            // Titles
            if (lblMainTitle != null)
            {
                lblMainTitle.ForeColor = Theme.Base;
                lblMainTitle.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            }
            if (lblSubTitle != null)
            {
                lblSubTitle.ForeColor = Theme.Secondary;
                lblSubTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            }

            // Buttons - square, consistent
            if (btnAddProduct != null)
            {
                btnAddProduct.FlatStyle = FlatStyle.Flat;
                btnAddProduct.FlatAppearance.BorderSize = 0;
                btnAddProduct.BackColor = Theme.Base;
                btnAddProduct.ForeColor = Color.White;
                btnAddProduct.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btnAddProduct.Size = new Size(260, 40);
            }

            if (btnEdit != null)
            {
                btnEdit.FlatStyle = FlatStyle.Flat;
                btnEdit.FlatAppearance.BorderSize = 0;
                btnEdit.BackColor = Theme.Base;
                btnEdit.ForeColor = Color.White;
                btnEdit.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                btnEdit.Size = new Size(274, 40);
            }

            if (btnClearSelection != null)
            {
                btnClearSelection.FlatStyle = FlatStyle.Flat;
                btnClearSelection.FlatAppearance.BorderSize = 1;
                btnClearSelection.FlatAppearance.BorderColor = Theme.Base;
                btnClearSelection.BackColor = Color.White;
                btnClearSelection.ForeColor = Theme.Base;
                btnClearSelection.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                btnClearSelection.Size = new Size(120, 28);
            }

            if (btnAddCategory != null)
            {
                btnAddCategory.FlatStyle = FlatStyle.Flat;
                btnAddCategory.FlatAppearance.BorderSize = 1;
                btnAddCategory.FlatAppearance.BorderColor = Theme.Divider;
                btnAddCategory.BackColor = Color.White;
                btnAddCategory.ForeColor = Theme.Base;
                btnAddCategory.Size = new Size(60, 28);
            }

            // Category combo and search box styling
            if (cmbCategory != null)
            {
                cmbCategory.BackColor = Color.White;
                cmbCategory.ForeColor = Theme.Base;
                cmbCategory.FlatStyle = FlatStyle.Flat;
            }
            if (txtSearch != null)
            {
                txtSearch.BackColor = Color.White;
                txtSearch.ForeColor = Theme.Base;
                txtSearch.BorderStyle = BorderStyle.FixedSingle;
                txtSearch.Font = new Font("Segoe UI", 9.5F);
            }
            if (lblSearch != null) lblSearch.ForeColor = Color.FromArgb(255, 255, 255);

            // Details panel
            if (pnlDetails != null)
            {
                pnlDetails.BackColor = Theme.CardBackground;
                pnlDetails.Padding = new Padding(20);
                pnlDetails.BorderStyle = BorderStyle.FixedSingle;
            }
            if (lblDetailsTitle != null)
            {
                lblDetailsTitle.ForeColor = Theme.Base;
                lblDetailsTitle.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            }

            // DataGridView styling
            if (dgvProducts != null)
            {
                dgvProducts.EnableHeadersVisualStyles = false;
                dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Theme.GridHeader;
                dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
                dgvProducts.DefaultCellStyle.BackColor = Color.White;
                dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
                dgvProducts.GridColor = Theme.Divider;
                dgvProducts.DefaultCellStyle.SelectionBackColor = Theme.Header;
                dgvProducts.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvProducts.RowTemplate.Height = 42;
            }

            // Labels and other text
            foreach (Control c in this.Controls)
            {
                if (c is Label lbl)
                {
                    if (lbl == lblMainTitle || lbl == lblSubTitle || lbl == lblDetailsTitle) continue;
                    lbl.ForeColor = Theme.Base;
                }
            }

            // Dynamic filters container background should be transparent to show header
            if (_flpDynamicFilters != null)
            {
                _flpDynamicFilters.BackColor = Color.Transparent;
            }

            try { dgvProducts.ClearSelection(); } catch { }
        }

        private void SetupDynamicFilterContainer()
        {
            // Hide the old hardcoded controls safely if they exist
            if (lblProcessor != null) lblProcessor.Visible = false;
            if (cmbFilterProcessor != null) cmbFilterProcessor.Visible = false;
            if (lblRAM != null) lblRAM.Visible = false;
            if (cmbFilterRAM != null) cmbFilterRAM.Visible = false;

            // Create a FlowLayoutPanel to automatically arrange our new dynamic filters cleanly
            _flpDynamicFilters = new FlowLayoutPanel
            {
                Location = new Point(0, DynamicFiltersTop),
                Size = new Size(10, DynamicFiltersHeight),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent,
                WrapContents = false,
                AutoScroll = true
            };
            this.pnlHeader.Controls.Add(_flpDynamicFilters);

            LayoutHeader();

            // Make sure the action button always stays visible above dynamic controls
            btnAddProduct.BringToFront();
        }

        private void LayoutHeader()
        {
            if (_flpDynamicFilters == null || pnlHeader == null || txtSearch == null) return;

            // Reserve space for the search label + textbox, and keep filters to its left.
            int rightEdge = txtSearch.Left - DynamicFiltersGapFromSearch;
            int leftEdge = btnAddProduct.Right + 10;

            int width = rightEdge - leftEdge;
            if (width < 0) width = 0;

            _flpDynamicFilters.SuspendLayout();
            try
            {
                _flpDynamicFilters.Location = new Point(leftEdge, DynamicFiltersTop);
                _flpDynamicFilters.Size = new Size(width, DynamicFiltersHeight);
            }
            finally
            {
                _flpDynamicFilters.ResumeLayout();
            }

            btnAddProduct.BringToFront();
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
                cmbCategory.Items.Add(template.CategoryName);
            }

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0; // Default to "All Categories"

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

            var template = MemoryStore.CategoryTemplates.FirstOrDefault(t => t.CategoryName == selectedCategory);

            if (template == null) return; // "All Categories" or category without filters

            foreach (var filter in template.AvailableFilters)
            {
                // Create Label
                Label lbl = new Label
                {
                    Text = filter.Key + ":",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9.5F),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    Margin = new Padding(10, 8, 3, 0)
                };

                // Create ComboBox
                ComboBox cmb = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9.5F),
                    Width = 120,
                    Margin = new Padding(3, 5, 10, 0)
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
        /// </summary>
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
                filteredList = filteredList.Where(p => p.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase));
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
                    product.Category,
                    product.Quantity.ToString(),
                    $"${product.Price:0.00}",
                    product.Status
                );
            }

            // Remove selection formatting issue
            dgvProducts.CellFormatting -= DgvProducts_CellFormatting;
            dgvProducts.CellFormatting += DgvProducts_CellFormatting;

            // Clear selection details if list is empty
            if (productsToDisplay.Count == 0)
                ClearDetails();
        }

        private void DgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null && dgvProducts.Columns[e.ColumnIndex].Name == "colStatus")
            {
                string status = e.Value.ToString();

                if (status == "Low Stock")
                    e.CellStyle.ForeColor = Color.DarkOrange;
                else if (status == "Out of Stock")
                    e.CellStyle.ForeColor = Color.Red;
                else
                    e.CellStyle.ForeColor = Color.ForestGreen;

                e.CellStyle.Font = new Font(dgvProducts.Font, FontStyle.Bold);
            }
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
                txtProdSpec.Text = $"Category: {_selectedProduct.Category}\r\nSerial No: {_selectedProduct.SerialNumber}\r\nSupplier ID: {_selectedProduct.SupplierId}\r\n\r\n{specDisplay}";

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

            // Simple validation
            if (string.IsNullOrWhiteSpace(txtProdName.Text))
            {
                MessageBox.Show("Product Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtProdPrice.Text, out decimal newPrice))
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update in MemoryStore
            _selectedProduct.Name = txtProdName.Text.Trim();
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
                    if (!MemoryStore.CategoryTemplates.Any(t => t.CategoryName.Equals(newCategory, StringComparison.OrdinalIgnoreCase)))
                    {
                        MemoryStore.CategoryTemplates.Add(new CategoryTemplate
                        {
                            CategoryName = newCategory,
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Add shadow to details panel for card effect
            pnlDetails.Paint += (s, pe) =>
            {
                using (var shadow = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    shadow.AddRectangle(new Rectangle(5, 5, pnlDetails.Width - 10, pnlDetails.Height - 10));
                    pe.Graphics.FillPath(new SolidBrush(Color.FromArgb(30, 0, 0, 0)), shadow);
                }
            };
            // Add icon to search box (optional, if you have resources)
            // txtSearch.PlaceholderText = "Search by name or ID..."; // For .NET6+, otherwise skip
        }
    }
}
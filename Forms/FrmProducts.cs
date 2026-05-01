using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

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

        public FrmProducts()
        {
            InitializeComponent();
            SetupDynamicFilterContainer();

            this.Load += FrmProducts_Load;

            // UI Events
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;
            btnEdit.Click += BtnEdit_Click;
            txtSearch.TextChanged += FilterData;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;
            btnAddProduct.Click += BtnAddProduct_Click;

            pnlHeader.Resize += (s, e) => LayoutHeader();

            EnableDoubleBuffered(dgvProducts);
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
    }
}
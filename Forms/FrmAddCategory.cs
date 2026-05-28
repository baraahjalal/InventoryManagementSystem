using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace InventoryManagementSystem.Forms
{
    public partial class FrmAddCategory : Form
    {
        public string CreatedCategoryName { get; private set; }

        // Dictionary to store "Filter Name" -> "List of Filter Values"
        public Dictionary<string, List<string>> CreatedFilters { get; private set; }

        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FrmAddCategory()
        {
            InitializeComponent();
            CreatedFilters = new Dictionary<string, List<string>>();

            // Set text boxes placeholders using extension method or handled manually
            txtFilterName.Text = "e.g. RAM";
            txtFilterName.ForeColor = Color.LightGray;
            txtFilterName.Enter += RemovePlaceholder;
            txtFilterName.Leave += SetPlaceholderName;

            txtFilterValues.Text = "e.g. 8GB, 16GB, 32GB";
            txtFilterValues.ForeColor = Color.LightGray;
            txtFilterValues.Enter += RemovePlaceholder;
            txtFilterValues.Leave += SetPlaceholderValues;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && (tb.Text == "e.g. RAM" || tb.Text == "e.g. 8GB, 16GB, 32GB"))
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void SetPlaceholderName(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterName.Text))
            {
                txtFilterName.Text = "e.g. RAM";
                txtFilterName.ForeColor = Color.LightGray;
            }
        }

        private void SetPlaceholderValues(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterValues.Text))
            {
                txtFilterValues.Text = "e.g. 8GB, 16GB, 32GB";
                txtFilterValues.ForeColor = Color.LightGray;
            }
        }

        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            string fName   = txtFilterName.Text.Trim();
            string fValues = txtFilterValues.Text.Trim();
            bool hasError  = false;

            if (string.IsNullOrWhiteSpace(fName) || fName == "e.g. RAM")
            { _errorProvider.SetError(txtFilterName, "Filter name is required."); hasError = true; }
            else
              _errorProvider.SetError(txtFilterName, string.Empty);

            if (string.IsNullOrWhiteSpace(fValues) || fValues == "e.g. 8GB, 16GB, 32GB")
            { _errorProvider.SetError(txtFilterValues, "Filter values are required (comma-separated)."); hasError = true; }
            else
              _errorProvider.SetError(txtFilterValues, string.Empty);

            if (hasError)
            {
                MessageBox.Show("Please enter both a valid Filter Name and valid Values.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if filter already added
            foreach (DataGridViewRow row in dgvFilters.Rows)
            {
                if (row.Cells["colFilterName"].Value.ToString().Equals(fName, StringComparison.OrdinalIgnoreCase))
                {
                    _errorProvider.SetError(txtFilterName, "This filter name is already added.");
                    MessageBox.Show("This filter name is already added.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Add to grid
            dgvFilters.Rows.Add(fName, fValues);

            // Reset Fields
            txtFilterName.Text  = "";
            txtFilterValues.Text = "";
            SetPlaceholderName(txtFilterName, null);
            SetPlaceholderValues(txtFilterValues, null);
        }

        private void dgvFilters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle "Remove" Button click
            if (e.RowIndex >= 0 && dgvFilters.Columns[e.ColumnIndex].Name == "colAction")
            {
                dgvFilters.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            bool isValid = true;
            string errorMsg;

            string catName = txtCategoryName.Text.Trim();

            // ✅ Validation
            if (!ValidationHelper.IsRequired(catName, out errorMsg))
            {
                _errorProvider.SetError(txtCategoryName, errorMsg);
                isValid = false;
            }
            else if (!ValidationHelper.IsValidLength(catName, 2, 50, out errorMsg))
            {
                _errorProvider.SetError(txtCategoryName, errorMsg);
                isValid = false;
            }
            else if (MemoryStore.Categories.Any(c => c.Name.Equals(catName, StringComparison.OrdinalIgnoreCase)))
            {
                _errorProvider.SetError(txtCategoryName, "A category with this name already exists.");
                isValid = false;
            }
            else
            {
                _errorProvider.SetError(txtCategoryName, string.Empty);
            }

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before saving.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔥 1. إنشاء Category
            int newCategoryId = MemoryStore.Categories.Count > 0
                ? MemoryStore.Categories.Max(c => c.Id) + 1
                : 1;

            var newCategory = new Category
            {
                Id = newCategoryId,
                Name = catName
            };

            MemoryStore.Categories.Add(newCategory);

            // 🔥 2. إنشاء Filters (Template)
            var filtersDict = new Dictionary<string, List<string>>();

            foreach (DataGridViewRow row in dgvFilters.Rows)
            {
                if (row.IsNewRow) continue;

                var nameCell = row.Cells["colFilterName"].Value;
                var valuesCell = row.Cells["colFilterValues"].Value;

                if (nameCell == null || valuesCell == null) continue;

                string fName = nameCell.ToString();
                string[] fValuesArray = valuesCell.ToString()
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                List<string> fValuesList = fValuesArray.Select(v => v.Trim()).ToList();

                if (!filtersDict.ContainsKey(fName))
                    filtersDict.Add(fName, fValuesList);
            }

            MemoryStore.CategoryTemplates.Add(new CategoryTemplate
            {
                CategoryId = newCategoryId,
                AvailableFilters = filtersDict
            });

            // 🔥🔥🔥 3. أهم جزء (حل مشكلتك)
            int newZoneId = MemoryStore.StorageZones.Count > 0
                ? MemoryStore.StorageZones.Max(z => z.Id) + 1
                : 1;

            MemoryStore.StorageZones.Add(new StorageZone
            {
                Id = newZoneId,
                Name = $"Auto Zone: {catName}",
                TargetCategoryId = newCategoryId
            });

            // Set Properties for parent form to retrieve
            CreatedCategoryName = catName;
            CreatedFilters = filtersDict;

            // (اختياري) Logging
            MemoryStore.LogAction("CATEGORY CREATED", $"New category '{catName}' created with storage zone.");

            MessageBox.Show("Category created successfully.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

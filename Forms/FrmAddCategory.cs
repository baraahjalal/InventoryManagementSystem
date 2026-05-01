using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class FrmAddCategory : Form
    {
        // Property to get the newly added Category Name when successful
        public string CreatedCategoryName { get; private set; }

        // Dictionary to store "Filter Name" -> "List of Filter Values"
        public Dictionary<string, List<string>> CreatedFilters { get; private set; }

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
            string fName = txtFilterName.Text.Trim();
            string fValues = txtFilterValues.Text.Trim();

            if (string.IsNullOrWhiteSpace(fName) || fName == "e.g. RAM" ||
                string.IsNullOrWhiteSpace(fValues) || fValues == "e.g. 8GB, 16GB, 32GB")
            {
                MessageBox.Show("Please enter both a valid Filter Name and valid Values.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if filter already added
            foreach (DataGridViewRow row in dgvFilters.Rows)
            {
                if (row.Cells["colFilterName"].Value.ToString().Equals(fName, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("This filter name is already added.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Add to grid
            dgvFilters.Rows.Add(fName, fValues);

            // Reset Fields
            txtFilterName.Text = "";
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
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Please enter a category name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save the data to properties
            CreatedCategoryName = txtCategoryName.Text.Trim();
            CreatedFilters.Clear();

            foreach (DataGridViewRow row in dgvFilters.Rows)
            {
                string fName = row.Cells["colFilterName"].Value.ToString();
                string[] fValuesArray = row.Cells["colFilterValues"].Value.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                List<string> fValuesList = new List<string>();
                foreach (var val in fValuesArray)
                {
                    fValuesList.Add(val.Trim());
                }

                CreatedFilters.Add(fName, fValuesList);
            }

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
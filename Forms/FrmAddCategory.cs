using System;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Forms
{
    public partial class FrmAddCategory : Form
    {
        public string CreatedCategoryName { get; private set; }

        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FrmAddCategory()
        {
            InitializeComponent();
        }

        private void btnAddSpecKey_Click(object sender, EventArgs e)
        {
            string key = txtSpecKey.Text.Trim();
            if (string.IsNullOrWhiteSpace(key))
            {
                _errorProvider.SetError(txtSpecKey, "Spec key cannot be empty.");
                return;
            }
            _errorProvider.SetError(txtSpecKey, string.Empty);

            foreach (DataGridViewRow row in dgvSpecKeys.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["colSpecKeyName"].Value?.ToString()
                        .Equals(key, StringComparison.OrdinalIgnoreCase) == true)
                {
                    MessageBox.Show("This spec key is already added.", "Duplicate",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            dgvSpecKeys.Rows.Add(key);
            txtSpecKey.Clear();
            txtSpecKey.Focus();
        }

        private void dgvSpecKeys_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSpecKeys.Columns[e.ColumnIndex].Name == "colSpecKeyRemove")
                dgvSpecKeys.Rows.RemoveAt(e.RowIndex);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            string catName = txtCategoryName.Text.Trim();
            bool isValid = true;
            string errorMsg;

            if (!ValidationHelper.IsRequired(catName, out errorMsg))
            { _errorProvider.SetError(txtCategoryName, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(catName, 2, 100, out errorMsg))
            { _errorProvider.SetError(txtCategoryName, errorMsg); isValid = false; }
            else if (CategoryRepository.Exists(catName))
            { _errorProvider.SetError(txtCategoryName, "A category with this name already exists."); isValid = false; }
            else
              _errorProvider.SetError(txtCategoryName, string.Empty);

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before saving.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CategoryRepository.Add(catName);

            StorageZoneRepository.Add(new StorageZone
            {
                ZoneName     = $"Auto Zone: {catName}",
                CategoryName = catName
            });

            foreach (DataGridViewRow row in dgvSpecKeys.Rows)
            {
                if (row.IsNewRow) continue;
                string key = row.Cells["colSpecKeyName"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(key))
                    CategorySpecTemplateRepository.Add(catName, key);
            }

            CreatedCategoryName = catName;
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmSupplierManagement : Form
    {
        private bool isEditMode = false;
        private int currentEditId = 0;

        public FrmSupplierManagement()
        {
            InitializeComponent();
            
            // Wire up the load event
            this.Load += FrmSupplierManagement_Load;
            
            // Enable double buffering to fix any grid flickering, matching the dashboard
            EnableDoubleBuffered(dgvSuppliers);

            // Wire up button events
            btnSave.Click += BtnSave_Click;
            btnClear.Click += BtnClear_Click;
            btnAddSupplier.Click += BtnAddSupplier_Click;
            btnEditSupplier.Click += BtnEditSupplier_Click;
            btnDeleteSupplier.Click += BtnDeleteSupplier_Click;
            btnExport.Click += BtnExport_Click;
            
            // Wire up search events
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;

            // Wire up custom cell formatting for status colors
            dgvSuppliers.CellFormatting += DgvSuppliers_CellFormatting;
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

        private ErrorProvider errorProvider = new ErrorProvider();

        private void FrmSupplierManagement_Load(object sender, EventArgs e)
        {
            // UI Feedback: Restrict phone to numbers
            txtPhone.KeyPress += InventoryManagementSystem.ValidationHelper.AllowOnlyDigits;

            LoadCategories();
            ClearForm();
            LoadData();
        }
        
        private void LoadCategories()
        {
            clbSuppliedProducts.Items.Clear();
            foreach (var cat in MemoryStore.Categories)
            {
                clbSuppliedProducts.Items.Add(cat);
            }
            clbSuppliedProducts.DisplayMember = "Name";
            clbSuppliedProducts.ValueMember = "Id";
        }

        private void LoadData(string searchTerm = "")
        {
            dgvSuppliers.Rows.Clear();
            var query = MemoryStore.Suppliers.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(searchTerm) && searchTerm != "Search...")
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(s => 
                    s.Name.ToLower().Contains(searchTerm) || 
                    s.ContactPerson.ToLower().Contains(searchTerm) ||
                    s.Email.ToLower().Contains(searchTerm) ||
                    (s.Phone != null && s.Phone.ToLower().Contains(searchTerm))
                );
            }

            foreach (var sup in query)
            {
                var categoryIds = MemoryStore.SupplierCategories.Where(sc => sc.SupplierId == sup.Id).Select(sc => sc.CategoryId).ToList();
                var categories = MemoryStore.Categories.Where(c => categoryIds.Contains(c.Id)).Select(c => c.Name).ToList();
                string products = categories != null ? string.Join(", ", categories) : "";
                string status = sup.IsActive ? "Active" : "Inactive";
                dgvSuppliers.Rows.Add(sup.Name, sup.ContactPerson, sup.Phone, sup.Email, products, status);
                
                // Store the ID in the row's Tag property for easy access
                dgvSuppliers.Rows[dgvSuppliers.Rows.Count - 1].Tag = sup.Id;
            }

            dgvSuppliers.ClearSelection();
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search...")
            {
                txtSearch.Text = "";
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search...";
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void ClearForm()
        {
            txtSupplierName.Clear();
          //  cmbCategory.SelectedIndex = -1;
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            chkIsActive.Checked = true;
            
            for (int i = 0; i < clbSuppliedProducts.Items.Count; i++)
            {
                clbSuppliedProducts.SetItemChecked(i, false);
            }

            isEditMode = false;
            currentEditId = 0;
            lblFormTitle.Text = "Add New Supplier";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnAddSupplier_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtSupplierName.Focus();
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();
            bool isValid = true;
            string errorMsg;

            if (!InventoryManagementSystem.ValidationHelper.IsRequired(txtSupplierName.Text, out errorMsg))
            {
                errorProvider.SetError(txtSupplierName, errorMsg);
                isValid = false;
            }
            else if (!InventoryManagementSystem.ValidationHelper.IsValidLength(txtSupplierName.Text.Trim(), 2, 100, out errorMsg))
            {
                errorProvider.SetError(txtSupplierName, errorMsg);
                isValid = false;
            }
            else if (!isEditMode && MemoryStore.Suppliers.Any(s => s.Name.Equals(txtSupplierName.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                // Unique Check enforced using Data Source (Business Rule)
                errorProvider.SetError(txtSupplierName, "A supplier with this name already exists.");
                isValid = false;
            }

            if (!InventoryManagementSystem.ValidationHelper.IsRequired(txtContactPerson.Text, out errorMsg))
            {
                errorProvider.SetError(txtContactPerson, errorMsg);
                isValid = false;
            }
            else if (!InventoryManagementSystem.ValidationHelper.IsValidLength(txtContactPerson.Text.Trim(), 2, 80, out errorMsg))
            {
                errorProvider.SetError(txtContactPerson, errorMsg);
                isValid = false;
            }

            if (!InventoryManagementSystem.ValidationHelper.IsValidPhone(txtPhone.Text, out errorMsg))
            {
                errorProvider.SetError(txtPhone, errorMsg);
                isValid = false;
            }

            if (!InventoryManagementSystem.ValidationHelper.IsValidEmail(txtEmail.Text, out errorMsg))
            {
                errorProvider.SetError(txtEmail, errorMsg);
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show("Please correct the errors indicated before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            var selectedCategories = new List<int>();
            foreach (var item in clbSuppliedProducts.CheckedItems)
            {
                if (item is Category cat) selectedCategories.Add(cat.Id);
            }

            if (isEditMode)
            {
                var supplier = MemoryStore.Suppliers.FirstOrDefault(s => s.Id == currentEditId);
                if (supplier != null)
                {
                    supplier.Name = txtSupplierName.Text.Trim();
               //     supplier.Category = cmbCategory.SelectedItem?.ToString();
                    supplier.ContactPerson = txtContactPerson.Text.Trim();
                    supplier.Phone = txtPhone.Text.Trim();
                    supplier.Email = txtEmail.Text.Trim();
                    supplier.IsActive = chkIsActive.Checked;
                    
                    MemoryStore.SupplierCategories.RemoveAll(sc => sc.SupplierId == supplier.Id);
                    foreach (var categoryId in selectedCategories)
                    {
                        MemoryStore.SupplierCategories.Add(new SupplierCategory { SupplierId = supplier.Id, CategoryId = categoryId });
                    }

                    MemoryStore.LogAction("EDIT SUPPLIER", $"Supplier '{supplier.Name}' updated.");
                    MessageBox.Show("Supplier updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                int newId = MemoryStore.Suppliers.Count > 0 ? MemoryStore.Suppliers.Max(s => s.Id) + 1 : 1;

                var newSupplier = new Supplier
                {
                    Id = newId,
                    Name = txtSupplierName.Text.Trim(),
                 //   Category = cmbCategory.SelectedItem?.ToString(),
                    ContactPerson = txtContactPerson.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    IsActive = chkIsActive.Checked
                };

                MemoryStore.Suppliers.Add(newSupplier);
                
                foreach (var categoryId in selectedCategories)
                {
                    MemoryStore.SupplierCategories.Add(new SupplierCategory { SupplierId = newSupplier.Id, CategoryId = categoryId });
                }

                MemoryStore.LogAction("ADD SUPPLIER", $"Supplier '{newSupplier.Name}' added.");
                MessageBox.Show("Supplier added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ClearForm();
            LoadData(txtSearch.Text);
        }

        private void BtnEditSupplier_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a supplier from the list to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = (int)dgvSuppliers.SelectedRows[0].Tag;
            var supplier = MemoryStore.Suppliers.FirstOrDefault(s => s.Id == id);
            var categoryIds = MemoryStore.SupplierCategories.Where(sc => sc.SupplierId == id).Select(sc => sc.CategoryId).ToList();

            if (supplier != null)
            {
                txtSupplierName.Text = supplier.Name;
               // cmbCategory.SelectedItem = supplier.Category;
                txtContactPerson.Text = supplier.ContactPerson;
                txtPhone.Text = supplier.Phone;
                txtEmail.Text = supplier.Email;
                chkIsActive.Checked = supplier.IsActive;

                for (int i = 0; i < clbSuppliedProducts.Items.Count; i++)
                {
                    if (clbSuppliedProducts.Items[i] is Category cat)
                    {
                        clbSuppliedProducts.SetItemChecked(i, categoryIds != null && categoryIds.Contains(cat.Id));
                    }
                }

                isEditMode = true;
                currentEditId = id;
                lblFormTitle.Text = "Edit Supplier";
            }
        }

        private void BtnDeleteSupplier_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a supplier from the list to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = (int)dgvSuppliers.SelectedRows[0].Tag;
            
            // Check if supplier is used in any products
            bool isInUse = MemoryStore.ProductSuppliers.Any(ps => ps.SupplierId == id);
            if (isInUse)
            {
                MessageBox.Show("Cannot delete this supplier because there are products associated with it.\n\nConsider changing the status to 'Inactive' instead.", "Delete Prevented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this supplier?\nThis action cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            
            if (result == DialogResult.Yes)
            {
                var supplier = MemoryStore.Suppliers.FirstOrDefault(s => s.Id == id);
                if (supplier != null)
                {
                    MemoryStore.Suppliers.Remove(supplier);
                    MemoryStore.SupplierCategories.RemoveAll(sc => sc.SupplierId == id);
                    MemoryStore.LogAction("DELETE SUPPLIER", $"Supplier '{supplier.Name}' deleted.");
                    
                    LoadData(txtSearch.Text);
                    ClearForm();
                    
                    MessageBox.Show("Supplier deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            // For now, provide a message or implement simple CSV export
            if (dgvSuppliers.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Export feature is currently under development.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DgvSuppliers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Apply professional text colors to the Status column based on value
            if (e.RowIndex >= 0 && e.Value != null && dgvSuppliers.Columns[e.ColumnIndex].Name == "colStatus")
            {
                string status = e.Value.ToString();

                if (status == "Active")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); // Vibrant Green
                }
                else if (status == "Inactive")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68); // Alert Red
                }

                // Emphasize the status text slightly to stand out
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            }
        }

        private void dgvSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

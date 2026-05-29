using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem
{
    public partial class FrmSupplierManagement : Form
    {
        private bool   isEditMode      = false;
        private string currentEditName = string.Empty;

        private ErrorProvider errorProvider = new ErrorProvider();

        public FrmSupplierManagement()
        {
            InitializeComponent();
            this.Load += FrmSupplierManagement_Load;
            EnableDoubleBuffered(dgvSuppliers);

            btnSave.Click            += BtnSave_Click;
            btnClear.Click           += BtnClear_Click;
            btnAddSupplier.Click     += BtnAddSupplier_Click;
            btnEditSupplier.Click    += BtnEditSupplier_Click;
            btnDeleteSupplier.Click  += BtnDeleteSupplier_Click;
            btnExport.Click          += BtnExport_Click;
            txtSearch.TextChanged    += TxtSearch_TextChanged;
            txtSearch.Enter          += TxtSearch_Enter;
            txtSearch.Leave          += TxtSearch_Leave;
            dgvSuppliers.CellFormatting += DgvSuppliers_CellFormatting;
        }

        private void EnableDoubleBuffered(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgv, new object[] { true });
        }

        private void FrmSupplierManagement_Load(object sender, EventArgs e)
        {
            txtPhone.KeyPress += ValidationHelper.AllowOnlyDigits;
            txtContactPerson.KeyPress += ValidationHelper.AllowOnlyDigits;

            // Repurpose txtContactPerson as Tax Number input
            if (lblContactPerson != null)
            {
                lblContactPerson.Visible = true;
                lblContactPerson.Text    = "Tax Number";
            }
            txtContactPerson.Visible = true;

            clbSuppliedProducts.Visible = false;
            if (lblSuppliedProducts != null) lblSuppliedProducts.Visible = false;

            ClearForm();
            LoadData();
        }

        private void LoadData(string searchTerm = "")
        {
            dgvSuppliers.Rows.Clear();
            var suppliers = SupplierRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(searchTerm) && searchTerm != "Search...")
            {
                searchTerm = searchTerm.ToLower();
                suppliers  = suppliers.Where(s =>
                    s.SupplierName.ToLower().Contains(searchTerm) ||
                    s.SupplierTaxNumber.ToString().Contains(searchTerm) ||
                    (s.Email != null && s.Email.ToLower().Contains(searchTerm)) ||
                    (s.Phone != null && s.Phone.ToLower().Contains(searchTerm))
                ).ToList();
            }

            foreach (var sup in suppliers)
            {
                string status = sup.IsActive ? "Active" : "Inactive";
                dgvSuppliers.Rows.Add(sup.SupplierName, sup.Phone, sup.Email, status);
                dgvSuppliers.Rows[dgvSuppliers.Rows.Count - 1].Tag = sup;
            }
            dgvSuppliers.ClearSelection();
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search...") txtSearch.Text = "";
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text)) txtSearch.Text = "Search...";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e) => LoadData(txtSearch.Text);

        private void ClearForm()
        {
            txtSupplierName.Clear();
            txtSupplierName.ReadOnly  = false;
            txtContactPerson.Clear();
            txtContactPerson.ReadOnly = false;
            txtPhone.Clear();
            txtEmail.Clear();
            chkIsActive.Checked = true;
            isEditMode          = false;
            currentEditName     = string.Empty;
            lblFormTitle.Text   = "Add New Supplier";
        }

        private void BtnClear_Click(object sender, EventArgs e)     => ClearForm();
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

            if (!ValidationHelper.IsRequired(txtSupplierName.Text, out errorMsg))
            { errorProvider.SetError(txtSupplierName, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(txtSupplierName.Text.Trim(), 2, 150, out errorMsg))
            { errorProvider.SetError(txtSupplierName, errorMsg); isValid = false; }
            else if (!isEditMode && SupplierRepository.Exists(txtSupplierName.Text.Trim()))
            { errorProvider.SetError(txtSupplierName, "A supplier with this name already exists."); isValid = false; }

            if (!ValidationHelper.IsRequired(txtContactPerson.Text, out errorMsg))
            { errorProvider.SetError(txtContactPerson, "Tax Number is required."); isValid = false; }
            else if (!int.TryParse(txtContactPerson.Text.Trim(), out int taxNum) || taxNum <= 0)
            { errorProvider.SetError(txtContactPerson, "Tax Number must be a positive integer."); isValid = false; }
            else if (!isEditMode && SupplierRepository.TaxNumberExists(taxNum))
            { errorProvider.SetError(txtContactPerson, "This Tax Number is already registered."); isValid = false; }

            if (!ValidationHelper.IsValidPhone(txtPhone.Text, out errorMsg))
            { errorProvider.SetError(txtPhone, errorMsg); isValid = false; }

            if (!ValidationHelper.IsValidEmail(txtEmail.Text, out errorMsg))
            { errorProvider.SetError(txtEmail, errorMsg); isValid = false; }

            if (!isValid)
                MessageBox.Show("Please correct the errors indicated before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return isValid;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            var supplier = new Supplier
            {
                SupplierTaxNumber = int.Parse(txtContactPerson.Text.Trim()),
                SupplierName      = txtSupplierName.Text.Trim(),
                Phone             = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                Email             = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                IsActive          = chkIsActive.Checked
            };

            if (isEditMode)
            {
                SupplierRepository.Update(supplier);
                MessageBox.Show("Supplier updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SupplierRepository.Add(supplier);
                MessageBox.Show("Supplier added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ClearForm();
            LoadData(txtSearch.Text);
        }

        private void BtnEditSupplier_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a supplier to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var supplier = dgvSuppliers.SelectedRows[0].Tag as Supplier;
            if (supplier == null) return;

            txtSupplierName.Text      = supplier.SupplierName;
            txtSupplierName.ReadOnly  = true;
            txtContactPerson.Text     = supplier.SupplierTaxNumber.ToString();
            txtContactPerson.ReadOnly = true;
            txtPhone.Text             = supplier.Phone;
            txtEmail.Text             = supplier.Email;
            chkIsActive.Checked       = supplier.IsActive;
            isEditMode                = true;
            currentEditName           = supplier.SupplierName;
            lblFormTitle.Text         = "Edit Supplier";
        }

        private void BtnDeleteSupplier_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a supplier to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var supplier = dgvSuppliers.SelectedRows[0].Tag as Supplier;
            if (supplier == null) return;

            if (SupplierRepository.HasRelatedRecords(supplier.SupplierTaxNumber))
            {
                MessageBox.Show(
                    "Cannot delete this supplier — they are referenced in existing stock movement records.",
                    "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete this supplier?\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SupplierRepository.Delete(supplier.SupplierTaxNumber);
                    LoadData(txtSearch.Text);
                    ClearForm();
                    MessageBox.Show("Supplier deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 547)
                {
                    MessageBox.Show(
                        "Cannot delete this supplier — they are referenced in existing records.",
                        "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"An error occurred while deleting the supplier: {ex.Message}",
                        "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export feature is currently under development.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DgvSuppliers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null && dgvSuppliers.Columns[e.ColumnIndex].Name == "colStatus")
            {
                string status = e.Value.ToString();
                e.CellStyle.ForeColor = status == "Active"
                    ? Color.FromArgb(16, 185, 129)
                    : Color.FromArgb(239, 68, 68);
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            }
        }
    }
}

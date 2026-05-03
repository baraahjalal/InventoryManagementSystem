using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmStockIn : Form
    {
        public FrmStockIn()
        {
            InitializeComponent();
            this.Load += FrmStockIn_Load;
            btnExecute.Click += BtnExecute_Click;
        }

        private void FrmStockIn_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            LoadSuppliers();
            LoadProducts();
            
            if (cmbStorageZone.Items.Count > 0)
                cmbStorageZone.SelectedIndex = 0;
        }

        private void LoadSuppliers()
        {
            var suppliers = MemoryStore.Suppliers.Where(s => s.IsActive).ToList();
            
            cmbSupplier.DataSource = null;
            cmbSupplier.DisplayMember = "Name";
            cmbSupplier.ValueMember = "Id";
            cmbSupplier.DataSource = suppliers;
            cmbSupplier.SelectedIndex = -1;
        }

        private void LoadProducts()
        {
            var products = MemoryStore.Products.ToList();
            
            cmbProduct.DataSource = null;
            cmbProduct.DisplayMember = "Name";
            cmbProduct.ValueMember = "Id";
            cmbProduct.DataSource = products;
            cmbProduct.SelectedIndex = -1;
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            // 1. Validation
            if (cmbSupplier.SelectedValue == null)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbProduct.SelectedValue == null)
            {
                MessageBox.Show("Please select a product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quantity = (int)numQuantity.Value;
            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOrderNumber.Text))
            {
                MessageBox.Show("Please enter a Purchase Order Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Prepare Data
            int productId = (int)cmbProduct.SelectedValue;
            
            string notes = $"PO: {txtOrderNumber.Text.Trim()} | Zone: {cmbStorageZone.SelectedItem} | Warranty: {txtWarrantyInfo.Text.Trim()}";
            if (!string.IsNullOrWhiteSpace(txtSerialNumbers.Text))
            {
                // Clean up newlines for the notes string
                string serials = txtSerialNumbers.Text.Replace("\r\n", ",").Replace("\n", ",");
                notes += $" | Serials: {serials}";
            }

            // 3. Execute Business Logic
            bool success = MemoryStore.PerformStockMovement(productId, quantity, "STOCK IN", notes);

            // 4. Handle Result
            if (success)
            {
                MessageBox.Show("Stock In operation recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            else
            {
                MessageBox.Show("Failed to record Stock In operation. Please check product details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            cmbSupplier.SelectedIndex = -1;
            cmbProduct.SelectedIndex = -1;
            txtOrderNumber.Clear();
            numQuantity.Value = 0;
            if (cmbStorageZone.Items.Count > 0) cmbStorageZone.SelectedIndex = 0;
            txtSerialNumbers.Clear();
            txtWarrantyInfo.Clear();
        }
    }
}

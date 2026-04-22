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
    public partial class FrmUsers : Form
    {
        public FrmUsers()
        {
            InitializeComponent();
            LoadGridData();

            // Wire up the event so we change permissions automatically when role changes
            cmbRole.SelectedIndexChanged += CmbRole_SelectedIndexChanged;
        }

        private void CmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear all checks first
            UncheckAllBoxes();

            string selectedRole = cmbRole.Text;

            switch (selectedRole)
            {
                case "System Administrator":
                    CheckAllBoxes(true); // Admin gets everything
                    break;

                case "Stock Clerk":
                    chkCanViewDashboard.Checked = true;
                    chkCanViewProducts.Checked = true;
                    chkCanDoStockIn.Checked = true;
                    chkCanDoStockOut.Checked = true;
                    chkCanViewSuppliers.Checked = true;
                    break;

                case "Reporting Officer":
                    chkCanViewDashboard.Checked = true;
                    chkCanViewProducts.Checked = true;
                    chkCanViewSuppliers.Checked = true;
                    chkCanViewReports.Checked = true;
                    chkCanPrint.Checked = true;
                    break;

                case "Procurement Manager":
                    chkCanViewDashboard.Checked = true;
                    chkCanViewProducts.Checked = true;
                    chkCanDoStockIn.Checked = true;
                    chkCanViewSuppliers.Checked = true;
                    chkCanManageSuppliers.Checked = true;
                    chkCanViewReports.Checked = true;
                    chkCanPrint.Checked = true;
                    break;
            }
        }

        private void UncheckAllBoxes()
        {
            CheckAllBoxes(false);
        }

        private void CheckAllBoxes(bool isChecked)
        {
            chkCanViewProducts.Checked = isChecked;
            chkCanAddProducts.Checked = isChecked;
            chkCanEditProducts.Checked = isChecked;
            chkCanDeleteProducts.Checked = isChecked;
            chkCanDoStockIn.Checked = isChecked;
            chkCanDoStockOut.Checked = isChecked;
            
            chkCanViewDashboard.Checked = isChecked;
            chkCanViewSuppliers.Checked = isChecked;
            chkCanManageSuppliers.Checked = isChecked;
            chkCanViewUsers.Checked = isChecked;
            chkCanManageUsers.Checked = isChecked;
            
            chkCanViewReports.Checked = isChecked;
            chkCanPrint.Checked = isChecked;
            chkCanViewAuditLog.Checked = isChecked;
        }

        private void LoadGridData()
        {
            // Clear existing rows before loading
            dgvUsers.Rows.Clear();

            // Loop through all users in our In-Memory Database
            int no = 1; // Counter for the "No" column
            foreach (var user in MemoryStore.Users)
            {
                // Format a fake Registration Number for the design (Optional)
                string regNum = $"REG-{user.Id:D3}";

                // Add the user to the DataGridView
                // Columns order: colId, colNo, colUserName, colPassword, colJob, colRegNum, colImage
                dgvUsers.Rows.Add(
                    user.Id,                // Hidden ID for lookups
                    no.ToString(),          // Display sequence number
                    user.Username,          // Username
                    "********",             // Masked Password for security
                    user.Role,              // Job / Role
                    regNum,                 // Reg Number
                    null                    // Image (null for now)
                );

                no++;
            }

            // Clear any default selection so it looks clean initially
            dgvUsers.ClearSelection();

        }
    }
}

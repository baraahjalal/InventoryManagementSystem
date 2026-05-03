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
            // Ensure the role combo contains only the simplified roles
            cmbRole.Items.Clear();
            cmbRole.Items.Add("System Administrator");
            cmbRole.Items.Add("Employee");

            LoadGridData();

            // Role selection no longer toggles granular permissions
            cmbRole.SelectedIndexChanged += CmbRole_SelectedIndexChanged;
        }

        private void CmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Only keep role selection; granular permission toggles were removed.
            // If needed, set additional defaults here. Currently role text is sufficient.
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

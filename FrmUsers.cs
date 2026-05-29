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
            LoadStaticData();
        }

        private void LoadStaticData()
        {
            // Adding static dummy data to the DataGridView for design preview
            // Columns order: colId, colNo, colUserName, colPassword, colJob, colRegNum, colImage
            
            dgvUsers.Rows.Add(1, "1", "admin.super", "********", "System Administrator", "REG-001", null);
            dgvUsers.Rows.Add(2, "2", "john.doe", "********", "Inventory Manager", "REG-002", null);
            dgvUsers.Rows.Add(3, "3", "sarah.smith", "********", "Sales Representative", "REG-003", null);
            dgvUsers.Rows.Add(4, "4", "mike.jones", "********", "Stock Clerk", "REG-004", null);
            dgvUsers.Rows.Add(5, "5", "anna.lee", "********", "Customer Support", "REG-005", null);
            
            // Optional: Clear any default selection so it looks clean initially
            dgvUsers.ClearSelection();
        }
    }
}

using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmSupplierManagement : Form
    {
        public FrmSupplierManagement()
        {
            InitializeComponent();
            
            // Wire up the load event
            this.Load += FrmSupplierManagement_Load;
            
            // Enable double buffering to fix any grid flickering, matching the dashboard
            EnableDoubleBuffered(dgvSuppliers);
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

        private void FrmSupplierManagement_Load(object sender, EventArgs e)
        {
            LoadStaticData();
        }

        private void LoadStaticData()
        {
            // Adding realistic dummy data for IT inventory suppliers
            dgvSuppliers.Rows.Add("Apple Inc.", "Manufacturer", "Tim Cook", "+1-800-692-7753", "contact@apple.com", "Laptops, Phones", "Active");
            dgvSuppliers.Rows.Add("Dell Technologies", "Manufacturer", "Michael Dell", "+1-800-456-3355", "support@dell.com", "Laptops, Monitors", "Active");
            dgvSuppliers.Rows.Add("Tech Data Corp", "Distributor", "Sarah Jenkins", "+1-800-237-8931", "sales@techdata.com", "Accessories, Networking", "Active");
            dgvSuppliers.Rows.Add("HP Enterprise", "Manufacturer", "Antonio Neri", "+1-800-786-0404", "partner@hpe.com", "Printers, Laptops", "Inactive");
            dgvSuppliers.Rows.Add("Ingram Micro", "Distributor", "Paul Bay", "+1-800-456-8000", "vendors@ingrammicro.com", "Phones, Accessories", "Active");
            dgvSuppliers.Rows.Add("Samsung Electronics", "Manufacturer", "JH Han", "+1-800-726-7864", "b2b.sales@samsung.com", "Phones, Monitors", "Active");

            // Clear the default blue selection highlight so it looks clean empty initially
            dgvSuppliers.ClearSelection();

            // Wire up custom cell formatting for status colors
            dgvSuppliers.CellFormatting += DgvSuppliers_CellFormatting;
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
    }
}

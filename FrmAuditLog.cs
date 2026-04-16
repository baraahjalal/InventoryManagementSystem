using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmAuditLog : Form
    {
        public FrmAuditLog()
        {
            InitializeComponent();
        }

        private void FrmAuditLog_Load(object sender, EventArgs e)
        {
            EnableDoubleBuffered(dgvAuditLog);
            LoadStaticData();
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

        private void LoadStaticData()
        {
            dgvAuditLog.Rows.Add("Apr 15, 08:32 AM", "Sarah Jenkins (Admin)", "STOCK IN", "MacBook Pro 14\" (APP-MBP-2023)", "Qty: 0 → 24");
            dgvAuditLog.Rows.Add("Apr 15, 09:15 AM", "Mike Ross (Manager)", "MODIFICATION", "Dell XPS 15 (DELL-XPS-2023)", "Price: $1,699 → $1,799");
            dgvAuditLog.Rows.Add("Apr 14, 02:40 PM", "System Auto", "DELETION", "HP LaserJet 1020", "Record Permanently Removed");
            dgvAuditLog.Rows.Add("Apr 14, 11:20 AM", "Sarah Jenkins (Admin)", "STOCK OUT", "iPad Air 5th Gen", "Qty: 35 → 30 (Assigned to IT)");
            dgvAuditLog.Rows.Add("Apr 14, 09:05 AM", "Mike Ross (Manager)", "ADDITION", "Supplier: Ingram Micro", "New Vendor Onboarded");
            dgvAuditLog.ClearSelection();
        }

        private void DgvAuditLog_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null && dgvAuditLog.Columns[e.ColumnIndex].Name == "colActionType")
            {
                string action = e.Value.ToString();
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);

                switch (action)
                {
                    case "STOCK IN":
                    case "ADDITION":
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); // Vibrant Green
                        break;
                    case "STOCK OUT":
                    case "MODIFICATION":
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246); // Clean Blue
                        break;
                    case "DELETION":
                        e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68); // Alert Red
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128); // Standard Gray
                        break;
                }
            }
        }
    }
}
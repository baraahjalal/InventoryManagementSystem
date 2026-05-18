using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;

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
            var user = DatabaseHelper.CurrentUser;
            if (user == null || !user.IsAdmin)
            {
                MessageBox.Show("Access Denied: You do not have permission to view the Audit Log.",
                    "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.BeginInvoke(new Action(() => this.Close()));
                return;
            }

            EnableDoubleBuffered(dgvAuditLog);
            LoadAuditData();
        }

        private void EnableDoubleBuffered(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgv, new object[] { true });
        }

        private void LoadAuditData()
        {
            dgvAuditLog.Rows.Clear();

            var logs = AuditLogRepository.GetAll();
            foreach (var log in logs)
            {
                int rowIndex = dgvAuditLog.Rows.Add(
                    log.LogTimestamp.ToString("MMM dd, hh:mm tt"),
                    log.Username,
                    FormatActionType(log.ActionType),
                    log.Description
                );
                dgvAuditLog.Rows[rowIndex].Tag = log.ActionType;
            }

            dgvAuditLog.ClearSelection();
        }

        private static string FormatActionType(string raw)
        {
            switch (raw)
            {
                case "STOCK STOCKIN":           return "Stock In";
                case "STOCK STOCKOUT":          return "Stock Out";
                case "STOCK RESTOCK":           return "Restock";
                case "STOCK RETURNTOSUPPLIER":  return "Return to Supplier";
                case "PRODUCT ADDED":           return "Product Added";
                case "PRODUCT DELETED":         return "Product Deleted";
                case "USER ADDED":              return "User Added";
                case "USER DELETED":            return "User Deleted";
                case "SUPPLIER ADDED":          return "Supplier Added";
                case "SUPPLIER DELETED":        return "Supplier Deleted";
                default: return raw;
            }
        }

        private void DgvAuditLog_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dgvAuditLog.Rows[e.RowIndex];
            string raw = row.Tag?.ToString() ?? "";
            Color bg;
            switch (raw)
            {
                case "STOCK STOCKIN":
                case "STOCK RESTOCK":
                    bg = Color.FromArgb(220, 252, 231); break;
                case "STOCK STOCKOUT":
                case "STOCK RETURNTOSUPPLIER":
                    bg = Color.FromArgb(254, 226, 226); break;
                case "USER ADDED":
                case "USER DELETED":
                    bg = Color.FromArgb(219, 234, 254); break;
                case "SUPPLIER ADDED":
                case "SUPPLIER DELETED":
                    bg = Color.FromArgb(254, 249, 195); break;
                default:
                    bg = Color.FromArgb(243, 244, 246); break;
            }
            row.DefaultCellStyle.BackColor          = bg;
            row.DefaultCellStyle.SelectionBackColor = bg;
        }

        private void DgvAuditLog_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null
                && dgvAuditLog.Columns[e.ColumnIndex].Name == "colActionType")
            {
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            }
        }
    }
}

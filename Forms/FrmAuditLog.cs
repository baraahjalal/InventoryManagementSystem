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
                MessageBox.Show("Access Denied: You do not have permission to view the Audit Log.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                dgvAuditLog.Rows.Add(
                    log.LogTimestamp.ToString("MMM dd, hh:mm tt"),
                    log.Username,
                    log.ActionType,
                    log.Description
                );
            }

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
                    case "STOCK STOCKIN":
                    case "PRODUCT ADDED":
                    case "SUPPLIER ADDED":
                    case "USER ADDED":
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129);
                        break;
                    case "STOCK STOCKOUT":
                    case "STOCK RESTOCK":
                    case "PRODUCT UPDATED":
                    case "SUPPLIER UPDATED":
                    case "USER UPDATED":
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246);
                        break;
                    case "PRODUCT DELETED":
                    case "SUPPLIER DELETED":
                    case "USER DELETED":
                        e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68);
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128);
                        break;
                }
            }
        }
    }
}

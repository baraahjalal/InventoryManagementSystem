using System;
using System.Drawing;
using System.Linq;
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
            // Double Protection: Verify admin access even if the form was opened bypassing button checks
            var user = MemoryStore.CurrentUser;
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
                null,
                dgv,
                new object[] { true }
            );
        }

        /// <summary>
        /// Loads real audit data from MemoryStore instead of hardcoded static rows.
        /// </summary>
        private void LoadAuditData()
        {
            dgvAuditLog.Rows.Clear();

            var logs = MemoryStore.AuditLogs
                .OrderByDescending(l => l.Timestamp)
                .ToList();

            foreach (var log in logs)
            {
                dgvAuditLog.Rows.Add(
                    log.Timestamp.ToString("MMM dd, hh:mm tt"),
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
                    case "STOCK IN":
                    case "ADDITION":
                    case "PRODUCT CREATED":
                    case "ADD SUPPLIER":
                    case "SUPPLIER ADDED":
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); // Vibrant Green
                        break;
                    case "STOCK OUT":
                    case "MODIFICATION":
                    case "PRODUCT UPDATED":
                    case "EDIT SUPPLIER":
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246); // Clean Blue
                        break;
                    case "DELETION":
                    case "DELETE SUPPLIER":
                        e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68); // Alert Red
                        break;
                    case "LOW STOCK ALERT":
                    case "STOCK ALERT":
                        e.CellStyle.ForeColor = Color.FromArgb(245, 158, 11); // Warning Orange
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128); // Standard Gray
                        break;
                }
            }
        }
    }
}
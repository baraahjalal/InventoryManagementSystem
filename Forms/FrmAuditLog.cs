using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

            var logs        = AuditLogRepository.GetAll();
            var productDict = ProductRepository.GetAll()
                .ToDictionary(p => p.ProductSerialNumber, p => p.ProductName);

            foreach (var log in logs)
            {
                ParseDescription(log.Description, productDict,
                    out string subject, out string qtyOrPrice, out string supplierOrCat, out string notes);

                int rowIndex = dgvAuditLog.Rows.Add(
                    log.LogTimestamp.ToString("MMM dd, hh:mm tt"),
                    log.Username,
                    FormatActionType(log.ActionType),
                    subject,
                    qtyOrPrice,
                    supplierOrCat,
                    notes
                );
                dgvAuditLog.Rows[rowIndex].Tag = log.ActionType;
            }

            dgvAuditLog.ClearSelection();
        }

        // Parses all 4 trigger description formats into clean column values.
        private static void ParseDescription(string desc, Dictionary<int, string> productDict,
            out string subject, out string qtyOrPrice, out string supplierOrCat, out string notes)
        {
            subject       = desc ?? "";
            qtyOrPrice    = "";
            supplierOrCat = "";
            notes         = "";

            if (string.IsNullOrEmpty(desc)) return;

            // ── Stock movements ──────────────────────────────────────────────────────
            // Format: "Product [ID]: N units[. Supplier: NAME][. Notes: Zone: X | Warranty: Y]"
            if (desc.StartsWith("Product ["))
            {
                // Resolve product name from DB dict
                int idStart = desc.IndexOf('[') + 1;
                int idEnd   = desc.IndexOf(']');
                if (idStart > 0 && idEnd > idStart &&
                    int.TryParse(desc.Substring(idStart, idEnd - idStart), out int productId))
                {
                    productDict.TryGetValue(productId, out string productName);
                    subject = productName != null
                        ? $"{productName} [{productId}]"
                        : $"Product [{productId}]";
                }

                // Quantity: between "]: " and next "."
                int afterClose = desc.IndexOf("]: ");
                if (afterClose >= 0)
                {
                    string rest  = desc.Substring(afterClose + 3);
                    int dotIndex = rest.IndexOf('.');
                    qtyOrPrice   = dotIndex > 0 ? rest.Substring(0, dotIndex) : rest;
                }

                int supplierStart = desc.IndexOf("Supplier: ");
                int notesStart    = desc.IndexOf("Notes: ");

                if (supplierStart > 0)
                {
                    int end   = notesStart > 0 ? notesStart - 2 : desc.Length;
                    supplierOrCat = desc.Substring(supplierStart + 10, end - (supplierStart + 10)).TrimEnd('.');
                }

                if (notesStart > 0)
                    notes = desc.Substring(notesStart + 7);

                return;
            }

            // ── Product Added ────────────────────────────────────────────────────────
            // Format: "New product: [ID] NAME | Category: CAT | Price: PRICE"
            if (desc.StartsWith("New product: "))
            {
                string[] parts    = desc.Substring(13).Split('|');
                string productPart = parts[0].Trim();
                int closeBracket  = productPart.IndexOf(']');
                subject = closeBracket >= 0
                    ? productPart.Substring(closeBracket + 2).Trim()
                    : productPart;

                foreach (var part in parts)
                {
                    var p = part.Trim();
                    if (p.StartsWith("Category:"))   supplierOrCat = p.Substring(9).Trim();
                    else if (p.StartsWith("Price:")) qtyOrPrice    = p.Substring(6).Trim();
                }
                return;
            }

            // ── Product Deleted ──────────────────────────────────────────────────────
            // Format: "Product removed: [ID] NAME"
            if (desc.StartsWith("Product removed: "))
            {
                string rest      = desc.Substring(17);
                int closeBracket = rest.IndexOf(']');
                subject = closeBracket >= 0
                    ? rest.Substring(closeBracket + 2).Trim()
                    : rest;
                return;
            }

            // ── User Added ───────────────────────────────────────────────────────────
            // Format: "New user: USERNAME (ROLE) — EmployeeID: ID"
            if (desc.StartsWith("New user: "))
            {
                string rest  = desc.Substring(10);
                int dashIdx  = rest.IndexOf(" — "); // em-dash
                if (dashIdx > 0)
                {
                    subject = rest.Substring(0, dashIdx).Trim();
                    notes   = rest.Substring(dashIdx + 3).Trim();
                }
                else
                {
                    subject = rest;
                }
                return;
            }

            // ── User Deleted ─────────────────────────────────────────────────────────
            // Format: "User removed: USERNAME (ROLE)"
            if (desc.StartsWith("User removed: "))
            {
                subject = desc.Substring(14).Trim();
                return;
            }

            // ── Supplier Added ───────────────────────────────────────────────────────
            // Format: "New supplier: NAME | TaxNumber: X[| Phone: Y][| Email: Z]"
            if (desc.StartsWith("New supplier: "))
            {
                string[] parts = desc.Substring(14).Split('|');
                subject = parts[0].Trim();
                var extra = new List<string>();

                foreach (var part in parts)
                {
                    var p = part.Trim();
                    if (p.StartsWith("TaxNumber:"))  supplierOrCat = "Tax: " + p.Substring(10).Trim();
                    else if (p.StartsWith("Phone:") || p.StartsWith("Email:")) extra.Add(p);
                }

                if (extra.Count > 0)
                    notes = string.Join(" | ", extra);
                return;
            }

            // ── Supplier Deleted ─────────────────────────────────────────────────────
            // Format: "Supplier removed: NAME"
            if (desc.StartsWith("Supplier removed: "))
            {
                subject = desc.Substring(18).Trim();
                return;
            }
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
            var row    = dgvAuditLog.Rows[e.RowIndex];
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

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
        private int _currentPage = 1;
        private int _totalPages  = 1;
        private const int PageSize = 20;
        private bool _suppressFilters;
        private Timer _searchDebounceTimer;

        private static readonly (string Display, string DbAction)[] ActionFilterOptions =
        {
            ("All Actions",          null),
            ("Stock In",             "STOCK STOCKIN"),
            ("Stock Out",            "STOCK STOCKOUT"),
            ("Restock",              "STOCK RESTOCK"),
            ("Return to Supplier",   "STOCK RETURNTOSUPPLIER"),
            ("Product Added",        "PRODUCT ADDED"),
            ("Product Deleted",      "PRODUCT DELETED"),
            ("User Added",           "USER ADDED"),
            ("User Deleted",         "USER DELETED"),
            ("Supplier Added",       "SUPPLIER ADDED"),
            ("Supplier Deleted",     "SUPPLIER DELETED"),
        };

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

            btnPurge.Visible = true;
            btnPrevPage.Click  += BtnPrevPage_Click;
            btnNextPage.Click  += BtnNextPage_Click;
            btnPurge.Click     += BtnPurge_Click;
            btnFilter.Click    += (s, ev) => ApplyFilters();
            txtSearch.GotFocus  += TxtSearch_GotFocus;
            txtSearch.LostFocus += TxtSearch_LostFocus;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            cmbActionType.SelectedIndexChanged += Filter_Changed;
            cmbDateRange.SelectedIndexChanged  += Filter_Changed;

            _searchDebounceTimer = new Timer { Interval = 300 };
            _searchDebounceTimer.Tick += (s, ev) =>
            {
                _searchDebounceTimer.Stop();
                ApplyFilters();
            };

            InitializeFilterControls();
            LoadAuditData();
        }

        private void InitializeFilterControls()
        {
            _suppressFilters = true;

            cmbActionType.Items.Clear();
            foreach (var option in ActionFilterOptions)
                cmbActionType.Items.Add(option.Display);
            cmbActionType.SelectedIndex = 0;

            cmbDateRange.SelectedIndex = 0;

            _suppressFilters = false;
        }

        private void EnableDoubleBuffered(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgv, new object[] { true });
        }

        // ── Data loading ──────────────────────────────────────────────────────

        private void LoadAuditData()
        {
            string actionKey   = GetActionTypeKey();
            string search      = GetSearchText();
            var (from, to)     = GetDateRange();

            int total   = AuditLogRepository.GetCount(actionKey, search, from, to);
            _totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
            if (_currentPage > _totalPages) _currentPage = _totalPages;
            if (_currentPage < 1)           _currentPage = 1;

            lblRecordCount.Text = $"Total: {total:N0} records";
            lblPageInfo.Text    = $"Page {_currentPage} of {_totalPages}";
            btnPrevPage.Enabled = _currentPage > 1;
            btnNextPage.Enabled = _currentPage < _totalPages;

            dgvAuditLog.Rows.Clear();

            var logs        = AuditLogRepository.GetPaged(_currentPage, PageSize, actionKey, search, from, to);
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

        // ── Filter helpers ────────────────────────────────────────────────────

        private string GetActionTypeKey()
        {
            int index = cmbActionType.SelectedIndex;
            if (index < 0 || index >= ActionFilterOptions.Length)
                return null;
            return ActionFilterOptions[index].DbAction;
        }

        private string GetSearchText()
        {
            string t = txtSearch.Text.Trim();
            return (t == "Search entities or users..." || t.Length == 0) ? null : t;
        }

        private (DateTime? from, DateTime? to) GetDateRange()
        {
            switch (cmbDateRange.SelectedItem?.ToString())
            {
                case "Today":
                    return (DateTime.Today, DateTime.Today.AddDays(1).AddTicks(-1));
                case "Last 7 Days":
                    return (DateTime.Today.AddDays(-7), null);
                case "Last 30 Days":
                    return (DateTime.Today.AddDays(-30), null);
                case "This Year":
                    return (new DateTime(DateTime.Today.Year, 1, 1), null);
                case "All Dates":
                default:
                    return (null, null);
            }
        }

        // ── Event handlers ────────────────────────────────────────────────────

        private void Filter_Changed(object sender, EventArgs e) => ApplyFilters();

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_suppressFilters) return;
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private void ApplyFilters()
        {
            if (_suppressFilters) return;
            _currentPage = 1;
            LoadAuditData();
        }

        private void BtnPrevPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1) { _currentPage--; LoadAuditData(); }
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages) { _currentPage++; LoadAuditData(); }
        }

        private void BtnPurge_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmDataMaintenance())
                frm.ShowDialog(this);
            _currentPage = 1;
            LoadAuditData();
        }

        private void TxtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search entities or users...")
            {
                txtSearch.Text      = "";
                txtSearch.ForeColor = Color.FromArgb(55, 65, 81);
            }
        }

        private void TxtSearch_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text      = "Search entities or users...";
                txtSearch.ForeColor = Color.FromArgb(107, 114, 128);
            }
        }

        // ── Description parsing ───────────────────────────────────────────────

        private static void ParseDescription(string desc, Dictionary<int, string> productDict,
            out string subject, out string qtyOrPrice, out string supplierOrCat, out string notes)
        {
            subject       = desc ?? "";
            qtyOrPrice    = "";
            supplierOrCat = "";
            notes         = "";

            if (string.IsNullOrEmpty(desc)) return;

            // ── Stock movements ──────────────────────────────────────────────
            // Format: "Product [ID]: N units[. Supplier: NAME][. Notes: Zone: X | Warranty: Y]"
            if (desc.StartsWith("Product ["))
            {
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

            // ── Product Added ────────────────────────────────────────────────
            // Format: "New product: [ID] NAME | Category: CAT | Price: PRICE"
            if (desc.StartsWith("New product: "))
            {
                string[] parts     = desc.Substring(13).Split('|');
                string productPart = parts[0].Trim();
                int closeBracket   = productPart.IndexOf(']');
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

            // ── Product Deleted ──────────────────────────────────────────────
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

            // ── User Added ───────────────────────────────────────────────────
            // Format: "New user: USERNAME (ROLE) — EmployeeID: ID"
            if (desc.StartsWith("New user: "))
            {
                string rest = desc.Substring(10);
                int dashIdx = rest.IndexOf(" — ");
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

            // ── User Deleted ─────────────────────────────────────────────────
            // Format: "User removed: USERNAME (ROLE)"
            if (desc.StartsWith("User removed: "))
            {
                subject = desc.Substring(14).Trim();
                return;
            }

            // ── Supplier Added ───────────────────────────────────────────────
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

            // ── Supplier Deleted ─────────────────────────────────────────────
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

            switch (raw)
            {
                case "STOCK STOCKIN":
                case "STOCK RESTOCK":
                    row.DefaultCellStyle.BackColor          = Color.FromArgb(240, 253, 244);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(213, 244, 222);
                    break;

                case "STOCK STOCKOUT":
                case "STOCK RETURNTOSUPPLIER":
                    row.DefaultCellStyle.BackColor          = Color.FromArgb(255, 242, 242);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 220, 220);
                    break;

                case "PRODUCT DELETED":
                case "USER DELETED":
                case "SUPPLIER DELETED":
                    row.DefaultCellStyle.BackColor          = Color.FromArgb(255, 253, 234);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 243, 199);
                    break;

                default:
                    row.DefaultCellStyle.BackColor          = e.RowIndex % 2 == 0
                        ? Color.White
                        : Color.FromArgb(249, 250, 251);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 232, 240);
                    break;
            }
        }

        private void DgvAuditLog_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null
                && dgvAuditLog.Columns[e.ColumnIndex].Name == "colActionType")
            {
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _searchDebounceTimer?.Stop();
            _searchDebounceTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}

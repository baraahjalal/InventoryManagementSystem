using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;

namespace InventoryManagementSystem
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
            EnableDoubleBuffered(dgvRecentActions);
            dgvRecentActions.CellFormatting += DgvRecentActions_CellFormatting;

            pnlLowStock.Cursor     = Cursors.Hand;
            pnlLowStock.Click      += PnlLowStock_Click;
            lblLowStockNum.Click   += PnlLowStock_Click;
            lblLowStockTitle.Click += PnlLowStock_Click;
            lblLowStockDesc.Click  += PnlLowStock_Click;

            pnlTotalProducts.Cursor     = Cursors.Hand;
            pnlTotalProducts.Click      += PnlTotalProducts_Click;
            lblTotalNum.Click           += PnlTotalProducts_Click;
            lblTotalTitle.Click         += PnlTotalProducts_Click;
            lblTotalDesc.Click          += PnlTotalProducts_Click;
        }

        private void EnableDoubleBuffered(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgv, new object[] { true });
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            SetupGridStyle();
            LoadDashboardMetrics();
            LoadRecentMovements();
        }

        private void LoadDashboardMetrics()
        {
            var products = ProductRepository.GetAll();
            lblTotalNum.Text    = products.Count.ToString("N0");
            lblLowStockNum.Text = products.Count(p => p.Quantity <= 10).ToString("N0");
        }

        private void LoadRecentMovements()
        {
            dgvRecentActions.Rows.Clear();
            dgvRecentActions.AutoGenerateColumns = false;

            var movements = StockMovementRepository.GetAll().Take(10).ToList();
            var productDict = ProductRepository.GetAll()
                .ToDictionary(p => p.SerialNumber, p => p.ProductName);

            foreach (var m in movements)
            {
                productDict.TryGetValue(m.ProductSerial, out string pName);
                string productDetails = $"{pName ?? m.ProductSerial} [{m.ProductSerial}]";
                string operatorName   = m.Username ?? "System";
                string formattedQty   = m.QuantityChanged > 0
                    ? $"+{m.QuantityChanged}"
                    : m.QuantityChanged.ToString();

                dgvRecentActions.Rows.Add(
                    productDetails,
                    m.MovementType,
                    formattedQty,
                    m.MovementDate.ToString("MMM dd, hh:mm tt"),
                    operatorName
                );
            }

            dgvRecentActions.ClearSelection();
        }

        private void SetupGridStyle()
        {
            dgvRecentActions.EnableHeadersVisualStyles = false;
            dgvRecentActions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvRecentActions.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 114, 128);
            dgvRecentActions.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvRecentActions.ColumnHeadersDefaultCellStyle.Padding   = new Padding(10, 5, 10, 5);
            dgvRecentActions.ColumnHeadersHeight                     = 50;
            dgvRecentActions.ColumnHeadersBorderStyle                = DataGridViewHeaderBorderStyle.None;
            dgvRecentActions.DefaultCellStyle.BackColor              = Color.White;
            dgvRecentActions.DefaultCellStyle.ForeColor              = Color.FromArgb(55, 65, 81);
            dgvRecentActions.DefaultCellStyle.Font                   = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgvRecentActions.DefaultCellStyle.SelectionBackColor     = Color.FromArgb(243, 244, 246);
            dgvRecentActions.DefaultCellStyle.SelectionForeColor     = Color.FromArgb(17, 24, 39);
            dgvRecentActions.DefaultCellStyle.Padding                = new Padding(10, 5, 10, 5);
            dgvRecentActions.RowTemplate.Height                      = 45;
            dgvRecentActions.BackgroundColor                         = Color.White;
            dgvRecentActions.BorderStyle                             = BorderStyle.None;
            dgvRecentActions.CellBorderStyle                         = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecentActions.GridColor                               = Color.FromArgb(229, 231, 235);
            dgvRecentActions.RowHeadersVisible                       = false;
            dgvRecentActions.AllowUserToAddRows                      = false;
            dgvRecentActions.AllowUserToResizeRows                   = false;
            dgvRecentActions.SelectionMode                           = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentActions.MultiSelect                             = false;
        }

        private void DgvRecentActions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;
            string col = dgvRecentActions.Columns[e.ColumnIndex].Name;

            if (col == "Type")
            {
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
                switch (e.Value.ToString())
                {
                    case "StockIn":
                    case "Restock":
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); break;
                    case "StockOut":
                    case "ReturnToSupplier":
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246); break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128); break;
                }
            }

            if (col == "Quantity")
                e.CellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        }

        private FrmMain FindMainForm()
        {
            var mainForm = this.TopLevelControl as FrmMain ?? this.Parent as FrmMain;
            if (mainForm != null) return mainForm;
            System.Windows.Forms.Control p = this.Parent;
            while (p != null && !(p is FrmMain)) p = p.Parent;
            if (p is FrmMain fm) return fm;
            foreach (Form f in Application.OpenForms)
                if (f is FrmMain frmMain) return frmMain;
            return null;
        }

        private void PnlLowStock_Click(object sender, EventArgs e)
        {
            var mainForm = FindMainForm();
            var form     = new FrmProducts { ShowLowStockFilter = true };
            if (mainForm != null) mainForm.OpenChildForm(form);
            else form.Show();
        }

        private void PnlTotalProducts_Click(object sender, EventArgs e)
        {
            var mainForm = FindMainForm();
            var form     = new FrmProducts { ForceShowAll = true };
            if (mainForm != null) mainForm.OpenChildForm(form);
            else form.Show();
        }

        private void pnlTotalProducts_Paint(object sender, PaintEventArgs e) { }
    }
}
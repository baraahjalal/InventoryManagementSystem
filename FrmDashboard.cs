using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
            EnableDoubleBuffered(dgvRecentActions); 


            // Wire up the cell formatting event to add custom colors
            dgvRecentActions.CellFormatting += DgvRecentActions_CellFormatting;
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
        // Keep this to avoid compilation errors from the Designer if it's currently linked
        private void lblMainTitle_Click(object sender, EventArgs e)
        {
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            SetupGridStyle();
            LoadStaticData();
        }

        private void SetupGridStyle()
        {
            // Professional DataGridView Styling overrides
            dgvRecentActions.EnableHeadersVisualStyles = false;

            // Header Styling - Clean and minimal
            dgvRecentActions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvRecentActions.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 114, 128);
            dgvRecentActions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvRecentActions.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvRecentActions.ColumnHeadersHeight = 50;
            dgvRecentActions.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Row Styling - Spacious and readable
            dgvRecentActions.DefaultCellStyle.BackColor = Color.White;
            dgvRecentActions.DefaultCellStyle.ForeColor = Color.FromArgb(55, 65, 81);
            dgvRecentActions.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgvRecentActions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246); // Very subtle gray for selection
            dgvRecentActions.DefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 24, 39);
            dgvRecentActions.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvRecentActions.RowTemplate.Height = 45;

            // General Grid Settings
            dgvRecentActions.BackgroundColor = Color.White;
            dgvRecentActions.BorderStyle = BorderStyle.None;
            dgvRecentActions.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecentActions.GridColor = Color.FromArgb(229, 231, 235); // Light separator lines
            dgvRecentActions.RowHeadersVisible = false;
            dgvRecentActions.AllowUserToAddRows = false;
            dgvRecentActions.AllowUserToResizeRows = false;
            dgvRecentActions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentActions.MultiSelect = false;
        }

        private void LoadStaticData()
        {
            // Creating realistic static data for visualization
            DataTable dt = new DataTable();
            dt.Columns.Add("Product Details");
            dt.Columns.Add("Type");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Timestamp");
            dt.Columns.Add("Operator");

            // Adding dummy rows for the mockup
            dt.Rows.Add("MacBook Pro 14\" M3 (SN: APP-MBP-2023)", "STOCK IN", "+12", "Apr 13, 10:42 AM", "Sarah Jenkins");
            dt.Rows.Add("iPhone 15 Pro Max (SN: APP-IPH-2023)", "STOCK OUT", "-4", "Apr 13, 09:15 AM", "Mike Ross");
            dt.Rows.Add("HP LaserJet Pro (SN: PRN-HP-2024)", "LOW STOCK", "2", "Apr 12, 04:30 PM", "System Alert");
            dt.Rows.Add("iPad Air 5th Gen (SN: APP-IPD-2023)", "STOCK IN", "+25", "Apr 12, 11:20 AM", "Sarah Jenkins");
            dt.Rows.Add("Dell XPS 15 (SN: DELL-XPS-2023)", "STOCK OUT", "-1", "Apr 11, 02:10 PM", "Mike Ross");
            dt.Rows.Add("Logitech MX Master 3S (SN: LOGI-MX-3S)", "RESTOCK", "+50", "Apr 11, 09:00 AM", "System Auto");

            dgvRecentActions.DataSource = dt;

            // Adjust relative column widths to look like a clean dashboard widget
            if (dgvRecentActions.Columns.Count > 0)
            {
                dgvRecentActions.Columns["Product Details"].FillWeight = 40;
                dgvRecentActions.Columns["Type"].FillWeight = 15;
                dgvRecentActions.Columns["Quantity"].FillWeight = 15;
                dgvRecentActions.Columns["Timestamp"].FillWeight = 15;
                dgvRecentActions.Columns["Operator"].FillWeight = 15;
            }
        }

        private void DgvRecentActions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if we are formatting the "Type" column
            if (dgvRecentActions.Columns[e.ColumnIndex].Name == "Type" && e.Value != null)
            {
                string status = e.Value.ToString();

                // Emphasize the status text slightly
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);

                // Assign professional colors based on status type
                switch (status)
                {
                    case "STOCK IN":
                    case "RESTOCK":
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); // Vibrant Green
                        break;
                    case "STOCK OUT":
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246); // Clean Blue
                        break;
                    case "LOW STOCK":
                        e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68); // Alert Red
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128); // Standard Gray
                        break;
                }
            }

            // Emphasize the Quantity Column to stand out visually
            if (dgvRecentActions.Columns[e.ColumnIndex].Name == "Quantity" && e.Value != null)
            {
                e.CellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            }
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using InventoryManagementSystem.Reports;

namespace InventoryManagementSystem
{
    public partial class FrmReports : Form
    {
        private static readonly Color CardBorderColor = Color.FromArgb(229, 231, 235);

        public FrmReports()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Card_Paint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using (var pen = new Pen(CardBorderColor))
                e.Graphics.DrawRectangle(pen, rect);
        }

        private void btnReport1_Click(object sender, EventArgs e) =>
            OpenReport(new StockInventoryStatus(), "Stock Inventory Status");

        private void btnReport2_Click(object sender, EventArgs e) =>
            OpenReport(new StockMovementHistory(), "Stock Movements History");

        private void OpenReport(ReportClass report, string title)
        {
            try
            {
                var viewer = new FrmReportViewer(report, title);
                var main   = FindForm() as FrmMain;
                if (main != null)
                    main.OpenChildForm(viewer);
                else
                    viewer.ShowDialog(this);
            }
            catch (Exception ex)
            {
                report?.Close();
                report?.Dispose();

                MessageBox.Show(
                    "Could not open the report.\r\n\r\n" + ex.Message,
                    "Reports",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

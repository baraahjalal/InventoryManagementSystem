using CrystalDecisions.CrystalReports.Engine;
using InventoryManagementSystem.Classes;
using InventoryManagementSystem.DAL;
using System;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmReportViewer : Form
    {
        private readonly ReportDocument _report;

        public FrmReportViewer(ReportDocument report, string title)
        {
            InitializeComponent();
            _report = report ?? throw new ArgumentNullException(nameof(report));

            lblTitle.Text = title;
            Text          = title;

            try
            {
                CrystalReportHelper.ApplyDatabaseLogon(_report);
                CrystalReportHelper.ApplySessionParameters(_report);
                crystalReportViewer.ParameterFieldInfo = CrystalReportHelper.BuildViewerParameterFields();
                _report.Refresh();
                crystalReportViewer.ReportSource = _report;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load the report.\r\n\r\n" + ex.Message,
                    "Report Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var main = FindForm() as FrmMain;
            if (main != null)
                main.OpenChildForm(new FrmReports());
            else
                Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                crystalReportViewer.ReportSource = null;
                _report?.Close();
                _report?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}

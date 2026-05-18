using System;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;

namespace InventoryManagementSystem
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private bool        isExpanded = true;
        private const int   MinWidth   = 70;
        private const int   MaxWidth   = 235;
        private const int   AnimSpeed  = 20;

        public void OpenChildForm(Form childForm)
        {
            if (pnlMainContent.Controls.Count > 0)
                pnlMainContent.Controls.Clear();

            childForm.TopLevel        = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock            = DockStyle.Fill;

            pnlMainContent.Controls.Add(childForm);
            pnlMainContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)   => OpenChildForm(new FrmDashboard());
        private void btnProducts_Click(object sender, EventArgs e)     => OpenChildForm(new FrmProducts());
        private void btnStockIn_Click(object sender, EventArgs e)      => OpenChildForm(new FrmStockIn());
        private void btnStockOut_Click(object sender, EventArgs e)     => OpenChildForm(new FrmStockOut());
        private void btnSuppliersManagement_Click(object sender, EventArgs e) => OpenChildForm(new FrmSupplierManagement());
        private void btnReports_Click(object sender, EventArgs e)      => OpenChildForm(new FrmReports());

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            var user = DatabaseHelper.CurrentUser;
            if (user == null || !user.IsAdmin)
            {
                MessageBox.Show("You do not have permission to access this feature.", "Permission Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new FrmUsers());
        }

        private void btnAuditLog_Click(object sender, EventArgs e)
        {
            var user = DatabaseHelper.CurrentUser;
            if (user == null || !user.IsAdmin)
            {
                MessageBox.Show("You do not have permission to access this feature.", "Permission Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new FrmAuditLog());
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (isExpanded)
            {
                pnlSideBar.Width -= AnimSpeed;
                if (pnlSideBar.Width <= MinWidth)
                {
                    pnlSideBar.Width = MinWidth;
                    isExpanded       = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                pnlSideBar.Width += AnimSpeed;
                if (pnlSideBar.Width >= MaxWidth)
                {
                    pnlSideBar.Width = MaxWidth;
                    isExpanded       = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void picNav_Click(object sender, EventArgs e) => sidebarTimer.Start();

        private void FrmMain_Load(object sender, EventArgs e) { }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                using (var exitDialog = new Forms.FrmExitDialog())
                {
                    exitDialog.ShowDialog(this);
                    if (exitDialog.SelectedAction == Forms.ExitAction.Cancel)
                        e.Cancel = true;
                    else if (exitDialog.SelectedAction == Forms.ExitAction.LogOut)
                    {
                        e.Cancel = true;
                        Application.Restart();
                    }
                    else if (exitDialog.SelectedAction == Forms.ExitAction.CloseSystem)
                        Application.Exit();
                }
            }
        }
    }
}

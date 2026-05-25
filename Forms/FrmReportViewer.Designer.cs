namespace InventoryManagementSystem
{
    partial class FrmReportViewer
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.pnlTop              = new System.Windows.Forms.Panel();
            this.lblTitle            = new System.Windows.Forms.Label();
            this.btnBack             = new System.Windows.Forms.Button();
            this.crystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTop
            //
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.pnlTop.Controls.Add(this.btnBack);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location  = new System.Drawing.Point(0, 0);
            this.pnlTop.Name      = "pnlTop";
            this.pnlTop.Size      = new System.Drawing.Size(1100, 48);
            this.pnlTop.TabIndex  = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize  = true;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(16, 12);
            this.lblTitle.Name      = "lblTitle";
            this.lblTitle.TabIndex  = 0;
            this.lblTitle.Text      = "Report";
            //
            // btnBack
            //
            this.btnBack.Anchor                          = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBack.BackColor                       = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnBack.Cursor                          = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatStyle                       = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize       = 0;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnBack.Font                            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnBack.ForeColor                       = System.Drawing.Color.White;
            this.btnBack.Location                        = new System.Drawing.Point(960, 8);
            this.btnBack.Name                            = "btnBack";
            this.btnBack.Size                            = new System.Drawing.Size(124, 32);
            this.btnBack.TabIndex                        = 1;
            this.btnBack.Text                            = "← Back to Reports";
            this.btnBack.UseVisualStyleBackColor         = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            //
            // crystalReportViewer
            //
            this.crystalReportViewer.ActiveViewIndex = -1;
            this.crystalReportViewer.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer.Cursor          = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer.Dock            = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer.Location        = new System.Drawing.Point(0, 48);
            this.crystalReportViewer.Name            = "crystalReportViewer";
            this.crystalReportViewer.ShowCloseButton = false;
            this.crystalReportViewer.ShowGroupTreeButton = false;
            this.crystalReportViewer.ShowLogo        = false;
            this.crystalReportViewer.Size            = new System.Drawing.Size(1100, 632);
            this.crystalReportViewer.TabIndex        = 1;
            this.crystalReportViewer.ToolPanelView   = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            //
            // FrmReportViewer
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor     = System.Drawing.Color.White;
            this.ClientSize    = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.crystalReportViewer);
            this.Controls.Add(this.pnlTop);
            this.Name          = "FrmReportViewer";
            this.Text          = "Report";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnBack;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer;
    }
}

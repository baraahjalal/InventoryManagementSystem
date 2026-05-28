namespace InventoryManagementSystem
{
    partial class FrmReports
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader     = new System.Windows.Forms.Panel();
            this.lblTitle      = new System.Windows.Forms.Label();
            this.lblSubTitle   = new System.Windows.Forms.Label();

            this.pnlCard1      = new System.Windows.Forms.Panel();
            this.pnlBadge1     = new System.Windows.Forms.Panel();
            this.lblBadge1     = new System.Windows.Forms.Label();
            this.lblCardTitle1 = new System.Windows.Forms.Label();
            this.lblCardDesc1  = new System.Windows.Forms.Label();
            this.btnReport1    = new System.Windows.Forms.Button();

            this.pnlCard2      = new System.Windows.Forms.Panel();
            this.pnlBadge2     = new System.Windows.Forms.Panel();
            this.lblBadge2     = new System.Windows.Forms.Label();
            this.lblCardTitle2 = new System.Windows.Forms.Label();
            this.lblCardDesc2  = new System.Windows.Forms.Label();
            this.btnReport2    = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBadge1.SuspendLayout();
            this.pnlCard1.SuspendLayout();
            this.pnlBadge2.SuspendLayout();
            this.pnlCard2.SuspendLayout();
            this.SuspendLayout();

            // ── Header ──────────────────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock     = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name     = "pnlHeader";
            this.pnlHeader.Size     = new System.Drawing.Size(1100, 120);
            this.pnlHeader.TabIndex = 0;

            this.lblTitle.AutoSize  = true;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(24, 18);
            this.lblTitle.Name      = "lblTitle";
            this.lblTitle.TabIndex  = 0;
            this.lblTitle.Text      = "Reports";

            this.lblSubTitle.AutoSize  = true;
            this.lblSubTitle.Font      = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubTitle.Location  = new System.Drawing.Point(28, 76);
            this.lblSubTitle.Name      = "lblSubTitle";
            this.lblSubTitle.TabIndex  = 1;
            this.lblSubTitle.Text      = "Generate and print inventory reports";

            // ── Card 1 — Stock Inventory Status ─────────────────────────────
            this.pnlBadge1.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.pnlBadge1.Controls.Add(this.lblBadge1);
            this.pnlBadge1.Location  = new System.Drawing.Point(24, 24);
            this.pnlBadge1.Name      = "pnlBadge1";
            this.pnlBadge1.Size      = new System.Drawing.Size(52, 52);
            this.pnlBadge1.TabIndex  = 0;

            this.lblBadge1.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblBadge1.Font      = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBadge1.ForeColor = System.Drawing.Color.White;
            this.lblBadge1.Name      = "lblBadge1";
            this.lblBadge1.TabIndex  = 0;
            this.lblBadge1.Text      = "S";
            this.lblBadge1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblCardTitle1.AutoSize  = true;
            this.lblCardTitle1.Font      = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblCardTitle1.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblCardTitle1.Location  = new System.Drawing.Point(24, 90);
            this.lblCardTitle1.Name      = "lblCardTitle1";
            this.lblCardTitle1.TabIndex  = 1;
            this.lblCardTitle1.Text      = "Stock Inventory Status";

            this.lblCardDesc1.AutoSize  = false;
            this.lblCardDesc1.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCardDesc1.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCardDesc1.Location  = new System.Drawing.Point(24, 118);
            this.lblCardDesc1.Name      = "lblCardDesc1";
            this.lblCardDesc1.Size      = new System.Drawing.Size(442, 44);
            this.lblCardDesc1.TabIndex  = 2;
            this.lblCardDesc1.Text      = "Current snapshot of all products — quantities, prices, and stock levels grouped by category.";

            this.btnReport1.BackColor                         = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnReport1.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnReport1.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport1.FlatAppearance.BorderSize         = 0;
            this.btnReport1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnReport1.Font                              = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReport1.ForeColor                         = System.Drawing.Color.White;
            this.btnReport1.Location                          = new System.Drawing.Point(24, 170);
            this.btnReport1.Name                              = "btnReport1";
            this.btnReport1.Size                              = new System.Drawing.Size(195, 36);
            this.btnReport1.TabIndex                          = 3;
            this.btnReport1.Text                              = "Generate Report  →";
            this.btnReport1.UseVisualStyleBackColor           = false;
            this.btnReport1.Click += new System.EventHandler(this.btnReport1_Click);

            this.pnlCard1.BackColor = System.Drawing.Color.White;
            this.pnlCard1.Controls.Add(this.pnlBadge1);
            this.pnlCard1.Controls.Add(this.lblCardTitle1);
            this.pnlCard1.Controls.Add(this.lblCardDesc1);
            this.pnlCard1.Controls.Add(this.btnReport1);
            this.pnlCard1.Location  = new System.Drawing.Point(40, 160);
            this.pnlCard1.Name      = "pnlCard1";
            this.pnlCard1.Size      = new System.Drawing.Size(490, 222);
            this.pnlCard1.TabIndex  = 1;
            this.pnlCard1.Paint    += new System.Windows.Forms.PaintEventHandler(this.Card_Paint);

            // ── Card 2 — Stock Movements History ────────────────────────────
            this.pnlBadge2.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.pnlBadge2.Controls.Add(this.lblBadge2);
            this.pnlBadge2.Location  = new System.Drawing.Point(24, 24);
            this.pnlBadge2.Name      = "pnlBadge2";
            this.pnlBadge2.Size      = new System.Drawing.Size(52, 52);
            this.pnlBadge2.TabIndex  = 0;

            this.lblBadge2.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblBadge2.Font      = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBadge2.ForeColor = System.Drawing.Color.White;
            this.lblBadge2.Name      = "lblBadge2";
            this.lblBadge2.TabIndex  = 0;
            this.lblBadge2.Text      = "M";
            this.lblBadge2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblCardTitle2.AutoSize  = true;
            this.lblCardTitle2.Font      = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblCardTitle2.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblCardTitle2.Location  = new System.Drawing.Point(24, 90);
            this.lblCardTitle2.Name      = "lblCardTitle2";
            this.lblCardTitle2.TabIndex  = 1;
            this.lblCardTitle2.Text      = "Stock Movements History";

            this.lblCardDesc2.AutoSize  = false;
            this.lblCardDesc2.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCardDesc2.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCardDesc2.Location  = new System.Drawing.Point(24, 118);
            this.lblCardDesc2.Name      = "lblCardDesc2";
            this.lblCardDesc2.Size      = new System.Drawing.Size(442, 44);
            this.lblCardDesc2.TabIndex  = 2;
            this.lblCardDesc2.Text      = "All StockIn, StockOut, Restock and Return movements, filterable by date range.";

            this.btnReport2.BackColor                         = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnReport2.Cursor                            = System.Windows.Forms.Cursors.Hand;
            this.btnReport2.FlatStyle                         = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport2.FlatAppearance.BorderSize         = 0;
            this.btnReport2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnReport2.Font                              = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReport2.ForeColor                         = System.Drawing.Color.White;
            this.btnReport2.Location                          = new System.Drawing.Point(24, 170);
            this.btnReport2.Name                              = "btnReport2";
            this.btnReport2.Size                              = new System.Drawing.Size(195, 36);
            this.btnReport2.TabIndex                          = 3;
            this.btnReport2.Text                              = "Generate Report  →";
            this.btnReport2.UseVisualStyleBackColor           = false;
            this.btnReport2.Click += new System.EventHandler(this.btnReport2_Click);

            this.pnlCard2.BackColor = System.Drawing.Color.White;
            this.pnlCard2.Controls.Add(this.pnlBadge2);
            this.pnlCard2.Controls.Add(this.lblCardTitle2);
            this.pnlCard2.Controls.Add(this.lblCardDesc2);
            this.pnlCard2.Controls.Add(this.btnReport2);
            this.pnlCard2.Location  = new System.Drawing.Point(570, 160);
            this.pnlCard2.Name      = "pnlCard2";
            this.pnlCard2.Size      = new System.Drawing.Size(490, 222);
            this.pnlCard2.TabIndex  = 2;
            this.pnlCard2.Paint    += new System.Windows.Forms.PaintEventHandler(this.Card_Paint);

            // ── Form ────────────────────────────────────────────────────────
            this.AutoScaleMode  = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor      = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize     = new System.Drawing.Size(1100, 420);
            this.Controls.Add(this.pnlCard1);
            this.Controls.Add(this.pnlCard2);
            this.Controls.Add(this.pnlHeader);
            this.Name           = "FrmReports";
            this.StartPosition  = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text           = "Reports";

            this.pnlBadge1.ResumeLayout(false);
            this.pnlBadge1.PerformLayout();
            this.pnlCard1.ResumeLayout(false);
            this.pnlCard1.PerformLayout();
            this.pnlBadge2.ResumeLayout(false);
            this.pnlBadge2.PerformLayout();
            this.pnlCard2.ResumeLayout(false);
            this.pnlCard2.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;

        private System.Windows.Forms.Panel pnlCard1;
        private System.Windows.Forms.Panel pnlBadge1;
        private System.Windows.Forms.Label lblBadge1;
        private System.Windows.Forms.Label lblCardTitle1;
        private System.Windows.Forms.Label lblCardDesc1;
        private System.Windows.Forms.Button btnReport1;

        private System.Windows.Forms.Panel pnlCard2;
        private System.Windows.Forms.Panel pnlBadge2;
        private System.Windows.Forms.Label lblBadge2;
        private System.Windows.Forms.Label lblCardTitle2;
        private System.Windows.Forms.Label lblCardDesc2;
        private System.Windows.Forms.Button btnReport2;
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    partial class FrmDataMaintenance
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle           = new System.Windows.Forms.Label();
            this.lblSubtitle        = new System.Windows.Forms.Label();
            this.pnlSeparator       = new System.Windows.Forms.Panel();
            this.chkEnabled         = new System.Windows.Forms.CheckBox();
            this.lblRetentionYears  = new System.Windows.Forms.Label();
            this.cmbRetentionYears  = new System.Windows.Forms.ComboBox();
            this.lblRetentionDesc   = new System.Windows.Forms.Label();
            this.lblWarnInterval    = new System.Windows.Forms.Label();
            this.cmbWarnInterval    = new System.Windows.Forms.ComboBox();
            this.lblWarnDesc        = new System.Windows.Forms.Label();
            this.pnlSeparator2      = new System.Windows.Forms.Panel();
            this.lblNote            = new System.Windows.Forms.Label();
            this.btnCancel          = new System.Windows.Forms.Button();
            this.btnSave            = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize  = true;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblTitle.Location  = new System.Drawing.Point(24, 24);
            this.lblTitle.Name      = "lblTitle";
            this.lblTitle.Text      = "Data Retention Settings";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize  = true;
            this.lblSubtitle.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Location  = new System.Drawing.Point(26, 60);
            this.lblSubtitle.Name      = "lblSubtitle";
            this.lblSubtitle.Text      = "Configure how long data is kept and how far in advance you are warned.";
            //
            // pnlSeparator
            //
            this.pnlSeparator.BackColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.pnlSeparator.Location  = new System.Drawing.Point(24, 90);
            this.pnlSeparator.Name      = "pnlSeparator";
            this.pnlSeparator.Size      = new System.Drawing.Size(432, 1);
            //
            // chkEnabled
            //
            this.chkEnabled.AutoSize  = true;
            this.chkEnabled.Font      = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.chkEnabled.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.chkEnabled.Location  = new System.Drawing.Point(26, 108);
            this.chkEnabled.Name      = "chkEnabled";
            this.chkEnabled.Text      = "Enable automatic data purging";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.ChkEnabled_CheckedChanged);
            //
            // lblRetentionYears
            //
            this.lblRetentionYears.AutoSize  = true;
            this.lblRetentionYears.Font      = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblRetentionYears.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblRetentionYears.Location  = new System.Drawing.Point(26, 150);
            this.lblRetentionYears.Name      = "lblRetentionYears";
            this.lblRetentionYears.Text      = "Auto-delete data older than:";
            //
            // cmbRetentionYears
            //
            this.cmbRetentionYears.DropDownStyle      = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRetentionYears.Font               = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cmbRetentionYears.ForeColor          = System.Drawing.Color.FromArgb(55, 65, 81);
            this.cmbRetentionYears.FormattingEnabled  = true;
            this.cmbRetentionYears.Items.AddRange(new object[] { "1 Year", "2 Years", "3 Years", "5 Years" });
            this.cmbRetentionYears.Location           = new System.Drawing.Point(260, 147);
            this.cmbRetentionYears.Name               = "cmbRetentionYears";
            this.cmbRetentionYears.Size               = new System.Drawing.Size(196, 27);
            this.cmbRetentionYears.TabIndex           = 1;
            //
            // lblRetentionDesc
            //
            this.lblRetentionDesc.AutoSize  = true;
            this.lblRetentionDesc.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRetentionDesc.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.lblRetentionDesc.Location  = new System.Drawing.Point(28, 178);
            this.lblRetentionDesc.Name      = "lblRetentionDesc";
            this.lblRetentionDesc.Text      = "Records older than this will be deleted automatically on app startup.";
            //
            // lblWarnInterval
            //
            this.lblWarnInterval.AutoSize  = true;
            this.lblWarnInterval.Font      = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblWarnInterval.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblWarnInterval.Location  = new System.Drawing.Point(26, 210);
            this.lblWarnInterval.Name      = "lblWarnInterval";
            this.lblWarnInterval.Text      = "Warn me before deletion:";
            //
            // cmbWarnInterval
            //
            this.cmbWarnInterval.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarnInterval.Font              = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cmbWarnInterval.ForeColor         = System.Drawing.Color.FromArgb(55, 65, 81);
            this.cmbWarnInterval.FormattingEnabled = true;
            this.cmbWarnInterval.Items.AddRange(new object[] { "1 Month", "3 Months", "6 Months", "1 Year" });
            this.cmbWarnInterval.Location          = new System.Drawing.Point(260, 207);
            this.cmbWarnInterval.Name              = "cmbWarnInterval";
            this.cmbWarnInterval.Size              = new System.Drawing.Size(196, 27);
            this.cmbWarnInterval.TabIndex          = 2;
            //
            // lblWarnDesc
            //
            this.lblWarnDesc.AutoSize  = true;
            this.lblWarnDesc.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarnDesc.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.lblWarnDesc.Location  = new System.Drawing.Point(28, 238);
            this.lblWarnDesc.Name      = "lblWarnDesc";
            this.lblWarnDesc.Text      = "A warning will appear when deletion is this many months away.";
            //
            // pnlSeparator2
            //
            this.pnlSeparator2.BackColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.pnlSeparator2.Location  = new System.Drawing.Point(24, 268);
            this.pnlSeparator2.Name      = "pnlSeparator2";
            this.pnlSeparator2.Size      = new System.Drawing.Size(432, 1);
            //
            // lblNote
            //
            this.lblNote.AutoSize  = true;
            this.lblNote.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblNote.Location  = new System.Drawing.Point(26, 280);
            this.lblNote.Name      = "lblNote";
            this.lblNote.Text      = "Purge runs automatically on startup — deleted data cannot be recovered.";
            //
            // btnCancel
            //
            this.btnCancel.BackColor                 = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font                      = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor                 = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnCancel.Location                  = new System.Drawing.Point(24, 314);
            this.btnCancel.Name                      = "btnCancel";
            this.btnCancel.Size                      = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex                  = 3;
            this.btnCancel.Text                      = "Cancel";
            this.btnCancel.UseVisualStyleBackColor   = false;
            this.btnCancel.Click                    += new System.EventHandler(this.BtnCancel_Click);
            //
            // btnSave
            //
            this.btnSave.BackColor                 = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font                      = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor                 = System.Drawing.Color.White;
            this.btnSave.Location                  = new System.Drawing.Point(356, 314);
            this.btnSave.Name                      = "btnSave";
            this.btnSave.Size                      = new System.Drawing.Size(100, 36);
            this.btnSave.TabIndex                  = 4;
            this.btnSave.Text                      = "Save Settings";
            this.btnSave.UseVisualStyleBackColor   = false;
            this.btnSave.Click                    += new System.EventHandler(this.BtnSave_Click);
            //
            // FrmDataMaintenance
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.White;
            this.ClientSize          = new System.Drawing.Size(480, 370);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.pnlSeparator);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.lblRetentionYears);
            this.Controls.Add(this.cmbRetentionYears);
            this.Controls.Add(this.lblRetentionDesc);
            this.Controls.Add(this.lblWarnInterval);
            this.Controls.Add(this.cmbWarnInterval);
            this.Controls.Add(this.lblWarnDesc);
            this.Controls.Add(this.pnlSeparator2);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle  = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox      = false;
            this.MinimizeBox      = false;
            this.Name             = "FrmDataMaintenance";
            this.StartPosition    = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text             = "Retention Settings";
            this.Load            += new System.EventHandler(this.FrmDataMaintenance_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label    lblTitle;
        private System.Windows.Forms.Label    lblSubtitle;
        private System.Windows.Forms.Panel    pnlSeparator;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Label    lblRetentionYears;
        private System.Windows.Forms.ComboBox cmbRetentionYears;
        private System.Windows.Forms.Label    lblRetentionDesc;
        private System.Windows.Forms.Label    lblWarnInterval;
        private System.Windows.Forms.ComboBox cmbWarnInterval;
        private System.Windows.Forms.Label    lblWarnDesc;
        private System.Windows.Forms.Panel    pnlSeparator2;
        private System.Windows.Forms.Label    lblNote;
        private System.Windows.Forms.Button   btnCancel;
        private System.Windows.Forms.Button   btnSave;
    }
}

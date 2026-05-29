using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    partial class FrmAuditLog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvAuditLog = new System.Windows.Forms.DataGridView();
            this.pnlFilterBar = new System.Windows.Forms.Panel();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cmbActionType = new System.Windows.Forms.ComboBox();
            this.cmbDateRange = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLog)).BeginInit();
            this.pnlFilterBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.dgvAuditLog);
            this.pnlMain.Controls.Add(this.pnlFilterBar);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(30);
            this.pnlMain.Size = new System.Drawing.Size(1000, 600);
            this.pnlMain.TabIndex = 0;
            // 
            // dgvAuditLog
            // 
            this.dgvAuditLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuditLog.Location = new System.Drawing.Point(30, 83);
            this.dgvAuditLog.Name = "dgvAuditLog";
            this.dgvAuditLog.Size = new System.Drawing.Size(940, 487);
            this.dgvAuditLog.TabIndex = 1;
            this.dgvAuditLog.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvAuditLog_CellFormatting);
            // 
            // pnlFilterBar
            // 
            this.pnlFilterBar.Controls.Add(this.btnFilter);
            this.pnlFilterBar.Controls.Add(this.cmbActionType);
            this.pnlFilterBar.Controls.Add(this.cmbDateRange);
            this.pnlFilterBar.Controls.Add(this.txtSearch);
            this.pnlFilterBar.Controls.Add(this.lblTitle);
            this.pnlFilterBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterBar.Location = new System.Drawing.Point(30, 30);
            this.pnlFilterBar.Name = "pnlFilterBar";
            this.pnlFilterBar.Size = new System.Drawing.Size(940, 53);
            this.pnlFilterBar.TabIndex = 0;
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(870, 2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(70, 29);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = false;
            // 
            // cmbActionType
            // 
            this.cmbActionType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cmbActionType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Items.AddRange(new object[] {
            "All Actions",
            "Stock In",
            "Stock Out",
            "Addition",
            "Modification",
            "Deletion"});
            this.cmbActionType.Location = new System.Drawing.Point(735, 3);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(120, 27);
            this.cmbActionType.TabIndex = 3;
            // 
            // cmbDateRange
            // 
            this.cmbDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDateRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateRange.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cmbDateRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.cmbDateRange.FormattingEnabled = true;
            this.cmbDateRange.Items.AddRange(new object[] {
            "Today",
            "Last 7 Days",
            "Last 30 Days",
            "This Year"});
            this.cmbDateRange.Location = new System.Drawing.Point(600, 3);
            this.cmbDateRange.Name = "cmbDateRange";
            this.cmbDateRange.Size = new System.Drawing.Size(120, 27);
            this.cmbDateRange.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.txtSearch.Location = new System.Drawing.Point(335, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Search entities or users...";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(22, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(114, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Audit Log";
            // 
            // FrmAuditLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAuditLog";
            this.Text = "Audit Log";
            this.Load += new System.EventHandler(this.FrmAuditLog_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLog)).EndInit();
            this.pnlFilterBar.ResumeLayout(false);
            this.pnlFilterBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlFilterBar;
        private System.Windows.Forms.DataGridView dgvAuditLog;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbDateRange;
        private System.Windows.Forms.ComboBox cmbActionType;
        private System.Windows.Forms.Button btnFilter;

        // Custom Layout and Behavior Logic Moved from Code-Behind
        private void FrmAuditLog_Load(object sender, EventArgs e)
        {
            EnableDoubleBuffered(dgvAuditLog);
            SetupGridStyle();
            LoadStaticData();
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

        private void SetupGridStyle()
        {
            dgvAuditLog.EnableHeadersVisualStyles = false;

            dgvAuditLog.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvAuditLog.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 114, 128);
            dgvAuditLog.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvAuditLog.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvAuditLog.ColumnHeadersHeight = 50;
            dgvAuditLog.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvAuditLog.DefaultCellStyle.BackColor = Color.White;
            dgvAuditLog.DefaultCellStyle.ForeColor = Color.FromArgb(55, 65, 81);
            dgvAuditLog.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgvAuditLog.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            dgvAuditLog.DefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 24, 39);
            dgvAuditLog.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvAuditLog.RowTemplate.Height = 45;

            dgvAuditLog.BackgroundColor = Color.White;
            dgvAuditLog.BorderStyle = BorderStyle.None;
            dgvAuditLog.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAuditLog.GridColor = Color.FromArgb(229, 231, 235);
            dgvAuditLog.RowHeadersVisible = false;
            dgvAuditLog.AllowUserToAddRows = false;
            dgvAuditLog.AllowUserToResizeRows = false;
            dgvAuditLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditLog.MultiSelect = false;
            dgvAuditLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadStaticData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Timestamp");
            dt.Columns.Add("User Identity");
            dt.Columns.Add("Action Type");
            dt.Columns.Add("Target Entity");
            dt.Columns.Add("Modification Details");

            dt.Rows.Add("Apr 15, 08:32 AM", "Sarah Jenkins (Admin)", "STOCK IN", "MacBook Pro 14\" (APP-MBP-2023)", "Qty: 0 → 24");
            dt.Rows.Add("Apr 15, 09:15 AM", "Mike Ross (Manager)", "MODIFICATION", "Dell XPS 15 (DELL-XPS-2023)", "Price: $1,699 → $1,799");
            dt.Rows.Add("Apr 14, 02:40 PM", "System Auto", "DELETION", "HP LaserJet 1020", "Record Permanently Removed");
            dt.Rows.Add("Apr 14, 11:20 AM", "Sarah Jenkins (Admin)", "STOCK OUT", "iPad Air 5th Gen", "Qty: 35 → 30 (Assigned to IT)");
            dt.Rows.Add("Apr 14, 09:05 AM", "Mike Ross (Manager)", "ADDITION", "Supplier: Ingram Micro", "New Vendor Onboarded");

            dgvAuditLog.DataSource = dt;

            dgvAuditLog.Columns["Timestamp"].FillWeight = 15;
            dgvAuditLog.Columns["User Identity"].FillWeight = 20;
            dgvAuditLog.Columns["Action Type"].FillWeight = 15;
            dgvAuditLog.Columns["Target Entity"].FillWeight = 25;
            dgvAuditLog.Columns["Modification Details"].FillWeight = 25;

            dgvAuditLog.ClearSelection();
        }

        private void DgvAuditLog_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null && dgvAuditLog.Columns[e.ColumnIndex].Name == "Action Type")
            {
                string action = e.Value.ToString();
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);

                switch (action)
                {
                    case "STOCK IN":
                    case "ADDITION":
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); // Vibrant Green
                        break;
                    case "STOCK OUT":
                    case "MODIFICATION":
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246); // Clean Blue
                        break;
                    case "DELETION":
                        e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68); // Alert Red
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128); // Standard Gray
                        break;
                }
            }
        }
    }
}
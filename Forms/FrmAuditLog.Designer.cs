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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvAuditLog = new System.Windows.Forms.DataGridView();
            this.pnlFilterBar = new System.Windows.Forms.Panel();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cmbActionType = new System.Windows.Forms.ComboBox();
            this.cmbDateRange = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.colTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserIdentity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dgvAuditLog.AllowUserToAddRows = false;
            this.dgvAuditLog.AllowUserToResizeRows = false;
            this.dgvAuditLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAuditLog.BackgroundColor = System.Drawing.Color.White;
            this.dgvAuditLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAuditLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAuditLog.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAuditLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAuditLog.ColumnHeadersHeight = 50;
            this.dgvAuditLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTimestamp,
            this.colUserIdentity,
            this.colActionType,
            this.colDescription});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAuditLog.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAuditLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuditLog.EnableHeadersVisualStyles = false;
            this.dgvAuditLog.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dgvAuditLog.Location = new System.Drawing.Point(30, 83);
            this.dgvAuditLog.MultiSelect = false;
            this.dgvAuditLog.Name = "dgvAuditLog";
            this.dgvAuditLog.ReadOnly = true;
            this.dgvAuditLog.RowHeadersVisible = false;
            this.dgvAuditLog.RowTemplate.Height = 45;
            this.dgvAuditLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAuditLog.Size = new System.Drawing.Size(940, 487);
            this.dgvAuditLog.TabIndex = 1;
            this.dgvAuditLog.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvAuditLog_CellFormatting);
            this.dgvAuditLog.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DgvAuditLog_RowPrePaint);
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
            // colTimestamp
            //
            this.colTimestamp.FillWeight = 15F;
            this.colTimestamp.HeaderText = "Timestamp";
            this.colTimestamp.Name = "colTimestamp";
            this.colTimestamp.ReadOnly = true;
            //
            // colUserIdentity
            //
            this.colUserIdentity.FillWeight = 15F;
            this.colUserIdentity.HeaderText = "Username";
            this.colUserIdentity.Name = "colUserIdentity";
            this.colUserIdentity.ReadOnly = true;
            //
            // colActionType
            //
            this.colActionType.FillWeight = 20F;
            this.colActionType.HeaderText = "Action";
            this.colActionType.Name = "colActionType";
            this.colActionType.ReadOnly = true;
            //
            // colDescription
            //
            this.colDescription.FillWeight = 50F;
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // FrmAuditLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAuditLog";
            this.Text = "Audit Log";
            this.Load += new System.EventHandler(this.FrmAuditLog_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLog)).EndInit();
            InventoryManagementSystem.Classes.AppTheme.ApplyStandardGrid(this.dgvAuditLog, headerHeight: 44, rowHeight: 40);
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
        private DataGridViewTextBoxColumn colTimestamp;
        private DataGridViewTextBoxColumn colUserIdentity;
        private DataGridViewTextBoxColumn colActionType;
        private DataGridViewTextBoxColumn colDescription;
    }
}

namespace InventoryManagementSystem.Forms
{
    partial class FrmAddCategory
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
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle cellStyle   = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader       = new System.Windows.Forms.Panel();
            this.lblTitle        = new System.Windows.Forms.Label();
            this.pnlFooter       = new System.Windows.Forms.Panel();
            this.btnCancel       = new System.Windows.Forms.Button();
            this.btnSave         = new System.Windows.Forms.Button();
            this.pnlBody         = new System.Windows.Forms.Panel();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblSpecsSection = new System.Windows.Forms.Label();
            this.lblSpecNote     = new System.Windows.Forms.Label();
            this.txtSpecKey      = new System.Windows.Forms.TextBox();
            this.btnAddSpecKey   = new System.Windows.Forms.Button();
            this.dgvSpecKeys     = new System.Windows.Forms.DataGridView();
            this.colSpecKeyName  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpecKeyRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecKeys)).BeginInit();
            this.SuspendLayout();
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(500, 60);
            this.pnlHeader.TabIndex = 0;
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New Category";
            // pnlFooter
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 520);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(500, 70);
            this.pnlFooter.TabIndex = 1;
            // btnCancel
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(265, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 38);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // btnSave
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(375, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 38);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // pnlBody
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.lblCategoryName);
            this.pnlBody.Controls.Add(this.txtCategoryName);
            this.pnlBody.Controls.Add(this.lblSpecsSection);
            this.pnlBody.Controls.Add(this.lblSpecNote);
            this.pnlBody.Controls.Add(this.txtSpecKey);
            this.pnlBody.Controls.Add(this.btnAddSpecKey);
            this.pnlBody.Controls.Add(this.dgvSpecKeys);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 60);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(25);
            this.pnlBody.Size = new System.Drawing.Size(500, 460);
            this.pnlBody.TabIndex = 2;
            // lblCategoryName
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCategoryName.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblCategoryName.Location = new System.Drawing.Point(21, 25);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.TabIndex = 0;
            this.lblCategoryName.Text = "Category Name:";
            // txtCategoryName
            this.txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCategoryName.Location = new System.Drawing.Point(25, 47);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(450, 27);
            this.txtCategoryName.TabIndex = 1;
            // lblSpecsSection
            this.lblSpecsSection.AutoSize = true;
            this.lblSpecsSection.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblSpecsSection.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblSpecsSection.Location = new System.Drawing.Point(21, 92);
            this.lblSpecsSection.Name = "lblSpecsSection";
            this.lblSpecsSection.TabIndex = 2;
            this.lblSpecsSection.Text = "Specification Keys (Optional)";
            // lblSpecNote
            this.lblSpecNote.AutoSize = true;
            this.lblSpecNote.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSpecNote.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.lblSpecNote.Location = new System.Drawing.Point(23, 115);
            this.lblSpecNote.Name = "lblSpecNote";
            this.lblSpecNote.TabIndex = 3;
            this.lblSpecNote.Text = "e.g. RAM, CPU, Storage, Color";
            // txtSpecKey
            this.txtSpecKey.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSpecKey.Location = new System.Drawing.Point(25, 138);
            this.txtSpecKey.Name = "txtSpecKey";
            this.txtSpecKey.Size = new System.Drawing.Size(340, 27);
            this.txtSpecKey.TabIndex = 4;
            // btnAddSpecKey
            this.btnAddSpecKey.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnAddSpecKey.FlatAppearance.BorderSize = 0;
            this.btnAddSpecKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSpecKey.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAddSpecKey.ForeColor = System.Drawing.Color.White;
            this.btnAddSpecKey.Location = new System.Drawing.Point(372, 135);
            this.btnAddSpecKey.Name = "btnAddSpecKey";
            this.btnAddSpecKey.Size = new System.Drawing.Size(103, 31);
            this.btnAddSpecKey.TabIndex = 5;
            this.btnAddSpecKey.Text = "Add Key";
            this.btnAddSpecKey.UseVisualStyleBackColor = false;
            this.btnAddSpecKey.Click += new System.EventHandler(this.btnAddSpecKey_Click);
            // dgvSpecKeys
            this.dgvSpecKeys.AllowUserToAddRows = false;
            this.dgvSpecKeys.AllowUserToDeleteRows = false;
            this.dgvSpecKeys.AllowUserToResizeRows = false;
            this.dgvSpecKeys.BackgroundColor = System.Drawing.Color.White;
            this.dgvSpecKeys.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSpecKeys.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSpecKeys.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            headerStyle.SelectionForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.dgvSpecKeys.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvSpecKeys.ColumnHeadersHeight = 35;
            this.dgvSpecKeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSpecKeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colSpecKeyName,
                this.colSpecKeyRemove });
            cellStyle.BackColor = System.Drawing.Color.White;
            cellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            cellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            cellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.dgvSpecKeys.DefaultCellStyle = cellStyle;
            this.dgvSpecKeys.EnableHeadersVisualStyles = false;
            this.dgvSpecKeys.Location = new System.Drawing.Point(25, 178);
            this.dgvSpecKeys.MultiSelect = false;
            this.dgvSpecKeys.Name = "dgvSpecKeys";
            this.dgvSpecKeys.RowHeadersVisible = false;
            this.dgvSpecKeys.RowTemplate.Height = 35;
            this.dgvSpecKeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpecKeys.Size = new System.Drawing.Size(450, 255);
            this.dgvSpecKeys.TabIndex = 6;
            this.dgvSpecKeys.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpecKeys_CellContentClick);
            // colSpecKeyName
            this.colSpecKeyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSpecKeyName.HeaderText = "Specification Key";
            this.colSpecKeyName.Name = "colSpecKeyName";
            this.colSpecKeyName.ReadOnly = true;
            // colSpecKeyRemove
            this.colSpecKeyRemove.HeaderText = "";
            this.colSpecKeyRemove.Name = "colSpecKeyRemove";
            this.colSpecKeyRemove.ReadOnly = true;
            this.colSpecKeyRemove.Text = "Remove";
            this.colSpecKeyRemove.UseColumnTextForButtonValue = true;
            this.colSpecKeyRemove.Width = 80;
            // FrmAddCategory
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 590);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Category";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecKeys)).EndInit();
            InventoryManagementSystem.Classes.AppTheme.ApplyStandardGrid(this.dgvSpecKeys, headerHeight: 35, rowHeight: 35);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.Label lblSpecsSection;
        private System.Windows.Forms.Label lblSpecNote;
        private System.Windows.Forms.TextBox txtSpecKey;
        private System.Windows.Forms.Button btnAddSpecKey;
        private System.Windows.Forms.DataGridView dgvSpecKeys;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpecKeyName;
        private System.Windows.Forms.DataGridViewButtonColumn colSpecKeyRemove;
    }
}

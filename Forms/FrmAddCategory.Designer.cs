namespace InventoryManagementSystem.Forms
{
    partial class FrmAddCategory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvFilters = new System.Windows.Forms.DataGridView();
            this.colFilterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFilterValues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlAddFilter = new System.Windows.Forms.Panel();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.txtFilterValues = new System.Windows.Forms.TextBox();
            this.lblFilterValues = new System.Windows.Forms.Label();
            this.txtFilterName = new System.Windows.Forms.TextBox();
            this.lblFilterName = new System.Windows.Forms.Label();
            this.lblFiltersSection = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilters)).BeginInit();
            this.pnlAddFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(500, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(175, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New Category";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 520);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(500, 70);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(265, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 38);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
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
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.dgvFilters);
            this.pnlBody.Controls.Add(this.pnlAddFilter);
            this.pnlBody.Controls.Add(this.lblFiltersSection);
            this.pnlBody.Controls.Add(this.txtCategoryName);
            this.pnlBody.Controls.Add(this.lblCategoryName);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 60);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(25);
            this.pnlBody.Size = new System.Drawing.Size(500, 460);
            this.pnlBody.TabIndex = 2;
            // 
            // dgvFilters
            // 
            this.dgvFilters.AllowUserToAddRows = false;
            this.dgvFilters.AllowUserToDeleteRows = false;
            this.dgvFilters.AllowUserToResizeRows = false;
            this.dgvFilters.BackgroundColor = System.Drawing.Color.White;
            this.dgvFilters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFilters.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvFilters.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFilters.ColumnHeadersHeight = 35;
            this.dgvFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFilters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFilterName,
            this.colFilterValues,
            this.colAction});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFilters.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFilters.EnableHeadersVisualStyles = false;
            this.dgvFilters.Location = new System.Drawing.Point(25, 275);
            this.dgvFilters.MultiSelect = false;
            this.dgvFilters.Name = "dgvFilters";
            this.dgvFilters.ReadOnly = true;
            this.dgvFilters.RowHeadersVisible = false;
            this.dgvFilters.RowTemplate.Height = 35;
            this.dgvFilters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilters.Size = new System.Drawing.Size(450, 160);
            this.dgvFilters.TabIndex = 4;
            this.dgvFilters.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilters_CellContentClick);
            // 
            // colFilterName
            // 
            this.colFilterName.HeaderText = "Filter Name";
            this.colFilterName.Name = "colFilterName";
            this.colFilterName.ReadOnly = true;
            this.colFilterName.Width = 120;
            // 
            // colFilterValues
            // 
            this.colFilterValues.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFilterValues.HeaderText = "Values (Options)";
            this.colFilterValues.Name = "colFilterValues";
            this.colFilterValues.ReadOnly = true;
            // 
            // colAction
            // 
            this.colAction.HeaderText = "";
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            this.colAction.Text = "Remove";
            this.colAction.UseColumnTextForButtonValue = true;
            this.colAction.Width = 80;
            // 
            // pnlAddFilter
            // 
            this.pnlAddFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlAddFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddFilter.Controls.Add(this.btnAddFilter);
            this.pnlAddFilter.Controls.Add(this.txtFilterValues);
            this.pnlAddFilter.Controls.Add(this.lblFilterValues);
            this.pnlAddFilter.Controls.Add(this.txtFilterName);
            this.pnlAddFilter.Controls.Add(this.lblFilterName);
            this.pnlAddFilter.Location = new System.Drawing.Point(25, 125);
            this.pnlAddFilter.Name = "pnlAddFilter";
            this.pnlAddFilter.Size = new System.Drawing.Size(450, 135);
            this.pnlAddFilter.TabIndex = 3;
            // 
            // btnAddFilter
            // 
            this.btnAddFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnAddFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnAddFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnAddFilter.Location = new System.Drawing.Point(345, 87);
            this.btnAddFilter.Name = "btnAddFilter";
            this.btnAddFilter.Size = new System.Drawing.Size(85, 27);
            this.btnAddFilter.TabIndex = 4;
            this.btnAddFilter.Text = "Add Filter";
            this.btnAddFilter.UseVisualStyleBackColor = false;
            this.btnAddFilter.Click += new System.EventHandler(this.btnAddFilter_Click);
            // 
            // txtFilterValues
            // 
            this.txtFilterValues.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilterValues.Location = new System.Drawing.Point(120, 52);
            this.txtFilterValues.Name = "txtFilterValues";
            this.txtFilterValues.Size = new System.Drawing.Size(310, 25);
            this.txtFilterValues.TabIndex = 3;
            // 
            // lblFilterValues
            // 
            this.lblFilterValues.AutoSize = true;
            this.lblFilterValues.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFilterValues.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblFilterValues.Location = new System.Drawing.Point(15, 55);
            this.lblFilterValues.Name = "lblFilterValues";
            this.lblFilterValues.Size = new System.Drawing.Size(48, 17);
            this.lblFilterValues.TabIndex = 2;
            this.lblFilterValues.Text = "Values:";
            // 
            // txtFilterName
            // 
            this.txtFilterName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilterName.Location = new System.Drawing.Point(120, 17);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.Size = new System.Drawing.Size(310, 25);
            this.txtFilterName.TabIndex = 2;
            // 
            // lblFilterName
            // 
            this.lblFilterName.AutoSize = true;
            this.lblFilterName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFilterName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblFilterName.Location = new System.Drawing.Point(15, 20);
            this.lblFilterName.Name = "lblFilterName";
            this.lblFilterName.Size = new System.Drawing.Size(78, 17);
            this.lblFilterName.TabIndex = 0;
            this.lblFilterName.Text = "Filter Name:";
            // 
            // lblFiltersSection
            // 
            this.lblFiltersSection.AutoSize = true;
            this.lblFiltersSection.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblFiltersSection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblFiltersSection.Location = new System.Drawing.Point(21, 95);
            this.lblFiltersSection.Name = "lblFiltersSection";
            this.lblFiltersSection.Size = new System.Drawing.Size(190, 20);
            this.lblFiltersSection.TabIndex = 2;
            this.lblFiltersSection.Text = "Category Filters (Optional)";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCategoryName.Location = new System.Drawing.Point(25, 47);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(450, 27);
            this.txtCategoryName.TabIndex = 1;
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCategoryName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblCategoryName.Location = new System.Drawing.Point(21, 25);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(103, 17);
            this.lblCategoryName.TabIndex = 0;
            this.lblCategoryName.Text = "Category Name:";
            // 
            // FrmAddCategory
            // 
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilters)).EndInit();
            this.pnlAddFilter.ResumeLayout(false);
            this.pnlAddFilter.PerformLayout();
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
        private System.Windows.Forms.Label lblFiltersSection;
        private System.Windows.Forms.Panel pnlAddFilter;
        private System.Windows.Forms.TextBox txtFilterName;
        private System.Windows.Forms.Label lblFilterName;
        private System.Windows.Forms.Label lblFilterValues;
        private System.Windows.Forms.TextBox txtFilterValues;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.DataGridView dgvFilters;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilterValues;
        private System.Windows.Forms.DataGridViewButtonColumn colAction;
    }
}

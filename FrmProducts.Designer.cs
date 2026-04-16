namespace InventoryManagementSystem
{
    partial class FrmProducts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblProcessor = new System.Windows.Forms.Label();
            this.cmbFilterProcessor = new System.Windows.Forms.ComboBox();
            this.lblRAM = new System.Windows.Forms.Label();
            this.cmbFilterRAM = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnCategoryPrinter = new System.Windows.Forms.Button();
            this.btnCategoryPhone = new System.Windows.Forms.Button();
            this.btnCategoryLaptop = new System.Windows.Forms.Button();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.lblProdSpec = new System.Windows.Forms.Label();
            this.lblProdPrice = new System.Windows.Forms.Label();
            this.lblProdName = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.txtProdSpec = new System.Windows.Forms.TextBox();
            this.txtProdPrice = new System.Windows.Forms.TextBox();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblProcessor);
            this.pnlHeader.Controls.Add(this.cmbFilterProcessor);
            this.pnlHeader.Controls.Add(this.lblRAM);
            this.pnlHeader.Controls.Add(this.cmbFilterRAM);
            this.pnlHeader.Controls.Add(this.lblSearch);
            this.pnlHeader.Controls.Add(this.txtSearch);
            this.pnlHeader.Controls.Add(this.btnCategoryPrinter);
            this.pnlHeader.Controls.Add(this.btnCategoryPhone);
            this.pnlHeader.Controls.Add(this.btnCategoryLaptop);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.pnlHeader.Size = new System.Drawing.Size(1200, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblProcessor
            // 
            this.lblProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProcessor.AutoSize = true;
            this.lblProcessor.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblProcessor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblProcessor.Location = new System.Drawing.Point(485, 31);
            this.lblProcessor.Name = "lblProcessor";
            this.lblProcessor.Size = new System.Drawing.Size(69, 17);
            this.lblProcessor.TabIndex = 8;
            this.lblProcessor.Text = "Processor:";
            // 
            // cmbFilterProcessor
            // 
            this.cmbFilterProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterProcessor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterProcessor.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbFilterProcessor.FormattingEnabled = true;
            this.cmbFilterProcessor.Items.AddRange(new object[] {
            "All Processors",
            "Apple M3",
            "Apple M2",
            "Intel Core i9",
            "Intel Core i7",
            "Intel Core i5",
            "Snapdragon 8 Gen 3",
            "A17 Pro"});
            this.cmbFilterProcessor.Location = new System.Drawing.Point(560, 28);
            this.cmbFilterProcessor.Name = "cmbFilterProcessor";
            this.cmbFilterProcessor.Size = new System.Drawing.Size(140, 25);
            this.cmbFilterProcessor.TabIndex = 7;
            // 
            // lblRAM
            // 
            this.lblRAM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRAM.AutoSize = true;
            this.lblRAM.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblRAM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblRAM.Location = new System.Drawing.Point(725, 31);
            this.lblRAM.Name = "lblRAM";
            this.lblRAM.Size = new System.Drawing.Size(39, 17);
            this.lblRAM.TabIndex = 6;
            this.lblRAM.Text = "RAM:";
            // 
            // cmbFilterRAM
            // 
            this.cmbFilterRAM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterRAM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterRAM.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbFilterRAM.FormattingEnabled = true;
            this.cmbFilterRAM.Items.AddRange(new object[] {
            "All RAM",
            "8 GB",
            "16 GB",
            "32 GB",
            "64 GB"});
            this.cmbFilterRAM.Location = new System.Drawing.Point(770, 28);
            this.cmbFilterRAM.Name = "cmbFilterRAM";
            this.cmbFilterRAM.Size = new System.Drawing.Size(100, 25);
            this.cmbFilterRAM.TabIndex = 5;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSearch.Location = new System.Drawing.Point(890, 31);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(50, 17);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(945, 28);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(220, 24);
            this.txtSearch.TabIndex = 3;
            // 
            // btnCategoryPrinter
            // 
            this.btnCategoryPrinter.BackColor = System.Drawing.Color.White;
            this.btnCategoryPrinter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCategoryPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryPrinter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCategoryPrinter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCategoryPrinter.Location = new System.Drawing.Point(260, 25);
            this.btnCategoryPrinter.Name = "btnCategoryPrinter";
            this.btnCategoryPrinter.Size = new System.Drawing.Size(100, 35);
            this.btnCategoryPrinter.TabIndex = 2;
            this.btnCategoryPrinter.Text = "Printers";
            this.btnCategoryPrinter.UseVisualStyleBackColor = false;
            // 
            // btnCategoryPhone
            // 
            this.btnCategoryPhone.BackColor = System.Drawing.Color.White;
            this.btnCategoryPhone.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCategoryPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCategoryPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCategoryPhone.Location = new System.Drawing.Point(140, 25);
            this.btnCategoryPhone.Name = "btnCategoryPhone";
            this.btnCategoryPhone.Size = new System.Drawing.Size(100, 35);
            this.btnCategoryPhone.TabIndex = 1;
            this.btnCategoryPhone.Text = "Phones";
            this.btnCategoryPhone.UseVisualStyleBackColor = false;
            // 
            // btnCategoryLaptop
            // 
            this.btnCategoryLaptop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnCategoryLaptop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCategoryLaptop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryLaptop.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCategoryLaptop.ForeColor = System.Drawing.Color.Black;
            this.btnCategoryLaptop.Location = new System.Drawing.Point(20, 25);
            this.btnCategoryLaptop.Name = "btnCategoryLaptop";
            this.btnCategoryLaptop.Size = new System.Drawing.Size(100, 35);
            this.btnCategoryLaptop.TabIndex = 0;
            this.btnCategoryLaptop.Text = "Laptops";
            this.btnCategoryLaptop.UseVisualStyleBackColor = false;
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetails.Controls.Add(this.lblProdSpec);
            this.pnlDetails.Controls.Add(this.lblProdPrice);
            this.pnlDetails.Controls.Add(this.lblProdName);
            this.pnlDetails.Controls.Add(this.btnEdit);
            this.pnlDetails.Controls.Add(this.txtProdSpec);
            this.pnlDetails.Controls.Add(this.txtProdPrice);
            this.pnlDetails.Controls.Add(this.txtProdName);
            this.pnlDetails.Controls.Add(this.lblDetailsTitle);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetails.Location = new System.Drawing.Point(880, 80);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDetails.Size = new System.Drawing.Size(320, 620);
            this.pnlDetails.TabIndex = 1;
            // 
            // lblProdSpec
            // 
            this.lblProdSpec.AutoSize = true;
            this.lblProdSpec.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProdSpec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblProdSpec.Location = new System.Drawing.Point(20, 210);
            this.lblProdSpec.Name = "lblProdSpec";
            this.lblProdSpec.Size = new System.Drawing.Size(80, 15);
            this.lblProdSpec.TabIndex = 7;
            this.lblProdSpec.Text = "Specifications";
            // 
            // lblProdPrice
            // 
            this.lblProdPrice.AutoSize = true;
            this.lblProdPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProdPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblProdPrice.Location = new System.Drawing.Point(20, 145);
            this.lblProdPrice.Name = "lblProdPrice";
            this.lblProdPrice.Size = new System.Drawing.Size(33, 15);
            this.lblProdPrice.TabIndex = 6;
            this.lblProdPrice.Text = "Price";
            // 
            // lblProdName
            // 
            this.lblProdName.AutoSize = true;
            this.lblProdName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProdName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblProdName.Location = new System.Drawing.Point(20, 80);
            this.lblProdName.Name = "lblProdName";
            this.lblProdName.Size = new System.Drawing.Size(84, 15);
            this.lblProdName.TabIndex = 5;
            this.lblProdName.Text = "Product Name";
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(23, 394);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(274, 40);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Save Changes";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // txtProdSpec
            // 
            this.txtProdSpec.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtProdSpec.Location = new System.Drawing.Point(23, 230);
            this.txtProdSpec.Multiline = true;
            this.txtProdSpec.Name = "txtProdSpec";
            this.txtProdSpec.Size = new System.Drawing.Size(274, 146);
            this.txtProdSpec.TabIndex = 3;
            // 
            // txtProdPrice
            // 
            this.txtProdPrice.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtProdPrice.Location = new System.Drawing.Point(23, 165);
            this.txtProdPrice.Name = "txtProdPrice";
            this.txtProdPrice.Size = new System.Drawing.Size(274, 27);
            this.txtProdPrice.TabIndex = 2;
            // 
            // txtProdName
            // 
            this.txtProdName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtProdName.Location = new System.Drawing.Point(23, 100);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(274, 27);
            this.txtProdName.TabIndex = 1;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblDetailsTitle.Location = new System.Drawing.Point(18, 25);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(143, 25);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "Product Details";
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.BackColor = System.Drawing.Color.White;
            this.pnlGridContainer.Controls.Add(this.dgvProducts);
            this.pnlGridContainer.Location = new System.Drawing.Point(0, 80);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(20);
            this.pnlGridContainer.Size = new System.Drawing.Size(880, 620);
            this.pnlGridContainer.TabIndex = 2;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dgvProducts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvProducts.ColumnHeadersHeight = 45;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colProductName,
            this.colCategory,
            this.colStock,
            this.colPrice,
            this.colStatus});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProducts.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.EnableHeadersVisualStyles = false;
            this.dgvProducts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvProducts.Location = new System.Drawing.Point(20, 20);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowTemplate.Height = 40;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(840, 580);
            this.dgvProducts.TabIndex = 0;
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            // 
            // colProductName
            // 
            this.colProductName.DataPropertyName = "Product Name";
            this.colProductName.HeaderText = "Product Name";
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colCategory
            // 
            this.colCategory.DataPropertyName = "Category";
            this.colCategory.HeaderText = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            // 
            // colStock
            // 
            this.colStock.DataPropertyName = "Stock";
            this.colStock.HeaderText = "Stock";
            this.colStock.Name = "colStock";
            this.colStock.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.DataPropertyName = "Price";
            this.colPrice.HeaderText = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;

            // 
            // FrmProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlGridContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmProducts";
            this.Text = "Products";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnCategoryPrinter;
        private System.Windows.Forms.Button btnCategoryPhone;
        private System.Windows.Forms.Button btnCategoryLaptop;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Label lblProdSpec;
        private System.Windows.Forms.Label lblProdPrice;
        private System.Windows.Forms.Label lblProdName;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TextBox txtProdSpec;
        private System.Windows.Forms.TextBox txtProdPrice;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Label lblProcessor;
        private System.Windows.Forms.ComboBox cmbFilterProcessor;
        private System.Windows.Forms.Label lblRAM;
        private System.Windows.Forms.ComboBox cmbFilterRAM;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}
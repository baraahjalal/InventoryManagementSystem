namespace InventoryManagementSystem.Forms
{
    partial class FrmAddProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.tblForm = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblSerial = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblSuppliers = new System.Windows.Forms.Label();
            this.clbSuppliers = new System.Windows.Forms.CheckedListBox();
            this.lblSpecs = new System.Windows.Forms.Label();
            this.dgvProductSpecs = new System.Windows.Forms.DataGridView();
            this.colSpecKey      = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpecValue    = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.tblForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductSpecs)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 20, 24, 0);
            this.pnlHeader.Size = new System.Drawing.Size(520, 90);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(138, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add Product";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSubTitle.Location = new System.Drawing.Point(22, 52);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(339, 19);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Enter product details and initial inventory information.";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.tblForm);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 90);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(24, 12, 24, 12);
            this.pnlBody.Size = new System.Drawing.Size(520, 460);
            this.pnlBody.TabIndex = 1;
            // 
            // tblForm
            // 
            this.tblForm.ColumnCount = 2;
            this.tblForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblForm.Controls.Add(this.lblName, 0, 0);
            this.tblForm.Controls.Add(this.txtName, 1, 0);
            this.tblForm.Controls.Add(this.lblPrice, 0, 1);
            this.tblForm.Controls.Add(this.txtPrice, 1, 1);
            this.tblForm.Controls.Add(this.lblQuantity, 0, 2);
            this.tblForm.Controls.Add(this.txtQuantity, 1, 2);
            this.tblForm.Controls.Add(this.lblSerial, 0, 3);
            this.tblForm.Controls.Add(this.txtSerialNumber, 1, 3);
            this.tblForm.Controls.Add(this.lblCategory, 0, 4);
            this.tblForm.Controls.Add(this.cmbCategory, 1, 4);
            this.tblForm.Controls.Add(this.lblSuppliers, 0, 5);
            this.tblForm.Controls.Add(this.clbSuppliers, 1, 5);
            this.tblForm.Controls.Add(this.lblSpecs, 0, 6);
            this.tblForm.Controls.Add(this.dgvProductSpecs, 1, 6);
            this.tblForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblForm.Location = new System.Drawing.Point(24, 12);
            this.tblForm.Name = "tblForm";
            this.tblForm.RowCount = 7;
            this.tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tblForm.Size = new System.Drawing.Size(472, 510);
            this.tblForm.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblName.Location = new System.Drawing.Point(3, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(92, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Product Name";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(153, 9);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(316, 25);
            this.txtName.TabIndex = 1;
            // 
            // lblPrice
            // 
            this.lblPrice.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblPrice.Location = new System.Drawing.Point(3, 57);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(36, 17);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price";
            // 
            // txtPrice
            // 
            this.txtPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPrice.Location = new System.Drawing.Point(153, 53);
            this.txtPrice.MaxLength = 20;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(316, 25);
            this.txtPrice.TabIndex = 3;
            // 
            // lblQuantity
            // 
            this.lblQuantity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblQuantity.Location = new System.Drawing.Point(3, 101);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(90, 17);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Initial Quantity";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtQuantity.Location = new System.Drawing.Point(153, 97);
            this.txtQuantity.MaxLength = 10;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(316, 25);
            this.txtQuantity.TabIndex = 5;
            // 
            // lblSerial
            // 
            this.lblSerial.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSerial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSerial.Location = new System.Drawing.Point(3, 145);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(92, 17);
            this.lblSerial.TabIndex = 6;
            this.lblSerial.Text = "Serial Number";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSerialNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSerialNumber.Location = new System.Drawing.Point(153, 141);
            this.txtSerialNumber.MaxLength = 60;
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(316, 25);
            this.txtSerialNumber.TabIndex = 7;
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblCategory.Location = new System.Drawing.Point(3, 189);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(61, 17);
            this.lblCategory.TabIndex = 8;
            this.lblCategory.Text = "Category";
            // 
            // cmbCategory
            // 
            this.cmbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(153, 184);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(316, 25);
            this.cmbCategory.TabIndex = 9;
            // 
            // lblSuppliers
            // 
            this.lblSuppliers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSuppliers.AutoSize = true;
            this.lblSuppliers.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSuppliers.Location = new System.Drawing.Point(3, 233);
            this.lblSuppliers.Name = "lblSuppliers";
            this.lblSuppliers.Size = new System.Drawing.Size(63, 17);
            this.lblSuppliers.TabIndex = 10;
            this.lblSuppliers.Text = "Suppliers";
            // 
            // clbSuppliers
            // 
            this.clbSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbSuppliers.CheckOnClick = true;
            this.clbSuppliers.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.clbSuppliers.FormattingEnabled = true;
            this.clbSuppliers.Location = new System.Drawing.Point(153, 223);
            this.clbSuppliers.Name = "clbSuppliers";
            this.clbSuppliers.Size = new System.Drawing.Size(316, 94);
            this.clbSuppliers.TabIndex = 11;
            // 
            // lblSpecs
            // 
            this.lblSpecs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSpecs.AutoSize = true;
            this.lblSpecs.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSpecs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSpecs.Location = new System.Drawing.Point(3, 406);
            this.lblSpecs.Name = "lblSpecs";
            this.lblSpecs.Size = new System.Drawing.Size(87, 17);
            this.lblSpecs.TabIndex = 12;
            this.lblSpecs.Text = "Specifications";
            //
            // dgvProductSpecs
            //
            this.dgvProductSpecs.AllowUserToAddRows    = false;
            this.dgvProductSpecs.AllowUserToDeleteRows = false;
            this.dgvProductSpecs.AllowUserToResizeRows = false;
            this.dgvProductSpecs.BackgroundColor       = System.Drawing.Color.White;
            this.dgvProductSpecs.BorderStyle           = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvProductSpecs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvProductSpecs.ColumnHeadersHeight   = 28;
            this.dgvProductSpecs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProductSpecs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colSpecKey,
                this.colSpecValue });
            this.dgvProductSpecs.Dock          = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductSpecs.EnableHeadersVisualStyles = false;
            this.dgvProductSpecs.Location      = new System.Drawing.Point(153, 323);
            this.dgvProductSpecs.Name          = "dgvProductSpecs";
            this.dgvProductSpecs.ReadOnly      = false;
            this.dgvProductSpecs.RowHeadersVisible = false;
            this.dgvProductSpecs.RowTemplate.Height = 28;
            this.dgvProductSpecs.Size          = new System.Drawing.Size(316, 184);
            this.dgvProductSpecs.TabIndex      = 13;
            //
            // colSpecKey
            //
            this.colSpecKey.HeaderText = "Property";
            this.colSpecKey.Name       = "colSpecKey";
            this.colSpecKey.ReadOnly   = true;
            this.colSpecKey.Width      = 120;
            //
            // colSpecValue
            //
            this.colSpecValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSpecValue.HeaderText   = "Value";
            this.colSpecValue.Name         = "colSpecValue";
            this.colSpecValue.ReadOnly     = false;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 550);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(24, 12, 24, 20);
            this.pnlFooter.Size = new System.Drawing.Size(520, 70);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(296, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(406, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 28);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // FrmAddProduct
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 720);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Product";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.tblForm.ResumeLayout(false);
            this.tblForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductSpecs)).EndInit();
            InventoryManagementSystem.Classes.AppTheme.ApplyCompactGrid(this.dgvProductSpecs);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.TableLayoutPanel tblForm;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblSuppliers;
        private System.Windows.Forms.CheckedListBox clbSuppliers;
        private System.Windows.Forms.Label lblSpecs;
        private System.Windows.Forms.DataGridView dgvProductSpecs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpecKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpecValue;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}

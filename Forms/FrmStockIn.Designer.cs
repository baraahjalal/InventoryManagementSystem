namespace InventoryManagementSystem
{
    partial class FrmStockIn
    {
        private System.ComponentModel.IContainer components = null;

        // Form Controls
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.Panel pnlMainCard;
        private System.Windows.Forms.Panel pnlHeader;

        // Input Controls
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.ComboBox cmbStorageZone;
        private System.Windows.Forms.TextBox txtSerialNumbers;
        private System.Windows.Forms.TextBox txtWarrantyInfo;
        private System.Windows.Forms.Button btnExecute;

        // Labels for Inputs
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblStorageZone;
        private System.Windows.Forms.Label lblSerialNumbers;
        private System.Windows.Forms.Label lblWarrantyInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSubHeader = new System.Windows.Forms.Label();
            this.pnlMainCard = new System.Windows.Forms.Panel();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblStorageZone = new System.Windows.Forms.Label();
            this.cmbStorageZone = new System.Windows.Forms.ComboBox();
            this.lblSerialNumbers = new System.Windows.Forms.Label();
            this.txtSerialNumbers = new System.Windows.Forms.TextBox();
            this.lblWarrantyInfo = new System.Windows.Forms.Label();
            this.txtWarrantyInfo = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.pnlMainCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 88);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHeader.Location = new System.Drawing.Point(36, 100);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(289, 47);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Stock In Receipt";
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubHeader.Location = new System.Drawing.Point(40, 142);
            this.lblSubHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(594, 21);
            this.lblSubHeader.TabIndex = 2;
            this.lblSubHeader.Text = "Record incoming inventory, supplier details, and physical placements.";
            // 
            // pnlMainCard
            // 
            this.pnlMainCard.BackColor = System.Drawing.Color.White;
            this.pnlMainCard.Controls.Add(this.lblSupplier);
            this.pnlMainCard.Controls.Add(this.cmbSupplier);
            this.pnlMainCard.Controls.Add(this.lblOrderNumber);
            this.pnlMainCard.Controls.Add(this.txtOrderNumber);
            this.pnlMainCard.Controls.Add(this.lblProduct);
            this.pnlMainCard.Controls.Add(this.cmbProduct);
            this.pnlMainCard.Controls.Add(this.lblQuantity);
            this.pnlMainCard.Controls.Add(this.numQuantity);
            this.pnlMainCard.Controls.Add(this.lblStorageZone);
            this.pnlMainCard.Controls.Add(this.cmbStorageZone);
            this.pnlMainCard.Controls.Add(this.lblSerialNumbers);
            this.pnlMainCard.Controls.Add(this.txtSerialNumbers);
            this.pnlMainCard.Controls.Add(this.lblWarrantyInfo);
            this.pnlMainCard.Controls.Add(this.txtWarrantyInfo);
            this.pnlMainCard.Controls.Add(this.btnExecute);
            this.pnlMainCard.Location = new System.Drawing.Point(20, 181);
            this.pnlMainCard.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMainCard.Name = "pnlMainCard";
            this.pnlMainCard.Size = new System.Drawing.Size(992, 615);
            this.pnlMainCard.TabIndex = 3;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSupplier.Location = new System.Drawing.Point(41, 37);
            this.lblSupplier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(58, 17);
            this.lblSupplier.TabIndex = 3;
            this.lblSupplier.Text = "Supplier";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(44, 68);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(431, 28);
            this.cmbSupplier.TabIndex = 4;
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOrderNumber.Location = new System.Drawing.Point(510, 37);
            this.lblOrderNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(155, 17);
            this.lblOrderNumber.TabIndex = 5;
            this.lblOrderNumber.Text = "Purchase Order Number";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNumber.Location = new System.Drawing.Point(513, 68);
            this.txtOrderNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(431, 27);
            this.txtOrderNumber.TabIndex = 6;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblProduct.Location = new System.Drawing.Point(41, 129);
            this.lblProduct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(95, 17);
            this.lblProduct.TabIndex = 7;
            this.lblProduct.Text = "Select Product";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(44, 160);
            this.cmbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(900, 28);
            this.cmbProduct.TabIndex = 8;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblQuantity.Location = new System.Drawing.Point(41, 222);
            this.lblQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(120, 17);
            this.lblQuantity.TabIndex = 9;
            this.lblQuantity.Text = "Received Quantity";
            // 
            // numQuantity
            // 
            this.numQuantity.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQuantity.Location = new System.Drawing.Point(44, 252);
            this.numQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.numQuantity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(432, 27);
            this.numQuantity.TabIndex = 10;
            // 
            // lblStorageZone
            // 
            this.lblStorageZone.AutoSize = true;
            this.lblStorageZone.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStorageZone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStorageZone.Location = new System.Drawing.Point(510, 222);
            this.lblStorageZone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStorageZone.Name = "lblStorageZone";
            this.lblStorageZone.Size = new System.Drawing.Size(160, 17);
            this.lblStorageZone.TabIndex = 11;
            this.lblStorageZone.Text = "Destination Storage Zone";
            // 
            // cmbStorageZone
            // 
            this.cmbStorageZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStorageZone.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStorageZone.FormattingEnabled = true;
            this.cmbStorageZone.Items.AddRange(new object[] {
            "Aisle A-1: Laptops",
            "Aisle B-2: Phones",
            "Aisle C-3: Accessories"});
            this.cmbStorageZone.Location = new System.Drawing.Point(513, 252);
            this.cmbStorageZone.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStorageZone.Name = "cmbStorageZone";
            this.cmbStorageZone.Size = new System.Drawing.Size(431, 28);
            this.cmbStorageZone.TabIndex = 12;
            // 
            // lblSerialNumbers
            // 
            this.lblSerialNumbers.AutoSize = true;
            this.lblSerialNumbers.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumbers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSerialNumbers.Location = new System.Drawing.Point(41, 314);
            this.lblSerialNumbers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerialNumbers.Name = "lblSerialNumbers";
            this.lblSerialNumbers.Size = new System.Drawing.Size(193, 17);
            this.lblSerialNumbers.TabIndex = 13;
            this.lblSerialNumbers.Text = "Serial Numbers (One per Line)";
            // 
            // txtSerialNumbers
            // 
            this.txtSerialNumbers.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNumbers.Location = new System.Drawing.Point(44, 356);
            this.txtSerialNumbers.Margin = new System.Windows.Forms.Padding(4);
            this.txtSerialNumbers.Multiline = true;
            this.txtSerialNumbers.Name = "txtSerialNumbers";
            this.txtSerialNumbers.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSerialNumbers.Size = new System.Drawing.Size(432, 117);
            this.txtSerialNumbers.TabIndex = 14;
            // 
            // lblWarrantyInfo
            // 
            this.lblWarrantyInfo.AutoSize = true;
            this.lblWarrantyInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarrantyInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblWarrantyInfo.Location = new System.Drawing.Point(510, 314);
            this.lblWarrantyInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWarrantyInfo.Name = "lblWarrantyInfo";
            this.lblWarrantyInfo.Size = new System.Drawing.Size(248, 17);
            this.lblWarrantyInfo.TabIndex = 15;
            this.lblWarrantyInfo.Text = "Warranty Information (e.g., 12 Months)";
            // 
            // txtWarrantyInfo
            // 
            this.txtWarrantyInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWarrantyInfo.Location = new System.Drawing.Point(513, 345);
            this.txtWarrantyInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtWarrantyInfo.Name = "txtWarrantyInfo";
            this.txtWarrantyInfo.Size = new System.Drawing.Size(431, 27);
            this.txtWarrantyInfo.TabIndex = 16;
            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnExecute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExecute.FlatAppearance.BorderSize = 0;
            this.btnExecute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.ForeColor = System.Drawing.Color.White;
            this.btnExecute.Location = new System.Drawing.Point(44, 511);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(901, 55);
            this.btnExecute.TabIndex = 17;
            this.btnExecute.Text = "Register Stock Entry";
            this.btnExecute.UseVisualStyleBackColor = false;
            // 
            // FrmStockIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 850);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlMainCard);
            this.Controls.Add(this.lblSubHeader);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmStockIn";
            this.Text = "Stock In Operations";
            this.pnlMainCard.ResumeLayout(false);
            this.pnlMainCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
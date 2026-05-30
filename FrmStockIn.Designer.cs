namespace InventoryManagementSystem
{
    partial class FrmStockIn
    {
        private System.ComponentModel.IContainer components = null;

        // Form Controls
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.Panel pnlMainCard;

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
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblHeader.Location = new System.Drawing.Point(40, 30);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(248, 41);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Stock In Receipt";

            // 
            // lblSubHeader
            // 
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(94)))), ((int)(((byte)(92)))));
            this.lblSubHeader.Location = new System.Drawing.Point(45, 75);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(462, 20);
            this.lblSubHeader.TabIndex = 1;
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
            this.pnlMainCard.Location = new System.Drawing.Point(50, 125);
            this.pnlMainCard.Name = "pnlMainCard";
            this.pnlMainCard.Size = new System.Drawing.Size(850, 500);
            this.pnlMainCard.TabIndex = 2;

            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSupplier.Location = new System.Drawing.Point(35, 30);
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
            this.cmbSupplier.Location = new System.Drawing.Point(38, 55);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(370, 28);
            this.cmbSupplier.TabIndex = 4;

            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOrderNumber.Location = new System.Drawing.Point(437, 30);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(149, 17);
            this.lblOrderNumber.TabIndex = 5;
            this.lblOrderNumber.Text = "Purchase Order Number";

            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNumber.Location = new System.Drawing.Point(440, 55);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(370, 27);
            this.txtOrderNumber.TabIndex = 6;

            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblProduct.Location = new System.Drawing.Point(35, 105);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(96, 17);
            this.lblProduct.TabIndex = 7;
            this.lblProduct.Text = "Select Product";

            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(38, 130);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(772, 28);
            this.cmbProduct.TabIndex = 8;

            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblQuantity.Location = new System.Drawing.Point(35, 180);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(121, 17);
            this.lblQuantity.TabIndex = 9;
            this.lblQuantity.Text = "Received Quantity";

            // 
            // numQuantity
            // 
            this.numQuantity.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQuantity.Location = new System.Drawing.Point(38, 205);
            this.numQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(370, 27);
            this.numQuantity.TabIndex = 10;

            // 
            // lblStorageZone
            // 
            this.lblStorageZone.AutoSize = true;
            this.lblStorageZone.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStorageZone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStorageZone.Location = new System.Drawing.Point(437, 180);
            this.lblStorageZone.Name = "lblStorageZone";
            this.lblStorageZone.Size = new System.Drawing.Size(163, 17);
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
            this.cmbStorageZone.Location = new System.Drawing.Point(440, 205);
            this.cmbStorageZone.Name = "cmbStorageZone";
            this.cmbStorageZone.Size = new System.Drawing.Size(370, 28);
            this.cmbStorageZone.TabIndex = 12;

            // 
            // lblSerialNumbers
            // 
            this.lblSerialNumbers.AutoSize = true;
            this.lblSerialNumbers.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumbers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSerialNumbers.Location = new System.Drawing.Point(35, 255);
            this.lblSerialNumbers.Name = "lblSerialNumbers";
            this.lblSerialNumbers.Size = new System.Drawing.Size(193, 17);
            this.lblSerialNumbers.TabIndex = 13;
            this.lblSerialNumbers.Text = "Serial Numbers (One per Line)";

            // 
            // txtSerialNumbers
            // 
            this.txtSerialNumbers.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNumbers.Location = new System.Drawing.Point(38, 280);
            this.txtSerialNumbers.Multiline = true;
            this.txtSerialNumbers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSerialNumbers.Name = "txtSerialNumbers";
            this.txtSerialNumbers.Size = new System.Drawing.Size(370, 95);
            this.txtSerialNumbers.TabIndex = 14;

            // 
            // lblWarrantyInfo
            // 
            this.lblWarrantyInfo.AutoSize = true;
            this.lblWarrantyInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarrantyInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblWarrantyInfo.Location = new System.Drawing.Point(437, 255);
            this.lblWarrantyInfo.Name = "lblWarrantyInfo";
            this.lblWarrantyInfo.Size = new System.Drawing.Size(262, 17);
            this.lblWarrantyInfo.TabIndex = 15;
            this.lblWarrantyInfo.Text = "Warranty Information (e.g., 12 Months)";

            // 
            // txtWarrantyInfo
            // 
            this.txtWarrantyInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWarrantyInfo.Location = new System.Drawing.Point(440, 280);
            this.txtWarrantyInfo.Name = "txtWarrantyInfo";
            this.txtWarrantyInfo.Size = new System.Drawing.Size(370, 27);
            this.txtWarrantyInfo.TabIndex = 16;

            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212))))); // Microsoft Fluent Primary Blue
            this.btnExecute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExecute.FlatAppearance.BorderSize = 0;
            this.btnExecute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(110)))), ((int)(((byte)(190)))));
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.ForeColor = System.Drawing.Color.White;
            this.btnExecute.Location = new System.Drawing.Point(38, 415);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(772, 45);
            this.btnExecute.TabIndex = 17;
            this.btnExecute.Text = "Register Stock Entry";
            this.btnExecute.UseVisualStyleBackColor = false;

            // 
            // FrmStockIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(241))))); // Microsoft Fluent App Background Light Gray
            this.ClientSize = new System.Drawing.Size(950, 750);
            this.Controls.Add(this.pnlMainCard);
            this.Controls.Add(this.lblSubHeader);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
namespace InventoryManagementSystem
{
    partial class FrmStockOut
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSubHeader = new System.Windows.Forms.Label();
            this.pnlMainCard = new System.Windows.Forms.Panel();
            this.btnExecuteStockOut = new System.Windows.Forms.Button();
            this.lblWarrantyInfo = new System.Windows.Forms.Label();
            this.lblStockStatus = new System.Windows.Forms.Label();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.lblQty = new System.Windows.Forms.Label();
            this.clbSerialNumbers = new System.Windows.Forms.CheckedListBox();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtRecipient = new System.Windows.Forms.TextBox();
            this.lblRecipient = new System.Windows.Forms.Label();
            this.cmbOutReason = new System.Windows.Forms.ComboBox();
            this.lblOutReason = new System.Windows.Forms.Label();
            this.txtWarrantyStatus = new System.Windows.Forms.TextBox();
            this.lblWarrantyInfoDetails = new System.Windows.Forms.Label();
            this.lblSystemID = new System.Windows.Forms.Label();
            this.pnlMainCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(40, 30);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(308, 41);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Inventory Outbound";
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeader.ForeColor = System.Drawing.Color.Gray;
            this.lblSubHeader.Location = new System.Drawing.Point(45, 75);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(346, 19);
            this.lblSubHeader.TabIndex = 1;
            this.lblSubHeader.Text = "Track and manage hardware distribution with precision.";
            // 
            // pnlMainCard
            // 
            this.pnlMainCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMainCard.BackColor = System.Drawing.Color.White;
            this.pnlMainCard.Controls.Add(this.btnExecuteStockOut);
            this.pnlMainCard.Controls.Add(this.lblWarrantyInfo);
            this.pnlMainCard.Controls.Add(this.lblStockStatus);
            this.pnlMainCard.Controls.Add(this.numQty);
            this.pnlMainCard.Controls.Add(this.lblQty);
            this.pnlMainCard.Controls.Add(this.clbSerialNumbers);
            this.pnlMainCard.Controls.Add(this.lblSerialNumber);
            this.pnlMainCard.Controls.Add(this.cmbProduct);
            this.pnlMainCard.Controls.Add(this.lblProduct);
            this.pnlMainCard.Controls.Add(this.txtRecipient);
            this.pnlMainCard.Controls.Add(this.lblRecipient);
            this.pnlMainCard.Controls.Add(this.cmbOutReason);
            this.pnlMainCard.Controls.Add(this.lblOutReason);
            this.pnlMainCard.Controls.Add(this.txtWarrantyStatus);
            this.pnlMainCard.Controls.Add(this.lblWarrantyInfoDetails);
            this.pnlMainCard.Location = new System.Drawing.Point(48, 120);
            this.pnlMainCard.Name = "pnlMainCard";
            this.pnlMainCard.Size = new System.Drawing.Size(800, 500);
            this.pnlMainCard.TabIndex = 2;
            // 
            // btnExecuteStockOut
            // 
            this.btnExecuteStockOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteStockOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnExecuteStockOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExecuteStockOut.FlatAppearance.BorderSize = 0;
            this.btnExecuteStockOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecuteStockOut.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecuteStockOut.ForeColor = System.Drawing.Color.White;
            this.btnExecuteStockOut.Location = new System.Drawing.Point(43, 410);
            this.btnExecuteStockOut.Name = "btnExecuteStockOut";
            this.btnExecuteStockOut.Size = new System.Drawing.Size(717, 50);
            this.btnExecuteStockOut.TabIndex = 12;
            this.btnExecuteStockOut.Text = "CONFIRM STOCK OUT";
            this.btnExecuteStockOut.UseVisualStyleBackColor = false;
            // 
            // lblWarrantyInfo
            // 
            this.lblWarrantyInfo.AutoSize = true;
            this.lblWarrantyInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarrantyInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblWarrantyInfo.Location = new System.Drawing.Point(40, 300);
            this.lblWarrantyInfo.Name = "lblWarrantyInfo";
            this.lblWarrantyInfo.Size = new System.Drawing.Size(176, 19);
            this.lblWarrantyInfo.TabIndex = 11;
            this.lblWarrantyInfo.Text = "Warranty Expires: --/--/----";
            // 
            // lblStockStatus
            // 
            this.lblStockStatus.AutoSize = true;
            this.lblStockStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblStockStatus.Location = new System.Drawing.Point(570, 222);
            this.lblStockStatus.Name = "lblStockStatus";
            this.lblStockStatus.Size = new System.Drawing.Size(76, 19);
            this.lblStockStatus.TabIndex = 10;
            this.lblStockStatus.Text = "In Stock: 0";
            // 
            // numQty
            // 
            this.numQty.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQty.Location = new System.Drawing.Point(420, 218);
            this.numQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(120, 27);
            this.numQty.TabIndex = 9;
            this.numQty.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblQty.Location = new System.Drawing.Point(417, 195);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(142, 15);
            this.lblQty.TabIndex = 8;
            this.lblQty.Text = "TOTAL SELECTED (AUTO)";
            // 
            // clbSerialNumbers
            // 
            this.clbSerialNumbers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbSerialNumbers.FormattingEnabled = true;
            this.clbSerialNumbers.Items.AddRange(new object[] {
            "SN: APP-MBP-1001",
            "SN: APP-MBP-1002",
            "SN: APP-MBP-1003"});
            this.clbSerialNumbers.Location = new System.Drawing.Point(43, 217);
            this.clbSerialNumbers.Name = "clbSerialNumbers";
            this.clbSerialNumbers.Size = new System.Drawing.Size(340, 64);
            this.clbSerialNumbers.TabIndex = 7;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSerialNumber.Location = new System.Drawing.Point(40, 195);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(274, 15);
            this.lblSerialNumber.TabIndex = 6;
            this.lblSerialNumber.Text = "SELECT SPECIFIC SERIAL NUMBERS TO DISPATCH";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(43, 137);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(717, 28);
            this.cmbProduct.TabIndex = 5;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblProduct.Location = new System.Drawing.Point(40, 115);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(103, 15);
            this.lblProduct.TabIndex = 4;
            this.lblProduct.Text = "SELECT PRODUCT";
            // 
            // txtRecipient
            // 
            this.txtRecipient.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecipient.Location = new System.Drawing.Point(420, 62);
            this.txtRecipient.Name = "txtRecipient";
            this.txtRecipient.Size = new System.Drawing.Size(340, 27);
            this.txtRecipient.TabIndex = 3;
            // 
            // lblRecipient
            // 
            this.lblRecipient.AutoSize = true;
            this.lblRecipient.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecipient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRecipient.Location = new System.Drawing.Point(417, 40);
            this.lblRecipient.Name = "lblRecipient";
            this.lblRecipient.Size = new System.Drawing.Size(174, 15);
            this.lblRecipient.TabIndex = 2;
            this.lblRecipient.Text = "RECIPIENT / CUSTOMER NAME";
            // 
            // cmbOutReason
            // 
            this.cmbOutReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutReason.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOutReason.FormattingEnabled = true;
            this.cmbOutReason.Items.AddRange(new object[] {
            "Customer Sale",
            "Internal Use",
            "Damaged/Waste",
            "Return to Supplier"});
            this.cmbOutReason.Location = new System.Drawing.Point(43, 62);
            this.cmbOutReason.Name = "cmbOutReason";
            this.cmbOutReason.Size = new System.Drawing.Size(340, 28);
            this.cmbOutReason.TabIndex = 1;
            // 
            // lblOutReason
            // 
            this.lblOutReason.AutoSize = true;
            this.lblOutReason.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutReason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOutReason.Location = new System.Drawing.Point(40, 40);
            this.lblOutReason.Name = "lblOutReason";
            this.lblOutReason.Size = new System.Drawing.Size(119, 15);
            this.lblOutReason.TabIndex = 0;
            this.lblOutReason.Text = "TRANSACTION TYPE";
            // 
            // txtWarrantyStatus
            // 
            this.txtWarrantyStatus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWarrantyStatus.Location = new System.Drawing.Point(43, 322);
            this.txtWarrantyStatus.Name = "txtWarrantyStatus";
            this.txtWarrantyStatus.Size = new System.Drawing.Size(340, 27);
            this.txtWarrantyStatus.TabIndex = 13;
            // 
            // lblWarrantyInfoDetails
            // 
            this.lblWarrantyInfoDetails.AutoSize = true;
            this.lblWarrantyInfoDetails.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarrantyInfoDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblWarrantyInfoDetails.Location = new System.Drawing.Point(40, 300);
            this.lblWarrantyInfoDetails.Name = "lblWarrantyInfoDetails";
            this.lblWarrantyInfoDetails.Size = new System.Drawing.Size(160, 15);
            this.lblWarrantyInfoDetails.TabIndex = 14;
            this.lblWarrantyInfoDetails.Text = "WARRANTY DETAILS / INFO";
            // 
            // lblSystemID
            // 
            this.lblSystemID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSystemID.AutoSize = true;
            this.lblSystemID.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemID.ForeColor = System.Drawing.Color.Silver;
            this.lblSystemID.Location = new System.Drawing.Point(45, 640);
            this.lblSystemID.Name = "lblSystemID";
            this.lblSystemID.Size = new System.Drawing.Size(156, 13);
            this.lblSystemID.TabIndex = 3;
            this.lblSystemID.Text = "SYSTEM USER: BARAAH AREBI";
            // 
            // FrmStockOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(900, 680);
            this.Controls.Add(this.lblSystemID);
            this.Controls.Add(this.pnlMainCard);
            this.Controls.Add(this.lblSubHeader);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmStockOut";
            this.Text = "FrmStockOut";
            this.pnlMainCard.ResumeLayout(false);
            this.pnlMainCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.Panel pnlMainCard;
        private System.Windows.Forms.Label lblOutReason;
        private System.Windows.Forms.ComboBox cmbOutReason;
        private System.Windows.Forms.Label lblRecipient;
        private System.Windows.Forms.TextBox txtRecipient;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.CheckedListBox clbSerialNumbers;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.Label lblStockStatus;
        private System.Windows.Forms.Label lblWarrantyInfo;
        private System.Windows.Forms.Button btnExecuteStockOut;
        private System.Windows.Forms.Label lblSystemID;
        private System.Windows.Forms.TextBox txtWarrantyStatus;
        private System.Windows.Forms.Label lblWarrantyInfoDetails;
    }
}
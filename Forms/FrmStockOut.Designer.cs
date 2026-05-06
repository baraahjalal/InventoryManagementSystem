namespace InventoryManagementSystem
{
    partial class FrmStockOut
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSubHeader = new System.Windows.Forms.Label();
            this.pnlMainCard = new System.Windows.Forms.Panel();
            this.btnExecuteStockOut = new System.Windows.Forms.Button();
            this.txtWarrantyInfo = new System.Windows.Forms.TextBox();
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
            this.pnlWarrantyCard = new System.Windows.Forms.Panel();
            this.lblWarrantyTitle = new System.Windows.Forms.Label();
            this.lblWarrantyDuration = new System.Windows.Forms.Label();
            this.lblWarrantyExpiry = new System.Windows.Forms.Label();
            this.lblSystemID = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlMainCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.pnlWarrantyCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.lblSubHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1029, 121);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(31, 18);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(359, 47);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Inventory Outbound";
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeader.ForeColor = System.Drawing.Color.White;
            this.lblSubHeader.Location = new System.Drawing.Point(22, 78);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(394, 21);
            this.lblSubHeader.TabIndex = 2;
            this.lblSubHeader.Text = "Track and manage hardware distribution with precision.";
            // 
            // pnlMainCard
            // 
            this.pnlMainCard.BackColor = System.Drawing.Color.White;
            this.pnlMainCard.Controls.Add(this.btnExecuteStockOut);
            this.pnlMainCard.Controls.Add(this.txtWarrantyInfo);
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
            this.pnlMainCard.Controls.Add(this.pnlWarrantyCard);
            this.pnlMainCard.Location = new System.Drawing.Point(17, 157);
            this.pnlMainCard.Name = "pnlMainCard";
            this.pnlMainCard.Size = new System.Drawing.Size(850, 533);
            this.pnlMainCard.TabIndex = 3;
            // 
            // btnExecuteStockOut
            // 
            this.btnExecuteStockOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnExecuteStockOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExecuteStockOut.FlatAppearance.BorderSize = 0;
            this.btnExecuteStockOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnExecuteStockOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecuteStockOut.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecuteStockOut.ForeColor = System.Drawing.Color.White;
            this.btnExecuteStockOut.Location = new System.Drawing.Point(38, 453);
            this.btnExecuteStockOut.Name = "btnExecuteStockOut";
            this.btnExecuteStockOut.Size = new System.Drawing.Size(772, 48);
            this.btnExecuteStockOut.TabIndex = 12;
            this.btnExecuteStockOut.Text = "CONFIRM STOCK OUT";
            this.btnExecuteStockOut.UseVisualStyleBackColor = false;
            // 
            // 
            // txtWarrantyInfo
            // 
            this.txtWarrantyInfo.BackColor = System.Drawing.Color.White;
            this.txtWarrantyInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWarrantyInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWarrantyInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.txtWarrantyInfo.Location = new System.Drawing.Point(437, 257);
            this.txtWarrantyInfo.Multiline = true;
            this.txtWarrantyInfo.Name = "txtWarrantyInfo";
            this.txtWarrantyInfo.ReadOnly = true;
            this.txtWarrantyInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWarrantyInfo.Size = new System.Drawing.Size(398, 166);
            this.txtWarrantyInfo.TabIndex = 11;
            this.txtWarrantyInfo.Text = "Warranty Expires: --/--/----";
            // 
            // lblStockStatus
            // 
            this.lblStockStatus.AutoSize = true;
            this.lblStockStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblStockStatus.Location = new System.Drawing.Point(634, 221);
            this.lblStockStatus.Name = "lblStockStatus";
            this.lblStockStatus.Size = new System.Drawing.Size(76, 19);
            this.lblStockStatus.TabIndex = 10;
            this.lblStockStatus.Text = "In Stock: 0";
            // 
            // numQty
            // 
            this.numQty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQty.Location = new System.Drawing.Point(440, 218);
            this.numQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(171, 27);
            this.numQty.TabIndex = 9;
            this.numQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblQty.Location = new System.Drawing.Point(437, 192);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(152, 17);
            this.lblQty.TabIndex = 8;
            this.lblQty.Text = "TOTAL SELECTED (AUTO)";
            // 
            // clbSerialNumbers
            // 
            this.clbSerialNumbers.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbSerialNumbers.FormattingEnabled = true;
            this.clbSerialNumbers.Items.AddRange(new object[] {
            "SN: APP-MBP-1001",
            "SN: APP-MBP-1002",
            "SN: APP-MBP-1003"});
            this.clbSerialNumbers.Location = new System.Drawing.Point(38, 218);
            this.clbSerialNumbers.Name = "clbSerialNumbers";
            this.clbSerialNumbers.Size = new System.Drawing.Size(370, 92);
            this.clbSerialNumbers.TabIndex = 7;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSerialNumber.Location = new System.Drawing.Point(35, 192);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(300, 17);
            this.lblSerialNumber.TabIndex = 6;
            this.lblSerialNumber.Text = "SELECT SPECIFIC SERIAL NUMBERS TO DISPATCH";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(38, 139);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(772, 28);
            this.cmbProduct.TabIndex = 5;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblProduct.Location = new System.Drawing.Point(35, 112);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(113, 17);
            this.lblProduct.TabIndex = 4;
            this.lblProduct.Text = "SELECT PRODUCT";
            // 
            // txtRecipient
            // 
            this.txtRecipient.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecipient.Location = new System.Drawing.Point(440, 59);
            this.txtRecipient.Name = "txtRecipient";
            this.txtRecipient.Size = new System.Drawing.Size(370, 27);
            this.txtRecipient.TabIndex = 3;
            // 
            // lblRecipient
            // 
            this.lblRecipient.AutoSize = true;
            this.lblRecipient.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecipient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRecipient.Location = new System.Drawing.Point(437, 32);
            this.lblRecipient.Name = "lblRecipient";
            this.lblRecipient.Size = new System.Drawing.Size(193, 17);
            this.lblRecipient.TabIndex = 2;
            this.lblRecipient.Text = "RECIPIENT / CUSTOMER NAME";
            // 
            // cmbOutReason
            // 
            this.cmbOutReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutReason.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOutReason.FormattingEnabled = true;
            this.cmbOutReason.Items.AddRange(new object[] {
            "Customer Sale",
            "Internal Use",
            "Damaged/Waste",
            "Return to Supplier"});
            this.cmbOutReason.Location = new System.Drawing.Point(38, 59);
            this.cmbOutReason.Name = "cmbOutReason";
            this.cmbOutReason.Size = new System.Drawing.Size(370, 28);
            this.cmbOutReason.TabIndex = 1;
            // 
            // lblOutReason
            // 
            this.lblOutReason.AutoSize = true;
            this.lblOutReason.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutReason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOutReason.Location = new System.Drawing.Point(35, 32);
            this.lblOutReason.Name = "lblOutReason";
            this.lblOutReason.Size = new System.Drawing.Size(131, 17);
            this.lblOutReason.TabIndex = 0;
            this.lblOutReason.Text = "TRANSACTION TYPE";
            // 
            // pnlWarrantyCard
            // 
            this.pnlWarrantyCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.pnlWarrantyCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWarrantyCard.Controls.Add(this.lblWarrantyTitle);
            this.pnlWarrantyCard.Controls.Add(this.lblWarrantyDuration);
            this.pnlWarrantyCard.Controls.Add(this.lblWarrantyExpiry);
            this.pnlWarrantyCard.Location = new System.Drawing.Point(38, 333);
            this.pnlWarrantyCard.Name = "pnlWarrantyCard";
            this.pnlWarrantyCard.Size = new System.Drawing.Size(370, 90);
            this.pnlWarrantyCard.TabIndex = 13;
            // 
            // lblWarrantyTitle
            // 
            this.lblWarrantyTitle.AutoSize = true;
            this.lblWarrantyTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarrantyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblWarrantyTitle.Location = new System.Drawing.Point(10, 8);
            this.lblWarrantyTitle.Name = "lblWarrantyTitle";
            this.lblWarrantyTitle.Size = new System.Drawing.Size(102, 12);
            this.lblWarrantyTitle.TabIndex = 0;
            this.lblWarrantyTitle.Text = "WARRANTY STATUS";
            // 
            // lblWarrantyDuration
            // 
            this.lblWarrantyDuration.AutoSize = true;
            this.lblWarrantyDuration.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarrantyDuration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblWarrantyDuration.Location = new System.Drawing.Point(8, 24);
            this.lblWarrantyDuration.Name = "lblWarrantyDuration";
            this.lblWarrantyDuration.Size = new System.Drawing.Size(35, 30);
            this.lblWarrantyDuration.TabIndex = 1;
            this.lblWarrantyDuration.Text = "—";
            // 
            // lblWarrantyExpiry
            // 
            this.lblWarrantyExpiry.AutoSize = true;
            this.lblWarrantyExpiry.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarrantyExpiry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblWarrantyExpiry.Location = new System.Drawing.Point(10, 63);
            this.lblWarrantyExpiry.Name = "lblWarrantyExpiry";
            this.lblWarrantyExpiry.Size = new System.Drawing.Size(190, 13);
            this.lblWarrantyExpiry.TabIndex = 2;
            this.lblWarrantyExpiry.Text = "Select items below to view warranty";
            // 
            // lblSystemID
            // 
            this.lblSystemID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSystemID.AutoSize = true;
            this.lblSystemID.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemID.ForeColor = System.Drawing.Color.Silver;
            this.lblSystemID.Location = new System.Drawing.Point(39, 702);
            this.lblSystemID.Name = "lblSystemID";
            this.lblSystemID.Size = new System.Drawing.Size(140, 13);
            this.lblSystemID.TabIndex = 4;
            this.lblSystemID.Text = "SYSTEM USER: UNKNOWN";
            // 
            // FrmStockOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1029, 683);
            this.Controls.Add(this.lblSystemID);
            this.Controls.Add(this.pnlMainCard);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmStockOut";
            this.Text = "FrmStockOut";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMainCard.ResumeLayout(false);
            this.pnlMainCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            this.pnlWarrantyCard.ResumeLayout(false);
            this.pnlWarrantyCard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
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
        private System.Windows.Forms.TextBox txtWarrantyInfo;
        private System.Windows.Forms.Button btnExecuteStockOut;
        private System.Windows.Forms.Label lblSystemID;
        private System.Windows.Forms.Panel pnlWarrantyCard;
        private System.Windows.Forms.Label lblWarrantyTitle;
        private System.Windows.Forms.Label lblWarrantyDuration;
        private System.Windows.Forms.Label lblWarrantyExpiry;
    }
}

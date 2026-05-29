namespace InventoryManagementSystem
{
    partial class FrmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.pnlTotalProducts = new System.Windows.Forms.Panel();
            this.lblTotalDesc = new System.Windows.Forms.Label();
            this.lblTotalNum = new System.Windows.Forms.Label();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.pnlLowStock = new System.Windows.Forms.Panel();
            this.lblLowStockDesc = new System.Windows.Forms.Label();
            this.lblLowStockNum = new System.Windows.Forms.Label();
            this.lblLowStockTitle = new System.Windows.Forms.Label();
            this.dgvRecentActions = new System.Windows.Forms.DataGridView();
            this.lblRecentMovements = new System.Windows.Forms.Label();
            this.pnlTotalProducts.SuspendLayout();
            this.pnlLowStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentActions)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblMainTitle.Location = new System.Drawing.Point(40, 30);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(183, 45);
            this.lblMainTitle.TabIndex = 0;
            this.lblMainTitle.Text = "Dashboard";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSubTitle.Location = new System.Drawing.Point(44, 75);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(268, 20);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Overview of current inventory health.";
            // 
            // pnlTotalProducts
            // 
            this.pnlTotalProducts.BackColor = System.Drawing.Color.White;
            this.pnlTotalProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotalProducts.Controls.Add(this.lblTotalDesc);
            this.pnlTotalProducts.Controls.Add(this.lblTotalNum);
            this.pnlTotalProducts.Controls.Add(this.lblTotalTitle);
            this.pnlTotalProducts.Location = new System.Drawing.Point(48, 120);
            this.pnlTotalProducts.Name = "pnlTotalProducts";
            this.pnlTotalProducts.Size = new System.Drawing.Size(320, 160);
            this.pnlTotalProducts.TabIndex = 2;
            // 
            // lblTotalDesc
            // 
            this.lblTotalDesc.AutoSize = true;
            this.lblTotalDesc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblTotalDesc.Location = new System.Drawing.Point(22, 115);
            this.lblTotalDesc.Name = "lblTotalDesc";
            this.lblTotalDesc.Size = new System.Drawing.Size(130, 17);
            this.lblTotalDesc.TabIndex = 2;
            this.lblTotalDesc.Text = "Active items in stock";
            // 
            // lblTotalNum
            // 
            this.lblTotalNum.AutoSize = true;
            this.lblTotalNum.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblTotalNum.Location = new System.Drawing.Point(13, 45);
            this.lblTotalNum.Name = "lblTotalNum";
            this.lblTotalNum.Size = new System.Drawing.Size(153, 65);
            this.lblTotalNum.TabIndex = 0;
            this.lblTotalNum.Text = "1,284";
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(115, 21);
            this.lblTotalTitle.TabIndex = 1;
            this.lblTotalTitle.Text = "Total Products";
            // 
            // pnlLowStock
            // 
            this.pnlLowStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlLowStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLowStock.Controls.Add(this.lblLowStockDesc);
            this.pnlLowStock.Controls.Add(this.lblLowStockNum);
            this.pnlLowStock.Controls.Add(this.lblLowStockTitle);
            this.pnlLowStock.Location = new System.Drawing.Point(398, 120);
            this.pnlLowStock.Name = "pnlLowStock";
            this.pnlLowStock.Size = new System.Drawing.Size(320, 160);
            this.pnlLowStock.TabIndex = 3;
            // 
            // lblLowStockDesc
            // 
            this.lblLowStockDesc.AutoSize = true;
            this.lblLowStockDesc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowStockDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLowStockDesc.Location = new System.Drawing.Point(22, 115);
            this.lblLowStockDesc.Name = "lblLowStockDesc";
            this.lblLowStockDesc.Size = new System.Drawing.Size(155, 17);
            this.lblLowStockDesc.TabIndex = 2;
            this.lblLowStockDesc.Text = "Requires immediate refill";
            // 
            // lblLowStockNum
            // 
            this.lblLowStockNum.AutoSize = true;
            this.lblLowStockNum.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowStockNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblLowStockNum.Location = new System.Drawing.Point(13, 45);
            this.lblLowStockNum.Name = "lblLowStockNum";
            this.lblLowStockNum.Size = new System.Drawing.Size(84, 65);
            this.lblLowStockNum.TabIndex = 0;
            this.lblLowStockNum.Text = "18";
            // 
            // lblLowStockTitle
            // 
            this.lblLowStockTitle.AutoSize = true;
            this.lblLowStockTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowStockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblLowStockTitle.Location = new System.Drawing.Point(20, 20);
            this.lblLowStockTitle.Name = "lblLowStockTitle";
            this.lblLowStockTitle.Size = new System.Drawing.Size(133, 21);
            this.lblLowStockTitle.TabIndex = 1;
            this.lblLowStockTitle.Text = "Low-Stock Alerts";
            // 
            // dgvRecentActions
            // 
            this.dgvRecentActions.AllowUserToAddRows = false;
            this.dgvRecentActions.AllowUserToDeleteRows = false;
            this.dgvRecentActions.AllowUserToResizeRows = false;
            this.dgvRecentActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecentActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentActions.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentActions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecentActions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecentActions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentActions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecentActions.ColumnHeadersHeight = 40;
            this.dgvRecentActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecentActions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecentActions.EnableHeadersVisualStyles = false;
            this.dgvRecentActions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dgvRecentActions.Location = new System.Drawing.Point(48, 350);
            this.dgvRecentActions.MultiSelect = false;
            this.dgvRecentActions.Name = "dgvRecentActions";
            this.dgvRecentActions.ReadOnly = true;
            this.dgvRecentActions.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentActions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecentActions.RowHeadersVisible = false;
            this.dgvRecentActions.RowTemplate.Height = 35;
            this.dgvRecentActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentActions.Size = new System.Drawing.Size(1000, 310);
            this.dgvRecentActions.TabIndex = 5;
            // 
            // lblRecentMovements
            // 
            this.lblRecentMovements.AutoSize = true;
            this.lblRecentMovements.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentMovements.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblRecentMovements.Location = new System.Drawing.Point(43, 310);
            this.lblRecentMovements.Name = "lblRecentMovements";
            this.lblRecentMovements.Size = new System.Drawing.Size(232, 25);
            this.lblRecentMovements.TabIndex = 4;
            this.lblRecentMovements.Text = "Recent Stock Movements";
            // 
            // FrmDashboard
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.lblMainTitle);
            this.Controls.Add(this.lblSubTitle);
            this.Controls.Add(this.pnlTotalProducts);
            this.Controls.Add(this.pnlLowStock);
            this.Controls.Add(this.lblRecentMovements);
            this.Controls.Add(this.dgvRecentActions);
            this.Name = "FrmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.pnlTotalProducts.ResumeLayout(false);
            this.pnlTotalProducts.PerformLayout();
            this.pnlLowStock.ResumeLayout(false);
            this.pnlLowStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentActions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel pnlTotalProducts;
        private System.Windows.Forms.Label lblTotalDesc;
        private System.Windows.Forms.Label lblTotalNum;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Panel pnlLowStock;
        private System.Windows.Forms.Label lblLowStockDesc;
        private System.Windows.Forms.Label lblLowStockNum;
        private System.Windows.Forms.Label lblLowStockTitle;
        private System.Windows.Forms.DataGridView dgvRecentActions;
        private System.Windows.Forms.Label lblRecentMovements;
    }
}
namespace InventoryManagementSystem
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.pnlSideBar = new System.Windows.Forms.Panel();
            this.btnStockOut = new System.Windows.Forms.Button();
            this.picStockOut = new System.Windows.Forms.PictureBox();
            this.btnStockIn = new System.Windows.Forms.Button();
            this.picStockIn = new System.Windows.Forms.PictureBox();
            this.btnProducts = new System.Windows.Forms.Button();
            this.picProducts = new System.Windows.Forms.PictureBox();
            this.picNav = new System.Windows.Forms.PictureBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.picDashboard = new System.Windows.Forms.PictureBox();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.lblSystemID = new System.Windows.Forms.Label();
            this.lblCompanyDetails = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.picLogoMain = new System.Windows.Forms.PictureBox();
            this.sidebarTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlSideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStockOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStockIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDashboard)).BeginInit();
            this.pnlMainContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.pnlSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlSideBar.Controls.Add(this.btnStockOut);
            this.pnlSideBar.Controls.Add(this.picStockOut);
            this.pnlSideBar.Controls.Add(this.btnStockIn);
            this.pnlSideBar.Controls.Add(this.picStockIn);
            this.pnlSideBar.Controls.Add(this.btnProducts);
            this.pnlSideBar.Controls.Add(this.picProducts);
            this.pnlSideBar.Controls.Add(this.picNav);
            this.pnlSideBar.Controls.Add(this.btnDashboard);
            this.pnlSideBar.Controls.Add(this.picDashboard);
            this.pnlSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(235, 691);
            this.pnlSideBar.TabIndex = 2;
            // 
            // btnStockOut
            // 
            this.btnStockOut.BackColor = System.Drawing.Color.Transparent;
            this.btnStockOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStockOut.FlatAppearance.BorderSize = 0;
            this.btnStockOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(155)))), ((int)(((byte)(254)))));
            this.btnStockOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockOut.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStockOut.ForeColor = System.Drawing.Color.White;
            this.btnStockOut.Location = new System.Drawing.Point(62, 338);
            this.btnStockOut.Name = "btnStockOut";
            this.btnStockOut.Size = new System.Drawing.Size(163, 40);
            this.btnStockOut.TabIndex = 12;
            this.btnStockOut.Text = "Stock Out";
            this.btnStockOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockOut.UseVisualStyleBackColor = false;
            this.btnStockOut.Click += new System.EventHandler(this.btnStockOut_Click);
            // 
            // picStockOut
            // 
            this.picStockOut.BackColor = System.Drawing.Color.Transparent;
            this.picStockOut.Image = ((System.Drawing.Image)(resources.GetObject("picStockOut.Image")));
            this.picStockOut.Location = new System.Drawing.Point(16, 341);
            this.picStockOut.Name = "picStockOut";
            this.picStockOut.Size = new System.Drawing.Size(35, 35);
            this.picStockOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStockOut.TabIndex = 11;
            this.picStockOut.TabStop = false;
            // 
            // btnStockIn
            // 
            this.btnStockIn.BackColor = System.Drawing.Color.Transparent;
            this.btnStockIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStockIn.FlatAppearance.BorderSize = 0;
            this.btnStockIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(155)))), ((int)(((byte)(254)))));
            this.btnStockIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockIn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStockIn.ForeColor = System.Drawing.Color.White;
            this.btnStockIn.Location = new System.Drawing.Point(63, 262);
            this.btnStockIn.Name = "btnStockIn";
            this.btnStockIn.Size = new System.Drawing.Size(163, 40);
            this.btnStockIn.TabIndex = 10;
            this.btnStockIn.Text = "Stock In";
            this.btnStockIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockIn.UseVisualStyleBackColor = false;
            this.btnStockIn.Click += new System.EventHandler(this.btnStockIn_Click);
            // 
            // picStockIn
            // 
            this.picStockIn.BackColor = System.Drawing.Color.Transparent;
            this.picStockIn.Image = ((System.Drawing.Image)(resources.GetObject("picStockIn.Image")));
            this.picStockIn.Location = new System.Drawing.Point(17, 265);
            this.picStockIn.Name = "picStockIn";
            this.picStockIn.Size = new System.Drawing.Size(35, 35);
            this.picStockIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStockIn.TabIndex = 9;
            this.picStockIn.TabStop = false;
            // 
            // btnProducts
            // 
            this.btnProducts.BackColor = System.Drawing.Color.Transparent;
            this.btnProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProducts.FlatAppearance.BorderSize = 0;
            this.btnProducts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(155)))), ((int)(((byte)(254)))));
            this.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducts.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProducts.ForeColor = System.Drawing.Color.White;
            this.btnProducts.Location = new System.Drawing.Point(62, 188);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(163, 40);
            this.btnProducts.TabIndex = 8;
            this.btnProducts.Text = "Products";
            this.btnProducts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProducts.UseVisualStyleBackColor = false;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // picProducts
            // 
            this.picProducts.BackColor = System.Drawing.Color.Transparent;
            this.picProducts.Image = ((System.Drawing.Image)(resources.GetObject("picProducts.Image")));
            this.picProducts.Location = new System.Drawing.Point(16, 191);
            this.picProducts.Name = "picProducts";
            this.picProducts.Size = new System.Drawing.Size(35, 35);
            this.picProducts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picProducts.TabIndex = 7;
            this.picProducts.TabStop = false;
            // 
            // picNav
            // 
            this.picNav.BackColor = System.Drawing.Color.Transparent;
            this.picNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picNav.Image = ((System.Drawing.Image)(resources.GetObject("picNav.Image")));
            this.picNav.Location = new System.Drawing.Point(-6, 0);
            this.picNav.Name = "picNav";
            this.picNav.Size = new System.Drawing.Size(81, 76);
            this.picNav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNav.TabIndex = 0;
            this.picNav.TabStop = false;
            this.picNav.Click += new System.EventHandler(this.picNav_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.Transparent;
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(155)))), ((int)(((byte)(254)))));
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(62, 113);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(163, 40);
            this.btnDashboard.TabIndex = 6;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // picDashboard
            // 
            this.picDashboard.BackColor = System.Drawing.Color.Transparent;
            this.picDashboard.Image = ((System.Drawing.Image)(resources.GetObject("picDashboard.Image")));
            this.picDashboard.Location = new System.Drawing.Point(16, 116);
            this.picDashboard.Name = "picDashboard";
            this.picDashboard.Size = new System.Drawing.Size(35, 35);
            this.picDashboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDashboard.TabIndex = 1;
            this.picDashboard.TabStop = false;
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.BackColor = System.Drawing.Color.White;
            this.pnlMainContent.Controls.Add(this.lblSystemID);
            this.pnlMainContent.Controls.Add(this.lblCompanyDetails);
            this.pnlMainContent.Controls.Add(this.lblCompanyName);
            this.pnlMainContent.Controls.Add(this.picLogoMain);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(235, 0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1054, 691);
            this.pnlMainContent.TabIndex = 3;
            // 
            // lblSystemID
            // 
            this.lblSystemID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSystemID.AutoSize = true;
            this.lblSystemID.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSystemID.ForeColor = System.Drawing.Color.Silver;
            this.lblSystemID.Location = new System.Drawing.Point(821, 661);
            this.lblSystemID.Name = "lblSystemID";
            this.lblSystemID.Size = new System.Drawing.Size(221, 21);
            this.lblSystemID.TabIndex = 16;
            this.lblSystemID.Text = "SYSTEM USER: BARAAH AREBI";
            // 
            // lblCompanyDetails
            // 
            this.lblCompanyDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.lblCompanyDetails.Location = new System.Drawing.Point(327, 350);
            this.lblCompanyDetails.Name = "lblCompanyDetails";
            this.lblCompanyDetails.Size = new System.Drawing.Size(400, 60);
            this.lblCompanyDetails.TabIndex = 15;
            this.lblCompanyDetails.Text = "Address | Phone | Email";
            this.lblCompanyDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCompanyName.Location = new System.Drawing.Point(327, 310);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(400, 40);
            this.lblCompanyName.TabIndex = 14;
            this.lblCompanyName.Text = "Modern Technology Co";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLogoMain
            // 
            this.picLogoMain.BackColor = System.Drawing.Color.Transparent;
            this.picLogoMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picLogoMain.BackgroundImage")));
            this.picLogoMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLogoMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogoMain.Location = new System.Drawing.Point(334, 13);
            this.picLogoMain.Name = "picLogoMain";
            this.picLogoMain.Size = new System.Drawing.Size(378, 365);
            this.picLogoMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogoMain.TabIndex = 13;
            this.picLogoMain.TabStop = false;
            // 
            // sidebarTimer
            // 
            this.sidebarTimer.Enabled = true;
            this.sidebarTimer.Interval = 10;
            this.sidebarTimer.Tick += new System.EventHandler(this.sidebarTimer_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 691);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlSideBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.pnlSideBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picStockOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStockIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDashboard)).EndInit();
            this.pnlMainContent.ResumeLayout(false);
            this.pnlMainContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Button btnStockOut;
        private System.Windows.Forms.PictureBox picStockOut;
        private System.Windows.Forms.Button btnStockIn;
        private System.Windows.Forms.PictureBox picStockIn;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.PictureBox picProducts;
        private System.Windows.Forms.PictureBox picNav;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.PictureBox picDashboard;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.PictureBox picLogoMain;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblCompanyDetails;
        private System.Windows.Forms.Timer sidebarTimer;
        private System.Windows.Forms.Label lblSystemID;
    }
}
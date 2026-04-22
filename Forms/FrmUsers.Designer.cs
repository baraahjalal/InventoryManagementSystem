namespace InventoryManagementSystem
{
    partial class FrmUsers
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.lblPermissionsTitle = new System.Windows.Forms.Label();
            this.tcPermissions = new System.Windows.Forms.TabControl();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.chkCanViewProducts = new System.Windows.Forms.CheckBox();
            this.chkCanAddProducts = new System.Windows.Forms.CheckBox();
            this.chkCanEditProducts = new System.Windows.Forms.CheckBox();
            this.chkCanDeleteProducts = new System.Windows.Forms.CheckBox();
            this.chkCanDoStockIn = new System.Windows.Forms.CheckBox();
            this.chkCanDoStockOut = new System.Windows.Forms.CheckBox();
            this.tabAdmin = new System.Windows.Forms.TabPage();
            this.chkCanViewDashboard = new System.Windows.Forms.CheckBox();
            this.chkCanViewSuppliers = new System.Windows.Forms.CheckBox();
            this.chkCanManageSuppliers = new System.Windows.Forms.CheckBox();
            this.chkCanViewUsers = new System.Windows.Forms.CheckBox();
            this.chkCanManageUsers = new System.Windows.Forms.CheckBox();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.chkCanViewReports = new System.Windows.Forms.CheckBox();
            this.chkCanPrint = new System.Windows.Forms.CheckBox();
            this.chkCanViewAuditLog = new System.Windows.Forms.CheckBox();
            this.btnResetPass = new System.Windows.Forms.Button();
            this.lblUserPassword = new System.Windows.Forms.Label();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnSetPic = new System.Windows.Forms.Button();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.cmsDeletePicture = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRegNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlActionButtons = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.pnlDetails.SuspendLayout();
            this.tcPermissions.SuspendLayout();
            this.tabInventory.SuspendLayout();
            this.tabAdmin.SuspendLayout();
            this.tabReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            this.cmsDeletePicture.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.pnlActionButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetails.Controls.Add(this.lblPermissionsTitle);
            this.pnlDetails.Controls.Add(this.tcPermissions);
            this.pnlDetails.Controls.Add(this.btnResetPass);
            this.pnlDetails.Controls.Add(this.lblUserPassword);
            this.pnlDetails.Controls.Add(this.txtUserPassword);
            this.pnlDetails.Controls.Add(this.lblRole);
            this.pnlDetails.Controls.Add(this.cmbRole);
            this.pnlDetails.Controls.Add(this.lblUserName);
            this.pnlDetails.Controls.Add(this.txtUserName);
            this.pnlDetails.Controls.Add(this.btnSetPic);
            this.pnlDetails.Controls.Add(this.picUser);
            this.pnlDetails.Controls.Add(this.lblDetailsTitle);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetails.Location = new System.Drawing.Point(680, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDetails.Size = new System.Drawing.Size(420, 700);
            this.pnlDetails.TabIndex = 0;
            // 
            // lblPermissionsTitle
            // 
            this.lblPermissionsTitle.AutoSize = true;
            this.lblPermissionsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblPermissionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblPermissionsTitle.Location = new System.Drawing.Point(18, 410);
            this.lblPermissionsTitle.Name = "lblPermissionsTitle";
            this.lblPermissionsTitle.Size = new System.Drawing.Size(132, 21);
            this.lblPermissionsTitle.TabIndex = 11;
            this.lblPermissionsTitle.Text = "User Permissions";
            // 
            // tcPermissions
            // 
            this.tcPermissions.Controls.Add(this.tabInventory);
            this.tcPermissions.Controls.Add(this.tabAdmin);
            this.tcPermissions.Controls.Add(this.tabReports);
            this.tcPermissions.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tcPermissions.ItemSize = new System.Drawing.Size(110, 30);
            this.tcPermissions.Location = new System.Drawing.Point(22, 440);
            this.tcPermissions.Name = "tcPermissions";
            this.tcPermissions.SelectedIndex = 0;
            this.tcPermissions.Size = new System.Drawing.Size(376, 190);
            this.tcPermissions.TabIndex = 10;
            // 
            // tabInventory
            // 
            this.tabInventory.BackColor = System.Drawing.Color.White;
            this.tabInventory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabInventory.Controls.Add(this.chkCanViewProducts);
            this.tabInventory.Controls.Add(this.chkCanAddProducts);
            this.tabInventory.Controls.Add(this.chkCanEditProducts);
            this.tabInventory.Controls.Add(this.chkCanDeleteProducts);
            this.tabInventory.Controls.Add(this.chkCanDoStockIn);
            this.tabInventory.Controls.Add(this.chkCanDoStockOut);
            this.tabInventory.Location = new System.Drawing.Point(4, 34);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Padding = new System.Windows.Forms.Padding(15);
            this.tabInventory.Size = new System.Drawing.Size(368, 152);
            this.tabInventory.TabIndex = 0;
            this.tabInventory.Text = "Inventory";
            // 
            // chkCanViewProducts
            // 
            this.chkCanViewProducts.AutoSize = true;
            this.chkCanViewProducts.Location = new System.Drawing.Point(18, 18);
            this.chkCanViewProducts.Name = "chkCanViewProducts";
            this.chkCanViewProducts.Size = new System.Drawing.Size(111, 21);
            this.chkCanViewProducts.TabIndex = 0;
            this.chkCanViewProducts.Text = "View Products";
            this.chkCanViewProducts.UseVisualStyleBackColor = true;
            // 
            // chkCanAddProducts
            // 
            this.chkCanAddProducts.AutoSize = true;
            this.chkCanAddProducts.Location = new System.Drawing.Point(18, 58);
            this.chkCanAddProducts.Name = "chkCanAddProducts";
            this.chkCanAddProducts.Size = new System.Drawing.Size(107, 21);
            this.chkCanAddProducts.TabIndex = 1;
            this.chkCanAddProducts.Text = "Add Products";
            this.chkCanAddProducts.UseVisualStyleBackColor = true;
            // 
            // chkCanEditProducts
            // 
            this.chkCanEditProducts.AutoSize = true;
            this.chkCanEditProducts.Location = new System.Drawing.Point(18, 98);
            this.chkCanEditProducts.Name = "chkCanEditProducts";
            this.chkCanEditProducts.Size = new System.Drawing.Size(105, 21);
            this.chkCanEditProducts.TabIndex = 2;
            this.chkCanEditProducts.Text = "Edit Products";
            this.chkCanEditProducts.UseVisualStyleBackColor = true;
            // 
            // chkCanDeleteProducts
            // 
            this.chkCanDeleteProducts.AutoSize = true;
            this.chkCanDeleteProducts.Location = new System.Drawing.Point(180, 18);
            this.chkCanDeleteProducts.Name = "chkCanDeleteProducts";
            this.chkCanDeleteProducts.Size = new System.Drawing.Size(120, 21);
            this.chkCanDeleteProducts.TabIndex = 3;
            this.chkCanDeleteProducts.Text = "Delete Products";
            this.chkCanDeleteProducts.UseVisualStyleBackColor = true;
            // 
            // chkCanDoStockIn
            // 
            this.chkCanDoStockIn.AutoSize = true;
            this.chkCanDoStockIn.Location = new System.Drawing.Point(180, 58);
            this.chkCanDoStockIn.Name = "chkCanDoStockIn";
            this.chkCanDoStockIn.Size = new System.Drawing.Size(126, 21);
            this.chkCanDoStockIn.TabIndex = 4;
            this.chkCanDoStockIn.Text = "Process Stock In";
            this.chkCanDoStockIn.UseVisualStyleBackColor = true;
            // 
            // chkCanDoStockOut
            // 
            this.chkCanDoStockOut.AutoSize = true;
            this.chkCanDoStockOut.Location = new System.Drawing.Point(180, 98);
            this.chkCanDoStockOut.Name = "chkCanDoStockOut";
            this.chkCanDoStockOut.Size = new System.Drawing.Size(136, 21);
            this.chkCanDoStockOut.TabIndex = 5;
            this.chkCanDoStockOut.Text = "Process Stock Out";
            this.chkCanDoStockOut.UseVisualStyleBackColor = true;
            // 
            // tabAdmin
            // 
            this.tabAdmin.BackColor = System.Drawing.Color.White;
            this.tabAdmin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabAdmin.Controls.Add(this.chkCanViewDashboard);
            this.tabAdmin.Controls.Add(this.chkCanViewSuppliers);
            this.tabAdmin.Controls.Add(this.chkCanManageSuppliers);
            this.tabAdmin.Controls.Add(this.chkCanViewUsers);
            this.tabAdmin.Controls.Add(this.chkCanManageUsers);
            this.tabAdmin.Location = new System.Drawing.Point(4, 34);
            this.tabAdmin.Name = "tabAdmin";
            this.tabAdmin.Padding = new System.Windows.Forms.Padding(15);
            this.tabAdmin.Size = new System.Drawing.Size(368, 152);
            this.tabAdmin.TabIndex = 1;
            this.tabAdmin.Text = "Administration";
            // 
            // chkCanViewDashboard
            // 
            this.chkCanViewDashboard.AutoSize = true;
            this.chkCanViewDashboard.Location = new System.Drawing.Point(18, 18);
            this.chkCanViewDashboard.Name = "chkCanViewDashboard";
            this.chkCanViewDashboard.Size = new System.Drawing.Size(123, 21);
            this.chkCanViewDashboard.TabIndex = 0;
            this.chkCanViewDashboard.Text = "View Dashboard";
            this.chkCanViewDashboard.UseVisualStyleBackColor = true;
            // 
            // chkCanViewSuppliers
            // 
            this.chkCanViewSuppliers.AutoSize = true;
            this.chkCanViewSuppliers.Location = new System.Drawing.Point(18, 58);
            this.chkCanViewSuppliers.Name = "chkCanViewSuppliers";
            this.chkCanViewSuppliers.Size = new System.Drawing.Size(113, 21);
            this.chkCanViewSuppliers.TabIndex = 1;
            this.chkCanViewSuppliers.Text = "View Suppliers";
            this.chkCanViewSuppliers.UseVisualStyleBackColor = true;
            // 
            // chkCanManageSuppliers
            // 
            this.chkCanManageSuppliers.AutoSize = true;
            this.chkCanManageSuppliers.Location = new System.Drawing.Point(18, 98);
            this.chkCanManageSuppliers.Name = "chkCanManageSuppliers";
            this.chkCanManageSuppliers.Size = new System.Drawing.Size(133, 21);
            this.chkCanManageSuppliers.TabIndex = 2;
            this.chkCanManageSuppliers.Text = "Manage Suppliers";
            this.chkCanManageSuppliers.UseVisualStyleBackColor = true;
            // 
            // chkCanViewUsers
            // 
            this.chkCanViewUsers.AutoSize = true;
            this.chkCanViewUsers.Location = new System.Drawing.Point(180, 18);
            this.chkCanViewUsers.Name = "chkCanViewUsers";
            this.chkCanViewUsers.Size = new System.Drawing.Size(91, 21);
            this.chkCanViewUsers.TabIndex = 3;
            this.chkCanViewUsers.Text = "View Users";
            this.chkCanViewUsers.UseVisualStyleBackColor = true;
            // 
            // chkCanManageUsers
            // 
            this.chkCanManageUsers.AutoSize = true;
            this.chkCanManageUsers.Location = new System.Drawing.Point(180, 58);
            this.chkCanManageUsers.Name = "chkCanManageUsers";
            this.chkCanManageUsers.Size = new System.Drawing.Size(111, 21);
            this.chkCanManageUsers.TabIndex = 4;
            this.chkCanManageUsers.Text = "Manage Users";
            this.chkCanManageUsers.UseVisualStyleBackColor = true;
            // 
            // tabReports
            // 
            this.tabReports.BackColor = System.Drawing.Color.White;
            this.tabReports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabReports.Controls.Add(this.chkCanViewReports);
            this.tabReports.Controls.Add(this.chkCanPrint);
            this.tabReports.Controls.Add(this.chkCanViewAuditLog);
            this.tabReports.Location = new System.Drawing.Point(4, 34);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(15);
            this.tabReports.Size = new System.Drawing.Size(368, 152);
            this.tabReports.TabIndex = 2;
            this.tabReports.Text = "Reports & Logs";
            // 
            // chkCanViewReports
            // 
            this.chkCanViewReports.AutoSize = true;
            this.chkCanViewReports.Location = new System.Drawing.Point(18, 18);
            this.chkCanViewReports.Name = "chkCanViewReports";
            this.chkCanViewReports.Size = new System.Drawing.Size(105, 21);
            this.chkCanViewReports.TabIndex = 0;
            this.chkCanViewReports.Text = "View Reports";
            this.chkCanViewReports.UseVisualStyleBackColor = true;
            // 
            // chkCanPrint
            // 
            this.chkCanPrint.AutoSize = true;
            this.chkCanPrint.Location = new System.Drawing.Point(180, 18);
            this.chkCanPrint.Name = "chkCanPrint";
            this.chkCanPrint.Size = new System.Drawing.Size(117, 21);
            this.chkCanPrint.TabIndex = 1;
            this.chkCanPrint.Text = "Print Outcomes";
            this.chkCanPrint.UseVisualStyleBackColor = true;
            // 
            // chkCanViewAuditLog
            // 
            this.chkCanViewAuditLog.AutoSize = true;
            this.chkCanViewAuditLog.Location = new System.Drawing.Point(18, 58);
            this.chkCanViewAuditLog.Name = "chkCanViewAuditLog";
            this.chkCanViewAuditLog.Size = new System.Drawing.Size(115, 21);
            this.chkCanViewAuditLog.TabIndex = 2;
            this.chkCanViewAuditLog.Text = "View Audit Log";
            this.chkCanViewAuditLog.UseVisualStyleBackColor = true;
            // 
            // btnResetPass
            // 
            this.btnResetPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnResetPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetPass.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnResetPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnResetPass.ForeColor = System.Drawing.Color.Black;
            this.btnResetPass.Location = new System.Drawing.Point(232, 290);
            this.btnResetPass.Name = "btnResetPass";
            this.btnResetPass.Size = new System.Drawing.Size(96, 27);
            this.btnResetPass.TabIndex = 9;
            this.btnResetPass.Text = "Reset";
            this.btnResetPass.UseVisualStyleBackColor = false;
            // 
            // lblUserPassword
            // 
            this.lblUserPassword.AutoSize = true;
            this.lblUserPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblUserPassword.Location = new System.Drawing.Point(19, 271);
            this.lblUserPassword.Name = "lblUserPassword";
            this.lblUserPassword.Size = new System.Drawing.Size(57, 15);
            this.lblUserPassword.TabIndex = 8;
            this.lblUserPassword.Text = "Password";
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUserPassword.Location = new System.Drawing.Point(22, 290);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.ReadOnly = true;
            this.txtUserPassword.Size = new System.Drawing.Size(200, 27);
            this.txtUserPassword.TabIndex = 7;
            this.txtUserPassword.UseSystemPasswordChar = true;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblRole.Location = new System.Drawing.Point(19, 335);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(72, 15);
            this.lblRole.TabIndex = 6;
            this.lblRole.Text = "System Role";
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "System Administrator",
            "Stock Clerk",
            "Reporting Officer",
            "Procurement Manager"});
            this.cmbRole.Location = new System.Drawing.Point(22, 355);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(306, 28);
            this.cmbRole.TabIndex = 5;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblUserName.Location = new System.Drawing.Point(19, 206);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 15);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "Username";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUserName.Location = new System.Drawing.Point(22, 226);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(306, 27);
            this.txtUserName.TabIndex = 3;
            // 
            // btnSetPic
            // 
            this.btnSetPic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnSetPic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetPic.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnSetPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSetPic.ForeColor = System.Drawing.Color.Black;
            this.btnSetPic.Location = new System.Drawing.Point(22, 114);
            this.btnSetPic.Name = "btnSetPic";
            this.btnSetPic.Size = new System.Drawing.Size(100, 30);
            this.btnSetPic.TabIndex = 2;
            this.btnSetPic.Text = "Set Picture";
            this.btnSetPic.UseVisualStyleBackColor = false;
            // 
            // picUser
            // 
            this.picUser.BackColor = System.Drawing.Color.White;
            this.picUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picUser.ContextMenuStrip = this.cmsDeletePicture;
            this.picUser.Location = new System.Drawing.Point(148, 65);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(120, 120);
            this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUser.TabIndex = 1;
            this.picUser.TabStop = false;
            // 
            // cmsDeletePicture
            // 
            this.cmsDeletePicture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDelete});
            this.cmsDeletePicture.Name = "cmsDeletePicture";
            this.cmsDeletePicture.Size = new System.Drawing.Size(148, 26);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemDelete.Text = "Delete Picture";
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblDetailsTitle.Location = new System.Drawing.Point(18, 20);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(158, 25);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "User Information";
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.BackColor = System.Drawing.Color.White;
            this.pnlGridContainer.Controls.Add(this.dgvUsers);
            this.pnlGridContainer.Controls.Add(this.pnlActionButtons);
            this.pnlGridContainer.Location = new System.Drawing.Point(0, 80);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(20);
            this.pnlGridContainer.Size = new System.Drawing.Size(680, 620);
            this.pnlGridContainer.TabIndex = 1;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dgvUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvUsers.ColumnHeadersHeight = 45;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNo,
            this.colUserName,
            this.colPassword,
            this.colJob,
            this.colRegNum,
            this.colImage});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.EnableHeadersVisualStyles = false;
            this.dgvUsers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvUsers.Location = new System.Drawing.Point(20, 20);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.RowTemplate.Height = 40;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(640, 510);
            this.dgvUsers.TabIndex = 0;
            // 
            // colId
            // 
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colNo
            // 
            this.colNo.FillWeight = 40F;
            this.colNo.HeaderText = "No";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colUserName
            // 
            this.colUserName.FillWeight = 180F;
            this.colUserName.HeaderText = "Username";
            this.colUserName.Name = "colUserName";
            this.colUserName.ReadOnly = true;
            // 
            // colPassword
            // 
            this.colPassword.HeaderText = "Password";
            this.colPassword.Name = "colPassword";
            this.colPassword.ReadOnly = true;
            this.colPassword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colJob
            // 
            this.colJob.FillWeight = 110F;
            this.colJob.HeaderText = "Job";
            this.colJob.Name = "colJob";
            this.colJob.ReadOnly = true;
            // 
            // colRegNum
            // 
            this.colRegNum.FillWeight = 80F;
            this.colRegNum.HeaderText = "Reg Number";
            this.colRegNum.Name = "colRegNum";
            this.colRegNum.ReadOnly = true;
            // 
            // colImage
            // 
            this.colImage.FillWeight = 80F;
            this.colImage.HeaderText = "Image";
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            // 
            // pnlActionButtons
            // 
            this.pnlActionButtons.Controls.Add(this.btnRefresh);
            this.pnlActionButtons.Controls.Add(this.btnDelete);
            this.pnlActionButtons.Controls.Add(this.btnUpdate);
            this.pnlActionButtons.Controls.Add(this.btnAdd);
            this.pnlActionButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionButtons.Location = new System.Drawing.Point(20, 530);
            this.pnlActionButtons.Name = "pnlActionButtons";
            this.pnlActionButtons.Size = new System.Drawing.Size(640, 70);
            this.pnlActionButtons.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnRefresh.Location = new System.Drawing.Point(0, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(110, 40);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(125, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 40);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.White;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnUpdate.Location = new System.Drawing.Point(405, 15);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 40);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(530, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 40);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add User";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblMainTitle.Location = new System.Drawing.Point(12, 9);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(308, 45);
            this.lblMainTitle.TabIndex = 4;
            this.lblMainTitle.Text = "Users Management";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSubTitle.Location = new System.Drawing.Point(16, 54);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(183, 20);
            this.lblSubTitle.TabIndex = 5;
            this.lblSubTitle.Text = "Manage your current users";
            // 
            // FrmUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.lblMainTitle);
            this.Controls.Add(this.lblSubTitle);
            this.Controls.Add(this.pnlGridContainer);
            this.Controls.Add(this.pnlDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1000, 640);
            this.Name = "FrmUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Users";
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.tcPermissions.ResumeLayout(false);
            this.tabInventory.ResumeLayout(false);
            this.tabInventory.PerformLayout();
            this.tabAdmin.ResumeLayout(false);
            this.tabAdmin.PerformLayout();
            this.tabReports.ResumeLayout(false);
            this.tabReports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            this.cmsDeletePicture.ResumeLayout(false);
            this.pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.pnlActionButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Panel pnlActionButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.Button btnSetPic;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label lblUserPassword;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Button btnResetPass;
        private System.Windows.Forms.Label lblPermissionsTitle;
        private System.Windows.Forms.TabControl tcPermissions;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.CheckBox chkCanViewProducts;
        private System.Windows.Forms.CheckBox chkCanAddProducts;
        private System.Windows.Forms.CheckBox chkCanEditProducts;
        private System.Windows.Forms.CheckBox chkCanDeleteProducts;
        private System.Windows.Forms.CheckBox chkCanDoStockIn;
        private System.Windows.Forms.CheckBox chkCanDoStockOut;
        private System.Windows.Forms.TabPage tabAdmin;
        private System.Windows.Forms.CheckBox chkCanViewDashboard;
        private System.Windows.Forms.CheckBox chkCanViewSuppliers;
        private System.Windows.Forms.CheckBox chkCanManageSuppliers;
        private System.Windows.Forms.CheckBox chkCanViewUsers;
        private System.Windows.Forms.CheckBox chkCanManageUsers;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.CheckBox chkCanViewReports;
        private System.Windows.Forms.CheckBox chkCanPrint;
        private System.Windows.Forms.CheckBox chkCanViewAuditLog;
        private System.Windows.Forms.ContextMenuStrip cmsDeletePicture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJob;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegNum;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.Label lblSubTitle;
    }
}
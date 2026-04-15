namespace InventoryManagementSystem
{
    partial class FrmSupplierManagement
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnDeleteSupplier = new System.Windows.Forms.Button();
            this.btnEditSupplier = new System.Windows.Forms.Button();
            this.btnAddSupplier = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlGridCard = new System.Windows.Forms.Panel();
            this.dgvSuppliers = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActiveProducts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlGridTitle = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlFormCard = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.clbSuppliedProducts = new System.Windows.Forms.CheckedListBox();
            this.lblSuppliedProducts = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.pnlFormTitle = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).BeginInit();
            this.pnlGridTitle.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlFormCard.SuspendLayout();
            this.pnlFormTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnExport);
            this.pnlHeader.Controls.Add(this.btnDeleteSupplier);
            this.pnlHeader.Controls.Add(this.btnEditSupplier);
            this.pnlHeader.Controls.Add(this.btnAddSupplier);
            this.pnlHeader.Controls.Add(this.txtSearch);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1360, 88);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnExport.Location = new System.Drawing.Point(1023, 20);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(84, 34);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnDeleteSupplier
            // 
            this.btnDeleteSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnDeleteSupplier.FlatAppearance.BorderSize = 0;
            this.btnDeleteSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSupplier.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteSupplier.ForeColor = System.Drawing.Color.White;
            this.btnDeleteSupplier.Location = new System.Drawing.Point(931, 20);
            this.btnDeleteSupplier.Name = "btnDeleteSupplier";
            this.btnDeleteSupplier.Size = new System.Drawing.Size(84, 34);
            this.btnDeleteSupplier.TabIndex = 5;
            this.btnDeleteSupplier.Text = "Delete";
            this.btnDeleteSupplier.UseVisualStyleBackColor = false;
            // 
            // btnEditSupplier
            // 
            this.btnEditSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnEditSupplier.FlatAppearance.BorderSize = 0;
            this.btnEditSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditSupplier.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditSupplier.ForeColor = System.Drawing.Color.White;
            this.btnEditSupplier.Location = new System.Drawing.Point(839, 20);
            this.btnEditSupplier.Name = "btnEditSupplier";
            this.btnEditSupplier.Size = new System.Drawing.Size(84, 34);
            this.btnEditSupplier.TabIndex = 4;
            this.btnEditSupplier.Text = "Edit";
            this.btnEditSupplier.UseVisualStyleBackColor = false;
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAddSupplier.FlatAppearance.BorderSize = 0;
            this.btnAddSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSupplier.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddSupplier.ForeColor = System.Drawing.Color.White;
            this.btnAddSupplier.Location = new System.Drawing.Point(747, 20);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(84, 34);
            this.btnAddSupplier.TabIndex = 3;
            this.btnAddSupplier.Text = "Add New";
            this.btnAddSupplier.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.txtSearch.Location = new System.Drawing.Point(302, 26);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(415, 24);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search suppliers, contact, or email...";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(24, 52);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(182, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Manage supplier details and links";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(22, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(243, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Suppliers Management";
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.pnlGridCard);
            this.pnlBody.Controls.Add(this.pnlRight);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 88);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(20);
            this.pnlBody.Size = new System.Drawing.Size(1360, 632);
            this.pnlBody.TabIndex = 1;
            // 
            // pnlGridCard
            // 
            this.pnlGridCard.BackColor = System.Drawing.Color.White;
            this.pnlGridCard.Controls.Add(this.dgvSuppliers);
            this.pnlGridCard.Controls.Add(this.pnlGridTitle);
            this.pnlGridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridCard.Location = new System.Drawing.Point(20, 20);
            this.pnlGridCard.Name = "pnlGridCard";
            this.pnlGridCard.Padding = new System.Windows.Forms.Padding(1);
            this.pnlGridCard.Size = new System.Drawing.Size(980, 592);
            this.pnlGridCard.TabIndex = 0;
            // 
            // dgvSuppliers
            // 
            this.dgvSuppliers.AllowUserToAddRows = false;
            this.dgvSuppliers.AllowUserToDeleteRows = false;
            this.dgvSuppliers.AllowUserToResizeRows = false;
            this.dgvSuppliers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuppliers.BackgroundColor = System.Drawing.Color.White;
            this.dgvSuppliers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSuppliers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSuppliers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSuppliers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSuppliers.ColumnHeadersHeight = 50;
            this.dgvSuppliers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colCategory,
            this.colContact,
            this.colPhone,
            this.colEmail,
            this.colActiveProducts,
            this.colStatus});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSuppliers.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSuppliers.EnableHeadersVisualStyles = false;
            this.dgvSuppliers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dgvSuppliers.Location = new System.Drawing.Point(1, 49);
            this.dgvSuppliers.MultiSelect = false;
            this.dgvSuppliers.Name = "dgvSuppliers";
            this.dgvSuppliers.ReadOnly = true;
            this.dgvSuppliers.RowHeadersVisible = false;
            this.dgvSuppliers.RowHeadersWidth = 51;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.dgvSuppliers.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSuppliers.RowTemplate.Height = 46;
            this.dgvSuppliers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuppliers.Size = new System.Drawing.Size(978, 542);
            this.dgvSuppliers.TabIndex = 1;
            // 
            // colName
            // 
            this.colName.FillWeight = 150F;
            this.colName.HeaderText = "Supplier Name";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colCategory
            // 
            this.colCategory.FillWeight = 90F;
            this.colCategory.HeaderText = "Category";
            this.colCategory.MinimumWidth = 6;
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            // 
            // colContact
            // 
            this.colContact.FillWeight = 120F;
            this.colContact.HeaderText = "Contact Person";
            this.colContact.MinimumWidth = 6;
            this.colContact.Name = "colContact";
            this.colContact.ReadOnly = true;
            // 
            // colPhone
            // 
            this.colPhone.FillWeight = 95F;
            this.colPhone.HeaderText = "Phone";
            this.colPhone.MinimumWidth = 6;
            this.colPhone.Name = "colPhone";
            this.colPhone.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.FillWeight = 135F;
            this.colEmail.HeaderText = "Email";
            this.colEmail.MinimumWidth = 6;
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // colActiveProducts
            // 
            this.colActiveProducts.FillWeight = 90F;
            this.colActiveProducts.HeaderText = "Products";
            this.colActiveProducts.MinimumWidth = 6;
            this.colActiveProducts.Name = "colActiveProducts";
            this.colActiveProducts.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.FillWeight = 75F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // pnlGridTitle
            // 
            this.pnlGridTitle.Controls.Add(this.lblGridTitle);
            this.pnlGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGridTitle.Location = new System.Drawing.Point(1, 1);
            this.pnlGridTitle.Name = "pnlGridTitle";
            this.pnlGridTitle.Size = new System.Drawing.Size(978, 48);
            this.pnlGridTitle.TabIndex = 0;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblGridTitle.Location = new System.Drawing.Point(16, 14);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(183, 20);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "Supplier Directory Listing";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.pnlFormCard);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(1000, 20);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlRight.Size = new System.Drawing.Size(340, 592);
            this.pnlRight.TabIndex = 1;
            // 
            // pnlFormCard
            // 
            this.pnlFormCard.BackColor = System.Drawing.Color.White;
            this.pnlFormCard.Controls.Add(this.btnClear);
            this.pnlFormCard.Controls.Add(this.btnSave);
            this.pnlFormCard.Controls.Add(this.chkIsActive);
            this.pnlFormCard.Controls.Add(this.clbSuppliedProducts);
            this.pnlFormCard.Controls.Add(this.lblSuppliedProducts);
            this.pnlFormCard.Controls.Add(this.txtAddress);
            this.pnlFormCard.Controls.Add(this.lblAddress);
            this.pnlFormCard.Controls.Add(this.txtEmail);
            this.pnlFormCard.Controls.Add(this.lblEmail);
            this.pnlFormCard.Controls.Add(this.txtPhone);
            this.pnlFormCard.Controls.Add(this.lblPhone);
            this.pnlFormCard.Controls.Add(this.txtContactPerson);
            this.pnlFormCard.Controls.Add(this.lblContactPerson);
            this.pnlFormCard.Controls.Add(this.cmbCategory);
            this.pnlFormCard.Controls.Add(this.lblCategory);
            this.pnlFormCard.Controls.Add(this.txtSupplierName);
            this.pnlFormCard.Controls.Add(this.lblSupplierName);
            this.pnlFormCard.Controls.Add(this.pnlFormTitle);
            this.pnlFormCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormCard.Location = new System.Drawing.Point(20, 0);
            this.pnlFormCard.Name = "pnlFormCard";
            this.pnlFormCard.Padding = new System.Windows.Forms.Padding(1);
            this.pnlFormCard.Size = new System.Drawing.Size(320, 592);
            this.pnlFormCard.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnClear.Location = new System.Drawing.Point(165, 539);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(135, 34);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 539);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(135, 34);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save Supplier";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkIsActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.chkIsActive.Location = new System.Drawing.Point(20, 504);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(70, 19);
            this.chkIsActive.TabIndex = 15;
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // clbSuppliedProducts
            // 
            this.clbSuppliedProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbSuppliedProducts.CheckOnClick = true;
            this.clbSuppliedProducts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.clbSuppliedProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.clbSuppliedProducts.FormattingEnabled = true;
            this.clbSuppliedProducts.Items.AddRange(new object[] {
            "Phones",
            "Laptops",
            "Printers",
            "Accessories",
            "Networking Devices",
            "Monitors"});
            this.clbSuppliedProducts.Location = new System.Drawing.Point(20, 366);
            this.clbSuppliedProducts.Name = "clbSuppliedProducts";
            this.clbSuppliedProducts.Size = new System.Drawing.Size(280, 128);
            this.clbSuppliedProducts.TabIndex = 14;
            // 
            // lblSuppliedProducts
            // 
            this.lblSuppliedProducts.AutoSize = true;
            this.lblSuppliedProducts.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSuppliedProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblSuppliedProducts.Location = new System.Drawing.Point(18, 345);
            this.lblSuppliedProducts.Name = "lblSuppliedProducts";
            this.lblSuppliedProducts.Size = new System.Drawing.Size(104, 15);
            this.lblSuppliedProducts.TabIndex = 13;
            this.lblSuppliedProducts.Text = "Supplied Products";
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAddress.Location = new System.Drawing.Point(20, 277);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(280, 60);
            this.txtAddress.TabIndex = 12;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblAddress.Location = new System.Drawing.Point(18, 257);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(49, 15);
            this.lblAddress.TabIndex = 11;
            this.lblAddress.Text = "Address";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(20, 226);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(280, 23);
            this.txtEmail.TabIndex = 10;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblEmail.Location = new System.Drawing.Point(18, 206);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(36, 15);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "Email";
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPhone.Location = new System.Drawing.Point(20, 175);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(280, 23);
            this.txtPhone.TabIndex = 8;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblPhone.Location = new System.Drawing.Point(18, 155);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(41, 15);
            this.lblPhone.TabIndex = 7;
            this.lblPhone.Text = "Phone";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactPerson.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtContactPerson.Location = new System.Drawing.Point(20, 124);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(280, 23);
            this.txtContactPerson.TabIndex = 6;
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblContactPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblContactPerson.Location = new System.Drawing.Point(18, 104);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(87, 15);
            this.lblContactPerson.TabIndex = 5;
            this.lblContactPerson.Text = "Contact Person";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "Manufacturer",
            "Distributor"});
            this.cmbCategory.Location = new System.Drawing.Point(165, 73);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(135, 23);
            this.cmbCategory.TabIndex = 4;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCategory.Location = new System.Drawing.Point(162, 53);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(54, 15);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Category";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupplierName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSupplierName.Location = new System.Drawing.Point(20, 73);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(135, 23);
            this.txtSupplierName.TabIndex = 2;
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.AutoSize = true;
            this.lblSupplierName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSupplierName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblSupplierName.Location = new System.Drawing.Point(18, 53);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(86, 15);
            this.lblSupplierName.TabIndex = 1;
            this.lblSupplierName.Text = "Supplier Name";
            // 
            // pnlFormTitle
            // 
            this.pnlFormTitle.Controls.Add(this.lblFormTitle);
            this.pnlFormTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFormTitle.Location = new System.Drawing.Point(1, 1);
            this.pnlFormTitle.Name = "pnlFormTitle";
            this.pnlFormTitle.Size = new System.Drawing.Size(318, 40);
            this.pnlFormTitle.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblFormTitle.Location = new System.Drawing.Point(16, 11);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(106, 20);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Supplier Form";
            // 
            // FrmSupplierManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1360, 720);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSupplierManagement";
            this.Text = "Suppliers Management";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).EndInit();
            this.pnlGridTitle.ResumeLayout(false);
            this.pnlGridTitle.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlFormCard.ResumeLayout(false);
            this.pnlFormCard.PerformLayout();
            this.pnlFormTitle.ResumeLayout(false);
            this.pnlFormTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAddSupplier;
        private System.Windows.Forms.Button btnEditSupplier;
        private System.Windows.Forms.Button btnDeleteSupplier;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.Panel pnlGridTitle;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvSuppliers;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlFormCard;
        private System.Windows.Forms.Panel pnlFormTitle;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblSupplierName;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblContactPerson;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblSuppliedProducts;
        private System.Windows.Forms.CheckedListBox clbSuppliedProducts;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActiveProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}
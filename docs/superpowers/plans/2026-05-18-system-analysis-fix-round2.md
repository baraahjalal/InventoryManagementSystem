# System Analysis Fix Round 2 — Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Apply 6 targeted fixes: dashboard product name display, category-driven spec templates, StockIn/StockOut cleanup, password min length, and AuditLog UX overhaul.

**Architecture:** WinForms forms are modified in both `.cs` (logic) and `.Designer.cs` (control declarations + InitializeComponent). Fix 2 is the largest: adds a new SQL table, a new repository, and updates 3 forms. All other fixes are 1-2 file edits.

**Tech Stack:** C# .NET Framework 4.8 | WinForms | ADO.NET | SQL Server LocalDB

---

## File Map

| File | Task |
|------|------|
| `Forms/FrmUsers.cs` | Task 1 |
| `Forms/FrmStockIn.cs` + `Designer.cs` | Task 2 |
| `Forms/FrmStockOut.cs` + `Designer.cs` | Task 3 |
| `Forms/FrmDashboard.cs` | Task 4 |
| `Forms/FrmAuditLog.cs` + `Designer.cs` | Task 5 |
| `Database/CreateDatabase.sql` | Task 6 |
| `DAL/CategorySpecTemplateRepository.cs` (**new**) | Task 7 |
| `Forms/FrmAddCategory.cs` + `Designer.cs` | Task 8 |
| `Forms/FrmAddProduct.cs` + `Designer.cs` | Task 9 |
| `Forms/FrmProducts.cs` + `Designer.cs` | Task 10 |

---

## Task 1: Fix 5 — Password Minimum Length

**Files:**
- Modify: `Forms/FrmUsers.cs:262`

- [ ] **Step 1: Change the minimum from 6 to 2**

In `Forms/FrmUsers.cs`, find line 262 inside `ValidateInputs()`:
```csharp
// BEFORE:
else if (!ValidationHelper.IsValidLength(txtUserPassword.Text, 6, 50, out errorMsg))
// AFTER:
else if (!ValidationHelper.IsValidLength(txtUserPassword.Text, 2, 50, out errorMsg))
```

- [ ] **Step 2: Build and verify**

Build → Build Solution (Ctrl+Shift+B).  
Expected: 0 errors. Open Users form, try saving a user with a 2-character password — should succeed.

- [ ] **Step 3: Commit**

```bash
git add Forms/FrmUsers.cs
git commit -m "fix: lower password minimum length from 6 to 2 characters"
```

---

## Task 2: Fix 3 — Remove Purchase Order Number from StockIn

**Files:**
- Modify: `Forms/FrmStockIn.Designer.cs`
- Modify: `Forms/FrmStockIn.cs`

- [ ] **Step 1: Remove field declarations from Designer.cs**

In `Forms/FrmStockIn.Designer.cs`, remove the two field declaration lines at the bottom:
```csharp
// DELETE these two lines:
private System.Windows.Forms.Label lblOrderNumber;
private System.Windows.Forms.TextBox txtOrderNumber;
```

- [ ] **Step 2: Remove from InitializeComponent in Designer.cs**

In `InitializeComponent()`, remove:
1. `this.lblOrderNumber = new System.Windows.Forms.Label();` (instantiation line)
2. `this.txtOrderNumber = new System.Windows.Forms.TextBox();` (instantiation line)
3. The entire `pnlMainCard.Controls.Add(this.lblOrderNumber)` and `pnlMainCard.Controls.Add(this.txtOrderNumber)` lines (they appear in the `pnlMainCard.Controls.Add` section)
4. The full `// lblOrderNumber` comment block + all `this.lblOrderNumber.XXX = ...` lines
5. The full `// txtOrderNumber` comment block + all `this.txtOrderNumber.XXX = ...` lines

- [ ] **Step 3: Remove from FrmStockIn.cs — ClearForm**

In `ClearForm()`, remove:
```csharp
txtOrderNumber.Clear();
```

- [ ] **Step 4: Remove from FrmStockIn.cs — validation**

In `BtnExecute_Click`, remove this entire validation block (currently lines ~133–137):
```csharp
if (!ValidationHelper.IsRequired(txtOrderNumber.Text, out errorMsg))
{ _errorProvider.SetError(txtOrderNumber, errorMsg); isValid = false; }
else if (!ValidationHelper.IsValidLength(txtOrderNumber.Text.Trim(), 2, 50, out errorMsg))
{ _errorProvider.SetError(txtOrderNumber, errorMsg); isValid = false; }
else _errorProvider.SetError(txtOrderNumber, string.Empty);
```

- [ ] **Step 5: Update the Notes string in FrmStockIn.cs**

Still in `BtnExecute_Click`, find and replace:
```csharp
// BEFORE:
string notes = $"PO: {txtOrderNumber.Text.Trim()} | Zone: {zoneName} | Warranty: {warrantyVal} Months";
// AFTER:
string notes = $"Zone: {zoneName} | Warranty: {warrantyVal} Months";
```

- [ ] **Step 6: Build and verify**

Build → 0 errors. Open StockIn form — no Purchase Order Number field should appear. Submit a stock-in entry — it should save successfully.

- [ ] **Step 7: Commit**

```bash
git add Forms/FrmStockIn.cs Forms/FrmStockIn.Designer.cs
git commit -m "fix: remove Purchase Order Number field from StockIn form"
```

---

## Task 3: Fix 4 — Remove TransactionType + Recipient from StockOut

**Files:**
- Modify: `Forms/FrmStockOut.Designer.cs`
- Modify: `Forms/FrmStockOut.cs`

- [ ] **Step 1: Remove field declarations from Designer.cs**

At the bottom of `Forms/FrmStockOut.Designer.cs`, remove these 4 lines:
```csharp
private System.Windows.Forms.Label lblOutReason;
private System.Windows.Forms.ComboBox cmbOutReason;
private System.Windows.Forms.Label lblRecipient;
private System.Windows.Forms.TextBox txtRecipient;
```

- [ ] **Step 2: Remove from InitializeComponent in Designer.cs**

In `InitializeComponent()`:
1. Remove the 4 instantiation lines:
   ```csharp
   this.lblOutReason = new System.Windows.Forms.Label();
   this.cmbOutReason = new System.Windows.Forms.ComboBox();
   this.lblRecipient = new System.Windows.Forms.Label();
   this.txtRecipient = new System.Windows.Forms.TextBox();
   ```
2. Remove from `pnlMainCard.Controls.Add` the 4 corresponding Add calls for these controls
3. Remove the full comment blocks + property assignments for `lblOutReason`, `cmbOutReason`, `lblRecipient`, `txtRecipient`
4. Move `lblProduct` and `cmbProduct` up to fill the gap. Change:
   ```csharp
   // BEFORE:
   this.lblProduct.Location = new System.Drawing.Point(35, 112);
   this.cmbProduct.Location = new System.Drawing.Point(38, 139);
   // AFTER:
   this.lblProduct.Location = new System.Drawing.Point(35, 32);
   this.cmbProduct.Location = new System.Drawing.Point(38, 59);
   ```

- [ ] **Step 3: Remove from FrmStockOut.cs — RefreshData**

In `RefreshData()`, remove:
```csharp
if (cmbOutReason.Items.Count > 0) cmbOutReason.SelectedIndex = 0;
txtRecipient.Clear();
```

- [ ] **Step 4: Remove validation from BtnExecuteStockOut_Click**

In `BtnExecuteStockOut_Click`, remove the `cmbOutReason` validation block:
```csharp
if (cmbOutReason.SelectedItem == null)
{ _errorProvider.SetError(cmbOutReason, "Please select a transaction type."); isValid = false; }
else _errorProvider.SetError(cmbOutReason, string.Empty);
```

And remove the `txtRecipient` validation block:
```csharp
if (!ValidationHelper.IsRequired(txtRecipient.Text, out errorMsg))
{ _errorProvider.SetError(txtRecipient, errorMsg); isValid = false; }
else if (!ValidationHelper.IsValidLength(txtRecipient.Text.Trim(), 2, 100, out errorMsg))
{ _errorProvider.SetError(txtRecipient, errorMsg); isValid = false; }
else _errorProvider.SetError(txtRecipient, string.Empty);
```

- [ ] **Step 5: Update reason/notes string in BtnExecuteStockOut_Click**

Remove:
```csharp
string reason  = cmbOutReason.SelectedItem.ToString();
string notes   = $"Reason: {reason} | Recipient: {txtRecipient.Text.Trim()} | Warranty: {lblWarrantyDuration.Text}";
```
Replace with:
```csharp
string notes = $"Warranty: {lblWarrantyDuration.Text}";
```

- [ ] **Step 6: Build and verify**

Build → 0 errors. Open StockOut form — no TransactionType or Recipient fields. Perform a stock-out — it should save successfully.

- [ ] **Step 7: Commit**

```bash
git add Forms/FrmStockOut.cs Forms/FrmStockOut.Designer.cs
git commit -m "fix: remove TransactionType and Recipient fields from StockOut form"
```

---

## Task 4: Fix 1 — Dashboard Product Details Column

**Files:**
- Modify: `Forms/FrmDashboard.cs`

- [ ] **Step 1: Update LoadRecentMovements**

In `FrmDashboard.cs`, replace the entire `LoadRecentMovements()` method:

```csharp
private void LoadRecentMovements()
{
    dgvRecentActions.Rows.Clear();
    dgvRecentActions.AutoGenerateColumns = false;

    var movements = StockMovementRepository.GetAll().Take(10).ToList();
    var productDict = ProductRepository.GetAll()
        .ToDictionary(p => p.SerialNumber, p => p.ProductName);

    foreach (var m in movements)
    {
        productDict.TryGetValue(m.ProductSerial, out string pName);
        string productDetails = $"{pName ?? m.ProductSerial} [{m.ProductSerial}]";
        string operatorName   = m.Username ?? "System";
        string formattedQty   = m.QuantityChanged > 0
            ? $"+{m.QuantityChanged}"
            : m.QuantityChanged.ToString();

        dgvRecentActions.Rows.Add(
            productDetails,
            m.MovementType,
            formattedQty,
            m.MovementDate.ToString("MMM dd, hh:mm tt"),
            operatorName
        );
    }

    dgvRecentActions.ClearSelection();
}
```

- [ ] **Step 2: Build and verify**

Build → 0 errors. Open Dashboard — the Product Details column should show `"MacBook Pro 14\" M3 [APP-MBP-2023]"` format.

- [ ] **Step 3: Commit**

```bash
git add Forms/FrmDashboard.cs
git commit -m "fix: show ProductName + Serial in dashboard stock movements column"
```

---

## Task 5: Fix 6 — Audit Log UX Overhaul

**Files:**
- Modify: `Forms/FrmAuditLog.cs`
- Modify: `Forms/FrmAuditLog.Designer.cs`

- [ ] **Step 1: Rename column and update FillWeights in Designer.cs**

In `Forms/FrmAuditLog.Designer.cs`:

**a) Rename field declaration at bottom:**
```csharp
// BEFORE:
private DataGridViewTextBoxColumn colTargetEntity;
// AFTER:
private DataGridViewTextBoxColumn colDescription;
```

**b) In InitializeComponent — rename instantiation:**
```csharp
// BEFORE:
this.colTargetEntity = new System.Windows.Forms.DataGridViewTextBoxColumn();
// AFTER:
this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
```

**c) In `dgvAuditLog.Columns.AddRange(...)` call:**
```csharp
// BEFORE:
this.colTimestamp,
this.colUserIdentity,
this.colActionType,
this.colTargetEntity
// AFTER:
this.colTimestamp,
this.colUserIdentity,
this.colActionType,
this.colDescription
```

**d) Update column property blocks. Replace the four column config blocks with:**
```csharp
// colTimestamp
this.colTimestamp.FillWeight = 15F;
this.colTimestamp.HeaderText = "Timestamp";
this.colTimestamp.Name = "colTimestamp";
this.colTimestamp.ReadOnly = true;
// 
// colUserIdentity
// 
this.colUserIdentity.FillWeight = 15F;
this.colUserIdentity.HeaderText = "Username";
this.colUserIdentity.Name = "colUserIdentity";
this.colUserIdentity.ReadOnly = true;
// 
// colActionType
// 
this.colActionType.FillWeight = 20F;
this.colActionType.HeaderText = "Action";
this.colActionType.Name = "colActionType";
this.colActionType.ReadOnly = true;
// 
// colDescription
// 
this.colDescription.FillWeight = 50F;
this.colDescription.HeaderText = "Description";
this.colDescription.Name = "colDescription";
this.colDescription.ReadOnly = true;
```

**e) Wire the RowPrePaint event** — add this line alongside the CellFormatting wire-up:
```csharp
this.dgvAuditLog.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DgvAuditLog_RowPrePaint);
```

- [ ] **Step 2: Rewrite FrmAuditLog.cs**

Replace the entire content of `Forms/FrmAuditLog.cs` with:

```csharp
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;

namespace InventoryManagementSystem
{
    public partial class FrmAuditLog : Form
    {
        public FrmAuditLog()
        {
            InitializeComponent();
        }

        private void FrmAuditLog_Load(object sender, EventArgs e)
        {
            var user = DatabaseHelper.CurrentUser;
            if (user == null || !user.IsAdmin)
            {
                MessageBox.Show("Access Denied: You do not have permission to view the Audit Log.",
                    "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.BeginInvoke(new Action(() => this.Close()));
                return;
            }

            EnableDoubleBuffered(dgvAuditLog);
            LoadAuditData();
        }

        private void EnableDoubleBuffered(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgv, new object[] { true });
        }

        private void LoadAuditData()
        {
            dgvAuditLog.Rows.Clear();

            var logs = AuditLogRepository.GetAll();
            foreach (var log in logs)
            {
                int rowIndex = dgvAuditLog.Rows.Add(
                    log.LogTimestamp.ToString("MMM dd, hh:mm tt"),
                    log.Username,
                    FormatActionType(log.ActionType),
                    log.Description
                );
                dgvAuditLog.Rows[rowIndex].Tag = log.ActionType;
            }

            dgvAuditLog.ClearSelection();
        }

        private static string FormatActionType(string raw)
        {
            switch (raw)
            {
                case "STOCK STOCKIN":           return "Stock In";
                case "STOCK STOCKOUT":          return "Stock Out";
                case "STOCK RESTOCK":           return "Restock";
                case "STOCK RETURNTOSUPPLIER":  return "Return to Supplier";
                case "PRODUCT ADDED":           return "Product Added";
                case "PRODUCT DELETED":         return "Product Deleted";
                case "USER ADDED":              return "User Added";
                case "USER DELETED":            return "User Deleted";
                case "SUPPLIER ADDED":          return "Supplier Added";
                case "SUPPLIER DELETED":        return "Supplier Deleted";
                default: return raw;
            }
        }

        private void DgvAuditLog_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dgvAuditLog.Rows[e.RowIndex];
            string raw = row.Tag?.ToString() ?? "";
            Color bg;
            switch (raw)
            {
                case "STOCK STOCKIN":
                case "STOCK RESTOCK":
                    bg = Color.FromArgb(220, 252, 231); break;
                case "STOCK STOCKOUT":
                case "STOCK RETURNTOSUPPLIER":
                    bg = Color.FromArgb(254, 226, 226); break;
                case "USER ADDED":
                case "USER DELETED":
                    bg = Color.FromArgb(219, 234, 254); break;
                case "SUPPLIER ADDED":
                case "SUPPLIER DELETED":
                    bg = Color.FromArgb(254, 249, 195); break;
                default:
                    bg = Color.FromArgb(243, 244, 246); break;
            }
            row.DefaultCellStyle.BackColor          = bg;
            row.DefaultCellStyle.SelectionBackColor = bg;
        }

        private void DgvAuditLog_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null
                && dgvAuditLog.Columns[e.ColumnIndex].Name == "colActionType")
            {
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            }
        }
    }
}
```

- [ ] **Step 3: Build and verify**

Build → 0 errors. Open Audit Log — verify:
- Columns: Timestamp | Username | Action | Description (widest)
- Action shows "Stock In", "Product Added", etc. (not raw values)
- Stock In rows are light green; Stock Out rows are light salmon; User rows are light blue; Supplier rows are light yellow

- [ ] **Step 4: Commit**

```bash
git add Forms/FrmAuditLog.cs Forms/FrmAuditLog.Designer.cs
git commit -m "fix: audit log UX — column rename, action labels, row color coding"
```

---

## Task 6: Fix 2a — Add CategorySpecTemplates Table to SQL

**Files:**
- Modify: `Database/CreateDatabase.sql`

- [ ] **Step 1: Add table to CreateDatabase.sql**

In `Database/CreateDatabase.sql`, find the `vw_ProductStock` view block. Insert the new table **before** the view:

```sql
-- ============================================================
-- TABLE 10: CategorySpecTemplates
-- PK : (CategoryName, SpecKey) — composite natural key
-- FK : CategoryName → Categories  (CASCADE on delete)
-- PURPOSE : Defines which specification keys are expected for
--           each category. Used to pre-fill FrmAddProduct.
-- ============================================================
CREATE TABLE CategorySpecTemplates (
    CategoryName NVARCHAR(100) NOT NULL,
    SpecKey      NVARCHAR(50)  NOT NULL,
    CONSTRAINT PK_CategorySpecTemplates PRIMARY KEY (CategoryName, SpecKey),
    CONSTRAINT FK_CST_Category
        FOREIGN KEY (CategoryName) REFERENCES Categories (CategoryName)
        ON DELETE CASCADE
);
GO
```

- [ ] **Step 2: Run the ALTER script on the live database**

Open SSMS or Visual Studio Server Explorer and run this one-time script:

```sql
USE InventoryDB;
GO
IF NOT EXISTS (
    SELECT 1 FROM sys.tables WHERE name = 'CategorySpecTemplates'
)
BEGIN
    CREATE TABLE CategorySpecTemplates (
        CategoryName NVARCHAR(100) NOT NULL,
        SpecKey      NVARCHAR(50)  NOT NULL,
        CONSTRAINT PK_CategorySpecTemplates PRIMARY KEY (CategoryName, SpecKey),
        CONSTRAINT FK_CST_Category
            FOREIGN KEY (CategoryName) REFERENCES Categories (CategoryName)
            ON DELETE CASCADE
    );
    PRINT 'CategorySpecTemplates table created.';
END
ELSE
    PRINT 'Table already exists — skipped.';
GO
```

- [ ] **Step 3: Verify table exists**

```sql
USE InventoryDB;
SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'CategorySpecTemplates';
```

Expected output: 2 rows — `CategoryName` (nvarchar) and `SpecKey` (nvarchar).

- [ ] **Step 4: Commit**

```bash
git add Database/CreateDatabase.sql
git commit -m "feat: add CategorySpecTemplates table to schema"
```

---

## Task 7: Fix 2b — CategorySpecTemplateRepository

**Files:**
- Create: `DAL/CategorySpecTemplateRepository.cs`

- [ ] **Step 1: Create the repository file**

Create `DAL/CategorySpecTemplateRepository.cs` with this content:

```csharp
using System.Collections.Generic;
using System.Data.SqlClient;

namespace InventoryManagementSystem.DAL
{
    public static class CategorySpecTemplateRepository
    {
        public static List<string> GetByCategory(string categoryName)
        {
            var list = new List<string>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT SpecKey FROM CategorySpecTemplates " +
                    "WHERE CategoryName = @cn ORDER BY SpecKey", conn))
                {
                    cmd.Parameters.AddWithValue("@cn", categoryName);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(r.GetString(0));
                }
            }
            return list;
        }

        public static void Add(string categoryName, string specKey)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "INSERT INTO CategorySpecTemplates (CategoryName, SpecKey) " +
                    "VALUES (@cn, @sk)", conn))
                {
                    cmd.Parameters.AddWithValue("@cn", categoryName);
                    cmd.Parameters.AddWithValue("@sk", specKey);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteByCategory(string categoryName)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "DELETE FROM CategorySpecTemplates WHERE CategoryName = @cn", conn))
                {
                    cmd.Parameters.AddWithValue("@cn", categoryName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
```

- [ ] **Step 2: Build and verify**

Build → 0 errors. No runtime test needed yet — the repository will be tested through the UI in Task 8.

- [ ] **Step 3: Commit**

```bash
git add DAL/CategorySpecTemplateRepository.cs
git commit -m "feat: add CategorySpecTemplateRepository with GetByCategory/Add/DeleteByCategory"
```

---

## Task 8: Fix 2c — FrmAddCategory Spec Keys UI

**Files:**
- Modify: `Forms/FrmAddCategory.Designer.cs`
- Modify: `Forms/FrmAddCategory.cs`

- [ ] **Step 1: Rewrite FrmAddCategory.Designer.cs**

Replace the entire contents of `Forms/FrmAddCategory.Designer.cs` with:

```csharp
namespace InventoryManagementSystem.Forms
{
    partial class FrmAddCategory
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle cellStyle   = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader       = new System.Windows.Forms.Panel();
            this.lblTitle        = new System.Windows.Forms.Label();
            this.pnlFooter       = new System.Windows.Forms.Panel();
            this.btnCancel       = new System.Windows.Forms.Button();
            this.btnSave         = new System.Windows.Forms.Button();
            this.pnlBody         = new System.Windows.Forms.Panel();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblSpecsSection = new System.Windows.Forms.Label();
            this.lblSpecNote     = new System.Windows.Forms.Label();
            this.txtSpecKey      = new System.Windows.Forms.TextBox();
            this.btnAddSpecKey   = new System.Windows.Forms.Button();
            this.dgvSpecKeys     = new System.Windows.Forms.DataGridView();
            this.colSpecKeyName  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpecKeyRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecKeys)).BeginInit();
            this.SuspendLayout();
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(500, 60);
            this.pnlHeader.TabIndex = 0;
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New Category";
            // pnlFooter
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 520);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(500, 70);
            this.pnlFooter.TabIndex = 1;
            // btnCancel
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(265, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 38);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // btnSave
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(375, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 38);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // pnlBody
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.lblCategoryName);
            this.pnlBody.Controls.Add(this.txtCategoryName);
            this.pnlBody.Controls.Add(this.lblSpecsSection);
            this.pnlBody.Controls.Add(this.lblSpecNote);
            this.pnlBody.Controls.Add(this.txtSpecKey);
            this.pnlBody.Controls.Add(this.btnAddSpecKey);
            this.pnlBody.Controls.Add(this.dgvSpecKeys);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 60);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(25);
            this.pnlBody.Size = new System.Drawing.Size(500, 460);
            this.pnlBody.TabIndex = 2;
            // lblCategoryName
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCategoryName.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblCategoryName.Location = new System.Drawing.Point(21, 25);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.TabIndex = 0;
            this.lblCategoryName.Text = "Category Name:";
            // txtCategoryName
            this.txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCategoryName.Location = new System.Drawing.Point(25, 47);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(450, 27);
            this.txtCategoryName.TabIndex = 1;
            // lblSpecsSection
            this.lblSpecsSection.AutoSize = true;
            this.lblSpecsSection.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblSpecsSection.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblSpecsSection.Location = new System.Drawing.Point(21, 92);
            this.lblSpecsSection.Name = "lblSpecsSection";
            this.lblSpecsSection.TabIndex = 2;
            this.lblSpecsSection.Text = "Specification Keys (Optional)";
            // lblSpecNote
            this.lblSpecNote.AutoSize = true;
            this.lblSpecNote.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSpecNote.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.lblSpecNote.Location = new System.Drawing.Point(23, 115);
            this.lblSpecNote.Name = "lblSpecNote";
            this.lblSpecNote.TabIndex = 3;
            this.lblSpecNote.Text = "e.g. RAM, CPU, Storage, Color";
            // txtSpecKey
            this.txtSpecKey.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSpecKey.Location = new System.Drawing.Point(25, 138);
            this.txtSpecKey.Name = "txtSpecKey";
            this.txtSpecKey.Size = new System.Drawing.Size(340, 27);
            this.txtSpecKey.TabIndex = 4;
            // btnAddSpecKey
            this.btnAddSpecKey.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnAddSpecKey.FlatAppearance.BorderSize = 0;
            this.btnAddSpecKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSpecKey.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAddSpecKey.ForeColor = System.Drawing.Color.White;
            this.btnAddSpecKey.Location = new System.Drawing.Point(372, 135);
            this.btnAddSpecKey.Name = "btnAddSpecKey";
            this.btnAddSpecKey.Size = new System.Drawing.Size(103, 31);
            this.btnAddSpecKey.TabIndex = 5;
            this.btnAddSpecKey.Text = "Add Key";
            this.btnAddSpecKey.UseVisualStyleBackColor = false;
            this.btnAddSpecKey.Click += new System.EventHandler(this.btnAddSpecKey_Click);
            // dgvSpecKeys
            this.dgvSpecKeys.AllowUserToAddRows = false;
            this.dgvSpecKeys.AllowUserToDeleteRows = false;
            this.dgvSpecKeys.AllowUserToResizeRows = false;
            this.dgvSpecKeys.BackgroundColor = System.Drawing.Color.White;
            this.dgvSpecKeys.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSpecKeys.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSpecKeys.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            headerStyle.SelectionForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.dgvSpecKeys.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvSpecKeys.ColumnHeadersHeight = 35;
            this.dgvSpecKeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSpecKeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colSpecKeyName,
                this.colSpecKeyRemove });
            cellStyle.BackColor = System.Drawing.Color.White;
            cellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            cellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            cellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.dgvSpecKeys.DefaultCellStyle = cellStyle;
            this.dgvSpecKeys.EnableHeadersVisualStyles = false;
            this.dgvSpecKeys.Location = new System.Drawing.Point(25, 178);
            this.dgvSpecKeys.MultiSelect = false;
            this.dgvSpecKeys.Name = "dgvSpecKeys";
            this.dgvSpecKeys.RowHeadersVisible = false;
            this.dgvSpecKeys.RowTemplate.Height = 35;
            this.dgvSpecKeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpecKeys.Size = new System.Drawing.Size(450, 255);
            this.dgvSpecKeys.TabIndex = 6;
            this.dgvSpecKeys.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpecKeys_CellContentClick);
            // colSpecKeyName
            this.colSpecKeyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSpecKeyName.HeaderText = "Specification Key";
            this.colSpecKeyName.Name = "colSpecKeyName";
            this.colSpecKeyName.ReadOnly = true;
            // colSpecKeyRemove
            this.colSpecKeyRemove.HeaderText = "";
            this.colSpecKeyRemove.Name = "colSpecKeyRemove";
            this.colSpecKeyRemove.ReadOnly = true;
            this.colSpecKeyRemove.Text = "Remove";
            this.colSpecKeyRemove.UseColumnTextForButtonValue = true;
            this.colSpecKeyRemove.Width = 80;
            // FrmAddCategory
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 590);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Category";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecKeys)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.Label lblSpecsSection;
        private System.Windows.Forms.Label lblSpecNote;
        private System.Windows.Forms.TextBox txtSpecKey;
        private System.Windows.Forms.Button btnAddSpecKey;
        private System.Windows.Forms.DataGridView dgvSpecKeys;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpecKeyName;
        private System.Windows.Forms.DataGridViewButtonColumn colSpecKeyRemove;
    }
}
```

- [ ] **Step 2: Rewrite FrmAddCategory.cs**

Replace the entire contents of `Forms/FrmAddCategory.cs` with:

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Forms
{
    public partial class FrmAddCategory : Form
    {
        public string CreatedCategoryName { get; private set; }

        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FrmAddCategory()
        {
            InitializeComponent();
        }

        private void btnAddSpecKey_Click(object sender, EventArgs e)
        {
            string key = txtSpecKey.Text.Trim();
            if (string.IsNullOrWhiteSpace(key))
            {
                _errorProvider.SetError(txtSpecKey, "Spec key cannot be empty.");
                return;
            }
            _errorProvider.SetError(txtSpecKey, string.Empty);

            foreach (DataGridViewRow row in dgvSpecKeys.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["colSpecKeyName"].Value?.ToString()
                        .Equals(key, StringComparison.OrdinalIgnoreCase) == true)
                {
                    MessageBox.Show("This spec key is already added.", "Duplicate",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            dgvSpecKeys.Rows.Add(key);
            txtSpecKey.Clear();
            txtSpecKey.Focus();
        }

        private void dgvSpecKeys_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSpecKeys.Columns[e.ColumnIndex].Name == "colSpecKeyRemove")
                dgvSpecKeys.Rows.RemoveAt(e.RowIndex);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            string catName = txtCategoryName.Text.Trim();
            bool isValid = true;
            string errorMsg;

            if (!ValidationHelper.IsRequired(catName, out errorMsg))
            { _errorProvider.SetError(txtCategoryName, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(catName, 2, 100, out errorMsg))
            { _errorProvider.SetError(txtCategoryName, errorMsg); isValid = false; }
            else if (CategoryRepository.Exists(catName))
            { _errorProvider.SetError(txtCategoryName, "A category with this name already exists."); isValid = false; }
            else
              _errorProvider.SetError(txtCategoryName, string.Empty);

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted errors before saving.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CategoryRepository.Add(catName);

            StorageZoneRepository.Add(new StorageZone
            {
                ZoneName     = $"Auto Zone: {catName}",
                CategoryName = catName
            });

            foreach (DataGridViewRow row in dgvSpecKeys.Rows)
            {
                if (row.IsNewRow) continue;
                string key = row.Cells["colSpecKeyName"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(key))
                    CategorySpecTemplateRepository.Add(catName, key);
            }

            CreatedCategoryName = catName;
            MessageBox.Show("Category created successfully.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
```

- [ ] **Step 3: Build and verify**

Build → 0 errors. Open "Add Category" — verify the spec key UI appears (input + Add Key button + grid). Add "RAM" and "CPU", save. Check DB:
```sql
SELECT * FROM CategorySpecTemplates WHERE CategoryName = 'YourNewCategory';
```
Expected: 2 rows.

- [ ] **Step 4: Commit**

```bash
git add Forms/FrmAddCategory.cs Forms/FrmAddCategory.Designer.cs
git commit -m "feat: replace filter UI in AddCategory with spec key template UI"
```

---

## Task 9: Fix 2d — FrmAddProduct Category-Driven Spec DGV

**Files:**
- Modify: `Forms/FrmAddProduct.Designer.cs`
- Modify: `Forms/FrmAddProduct.cs`

- [ ] **Step 1: Replace flpDynamicSpecs with dgvProductSpecs in Designer.cs**

In `Forms/FrmAddProduct.Designer.cs`:

**a) In field declarations at bottom**, replace:
```csharp
private System.Windows.Forms.FlowLayoutPanel flpDynamicSpecs;
```
with:
```csharp
private System.Windows.Forms.DataGridView dgvProductSpecs;
private System.Windows.Forms.DataGridViewTextBoxColumn colSpecKey;
private System.Windows.Forms.DataGridViewTextBoxColumn colSpecValue;
```

**b) In InitializeComponent instantiation block**, replace:
```csharp
this.flpDynamicSpecs = new System.Windows.Forms.FlowLayoutPanel();
```
with:
```csharp
this.dgvProductSpecs = new System.Windows.Forms.DataGridView();
this.colSpecKey      = new System.Windows.Forms.DataGridViewTextBoxColumn();
this.colSpecValue    = new System.Windows.Forms.DataGridViewTextBoxColumn();
```

**c) Add `BeginInit`/`EndInit` for the new DGV** — add immediately after the FlowLayoutPanel SuspendLayout removal:
```csharp
((System.ComponentModel.ISupportInitialize)(this.dgvProductSpecs)).BeginInit();
```
And add before `this.ResumeLayout(false)` at the end:
```csharp
((System.ComponentModel.ISupportInitialize)(this.dgvProductSpecs)).EndInit();
```

**d) In `tblForm.Controls.Add` section**, replace:
```csharp
this.tblForm.Controls.Add(this.flpDynamicSpecs, 1, 6);
```
with:
```csharp
this.tblForm.Controls.Add(this.dgvProductSpecs, 1, 6);
```

**e) Replace the entire `// flpDynamicSpecs` property block** with:
```csharp
// dgvProductSpecs
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
// colSpecKey
this.colSpecKey.HeaderText = "Property";
this.colSpecKey.Name       = "colSpecKey";
this.colSpecKey.ReadOnly   = true;
this.colSpecKey.Width      = 120;
// colSpecValue
this.colSpecValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
this.colSpecValue.HeaderText   = "Value";
this.colSpecValue.Name         = "colSpecValue";
this.colSpecValue.ReadOnly     = false;
```

Also remove `this.tblForm.SuspendLayout()` / `ResumeLayout` for FlowLayoutPanel (there was none for flp — the `tblForm.SuspendLayout()` already covers it, just ensure the BeginInit/EndInit for dgvProductSpecs is added as noted in step c).

- [ ] **Step 2: Update FrmAddProduct.cs**

**a) Remove `_specSelectors` field declaration** (line 13):
```csharp
// DELETE:
private readonly Dictionary<string, ComboBox> _specSelectors = new Dictionary<string, ComboBox>();
```

**b) Remove `using System.Collections.Generic`** only if no other usage — check first. `List<string>` and `List<ProductItem>` etc. are still used, so **keep** the using.

**c) Replace `CmbCategory_SelectedIndexChanged`**:
```csharp
private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
{
    dgvProductSpecs.Rows.Clear();
    if (cmbCategory.SelectedItem is Models.Category cat)
    {
        var keys = CategorySpecTemplateRepository.GetByCategory(cat.CategoryName);
        foreach (var key in keys)
            dgvProductSpecs.Rows.Add(key, "");
    }
    LoadAllActiveSuppliers();
}
```

**d) Add using** at top of file if not present:
```csharp
using InventoryManagementSystem.DAL;
```
(Already present — no change needed.)

**e) Replace the `_specSelectors` loop in `BtnSave_Click`** (the foreach after `specs` is declared):
```csharp
// REPLACE:
var specs = new List<ProductSpecification>();
foreach (var kvp in _specSelectors)
{
    string val = kvp.Value.SelectedItem?.ToString();
    if (!string.IsNullOrWhiteSpace(val))
        specs.Add(new ProductSpecification { ProductSerial = serial, SpecKey = kvp.Key, SpecValue = val });
}
// WITH:
var specs = new List<ProductSpecification>();
foreach (DataGridViewRow row in dgvProductSpecs.Rows)
{
    if (row.IsNewRow) continue;
    string key = row.Cells["colSpecKey"].Value?.ToString();
    string val = row.Cells["colSpecValue"].Value?.ToString();
    if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(val))
        specs.Add(new ProductSpecification { ProductSerial = serial, SpecKey = key, SpecValue = val });
}
```

- [ ] **Step 3: Build and verify**

Build → 0 errors. Open Add Product, select a category that has spec keys (e.g. one created in Task 8) — the DGV should show Property rows with editable Value cells. Save a product — check DB:
```sql
SELECT * FROM ProductSpecifications WHERE ProductSerial = 'YOUR-SERIAL';
```

- [ ] **Step 4: Commit**

```bash
git add Forms/FrmAddProduct.cs Forms/FrmAddProduct.Designer.cs
git commit -m "feat: replace flpDynamicSpecs with category-driven spec DGV in AddProduct"
```

---

## Task 10: Fix 2e — FrmProducts Add Specs DGV Above Item Tracking

**Files:**
- Modify: `Forms/FrmProducts.Designer.cs`
- Modify: `Forms/FrmProducts.cs`

- [ ] **Step 1: Add dgvSpecs to pnlDetails in Designer.cs**

In `Forms/FrmProducts.Designer.cs`:

**a) Add field declarations** at the bottom of the class (alongside existing declarations):
```csharp
private System.Windows.Forms.DataGridView dgvSpecs;
private System.Windows.Forms.DataGridViewTextBoxColumn colSpecProperty;
private System.Windows.Forms.DataGridViewTextBoxColumn colSpecValue;
private System.Windows.Forms.Label lblItemTracking;
```

**b) In InitializeComponent instantiation block**, add:
```csharp
this.dgvSpecs          = new System.Windows.Forms.DataGridView();
this.colSpecProperty   = new System.Windows.Forms.DataGridViewTextBoxColumn();
this.colSpecValue      = new System.Windows.Forms.DataGridViewTextBoxColumn();
this.lblItemTracking   = new System.Windows.Forms.Label();
```
Add `BeginInit` for dgvSpecs right after `((ISupportInitialize)(this.dgvProducts)).BeginInit();`:
```csharp
((System.ComponentModel.ISupportInitialize)(this.dgvSpecs)).BeginInit();
```
Add `EndInit` alongside `dgvProducts`:
```csharp
((System.ComponentModel.ISupportInitialize)(this.dgvSpecs)).EndInit();
```

**c) Add the new controls to `pnlDetails.Controls.Add` section**. Add these lines alongside the existing Controls.Add calls:
```csharp
this.pnlDetails.Controls.Add(this.lblItemTracking);
this.pnlDetails.Controls.Add(this.dgvSpecs);
```

**d) Add property blocks for new controls.** Add after the `// lblProdSpec` block:
```csharp
// dgvSpecs
this.dgvSpecs.AllowUserToAddRows    = false;
this.dgvSpecs.AllowUserToDeleteRows = false;
this.dgvSpecs.AllowUserToResizeRows = false;
this.dgvSpecs.BackgroundColor       = System.Drawing.Color.White;
this.dgvSpecs.BorderStyle           = System.Windows.Forms.BorderStyle.None;
this.dgvSpecs.CellBorderStyle       = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
this.dgvSpecs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
this.dgvSpecs.ColumnHeadersHeight   = 26;
this.dgvSpecs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
this.dgvSpecs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
    this.colSpecProperty,
    this.colSpecValue });
this.dgvSpecs.EnableHeadersVisualStyles = false;
this.dgvSpecs.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
this.dgvSpecs.Location  = new System.Drawing.Point(23, 228);
this.dgvSpecs.Name      = "dgvSpecs";
this.dgvSpecs.ReadOnly  = true;
this.dgvSpecs.RowHeadersVisible = false;
this.dgvSpecs.RowTemplate.Height = 26;
this.dgvSpecs.Size      = new System.Drawing.Size(274, 88);
this.dgvSpecs.TabIndex  = 8;
// colSpecProperty
this.colSpecProperty.HeaderText = "Property";
this.colSpecProperty.Name       = "colSpecProperty";
this.colSpecProperty.ReadOnly   = true;
this.colSpecProperty.Width      = 100;
// colSpecValue
this.colSpecValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
this.colSpecValue.HeaderText   = "Value";
this.colSpecValue.Name         = "colSpecValue";
this.colSpecValue.ReadOnly     = true;
// lblItemTracking
this.lblItemTracking.AutoSize  = true;
this.lblItemTracking.Font      = new System.Drawing.Font("Segoe UI", 9F);
this.lblItemTracking.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
this.lblItemTracking.Location  = new System.Drawing.Point(20, 323);
this.lblItemTracking.Name      = "lblItemTracking";
this.lblItemTracking.TabIndex  = 9;
this.lblItemTracking.Text      = "Item Tracking";
```

**e) Reposition existing controls** to make room. Find and update these location lines:
```csharp
// txtProdSpec: move down from (23, 230) to (23, 340)
this.txtProdSpec.Location = new System.Drawing.Point(23, 340);
this.txtProdSpec.Size     = new System.Drawing.Size(274, 95);

// btnEdit: move down from (23, 394) to (23, 447)
this.btnEdit.Location = new System.Drawing.Point(23, 447);

// btnAddProduct: move from (23, 559) to (23, 498)
this.btnAddProduct.Location = new System.Drawing.Point(23, 498);
```

- [ ] **Step 2: Update FrmProducts.cs — ShowProductDetails**

In `ShowProductDetails()`, add the DGV population **before** the `txtProdSpec` block:

```csharp
private void ShowProductDetails()
{
    if (_selectedProduct == null) return;

    txtProdName.Text  = _selectedProduct.ProductName;
    txtProdPrice.Text = _selectedProduct.Price.ToString("0.00");

    var specs = ProductRepository.GetSpecifications(_selectedProduct.SerialNumber);
    var items = ProductItemRepository.GetAvailable(_selectedProduct.SerialNumber);

    // Populate specs DGV
    dgvSpecs.Rows.Clear();
    foreach (var s in specs)
        dgvSpecs.Rows.Add(s.SpecKey, s.SpecValue);

    // Populate item tracking text (unchanged)
    var sb = new StringBuilder();
    sb.AppendLine($"Category: {_selectedProduct.CategoryName}");
    sb.AppendLine($"Product Serial: {_selectedProduct.SerialNumber}");
    sb.AppendLine($"In Stock: {items.Count}");

    if (items.Count > 0)
    {
        sb.AppendLine();
        sb.AppendLine("Available Items:");
        foreach (var item in items.Take(15).OrderBy(i => i.ItemSerialNumber))
            sb.AppendLine($"  ► {item.ItemSerialNumber}");
        if (items.Count > 15)
            sb.AppendLine($"  ... and {items.Count - 15} more");
    }

    txtProdSpec.Text = sb.ToString();
    btnEdit.Enabled  = true;
}
```

- [ ] **Step 3: Update ClearDetails to clear the DGV**

```csharp
private void ClearDetails()
{
    _selectedProduct  = null;
    txtProdName.Text  = string.Empty;
    txtProdPrice.Text = string.Empty;
    txtProdSpec.Text  = string.Empty;
    dgvSpecs.Rows.Clear();
    btnEdit.Enabled   = false;
}
```

- [ ] **Step 4: Build and verify**

Build → 0 errors. Open Products, select a product that has specifications — the spec DGV should show Property/Value pairs above the item tracking TextBox.

- [ ] **Step 5: Commit**

```bash
git add Forms/FrmProducts.cs Forms/FrmProducts.Designer.cs
git commit -m "feat: add read-only spec DGV to product details panel in FrmProducts"
```

---

## Final Verification

- [ ] Run the full application and test each fix end-to-end:
  1. Dashboard → Recent movements show `"ProductName [SERIAL]"` format
  2. Add Category → Spec keys saved to DB, no filter UI
  3. Add Product → Selecting category populates spec DGV rows; saving stores specs
  4. Products → Selecting a product shows specs in DGV + item tracking in TextBox
  5. StockIn → No Purchase Order Number field
  6. StockOut → No TransactionType or Recipient fields; product selection moved to top
  7. Users → Password of 2 characters accepted
  8. Audit Log → Clean action labels, row color coding, Description column is widest

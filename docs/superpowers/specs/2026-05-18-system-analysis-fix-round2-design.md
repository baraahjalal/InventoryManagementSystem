# Design Spec — System Analysis & Fix Round 2
**Date:** 2026-05-18  
**Branch:** feature/sql-server-final  
**Project:** Inventory Management System — Advanced C#

---

## Overview

6 targeted fixes across the WinForms UI and DAL layer.  
No schema destructive changes — only additive SQL (new table + ALTER).  
No changes to Triggers, Models (except where noted), or existing Repository contracts.

---

## Fix 1 — Dashboard: Stock Movements Product Details Column

**File:** `Forms/FrmDashboard.cs`

**Problem:** `LoadRecentMovements()` shows only `[ProductSerial]` in the Product Details column.

**Solution:**
- At the top of `LoadRecentMovements()`, call `ProductRepository.GetAll()` once and build a `Dictionary<string, string>` keyed by `SerialNumber` → `ProductName`.
- Replace `string productDetails = $"[{m.ProductSerial}]"` with:
  ```
  productDict.TryGetValue(m.ProductSerial, out string pName);
  string productDetails = $"{pName ?? m.ProductSerial} [{m.ProductSerial}]";
  ```
- No DAL changes required.

**Result:** `MacBook Pro 14" M3 [APP-MBP-2023]`

---

## Fix 2 — Product Specifications — Full Logic Overhaul

### 2.1 Database — New Table

**File:** `Database/CreateDatabase.sql` (add at end, before `vw_ProductStock`)

```sql
CREATE TABLE CategorySpecTemplates (
    CategoryName NVARCHAR(100) NOT NULL,
    SpecKey      NVARCHAR(50)  NOT NULL,
    CONSTRAINT PK_CategorySpecTemplates PRIMARY KEY (CategoryName, SpecKey),
    CONSTRAINT FK_CST_Category
        FOREIGN KEY (CategoryName) REFERENCES Categories (CategoryName)
        ON DELETE CASCADE
);
```

**Also needed:** A standalone `ALTER` script for the live database (DB already exists on dev machine). This is a separate SQL snippet to run once:
```sql
USE InventoryDB;
GO
CREATE TABLE CategorySpecTemplates (
    CategoryName NVARCHAR(100) NOT NULL,
    SpecKey      NVARCHAR(50)  NOT NULL,
    CONSTRAINT PK_CategorySpecTemplates PRIMARY KEY (CategoryName, SpecKey),
    CONSTRAINT FK_CST_Category
        FOREIGN KEY (CategoryName) REFERENCES Categories (CategoryName)
        ON DELETE CASCADE
);
```

### 2.2 DAL — New Repository

**File:** `DAL/CategorySpecTemplateRepository.cs` (new)

```
Methods:
  GetByCategory(string categoryName) → List<string>   (returns SpecKey list)
  Add(string categoryName, string specKey)             (INSERT one row)
  DeleteByCategory(string categoryName)                (DELETE WHERE CategoryName = ...)
```

Uses parameterized `SqlCommand` via `DatabaseHelper.GetConnection()`, consistent with existing repositories.

### 2.3 FrmAddCategory — Replace Filter UI with SpecKey UI

**Files:** `Forms/FrmAddCategory.cs`, `Forms/FrmAddCategory.Designer.cs`

**Remove:** `txtFilterName`, `txtFilterValues`, `btnAddFilter`, `dgvFilters` (two-column filter grid), `lblFilterName`, `lblFilterValues` and all their event handlers.

**Add:** 
- `lblSpecKeys` label: "Specification Keys (e.g. RAM, CPU, Storage)"
- `txtSpecKey` TextBox: for entering a new spec key
- `btnAddSpecKey` Button: adds the text to `dgvSpecKeys`
- `dgvSpecKeys` DataGridView: single column "SpecKey" + a delete button column "Remove"

**Save logic (`btnSave_Click`):**
1. `CategoryRepository.Add(catName)` — unchanged
2. `StorageZoneRepository.Add(...)` — unchanged
3. Loop `dgvSpecKeys.Rows` → `CategorySpecTemplateRepository.Add(catName, specKey)` for each row
4. Remove the old `CreatedFilters` dictionary population (no longer needed)

**`CreatedFilters` property:** Remove or leave as empty — callers (`FrmProducts.btnAddCategory_Click`) only used `CreatedCategoryName`, not `CreatedFilters`.

### 2.4 FrmAddProduct — Category-Driven Spec Entry

**Files:** `Forms/FrmAddProduct.cs`, `Forms/FrmAddProduct.Designer.cs`

**Remove:** `flpDynamicSpecs` FlowLayoutPanel and `_specSelectors` Dictionary (both currently unused/empty).

**Add:** `dgvProductSpecs` DataGridView with two columns:
- `colSpecKey` (header: "Property") — `ReadOnly = true`, filled from template
- `colSpecValue` (header: "Value") — editable by user

**`CmbCategory_SelectedIndexChanged` logic:**
```
dgvProductSpecs.Rows.Clear();
var keys = CategorySpecTemplateRepository.GetByCategory(selectedCategoryName);
foreach (var key in keys)
    dgvProductSpecs.Rows.Add(key, "");  // SpecKey pre-filled, SpecValue empty
```

**Save logic (`BtnSave_Click`):** Replace `_specSelectors` loop with:
```
foreach (DataGridViewRow row in dgvProductSpecs.Rows)
{
    if (row.IsNewRow) continue;
    string key = row.Cells["colSpecKey"].Value?.ToString();
    string val = row.Cells["colSpecValue"].Value?.ToString();
    if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(val))
        specs.Add(new ProductSpecification { ProductSerial = serial, SpecKey = key, SpecValue = val });
}
```

### 2.5 FrmProducts — Spec Display as DGV

**Files:** `Forms/FrmProducts.cs`, `Forms/FrmProducts.Designer.cs`

**Add:** `dgvSpecs` DataGridView positioned above `txtProdSpec`:
- Two columns: "Property" | "Value"
- `ReadOnly = true`, `AllowUserToAddRows = false`
- No row headers, single horizontal border style
- Sized to fit the panel without overlapping txtProdSpec

**`ShowProductDetails()` change:**
- Keep all `txtProdSpec` content exactly as-is (item tracking)
- Add before txtProdSpec population:
  ```
  dgvSpecs.Rows.Clear();
  foreach (var s in specs)
      dgvSpecs.Rows.Add(s.SpecKey, s.SpecValue);
  ```

**`ClearDetails()`:** Add `dgvSpecs.Rows.Clear()`

---

## Fix 3 — StockIn: Remove Purchase Order Number

**Files:** `Forms/FrmStockIn.cs`, `Forms/FrmStockIn.Designer.cs`

**Remove from Designer:**
- `lblOrderNumber` Label
- `txtOrderNumber` TextBox (declared in field list + InitializeComponent)

**Remove from `.cs`:**
- Field declaration `private System.Windows.Forms.TextBox txtOrderNumber`
- Validation block (lines 133–137) that checks `txtOrderNumber`
- `txtOrderNumber.Clear()` in `ClearForm()`

**Update Notes string:**
```csharp
// Before:
string notes = $"PO: {txtOrderNumber.Text.Trim()} | Zone: {zoneName} | Warranty: {warrantyVal} Months";
// After:
string notes = $"Zone: {zoneName} | Warranty: {warrantyVal} Months";
```

---

## Fix 4 — StockOut: Remove TransactionType + Recipient

**Files:** `Forms/FrmStockOut.cs`, `Forms/FrmStockOut.Designer.cs`

**Remove from Designer:**
- `cmbOutReason` ComboBox + `lblOutReason` Label
- `txtRecipient` TextBox + `lblRecipient` Label

**Remove from `.cs`:**
- Field declarations for the above 4 controls
- `if (cmbOutReason.SelectedItem == null)` validation block
- `txtRecipient` validation block (IsRequired + IsValidLength check)
- `cmbOutReason.SelectedIndex = 0` in `RefreshData()`
- `txtRecipient.Clear()` in `RefreshData()`
- `string reason = cmbOutReason.SelectedItem.ToString()` in `BtnExecuteStockOut_Click`

**Update Notes string:**
```csharp
// Before:
string notes = $"Reason: {reason} | Recipient: {txtRecipient.Text.Trim()} | Warranty: {lblWarrantyDuration.Text}";
// After:
string notes = $"Warranty: {lblWarrantyDuration.Text}";
```

---

## Fix 5 — Users: Password Minimum Length

**File:** `Forms/FrmUsers.cs`, line 262

```csharp
// Before:
else if (!ValidationHelper.IsValidLength(txtUserPassword.Text, 6, 50, out errorMsg))
// After:
else if (!ValidationHelper.IsValidLength(txtUserPassword.Text, 2, 50, out errorMsg))
```

One line change only.

---

## Fix 6 — Audit Log: UX Overhaul

**Files:** `Forms/FrmAuditLog.cs`, `Forms/FrmAuditLog.Designer.cs`

### 6.1 Column Changes (Designer)

Current columns: `colTimestamp` (15%) | `colUserIdentity` (20%) | `colActionType` (15%) | `colTargetEntity` (25%)

New layout:
| Column Name | Header | FillWeight | Notes |
|---|---|---|---|
| `colTimestamp` | "Timestamp" | 15 | unchanged |
| `colUserIdentity` | "Username" | 15 | rename header |
| `colActionType` | "Action" | 20 | rename header |
| `colDescription` | "Description" | 50 | rename from `colTargetEntity` |

### 6.2 LoadAuditData — Format ActionType

```csharp
dgvAuditLog.Rows.Add(
    log.LogTimestamp.ToString("MMM dd, hh:mm tt"),
    log.Username,
    FormatActionType(log.ActionType),   // ← apply formatter
    log.Description
);
```

**`FormatActionType(string raw)` method:**
```
"STOCK STOCKIN"           → "Stock In"
"STOCK STOCKOUT"          → "Stock Out"
"STOCK RESTOCK"           → "Restock"
"STOCK RETURNTOSUPPLIER"  → "Return to Supplier"
"PRODUCT ADDED"           → "Product Added"
"PRODUCT DELETED"         → "Product Deleted"
"USER ADDED"              → "User Added"
"USER DELETED"            → "User Deleted"
"SUPPLIER ADDED"          → "Supplier Added"
"SUPPLIER DELETED"        → "Supplier Deleted"
default                   → raw (fallback)
```

### 6.3 Row Background Coloring

Replace current `DgvAuditLog_CellFormatting` foreground coloring with full-row background via `RowPrePaint` — keyed on the **raw** `log.ActionType` stored in a `Tag` on each row, so the formatter doesn't break the color lookup.

Strategy: store raw ActionType in `row.Tag` when adding rows, then in `RowPrePaint`:
```
"STOCK STOCKIN" / "STOCK RESTOCK"            → Color.FromArgb(220, 252, 231)  // green
"STOCK STOCKOUT" / "STOCK RETURNTOSUPPLIER"  → Color.FromArgb(254, 226, 226)  // salmon
"USER ADDED" / "USER DELETED"                → Color.FromArgb(219, 234, 254)  // blue
"SUPPLIER ADDED" / "SUPPLIER DELETED"        → Color.FromArgb(254, 249, 195)  // yellow
"PRODUCT ADDED" / "PRODUCT DELETED"          → Color.FromArgb(243, 244, 246)  // light grey
```

Keep ActionType column bold font (from existing CellFormatting, just remove foreground color override).

---

## Files Affected Summary

| File | Change Type |
|------|-------------|
| `Database/CreateDatabase.sql` | Add `CategorySpecTemplates` table |
| `DAL/CategorySpecTemplateRepository.cs` | **New file** |
| `Forms/FrmDashboard.cs` | Fix 1 — product name lookup |
| `Forms/FrmAddCategory.cs` | Fix 2 — replace Filter UI with SpecKey UI |
| `Forms/FrmAddCategory.Designer.cs` | Fix 2 — remove/add controls |
| `Forms/FrmAddProduct.cs` | Fix 2 — category-driven DGV |
| `Forms/FrmAddProduct.Designer.cs` | Fix 2 — remove flpDynamicSpecs, add dgvProductSpecs |
| `Forms/FrmProducts.cs` | Fix 2 — add dgvSpecs above txtProdSpec |
| `Forms/FrmProducts.Designer.cs` | Fix 2 — add dgvSpecs control |
| `Forms/FrmStockIn.cs` | Fix 3 — remove PO field |
| `Forms/FrmStockIn.Designer.cs` | Fix 3 — remove controls |
| `Forms/FrmStockOut.cs` | Fix 4 — remove OutReason + Recipient |
| `Forms/FrmStockOut.Designer.cs` | Fix 4 — remove controls |
| `Forms/FrmUsers.cs` | Fix 5 — password min 6→2 |
| `Forms/FrmAuditLog.cs` | Fix 6 — row colors, formatter, column order |
| `Forms/FrmAuditLog.Designer.cs` | Fix 6 — column rename/resize |

---

## Constraints

- Branch: `feature/sql-server-final` only — never touch master
- No new Models needed — `StockMovement` stays as-is
- `AuditLog` table never written from C# — unchanged
- All Triggers stay unchanged
- `ProductSpecification` model already exists and is used by `ProductRepository.GetSpecifications()`

# MIGRATION_CONTEXT.md
# InventoryManagementSystem — Migration to InventoryDBv3
# Context document for agent handoff — read this before touching any file.

---

## 1. حالة الـ Phases

| Phase | الوصف | الحالة |
|---|---|---|
| Phase 0 | استكشاف — قائمة كل الملفات | ✅ مكتمل |
| Phase 1 | Connection String + وضع SQL في Database/ | ✅ مكتمل (commit `b600878`) |
| Phase 2 | تحديث Models لتطابق InventoryDBv3 | ✅ مكتمل (commit `c7c74d9`) |
| Phase 3 | تحديث DAL Repositories | ✅ مكتمل (commit `fe565d9`) |
| Phase 4 | تحديث Forms | ✅ مكتمل (commit `39d8e2a`) |
| Phase 5 | حذف كود AuditLog INSERT من C# | ✅ لا يوجد — الكود نظيف من الأساس |
| Phase 6 | Build & Verify | ✅ Build ناجح، 0 errors |

**المشروع اكتمل بالكامل. لا توجد phases متبقية.**
البرانش: `InventoryDBv3-migration` — 4 commits فوق `master`.

---

## 2. ملخص كل Phase

### Phase 1 — Connection String + SQL Schema
**الملفات المعدّلة:**
- `DAL/DatabaseHelper.cs:9` — تغيير `Initial Catalog=InventoryDB` → `Initial Catalog=InventoryDBv3`
- `Database/InventoryDBv3.sql` — ملف جديد (وُضع في مجلد Database/)
- `InventoryManagementSystem.csproj` — إضافة كل ملفات `Database/*.sql` كـ `<None Include="..."/>` حتى تظهر في Solution Explorer

**ملاحظة مهمة:** الـ Connection String **مدمج في الكود** في `DatabaseHelper.cs`، وليس في `App.config`. الـ `App.config` لا يحتوي على connection string على الإطلاق.

---

### Phase 2 — Models
**كل ملف في مجلد `Models/`:**

| الملف | التغييرات |
|---|---|
| `User.cs` | أُضيف `int EmployeeID` كـ PK جديد |
| `Category.cs` | أُضيف `int CategoryID` كـ IDENTITY PK |
| `Supplier.cs` | أُضيف `int SupplierTaxNumber` كـ PK |
| `StorageZone.cs` | أُضيف `int ZoneID`، حُذف `string CategoryName`، أُضيف `int CategoryID` |
| `Product.cs` | `string SerialNumber` → `int ProductID`، أُضيف `int CategoryID`، بقي `string CategoryName` (من الـ view) |
| `ProductSpecification` (nested in Product.cs) | `string ProductSerial` → `int ProductID`، أُضيف `int SpecID` |
| `ProductItem.cs` | `string ItemSerialNumber` → `int ItemID`، `string ProductSerial` → `int ProductID` |
| `StockMovement.cs` | `string ProductSerial` → `int ProductID`، `string Username` → `int? EmployeeID`، `string SupplierName` → `int? SupplierTaxNumber` |
| `AuditLogEntry.cs` | **لا تغيير** — `Username` لا يزال string في جدول AuditLog |

---

### Phase 3 — DAL Repositories
**كل ملف في مجلد `DAL/`:**

| الملف | أبرز التغييرات |
|---|---|
| `UserRepository.cs` | SELECT/INSERT يشمل `EmployeeID`، `Delete(int)` بدل `Delete(string)`، أُضيف `EmployeeIdExists(int)` |
| `CategoryRepository.cs` | `Add()` يرجع `int` (SCOPE_IDENTITY)، `Delete(int categoryId)`، أُضيف `GetIdByName(string)` |
| `SupplierRepository.cs` | `SupplierTaxNumber` في كل queries، `Delete(int)` بدل `Delete(string)`، أُضيف `TaxNumberExists(int)` |
| `StorageZoneRepository.cs` | `GetByCategory(int categoryId)` بدل string، `Add` يستخدم `CategoryID`، `Delete(int zoneId)` |
| `ProductRepository.cs` | vw_ProductStock يرجع `ProductID, CategoryID, CategoryName`، `GetById(int)` بدل `GetBySerial(string)`، `Exists(int)` |
| `ProductItemRepository.cs` | `GetAvailable(int productId)`، `AddBatch` يستدعي `SELECT NEXT VALUE FOR seq_ProductItems` لكل item، `MarkRemoved(int itemId)`، `MarkRemovedBatch(int productId, int qty)` |
| `StockMovementRepository.cs` | INSERT يستخدم `ProductID, EmployeeID, SupplierTaxNumber` (ints)، `GetByProduct(int)` |
| `CategorySpecTemplateRepository.cs` | `GetByCategory(int categoryId)`، `Add(int categoryId, string specKey)`، `DeleteByCategory(int)` |
| `AuditLogRepository.cs` | **لا تغيير** — قراءة فقط، triggers تكتب فيه |

**ملاحظة على seq_ProductItems:**
في `ProductItemRepository.AddBatch()`:
```csharp
int itemId = NextItemId(conn);  // SELECT NEXT VALUE FOR seq_ProductItems
```
الـ ItemID الآن يأتي من الـ sequence — ليس من C#.

---

### Phase 4 — Forms

| الملف | أبرز التغييرات |
|---|---|
| `FrmSupplierManagement.cs` | `txtContactPerson` (كان مخفياً) أُعيد توظيفه كحقل Tax Number مع تغيير label، `Tag` يخزن `Supplier` كامل بدل string، `Delete/Edit` بـ int PK |
| `FrmUsers.cs` | حقل `EmployeeID` يُضاف ديناميكياً في `FrmUsers_Load`، `_selectedEmployeeId` (int)، `Delete/Update` بـ int، مقارنة الـ session بـ `EmployeeID` |
| `FrmAddCategory.cs` | `CategoryRepository.Add()` يرجع `int newCategoryId`، `StorageZone.CategoryID = newCategoryId`، `CategorySpecTemplateRepository.Add(int, string)` |
| `FrmAddProduct.cs` | `txtSerialNumber` للـ ProductID (int)، `cmbCategory.ValueMember = "CategoryID"`، `EmployeeID` من session، `SupplierTaxNumber` من selection |
| `FrmProducts.cs` | `cmbCategory.ValueMember = "CategoryID"`، filter بـ `p.CategoryID`، search بـ `p.ProductID.ToString()`، `ProductID` في grid |
| `FrmStockIn.cs` | constructor يقبل `int productId`، `StorageZoneRepository.GetByCategory(product.CategoryID)`، `cmbSupplier.ValueMember = "SupplierTaxNumber"` |
| `FrmStockOut.cs` | constructor يقبل `int productId`، `clbSerialNumbers` يعرض `ItemID` (int)، `MarkRemoved(int itemId)` |
| `FrmDashboard.cs` | `productDict` بـ `int` key، `m.ProductID`، `m.EmployeeID` للعرض |

---

## 3. الـ Mapping الكامل — قديم → جديد

### PKs (المفاتيح الأساسية)

| الجدول | القديم (string PK) | الجديد (int PK) | ملاحظة |
|---|---|---|---|
| Users | `Username` (string PK) | `EmployeeID` (int PK) | `Username` بقي UNIQUE عادي |
| Suppliers | `SupplierName` (string PK) | `SupplierTaxNumber` (int PK) | `SupplierName` بقي UNIQUE |
| Categories | `CategoryName` (string PK) | `CategoryID` (int IDENTITY PK) | `CategoryName` بقي UNIQUE |
| StorageZones | `ZoneName` (string PK) | `ZoneID` (int IDENTITY PK) | `ZoneName` بقي UNIQUE |
| Products | `SerialNumber` (string PK) | `ProductID` (int PK) | يُدخله المستخدم، ليس IDENTITY |
| ProductItems | `ItemSerialNumber` (string PK) | `ItemID` (int من seq_ProductItems) | |
| CategorySpecTemplates | Composite (CategoryName+SpecKey) | `TemplateID` (int IDENTITY PK) + `CategoryID` FK int | |
| ProductSpecifications | Composite (ProductSerial+SpecKey) | `SpecID` (int IDENTITY PK) + `ProductID` FK int | |
| StockMovements | auto-increment | `MovementID` (int IDENTITY PK) | لم يتغير المبدأ |

### FKs في StockMovements

| القديم | الجديد |
|---|---|
| `string ProductSerial` | `int ProductID` |
| `string Username` | `int? EmployeeID` |
| `string SupplierName` | `int? SupplierTaxNumber` |

### الـ View — vw_ProductStock

| القديم (columns) | الجديد (columns) |
|---|---|
| `SerialNumber, ProductName, CategoryName, Price, Quantity, StockStatus` | `ProductID, ProductName, CategoryID, CategoryName, Price, Quantity, StockStatus` |

---

## 4. القواعد الصارمة

1. **البرانش الوحيد**: كل commits تروح لـ `InventoryDBv3-migration` فقط.
2. **AuditLog محظور من C#**: لا `INSERT INTO AuditLog` في أي كود C# — Triggers في DB تتولى ذلك تلقائياً. `AuditLogRepository` للقراءة فقط.
3. **لا تعدّل InventoryDBv3.sql**: الـ schema نهائي ومحسوم.
4. **لا نقل بيانات**: InventoryDBv3 فارغة تماماً — لا migration scripts لنقل بيانات من InventoryDB القديمة.
5. **ProductID ليس IDENTITY**: يُدخله المستخدم يدوياً (رقم منتج حقيقي). لا تستخدم `SCOPE_IDENTITY()` له.
6. **seq_ProductItems للـ ItemID فقط**: كل ItemID جديد يأتي من `SELECT NEXT VALUE FOR seq_ProductItems` في الكود.

---

## 5. التحديات والملاحظات المكتشفة

### مشاكل معالجة أثناء التنفيذ:

| التحدي | الحل المطبّق |
|---|---|
| Connection string مدمج في كود (`DatabaseHelper.cs`) وليس `App.config` | عُدّل `DatabaseHelper.cs:9` مباشرة |
| `FrmSupplierManagement` ليس فيها حقل SupplierTaxNumber في Designer | أُعيد توظيف `txtContactPerson` المخفي كحقل Tax Number + تغيير label |
| `FrmUsers` ليس فيها حقل EmployeeID في Designer | حقل يُضاف ديناميكياً في `FrmUsers_Load` فوق txtUserName |
| `FrmStockOut` و`FrmStockIn` constructors كانت تقبل `string serial` | حُوّلت لـ `int productId` |
| `FrmAddProduct` كانت تولّد ItemSerialNumber بصيغة `{serial}-{count:D2}` | حُذفت هذه الصيغة — `ItemID` يأتي من sequence في `ProductItemRepository.AddBatch()` |
| ملفات `Database/*.sql` لم تكن مضافة للـ `.csproj` | أُضيفت كـ `<None Include="..."/>` في `.csproj` |
| `StorageZoneRepository.GetByCategory` كانت تقبل string | حُوّلت لـ `int categoryId` — الـ Forms تمرّر `product.CategoryID` |
| في `FrmProducts`، فلتر Category كان بالاسم | حُوّل لـ `p.CategoryID == selectedCat.CategoryID` |
| `FrmDashboard` يعرض اسم الـ operator | يعرض الآن `ID:{EmployeeID}` أو "System" |

### ملاحظات على البنية المعمارية:
- `DatabaseHelper.CurrentUser` يخزن كائن `User` كامل بعد تسجيل الدخول — يحتوي الآن على `EmployeeID` ويُستخدم في كل Forms
- الـ `vw_ProductStock` view يجمع `CategoryName` بـ JOIN — لهذا Model Product يحتفظ بـ `CategoryName` رغم أن الجدول يخزن `CategoryID` فقط
- `AuditLog.Username` عمود string (ليس FK) — يعني `AuditLogEntry` Model لم يحتج تعديل

---

## 6. الخطوة القادمة

**لا توجد phases متبقية — المشروع مكتمل.**

لو أردت مواصلة، الأولويات المقترحة:
1. **اختبار فعلي للتطبيق** — تشغيله على InventoryDBv3 والتأكد من كل شاشة
2. **إضافة بيانات تجريبية** — إنشاء user، supplier، category، product لاختبار الـ flow كاملاً
3. **تحسين FrmDashboard** — عرض Username بدل EmployeeID في Recent Movements (يحتاج JOIN مع Users)
4. **FrmLogin enhancement** — يمكن إضافة Employee ID في نافذة الـ session display

---

## 7. قائمة كل الملفات المعدّلة في المشروع

### مجلد Database/
- `Database/InventoryDBv3.sql` ← **جديد** (وُضع في Phase 1)

### DAL/
- `DAL/DatabaseHelper.cs` — connection string
- `DAL/UserRepository.cs` — EmployeeID int PK
- `DAL/CategoryRepository.cs` — CategoryID IDENTITY
- `DAL/SupplierRepository.cs` — SupplierTaxNumber int PK
- `DAL/StorageZoneRepository.cs` — ZoneID + CategoryID ints
- `DAL/ProductRepository.cs` — ProductID int, vw_ProductStock columns
- `DAL/ProductItemRepository.cs` — ItemID from sequence, ProductID int
- `DAL/StockMovementRepository.cs` — int FKs throughout
- `DAL/CategorySpecTemplateRepository.cs` — CategoryID int FK

### Models/
- `Models/User.cs` — +EmployeeID
- `Models/Category.cs` — +CategoryID
- `Models/Supplier.cs` — +SupplierTaxNumber
- `Models/StorageZone.cs` — +ZoneID, CategoryName→CategoryID
- `Models/Product.cs` — SerialNumber→ProductID(int), +CategoryID
- `Models/ProductItem.cs` — ItemSerialNumber→ItemID(int), ProductSerial→ProductID(int)
- `Models/StockMovement.cs` — ProductSerial/Username/SupplierName → ProductID/EmployeeID/SupplierTaxNumber (ints)
- `Models/AuditLogEntry.cs` — **لا تغيير**

### Forms/
- `Forms/FrmLogin.cs` — **لا تغيير** (Username string لا يزال صالح)
- `Forms/FrmMain.cs` — **لا تغيير**
- `Forms/FrmDashboard.cs` — ProductID dict, EmployeeID display
- `Forms/FrmProducts.cs` — ProductID int, CategoryID filter
- `Forms/FrmAddProduct.cs` — ProductID (int input), CategoryID ValueMember
- `Forms/FrmAddCategory.cs` — int categoryId من Add(), StorageZone.CategoryID
- `Forms/FrmSupplierManagement.cs` — txtContactPerson=TaxNumber, Supplier tag
- `Forms/FrmUsers.cs` — EmployeeID dynamic field, int tracking
- `Forms/FrmStockIn.cs` — int productId constructor, int FKs
- `Forms/FrmStockOut.cs` — int productId constructor, ItemID checklist
- `Forms/FrmAuditLog.cs` — **لا تغيير** (قراءة فقط)
- `Forms/FrmReports.cs` — **لا تغيير** (شاشة فارغة)
- `Forms/FrmExitDialog.cs` — **لا تغيير**

### Root
- `InventoryManagementSystem.csproj` — Database/*.sql مضافة
- `App.config` — **لا تغيير** (لا يحتوي connection string)

---

## Git Log (InventoryDBv3-migration branch)

```
39d8e2a  feat: update all Forms for InventoryDBv3 int PKs and new bindings
fe565d9  feat: rewrite all DAL repositories for InventoryDBv3 int PKs/FKs
c7c74d9  feat: update models and csproj for InventoryDBv3 schema
b600878  feat: migrate connection string to InventoryDBv3 and add schema SQL
```

---
*Generated at end of migration session — 2026-05-25*

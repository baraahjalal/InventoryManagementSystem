# خارطة طريق المشروع النهائية
## نظام إدارة المخزون — مادة Advanced C#

> هذا الملف هو **المرجع الوحيد** للمشروع. يحتوي على كل القرارات التصميمية، المبررات الأكاديمية، وخارطة التنفيذ الكاملة.

---

## 1. السياق العام

| البند | التفاصيل |
|-------|---------|
| المادة | Advanced C# |
| المرحلة | النهائية (Final Phase) |
| الهدف | تحويل النظام من Runtime-only إلى SQL Server + Crystal Reports |
| ملاحظة | الكود القديم (MemoryStore) يُستخدم لفهم المنطق فقط — التصميم من الصفر |

---

## 2. تصميم قاعدة البيانات — 9 جداول

### 2.1 القاعدة العامة للمفاتيح

> **متطلب الدكتورة:** استخدام المفتاح الطبيعي حيثما أمكن. IDENTITY فقط حين لا يوجد مفتاح طبيعي.

---

### 2.2 تفاصيل الجداول

#### جدول 1: Users
```
Username     VARCHAR(50)    PRIMARY KEY  ← مفتاح طبيعي
Password     VARCHAR(255)   NOT NULL
Role         VARCHAR(30)    NOT NULL     ('System Administrator' | 'Employee')
IsAdmin      BIT            DEFAULT 0
ProfilePhoto VARBINARY(MAX) NULL
```
**مبرر المفتاح:** Username فريد دائماً ويُستخدم مرجعاً في StockMovements وAuditLog. لا قيمة لإضافة ID.

---

#### جدول 2: Categories
```
CategoryName VARCHAR(100) PRIMARY KEY  ← مفتاح طبيعي
```
**مبرر المفتاح:** الاسم فريد ومستقر ويُعرض في كل فورم. لا داعي لـ ID.

---

#### جدول 3: Suppliers
```
SupplierName VARCHAR(150) PRIMARY KEY  ← مفتاح طبيعي
Phone        VARCHAR(20)  NULL
Email        VARCHAR(100) NULL
IsActive     BIT          DEFAULT 1
```
**مبرر المفتاح:** الكود الأصلي يتحقق من تفرد الاسم صراحةً في ValidateForm().
**ملاحظة:** حُذف ContactPerson — هذا حقل CRM، لا ينتمي لنظام مخزون.

---

#### جدول 4: StorageZones
```
ZoneName     VARCHAR(100) PRIMARY KEY  ← مفتاح طبيعي
CategoryName VARCHAR(100) NOT NULL  → FK → Categories
```
**مبرر المفتاح:** "Aisle A-1: Laptops" فريد ومعبّر. لا داعي لـ ID.

---

#### جدول 5: Products
```
SerialNumber VARCHAR(50)   PRIMARY KEY  ← مفتاح طبيعي
ProductName  NVARCHAR(200) NOT NULL
CategoryName VARCHAR(100)  NOT NULL  → FK → Categories
Price        DECIMAL(10,2) NOT NULL
```
**مبرر المفتاح:** SerialNumber كـ "APP-MBP-2023" هو هوية المنتج الفعلية في أي نظام مخزون.
**قرار هام:** **لا يوجد عمود Quantity** — تُحسب من ProductItems عبر View.

---

#### جدول 6: ProductSpecifications
```
ProductSerial VARCHAR(50)   NOT NULL  → FK → Products
SpecKey       VARCHAR(50)   NOT NULL
SpecValue     NVARCHAR(100) NOT NULL
PRIMARY KEY (ProductSerial, SpecKey)  ← مفتاح طبيعي مركّب
```
**شرح SpecKey:** هو اسم الخاصية. مثال حقيقي:

| ProductSerial | SpecKey | SpecValue |
|---|---|---|
| APP-MBP-2023 | Processor | Apple M3 |
| APP-MBP-2023 | RAM | 16GB |
| SAM-S24U-001 | Storage | 512GB |
| SAM-S24U-001 | Color | Phantom Black |

**مبرر المفتاح المركّب:** منتج واحد لا يملك قيمتين لنفس الخاصية. (ProductSerial + SpecKey) فريد دائماً.

---

#### جدول 7: StockMovements
```
MovementId      INT IDENTITY(1,1) PRIMARY KEY  ← IDENTITY (مبرر)
ProductSerial   VARCHAR(50)   NOT NULL  → FK → Products
MovementType    VARCHAR(20)   NOT NULL  ('StockIn'|'StockOut'|'Restock'|'ReturnToSupplier')
QuantityChanged INT           NOT NULL
MovementDate    DATETIME      DEFAULT GETDATE()
Username        VARCHAR(50)   NULL      → FK → Users
Notes           NVARCHAR(500) NULL
WarrantyMonths  INT           NULL
SupplierName    VARCHAR(150)  NULL      → FK → Suppliers
```
**مبرر IDENTITY:** سجل معاملات — نفس المنتج يُدخل مئات المرات في أوقات مختلفة. لا مفتاح طبيعي.
**لماذا لا Timestamp؟** دقة DATETIME = 3.33ms، إذا حدثت عمليتان في نفس اللحظة → تعارض في PK. خطأ تقني وأكاديمي.

---

#### جدول 8: ProductItems
```
ItemSerialNumber VARCHAR(80) PRIMARY KEY  ← مفتاح طبيعي
ProductSerial    VARCHAR(50) NOT NULL  → FK → Products
IsInStock        BIT         DEFAULT 1
DateAdded        DATETIME    DEFAULT GETDATE()
DateRemoved      DATETIME    NULL
BatchMovementId  INT         NULL      → FK → StockMovements
```
**مبرر المفتاح:** "APP-MBP-2023-01" فريد عالمياً بالبناء (Serial المنتج + رقم تسلسلي).
**هدف BatchMovementId:** ربط كل وحدة فيزيائية بدفعة الشراء التي جاءت منها → استخراج الضمان الخاص بها.

---

#### جدول 9: AuditLog
```
LogId        INT IDENTITY(1,1) PRIMARY KEY  ← IDENTITY (مبرر)
LogTimestamp DATETIME      DEFAULT GETDATE()
ActionType   VARCHAR(50)   NOT NULL
Description  NVARCHAR(500) NOT NULL
Username     VARCHAR(50)   NOT NULL
```
**مبرر IDENTITY:** سجل أحداث — نفس المستخدم يقوم بأفعال متعددة في اللحظة ذاتها. لا مفتاح طبيعي.
**قاعدة صارمة:** هذا الجدول يُملأ فقط عبر SQL Triggers. لا يكتب فيه كود C# أبداً.

---

### 2.3 View للكميات

```sql
CREATE VIEW vw_ProductStock AS
SELECT
    p.SerialNumber, p.ProductName, p.CategoryName, p.Price,
    COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) AS Quantity,
    CASE
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) = 0   THEN 'Out of Stock'
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) <= 10 THEN 'Low Stock'
        ELSE 'In Stock'
    END AS StockStatus
FROM Products p
LEFT JOIN ProductItems pi ON pi.ProductSerial = p.SerialNumber
GROUP BY p.SerialNumber, p.ProductName, p.CategoryName, p.Price;
```
**كل فورم يحتاج كمية المنتج → يقرأ من هذا الـ View مباشرة.**

---

### 2.4 ملخص قرارات المفاتيح (للدكتورة)

| الجدول | نوع المفتاح | القيمة | المبرر |
|--------|------------|--------|--------|
| Users | طبيعي | `Username` | فريد، يُستخدم مرجعاً في كل مكان |
| Categories | طبيعي | `CategoryName` | فريد ومستقر |
| Suppliers | طبيعي | `SupplierName` | الكود يتحقق من تفرده |
| StorageZones | طبيعي | `ZoneName` | فريد ومعبّر |
| Products | طبيعي | `SerialNumber` | هوية المنتج الفعلية |
| ProductSpecifications | Composite طبيعي | `(ProductSerial, SpecKey)` | خاصية واحدة لكل منتج |
| ProductItems | طبيعي | `ItemSerialNumber` | فريد عالمياً بالبناء |
| **StockMovements** | **IDENTITY** | `MovementId` | **سجل معاملات — لا مفتاح طبيعي** |
| **AuditLog** | **IDENTITY** | `LogId` | **سجل أحداث — لا مفتاح طبيعي** |

---

## 3. ما تم حذفه ولماذا

| المحذوف | السبب |
|---------|-------|
| `CategoryFilters` | يخدم UI فقط (dropdowns ديناميكية)، ليس بيانات أعمال. المواصفات تُكتب يدوياً كنص حر. |
| `CategoryFilterOptions` | نفس السبب. |
| `SupplierCategories` | المورد جهة شراء بسيطة. لا حاجة لربطه بتصنيفات. عند StockIn تُعرض كل الموردين النشطين. |
| `ProductSuppliers` | زائد — StockMovements.SupplierName يحفظ من ورّد كل دفعة مباشرة. |
| عمود `Quantity` في Products | قيمة مشتقة من ProductItems → تخزينها يخلق redundancy. تُحسب عبر View. |
| `ContactPerson` في Suppliers | بيانات CRM لا تنتمي لنظام مخزون. |

---

## 4. معمارية الـ AuditLog عبر Triggers

### المبدأ
كود C# لا يكتب في AuditLog **أبداً**. كل السجلات تُنشأ تلقائياً عند وقوع الحدث في قاعدة البيانات.

### لماذا هذا القرار جيد أكاديمياً؟
- يُظهر أن قاعدة البيانات تدير نفسها بشكل مستقل عن التطبيق
- ضمان اتساق البيانات حتى لو استُدعيت قاعدة البيانات من مكان آخر
- يُبرز معرفتك بـ Database-level programming

### الـ Triggers الأربعة

#### Trigger 1 — StockMovements (الأهم)
- **الحدث:** أي INSERT في StockMovements
- **الميزة:** يعرف Username مباشرة من الصف المُدخل
- **مثال السجل:** `STOCK STOCKIN | Product [APP-MBP-2023]: 10 units. Supplier: TechSource Inc.`

#### Trigger 2 — Products
- **الحدث:** INSERT أو DELETE في Products
- **يسجل:** إضافة منتج جديد أو حذف منتج
- **مثال السجل:** `PRODUCT ADDED | New product: [APP-MBP-2023] MacBook Pro 14" M3`

#### Trigger 3 — Users
- **الحدث:** INSERT أو DELETE في Users
- **يسجل:** إنشاء حساب جديد أو حذف حساب
- **مثال السجل:** `USER ADDED | New user: ahmed.clerk (Employee)`

#### Trigger 4 — Suppliers
- **الحدث:** INSERT أو DELETE في Suppliers
- **يسجل:** تسجيل مورد جديد أو حذفه
- **مثال السجل:** `SUPPLIER ADDED | New supplier: TechSource Inc.`

---

## 5. القواعد التجارية (Business Rules)

| القاعدة | التفاصيل |
|---------|---------|
| StockIn يتطلب مورّداً | لا يمكن إدخال مخزون بدون تحديد المورد |
| ReturnToSupplier يتطلب مورّداً | نفس القاعدة |
| StockOut لا يتطلب مورّداً | الإخراج يكون للعملاء لا للموردين |
| لا يمكن إخراج أكثر من المتوفر | Quantity >= 0 دائماً |
| لا يمكن حذف المستخدم الحالي | يُمنع حذف الحساب المتصل حالياً |
| لا يمكن إزالة صلاحية Admin من نفسك | يُمنع self-demotion |
| حظر تسجيل دخول بعد 3 محاولات فاشلة | 30 ثانية lockout |
| Username فريد | لا يمكن تسجيل مستخدمين بنفس الاسم |
| SerialNumber المنتج فريد | لا يمكن إضافة منتجين بنفس الـ Serial |
| SupplierName فريد | لا يمكن تسجيل موردين بنفس الاسم |
| AuditLog للقراءة فقط من C# | لا يكتب فيه الكود مباشرة أبداً |

---

## 6. هيكل ملفات المشروع

```
InventoryManagementSystem/
│
├── Database/                        ← سكريبتات SQL
│   ├── CreateDatabase.sql           ← 9 جداول + View
│   ├── Triggers.sql                 ← 4 Triggers للـ AuditLog
│   └── SeedData.sql                 ← بيانات أولية
│
├── Models/                          ← POCO classes (بيانات فقط، بدون منطق)
│   ├── User.cs
│   ├── Category.cs
│   ├── Supplier.cs
│   ├── Product.cs                   ← Quantity و StockStatus كـ properties
│   ├── ProductItem.cs
│   ├── StockMovement.cs
│   ├── StorageZone.cs
│   └── AuditLogEntry.cs
│
├── DAL/                             ← Data Access Layer
│   ├── DatabaseHelper.cs            ← Connection String + CurrentUser session
│   ├── CategoryRepository.cs        ← GetAll, Add, Delete, Exists
│   ├── UserRepository.cs            ← Authenticate, GetAll, Add, Update, Delete
│   ├── SupplierRepository.cs        ← GetAll, GetActive, Add, Update, Delete
│   ├── StorageZoneRepository.cs     ← GetAll, GetByCategory
│   ├── ProductRepository.cs         ← GetAll (from View), Add, Delete
│   ├── ProductItemRepository.cs     ← GetAvailable, AddBatch, MarkAsRemoved
│   ├── StockMovementRepository.cs   ← Add, GetRecent, GetByProduct
│   └── AuditLogRepository.cs        ← GetAll (read-only)
│
├── Forms/                           ← الفورمز الموجودة — تُعدَّل فقط
│   ├── FrmLogin.cs
│   ├── FrmMain.cs
│   ├── FrmDashboard.cs
│   ├── FrmProducts.cs + FrmAddProduct.cs
│   ├── FrmStockIn.cs
│   ├── FrmStockOut.cs
│   ├── FrmSupplierManagement.cs     ← مبسّط (بدون ContactPerson/SupplierCategories)
│   ├── FrmUsers.cs
│   ├── FrmAuditLog.cs               ← read-only عرض فقط
│   ├── FrmReports.cs                ← Crystal Reports
│   └── FrmAddCategory.cs
│
├── Reports/                         ← ملفات Crystal Reports
│   ├── rpt_InventoryList.rpt        ← كل المنتجات + كمياتها + حالتها
│   ├── rpt_StockMovements.rpt       ← حركات المخزون مع التواريخ
│   ├── rpt_LowStock.rpt             ← المنتجات أقل من 10 وحدات
│   └── rpt_AuditLog.rpt             ← سجل نشاط النظام
│
└── docs/
    └── PROJECT_ROADMAP.md           ← هذا الملف
```

---

## 7. خارطة التنفيذ — 5 مراحل

---

### المرحلة 1: قاعدة البيانات
**الملفات:** `Database/CreateDatabase.sql`، `Database/Triggers.sql`، `Database/SeedData.sql`

**الخطوات:**
1. كتابة `CreateDatabase.sql` — ينشئ InventoryDB ويبني 9 جداول + View
2. كتابة `Triggers.sql` — 4 triggers تملأ AuditLog تلقائياً
3. كتابة `SeedData.sql` — بيانات أولية (5 تصنيفات، 2 مستخدمين، 3 موردين، 5+ منتجات)
4. تشغيل السكريبتات على SQL Server Management Studio أو Visual Studio Server Explorer
5. **التحقق:**
   - كل 9 جداول موجودة
   - `SELECT * FROM vw_ProductStock` تُرجع نتائج بكميات صحيحة
   - INSERT في StockMovements → يظهر تلقائياً في AuditLog (اختبار الـ Trigger)

---

### المرحلة 2: Models + DAL
**الملفات:** `Models/*.cs`، `DAL/*.cs`

**الخطوات:**
1. إنشاء مجلد `Models/` وكتابة كل Model كـ POCO class بدون منطق
2. إنشاء `DAL/DatabaseHelper.cs` مع Connection String وSessionUser
3. كتابة Repositories بهذا الترتيب (من الأبسط للأعقد):
   - `CategoryRepository`
   - `UserRepository`
   - `SupplierRepository`
   - `StorageZoneRepository`
   - `ProductRepository` ← يقرأ من `vw_ProductStock`
   - `StockMovementRepository`
   - `ProductItemRepository`
   - `AuditLogRepository` ← read-only فقط
4. **التحقق:** كتابة Console test أو Unit test بسيط لكل Repository يتأكد من قراءة/كتابة البيانات

---

### المرحلة 3: تعديل الفورمز
**الملفات:** جميع `Forms/*.cs` الموجودة — تُعدَّل فقط، لا تُنشأ من الصفر

**قاعدة التعديل لكل فورم:**
- احذف كل سطر يستخدم `MemoryStore.XXX`
- استبدله بـ `XXXRepository.Method()`
- استبدل `MemoryStore.CurrentUser` بـ `DatabaseHelper.CurrentUser`

**ترتيب التعديل:**

| الأولوية | الفورم | السبب |
|---------|--------|-------|
| 1 | `FrmLogin` | الدخول للنظام — كل شيء يعتمد عليه |
| 2 | `FrmUsers` | CRUD المستخدمين |
| 3 | `FrmAddProduct` + `FrmProducts` | إضافة وعرض المنتجات |
| 4 | `FrmStockIn` + `FrmStockOut` | حركات المخزون |
| 5 | `FrmSupplierManagement` | مبسّط بدون SupplierCategories |
| 6 | `FrmAuditLog` | read-only من AuditLogRepository |
| 7 | `FrmDashboard` | أرقام من vw_ProductStock |
| 8 | `FrmAddCategory` | إضافة تصنيفات |

---

### المرحلة 4: Crystal Reports
**الملفات:** `Reports/*.rpt`، تعديل `Forms/FrmReports.cs`

**الخطوات لكل تقرير:**
1. تثبيت SAP Crystal Reports for Visual Studio (مجاني)
2. Add New Item → Crystal Report → اختر DataSet موصول بـ SQL Server
3. تصميم Layout: Header (عنوان + تاريخ الطباعة) + جدول البيانات + Footer
4. ربط التقرير بزر في `FrmReports.cs`

**التقارير الأربعة:**

| # | الاسم | مصدر البيانات | المحتوى |
|---|-------|--------------|---------|
| 1 | قائمة المخزون الكاملة | `vw_ProductStock` | كل المنتجات + كمياتها + حالتها + سعرها |
| 2 | حركات المخزون | `StockMovements JOIN Products` | كل عمليات الإدخال والإخراج مع التواريخ والمستخدمين |
| 3 | المنتجات منخفضة المخزون | `vw_ProductStock WHERE Quantity <= 10` | تنبيه بالمنتجات التي تحتاج إعادة تموين |
| 4 | سجل نشاط النظام | `AuditLog` | كل الأحداث المسجلة تلقائياً بالـ Triggers |

---

### المرحلة 5: مراجعة نهائية
**الخطوات:**
1. تشغيل كل الفورمز وتأكيد قراءة/كتابة البيانات من DB
2. تأكيد أن AuditLog يُملأ تلقائياً — لا يوجد `LogAction()` في C# في أي مكان
3. تشغيل كل تقرير Crystal وتأكيد ظهور البيانات الصحيحة
4. اختبار قواعد الأعمال:
   - StockIn بدون مورد → يُرفض
   - StockOut بدون مورد → يُقبل
   - حذف المستخدم الحالي → يُرفض
   - محاولة 4 → حظر 30 ثانية

---

## 8. مقارنة StockMovements مقابل AuditLog

| | StockMovements | AuditLog |
|---|---|---|
| **الغرض** | بيانات أعمال منظمة | سجل أحداث أمني |
| **من يكتب فيه** | كود C# مباشرة | SQL Triggers فقط |
| **يُستخدم في** | Dashboard, Reports, حسابات الكمية | FrmAuditLog فقط |
| **يحتوي على** | ProductSerial, SupplierName, WarrantyMonths | نص حر لأي حدث |
| **لماذا لا يمكن دمجهما؟** | حقول StockMovements لا تنطبق على أحداث مثل "User Login" | → نصف الحقول NULL في كل صف = تصميم سيء |

---

## 9. ملاحظات للنقاش مع الدكتورة

### لماذا حذفنا ContactPerson من Suppliers؟
في نظام المخزون، الطرف المتعاقد هو الشركة لا الشخص. بيانات التواصل الشخصي تنتمي لنظام CRM. نحن نهتم بـ: من يورّد؟ وهل هو نشط؟

### لماذا لا Quantity في Products؟
الكمية قيمة مشتقة من عدد وحدات ProductItems التي IsInStock = 1. تخزينها مباشرة يخلق redundancy ويخاطر بعدم الاتساق. نحسبها بـ View — هذا هو التصميم الصحيح أكاديمياً.

### لماذا IDENTITY في جدولين فقط؟
لأن StockMovements وAuditLog سجلات تاريخية بدون هوية طبيعية. في جميع الجداول الأخرى يوجد مفتاح طبيعي مبرر. هذا بالضبط ما تطلبه الدكتورة.

### لماذا لا Timestamp كـ PK في StockMovements؟
دقة DATETIME = 3.33ms. عمليتان في نفس اللحظة تتشاركان Timestamp → تعارض في PK → فشل العملية. هذا خطأ تقني وأكاديمي. IDENTITY هو الصح.

### لماذا AuditLog عبر Triggers؟
لأن قاعدة البيانات يجب أن تكون مسؤولة عن تكاملها الخاص. الـ Triggers تضمن التسجيل حتى لو استُدعيت قاعدة البيانات من تطبيق آخر. هذا تصميم قوي أكاديمياً.

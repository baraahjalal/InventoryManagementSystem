# خطة المرحلة النهائية — نظام إدارة المخزون (SQL Server + Crystal Reports)

> **تنبيه:** هذا تصميم مستقل من الصفر. الـ MemoryStore مصدر للفهم فقط، ليس للترحيل.

**الهدف:** بناء نظام إدارة مخزون احترافي مبني على SQL Server بتصميم قاعدة بيانات صحيح أكاديمياً وعملياً.

**المعمارية:** WinForms → DAL (Repository Pattern) → ADO.NET → SQL Server LocalDB

**التقنيات:** C# .NET Framework 4.8 | SQL Server LocalDB | ADO.NET | Crystal Reports for Visual Studio

---

## الجزء الأول: استخراج المنطق التجاري من الكود الحالي

بعد قراءة جميع الملفات، المنطق التجاري الأساسي للنظام هو:

### الكيانات الحقيقية في النظام:
1. **المستخدمون (Users)** — تسجيل دخول، أدوار، تحكم بالصلاحيات
2. **التصنيفات (Categories)** — تصنيف المنتجات مع خصائص ديناميكية لكل تصنيف
3. **الموردون (Suppliers)** — من يورّد المنتجات (مبسّط)
4. **المنتجات (Products)** — المنتج بمواصفاته الديناميكية
5. **وحدات المنتج (ProductItems)** — كل وحدة مادية فردية بـ serial خاص بها
6. **حركات المخزون (StockMovements)** — سجل كل عملية إدخال/إخراج
7. **مناطق التخزين (StorageZones)** — مناطق المستودع مرتبطة بالتصنيف
8. **سجل النشاط (AuditLog)** — تتبع كل إجراء في النظام

### القواعد التجارية المستخرجة:
- StockIn: يتطلب مورّداً إلزامياً
- ReturnToSupplier: يتطلب مورّداً إلزامياً
- StockOut: لا يتطلب مورّداً
- لا يمكن أن تصبح الكمية سالبة
- يُمنع حذف المستخدم المتصل حالياً
- يُمنع إزالة صلاحية Admin من الحساب الحالي
- حظر تسجيل الدخول 30 ثانية بعد 3 محاولات فاشلة

---

## الجزء الثاني: تصميم قاعدة البيانات

### قرار جوهري: ماذا نخزّن؟ ماذا نحسب؟

**الكمية (Quantity)** في الكود الحالي هي حقل في `Product`، لكن في الحقيقة هي
**مشتقة** من عدد الـ `ProductItems` التي `IsInStock = true` لذلك المنتج.

**القرار الصحيح أكاديمياً:** لا نخزّن الكمية في جدول المنتجات.
نحسبها عبر View أو استعلام مباشر.
هذا يلغي إمكانية التناقض بين الجدولين.

```sql
-- View للكميات (تُستخدم في كل الفورمز)
CREATE VIEW vw_ProductStock AS
SELECT 
    p.SerialNumber,
    p.ProductName,
    p.CategoryName,
    p.Price,
    COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) AS Quantity,
    CASE 
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) = 0 THEN 'Out of Stock'
        WHEN COUNT(CASE WHEN pi.IsInStock = 1 THEN 1 END) <= 10 THEN 'Low Stock'
        ELSE 'In Stock'
    END AS StockStatus
FROM Products p
LEFT JOIN ProductItems pi ON pi.ProductSerial = p.SerialNumber
GROUP BY p.SerialNumber, p.ProductName, p.CategoryName, p.Price;
```

---

### جداول قاعدة البيانات

#### ترتيب الإنشاء (مراعاةً للـ Foreign Keys):

```
1. Users
2. Categories
3. CategoryFilters
4. CategoryFilterOptions
5. Suppliers
6. SupplierCategories
7. StorageZones
8. Products
9. ProductSpecifications
10. StockMovements
11. ProductItems
12. AuditLog
```

---

### تفاصيل كل جدول مع قرار المفتاح ومبرره

---

#### 1. جدول Users

```sql
CREATE TABLE Users (
    Username     VARCHAR(50)  NOT NULL,
    Password     VARCHAR(255) NOT NULL,
    Role         VARCHAR(30)  NOT NULL CHECK (Role IN ('System Administrator', 'Employee')),
    IsAdmin      BIT          NOT NULL DEFAULT 0,
    ProfilePhoto VARBINARY(MAX) NULL,
    CONSTRAINT PK_Users PRIMARY KEY (Username)
);
```

**قرار المفتاح:** `Username` — طبيعي
**المبرر:** Username فريد بالتعريف (الكود يمنع التكرار). يُستخدم في كل سجلات AuditLog والحركات كمرجع. استخدام ID هنا يضيف عمودًا زائداً بلا قيمة.
**ملاحظة:** `IsAdmin` ليست معلومة مستقلة — هي دائمًا `Role = 'System Administrator'`. نحتفظ بها للسرعة في التحقق.
**تخزين الصورة:** `VARBINARY(MAX)` يخزّن الصورة مباشرة في قاعدة البيانات — مناسب لمشروع جامعي.

---

#### 2. جدول Categories

```sql
CREATE TABLE Categories (
    CategoryName VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Categories PRIMARY KEY (CategoryName)
);
```

**قرار المفتاح:** `CategoryName` — طبيعي
**المبرر:** اسم التصنيف فريد ومستقر ومعبّر. في جميع الفورمز يُعرض الاسم وليس ID. استخدام ID زائد.

---

#### 3. جدول CategoryFilters (بدل CategoryTemplate.AvailableFilters)

```sql
CREATE TABLE CategoryFilters (
    CategoryName VARCHAR(100) NOT NULL,
    FilterName   VARCHAR(50)  NOT NULL,
    CONSTRAINT PK_CategoryFilters PRIMARY KEY (CategoryName, FilterName),
    CONSTRAINT FK_CatFilters_Category FOREIGN KEY (CategoryName) 
        REFERENCES Categories(CategoryName) ON DELETE CASCADE
);
```

**قرار المفتاح:** Composite (CategoryName, FilterName)
**المبرر:** مفتاح طبيعي ثنائي — تصنيف + اسم الخاصية. لا معنى لـ ID هنا.

---

#### 4. جدول CategoryFilterOptions (خيارات كل خاصية)

```sql
CREATE TABLE CategoryFilterOptions (
    CategoryName VARCHAR(100)  NOT NULL,
    FilterName   VARCHAR(50)   NOT NULL,
    OptionValue  NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_CategoryFilterOptions 
        PRIMARY KEY (CategoryName, FilterName, OptionValue),
    CONSTRAINT FK_CatFilterOpts_Filter 
        FOREIGN KEY (CategoryName, FilterName) 
        REFERENCES CategoryFilters(CategoryName, FilterName) ON DELETE CASCADE
);
```

**قرار المفتاح:** Composite ثلاثي
**مثال:** (Laptops, RAM, 16GB) | (Laptops, RAM, 32GB)

---

#### 5. جدول Suppliers (مبسّط حسب متطلبات الدكتورة)

```sql
CREATE TABLE Suppliers (
    SupplierName VARCHAR(150) NOT NULL,
    Phone        VARCHAR(20)  NULL,
    Email        VARCHAR(100) NULL,
    IsActive     BIT          NOT NULL DEFAULT 1,
    CONSTRAINT PK_Suppliers PRIMARY KEY (SupplierName)
);
```

**قرار المفتاح:** `SupplierName` — طبيعي
**المبرر:** الاسم فريد (الكود يتحقق منه). في نظام مخزون، الوحدة المتعاملة هي "الشركة" وليس شخصاً بعينه.
**ما تم حذفه:** `ContactPerson` — هذا حقل CRM وليس مخزوناً. شرح للدكتورة: نظام المخزون يهتم بمن يورّد الصنف، لا بمن يرد على الهاتف.

---

#### 6. جدول SupplierCategories (علاقة M:N)

```sql
CREATE TABLE SupplierCategories (
    SupplierName VARCHAR(150) NOT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    CONSTRAINT PK_SupplierCategories 
        PRIMARY KEY (SupplierName, CategoryName),
    CONSTRAINT FK_SC_Supplier 
        FOREIGN KEY (SupplierName) REFERENCES Suppliers(SupplierName) ON DELETE CASCADE,
    CONSTRAINT FK_SC_Category 
        FOREIGN KEY (CategoryName) REFERENCES Categories(CategoryName) ON DELETE CASCADE
);
```

**المبرر:** يخبرنا ما التصنيفات التي يتخصص فيها المورد — مفيد لفلترة الموردين عند عملية StockIn.

---

#### 7. جدول StorageZones

```sql
CREATE TABLE StorageZones (
    ZoneName     VARCHAR(100) NOT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    CONSTRAINT PK_StorageZones PRIMARY KEY (ZoneName),
    CONSTRAINT FK_Zone_Category 
        FOREIGN KEY (CategoryName) REFERENCES Categories(CategoryName)
);
```

**قرار المفتاح:** `ZoneName` — طبيعي
**مثال:** "Aisle A-1: Laptops" فريد ومعبّر.

---

#### 8. جدول Products

```sql
CREATE TABLE Products (
    SerialNumber VARCHAR(50)    NOT NULL,
    ProductName  NVARCHAR(200)  NOT NULL,
    CategoryName VARCHAR(100)   NOT NULL,
    Price        DECIMAL(10, 2) NOT NULL,
    CONSTRAINT PK_Products PRIMARY KEY (SerialNumber),
    CONSTRAINT FK_Product_Category 
        FOREIGN KEY (CategoryName) REFERENCES Categories(CategoryName)
);
```

**قرار المفتاح:** `SerialNumber` — طبيعي
**المبرر:** رقم تسلسلي للمنتج كـ `APP-MBP-2023` هو هوية المنتج في أي نظام مخزون حقيقي.
**ملاحظة هامة:** لا يوجد عمود `Quantity` هنا — تُحسب من جدول ProductItems عبر الـ View.

---

#### 9. جدول ProductSpecifications (بدل Dictionary في Product)

```sql
CREATE TABLE ProductSpecifications (
    ProductSerial VARCHAR(50)    NOT NULL,
    SpecKey       VARCHAR(50)    NOT NULL,
    SpecValue     NVARCHAR(100)  NOT NULL,
    CONSTRAINT PK_ProductSpecs 
        PRIMARY KEY (ProductSerial, SpecKey),
    CONSTRAINT FK_Specs_Product 
        FOREIGN KEY (ProductSerial) REFERENCES Products(SerialNumber) ON DELETE CASCADE
);
```

**مثال:** (APP-MBP-2023, Processor, Apple M3) | (APP-MBP-2023, RAM, 16GB)
**المبرر:** تحويل الـ Dictionary إلى شكل جدولي منظّم — هذا هو الشكل الصحيح أكاديمياً.

---

#### 10. جدول StockMovements

```sql
CREATE TABLE StockMovements (
    MovementId      INT           NOT NULL IDENTITY(1,1),
    ProductSerial   VARCHAR(50)   NOT NULL,
    MovementType    VARCHAR(20)   NOT NULL 
        CHECK (MovementType IN ('StockIn','StockOut','Restock','ReturnToSupplier')),
    QuantityChanged INT           NOT NULL,
    MovementDate    DATETIME      NOT NULL DEFAULT GETDATE(),
    Username        VARCHAR(50)   NULL,
    Notes           NVARCHAR(500) NULL,
    WarrantyMonths  INT           NULL,
    SupplierName    VARCHAR(150)  NULL,
    CONSTRAINT PK_StockMovements PRIMARY KEY (MovementId),
    CONSTRAINT FK_SM_Product  FOREIGN KEY (ProductSerial) REFERENCES Products(SerialNumber),
    CONSTRAINT FK_SM_User     FOREIGN KEY (Username) REFERENCES Users(Username) ON DELETE SET NULL,
    CONSTRAINT FK_SM_Supplier FOREIGN KEY (SupplierName) REFERENCES Suppliers(SupplierName) ON DELETE SET NULL
);
```

**قرار المفتاح:** `IDENTITY` — مبرر
**المبرر:** سجل معاملات (Transaction Log). لا يوجد مفتاح طبيعي — نفس المنتج يمكن أن يُدخل مئات المرات في أي وقت. هذا أحد الحالتين اللتين يُبرَّر فيهما IDENTITY بوضوح.

---

#### 11. جدول ProductItems

```sql
CREATE TABLE ProductItems (
    ItemSerialNumber VARCHAR(80) NOT NULL,
    ProductSerial    VARCHAR(50) NOT NULL,
    IsInStock        BIT         NOT NULL DEFAULT 1,
    DateAdded        DATETIME    NOT NULL DEFAULT GETDATE(),
    DateRemoved      DATETIME    NULL,
    BatchMovementId  INT         NULL,
    CONSTRAINT PK_ProductItems PRIMARY KEY (ItemSerialNumber),
    CONSTRAINT FK_PI_Product  FOREIGN KEY (ProductSerial) REFERENCES Products(SerialNumber),
    CONSTRAINT FK_PI_Movement FOREIGN KEY (BatchMovementId) REFERENCES StockMovements(MovementId) ON DELETE SET NULL
);
```

**قرار المفتاح:** `ItemSerialNumber` — طبيعي
**مثال:** `APP-MBP-2023-01`, `APP-MBP-2023-02` — فريد عالمياً بالتعريف.
**البُعد الأكاديمي:** ربط الوحدة الفردية بـ BatchMovementId يمكّننا من استخراج الضمان الخاص بكل وحدة بناءً على دفعة الشراء.

---

#### 12. جدول AuditLog

```sql
CREATE TABLE AuditLog (
    LogId        INT           NOT NULL IDENTITY(1,1),
    LogTimestamp DATETIME      NOT NULL DEFAULT GETDATE(),
    ActionType   VARCHAR(50)   NOT NULL,
    Description  NVARCHAR(500) NOT NULL,
    Username     VARCHAR(50)   NOT NULL,
    CONSTRAINT PK_AuditLog PRIMARY KEY (LogId)
);
```

**قرار المفتاح:** `IDENTITY` — مبرر
**المبرر:** سجل أحداث. نفس المستخدم يمكن أن يقوم بنفس الإجراء مرات متعددة في الثانية. لا مفتاح طبيعي.

---

### ملخص قرارات المفاتيح للدكتورة

| الجدول | نوع المفتاح | القيمة | المبرر |
|--------|------------|--------|--------|
| Users | طبيعي | Username | فريد دائماً، يُستخدم كمرجع في كل مكان |
| Categories | طبيعي | CategoryName | فريد ومستقر |
| CategoryFilters | Composite طبيعي | (CategoryName, FilterName) | علاقة تبعية |
| CategoryFilterOptions | Composite طبيعي | (CategoryName, FilterName, OptionValue) | كل خيار فريد ضمن خاصيته |
| Suppliers | طبيعي | SupplierName | فريد، الكود يتحقق منه |
| SupplierCategories | Composite طبيعي | (SupplierName, CategoryName) | جدول ربط M:N |
| StorageZones | طبيعي | ZoneName | فريد ومعبّر |
| Products | طبيعي | SerialNumber | هوية المنتج الفعلية |
| ProductSpecifications | Composite طبيعي | (ProductSerial, SpecKey) | خاصية واحدة لكل منتج |
| **StockMovements** | **IDENTITY** | **MovementId** | **سجل معاملات — لا مفتاح طبيعي** |
| ProductItems | طبيعي | ItemSerialNumber | فريد عالمياً |
| **AuditLog** | **IDENTITY** | **LogId** | **سجل أحداث — لا مفتاح طبيعي** |

---

## الجزء الثالث: مخطط قاعدة البيانات (ERD نصي)

```
Users ──────────────────────────────────────────────┐
  │ Username (PK)                                   │
  │                                                 │
  ├─→ StockMovements.Username                       │
  └─→ AuditLog.Username                            │
                                                    │
Categories                                          │
  │ CategoryName (PK)                               │
  │                                                 │
  ├─→ CategoryFilters.CategoryName                  │
  ├─→ SupplierCategories.CategoryName               │
  ├─→ StorageZones.CategoryName                     │
  └─→ Products.CategoryName                         │
                                                    │
Suppliers                                           │
  │ SupplierName (PK)                               │
  │                                                 │
  ├─→ SupplierCategories.SupplierName               │
  └─→ StockMovements.SupplierName                   │
                                                    │
Products                                            │
  │ SerialNumber (PK)                               │
  │                                                 │
  ├─→ ProductSpecifications.ProductSerial           │
  ├─→ StockMovements.ProductSerial                  │
  └─→ ProductItems.ProductSerial                    │
                                                    │
StockMovements                                      │
  │ MovementId (IDENTITY PK)  ◄───────────────────── │
  │                                                 │
  └─→ ProductItems.BatchMovementId                  │
```

---

## الجزء الرابع: معمارية التطبيق (من الصفر)

```
InventoryManagementSystem/
│
├── Database/
│   ├── CreateDatabase.sql       ← سكريبت إنشاء كل الجداول
│   ├── Views.sql                ← vw_ProductStock وغيرها
│   └── SeedData.sql             ← البيانات الأولية
│
├── DAL/
│   ├── DatabaseHelper.cs        ← Connection + ExecuteNonQuery + ExecuteReader
│   ├── UserRepository.cs
│   ├── CategoryRepository.cs
│   ├── SupplierRepository.cs
│   ├── ProductRepository.cs
│   ├── ProductItemRepository.cs
│   ├── StockMovementRepository.cs
│   ├── StorageZoneRepository.cs
│   └── AuditLogRepository.cs
│
├── Models/                      ← نماذج بيانات نظيفة (بدون منطق)
│   ├── User.cs
│   ├── Category.cs
│   ├── Supplier.cs
│   ├── Product.cs               ← يحتوي على Quantity و Status كـ properties
│   ├── ProductItem.cs
│   ├── StockMovement.cs
│   ├── StorageZone.cs
│   └── AuditLog.cs
│
├── Forms/                       ← موجودة ← تُعدَّل فقط
│   ├── FrmLogin.cs
│   ├── FrmMain.cs
│   ├── FrmDashboard.cs
│   ├── FrmProducts.cs
│   ├── FrmAddProduct.cs
│   ├── FrmStockIn.cs
│   ├── FrmStockOut.cs
│   ├── FrmSupplierManagement.cs ← يُبسَّط
│   ├── FrmUsers.cs
│   ├── FrmAuditLog.cs
│   └── FrmReports.cs            ← يُفعَّل بـ Crystal Reports
│
└── Reports/                     ← ملفات Crystal Reports
    ├── rpt_InventoryList.rpt
    ├── rpt_StockMovements.rpt
    ├── rpt_LowStock.rpt
    └── rpt_AuditLog.rpt
```

---

## الجزء الخامس: خارطة الطريق التنفيذية

### المرحلة 1 — قاعدة البيانات (يوم 1)

- [ ] إنشاء مجلد `Database/` في المشروع
- [ ] كتابة `CreateDatabase.sql` بكل الجداول الموضحة أعلاه
- [ ] كتابة `Views.sql` (vw_ProductStock على الأقل)
- [ ] كتابة `SeedData.sql` ببيانات أولية (5 تصنيفات، 5 منتجات، مستخدم admin)
- [ ] تشغيل السكريبتين على SSMS أو localdb عبر Package Manager Console
- [ ] التحقق: كل الجداول موجودة، والـ View ترجع نتائج

### المرحلة 2 — Models + DAL (يوم 2)

**الخطوة 1: Models نظيفة**
- [ ] إنشاء مجلد `Models/`
- [ ] كتابة كل Model كـ POCO (Plain C# class) بدون أي منطق

**الخطوة 2: DatabaseHelper**
- [ ] إنشاء `DAL/DatabaseHelper.cs` مع Connection String
- [ ] اختبار الاتصال

**الخطوة 3: Repositories (من الأبسط للأعقد)**
- [ ] `CategoryRepository` (GetAll, Add, Delete)
- [ ] `UserRepository` (GetAll, GetByUsername, Add, Update, Delete, Authenticate)
- [ ] `SupplierRepository` (GetAll, Add, Update, Delete, GetByCategory)
- [ ] `StorageZoneRepository` (GetAll, GetByCategory)
- [ ] `ProductRepository` (GetAll من vw_ProductStock, Add, Update, Delete)
- [ ] `StockMovementRepository` (GetRecent, Add, GetByProduct)
- [ ] `ProductItemRepository` (GetAvailable, AddBatch, MarkAsRemoved)
- [ ] `AuditLogRepository` (GetAll, Add)

### المرحلة 3 — تعديل الفورمز (يوم 3-4)

**الأولوية:**
1. `FrmLogin` — Authentication من DB
2. `FrmUsers` — CRUD من DB
3. `FrmAddProduct` + `FrmProducts` — عرض وإضافة المنتجات
4. `FrmStockIn` + `FrmStockOut` — حركات المخزون
5. `FrmSupplierManagement` — مبسّط
6. `FrmAuditLog` — عرض السجلات
7. `FrmDashboard` — أرقام حقيقية من DB

**مبدأ التعديل لكل فورم:**
- احذف كل `MemoryStore.XXX`
- استبدلها بـ `XXXRepository.GetAll()` أو `.Save()` إلخ
- الـ Session تُدار بـ static property بسيطة (مستخدم حالي)

### المرحلة 4 — Crystal Reports (يوم 5)

**تقارير مقترحة (4 كافية للمتطلبات):**

| # | اسم التقرير | البيانات من |
|---|-------------|------------|
| 1 | قائمة المخزون الكاملة | vw_ProductStock |
| 2 | تقرير حركات المخزون | StockMovements + Products |
| 3 | منتجات منخفضة المخزون | vw_ProductStock WHERE Quantity <= 10 |
| 4 | سجل نشاط النظام | AuditLog |

**خطوات التنفيذ:**
1. تثبيت SAP Crystal Reports for Visual Studio (مجاني)
2. إضافة ReportViewer إلى FrmReports
3. إنشاء DataSet موصول بـ SQL Server لكل تقرير
4. تصميم الـ `.rpt` file: ترويسة + جدول بيانات + تذييل بالتاريخ
5. تشغيل التقرير بزر من FrmReports

---

## الجزء السادس: نقاط للنقاش مع الدكتورة

### نقطة 1: لماذا حذفنا ContactPerson؟
في نظام المخزون، الطرف المتعاقد هو الشركة المورّدة، ليس موظفاً بعينه. بيانات التواصل الشخصي تنتمي لنظام CRM. هنا نهتم فقط بـ: من يورّد؟ وهل هو نشط؟

### نقطة 2: لماذا لا يوجد Quantity في جدول Products؟
`Quantity` هي قيمة مشتقة من `ProductItems` (عدد الوحدات التي `IsInStock = 1`). تخزينها يخلق redundancy ويخاطر بعدم الاتساق. نحسبها بـ View — هذا هو التصميم الصحيح للـ 3NF.

### نقطة 3: لماذا IDENTITY في جدولين فقط؟
`StockMovements` و `AuditLog` كلاهما سجلات تاريخية لا تملك هوية طبيعية — نفس المنتج يمكن أن يُدخل مرتين في الثانية الواحدة. في جميع الجداول الأخرى استخدمنا مفتاحاً طبيعياً مبرراً.

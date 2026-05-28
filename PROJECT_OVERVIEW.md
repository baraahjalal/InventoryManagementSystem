# نظام إدارة المخزون — نظرة عامة على المشروع

> **الهدف من هذا الملف:** تقديم سياق كامل للمشروع لمساعدة AI في تحديد احتياجات التوثيق، خصوصاً مرحلة الاختبار (Testing).

---

## 1. ملخص المشروع

| البند | التفاصيل |
|-------|-----------|
| **النوع** | تطبيق سطح مكتب (WinForms) |
| **اللغة** | C# .NET Framework 4.8 |
| **قاعدة البيانات** | SQL Server — اسم القاعدة: `InventoryDBv3` |
| **التقارير** | Crystal Reports 13.0 |
| **الهدف الأكاديمي** | مشروع مادة Advanced C# |
| **الحالة الحالية** | اكتمال الفيتشرات الأساسية — لا توجد اختبارات |

---

## 2. الوحدات الرئيسية

### A. طبقة البيانات (DAL — 11 Repository)

| Repository | المسؤولية |
|------------|-----------|
| `UserRepository` | تسجيل الدخول، إدارة المستخدمين والأدوار |
| `CategoryRepository` | إدارة التصنيفات وربطها بالمواصفات |
| `SupplierRepository` | إدارة الموردين (المفتاح الرئيسي = رقم ضريبي) |
| `ProductRepository` | عمليات المنتجات عبر `vw_ProductStock` View |
| `ProductItemRepository` | إدارة وحدات المخزون الفردية (Batch Add / Mark Removed) |
| `StorageZoneRepository` | مناطق التخزين المرتبطة بالتصنيفات |
| `StockMovementRepository` | تسجيل جميع حركات المخزون (دخول/خروج/إرجاع) |
| `CategorySpecTemplateRepository` | قوالب مواصفات ديناميكية لكل تصنيف |
| `AuditLogRepository` | قراءة سجل التدقيق فقط (الكتابة عبر Triggers) |
| `DataMaintenanceRepository` | تنفيذ سياسة الاحتفاظ بالبيانات وحذف القديم |
| `DatabaseHelper` | إدارة Connection String + جلسة المستخدم الحالي |

### B. النماذج (Forms — 20 نموذج)

**تدفق التطبيق:**
```
FrmLogin → FrmMain (MDI)
  ├── FrmDashboard       (الصفحة الرئيسية)
  ├── FrmProducts        (عرض المنتجات)
  │   ├── FrmAddProduct  (إضافة منتج)
  │   ├── FrmStockIn     (إدخال مخزون)
  │   └── FrmStockOut    (إخراج مخزون)
  ├── FrmAddCategory     (إضافة تصنيف + مناطق + قوالب)
  ├── FrmSupplierManagement
  ├── FrmUsers
  ├── FrmReports → FrmReportViewer
  ├── FrmAuditLog
  └── FrmDataMaintenance
```

### C. قاعدة البيانات (9 جداول)

```
Users, Categories, Suppliers, Products, ProductItems,
ProductSpecifications, StorageZones, StockMovements,
AuditLog, CategorySpecTemplates
```

- **View:** `vw_ProductStock` — يجمع الكميات من ProductItems
- **ItemID:** `ProductSerialNumber` + لاحقة (2 أو 3 أرقام) — مثال: `10050101`؛ عند الإخراج يُعاد استخدام الرقم عند Stock In
- **Triggers:** تكتب في AuditLog عند أي تغيير في الجداول الرئيسية
- **Stored Proc:** `sp_PurgeOldData` — حذف بيانات قديمة حسب سياسة الاحتفاظ

### D. التقارير (Crystal Reports — 3 تقارير فعّالة)

| التقرير | المحتوى |
|---------|---------|
| `StockInventoryStatus` | حالة المخزون الحالية لجميع المنتجات |
| `StockMovementHistory` | سجل حركات الدخول والخروج |


---

## 3. القرارات المعمارية الرئيسية

1. **لا Business Logic Layer:** النماذج (Forms) تتصل مباشرة بالـ Repositories — لا توجد طبقة وسيطة.
2. **Primary Keys أعداد صحيحة:** تم تحويل جميع المفاتيح من String إلى int في مرحلة الهجرة إلى InventoryDBv3.
3. **Session عبر Static Property:** `DatabaseHelper.CurrentUser` يحمل بيانات المستخدم الحالي طوال الجلسة.
4. **AuditLog مقفول:** يُكتب فقط عبر Database Triggers، الـ Repository للقراءة فقط.
5. **Crystal Reports Pull Model:** التقارير تقرأ مباشرة من SQL Server عبر OLE DB.
6. **مواصفات ديناميكية:** مواصفات المنتج تُولَّد من قوالب مرتبطة بالتصنيف.

---

## 4. ما يوجد حالياً من توثيق

| الملف | المحتوى |
|-------|---------|
| `README.md` | فارغ تقريباً |
| `MIGRATION_CONTEXT.md` | سياق هجرة قاعدة البيانات إلى v3 |
| `CHAT_CONTEXT_REPORTS_PHASE.md` | سياق مرحلة Crystal Reports |
| `docs/PROJECT_ROADMAP.md` | خارطة طريق أكاديمية شاملة |

---

## 5. حالة الاختبار (Testing) — الفجوة الكبرى

| الجانب | الحالة |
|--------|--------|
| **مشروع اختبار منفصل** | ❌ غير موجود |
| **Unit Tests** | ❌ صفر |
| **Integration Tests** | ❌ صفر |
| **UI Tests** | ❌ صفر |
| **اختبار يدوي موثّق** | ❌ غير موثّق |
| **Test Cases للـ Validation** | ❌ غير موجودة |

### الوحدات التي تحتاج اختبارات بشكل عاجل:

1. **UserRepository** — منطق المصادقة وصلاحيات الأدوار
2. **StockMovementRepository** — صحة تسجيل الحركات ومنطق الكميات
3. **ProductItemRepository** — منطق Batch Add وتوليد الـ ItemID
4. **DataMaintenanceRepository** — منطق الحذف حسب التاريخ (خطر!)
5. **ValidationHelper** — جميع قواعد التحقق من المدخلات
6. **DatabaseHelper** — إدارة الاتصال وجلسة المستخدم
7. **FrmLogin** — سيناريوهات تسجيل الدخول (صحيح/خاطئ/مقفول)
8. **FrmStockIn / FrmStockOut** — منطق العمليات الحرجة على المخزون

---

## 6. احتياجات التوثيق المقترحة

بناءً على حالة المشروع، هذه هي الفجوات التوثيقية المقترحة للمعالجة:

### أولوية عالية
- [ ] **دليل اختبار يدوي** — خطوات اختبار كل سيناريو رئيسي
- [ ] **Test Cases موثّقة** — للـ Validation و Business Rules
- [ ] **وصف Architecture** — شرح تدفق البيانات بين الطبقات

### أولوية متوسطة
- [ ] **دليل إعداد قاعدة البيانات** — خطوات نشر `InventoryDBv3`
- [ ] **دليل المستخدم** — شرح الواجهات للمستخدم النهائي
- [ ] **API Reference للـ Repositories** — توثيق الدوال والمعاملات

### أولوية منخفضة
- [ ] **توثيق التقارير** — كيف تُولَّد وتُعدَّل تقارير Crystal
- [ ] **دليل النشر** — خطوات نشر التطبيق على أجهزة جديدة

---

## 7. المعلومات التقنية للبيئة

```
Server:   BARAAH-PC
Database: InventoryDBv3
Auth:     Windows Integrated Security
Framework: .NET Framework 4.8
IDE:      Visual Studio (WinForms)
Branch:   InventoryDBv3-migration
```

---

*تاريخ إنشاء هذا الملف: 2026-05-28*

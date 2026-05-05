using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private bool isExpanded = false; // متغير لتتبع حالة الشريط الجانبي (موسع أم لا)
        private Size originalNavSize; // حجم أيقونة التنقل الأصلي
        private Point originalNavLoc; // موقع أيقونة التنقل الأصلي
        private const int MinWidth = 70; // الحد الأدنى لعرض الشريط الجانبي (عندما يكون منكمشاً)
        private const int MaxWidth = 235; // الحد الأقصى لعرض الشريط الجانبي (عندما يكون موسعاً)
        private const int AnimSpeed = 20; // سرعة حركة التوسع والانكماش

        // Changed from private to public so child forms can request navigation
        public void OpenChildForm(Form childForm)
        {
            // إذا كان فيه فورم مفتوح مسبقاً داخل البانل، نقوم بإغلاقه
            if (pnlMainContent.Controls.Count > 0)
                pnlMainContent.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlMainContent.Controls.Add(childForm);
            pnlMainContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Dashboard is available to all logged in users
            OpenChildForm(new FrmDashboard());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            // Products available to all employees
            OpenChildForm(new FrmProducts());
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {

            if (isExpanded)
            {
                pnlSideBar.Width -= AnimSpeed; // تقليص العرض
                if (pnlSideBar.Width <= MinWidth) // التحقق من الوصول للحد الأدنى
                {
                    pnlSideBar.Width = MinWidth; // تثبيت العرض
                    isExpanded = false; // تحديث الحالة
                    sidebarTimer.Stop(); // إيقاف المؤقت
                }
            }
            else
            {
                pnlSideBar.Width += AnimSpeed; // توسيع العرض
                if (pnlSideBar.Width >= MaxWidth) // التحقق من الوصول للحد الأقصى
                {
                    pnlSideBar.Width = MaxWidth; // تثبيت العرض
                    isExpanded = true; // تحديث الحالة
                    sidebarTimer.Stop(); // إيقاف المؤقت
                }
            }
        }

        private void picNav_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start(); // بدء المؤقت لتحريك الشريط الجانبي
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmStockIn());
        }

        private void btnStockOut_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmStockOut());
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            var user = MemoryStore.CurrentUser;
            if (user == null || !user.IsAdmin)
            {
                MessageBox.Show("You do not have permission to access this feature.", "Permission Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenChildForm(new FrmUsers());
        }

        private void btnSuppliersManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmSupplierManagement());
        }

        private void btnAuditLog_Click(object sender, EventArgs e)
        {
            var user = MemoryStore.CurrentUser;
            if (user == null || !user.IsAdmin)
            {
                MessageBox.Show("You do not have permission to access this feature.", "Permission Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenChildForm(new FrmAuditLog());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmReports());
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ApplyUserPermissions();
            
            // Optionally open the Dashboard by default if a user is logged in
            if (MemoryStore.CurrentUser != null)
            {
                btnDashboard_Click(this, EventArgs.Empty);
            }
        }

        private void ApplyUserPermissions()
        {
            // All buttons remain visible, authorization check is done on click
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // استدعاء الفورم المخصص كـ Dialog
                using (var exitDialog = new Forms.FrmExitDialog())
                {
                    exitDialog.ShowDialog(this);

                    if (exitDialog.SelectedAction == Forms.ExitAction.Cancel)
                    {
                        // إذا تم اختيار إلغاء نمنع إغلاق الفورم
                        e.Cancel = true;
                    }
                    else if (exitDialog.SelectedAction == Forms.ExitAction.LogOut)
                    {
                        // في حالة تسجيل الخروج، نسمح بإغلاق الفورم الحالي (الـ Main) 
                        // ولكننا نقوم بعمل إعادة تشغيل للتطبيق لضمان تنظيف كافة الـ MemoryStore 
                        // وظهور شاشة تسجيل الدخول من جديد بشكل نظيف
                        e.Cancel = true; // نلغي الإغلاق العادي
                        Application.Restart();
                    }
                    else if (exitDialog.SelectedAction == Forms.ExitAction.CloseSystem)
                    {
                        // إغلاق النظام بالكامل
                        Application.Exit();
                    }
                }
            }
        }
    }
}

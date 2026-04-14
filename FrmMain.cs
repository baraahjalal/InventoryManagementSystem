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

        private void OpenChildForm(Form childForm)
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
            OpenChildForm(new FrmDashboard());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
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
            OpenChildForm(new FrmUsers());
        }
    }
}

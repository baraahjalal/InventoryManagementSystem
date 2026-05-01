using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public enum ExitAction
    {
        LogOut,
        CloseSystem,
        Cancel
    }

    public partial class FrmExitDialog : Form
    {
        public ExitAction SelectedAction { get; private set; } = ExitAction.Cancel;

        public FrmExitDialog()
        {
            InitializeComponent();

            // إضافة حواف دائرية أنيقة للفورم (Optional)
            this.Paint += FrmExitDialog_Paint;
        }

        private void FrmExitDialog_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.FromArgb(200, 200, 200), ButtonBorderStyle.Solid);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SelectedAction = ExitAction.LogOut;
            this.Close();
        }

        private void btnCloseSystem_Click(object sender, EventArgs e)
        {
            SelectedAction = ExitAction.CloseSystem;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedAction = ExitAction.Cancel;
            this.Close();
        }
    }
}
using System.Windows.Forms;
using System.Drawing;

namespace InventoryManagementSystem
{
    partial class FrmLogin : Form
    {
        private System.ComponentModel.IContainer components = null;

        // Core Components
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;

        // Inputs
        private System.Windows.Forms.Label lblUserNameTitle;
        private System.Windows.Forms.ComboBox cmbUserName;
        private System.Windows.Forms.Label lblPasswordTitle;
        private System.Windows.Forms.TextBox txtAccessKey;

        // Actions
        private System.Windows.Forms.Button btnAuthenticate;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblUserNameTitle = new System.Windows.Forms.Label();
            this.cmbUserName = new System.Windows.Forms.ComboBox();
            this.lblPasswordTitle = new System.Windows.Forms.Label();
            this.txtAccessKey = new System.Windows.Forms.TextBox();
            this.btnAuthenticate = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Controls.Add(this.lblSubTitle);
            this.pnlCard.Controls.Add(this.lblUserNameTitle);
            this.pnlCard.Controls.Add(this.cmbUserName);
            this.pnlCard.Controls.Add(this.lblPasswordTitle);
            this.pnlCard.Controls.Add(this.txtAccessKey);
            this.pnlCard.Controls.Add(this.btnAuthenticate);
            this.pnlCard.Controls.Add(this.picLogo);
            this.pnlCard.Location = new System.Drawing.Point(0, 0);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(434, 611);
            this.pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 130);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(434, 45);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Welcome Back";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSubTitle.Location = new System.Drawing.Point(0, 175);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(434, 25);
            this.lblSubTitle.TabIndex = 2;
            this.lblSubTitle.Text = "Please sign in to your account";
            this.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserNameTitle
            // 
            this.lblUserNameTitle.AutoSize = true;
            this.lblUserNameTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUserNameTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUserNameTitle.Location = new System.Drawing.Point(50, 260);
            this.lblUserNameTitle.Name = "lblUserNameTitle";
            this.lblUserNameTitle.Size = new System.Drawing.Size(72, 17);
            this.lblUserNameTitle.TabIndex = 9;
            this.lblUserNameTitle.Text = "Username:";
            // 
            // cmbUserName
            // 
            this.cmbUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUserName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbUserName.FormattingEnabled = true;
            this.cmbUserName.Location = new System.Drawing.Point(50, 283);
            this.cmbUserName.Name = "cmbUserName";
            this.cmbUserName.Size = new System.Drawing.Size(330, 28);
            this.cmbUserName.TabIndex = 10;
            // 
            // lblPasswordTitle
            // 
            this.lblPasswordTitle.AutoSize = true;
            this.lblPasswordTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPasswordTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPasswordTitle.Location = new System.Drawing.Point(50, 330);
            this.lblPasswordTitle.Name = "lblPasswordTitle";
            this.lblPasswordTitle.Size = new System.Drawing.Size(69, 17);
            this.lblPasswordTitle.TabIndex = 8;
            this.lblPasswordTitle.Text = "Password:";
            // 
            // txtAccessKey
            // 
            this.txtAccessKey.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtAccessKey.Location = new System.Drawing.Point(50, 353);
            this.txtAccessKey.Name = "txtAccessKey";
            this.txtAccessKey.Size = new System.Drawing.Size(330, 29);
            this.txtAccessKey.TabIndex = 6;
            this.txtAccessKey.UseSystemPasswordChar = true;
            // 
            // btnAuthenticate
            // 
            this.btnAuthenticate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnAuthenticate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAuthenticate.FlatAppearance.BorderSize = 0;
            this.btnAuthenticate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.btnAuthenticate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(190)))));
            this.btnAuthenticate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuthenticate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAuthenticate.ForeColor = System.Drawing.Color.White;
            this.btnAuthenticate.Location = new System.Drawing.Point(50, 425);
            this.btnAuthenticate.Name = "btnAuthenticate";
            this.btnAuthenticate.Size = new System.Drawing.Size(330, 45);
            this.btnAuthenticate.TabIndex = 7;
            this.btnAuthenticate.Text = "Sign In";
            this.btnAuthenticate.UseVisualStyleBackColor = false;
            this.btnAuthenticate.Click += new System.EventHandler(this.btnAuthenticate_Click);
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picLogo.BackgroundImage")));
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picLogo.Location = new System.Drawing.Point(102, -16);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(221, 181);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.btnAuthenticate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(434, 611);
            this.Controls.Add(this.pnlCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Login";
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
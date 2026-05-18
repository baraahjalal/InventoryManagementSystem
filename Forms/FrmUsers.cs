using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem
{
    public partial class FrmUsers : Form
    {
        private string _selectedUsername = string.Empty;
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FrmUsers()
        {
            InitializeComponent();
            
            txtUserPassword.ReadOnly = false;

            cmbRole.Items.Clear();
            cmbRole.Items.Add("System Administrator");
            cmbRole.Items.Add("Employee");

            this.Load += FrmUsers_Load;
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnSetPic.Click += BtnSetPic_Click;
            btnResetPass.Click += BtnResetPass_Click;
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;
            
            if (toolStripMenuItemDelete != null)
                toolStripMenuItemDelete.Click += ToolStripMenuItemDelete_Click;
        }

        private void FrmUsers_Load(object sender, EventArgs e)
        {
            if (DatabaseHelper.CurrentUser != null && !DatabaseHelper.CurrentUser.IsAdmin)
            {
                MessageBox.Show("Unauthorized Access. Only System Administrators can manage users.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pnlDetails.Enabled = false;
                pnlActionButtons.Enabled = false;
                this.BeginInvoke(new Action(this.Close));
                return;
            }
            LoadGridData();
        }

        private void LoadGridData()
        {
            dgvUsers.Rows.Clear();
            int no = 1;
            
            var users = UserRepository.GetAll();
            foreach (var user in users)
            {
                Image pic = ByteArrayToImage(user.ProfilePhoto);
                dgvUsers.Rows.Add(
                    user.Username,
                    no.ToString(),
                    user.Username,
                    "********", 
                    user.Role,
                    user.Username, // regNum removed, just show username
                    pic
                );
                no++;
            }
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedUsername = string.Empty;
            txtUserName.Clear();
            txtUserPassword.Clear();
            cmbRole.SelectedIndex = -1;
            picUser.Image = null;
            dgvUsers.ClearSelection();
        }

        private void BtnResetPass_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void DgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var row = dgvUsers.SelectedRows[0];
                if (row.Cells[0].Value != null)
                {
                    _selectedUsername = row.Cells[0].Value.ToString();
                    var users = UserRepository.GetAll();
                    var user = users.FirstOrDefault(u => u.Username == _selectedUsername);
                    
                    if (user != null)
                    {
                        txtUserName.Text = user.Username;
                        txtUserPassword.Text = user.Password; 
                        cmbRole.SelectedItem = user.Role;
                        picUser.Image = ByteArrayToImage(user.ProfilePhoto);
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string username = txtUserName.Text.Trim();
            
            if (UserRepository.Exists(username))
            {
                MessageBox.Show("Username already exists. Please choose a different one.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = cmbRole.SelectedItem.ToString();
            
            var newUser = new User
            {
                Username = username,
                Password = txtUserPassword.Text,
                Role = role,
                IsAdmin = (role == "System Administrator"),
                ProfilePhoto = ImageToByteArray(picUser.Image)
            };

            UserRepository.Add(newUser);
            
            MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadGridData();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedUsername))
            {
                MessageBox.Show("Please select a user to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            string newUsername = txtUserName.Text.Trim();

            if (newUsername != _selectedUsername && UserRepository.Exists(newUsername))
            {
                MessageBox.Show("Username already exists. Please choose a different one.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var users = UserRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Username == _selectedUsername);
            if (user == null) return;

            string oldRole = user.Role;
            string newRole = cmbRole.SelectedItem.ToString();

            if (DatabaseHelper.CurrentUser != null && DatabaseHelper.CurrentUser.Username == user.Username && oldRole == "System Administrator" && newRole != "System Administrator")
            {
                MessageBox.Show("You cannot remove your own System Administrator privileges.", "Operation Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            user.Username = newUsername;
            user.Password = txtUserPassword.Text;
            user.Role = newRole;
            user.IsAdmin = (newRole == "System Administrator");
            user.ProfilePhoto = ImageToByteArray(picUser.Image);

            if (newUsername != _selectedUsername)
            {
                UserRepository.Add(user);
                UserRepository.Delete(_selectedUsername);
            }
            else
            {
                UserRepository.Update(user);
            }
            
            MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadGridData();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedUsername))
            {
                MessageBox.Show("Please select a user to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DatabaseHelper.CurrentUser != null && _selectedUsername == DatabaseHelper.CurrentUser.Username)
            {
                MessageBox.Show("You cannot delete your own account while logged in.", "Operation Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete user '{_selectedUsername}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                UserRepository.Delete(_selectedUsername);
                
                MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGridData();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        private void BtnSetPic_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Select Profile Picture";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picUser.Image = Image.FromFile(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to load image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            picUser.Image = null;
        }

        private bool ValidateInputs()
        {
            _errorProvider.Clear();
            bool isValid = true;
            string errorMsg;

            if (!ValidationHelper.IsRequired(txtUserName.Text, out errorMsg))
            { _errorProvider.SetError(txtUserName, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(txtUserName.Text.Trim(), 3, 30, out errorMsg))
            { _errorProvider.SetError(txtUserName, errorMsg); isValid = false; }
            else
              _errorProvider.SetError(txtUserName, string.Empty);

            if (!ValidationHelper.IsRequired(txtUserPassword.Text, out errorMsg))
            { _errorProvider.SetError(txtUserPassword, errorMsg); isValid = false; }
            else if (!ValidationHelper.IsValidLength(txtUserPassword.Text, 6, 50, out errorMsg))
            { _errorProvider.SetError(txtUserPassword, errorMsg); isValid = false; }
            else
              _errorProvider.SetError(txtUserPassword, string.Empty);

            if (cmbRole.SelectedIndex == -1)
            { _errorProvider.SetError(cmbRole, "Please select a system role."); isValid = false; }
            else
              _errorProvider.SetError(cmbRole, string.Empty);

            if (!isValid)
                MessageBox.Show("Please correct the highlighted errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return isValid;
        }

        private byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                var format = image.RawFormat;
                // If the format is MemoryBmp, save as PNG to avoid errors
                if (format.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
                {
                    format = System.Drawing.Imaging.ImageFormat.Png;
                }
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        private Image ByteArrayToImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}

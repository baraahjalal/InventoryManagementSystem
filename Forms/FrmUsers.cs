using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmUsers : Form
    {
        private int _selectedUserId = -1;

        public FrmUsers()
        {
            InitializeComponent();
            
            // Allow editing password manually instead of being read-only
            txtUserPassword.ReadOnly = false;

            // Ensure the role combo contains only the simplified roles defined in the SRS
            cmbRole.Items.Clear();
            cmbRole.Items.Add("System Administrator");
            cmbRole.Items.Add("Employee");

            // Attach Event Handlers
            this.Load += FrmUsers_Load;
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnSetPic.Click += BtnSetPic_Click;
            btnResetPass.Click += BtnResetPass_Click;
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;
            
            // Assuming cmsDeletePicture has toolStripMenuItemDelete added in designer
            if (toolStripMenuItemDelete != null)
                toolStripMenuItemDelete.Click += ToolStripMenuItemDelete_Click;
        }

        private void FrmUsers_Load(object sender, EventArgs e)
        {
            // Security Check: Only Admins can manage users according to SRS
            if (MemoryStore.CurrentUser != null && !MemoryStore.CurrentUser.IsAdmin)
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
            
            // Fetch all users from MemoryStore
            foreach (var user in MemoryStore.Users)
            {
                string regNum = $"REG-{user.Id:D3}";
                dgvUsers.Rows.Add(
                    user.Id,
                    no.ToString(),
                    user.Username,
                    "********", // Mask password for security
                    user.Role,
                    regNum,
                    user.ProfilePicture
                );
                no++;
            }

            ClearForm();
        }

        private void ClearForm()
        {
            _selectedUserId = -1;
            txtUserName.Clear();
            txtUserPassword.Clear();
            cmbRole.SelectedIndex = -1;
            picUser.Image = null;
            dgvUsers.ClearSelection();
        }

        private void BtnResetPass_Click(object sender, EventArgs e)
        {
            // The "Reset" button clears the form to allow adding a new user easily
            ClearForm();
        }

        private void DgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var row = dgvUsers.SelectedRows[0];
                if (row.Cells[0].Value != null)
                {
                    _selectedUserId = Convert.ToInt32(row.Cells[0].Value);
                    var user = MemoryStore.Users.FirstOrDefault(u => u.Id == _selectedUserId);
                    
                    if (user != null)
                    {
                        txtUserName.Text = user.Username;
                        txtUserPassword.Text = user.Password; // Show real password when editing
                        cmbRole.SelectedItem = user.Role;
                        picUser.Image = user.ProfilePicture;
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string username = txtUserName.Text.Trim();
            
            // Prevent duplicate usernames
            if (MemoryStore.Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Username already exists. Please choose a different one.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int newId = MemoryStore.Users.Count > 0 ? MemoryStore.Users.Max(u => u.Id) + 1 : 1;
            string role = cmbRole.SelectedItem.ToString();
            
            var newUser = new User
            {
                Id = newId,
                Username = username,
                Password = txtUserPassword.Text,
                Role = role,
                IsAdmin = (role == "System Administrator"),
                ProfilePicture = picUser.Image
            };

            MemoryStore.Users.Add(newUser);
            MemoryStore.LogAction("USER ADDED", $"User '{username}' was added as {role}.");
            
            MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadGridData();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1)
            {
                MessageBox.Show("Please select a user to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            var user = MemoryStore.Users.FirstOrDefault(u => u.Id == _selectedUserId);
            if (user == null) return;

            string newUsername = txtUserName.Text.Trim();

            // Check if the username is taken by someone else
            if (MemoryStore.Users.Any(u => u.Id != _selectedUserId && u.Username.Equals(newUsername, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Username already exists. Please choose a different one.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string oldRole = user.Role;
            string newRole = cmbRole.SelectedItem.ToString();

            // Prevent self-demotion from System Administrator to avoid locking everyone out
            if (MemoryStore.CurrentUser != null && MemoryStore.CurrentUser.Id == user.Id && oldRole == "System Administrator" && newRole != "System Administrator")
            {
                MessageBox.Show("You cannot remove your own System Administrator privileges.", "Operation Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            user.Username = newUsername;
            user.Password = txtUserPassword.Text;
            user.Role = newRole;
            user.IsAdmin = (newRole == "System Administrator");
            user.ProfilePicture = picUser.Image;

            MemoryStore.LogAction("USER UPDATED", $"User '{newUsername}' details were updated.");
            
            MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadGridData();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1)
            {
                MessageBox.Show("Please select a user to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = MemoryStore.Users.FirstOrDefault(u => u.Id == _selectedUserId);
            if (user == null) return;

            // Prevent deleting currently logged-in user
            if (MemoryStore.CurrentUser != null && user.Id == MemoryStore.CurrentUser.Id)
            {
                MessageBox.Show("You cannot delete your own account while logged in.", "Operation Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete user '{user.Username}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                MemoryStore.Users.Remove(user);
                MemoryStore.LogAction("USER DELETED", $"User '{user.Username}' was removed from the system.");
                
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
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUserPassword.Text))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserPassword.Focus();
                return false;
            }

            if (cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a system role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return false;
            }

            return true;
        }
    }
}

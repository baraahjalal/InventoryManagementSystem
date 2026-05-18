using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;


namespace InventoryManagementSystem
{
    public partial class FrmLogin : Form
    {
        // Variable to count failed attempts
        private int failedAttempts = 0;

        // Variable to track the countdown
        private int lockoutSecondsRemaining = 0;

        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FrmLogin()
        {
            InitializeComponent();
            LoadUsersIntoComboBox();
        }

        private void LoadUsersIntoComboBox()
        {
            cmbUserName.Items.Clear();

            foreach (var user in UserRepository.GetAll())
            {
                cmbUserName.Items.Add(user.Username);
            }

            if (cmbUserName.Items.Count > 0)
            {
                cmbUserName.SelectedIndex = 0;
            }
        }

        private void btnAuthenticate_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(cmbUserName.Text))
            { _errorProvider.SetError(cmbUserName, "Please select or enter a username."); isValid = false; }
            else
              _errorProvider.SetError(cmbUserName, string.Empty);

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            { _errorProvider.SetError(txtPassword, "Password cannot be empty."); isValid = false; }
            else
              _errorProvider.SetError(txtPassword, string.Empty);

            if (!isValid) return;

            string enteredUsername = cmbUserName.Text.Trim();
            string enteredPassword = txtPassword.Text;

            User authenticatedUser = UserRepository.Authenticate(enteredUsername, enteredPassword);
            // في حالة المستخدم صح !
            if (authenticatedUser != null)
            {
                // Successful login
                failedAttempts = 0;

                // Save the current user for the session
                DatabaseHelper.CurrentUser = authenticatedUser;

             //   MessageBox.Show($"Welcome, {authenticatedUser.Role}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FrmMain mainForm = new FrmMain();
                mainForm.Show();
                this.Hide();
            }

            // في حالة المستخدم غلط !
            else
            {
                // Failed login
                failedAttempts++;

                if (failedAttempts >= 3)
                {
                    // Trigger the lockout mechanism
                    StartLockout();
                }
                else
                {
                    // Show remaining attempts
                    int attemptsLeft = 3 - failedAttempts;
                    MessageBox.Show($"Invalid credentials. You have {attemptsLeft} attempt(s) left.",
                                    "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void StartLockout()
        {
            // Setup the lockout variables
            lockoutSecondsRemaining = 30;

            // Disable the login button entirely so they can't click it
            btnAuthenticate.Enabled = false;

            // Show the label and set initial text
            label1.Visible = true;
            label1.Text = $"Locked. Try again in {lockoutSecondsRemaining} seconds.";
            label1.ForeColor = System.Drawing.Color.Red;

            // Configure and start timer (1000 milliseconds = 1 second)
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lockoutSecondsRemaining--;

            if (lockoutSecondsRemaining > 0)
            {
                // Update the countdown on screen
                label1.Text = $"Locked. Try again in {lockoutSecondsRemaining} seconds.";
            }
            else
            {
                // Lockout time is over!
                timer1.Stop();
                label1.Visible = false;
                btnAuthenticate.Enabled = true;
                failedAttempts = 0; // Reset their attempts so they get 3 fresh tries
            }
        }
    }
}

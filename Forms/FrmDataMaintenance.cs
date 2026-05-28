using System;
using System.Windows.Forms;
using InventoryManagementSystem.DAL;

namespace InventoryManagementSystem
{
    public partial class FrmDataMaintenance : Form
    {
        public FrmDataMaintenance()
        {
            InitializeComponent();
        }

        private void FrmDataMaintenance_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            var s = DataMaintenanceRepository.GetRetentionSettings();
            chkEnabled.Checked = s.IsEnabled;
            cmbRetentionYears.SelectedIndex = YearsToIndex(s.RetentionYears);
            cmbWarnInterval.SelectedIndex   = MonthsToIndex(s.WarnIntervalMonths);
            UpdateControlStates();
        }

        private static int YearsToIndex(int years)
        {
            switch (years)
            {
                case 1: return 0;
                case 2: return 1;
                case 5: return 3;
                default: return 2; // 3 years
            }
        }

        private static int MonthsToIndex(int months)
        {
            switch (months)
            {
                case 1:  return 0;
                case 6:  return 2;
                case 12: return 3;
                default: return 1; // 3 months
            }
        }

        private void ChkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControlStates();
        }

        private void UpdateControlStates()
        {
            bool on = chkEnabled.Checked;
            cmbRetentionYears.Enabled = on;
            cmbWarnInterval.Enabled   = on;
            lblRetentionYears.Enabled = on;
            lblWarnInterval.Enabled   = on;
        }

        private int GetRetentionYears()
        {
            switch (cmbRetentionYears.SelectedIndex)
            {
                case 0: return 1;
                case 1: return 2;
                case 3: return 5;
                default: return 3;
            }
        }

        private int GetWarnIntervalMonths()
        {
            switch (cmbWarnInterval.SelectedIndex)
            {
                case 0: return 1;
                case 2: return 6;
                case 3: return 12;
                default: return 3;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbRetentionYears.SelectedIndex < 0 || cmbWarnInterval.SelectedIndex < 0)
            {
                MessageBox.Show("Please select values for all settings.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataMaintenanceRepository.SaveRetentionSettings(
                    GetRetentionYears(),
                    GetWarnIntervalMonths(),
                    chkEnabled.Checked);

                MessageBox.Show("Settings saved successfully.", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save settings:\n\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

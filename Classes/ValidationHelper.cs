using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;

namespace InventoryManagementSystem
{
    public static class ValidationHelper
    {
        // 1. Required Check
        public static bool IsRequired(string value, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                errorMessage = "هذا الحقل مطلوب ولا يمكن تركه فارغاً.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        // 2. Number Validation (Decimal)
        public static bool IsValidDecimal(string value, out string errorMessage)
        {
            if (!decimal.TryParse(value, out decimal parsedValue))
            {
                errorMessage = "الرجاء إدخال رقم صحيح أو عشري صالح.";
                return false;
            }
            if (parsedValue <= 0)
            {
                errorMessage = "يجب أن تكون القيمة أكبر من الصفر.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        // 3. Integer Validation (e.g. Quantity)
        public static bool IsValidInteger(string value, out string errorMessage)
        {
            if (!int.TryParse(value, out int parsedValue))
            {
                errorMessage = "الرجاء إدخال رقم صحيح.";
                return false;
            }
            if (parsedValue < 0)
            {
                errorMessage = "لا يمكن أن تكون القيمة سالبة.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        // 4. Email Validation
        public static bool IsValidEmail(string email, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                errorMessage = string.Empty; // Assuming optional unless IsRequired is called first
                return true; 
            }

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!regex.IsMatch(email))
            {
                errorMessage = "صيغة البريد الإلكتروني غير صحيحة.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        // 5. Phone Validation (Numbers only, Min/Max length)
        public static bool IsValidPhone(string phone, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                errorMessage = "رقم الهاتف مطلوب.";
                return false;
            }

            var regex = new Regex(@"^[\d\+\s\-]{9,15}$");
            if (!regex.IsMatch(phone))
            {
                errorMessage = "رقم الهاتف يجب أن يحتوي على أرقام فقط (9 إلى 15 رقم).";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        // 6. Min/Max Length
        public static bool IsValidLength(string value, int min, int max, out string errorMessage)
        {
            value = value?.Trim() ?? "";
            if (value.Length < min || value.Length > max)
            {
                errorMessage = $"يجب أن يكون طول النص بين {min} و {max} حرف.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        // 8. Handle UI Validation Result (ErrorProvider)

        // 9. Restrict KeyPress to Digits Only (UI Level)
        public static void AllowOnlyDigits(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // 10. Restrict KeyPress to Digits and Decimals
        public static void AllowOnlyDecimals(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
    }
}

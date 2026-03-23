using System.Text.RegularExpressions;

namespace QuantityMeasurementConsoleApp
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Simple email validation regex
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Password requirements:
            // - At least 8 characters
            // - At least one uppercase letter
            // - At least one lowercase letter
            // - At least one digit
            // - At least one special character
            var hasMinimumLength = password.Length >= 8;
            var hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            var hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            var hasDigit = Regex.IsMatch(password, @"\d");
            var hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]");

            return hasMinimumLength && hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        public static string GetPasswordRequirements()
        {
            return "Password must contain:\n" +
                   "  • At least 8 characters\n" +
                   "  • At least one uppercase letter\n" +
                   "  • At least one lowercase letter\n" +
                   "  • At least one number\n" +
                   "  • At least one special character (!@#$%^&*)";
        }
    }
}
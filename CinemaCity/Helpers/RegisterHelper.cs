using System.Text.RegularExpressions;

namespace CinemaCity.Helpers
{
    public static class RegisterHelper
    {
        public static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
        public static bool IsStrongPassword(string password)
        {
            return password.Length>=8 &&
                password.Any(char.IsUpper)&&
                password.Any(char.IsLower) &&
                password.Any(char.IsDigit);
        }
    }
}

using System;
using System.Linq;

namespace PSP
{
    public class PasswordChecker
    {
        private static readonly int _minLength = 8;

        public static bool IsValid(string password)
        {
            return CorrectLength(password) && IncludesUpperCase(password) && IncludesSpecialChar(password);
        }

        private static bool IncludesSpecialChar(string password)
        {
            var specialChars = password.ToCharArray()
                .Where(c => !Char.IsLetter(c) && !Char.IsDigit(c))
                .Where(c => !String.IsNullOrWhiteSpace(c.ToString()))
                .ToArray();

            if (specialChars.Count() == 0)
            {
                return false;
            }

            return true;
        }

        private static bool CorrectLength(string password)
        {
            return password.Length >= _minLength;
        }

        private static bool IncludesUpperCase(string password)
        {
            var upperCaseChars = password.ToCharArray()
                .Where(c => Char.IsLetter(c))
                .Where(c => Char.IsUpper(c))
                .ToArray();

            if (upperCaseChars.Count() == 0)
            {
                return false;
            }

            return true;
        }


    }
}

using SimpleValidation.Interfaces;
using System;

namespace SimpleValidation
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly int _passwordMinimumLength;
        private readonly string _specialCharacters;

        public PasswordValidator(int passwordMinimumLength = 1, string specialCharacters = "")
        {
            _passwordMinimumLength = passwordMinimumLength;
            _specialCharacters = specialCharacters ?? "";
        }

        public bool Validate(string password)
        {
            if(password is null)
            {
                throw new ArgumentException("Password cannot be NULL");
            }

            return IsLongerOrEqualsThanMinimumLength(password) && ContainsSpecialCharacter(password) && ContainsUppercase(password);
        }

        private bool IsLongerOrEqualsThanMinimumLength(string password)
        {
            return password.Length >= _passwordMinimumLength;
        }

        private bool ContainsSpecialCharacter(string password)
        {
            foreach(char letter in password)
            {
                if (_specialCharacters.Contains(letter))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ContainsUppercase(string password)
        {
            var lowerCasePassword = password.ToLower();
            return !password.Equals(lowerCasePassword);
        }

    }
}
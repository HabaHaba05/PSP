using SimpleValidation.Interfaces;

namespace SimpleValidation
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly int _passwordMinimumLength;
        private readonly string _specialCharacters;

        public PasswordValidator(int passwordMinimumLength = 1, string specialCharacters = "")
        {
            _passwordMinimumLength = passwordMinimumLength;
            _specialCharacters = specialCharacters;
        }

        public bool Validate(string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
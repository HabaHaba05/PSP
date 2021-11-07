using LibraryUsage.Mapper;
using PSP;

namespace LibraryUsage.Validators
{
    public class UserValidator : IUserValidator
    {
        private readonly PhoneValidator _phoneValidator;

        public UserValidator()
        {
            _phoneValidator = new PhoneValidator();
        }

        public bool IsValid(CreateUser user)
        {
            return EmailValidator.IsValid(user.Email)
                && PasswordChecker.IsValid(user.Password)
                && _phoneValidator.IsValid(user.PhoneNr);
        }
    }
}

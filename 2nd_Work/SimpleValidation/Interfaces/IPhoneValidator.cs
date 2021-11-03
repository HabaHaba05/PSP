using SimpleValidation.PhoneNumberHelpers;

namespace SimpleValidation.Interfaces
{
    public interface IPhoneValidator
    {
        bool Validate(ref string phoneNumber, CountryIso? countryIso);
    }
}
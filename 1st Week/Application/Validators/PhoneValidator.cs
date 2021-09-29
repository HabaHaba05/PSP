using Common;
using System;

namespace Validators
{
    public class PhoneValidator
    {
        public bool ContainsOnlyNumbers(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        // Breaks SOLID
        // As Validator should be responsible only for validation
        public string ConvertToInternationalPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsValidInCountry(string phoneNumber, CountryIso countryIso)
        {
            throw new NotImplementedException();
        }
    }
}

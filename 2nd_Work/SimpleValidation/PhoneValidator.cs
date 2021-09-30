using SimpleValidation.Interfaces;
using SimpleValidation.PhoneNumberHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleValidation
{
    public class PhoneValidator : IPhoneValidator
    {
        private readonly Dictionary<CountryIso, CountryRule> _countriesRules;
        public PhoneValidator(Dictionary<CountryIso,CountryRule> countriesRules = null)
        {
            _countriesRules = countriesRules ?? new Dictionary<CountryIso, CountryRule>();
        }

        public bool Validate(ref string phoneNumber, CountryIso? countryIso = null)
        {
            if(string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("Phone Number cannot be empty or null");
            }

            if(ContainsOnlyNumbers(phoneNumber) && (countryIso == null || ValidInCountry(phoneNumber, countryIso.Value)))
            {
                ConvertToInternationalNumber(ref phoneNumber);

                return true;
            }

            return false;
        }

        private static bool ContainsOnlyNumbers(string phoneNumber)
        {
            foreach(var character in phoneNumber)
            {
                if (char.IsLetter(character))
                {
                    return false;
                }
            }
            return true;
        }
        
        // Breaks SOLID :)
        private static void ConvertToInternationalNumber(ref string phoneNumber)
        {
            if(phoneNumber.First() == '8')
            {
                phoneNumber = "+370" + phoneNumber.Remove(0,1);
            }
        }

        private bool ValidInCountry(string phoneNumber, CountryIso countryIso)
        {
            if(!_countriesRules.TryGetValue(countryIso, out CountryRule countryRule))
            {
                throw new Exception($"Rule doesn't exists for coutryIso = {countryIso}");
            }

            return phoneNumber.Length == countryRule.Length && phoneNumber.StartsWith(countryRule.PhoneNumberPrefix);
        }

    }
}
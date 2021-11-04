using System;
using System.Collections.Generic;
using System.Linq;

namespace PSP
{
    public class PhoneValidator
    {
        private readonly char _plusSign = '+';

        private List<PhoneCountryRule> countryRules = new List<PhoneCountryRule>() { 
            new PhoneCountryRule {Prefix = "+370", Length = 12},
            new PhoneCountryRule {Prefix = "+66", Length = 10}
        };
        public bool IsValid(string number)
        {
            if (number.StartsWith("86"))
            {
                number = "+370" + number.Remove(0, 1);
            }

            return ValidPrefixAndLength(number) && ContainsOnePlusSign(number) && ContainsNoExtraSymbols(number);
        }

        private bool ContainsOnePlusSign(string number)
        {
            if (!number.Contains(_plusSign))
            {
                return false;
            }

            if (number.IndexOf(_plusSign) != number.LastIndexOf(_plusSign))
            {
                return false;
            }

            return true;
        }

        private bool ContainsNoExtraSymbols(string number)
        {
            var chars = number.ToCharArray()
                .Where(c => !Char.IsDigit(c))
                .ToArray();

            if (chars.Count() == 1) //one for plus sign
            {
                return true;
            }

            return false;
        }

        private bool ValidPrefixAndLength(string number)
        {
            foreach(PhoneCountryRule cr in countryRules)
            {
                if (number.StartsWith(cr.Prefix) && number.Length == cr.Length)
                {
                    return true;
                }
            }
                
            return false;
        }

        public void AddCountryRule(string prefix, int length)
        {
            countryRules.Add(new PhoneCountryRule { Prefix = prefix, Length = length });
        }
    }

    public class PhoneCountryRule
    {
        public string Prefix { get; set; }
        public int Length { get; set; }
    }
}

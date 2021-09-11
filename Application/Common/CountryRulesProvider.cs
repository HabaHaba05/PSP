using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class CountryRulesProvider
    {
        private readonly List<CountryRule> countryRules = new()
        {
            new() { CountryIso = CountryIso.LT, Length = 9, PhoneNumberPrefix="86"},
            new() { CountryIso = CountryIso.PL, Length = 11, PhoneNumberPrefix = "48" },
        };

        public CountryRule GetCountryRule(CountryIso countryIso)
        {
            var countryRule = countryRules.Where(x => x.CountryIso == countryIso).First() ?? throw new ArgumentException($"{nameof(countryIso)} doesnt have any rules");
            return countryRule;
        }
    }
}

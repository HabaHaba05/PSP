using NUnit.Framework;
using SimpleValidation.PhoneNumberHelpers;
using System;
using System.Collections.Generic;

namespace SimpleValidation.Tests
{
    [TestFixture]
    public class PhoneValidatorTests
    {
        [Test]
        public void GivenPhoneValidator_WhenNumberContainsOtherThanDigits_ReturnsFalse()
        {
            var phoneNumberValidator = new PhoneValidator();
            var number = "+370s0000000";

            var result = phoneNumberValidator.Validate(ref number);

            Assert.IsFalse(result);
        }

        [Test]
        public void GivenPhoneValidator_WhenNumberStartsWith8_ConvertsTo370()
        {
            var phoneNumberValidator = new PhoneValidator();
            var number = "860000000";

            phoneNumberValidator.Validate(ref number);

            Assert.AreEqual("+37060000000", number);
        }

        [Test]
        public void GivenPhoneValidator_WhenLengthIsLongerOrShorter_ThanSpecifiedInTheRule_ReturnsFalse()
        {
            var countryIso = CountryIso.LT;
            var rules = new Dictionary<CountryIso, CountryRule>()
            {
                { countryIso, new CountryRule(){ Length = 5 , PhoneNumberPrefix = "86"} }
            };

            var phoneNumberValidator = new PhoneValidator(rules);
            var number = "860000000";
            var number2 = "86";

            Assert.False(phoneNumberValidator.Validate(ref number, countryIso));
            Assert.False(phoneNumberValidator.Validate(ref number2, countryIso));
        }

        [Test]
        public void GivenPhoneValidator_WhenPrefixIsDifferent_ThanSpecifiedInTheRule_ReturnsFalse()
        {
            var countryIso = CountryIso.LT;
            var rules = new Dictionary<CountryIso, CountryRule>()
            {
                { countryIso, new CountryRule(){ Length = 5 , PhoneNumberPrefix = "86"} }
            };

            var phoneNumberValidator = new PhoneValidator(rules);
            var number = "12345";

            Assert.False(phoneNumberValidator.Validate(ref number, countryIso));
        }

        [Test]
        public void GivenPhoneValidator_WhenLengthAndPrefixAreTheSame_AsSpecifiedInTheRule_ReturnsTrue()
        {
            var countryIso = CountryIso.LT;
            var rules = new Dictionary<CountryIso, CountryRule>()
            {
                { countryIso, new CountryRule(){ Length = 5 , PhoneNumberPrefix = "86"} }
            };

            var phoneNumberValidator = new PhoneValidator(rules);
            var number = "86123";

            Assert.True(phoneNumberValidator.Validate(ref number, countryIso));
        }

        [Test]
        public void GivenPhoneValidator_WhenNumberIsEmpty_ReturnsFalse()
        {
            var phoneNumberValidator = new PhoneValidator();
            var number = string.Empty;

            Assert.Throws<ArgumentException>(()=>phoneNumberValidator.Validate(ref number));
        }
    }
}
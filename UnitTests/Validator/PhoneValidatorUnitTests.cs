using Common;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Validators;
using Xunit;

namespace UnitTests.Validator
{
    public abstract class PhoneValidatorUnitTests
    {
        protected PhoneValidator _sut;

        public PhoneValidatorUnitTests()
        {
            _sut = new PhoneValidator();
        }

        public class ContainsOnlyNumbers : PhoneValidatorUnitTests
        {
            [Theory]
            [InlineData("", false)]
            [InlineData("123456789", true)]
            [InlineData("123Y456789", false)]
            public void ShouldReturnIfPhoneNumberContainsOnlyNumbers(string phoneNumber, bool expectedResult)
            {
                //When
                var containsOnlyNumbers = _sut.ContainsOnlyNumbers(phoneNumber);
                //Then
                containsOnlyNumbers.Should().Be(expectedResult);
            }
        }

        public class ConvertToInternationalPhoneNumber : PhoneValidatorUnitTests
        {
            [Theory]
            [InlineData("8", "+370")]
            [InlineData("8654", "+370654")]
            public void ShouldReturnConvertedPhoneNumber(string phoneNumber, string expectedResult)
            {
                //When
                var newNumber = _sut.ConvertToInternationalPhoneNumber(phoneNumber);
                //Then
                newNumber.Should().Be(expectedResult);
            }

            [Theory]
            [InlineData("")]
            [InlineData(" 8 $#")]
            [InlineData("7654")]
            public void ShouldReturnNotChangedPhoneNumber(string phoneNumber)
            {
                //When
                var newNumber = _sut.ConvertToInternationalPhoneNumber(phoneNumber);
                //Then
                newNumber.Should().Be(phoneNumber);
            }
        }

        public class IsValidInCountry : PhoneValidatorUnitTests
        {
            [Theory]
            [InlineData("86123", CountryIso.DK, true)]
            [InlineData("76123", CountryIso.DK, false)]
            [InlineData("861237", CountryIso.DK, false)]
            [InlineData("8612", CountryIso.DK, false)]
            [InlineData("481", CountryIso.PL, true)]
            [InlineData("7612", CountryIso.PL, false)]
            [InlineData("4812895", CountryIso.PL, false)]
            [InlineData("48", CountryIso.PL, false)]
            public void ShouldReturnIfPhoneNumberIsValidInCountry(string phoneNumber, CountryIso countryIso, bool expectedResult)
            {
                //Given
                var rules = new List<CountryRule>()
                {
                    new CountryRule() {CountryIso = CountryIso.DK, Length = 5, PhoneNumberPrefix = "86" },
                    new CountryRule() {CountryIso = CountryIso.PL, Length = 3, PhoneNumberPrefix = "48" },
                };

                var countryRulesProviderMock = new Mock<CountryRulesProvider>();
                rules.ForEach(x => countryRulesProviderMock.Setup(o => o.GetCountryRule(x.CountryIso)).Returns(x));

                //When
                var isValid = _sut.IsValidInCountry(phoneNumber, countryIso);
                //Then
                isValid.Should().Be(expectedResult);
            }
        }
    }
}

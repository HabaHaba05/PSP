using FluentAssertions;
using Validators;
using Xunit;

namespace UnitTests.Validator
{
    public class EmailValidatorUnitTests
    {
        protected EmailValidator _sut;

        public EmailValidatorUnitTests()
        {
            _sut = new EmailValidator();
        }

        public class IsValidAtSign : EmailValidatorUnitTests
        {
            [Theory]
            [InlineData("", false)]
            [InlineData("@", false)]
            [InlineData("abc@a@gmail.com", false)]
            [InlineData("@agmail.com", false)]
            [InlineData("abc@", false)]
            [InlineData("abc@gmail.com", true)]
            public void ShouldReturnIfAtSignIsValid(string email, bool expectedResult)
            {
                //When
                var isValid = _sut.IsValidAtSign(email);
                //Then
                isValid.Should().Be(expectedResult);
            }
        }
        public class IsValidLocalPart : EmailValidatorUnitTests
        {
            [Theory]
            [InlineData("", false)]
            [InlineData("@", false)]
            [InlineData("a", true)]
            [InlineData("a@", false)]
            [InlineData("Aa0123456789!#$%&'*+-/=?^_`{|}~", true)]
            [InlineData(".abc", false)]
            [InlineData("abc.", false)]
            [InlineData("a..bc", false)]
            [InlineData("a...bc", false)]
            [InlineData("a.b.c", true)]
            [InlineData("a.bc", true)]
            public void ShouldReturnIfLocalPartIsValid(string localPartOfEmail, bool expectedResult)
            {
                //When
                var isValid = _sut.IsValidLocalPart(localPartOfEmail);
                //Then
                isValid.Should().Be(expectedResult);
            }
        }

        public class IsValidDomain : EmailValidatorUnitTests
        {
            [Theory]
            [InlineData("", false)]
            [InlineData("@", false)]
            [InlineData("@gmail", false)]
            [InlineData("gma.il", false)]
            [InlineData("gmail", true)]
            [InlineData("Aa0123456789!#$%&'*+-/=?^_`{|}~", false)]
            [InlineData("-Abc", false)]
            [InlineData("aBc-", false)]
            public void ShouldReturnIfDomainIsValid(string domainOfEmail, bool expectedResult)
            {
                //When
                var isValid = _sut.IsValidDomain(domainOfEmail);
                //Then
                isValid.Should().Be(expectedResult);
            }
        }

        public class IsValidTopLevelDomain : EmailValidatorUnitTests
        {
            [Theory]
            [InlineData("", false)]
            [InlineData("l", false)]
            [InlineData("lT", true)]
            [InlineData("qweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqwe", true)]   //63chars
            [InlineData("qweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweA", false)]   //64chars
            [InlineData("com1", false)]
            [InlineData("lv#@4", false)]
            public void ShouldReturnIfTopLevelDomainIsValid(string tldOfEmail, bool expectedResult)
            {
                //When
                var isValid = _sut.IsValidTopLevelDomain(tldOfEmail);
                //Then
                isValid.Should().Be(expectedResult);
            }
        }

    }
}

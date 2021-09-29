using FluentAssertions;
using Validators;
using Xunit;

namespace UnitTests.Validator
{
    public abstract class EmailValidatorUnitTests
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
            [InlineData("abc@a@gmail.com", false)]
            [InlineData("@", true)]
            [InlineData("@agmail.com", true)]
            [InlineData("abc@", true)]
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
            [InlineData("a@", true)]
            [InlineData("Aa0123456789!#$%&'*+-/=?^_`{|}~@", true)]
            [InlineData(".abc@", false)]
            [InlineData("abc.@", false)]
            [InlineData("a..bc@", false)]
            [InlineData("a...bc@", false)]
            [InlineData("a.b.c@", true)]
            [InlineData("a.bc@", true)]
            public void ShouldReturnIfLocalPartIsValid(string email, bool expectedResult)
            {
                //When
                var isValid = _sut.IsValidLocalPart(email);
                //Then
                isValid.Should().Be(expectedResult);
            }
        }

        public class IsValidDomain : EmailValidatorUnitTests
        {
            [Theory]
            [InlineData("", false)]
            [InlineData("@", false)]
            [InlineData("gg@gmail", false)]
            [InlineData("gg@gmail.g.lt", false)]
            [InlineData("abc@gma.i", true)]
            [InlineData("abc@gma..il", false)]
            [InlineData("abc@Aa0123456789!#$%&'*+-/=?^_`{|}~.lt", false)]
            [InlineData("ab@-Abc.lt", false)]
            [InlineData("@A-bc.lt", true)]
            [InlineData("@123.lt", false)]
            [InlineData("@aBc-.lt", false)]
            public void ShouldReturnIfDomainIsValid(string email, bool expectedResult)
            {
                //When
                var isValid = _sut.IsValidDomain(email);
                //Then
                isValid.Should().Be(expectedResult);
            }
        }

        public class IsValidTopLevelDomain : EmailValidatorUnitTests
        {
            [Theory]
            [InlineData("", false)]
            [InlineData("@.l", false)]
            [InlineData("@.lT", true)]
            [InlineData("ab@gmail.qweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqwe", true)]   //63chars
            [InlineData("ab@gmail.qweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweasdzxcrqweA", false)]   //64chars
            [InlineData("@gmail.com1", false)]
            [InlineData("@gmail.lv#@4", false)]
            public void ShouldReturnIfTopLevelDomainIsValid(string email, bool expectedResult)
            {
                //When
                var isValid = _sut.IsValidTopLevelDomain(email);
                //Then
                isValid.Should().Be(expectedResult);
            }
        }

    }
}

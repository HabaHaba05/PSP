using NUnit.Framework;

namespace SimpleValidation.Tests
{
    [TestFixture]
    public class PasswordValidatorTests
    {
        [Test]
        public void GivenPasswordValidator_WhenPasswordIsTooShort_ReturnsFalse()
        {
            int passwordMinimumLength = 8;
            var passwordValidator = new PasswordValidator(passwordMinimumLength);
            var password = "Shrt1!";

            var result = passwordValidator.Validate(password);

            Assert.IsFalse(result);
        }

        [Test]
        public void GivenPasswordValidator_WhenPasswordDoesntHaveUpperCase_ReturnsFalse()
        {
            var passwordValidator = new PasswordValidator();
            var password = "notuppercase1!";

            var result = passwordValidator.Validate(password);

            Assert.IsFalse(result);
        }

        [Test]
        public void GivenPasswordValidator_WhenPasswordDoesntHaveSpecial_ReturnsFalse()
        {
            string specialChars = "#@!";
            var passwordValidator = new PasswordValidator(specialCharacters: specialChars);
            var password = "NoSpecialSymbol1";

            var result = passwordValidator.Validate(password);

            Assert.IsFalse(result);
        }

        [Test]
        [Ignore("There wasn't such a requirement ")]
        public void GivenPasswordValidator_WhenPasswordDoesntHaveDigit_ReturnsFalse()
        {
            var passwordValidator = new PasswordValidator();
            var password = "NodigitPassword@";

            var result = passwordValidator.Validate(password);

            Assert.IsFalse(result);
        }

        [Test]
        [Ignore("Don't get an idea what is InvalidSpecialSymbol, and if it's called Invalid, why validator should return True")]
        public void GivenPasswordValidator_WhenPasswordContainsInvalidSpecialSymbol_ReturnsTrue()
        {
            var passwordValidator = new PasswordValidator();
            var password = "GoodPassword+!";

            var result = passwordValidator.Validate(password);

            Assert.IsTrue(result);
        }

        [Test]
        public void GivenPasswordValidator_WhenPasswordIsValid_ReturnsTrue()
        {
            string specialChars = "#@!";
            var passwordValidator = new PasswordValidator(specialCharacters: specialChars);
            var password = "GoodPassword1!";

            var result = passwordValidator.Validate(password);

            Assert.IsTrue(result);
        }

        [Test]
        public void GivenNumberValidator_WhenPasswordIsEmpty_ReturnsFalse()
        {
            var passwordValidator = new PasswordValidator();
            var number = string.Empty;

            var result = passwordValidator.Validate(number);

            Assert.IsFalse(result);
        }
    }
}
using NUnit.Framework;
using SimpleValidation.Interfaces;

namespace SimpleValidation.Tests
{
    [TestFixture]
    public class PasswordValidatorTests
    {
        private IPasswordValidator _passwordValidator;
        
        [SetUp]
        public void Setup()
        {
            _passwordValidator = new PasswordValidator();
        }

        [Test]
        public void GivenPasswordValidator_WhenPasswordIsTooShort_ReturnsFalse()
        {
            var password = "Shrt1!";

            var result = _passwordValidator.Validate(password);
            
            Assert.IsFalse(result);
        }

        [Test]
        public void GivenPasswordValidator_WhenPasswordDoesntHaveUpperCase_ReturnsFalse()
        {
            var password = "notuppercase1!";

            var result = _passwordValidator.Validate(password);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenPasswordValidator_WhenPasswordDoesntHaveSpecial_ReturnsFalse()
        {
            var password = "NoSpecialSymbol1";

            var result = _passwordValidator.Validate(password);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenPasswordValidator_WhenPasswordDoesntHaveDigit_ReturnsFalse()
        {
            var password = "NodigitPassword@";

            var result = _passwordValidator.Validate(password);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenPasswordValidator_WhenPasswordContainsInvalidSpecialSymbol_ReturnsTrue()
        {
            var password = "GoodPassword+!";

            var result = _passwordValidator.Validate(password);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenPasswordValidator_WhenPasswordIsValid_ReturnsTrue()
        {
            var password = "GoodPassword1!";

            var result = _passwordValidator.Validate(password);
            
            Assert.IsTrue(result);
        }
        
        [Test]
        public void GivenNumberValidator_WhenPasswordIsEmpty_ReturnsFalse()
        {
            var number = string.Empty;

            var result = _passwordValidator.Validate(number);
            
            Assert.IsFalse(result);
        }
    }
}
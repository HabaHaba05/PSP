using System.Collections.Generic;
using NUnit.Framework;
using SimpleValidation.Interfaces;

namespace SimpleValidation.Tests
{
    [TestFixture]
    public class NumberValidatorTests
    {
        private INumberValidator _numberValidator;
        
        [SetUp]
        public void Setup()
        {
            _numberValidator = new NumberValidator();
        }

        [Test]
        public void GivenNumberValidator_WhenNumberContainsOtherThanDigits_ReturnsFalse()
        {
            var number = "+370s0000000";

            var result = _numberValidator.Validate(number);

            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenNumberValidator_WhenNumberIsTooLong_ReturnsFalse()
        {
            var number = "+3700000000000000000000000";

            var result = _numberValidator.Validate(number);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenNumberValidator_WhenNumberStartsWith8_ReturnsTrue()
        {
            var number = "860000000";

            var result = _numberValidator.Validate(number);
            
            Assert.IsTrue(result);
        }
        
        [Test]
        public void GivenNumberValidator_WhenNumberStartsWithInvalidCode_ReturnsFalse()
        {
            var number = "+12412345678";

            var result = _numberValidator.Validate(number);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenNumberValidator_WhenNumberIsTooShort_ReturnsFalse()
        {
            var number = "+22012";

            var result = _numberValidator.Validate(number);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenNumberValidator_WhenNumberCodeIsFromConfig_ReturnsTrue()
        {
            var number = "+20212345678";

            var result = _numberValidator.Validate(number);
            
            Assert.IsTrue(result);
        }
        
        [Test]
        public void GivenNumberValidator_WhenNumberDoesntStartWithPlus_ReturnsFalse()
        {
            var number = "20212345678";

            var result = _numberValidator.Validate(number);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenNumberValidator_WhenNumberIsEmpty_ReturnsFalse()
        {
            var number = string.Empty;

            var result = _numberValidator.Validate(number);
            
            Assert.IsFalse(result);
        }
    }
}
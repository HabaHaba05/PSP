using NUnit.Framework;
using SimpleValidation.Interfaces;

namespace SimpleValidation.Tests
{
    [TestFixture]
    public class EmailValidatorTests
    {
        private IEmailValidator _emailValidator;

        [SetUp]
        public void Setup()
        {
            _emailValidator = new EmailValidator();
        }
        
        [Test]
        public void GivenEmailValidator_WhenEmailDoesntContainAt_ReturnsFalse()
        {
            var email = "testemail.com";

            var result = _emailValidator.Validate(email);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenEmailValidator_WhenEmailContainsInvalidLetters_ReturnsFalse()
        {
            var email = "žalia@gmail.com";

            var result = _emailValidator.Validate(email);
            
            Assert.IsFalse(result);
        }

        [Test]
        public void GivenEmailValidator_WhenEmailContainsInvalidSymbols_ReturnsFalse()
        {
            var email = "inva¢lid@gmail.com";

            var result = _emailValidator.Validate(email);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenEmailValidator_WhenEmailContainsValidSymbols_ReturnsTrue()
        {
            var email = "validemail!#$%&'*+-/=?^_`{|}~@gmail.com";

            var result = _emailValidator.Validate(email);
            
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenEmailValidator_WhenEmailContainsValidDots_ReturnTrue()
        {
            var email1 = "emai.l@gmail.com";
            var email2 = "\"email..test\"@gmail.com";

            var result1 = _emailValidator.Validate(email1);
            var result2 = _emailValidator.Validate(email2);
            
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void GivenEmailValidator_WhenEmailContainsInvalidDots_ReturnFalse()
        {
            var email1 = ".email@gmail.com";
            var email2 = "email..test@gmail.com";
            var email3 = "emailtest.@gmail.com";

            var result1 = _emailValidator.Validate(email1);
            var result2 = _emailValidator.Validate(email2);
            var result3 = _emailValidator.Validate(email3);
            
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
            Assert.IsFalse(result3);
        }

        [Test]
        public void GivenEmailValidator_WhenEmailDomainIsCorrect_ReturnsTrue()
        {
            var email1 = "tomas@gmail.com";
            var email2 = "goood@doma-in.lt";
            
            var result1 = _emailValidator.Validate(email1);
            var result2 = _emailValidator.Validate(email2);
            
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [Test]
        public void GivenEmailValidator_WhenEmailDomainIsEmpty_ReturnsFalse()
        {
            var email = "tomas@.com";

            var result = _emailValidator.Validate(email);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenEmailValidator_WhenDomainContainsInvalidChars_ReturnsFalse()
        {
            var email = "tomas@tesžtas.com";

            var result = _emailValidator.Validate(email);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenEmailValidator_WhenTLDContainsInvalidChars_ReturnsFalse()
        {
            var email = "tomas@tesstas.co!";

            var result = _emailValidator.Validate(email);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenEmailValidator_WhenEmailIsEmpty_ReturnsFalse()
        {
            var email = string.Empty;

            var result = _emailValidator.Validate(email);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenEmailValidator_WhenNameIsEmpty_ReturnsFalse()
        {
            var email = "@gmail.com";

            var result = _emailValidator.Validate(email);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void GivenEmailValidator_WhenTLDIsEmpty_ReturnsFalse()
        {
            var email = "tomas@gmail.";

            var result = _emailValidator.Validate(email);
            
            Assert.IsFalse(result);
        }
    }
}
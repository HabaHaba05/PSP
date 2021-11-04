using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSP;

namespace ValidatorsUnitTests
{
    [TestClass]
    public class PhoneNumberValidatorUnitTests
    {
        public PhoneValidator _phoneValidator;
        public PhoneNumberValidatorUnitTests()
        {
            _phoneValidator = new PhoneValidator();
        }
        [TestMethod]
        public void Number_Blank_Spaces_Not_Valid()
        {
            var isValid = _phoneValidator.IsValid("              ");
            Assert.AreEqual(false, isValid, "Number only spaces");
        }

        [TestMethod]
        public void Number_Contains_Chars_Not_Valid()
        {
            var isValid = _phoneValidator.IsValid("8612h4123");
            Assert.AreEqual(false, isValid, "Number contains characters");
        }

        [TestMethod]
        public void Number_Too_Long_Not_Valid()
        {
            var isValid = _phoneValidator.IsValid("+37063535327123");
            Assert.AreEqual(false, isValid, "Number too long");
        }

        [TestMethod]
        public void Number_Too_Short_Not_Valid()
        {
            var isValid = _phoneValidator.IsValid("861234");
            Assert.AreEqual(false, isValid, "Number too short");
        }

        [TestMethod]
        public void Number_International_No_Plus_Not_Valid()
        {
            var isValid = _phoneValidator.IsValid("37063535327");
            Assert.AreEqual(false, isValid, "International numbers without a plus");
        }

        [TestMethod]
        public void Number_Invalid_First_Char_Not_Valid()
        {
            var isValid = _phoneValidator.IsValid("-37063535327");
            Assert.AreEqual(false, isValid, "Invalid character at the beginning of the number");
        }

        [TestMethod]
        public void Number_Begins_With_Eigth_Is_Valid()
        {
            var isValid = _phoneValidator.IsValid("863625327");
            Assert.AreEqual(true, isValid, "Valid number");
        }

        [TestMethod]
        public void Number_Begins_With_Plus_Is_Valid()
        {
            var isValid = _phoneValidator.IsValid("+37063535327");
            Assert.AreEqual(true, isValid, "Valid number");
        }

    }
}
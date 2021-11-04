using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSP;

namespace ValidatorsUnitTests
{
    [TestClass]
    public class EmailValidatorTests
    {
        [TestMethod]
        public void Email_Blank_Spaces_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("       ");
            Assert.AreEqual(false, isValid, "Email only spaces");
        }

        [TestMethod]
        public void Email_Too_Short_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("abc");
            Assert.AreEqual(false, isValid, "Email too short");
        }

        [TestMethod]
        public void Email_Doesnt_Have_At_Sign_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("kkersisgmail.com");
            Assert.AreEqual(false, isValid, "No @ sign");
        }


        [TestMethod]
        public void Email_Doesnt_Have_Dot_In_Domain_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("kkersis@gmailas");
            Assert.AreEqual(false, isValid, "No dot in domain.");
        }

        [TestMethod]
        public void Email_Has_Invalid_TLD_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("kkersis@la.parampam.pypupypu");
            Assert.AreEqual(false, isValid, "Invalid TLD");
        }

        [TestMethod]
        public void Email_No_Domain_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("kkersis@.lt");
            Assert.AreEqual(false, isValid, "No domain");
        }

        [TestMethod]
        public void Email_No_TLD_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("kkersis@pimpim.");
            Assert.AreEqual(false, isValid, "No TLD");
        }


        [TestMethod]
        public void Email_Has_Spaces_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("kker sis@gmail.com");
            Assert.AreEqual(false, isValid, "Has spaces");
        }

        [TestMethod]
        public void Email_Has_Forbidden_Chars_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("kkersis&^!)(lala@gmail.com");
            Assert.AreEqual(false, isValid, "Contains forbidden characters");
        }

        [TestMethod]
        public void Email_Has_Multiple_At_Signs_Not_Valid()
        {
            var isValid = EmailValidator.IsValid("kkersis@kkersis@kkersis@gmail.com");
            Assert.AreEqual(false, isValid, "Multiple @ signs");
        }

        [TestMethod]
        public void Email_Good_Is_Valid()
        {
            var isValid = EmailValidator.IsValid("kkersis@gmail.com");
            Assert.AreEqual(true, isValid, "All good");
        }
    }
}
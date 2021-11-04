using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSP;

namespace ValidatorsUnitTests
{
    [TestClass]
    public class PasswordValidatorUnitTests
    {
        [TestMethod]
        public void Password_Blank_Spaces_Not_Valid()
        {
            var isValid = PasswordChecker.IsValid("               ");
            Assert.AreEqual(false, isValid, "Password only spaces");
        }

        [TestMethod]
        public void Password_Too_Short_Not_Valid()
        {
            var isValid = PasswordChecker.IsValid("ba");
            Assert.AreEqual(false, isValid, "Password too short");
        }

        [TestMethod]
        public void Password_No_Uppercase_Not_Valid()
        {
            var isValid = PasswordChecker.IsValid("aaaasdjahsdkjhasd");
            Assert.AreEqual(false, isValid, "No uppercase letter in password");
        }

        [TestMethod]
        public void Password_No_Numbers_Not_Valid()
        {
            var isValid = PasswordChecker.IsValid("SlapTaZoDziuikasssssssss");
            Assert.AreEqual(false, isValid, "Password doesnt contain numbers");
        }

        [TestMethod]
        public void Password_No_Special_Chars_Not_Valid()
        {
            var isValid = PasswordChecker.IsValid("SlapTaZoDz12345");
            Assert.AreEqual(false, isValid, "Password doesnt contain special characters");
        }

        [TestMethod]
        public void Password_Good_Is_Valid()
        {
            var isValid = PasswordChecker.IsValid("slapTaZodZiukas13223!@#%%");
            Assert.AreEqual(true, isValid, "A valid password.");
        }

    }
}
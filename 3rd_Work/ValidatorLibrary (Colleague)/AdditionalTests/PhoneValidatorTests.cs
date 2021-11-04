namespace AdditionalTests
{
    using Xunit;
    using PSP;
    public class PhoneValidatorTests
    {
        private PhoneValidator phoneValidator;

        public PhoneValidatorTests()
        {
            phoneValidator = new PhoneValidator();
        }

        [Fact]
        public void PhoneValidator_AddingNewCountryPrefix_Success()
        {
            var foreignNumber = "+442222333333";

            Assert.False(phoneValidator.IsValid(foreignNumber));

            phoneValidator.AddCountryRule("+44", 13);

            Assert.True(phoneValidator.IsValid(foreignNumber));
        }
    }
}

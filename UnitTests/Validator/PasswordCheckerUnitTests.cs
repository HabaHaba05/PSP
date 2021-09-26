using Common;
using FluentAssertions;
using Moq;
using Validator;
using Xunit;

namespace UnitTests.Validator
{
    public abstract class PasswordCheckerUnitTests
    {
        protected PasswordChecker _sut;

        public PasswordCheckerUnitTests()
        {
            _sut = new PasswordChecker();
        }

        public class LongerThan : PasswordCheckerUnitTests
        {
            [Theory]
            [InlineData("", 1, false)]
            [InlineData(" ", 1, true)]
            [InlineData(" _", 1, true)]
            [InlineData(" a_B ", 5, true)]
            [InlineData(" 1 # ", 5, true)]
            public void ShouldReturnIfPasswordIsLongerOrEqualThanSpecificLength(string password, int length, bool expectedResult)
            {
                //When
                var isLonger = _sut.LongerThan(password, length);
                //Then
                isLonger.Should().Be(expectedResult);
            }
        }

        public class ContainsUppercase : PasswordCheckerUnitTests
        {
            [Theory]
            [InlineData("1", false)]
            [InlineData(" A $#", true)]
            [InlineData(" b $# G", true)]
            [InlineData("!", false)]
            [InlineData("absa", false)]
            public void ShouldReturnIfPasswordContainsUppercase(string password, bool expectedResult)
            {
                //When
                var containsUppercase = _sut.ContainsUppercase(password);
                //Then
                containsUppercase.Should().Be(expectedResult);
            }
        }

        public class ContainsSpecialCharacter : PasswordCheckerUnitTests
        {
            [Theory]
            [InlineData("", "", true)]
            [InlineData("", "a1A@#", false)]
            [InlineData(" ", "9 !32A;#", true)]
            [InlineData("aBs", "", true)]
            [InlineData("pqbc!2d", "QweP", false)]
            [InlineData("aQ", "Q", true)]
            [InlineData("aQ", "a", true)]
            public void ShouldReturnIfPasswordContainsSpecialCharacter(string password, string specialChars, bool expectedResult)
            {
                //Given
                var configurationMock = new Mock<Configuration>();
                configurationMock.Setup(o => o.SpecialCharacters).Returns(specialChars);

                //When
                var containsSpecialCharacter = _sut.ContainsSpecialCharacter(password);
                //Then
                containsSpecialCharacter.Should().Be(expectedResult);
            }
        }
    }
}

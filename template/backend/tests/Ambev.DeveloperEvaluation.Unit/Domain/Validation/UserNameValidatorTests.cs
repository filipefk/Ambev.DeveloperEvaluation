using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;
public class UserNameValidatorTests
{
    [Theory(DisplayName = "Username must between 3 and 50 characters")]
    [InlineData(0, false)]               // Invalid - too short
    [InlineData(1, false)]               // Invalid - too short
    [InlineData(2, false)]               // Invalid - too short
    [InlineData(3, true)]               // Valid
    [InlineData(4, true)]               // Valid
    [InlineData(5, true)]               // Valid
    [InlineData(50, true)]               // Valid
    [InlineData(51, false)]               // Invalid - too long
    [InlineData(52, false)]               // Invalid - too long
    public void UsernameTest(int sizeName, bool expectedResult)
    {
        // Arrange
        var userName = UserTestData.GenerateUserNameWithLength(sizeName);
        var validator = new UserNameValidator();

        // Act
        var result = validator.Validate(userName);

        // Assert
        result.IsValid.Should().Be(expectedResult);
    }
}

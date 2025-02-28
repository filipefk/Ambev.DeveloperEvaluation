using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;
public class EnumStatusValidatorTest
{
    [Theory(DisplayName = "Status cannot be 'Unknown'")]
    [InlineData(UserStatus.Unknown, false)]   // Invalid - Unknown
    [InlineData(UserStatus.Active, true)]     // Valid - Active
    [InlineData(UserStatus.Inactive, true)]   // Valid - Inactive
    [InlineData(UserStatus.Suspended, true)]  // Valid - Suspended
    public void RoleTest(UserStatus enumValue, bool expectedResult)
    {
        // Arrange
        var validator = new EnumStatusValidator();

        // Act
        var result = validator.Validate(enumValue);

        // Assert
        result.IsValid.Should().Be(expectedResult);
    }
}



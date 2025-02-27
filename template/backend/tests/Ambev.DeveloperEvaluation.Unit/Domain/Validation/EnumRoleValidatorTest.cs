using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class EnumRoleValidatorTest
{
    [Theory(DisplayName = "Role cannot be 'None'")]
    [InlineData(UserRole.None, false)]     // Invalid - None
    [InlineData(UserRole.Customer, true)]  // Valid - Customer
    [InlineData(UserRole.Manager, true)]   // Valid - Manager
    [InlineData(UserRole.Admin, true)]     // Valid - Admin
    public void RoleTest(UserRole enumValue, bool expectedResult)
    {
        // Arrange
        var validator = new EnumRoleValidator();

        // Act
        var result = validator.Validate(enumValue);

        // Assert
        result.IsValid.Should().Be(expectedResult);
    }
}
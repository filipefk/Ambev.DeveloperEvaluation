using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class UserIdValidatorTest
{

    [Fact(DisplayName = "User Id is not empty and valid")]
    public void Not_Empty_UserId()
    {
        // Arrange
        var validator = new UserIdValidator();
        var userId = Guid.NewGuid();
        // Act
        var result = validator.Validate(userId);
        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact(DisplayName = "User is empty and invalid")]
    public void Empty_UserId()
    {
        // Arrange
        var validator = new UserIdValidator();
        var userId = new Guid();
        // Act
        var result = validator.Validate(userId);
        // Assert
        result.IsValid.Should().BeFalse();
    }
}
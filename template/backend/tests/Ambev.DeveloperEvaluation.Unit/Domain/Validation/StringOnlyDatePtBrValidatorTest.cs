using Ambev.DeveloperEvaluation.Domain.Validation.Date;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class StringOnlyDatePtBrValidatorTest
{
    [Theory(DisplayName = "Given a string date When validating Then should validate only dd/MM/yyyy format")]
    [InlineData("10/05/2025", true)]
    [InlineData("2025/10/18", false)]
    [InlineData("10-07-2019", false)]
    [InlineData("30/02/2000", false)]
    [InlineData("2020-01-01", false)]
    [InlineData("14/04/1972", true)]
    [InlineData("0123456789", false)]
    [InlineData("", false)]
    [InlineData("////", false)]
    public void Given_String_Date_When_Validating_Then_Should_Validate_Only_Correct_Format(string date, bool expectedResult)
    {
        // Arrange
        var validator = new StringOnlyDatePtBrValidator();

        // Act
        var result = validator.Validate(date);

        // Assert
        result.IsValid.Should().Be(expectedResult);
    }
}

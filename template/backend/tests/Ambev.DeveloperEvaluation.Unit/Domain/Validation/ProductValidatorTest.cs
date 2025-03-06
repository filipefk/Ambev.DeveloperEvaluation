using Ambev.DeveloperEvaluation.Domain.Validation.Product;
using FluentValidation.TestHelper;
using TestUtil.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class ProductValidatorTest
{
    private readonly ProductValidator _validator;

    public ProductValidatorTest()
    {
        _validator = new ProductValidator();
    }

    [Fact(DisplayName = "Given valid product When validate Then should pass all validation rules")]
    public void Given_ValidUser_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var product = ProductBuilder.Build();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Given empty id When validate Then should fail validation")]
    public void Given_Empty_Id_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Id = new Guid();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Id);
    }

    [Fact(DisplayName = "Given empty title When validate Then should fail validation")]
    public void Given_Empty_Title_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Title = "";

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Title);
    }

    [Fact(DisplayName = "Too large title When validate Then should fail validation")]
    public void Given_Too_Large_Title_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Title = new string('A', 101);

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Title);
    }

    [Fact(DisplayName = "Given empty description When validate Then should fail validation")]
    public void Given_Empty_Description_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Description = "";

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Description);
    }

    [Fact(DisplayName = "Too large description When validate Then should fail validation")]
    public void Given_Too_Large_Description_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Description = new string('A', 501);

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Description);
    }

    [Fact(DisplayName = "Given zero price When validate Then should fail validation")]
    public void Given_Zero_Price_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Price = 0;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Price);
    }

    [Fact(DisplayName = "Given empty category When validate Then should fail validation")]
    public void Given_Empty_Category_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Category = "";

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Category);
    }

    [Fact(DisplayName = "Too large category When validate Then should fail validation")]
    public void Given_Too_Large_Category_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Category = new string('A', 101);

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Category);
    }

    [Fact(DisplayName = "Given negative rating rate When validate Then should fail validation")]
    public void Given_Negative_Rating_Rate_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Rating.Rate = -1;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Rating.Rate);
    }

    [Fact(DisplayName = "Given negative rating count When validate Then should fail validation")]
    public void Given_Negative_Rating_Count_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = ProductBuilder.Build();
        product.Rating.Count = -1;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(product => product.Rating.Count);
    }
}

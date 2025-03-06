using FluentAssertions;
using System.Net;
using TestUtil.HttpUtil;
using TestUtil.Token;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Product.GetProduct;

public class GetProductTest : AmbevClassFixture
{
    private readonly string _baseUrl = "api/Product";

    private readonly Domain.Entities.User _activeUser;
    private readonly Domain.Entities.Product _product;

    public GetProductTest(CustomWebApplicationFactory factory) : base(factory)
    {
        var entities = factory.GetEntitiesCreated();
        _activeUser = (Domain.Entities.User)entities["ActiveUser"];
        _product = (Domain.Entities.Product)entities["Product"];
    }

    [Fact(DisplayName = "Given valid product id When get Then returns ok")]
    public async Task Success()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        // When
        var response = await DoGet($"{_baseUrl}/{_product.Id}", token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await HttpResponseUtil.GetResponseData(response);

        responseData.RootElement
            .GetProperty("data")
            .Should()
            .NotBeNull();

        responseData.RootElement
            .GetProperty("data").GetProperty("id").GetString()
            .Should().NotBeNullOrWhiteSpace();

        responseData.RootElement
            .GetProperty("data").GetProperty("title").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_product.Title);

        responseData.RootElement
            .GetProperty("data").GetProperty("price").GetDecimal()
            .Should().Be(_product.Price);

        responseData.RootElement
            .GetProperty("data").GetProperty("description").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_product.Description);

        responseData.RootElement
            .GetProperty("data").GetProperty("category").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_product.Category);

        responseData.RootElement
            .GetProperty("data").GetProperty("image").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_product.Image);

        responseData.RootElement
            .GetProperty("data").GetProperty("rating").GetProperty("rate").GetDecimal()
            .Should().Be(_product.Rating.Rate);

        responseData.RootElement
            .GetProperty("data").GetProperty("rating").GetProperty("count").GetInt64()
            .Should().Be(_product.Rating.Count);

        responseData.RootElement
            .GetProperty("success").GetBoolean()
            .Should().BeTrue();

        responseData.RootElement
            .GetProperty("message").GetString()
            .Should().BeNullOrWhiteSpace();

        responseData.RootElement
            .GetProperty("errors").GetArrayLength()
            .Should().Be(0);
    }

    [Fact(DisplayName = "Given invalid token When get Then returns Unauthorized")]
    public async Task Error_Token_Invalid()
    {
        // Given
        var token = "invalidToken";

        // When
        var response = await DoGet($"{_baseUrl}/{_product.Id}", token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }

    [Fact(DisplayName = "Given invalid product id When get Then returns BadRequest")]
    public async Task Error_Invalid_Product_Id()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        // When
        var response = await DoDelete($"{_baseUrl}/{new Guid()}", token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await HttpResponseUtil.GetErrorValidationMessages(response);

        responseData.Count().Should().Be(1);

        var firstElement = responseData.First();

        firstElement.GetProperty("propertyName").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("Id");

        firstElement.GetProperty("errorMessage").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("Product ID is required");
    }

    [Fact(DisplayName = "Given non-existent product id When get Then returns NotFound")]
    public async Task Error_Non_Existent_Product_Id()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        var newGuid = Guid.NewGuid();

        // When
        var response = await DoDelete($"{_baseUrl}/{newGuid}", token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseData = await HttpResponseUtil.GetResponseData(response);

        responseData.RootElement
            .GetProperty("success").GetBoolean()
            .Should().BeFalse();

        responseData.RootElement
            .GetProperty("message").GetString()
            .Should().Be($"Product with ID {newGuid} not found");

        responseData.RootElement
            .GetProperty("errors").GetArrayLength()
            .Should().Be(0);
    }
}

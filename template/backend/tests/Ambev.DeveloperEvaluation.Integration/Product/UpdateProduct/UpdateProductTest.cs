using FluentAssertions;
using System.Net;
using TestUtil.HttpUtil;
using TestUtil.Request;
using TestUtil.Token;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Product.UpdateProduct;

public class UpdateProductTest : AmbevClassFixture
{
    private readonly string _baseUrl = "api/Product";

    private readonly Domain.Entities.User _activeUser;
    private readonly Domain.Entities.Product _product;

    public UpdateProductTest(CustomWebApplicationFactory factory) : base(factory)
    {
        var entities = factory.GetEntitiesCreated();
        _activeUser = (Domain.Entities.User)entities["ActiveUser"];
        _product = (Domain.Entities.Product)entities["Product"];
    }

    [Fact(DisplayName = "Given valid product data When update Then returns ok")]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        var request = UpdateProductRequestBuilder.Build();

        var response = await DoPut($"{_baseUrl}/{_product.Id}", request, token);

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
            .And.Be(request.Title);

        responseData.RootElement
            .GetProperty("data").GetProperty("price").GetDecimal()
            .Should().Be(request.Price);

        responseData.RootElement
            .GetProperty("data").GetProperty("description").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(request.Description);

        responseData.RootElement
            .GetProperty("data").GetProperty("category").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(request.Category);

        responseData.RootElement
            .GetProperty("data").GetProperty("image").GetString()
            .Should().Be(request.Image);

        responseData.RootElement
            .GetProperty("data").GetProperty("rating").GetProperty("rate").GetDecimal()
            .Should().Be(request.Rating.Rate);

        responseData.RootElement
            .GetProperty("data").GetProperty("rating").GetProperty("count").GetInt64()
            .Should().Be(request.Rating.Count);

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

    [Fact(DisplayName = "Given invalid token When update Then returns Unauthorized")]
    public async Task Error_Token_Invalid()
    {
        // Given
        var token = "invalidToken";

        var request = UpdateProductRequestBuilder.Build();

        // When
        var response = await DoPut($"{_baseUrl}/{_product.Id}", request, token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }

    [Fact(DisplayName = "Given invalid product id When update Then returns BadRequest")]
    public async Task Error_Invalid_Product_Id()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        var request = UpdateProductRequestBuilder.Build();

        // When
        var response = await DoPut($"{_baseUrl}/{new Guid()}", request, token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await HttpResponseUtil.GetResponseData(response);

        responseData.RootElement
            .GetProperty("success").GetBoolean()
            .Should().BeFalse();

        responseData.RootElement
            .GetProperty("message").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("Validation Failed");

        var errorMessages = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errorMessages.Count().Should().Be(1);

        var firstElement = errorMessages.First();

        firstElement.GetProperty("error").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("NotEmptyValidator");

        firstElement.GetProperty("detail").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("Product ID is required");
    }

    [Fact(DisplayName = "Given non-existent product id When update Then returns NotFound")]
    public async Task Error_Non_Existent_Product_Id()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        var newGuid = Guid.NewGuid();

        var request = UpdateProductRequestBuilder.Build();

        // When
        var response = await DoPut($"{_baseUrl}/{newGuid}", request, token);

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

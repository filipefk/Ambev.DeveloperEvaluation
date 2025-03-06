using FluentAssertions;
using System.Net;
using TestUtil.HttpUtil;
using TestUtil.Token;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Product.ListProducts;

public class ListProductsTest : AmbevClassFixture
{
    private readonly string _baseUrl = "api/Product";

    private readonly Domain.Entities.User _activeUser;
    private readonly Domain.Entities.Product _product;
    private readonly CustomWebApplicationFactory _factory;

    public ListProductsTest(CustomWebApplicationFactory factory) : base(factory)
    {
        var entities = factory.GetEntitiesCreated();
        _activeUser = (Domain.Entities.User)entities["ActiveUser"];
        _product = (Domain.Entities.Product)entities["Product"];
        _factory = factory;
    }

    [Fact(DisplayName = "Given exists products When list Then returns ok")]
    public async Task Success()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        // When
        var response = await DoGet($"{_baseUrl}?_page=1&_size=10&_order=title", token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await HttpResponseUtil.GetResponseData(response);

        responseData.RootElement
            .GetProperty("currentPage").GetInt16()
            .Should().Be(1);

        responseData.RootElement
            .GetProperty("totalPages").GetInt16()
            .Should().Be(1);

        responseData.RootElement
            .GetProperty("totalCount").GetInt16()
            .Should().Be(1);

        responseData.RootElement
            .GetProperty("data")
            .Should()
            .NotBeNull();

        var dataArray = responseData.RootElement.GetProperty("data").EnumerateArray();

        dataArray.Count().Should().Be(1);

        var first = dataArray.First();

        first.GetProperty("title").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_product.Title);

        first.GetProperty("price").GetDecimal()
            .Should().Be(_product.Price);

        first.GetProperty("description").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_product.Description);

        first.GetProperty("category").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_product.Category);

        first.GetProperty("image").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_product.Image);

        first.GetProperty("rating").GetProperty("rate").GetDecimal()
            .Should().Be(_product.Rating.Rate);

        first.GetProperty("rating").GetProperty("count").GetInt64()
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
        var response = await DoGet($"{_baseUrl}", token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }

    [Fact(DisplayName = "Given empty product database When get Then returns NotFound")]
    public async Task Error_Non_Existent_Products()
    {
        // Given
        var entities = _factory.GetEntitiesCreated();
        var activeUser = (Domain.Entities.User)entities["ActiveUser"];
        var product = (Domain.Entities.Product)entities["Product"];

        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(activeUser);

        await DoDelete($"{_baseUrl}/{product.Id}", token);

        // When
        var response = await DoGet($"{_baseUrl}?_page=1&_size=10&_order=title", token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseData = await HttpResponseUtil.GetResponseData(response);

        responseData.RootElement
            .GetProperty("success").GetBoolean()
            .Should().BeFalse();

        responseData.RootElement
            .GetProperty("message").GetString()
            .Should().Be($"No products found");

        responseData.RootElement
            .GetProperty("errors").GetArrayLength()
            .Should().Be(0);
    }
}

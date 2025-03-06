using FluentAssertions;
using System.Net;
using TestUtil.HttpUtil;
using TestUtil.Request;
using TestUtil.Token;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Product.CreateProduct;

public class CreateProductTest : AmbevClassFixture
{
    private readonly string _baseUrl = "api/Product";

    private readonly Domain.Entities.User _activeUser;

    public CreateProductTest(CustomWebApplicationFactory factory) : base(factory)
    {
        var entities = factory.GetEntitiesCreated();
        _activeUser = (Domain.Entities.User)entities["ActiveUser"];
    }

    [Fact(DisplayName = "Given valid product data When create Then returns created response and product")]
    public async Task Success()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        var request = CreateProductRequestBuilder.Build();

        // When
        var response = await DoPost(_baseUrl, request, token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.Created);

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
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(request.Image);

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

    [Fact(DisplayName = "Given valid product data without image When create Then returns created response and product")]
    public async Task Success_Without_Image()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        var request = CreateProductRequestBuilder.Build();
        request.Image = "";

        // When
        var response = await DoPost(_baseUrl, request, token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.Created);

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
            .Should().BeNullOrWhiteSpace();

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

    [Fact(DisplayName = "Given invalid token When delete Then returns Unauthorized")]
    public async Task Error_Token_Invalid()
    {
        // Given
        var request = CreateProductRequestBuilder.Build();

        var token = "invalidToken";

        // When
        var response = await DoPost(_baseUrl, request, token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }

    [Fact(DisplayName = "Given product data without title When create Then returns BadRequest and The Title cannot be empty.")]
    public async Task Error_Without_Title()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        var request = CreateProductRequestBuilder.Build();
        request.Title = "";

        // When
        var response = await DoPost(_baseUrl, request, token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await HttpResponseUtil.GetErrorValidationMessages(response);

        responseData.Count().Should().Be(1);

        var firstElement = responseData.First();

        firstElement.GetProperty("propertyName").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("Title");

        firstElement.GetProperty("errorMessage").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("The Title cannot be empty.");
    }

    [Fact(DisplayName = "Given product data with price zero When create Then returns BadRequest and The Price must be greater than zero.")]
    public async Task Error_With_Price_Zero()
    {
        // Given
        var token = JwtTokenGeneratorBuilder.Build().GenerateToken(_activeUser);

        var request = CreateProductRequestBuilder.Build();
        request.Price = 0;

        // When
        var response = await DoPost(_baseUrl, request, token);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await HttpResponseUtil.GetErrorValidationMessages(response);

        responseData.Count().Should().Be(1);

        var firstElement = responseData.First();

        firstElement.GetProperty("propertyName").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("Price");

        firstElement.GetProperty("errorMessage").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("The Price must be greater than zero.");
    }
}
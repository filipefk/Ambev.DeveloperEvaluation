using FluentAssertions;
using System.Net;
using TestUtil.HttpUtil;
using TestUtil.Request;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Auth;

public class AuthTest : AmbevClassFixture
{
    private readonly string _baseUrl = "api/Auth";

    private readonly Domain.Entities.User _activeUser;
    private readonly Domain.Entities.User _inactiveUser;

    public AuthTest(CustomWebApplicationFactory factory) : base(factory)
    {
        var entities = factory.GetEntitiesCreated();
        _activeUser = (Domain.Entities.User)entities["ActiveUser"];
        _inactiveUser = (Domain.Entities.User)entities["InactiveUser"];
    }

    [Fact(DisplayName = "Given valid user data When authenticate Then returns token, user data and success response")]
    public async Task Success()
    {
        // Given
        var request = AuthenticateUserRequestBuilder.Build(_activeUser);

        // When
        var response = await DoPost(_baseUrl, request);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await HttpResponseUtil.GetResponseData(response);

        responseData.RootElement
            .GetProperty("data")
            .Should()
            .NotBeNull();

        responseData.RootElement
            .GetProperty("data").GetProperty("token").GetString()
            .Should().NotBeNullOrWhiteSpace();

        responseData.RootElement
            .GetProperty("data").GetProperty("email").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_activeUser.Email);

        responseData.RootElement
            .GetProperty("data").GetProperty("userName").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be(_activeUser.Username);

        responseData.RootElement
            .GetProperty("success").GetBoolean()
            .Should().BeTrue();

        responseData.RootElement
            .GetProperty("message").GetString()
            .Should().BeNullOrWhiteSpace();

    }

    [Fact(DisplayName = "Given invalid user data When authenticate Then returns Unauthorized and Invalid credentials")]
    public async Task Error_Invalid_Credentials()
    {
        // Given
        var request = AuthenticateUserRequestBuilder.Build();

        // When
        var response = await DoPost(_baseUrl, request);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseData = await HttpResponseUtil.GetResponseData(response);

        responseData.RootElement
            .GetProperty("success").GetBoolean()
            .Should().BeFalse();

        responseData.RootElement
            .GetProperty("message").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("Invalid credentials");
    }

    [Fact(DisplayName = "Given invalid user data When authenticate Then returns Unauthorized and Invalid credentials")]
    public async Task Error_User_Is_Not_Active()
    {
        // Given
        var request = AuthenticateUserRequestBuilder.Build(_inactiveUser);

        // When
        var response = await DoPost(_baseUrl, request);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseData = await HttpResponseUtil.GetResponseData(response);

        responseData.RootElement
            .GetProperty("success").GetBoolean()
            .Should().BeFalse();

        responseData.RootElement
            .GetProperty("message").GetString()
            .Should().NotBeNullOrWhiteSpace()
            .And.Be("User is not active");
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Bogus;

namespace TestUtil.Request;

public class AuthenticateUserRequestBuilder
{
    public static AuthenticateUserRequest Build()
    {
        return new Faker<AuthenticateUserRequest>()
            .RuleFor(user => user.Email, fake => fake.Internet.Email())
            .RuleFor(user => user.Password, fake => fake.Internet.Password());
    }

    public static AuthenticateUserRequest Build(User user)
    {
        return new AuthenticateUserRequest()
        {
            Email = user.Email,
            Password = user.Password
        };
    }
}
